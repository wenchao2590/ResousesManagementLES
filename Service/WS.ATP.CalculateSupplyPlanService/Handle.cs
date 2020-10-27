using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BLL.LES;
using DM.LES;
using BLL.SYS;
using System.Xml;
using Infrustructure.Utilities;
using DM.SYS;
using Infrustructure.Logging;
using DAL.LES;

namespace WS.ATP.CalculateSupplyPlanService
{
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "CalculateSupplyPlanService";
        ///系统配置CALCULATE_SUPPLY_PLAN_FLAG(是否启用供货计划计算) = true
        ///true时执行以下逻辑
        ///false时直接执行更新生产订单及物料清单的逻辑而不生成供货计划
        string calculateSupplyPlanFlag = new ConfigBLL().GetValueByCode("CALCULATE_SUPPLY_PLAN_FLAG");
        /// <summary>
        /// 物料需求提前天数
        /// </summary>
        string materialRequireAdvanceDays = new ConfigBLL().GetValueByCode("MATERIAL_REQUIRE_ADVANCE_DAYS");
        #endregion

        #region Handler
        public void Handler()
        {
            #region 基础变量
            ///最近一条的中间表数据状态
            int processFlag = 0;
            ///本次线程已处理的中间表主键
            List<long> dealedIds = new List<long>();
            //数据库执行语句
            StringBuilder @string = new StringBuilder();
            ///物料需求提前天数
            int.TryParse(materialRequireAdvanceDays, out int intMaterialRequrieAdvanceDays);
            intMaterialRequrieAdvanceDays = 0 - intMaterialRequrieAdvanceDays;
            #endregion

            while (processFlag != 10)
            {
                ///开始处理时间
                DateTime startExecuteTime = DateTime.Now;
                ///获取状态⑮为10.未处理的SAP生产订单数据
                ///因为后续处理过程较为复杂，所以一次获取一条ID主键最靠前的10.未处理或30.挂起或40.逆处理状态数据
                ///当上一条处理数据为30.挂起状态时需要继续处理下一条，否则执行结束
                ///也就意味着30.挂起的数据将优先处理且为了保障挂起数据不影响正常未处理数据而设定的逻辑
                #region 获取待处理的数据
                string textWhere = "[PROCESS_FLAG] in (" + (int)ProcessFlagConstants.Untreated + "," + (int)ProcessFlagConstants.Suspend + "," + (int)ProcessFlagConstants.ConverseProgress + ")";
                ///集合大于0，排除
                if (dealedIds.Count > 0)
                    textWhere += "and [ID] not in (" + string.Join(",", dealedIds.ToArray()) + ")";
                ///
                SapProductOrderInfo sapProductOrderInfo = new SapProductOrderBLL().GetTopOneInfo(textWhere, "[ID] asc");
                if (sapProductOrderInfo == null)
                    throw new Exception("MC:3x00000015");///没有已启用的SAP生产订单信息
                if (sapProductOrderInfo.OnlineDate == null)
                    throw new Exception("MC:3x00000033");///SAP生产订单上线日期信息错误


                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":Start " + sapProductOrderInfo.Aufnr);
                ///SAP生产订单上线日期
                string sapProductOrderDate = sapProductOrderInfo.OnlineDate.GetValueOrDefault().ToString("yyyyMMdd");
                ///处理状态
                processFlag = sapProductOrderInfo.ProcessFlag.GetValueOrDefault();
                ///
                dealedIds.Add(sapProductOrderInfo.Id);
                #endregion

                #region SAP生产订单物料清单
                ///获取SAP生产订单中订单号⑤=③对应的SAP生产订单物料清单
                SapProductOrderBomInfo sapProductOrderBomInfo = new SapProductOrderBomBLL().GetInfoByAufnr(sapProductOrderInfo.Aufnr);
                ///若此时未能获取到数据则表示该SAP生产订单的物料清单还未能从SAP成功接收到
                if (sapProductOrderBomInfo == null)
                {
                    ///需要终止该条生产订单的处理，并且标记为挂起状态⑮
                    new SapProductOrderBLL().UpdateInfo("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", sapProductOrderInfo.Id);
                    continue;
                }
                #endregion    

                #region 订单物料清单XML解析
                ///SAP生产订单物料清单中的物料信息⑦需要XML解析后并逐条以工厂③=②、物料号①=⑦.MATNR、供应商②=⑦.LIFNR
                XmlWrapper xmlWrapper = new XmlWrapper(sapProductOrderBomInfo.Matnrs, LoadType.FromString);
                List<object> objMatnrs = xmlWrapper.XmlToList("/MatnrsAll/Matnrs", typeof(Matnrs));
                if (objMatnrs.Count == 0)
                    throw new Exception("0x00000182");///无订单物料清单
                List<string> partNos = new List<string>();
                List<string> supplierNums = new List<string>();
                foreach (Matnrs matnr in objMatnrs)
                {
                    partNos.Add(matnr.Matnr);
                    supplierNums.Add(matnr.Lifnr);
                }
                List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(partNos);
                if (maintainPartsInfos.Count == 0)
                    throw new Exception("0x00000182");///无订单物料清单
                List<SupplierInfo> supplierInfos = new SupplierBLL().GetListForInterfaceDataSync(supplierNums);
                #endregion

                ///需根据SAP生产订单中的上线日期⑩判定供货计划TT_ATP_SUPPLY_PLAN中的对应日期列是否存在
                ///日期列的列名规则为yyyyMMdd数据类型为decimal(18,4)，不存在则需要新建列
                ProcessSupplyPlanDate(sapProductOrderInfo.OnlineDate.GetValueOrDefault().AddDays(intMaterialRequrieAdvanceDays));
                ///
                string plant = new PlantBLL().GetPlantBySapPlantCode(sapProductOrderInfo.Dwerk);
                if (string.IsNullOrEmpty(plant))
                    throw new Exception("3x00000016");///工厂不存在
                string assemblyLine = new AssemblyLineBLL().GetAssemblyLineBySapAssemblyLine(sapProductOrderInfo.Verid);
                if (string.IsNullOrEmpty(assemblyLine))
                    throw new Exception("3x00000017");///产线不存在

                ///根据生产订单号⑤=①获取生产订单TT_BAS_PULL_ORDERS数据
                PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(sapProductOrderInfo.Aufnr);
                ///生产订单上线日期
                string productOrderDate = sapProductOrderDate;
                ///供货日期
                string materialRequireDate = sapProductOrderInfo.OnlineDate.GetValueOrDefault().AddDays(intMaterialRequrieAdvanceDays).ToString("yyyyMMdd");
                #region 首次下发
                ///若此时未能成功获取数据则表示该生产订单为首次下发,且不是删除的生产订单
                if (pullOrdersInfo == null && string.IsNullOrEmpty(sapProductOrderInfo.Zsc))
                {
                    if (calculateSupplyPlanFlag.ToLower() == "true")
                    {
                        #region 供货计划
                        foreach (Matnrs matnr in objMatnrs)
                        {
                            decimal partQty = 0;
                            decimal.TryParse(matnr.Bdmng, out partQty);
                            MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == matnr.Matnr);
                            string partCname = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartCname.Replace("'", "''");
                            string partPurchaser = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartPurchaser;
                            SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == matnr.Lifnr);
                            string supplierName = supplierInfo == null ? string.Empty : supplierInfo.SupplierName;
                            ///若不存在则insert，再以工厂③、物料号①、供应商②更新其对应日期的物料数量
                            @string.AppendFormat(@"
                                                if not exists (select 1 from [LES].[TT_ATP_SUPPLY_PLAN] with(nolock) where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}' and [VALID_FLAG] = 1)
                                                begin
                                                    insert into [LES].[TT_ATP_SUPPLY_PLAN] ([FID],[PART_NO],[PART_CNAME],[PART_PURCHASER],[SUPPLIER_NUM],[SUPPLIER_NAME],[PLANT],[VALID_FLAG],[CREATE_USER],[CREATE_DATE]) values (NEWID(),N'{0}',N'{6}',N'{7}',N'{1}',N'{8}',N'{2}',1,N'{5}',GETDATE());
                                                end
                                                update [LES].[TT_ATP_SUPPLY_PLAN] set [PART_CNAME] = N'{6}',[PART_PURCHASER] = N'{7}',[SUPPLIER_NAME] = N'{8}',[{3}] = ISNULL([{3}] , 0) + {4},[MODIFY_USER] = N'{5}',[MODIFY_DATE] = GETDATE() where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}';"
                                               , matnr.Matnr, matnr.Lifnr, plant, materialRequireDate, partQty, loginUser, partCname, partPurchaser, supplierName);
                        }
                        #endregion
                    }

                    #region 生产订单
                    ///
                    pullOrdersInfo = PullOrdersBLL.CreatePullOrdersInfo(loginUser);
                    ///
                    PullOrdersBLL.GetPullOrdersInfo(sapProductOrderInfo, ref pullOrdersInfo);
                    ///WERK,接口_工厂
                    pullOrdersInfo.Werk = plant;
                    ///ORDER_DATE,订单日期
                    pullOrdersInfo.OrderDate = BLL.LES.CommonBLL.TryParseDatetime(sapProductOrderDate);
                    ///ASSEMBLY_LINE,工厂模型_流水线
                    pullOrdersInfo.AssemblyLine = assemblyLine;
                    ///PLAN_EXECUTE_TIME,计划执行时间
                    pullOrdersInfo.PlanExecuteTime = BLL.LES.CommonBLL.TryParseDatetime(sapProductOrderDate);
                    ///
                    @string.AppendLine(PullOrdersDAL.GetInsertSql(pullOrdersInfo));
                    ///并批量插入生产订单物料清单（参见TT_BAS_PULL_ORDER_BOM备注中的对应关系）
                    WmsVmiProductOrderInfo wmsVmiProductOrderInfo = WmsVmiProductOrderBLL.CreateWmsVmiProductOrderInfo(loginUser);
                    ///
                    WmsVmiProductOrderBLL.GetWmsVmiProductOrderInfo(pullOrdersInfo, ref wmsVmiProductOrderInfo);
                    ///
                    wmsVmiProductOrderInfo.DownLineTime = sapProductOrderInfo.OfflineDate;
                    wmsVmiProductOrderInfo.OnlineTime = sapProductOrderInfo.OnlineDate;
                    wmsVmiProductOrderInfo.LockFlag = sapProductOrderInfo.LockFlag;

                    @string.AppendLine(WmsVmiProductOrderDAL.GetInsertSql(wmsVmiProductOrderInfo));
                    @string.AppendLine(BLL.LES.CommonBLL.GetCreateOutboundLogSql("VMI", wmsVmiProductOrderInfo.LogFid.GetValueOrDefault(), "LES-WMS-012", wmsVmiProductOrderInfo.OrderNo, loginUser));

                    foreach (Matnrs matnr in objMatnrs)
                    {
                        decimal partQty = 0;
                        decimal.TryParse(matnr.Bdmng, out partQty);
                        MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == matnr.Matnr);
                        string partCname = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartCname;
                        @string.AppendFormat(@"insert into [LES].[TT_BAS_PULL_ORDER_BOM] 
([FID],[ORDERFID],[ZORDNO],[ZKWERK],[ZBOMID],[ZCOMNO],[ZQTY],[ZLOC],[SUPPLIER_NUM],[PLATFORM],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[ZCOMDS],[ZDATE]) 
values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',{5},N'{6}',N'{7}',N'{8}',N'{9}',GETDATE(),1,N'{10}',N'{11}');"
, pullOrdersInfo.Fid.GetValueOrDefault(), sapProductOrderInfo.Aufnr, plant, matnr.Aennr, matnr.Matnr, partQty, matnr.Ebort, matnr.Lifnr, matnr.Platform, loginUser, partCname, sapProductOrderDate);
                    }
                    #endregion

                    #region 计划拉动状态
                    ///获取已启用的计划零件类的零件类外键①
                    ///并将TT_BAS_PULL_ORDERS的订单外键②写入TT_MPM_PLAN_PULL_CREATE_STATUS
                    ///其中的状态③为10.未生成（20.已生成，在系统代码中创建CREATE_STATUS，还需修改计划拉动单生成逻辑中的对应枚举项）
                    List<PlanPartBoxInfo> planPartBoxInfos = new PlanPartBoxBLL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", string.Empty);
                    ///
                    foreach (PlanPartBoxInfo planPartBoxInfo in planPartBoxInfos)
                    {
                        @string.AppendFormat(@"insert into [LES].[TT_MPM_PLAN_PULL_CREATE_STATUS] 
(FID,PART_BOX_FID,ORDER_FID,STATUS,CREATE_USER,CREATE_DATE,VALID_FLAG) 
values (NEWID(),N'{0}',N'{1}',{2},'{3}',GETDATE(),1);"
, planPartBoxInfo.Fid.GetValueOrDefault(), pullOrdersInfo.Fid.GetValueOrDefault(), (int)CreateStatusConstants.NotGenerated, loginUser);
                    }
                    #endregion

                    ///同时更新SAP生产订单物料清单处理状态⑮为20.已处理
                    @string.AppendFormat(@"update [LES].[TI_IFM_SAP_PRODUCT_ORDER_BOM] 
set PROCESS_FLAG = {0},PROCESS_TIME = GETDATE(),[MODIFY_USER] = N'{1}',[MODIFY_DATE] = GETDATE() where [ID] = {2};"
, (int)ProcessFlagConstants.Processed, loginUser, sapProductOrderBomInfo.Id);
                }
                #endregion

                #region 不是首次下发
                else
                {
                    if (calculateSupplyPlanFlag.ToLower() == "true")
                    {
                        ///若之前成功获取了生产订单则比对上线日期⑩⑤是否较SAP生产订单有变化
                        #region 供货计划
                        ///获取生产订单物料清单
                        List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomBLL().GetList("[ZORDNO] = N'" + pullOrdersInfo.OrderNo + "'", string.Empty);
                        ///SAP订单删除，需要重新计算供货计划
                        if (sapProductOrderInfo.Zsc.ToUpper() == "X")
                        {
                            ///需要根据生产订单物料清单TT_BAS_PULL_ORDER_BOM和生产订单的订单日期⑤扣减供货计划                              
                            foreach (PullOrderBomInfo pullOrderBomInfo in pullOrderBomInfos)
                            {
                                @string.AppendLine("update [LES].[TT_ATP_SUPPLY_PLAN] " +
                                    "set [" + materialRequireDate + "] = ISNULL([" + materialRequireDate + "] , 0) - " + pullOrderBomInfo.Zqty.GetValueOrDefault() + "," +
                                    "[MODIFY_USER] = N'" + loginUser + "'," +
                                    "[MODIFY_DATE] = GETDATE() " +
                                    "where [PART_NO] = N'" + pullOrderBomInfo.Zcomno + "' and " +
                                    "[SUPPLIER_NUM] = N'" + pullOrderBomInfo.SupplierNum + "' and " +
                                    "[PLANT] = N'" + plant + "';");
                            }
                        }
                        else
                        {
                            ///若日期无变化⑩=⑤处理状态⑮为10.未处理或30.挂起时不需要更新供货计划
                            if (sapProductOrderInfo.OnlineDate.GetValueOrDefault() == pullOrdersInfo.OrderDate.GetValueOrDefault())
                            {
                                ///若处理状态⑮为40.逆处理时
                                if (processFlag == (int)ProcessFlagConstants.ConverseProgress)
                                {
                                    ///根据SAP生产订单上线日期⑩及物料清单扣减供货计划
                                    foreach (Matnrs matnr in objMatnrs)
                                    {
                                        decimal partQty = 0;
                                        decimal.TryParse(matnr.Bdmng, out partQty);
                                        @string.AppendFormat(@"update [LES].[TT_ATP_SUPPLY_PLAN] 
set [{3}] = ISNULL([{3}] , 0) - {4},[MODIFY_USER] = N'{5}',[MODIFY_DATE] = GETDATE()
where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}';"
    , matnr.Matnr, matnr.Lifnr, plant, materialRequireDate, partQty, loginUser);
                                    }
                                    ///再以生产订单⑤日期及物料清单累加供货计划
                                    foreach (PullOrderBomInfo pullOrderBomInfo in pullOrderBomInfos)
                                    {
                                        MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == pullOrderBomInfo.Zcomno);
                                        string partCname = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartCname;
                                        string partPurchaser = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartPurchaser;
                                        SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == pullOrderBomInfo.SupplierNum);
                                        string supplierName = supplierInfo == null ? string.Empty : supplierInfo.SupplierName;
                                        @string.AppendFormat(@"update [LES].[TT_ATP_SUPPLY_PLAN] 
set [PART_CNAME] = N'{6}',[PART_PURCHASER] = N'{7}',[SUPPLIER_NAME] = N'{8}',[{3}] = ISNULL([{3}] , 0) + {4},[MODIFY_USER] = N'{5}',[MODIFY_DATE] = GETDATE()
where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}';"
    , pullOrderBomInfo.Zcomno, pullOrderBomInfo.SupplierNum, plant, materialRequireDate, pullOrderBomInfo.Zqty.GetValueOrDefault(), loginUser, partCname, partPurchaser, supplierName);
                                    }
                                }
                            }
                            ///日期有变化⑩<>⑤
                            else
                            {
                                ///SAP生产订单处理状态⑮为10.未处理或30.挂起时
                                if (processFlag == (int)ProcessFlagConstants.Untreated || processFlag == (int)ProcessFlagConstants.Suspend)
                                {
                                    ///需要根据生产订单物料清单TT_BAS_PULL_ORDER_BOM和生产订单的订单日期⑤扣减供货计划                              
                                    foreach (PullOrderBomInfo pullOrderBomInfo in pullOrderBomInfos)
                                    {
                                        @string.AppendFormat(@"update [LES].[TT_ATP_SUPPLY_PLAN] 
set [{3}] = ISNULL([{3}] , 0) - {4},[MODIFY_USER] = N'{5}',[MODIFY_DATE] = GETDATE() 
where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}';"
    , pullOrderBomInfo.Zcomno, pullOrderBomInfo.SupplierNum, plant, materialRequireDate, pullOrderBomInfo.Zqty.GetValueOrDefault(), loginUser);
                                    }
                                    ///同时根据SAP生产订单上线日期⑩及物料清单累加供货计划
                                    foreach (Matnrs matnr in objMatnrs)
                                    {
                                        decimal partQty = 0;
                                        decimal.TryParse(matnr.Bdmng, out partQty);
                                        MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == matnr.Matnr);
                                        string partCname = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartCname;
                                        string partPurchaser = maintainPartsInfo == null ? string.Empty : maintainPartsInfo.PartPurchaser;
                                        SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == matnr.Lifnr);
                                        string supplierName = supplierInfo == null ? string.Empty : supplierInfo.SupplierName;
                                        @string.AppendFormat(@"update [LES].[TT_ATP_SUPPLY_PLAN] 
set [PART_CNAME] = N'{6}',[PART_PURCHASER] = N'{7}',[SUPPLIER_NAME] = N'{8}',[{3}] = ISNULL([{3}] , 0) + {4},[MODIFY_USER] = N'{5}',[MODIFY_DATE] = GETDATE() 
where [PART_NO] = N'{0}' and [SUPPLIER_NUM] = N'{1}' and [PLANT] = N'{2}';"
    , matnr.Matnr, matnr.Lifnr, plant, materialRequireDate, partQty, loginUser, partCname, partPurchaser, supplierName);
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    #region 更新生产订单
                    ///生产订单删除
                    if (sapProductOrderInfo.Zsc.ToUpper() == "X")
                    {
                        @string.AppendLine("update [LES].[TT_BAS_PULL_ORDERS] " +
                            "set [VALID_FLAG] = 0," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() " +
                            "where [ID] = " + sapProductOrderInfo.Id + " and " +
                            "[VALID_FLAG] = 1;");
                        @string.AppendLine("update [LES].[TT_BAS_PULL_ORDER_BOM] " +
                            "set [VALID_FLAG] = 0," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() " +
                            "where [ZORDNO] = N'" + pullOrdersInfo.OrderNo + "' and " +
                            "[VALID_FLAG] = 1;");
                    }
                    else
                    {
                        ///更新生产订单时版本号⑨累加，同时更新生产订单内容、以及SAP生产订单处理状态⑮为20.已处理
                        @string.AppendFormat("update [LES].[TT_BAS_PULL_ORDERS] set " +
                            "[WERK] = N'{1}'," +
                            "[MODEL_YEAR] = N'{2}'," +
                            "[VEHICLE_ORDER] = N'{3}'," +
                            "[ORDER_DATE] = N'{4}'," +
                            "[ASSEMBLY_LINE] = N'{5}'," +
                            "[PART_NO] = N'{6}'," +
                            "[VERSION] = ISNULL([VERSION],0) + 1," +
                            "[MODIFY_USER] = N'{7}'," +
                            "[MODIFY_DATE] = GETDATE()," +
                            "[PLAN_EXECUTE_TIME] = N'{8}' " +
                            "where [ID] = {0};",
                            pullOrdersInfo.Id,
                            plant,
                            sapProductOrderInfo.CarColor,
                            sapProductOrderInfo.OnlineSeq,
                            sapProductOrderDate,
                            assemblyLine,
                            sapProductOrderInfo.Matnr,
                            loginUser,
                            sapProductOrderDate);
                    }
                    ///TODO:在下发给WMS时也需要提供生产订单删除的逻辑标识
                    WmsVmiProductOrderInfo wmsVmiProductOrderInfo = WmsVmiProductOrderBLL.CreateWmsVmiProductOrderInfo(loginUser);
                    ///
                    PullOrdersBLL.GetPullOrdersInfo(sapProductOrderInfo, ref pullOrdersInfo);
                    ///WERK,接口_工厂
                    pullOrdersInfo.Werk = plant;
                    ///ORDER_DATE,订单日期
                    pullOrdersInfo.OrderDate = BLL.LES.CommonBLL.TryParseDatetime(sapProductOrderDate);
                    ///ASSEMBLY_LINE,工厂模型_流水线
                    pullOrdersInfo.AssemblyLine = assemblyLine;
                    ///PLAN_EXECUTE_TIME,计划执行时间
                    pullOrdersInfo.PlanExecuteTime = BLL.LES.CommonBLL.TryParseDatetime(sapProductOrderDate);
                    ///VERSION,版本号
                    pullOrdersInfo.Version = pullOrdersInfo.Version.GetValueOrDefault() + 1;
                    ///
                    WmsVmiProductOrderBLL.GetWmsVmiProductOrderInfo(pullOrdersInfo, ref wmsVmiProductOrderInfo);
                    ///

                    wmsVmiProductOrderInfo.DownLineTime = sapProductOrderInfo.OfflineDate;
                    wmsVmiProductOrderInfo.OnlineTime = sapProductOrderInfo.OnlineDate;
                    wmsVmiProductOrderInfo.LockFlag = sapProductOrderInfo.LockFlag;

                    @string.AppendLine(WmsVmiProductOrderDAL.GetInsertSql(wmsVmiProductOrderInfo));
                    @string.AppendLine(BLL.LES.CommonBLL.GetCreateOutboundLogSql("VMI", wmsVmiProductOrderInfo.LogFid.GetValueOrDefault(), "LES-WMS-012", wmsVmiProductOrderInfo.OrderNo, loginUser));
                    #endregion
                }
                #endregion

                #region 更新SAP生产订单
                @string.AppendFormat(@"update [LES].[TI_IFM_SAP_PRODUCT_ORDER] 
set PROCESS_FLAG = {0},PROCESS_TIME = GETDATE(),[MODIFY_USER] = N'{1}',[MODIFY_DATE] = GETDATE() 
where [ID] = {2};"
, (int)ProcessFlagConstants.Processed, loginUser, sapProductOrderInfo.Id);
                #endregion

                #region 数据库语句执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (@string.Length > 0)
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                    trans.Complete();
                }
                ///这个很重要
                @string.Clear();
                #endregion
                ///订单计算用时
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - startExecuteTime;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":End " + sapProductOrderInfo.Aufnr + "," + ts.TotalSeconds + "s");
            }
        }
        #endregion

        #region 供货计划的日期列处理
        /// <summary>
        /// 日期处理
        /// </summary>
        /// <param name="onlineDate"></param>
        /// <returns></returns>
        public void ProcessSupplyPlanDate(DateTime onlineDate)
        {
            ///需根据SAP生产订单中的上线日期⑩判定供货计划TT_ATP_SUPPLY_PLAN中的对应日期列是否存在
            string sqlTemplate = "if not exists (select 1 from dbo.[syscolumns] where [id] = object_id('[LES].[TT_ATP_SUPPLY_PLAN]') and [name] = N'{0}') "
                + "alter table [LES].[TT_ATP_SUPPLY_PLAN] add [{0}] decimal(18,4) null;";
            ///TODO:考虑形成系统配置
            int totalDays = 14;
            ///
            string sql = string.Empty;
            for (int i = 0; i < totalDays; i++)
            {
                sql += string.Format(sqlTemplate, onlineDate.AddDays(i).ToString("yyyyMMdd"));
            }
            ///
            BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}
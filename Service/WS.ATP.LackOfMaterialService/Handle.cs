using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infrustructure.Utilities;
using BLL.SYS;
using DM.LES;
using BLL.LES;
using System.Transactions;
using DM.SYS;

namespace WS.ATP.LackOfMaterialService
{
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        private string loginUser = "LackOfMaterialService";
        /// <summary>
        /// 是否将缺件生产订单发送给SAP
        /// </summary>
        private string lackMaterialProductionOrdersToSap = new ConfigBLL().GetValueByCode("LACK_MATERIAL_PRODUCTION_ORDERS_TO_SAP");
        /// <summary>
        /// 是否将缺件生产订单发送给MES
        /// </summary>
        private string lackMaterialProductionOrdersToMes = new ConfigBLL().GetValueByCode("LACK_MATERIAL_PRODUCTION_ORDERS_TO_MES");
        /// <summary>
        /// LackOfMaterialBLL
        /// </summary>
        LackOfMaterialBLL lackOfMaterialBLL = new LackOfMaterialBLL();

        ///TODO:系统配置，物料需求提前天数
        int materialRequrieAdvanceDays = 0 + 1;
        #endregion

        #region Handler
        public void Handler()
        {
            ///数据库执行语句
            StringBuilder sqlText = new StringBuilder();
            ///获取状态⑦为10已创建的缺件表
            List<LackOfMaterialInfo> lackOfMaterialInfos = lackOfMaterialBLL.GetListByPage("" +
                "[STATUS] = " + (int)LackOfMaterialStatusConstants.WaitForCalculation + "",
                "[ID]", 1, 1, out int dataCnt);
            ///没有需要计算的缺件表
            if (lackOfMaterialInfos.Count == 0) return;
            LackOfMaterialInfo lackOfMaterialInfo = lackOfMaterialInfos.FirstOrDefault();

            ///准备开始计算时需要先将状态⑦更新为20处理中同时记录开始计算时间⑧
            lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.Calculating, lackOfMaterialInfo.Id, "", loginUser);

            #region 缺件表中的检索条件暂时保留
            ///物料号②、供应商代码③、工厂代码④为空时表示全部，物料号②可能出现模糊条件需要用charindex进行条件判定
            ///开始日期⑤与结束日期⑥决定了获取供货计划的数据范围
            ///物料号更新为多选状态
            string[] partNos = new string[] { };
            if (!string.IsNullOrEmpty(lackOfMaterialInfo.PartNo))
                partNos = lackOfMaterialInfo.PartNo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string partNoCondition = string.Empty;
            if (partNos.Length > 0)
                partNoCondition = "and [PART_NO] in ('" + string.Join("','", partNos) + "') ";
            ///
            string supplierNumConidtion = string.Empty;
            if (!string.IsNullOrEmpty(lackOfMaterialInfo.SupplierNum))
                supplierNumConidtion = "and [SUPPLIER_NUM] = N'" + lackOfMaterialInfo.SupplierNum + "' ";
            ///
            string plantCondition = string.Empty;
            if (!string.IsNullOrEmpty(lackOfMaterialInfo.Plant))
                plantCondition = "and [PLANT] = N'" + lackOfMaterialInfo.Plant + "' ";
            ///
            string partPurchaserCondition = string.Empty;
            if (!string.IsNullOrEmpty(lackOfMaterialInfo.PartPurchaser))
                partPurchaserCondition = "and CHARINDEX(N'" + lackOfMaterialInfo.PartPurchaser + "',[PART_PURCHASER]) > 0 ";
            #endregion

            /// 获取供货计划数据
            List<string> dateColumns = new List<string>();
            ///获取缺件表计算的日期范围
            DateTime cDateTime = lackOfMaterialInfo.StartDate.GetValueOrDefault();
            while (cDateTime <= lackOfMaterialInfo.EndDate.GetValueOrDefault())
            {
                dateColumns.Add(cDateTime.ToString("yyyyMMdd"));
                cDateTime = cDateTime.AddDays(1);
            }
            ///获取数据库中有效的日期列
            dateColumns = new SupplyPlanBLL().GetDatabaseExistsDateColumns(dateColumns);
            ///无效的日期参数
            if (dateColumns.Count == 0)
            {
                lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.CalculateFailed, lackOfMaterialInfo.Id, "无效的日期参数", loginUser);
                return;
            }

            ///根据物料号②、供应商代码③、工厂代码④获取物料仓储信息，仅获取缺件检查标记为True的数据
            #region 获取物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockBLL().GetList("[LACK_OF_INSPECTION_FLAG] = 1 ", string.Empty);
            ///没有物料仓储信息数据
            if (partsStockInfos.Count == 0)
            {
                lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.CalculateFailed, lackOfMaterialInfo.Id, "没有物料仓储信息数据", loginUser);
                return;
            }
            #endregion
            ///根据物料仓储信息中需要做缺件检查的物料对物料条件重新定义
            partNoCondition = "and [PART_NO] in ('" + string.Join("','", partsStockInfos.Select(d => d.PartNo).ToArray()) + "') ";
            ///
            supplierNumConidtion = "and [SUPPLIER_NUM] in ('" + string.Join("','", partsStockInfos.Select(d => d.SupplierNum).ToArray()) + "') ";

            ///获取日期④对应的供货计划数据
            List<SrmPrevSupplyPlanInfo> supplyPlanInfos = new List<SrmPrevSupplyPlanInfo>();
            foreach (string dateColumn in dateColumns)
            {
                supplyPlanInfos.AddRange(new SrmPrevSupplyPlanBLL().GetListByDateColumn(dateColumn, partNoCondition + supplierNumConidtion + plantCondition));
            }
            ///无供货计划  
            if (supplyPlanInfos.Count == 0)
            {
                lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.CalculateFailed, lackOfMaterialInfo.Id, "无供货计划", loginUser);
                return;
            }

            ///分组
            var supplyPlanInfosQuerys = from g in (from s in supplyPlanInfos
                                                   join p in partsStockInfos on new { s.PartNo, s.SupplierNum } equals new { p.PartNo, p.SupplierNum }
                                                   select s)
                                        group g by new { g.PartNo, g.SupplierNum, g.Plant, g.PartPurchaser } into h
                                        select new { h.Key.PartNo, h.Key.SupplierNum, h.Key.Plant, h.Key.PartPurchaser, RequireQty = h.Sum(x => x.RequireQty.GetValueOrDefault()) };

            ///获取库存数据
            List<StocksInfo> stocksInfos = new StocksBLL().GetList(partNoCondition + supplierNumConidtion, string.Empty);
            ///获取物料号、供应商、工厂、仓库、存储区分组汇总的可用库存数量
            var stocksInfosQuery = stocksInfos
                                .GroupBy(s => new { s.PartNo, s.SupplierNum, s.Plant })
                                .Select(p => new { p.Key.PartNo, p.Key.SupplierNum, p.Key.Plant, AvailbleStocks = p.Sum(x => x.AvailbleStocks.GetValueOrDefault()) }).ToList();

            ///以供货计划中开始日期至结束日期的需求数量合计 – 库存合计数量作为缺件数量⑤，当缺件数量 > 0 时，将缺件信息写入本次缺件表
            ///以上为计算逻辑内容，实际程序获取比对基础数据时需尽量减少与数据库的交互次数而改用LINQ方式进行计算，以提高程序计算效率
            ///一份缺件表计算完成后，将缺件明细一次性写入数据库同时更新缺件表状态⑦为30已处理，并记录计算结束时间⑨
            foreach (var supplyPlanInfoQuery in supplyPlanInfosQuerys)
            {
                var stocksInfoQuery = stocksInfosQuery.FirstOrDefault(d =>
                d.PartNo == supplyPlanInfoQuery.PartNo &&
                d.Plant == supplyPlanInfoQuery.Plant &&
                d.SupplierNum == supplyPlanInfoQuery.SupplierNum);
                sqlText.AppendFormat("insert into [LES].[TT_ATP_LACK_OF_MATERIAL_DETAIL] (" +
                    "[FID],[LACK_ORDER_FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[LACK_QTY]," +
                    "[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PART_PURCHASER],[FEEDBACK_FLAG]) values (" +
                    "NEWID(),'{0}','{1}','{2}','{3}',{4}," +
                    "1,'{5}',GETDATE(),'{6}',{7});",
                    lackOfMaterialInfo.Fid, ///LACK_ORDER_FID,0
                        supplyPlanInfoQuery.PartNo,///PART_NO,1
                        supplyPlanInfoQuery.SupplierNum, ///SUPPLIER_NUM,2
                        supplyPlanInfoQuery.Plant, ///PLANT,3
                        supplyPlanInfoQuery.RequireQty - (stocksInfoQuery == null ? 0 : stocksInfoQuery.AvailbleStocks),///LACK_QTY,4
                        loginUser,///CREATE_USER,5
                        supplyPlanInfoQuery.PartPurchaser,///PART_PURCHASER,6
                        0///FEEDBACK_FLAG,7,反馈标记为了页面搜索方便把默认值填写为0
                        );
            }
            ///更新缺件表状态
            sqlText.AppendFormat("update [LES].[TT_ATP_LACK_OF_MATERIAL] set " +
                "[STATUS] = {2}," +
                "[EXECUTE_END_TIME] = GETDATE()," +
                "[MODIFY_USER] = N'{0}'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = {1};",
                loginUser,
                lackOfMaterialInfo.Id,
                (int)LackOfMaterialStatusConstants.Completed
                );

            #region 执行
            using (TransactionScope trans = new TransactionScope())
            {
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlText.ToString());
                trans.Complete();
            }
            #endregion
        }
        #endregion

        #region Handler-CheckLackProductionOrder
        /// <summary>
        /// 缺件生产订单生成
        /// </summary>
        public void CheckLackProductionOrder()
        {
            ///获取状态⑦为50已发布的缺件表数据，记录开始计算时间⑧并将状态⑦更新为60处理中
            ///数据库执行语句
            StringBuilder sqlText = new StringBuilder();
            ///获取状态⑦为50反馈完成的缺件表
            List<LackOfMaterialInfo> lackOfMaterialInfos = lackOfMaterialBLL.GetListByPage("" +
                "[STATUS] = " + (int)LackOfMaterialStatusConstants.Feedbacked + "",
                "[ID]", 1, 1, out int dataCnt);
            ///没有需要计算的缺件表
            if (lackOfMaterialInfos.Count == 0) return;
            LackOfMaterialInfo lackOfMaterialInfo = lackOfMaterialInfos.FirstOrDefault();

            ///获取状态⑦为50已发布的缺件表数据，记录开始计算时间⑧并将状态⑦更新为60处理中
            lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.OrderCalculating, lackOfMaterialInfo.Id, "", loginUser);
            ///工厂
            string plantCondition = string.Empty;
            if (!string.IsNullOrEmpty(lackOfMaterialInfo.Plant))
                plantCondition = " and [WERK] = N'" + lackOfMaterialInfo.Plant + "' ";

            ///根据开始日期⑤与结束日期⑥、工厂代码④范围获取生产订单列表，按生产日期+生产顺序从晚到早进行排列
            ///供货计划与生产订单日期相差一天
            List<PullOrdersInfo> pullOrdersInfos = new PullOrdersBLL().GetList("" + plantCondition + " and " +
                "[ORDER_DATE] between " +
                "N'" + lackOfMaterialInfo.StartDate.GetValueOrDefault().AddDays(materialRequrieAdvanceDays) + "' and " +
                "N'" + lackOfMaterialInfo.EndDate.GetValueOrDefault().AddDays(materialRequrieAdvanceDays) + "'",
                "[ORDER_DATE] desc,[VEHICLE_ORDER] desc");
            ///没有生产订单时计算失败
            if (pullOrdersInfos.Count == 0)
            {
                lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.OrderCalculateFailed, lackOfMaterialInfo.Id, "没有生产订单时计算失败", loginUser);
                return;
            }

            ///根据缺件表明细中的物料号②、供应商代码③获取该生产订单的订单BOM中的数据
            List<LackOfMaterialDetailInfo> lackOfMaterialDetailInfos = new LackOfMaterialDetailBLL().GetList("" +
                "[LACK_ORDER_FID] = N'" + lackOfMaterialInfo.Fid.GetValueOrDefault() + "' and " +
                "[FEEDBACK_FLAG] = 1", string.Empty);
            if (lackOfMaterialDetailInfos.Count == 0)
            {
                lackOfMaterialBLL.UpdateStatus((int)LackOfMaterialStatusConstants.OrderCalculateFailed, lackOfMaterialInfo.Id, "缺件表中无物料缺件信息", loginUser);
                return;
            }
            ///BOM
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomBLL().GetList("" +
                "[ORDERFID] in ('" + string.Join("','", pullOrdersInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                "[ZCOMNO] in ('" + string.Join("','", lackOfMaterialDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", lackOfMaterialDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);

            ///历史缺件生产订单 ORDER_DATE
            List<PorderLackMaterialInfo> lackOfMaterialProductionOrderInfos = new PorderLackMaterialBLL().GetList(
                "[ORDER_DATE] between " +
                "N'" + lackOfMaterialInfo.StartDate.GetValueOrDefault().AddDays(materialRequrieAdvanceDays) + "' and " +
                "N'" + lackOfMaterialInfo.EndDate.GetValueOrDefault().AddDays(materialRequrieAdvanceDays) + "'", string.Empty);
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            foreach (PullOrdersInfo pullOrdersInfo in pullOrdersInfos)
            {
                ///并将该数据集合中的消耗数量与反馈缺件数量⑦进行对比，当反馈缺件数量⑦ – 消耗数量 >= 0 时
                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d => d.Orderfid.GetValueOrDefault() == pullOrdersInfo.Fid.GetValueOrDefault()).ToList();
                if (pullOrderBoms.Count == 0) continue;

                ///此时需要记录对比结果到生产订单缺件明细中，物料号③、供应商代码④、工厂代码⑤、需求数量⑥即为BOM消耗数量
                var pullOrderBomQuery = pullOrderBoms
                    .GroupBy(b => new { b.Zcomno, b.SupplierNum })
                    .Select(d => new { PartNo = d.Key.Zcomno, d.Key.SupplierNum, Zqty = d.Sum(x => x.Zqty.GetValueOrDefault()) }).ToList();

                ///缺件数量⑦即当反馈缺件数量⑦ – 消耗数量 >= 0 是为消耗数量，否则为反馈缺件剩余数量⑦       
                ///同时记录缺件生产订单，生产订单号③、生产订单外键②、工厂代码④、车间代码⑤、生产线代码⑥从生产订单中继承、缺件表外键①从缺件表继承、缺件标记⑦为True
                var lackDetailInfos = (from l in lackOfMaterialDetailInfos
                                       join p in pullOrderBomQuery
                                       on new { l.PartNo, l.SupplierNum } equals new { p.PartNo, p.SupplierNum }
                                       where l.FeedbackLackQty > 0
                                       select new { l.PartNo, l.SupplierNum, l.Plant, l.PartPurchaser, p.Zqty, l.FeedbackLackQty, l.Fid, l.LackOrderFid }).ToList();

                ///历史生产订单中是否存在此订单号
                PorderLackMaterialInfo lackOfMaterialProductionOrderInfo
                    = lackOfMaterialProductionOrderInfos.FirstOrDefault(d =>
                    d.ProductionOrderFid.GetValueOrDefault() == pullOrdersInfo.Fid.GetValueOrDefault());
                ///当缺件明细中的反馈缺件数量全部被扣除到小于等于零时，不再记录生产订单缺件明细，仅记录缺件生产订单，此时开始缺件标记⑦为False
                Guid lackOfMaterialProductionOrderFid = Guid.NewGuid();

                #region TT_ATP_PORDER_LACK_MATERIAL
                if (lackOfMaterialProductionOrderInfo == null)
                {
                    ///不存在、直接插入
                    sqlText.AppendFormat("insert into [LES].[TT_ATP_PORDER_LACK_MATERIAL] (" +
                        "[FID],[LACK_ORDER_FID],[PRODUCTION_ORDER_FID],[PRODUCTION_ORDER_NO],[PLANT],[ASSEMBLY_LINE]," +
                        "[VALID_FLAG],[LACK_FLAG],[CREATE_USER],[CREATE_DATE],[ORDER_DATE]) values (" +
                        "'{8}','{0}','{1}','{2}','{3}','{4}'," +
                        "1,{7},'{5}',GETDATE(),'{6}');",
                            lackOfMaterialInfo.Fid,///LACK_ORDER_FID,0
                            pullOrdersInfo.Fid,///PRODUCTION_ORDER_FID,1
                            pullOrdersInfo.OrderNo,///PRODUCTION_ORDER_NO,2
                            pullOrdersInfo.Werk,///PLANT,3
                            pullOrdersInfo.AssemblyLine,///ASSEMBLY_LINE,4
                            loginUser,///CREATE_USER,5
                            pullOrdersInfo.OrderDate.GetValueOrDefault(),///ORDER_DATE,6
                            (lackDetailInfos.Count == 0 ? 0 : 1),///LACK_FLAG,7
                            lackOfMaterialProductionOrderFid///FID,8
                            );
                }
                else
                {
                    lackOfMaterialProductionOrderFid = lackOfMaterialProductionOrderInfo.Fid.GetValueOrDefault();
                    ///已存在、更新状态
                    sqlText.AppendFormat("update [LES].[TT_ATP_PORDER_LACK_MATERIAL] set " +
                        "[LACK_FLAG] = {4}," +
                        "[LACK_ORDER_FID] = N'{0}'," +
                         "[ORDER_DATE] = N'{3}'," +
                        "[MODIFY_USER] = N'{1}'," +
                        "[MODIFY_DATE] = GETDATE() where " +
                        "[FID] = N'{2}';",
                            lackOfMaterialInfo.Fid.GetValueOrDefault(),///LACK_ORDER_FID,0
                            loginUser,///MODIFY_USER,1
                            lackOfMaterialProductionOrderFid,///FID,2
                            pullOrdersInfo.OrderDate.GetValueOrDefault(),///ORDER_DATE,3
                             (lackDetailInfos.Count == 0 ? 0 : 1)///LACK_FLAG,4
                            );
                    ///删除之前计算的生产订单缺件明细
                    sqlText.AppendFormat("delete from [LES].[TT_ATP_PORDER_LACK_MATERIAL_DETAIL] where " +
                        "[LACK_PORDER_FID] = N'{0}';",
                        lackOfMaterialProductionOrderFid///LACK_PORDER_FID,0
                            );
                    ///移除
                    //lackOfMaterialProductionOrderInfos.Remove(lackOfMaterialProductionOrderInfo);
                }
                #endregion

                #region TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL
                ///插入到SAP中间表
                if (lackMaterialProductionOrdersToSap.ToLower() == "true")
                {
                    sqlText.AppendFormat(" insert into [LES].[TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL] (" +
                        "[FID],[ENTERPRISE],[AREA_NO],[DMS_NO],[MATERIAL_CHECK],[SEND_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG],[LOG_FID]) values (" +
                        "NEWID(),'{0}','{1}','{2}',0,GETDATE(),1,'{3}',GETDATE(),{5},'{4}');",
                        pullOrdersInfo.Werk, ///ENTERPRISE,0
                                pullOrdersInfo.AssemblyLine,///AREA_NO,1
                                pullOrdersInfo.OrderNo,///DMS_NO,2
                                loginUser,/// CREATE_USER,3
                                logFid,///LOG_FID,4
                                (int)ProcessFlagConstants.Untreated///PROCESS_FLAG,5
                                );
                }
                #endregion

                #region TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL
                ///插入到MES中间表
                if (lackMaterialProductionOrdersToMes.ToLower() == "true")
                {
                    sqlText.AppendFormat("insert into [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] (" +
                        "[FID],[ENTERPRISE],[AREA_NO],[DMS_NO],[MATERIAL_CHECK],[SEND_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG],[LOG_FID]) values (" +
                        "NEWID(),'{0}','{1}','{2}',0,GETDATE(),1,'{3}',GETDATE(),{5},'{4}');",
                                pullOrdersInfo.Werk,///ENTERPRISE,0
                                pullOrdersInfo.AssemblyLine,///AREA_NO,1
                                pullOrdersInfo.OrderNo,///DMS_NO,2
                                loginUser,/// CREATE_USER,3
                                logFid,///LOG_FID,4
                                (int)ProcessFlagConstants.Untreated///PROCESS_FLAG,5
                                );
                }
                #endregion

                #region TT_ATP_PORDER_LACK_MATERIAL_DETAIL
                if (lackDetailInfos.Count > 0)
                {
                    ///写入生产订单缺件明细
                    foreach (var lackDetailInfo in lackDetailInfos)
                    {
                        sqlText.AppendFormat("insert into [LES].[TT_ATP_PORDER_LACK_MATERIAL_DETAIL] (" +
                            "[FID],[LACK_PORDER_FID],[PRODUCTION_ORDER_FID],[LACK_DETAIL_FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[REQUIRE_QTY],[LACK_QTY]," +
                            "[VALID_FLAG],[CREATE_USER],[CREATE_DATE]," +
                            "[PART_PURCHASER],[PRODUCTION_ORDER_NO],[ASSEMBLY_LINE],[ORDER_DATE],[LACK_ORDER_FID]) values " +
                            "(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7}," +
                            "1,'{8}',GETDATE(),'{9}','{10}','{11}','{12}','{13}');",
                                lackOfMaterialProductionOrderFid,///LACK_PORDER_FID,0
                                pullOrdersInfo.Fid.GetValueOrDefault(),///PRODUCTION_ORDER_FID,1
                                lackDetailInfo.Fid.GetValueOrDefault(),///LACK_DETAIL_FID,2
                                lackDetailInfo.PartNo,///PART_NO,3
                                lackDetailInfo.SupplierNum,///SUPPLIER_NUM,4
                                lackDetailInfo.Plant,///PLANT,5
                                lackDetailInfo.Zqty,///REQUIRE_QTY,6
                                lackDetailInfo.FeedbackLackQty >= lackDetailInfo.Zqty ? lackDetailInfo.Zqty : lackDetailInfo.FeedbackLackQty,///LACK_QTY,7
                                loginUser, ///CREATE_USER,8
                                lackDetailInfo.PartPurchaser,///PART_PURCHASER,9
                                pullOrdersInfo.OrderNo,///PRODUCTION_ORDER_NO,10
                                pullOrdersInfo.AssemblyLine,///ASSEMBLY_LINE,11
                                pullOrdersInfo.OrderDate.GetValueOrDefault(),///ORDER_DATE,12
                                lackDetailInfo.LackOrderFid.GetValueOrDefault()///LACK_ORDER_FID,13
                                );
                    }
                }
                #endregion

                ///将差异值作为缺件数量以用于下一份生产订单的比对
                lackOfMaterialDetailInfos = (from l in lackOfMaterialDetailInfos
                                             join p in pullOrderBomQuery
                                             on new { l.PartNo, l.SupplierNum } equals new { p.PartNo, p.SupplierNum } into temp
                                             from tt in temp.DefaultIfEmpty()
                                             select new LackOfMaterialDetailInfo { PartNo = l.PartNo, SupplierNum = l.SupplierNum, Plant = l.Plant, PartPurchaser = l.PartPurchaser, FeedbackLackQty = l.FeedbackLackQty - (tt == null ? 0 : tt.Zqty), Fid = l.Fid, LackOrderFid = l.LackOrderFid }).ToList();
            }
            #region 从检查日期范围内移出的生产订单
            ///移除的历史生产订单
            var lackOfMaterialProductionOrderRemoveInfos = (from l in lackOfMaterialProductionOrderInfos
                                                            where !(from p in pullOrdersInfos where p.OrderNo == l.ProductionOrderNo select p).Any()
                                                            select l).ToList();
            foreach (var lackOfMaterialProductionOrderRemoveInfo in lackOfMaterialProductionOrderRemoveInfos)
            {
                ///更新状态
                sqlText.AppendFormat("update [LES].[TT_ATP_PORDER_LACK_MATERIAL] set " +
                    "[LACK_FLAG] = {3}," +
                    "[LACK_ORDER_FID] = N'{0}'," +
                    "[MODIFY_USER] = N'{1}'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[PRODUCTION_ORDER_FID] = N'{2}';",
                    lackOfMaterialInfo.Fid.GetValueOrDefault(),///LACK_ORDER_FID,0
                    loginUser,///MODIFY_USER,1
                    lackOfMaterialProductionOrderRemoveInfo.ProductionOrderFid.GetValueOrDefault(),///PRODUCTION_ORDER_FID,2
                    0///LACK_FLAG,3
                    );
                ///删除明细
                sqlText.AppendFormat("delete from [LES].[TT_ATP_PORDER_LACK_MATERIAL_DETAIL] where " +
                    "[PRODUCTION_ORDER_FID] = N'{0}';",
                    lackOfMaterialProductionOrderRemoveInfo.ProductionOrderFid.GetValueOrDefault()///PRODUCTION_ORDER_FID,0
                    );
                ///插入到SAP中间表
                if (lackMaterialProductionOrdersToSap.ToLower() == "true")
                {
                    sqlText.AppendFormat(" insert into [LES].[TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL] (" +
                        "[FID],[ENTERPRISE],[AREA_NO],[DMS_NO],[MATERIAL_CHECK],[SEND_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG],[LOG_FID]) values (" +
                        "NEWID(),'{0}','{1}','{2}',{6},GETDATE(),1,'{3}',GETDATE(),{5},'{4}');",
                                lackOfMaterialProductionOrderRemoveInfo.Plant, ///ENTERPRISE,0
                                lackOfMaterialProductionOrderRemoveInfo.AssemblyLine,///AREA_NO,1
                                lackOfMaterialProductionOrderRemoveInfo.ProductionOrderNo,///DMS_NO,2
                                loginUser,/// CREATE_USER,3
                                logFid,///LOG_FID,4
                                (int)ProcessFlagConstants.Untreated,///PROCESS_FLAG,5
                                0///MATERIAL_CHECK,6
                                );
                }
                ///插入到MES中间表
                if (lackMaterialProductionOrdersToMes.ToLower() == "true")
                {
                    sqlText.AppendFormat("insert into [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] (" +
                        "[FID],[ENTERPRISE],[AREA_NO],[DMS_NO],[MATERIAL_CHECK],[SEND_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG],[LOG_FID]) values (" +
                        "NEWID(),'{0}','{1}','{2}',{6},GETDATE(),1,'{3}',GETDATE(),{5},'{4}');",
                                lackOfMaterialProductionOrderRemoveInfo.Plant,///ENTERPRISE,0
                                lackOfMaterialProductionOrderRemoveInfo.AssemblyLine,///AREA_NO,1
                                lackOfMaterialProductionOrderRemoveInfo.ProductionOrderNo,///DMS_NO,2
                                loginUser,/// CREATE_USER,3
                                logFid,///LOG_FID,4
                                (int)ProcessFlagConstants.Untreated,///PROCESS_FLAG,5
                                0///MATERIAL_CHECK,6
                                );
                }
            }
            #endregion
            ///
            if (sqlText.Length > 0)
            {
                ///调用发送数据
                string keyValue = lackOfMaterialInfo.StartDate.GetValueOrDefault().ToString("yyyyMMdd") + "~" + lackOfMaterialInfo.EndDate.GetValueOrDefault().ToString("yyyyMMdd");
                if (lackMaterialProductionOrdersToSap.ToLower() == "true")
                    sqlText.AppendFormat(BLL.LES.CommonBLL.GetCreateOutboundLogSql("SAP", logFid, "LES-SAP-017", keyValue, loginUser));
                if (lackMaterialProductionOrdersToMes.ToLower() == "true")
                    sqlText.AppendFormat(BLL.LES.CommonBLL.GetCreateOutboundLogSql("MES", logFid, "LES-MES-002", keyValue, loginUser));
                ///计算完成后记录计算结束时间⑨并更新状态⑦为70已完成，同时将缺件生产订单及生产订单缺件明细一次性写入数据库中
                sqlText.AppendFormat("update [LES].[TT_ATP_LACK_OF_MATERIAL] set " +
                    "[STATUS] = {2}," +
                    "[EXECUTE_END_TIME] = GETDATE()," +
                    "[MODIFY_USER] = N'{0}'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = {1};",
                    loginUser,///MODIFY_USER,0
                    lackOfMaterialInfo.Id,///ID,1
                    (int)LackOfMaterialStatusConstants.OrderCompleted///STATUS,2
                    );
                #region 执行
                using (TransactionScope trans = new TransactionScope())
                {
                    BLL.LES.CommonBLL.ExecuteNonQueryBySql(sqlText.ToString());
                    trans.Complete();
                }
                #endregion
            }
        }
        #endregion
    }
}
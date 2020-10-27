 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infrustructure.Utilities;
using BLL.LES;
using BLL.SYS;
using DM.LES;
using System.Data;
using System.Transactions;
using DM.SYS;
using Infrustructure.Logging;

namespace WS.VMI.CalculateSupplyPlanService
{
    class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "VMI.CalculateSupplyPlanService";
        ///系统配置中新增_SUPPLY_PLAN_SYNC_DAYS参数
        string vmiSupplyPlanSyncDays = new ConfigBLL().GetValueByCode("VMI_SUPPLY_PLAN_SYNC_DAYS");
        #endregion

        #region Handler
        public void Handler()
        {
            #region 逻辑
            ///判断VMI系统供货计划同步天数
            if (string.IsNullOrEmpty(vmiSupplyPlanSyncDays))
                throw new Exception("0x00000192");///系统配置中未发现VMI系统供货计划同步天数


            Log.WriteLogToFile("VMI系统供货计划同步天数:" + vmiSupplyPlanSyncDays.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\LOG\", DateTime.Now.ToString("yyyyMMdd"));


            int supplyPlanSyncDays = 0;
            int.TryParse(vmiSupplyPlanSyncDays, out supplyPlanSyncDays);
            ///以当日为起始，逐天进行数据对比，直至系统配置中设定的天数
            ///获取当前日期
            DateTime dateTime = DateTime.Now;         
           
            ///获取供应商对应的VMI仓库，不启用LES中VMI功能的仓库
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierBLL().GetList("[VMI_FLAG] = 0", string.Empty);
            for (int i = 0; i < supplyPlanSyncDays; i++)
            {
                //////LOG_FID
                Guid logFid = Guid.NewGuid();
                string dateColumn = string.Empty;
                dateColumn = dateTime.AddDays(i).ToString("yyyyMMdd");
                ///获取数据库中有效的日期列
                dateColumn = new SupplyPlanBLL().GetDatabaseExistsDateColumn(dateColumn);

                if (string.IsNullOrEmpty(dateColumn))
                    continue;

                ///获取日期④对应的供货计划数据
                List<VmiPrevSupplyPlanInfo> supplyPlanInfos = new VmiPrevSupplyPlanBLL().GetListByDateColumn(dateColumn);

                Log.WriteLogToFile("日期④对应的供货计划数据:" + supplyPlanInfos.Count.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\LOG\", DateTime.Now.ToString("yyyyMMdd"));

                if (supplyPlanInfos.Count == 0) continue;

                supplyPlanInfos = (from s in supplyPlanInfos
                                   join v in vmiSupplierInfos
                                   on s.SupplierNum equals v.SupplierNum into sv
                                   select new VmiPrevSupplyPlanInfo { PartNo = s.PartNo, SupplierNum = s.SupplierNum, Plant = s.Plant, DeliveryDate = s.DeliveryDate, RequireQty = s.RequireQty, VmiWmNo = sv.Select(d => d.WmNo).FirstOrDefault() }).ToList();

                ///再获取日期④对应的上一版供货计划数据
                List<VmiPrevSupplyPlanInfo> prevSupplyPlanInfos = new VmiPrevSupplyPlanBLL().GetList("[DELIVERY_DATE] = N'" + dateColumn + "'", string.Empty);
                ///数据库执行语句
                StringBuilder sqlSb = new StringBuilder();
                ///对比两个数据集合中的差异数据行（以相同工厂③、供应商②、物料号①为比对维度，数量有变化的视为差异数据行，包括两个数据集合未能交集的部分也为差异数据行）
                var query = (from s in supplyPlanInfos
                             join p in prevSupplyPlanInfos
                             on new { s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate }
                             equals new { p.PartNo, p.SupplierNum, p.Plant, p.DeliveryDate }
                             select new { s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty, pQty = p.RequireQty, s.VmiWmNo }).ToList();
                ///取出上一版供货计划删除的部分
                List<VmiPrevSupplyPlanInfo> prevSupplyPlans = (from p in prevSupplyPlanInfos
                                                               where !(from q in query where p.PartNo == q.PartNo && p.SupplierNum == q.SupplierNum && p.Plant == q.Plant && p.DeliveryDate == q.DeliveryDate select q).Any()
                                                               select p).ToList();
                foreach (var p in prevSupplyPlans)
                {
                    ///被删除的数据以需求数量⑤0表示
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[PROCESS_FLAG],[VMI_WM_NO],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',0,'{4}',GETDATE(),1,{7},'{5}','{6}');"
                    , p.PartNo, p.SupplierNum, p.Plant, p.DeliveryDate, loginUser, p.VmiWmNo, logFid, (int)ProcessFlagConstants.Untreated);
                }
                ///取出最新版供货计划新增的部分
                List<VmiPrevSupplyPlanInfo> supplyPlans = (from p in supplyPlanInfos
                                                           where !(from q in query where p.PartNo == q.PartNo && p.SupplierNum == q.SupplierNum && p.Plant == q.Plant && p.DeliveryDate == q.DeliveryDate select q).Any()
                                                           select p).ToList();
                foreach (var s in supplyPlans)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[VMI_WM_NO],[PROCESS_FLAG],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1,'{6}',{8},'{7}');"
                    , s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty.GetValueOrDefault(), loginUser, s.VmiWmNo, logFid, (int)ProcessFlagConstants.Untreated);
                }
                ///取出数据交集部分 数量有变化的差异行
                var queryinfos = query.Where(d => d.RequireQty != d.pQty).ToList();
                foreach (var q in queryinfos)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[VMI_WM_NO],[PROCESS_FLAG],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1,'{6}',{8},'{7}');"
                    , q.PartNo, q.SupplierNum, q.Plant, q.DeliveryDate, q.RequireQty.GetValueOrDefault(), loginUser, q.VmiWmNo, logFid, (int)ProcessFlagConstants.Untreated);
                }
                if (sqlSb.Length > 0)
                {
                    ///调用数据发送
                    string targetSystem = "VMI";
                    string methodCode = "LES-WMS-009";
                    string keyValue = dateColumn;
                    sqlSb.AppendFormat(BLL.LES.CommonBLL.GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser));
                }
                ///删除上一版供货计划的数据
                if (prevSupplyPlanInfos.Count > 0)
                    sqlSb.Append(@"delete from [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] where [ID] in (" + string.Join(",", prevSupplyPlanInfos.Select(d => d.Id).ToArray()) + ");");
                ///更新TE供货计划为最新版
                foreach (var s in supplyPlanInfos)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[VMI_WM_NO]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1,'{6}');"
                    , s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty.GetValueOrDefault(), loginUser, s.VmiWmNo);
                }
                
                #region 执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (sqlSb.Length > 0)
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
                    trans.Complete();
                }
                #endregion
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + dateColumn
                    + "|D" + prevSupplyPlans.Count + "|A" + supplyPlans.Count + "|U" + queryinfos.Count);
            }
            #endregion
        }
        #endregion
    }
}
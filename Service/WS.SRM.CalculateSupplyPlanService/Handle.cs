namespace WS.SRM.CalculateSupplyPlanService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL.LES;
    using BLL.SYS;
    using DM.LES;
    using System.Data;
    using System.Transactions;
    using DM.SYS;
    using Infrustructure.Logging;

    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "SRM.CalculateSupplyPlanService";
        ///系统配置中新增SRM_SUPPLY_PLAN_SYNC_DAYS参数
        string srmSupplyPlanSyncDays = new ConfigBLL().GetValueByCode("SRM_SUPPLY_PLAN_SYNC_DAYS");
        #endregion

        #region Handler
        public void Handler()
        {

            #region 逻辑
            ///判断SRM系统供货计划同步天数
            if (string.IsNullOrEmpty(srmSupplyPlanSyncDays))
                throw new Exception("0x00000192");///系统配置中未发现SRM系统供货计划同步天数

            int supplyPlanSyncDays = 0;
            int.TryParse(srmSupplyPlanSyncDays, out supplyPlanSyncDays);

            ///以当日为起始，逐天进行数据对比，直至系统配置中设定的天数
            ///获取当前日期
            DateTime dateTime = DateTime.Now;
            for (int i = 0; i < supplyPlanSyncDays; i++)
            {
                string dateColumn = string.Empty;
                dateColumn = dateTime.AddDays(i).ToString("yyyyMMdd");

                ///获取数据库中有效的日期列
                dateColumn = new SupplyPlanBLL().GetDatabaseExistsDateColumn(dateColumn);
                if (string.IsNullOrEmpty(dateColumn))
                    continue;  


                ///获取日期④对应的供货计划数据
                List<SrmPrevSupplyPlanInfo> supplyPlanInfos = new SrmPrevSupplyPlanBLL().GetListByDateColumn(dateColumn);
                if (supplyPlanInfos.Count == 0) continue;

                ///再获取日期④对应的上一版供货计划数据
                List<SrmPrevSupplyPlanInfo> prevSupplyPlanInfos = new SrmPrevSupplyPlanBLL().GetList("[DELIVERY_DATE] = N'" + dateColumn + "'", string.Empty);
                ///数据库执行语句
                StringBuilder sqlSb = new StringBuilder();
                ///对比两个数据集合中的差异数据行（以相同工厂③、供应商②、物料号①为比对维度，数量有变化的视为差异数据行，包括两个数据集合未能交集的部分也为差异数据行）
                var query = (from s in supplyPlanInfos
                             join p in prevSupplyPlanInfos
                             on new { s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate }
                             equals new { p.PartNo, p.SupplierNum, p.Plant, p.DeliveryDate }
                             select new { s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty, pQty = p.RequireQty }).ToList();
                ///取出上一版供货计划删除的部分
                List<SrmPrevSupplyPlanInfo> prevSupplyPlans = (from p in prevSupplyPlanInfos
                                                               where !(from q in query where p.PartNo == q.PartNo && p.SupplierNum == q.SupplierNum && p.Plant == q.Plant && p.DeliveryDate == q.DeliveryDate select q).Any()
                                                               select p).ToList();
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                foreach (var p in prevSupplyPlans)
                {
                    ///被删除的数据以需求数量⑤0表示
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_SRM_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[PROCESS_FLAG],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',0,'{4}',GETDATE(),1,{5},'{6}');"
                    , p.PartNo, p.SupplierNum, p.Plant, p.DeliveryDate, loginUser, (int)ProcessFlagConstants.Untreated, logFid);
                }
                ///取出最新版供货计划新增的部分
                List<SrmPrevSupplyPlanInfo> supplyPlans = (from p in supplyPlanInfos
                                                           where !(from q in query where p.PartNo == q.PartNo && p.SupplierNum == q.SupplierNum && p.Plant == q.Plant && p.DeliveryDate == q.DeliveryDate select q).Any()
                                                           select p).ToList();
                foreach (var s in supplyPlans)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_SRM_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[PROCESS_FLAG],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1,{6},'{7}');"
                    , s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty.GetValueOrDefault(), loginUser, (int)ProcessFlagConstants.Untreated, logFid);
                }
                ///取出数据交集部分 数量有变化的差异行
                var queryinfos = query.Where(d => d.RequireQty != d.pQty).ToList();
                foreach (var q in queryinfos)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TI_IFM_SRM_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG],[PROCESS_FLAG],[LOG_FID]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1,{6},'{7}');"
                    , q.PartNo, q.SupplierNum, q.Plant, q.DeliveryDate, q.RequireQty.GetValueOrDefault(), loginUser, (int)ProcessFlagConstants.Untreated, logFid);
                }
                if (sqlSb.Length > 0)
                {
                    ///调用数据发送
                    string targetSystem = "SRM";
                    string methodCode = "LES-SRM-002";
                    string keyValue = dateColumn;
                    sqlSb.AppendFormat(BLL.LES.CommonBLL.GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser));
                }
                ///删除上一版供货计划的数据
                if (prevSupplyPlanInfos.Count > 0)
                    sqlSb.Append(@"delete from [LES].[TE_ATP_SRM_PREV_SUPPLY_PLAN] where [ID] in (" + string.Join(",", prevSupplyPlanInfos.Select(d => d.Id).ToArray()) + ");");
                ///更新TE供货计划为最新版
                foreach (var s in supplyPlanInfos)
                {
                    sqlSb.AppendFormat(@"insert into [LES].[TE_ATP_SRM_PREV_SUPPLY_PLAN] 
                ([FID],[PART_NO],[SUPPLIER_NUM],[PLANT],[DELIVERY_DATE],[REQUIRE_QTY],[CREATE_USER],[CREATE_DATE],[VALID_FLAG]) 
                values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}',{4},'{5}',GETDATE(),1);"
                    , s.PartNo, s.SupplierNum, s.Plant, s.DeliveryDate, s.RequireQty.GetValueOrDefault(), loginUser);
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
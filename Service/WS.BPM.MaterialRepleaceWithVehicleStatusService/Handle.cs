namespace WS.BPM.MaterialRepleaceWithVehicleStatusService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using BLL.SYS;
    using System.Transactions;
    using System;
    using DAL.LES;

    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "VehicleStatusService";
        /// <summary>
        /// 是否启用车辆 过点替换生产订单物料清单
        /// </summary>
        private string vehicleStatusRepleacePorderBom = new ConfigBLL().GetValueByCode("VEHICLE_STATUS_REPLEACE_PORDER_BOM");
        /// <summary>
        /// 主函数
        /// </summary>
        public void Handler()
        {
            ///当系统配置标记启用时，获取车辆状态点中状态为物料替换的过点记录
            if(vehicleStatusRepleacePorderBom.ToLower()=="true")
            {
                List<VehiclePointStatusInfo> vehiclePointStatusInfos = new VehiclePointStatusBLL().GetList("and [VEHICLE_STATUS]="+(int)VehicleStatusTypeConstants.MaterialReplacement+"", string.Empty);
                if (vehiclePointStatusInfos.Count == 0) return;
                ///根据其生产订单号判断该生产订单是否已经进行过物料替换，若已替换则不能重复替换
                List<PullOrdersInfo> pullOrdersInfos = new PullOrdersBLL().GetList("" +
                    " and [ORDER_NO] in ('" + string.Join("','", vehiclePointStatusInfos.Select(d => d.OrderNo).ToArray()) + "')" +
                    " and [CHANGE_FLAG]="+(int)ChangeFlagConstants.NotReplaced+"", string.Empty);
                if (pullOrdersInfos.Count == 0) return;
                foreach(PullOrdersInfo pullOrdersInfo in pullOrdersInfos)
                {
                    StringBuilder @string = new StringBuilder();
                    ///更改单 TT_BPM_BOM_CHANGE_ORDER
                    @string.AppendFormat(new BomChangeOrderBLL().TransitionBreakpoint(pullOrdersInfo,loginUser));
                    ///替换条件 TT_BPM_BOM_REPLEACE_CONDITION
                    @string.AppendFormat(new BomRepleaceConditionBLL().ReplacementCriteria(pullOrdersInfo,loginUser));
                    if (@string.Length>0)
                    {
                        ///更改生产订单状态为已替换
                        @string.AppendFormat("update [LES].[TT_BAS_PULL_ORDERS] set " +
                            "[CHANGE_FLAG]=" + (int)ChangeFlagConstants.Replaced + "," +
                            "[MODIFY_DATE] = GETDATE()," +
                            "[MODIFY_USER] = N'" + loginUser + "'" +
                            "where [ID]=" + pullOrdersInfo.Id + "");
                        using (TransactionScope trans = new TransactionScope()) 
                        {
                            BLL.LES.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                            trans.Complete();
                        }
                    }
                }
            }
        }


    }
}


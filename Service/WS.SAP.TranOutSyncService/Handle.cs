namespace WS.SAP.TranOutSyncService
{
    using BLL.LES;
    using BLL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Transactions;
    using System.Linq;
    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.SAP.TranOutSyncService";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            ///SAP交易记录单次传输最大行数
            string sapTranOutDataMaxRows = new ConfigBLL().GetValueByCode("SAP_TRAN_OUT_DATA_MAX_ROWS");
            ///默认为1000行
            if (!int.TryParse(sapTranOutDataMaxRows, out int maxRows)) maxRows = 1000;
            ///SAP交易记录单次传输最小行数
            string sapTranOutDataMinRows = new ConfigBLL().GetValueByCode("SAP_TRAN_OUT_DATA_MIN_ROWS");
            ///默认为1000行
            if (!int.TryParse(sapTranOutDataMinRows, out int minRows)) minRows = 20;
            ///未处理数据条件
            string untreatedDataCondition = "[LOG_FID] is NULL and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + " ";
            ///
            int dataCnt = minRows + 1;
            while (dataCnt > minRows)
            {
                ///获取数据，TODO:考虑修改为只获取主键以提高程序执行效率
                List<SapTranOutInfo> sapTranOutInfos = new SapTranOutBLL().GetListByPage(untreatedDataCondition, "[ID]", 1, maxRows, out dataCnt);
                if (dataCnt == 0) break;
                Guid logFid = Guid.NewGuid();
                ///生成发送任务
                string sql = BLL.LES.CommonBLL.GetCreateOutboundLogSql("SAP", logFid, "LES-SAP-009", string.Empty, loginUser);
                sql += "update [LES].[TI_IFM_SAP_TRAN_OUT] "
                    + "set [LOG_FID] = N'" + logFid + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                    + "where [ID] in (" + string.Join(",", sapTranOutInfos.Select(d => d.Id).ToArray()) + ");";
                using (var trans = new TransactionScope())
                {
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql);
                    trans.Complete();
                }
            }

        }
    }
}

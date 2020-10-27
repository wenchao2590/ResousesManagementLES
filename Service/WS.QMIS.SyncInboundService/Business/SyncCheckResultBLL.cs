namespace WS.QMIS.SyncInboundService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System;
    using Infrustructure.Logging;

    public partial class SyncCheckResultBLL
    {

        /// <summary>
        /// QMIS检验结果同步
        /// </summary>
        /// <returns></returns>
        public static void SyncCheckResult(string loginUser)
        {

            ///sql 添加语句
            StringBuilder sql = new StringBuilder();

            ///获取未处理的检验模式中间表数据
            List<QmisCheckResultInfo> qmisCheckResultInfos = new QmisCheckResultBLL().GetListByPage("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;

            List<long> dealedIds = new List<long>();

            foreach (var item in qmisCheckResultInfos)
            {
                ///获取入库单明细表中,零件未检测状态的信息
                var maintainPartsInfo = new ReceiveDetailBLL().GetListBySql("SELECT TOP 1 * FROM LES.TT_WMM_RECEIVE_DETAIL WHERE RECEIVE_FID IN( SELECT FID FROM LES.TT_WMM_RECEIVE WHERE RECEIVE_NO = '" + item.AsnNo + "' ) AND INSPECTION_STATUS = " + (int)InspectionStatusConstants.Unchecked + " AND PART_NO='" + item.PartNo + "' ORDER BY id ASC ").FirstOrDefault();
                dealedIds.Add(item.Id);
                if (maintainPartsInfo == null)
                {
                    continue;
                }

                sql.Append("UPDATE [LES].[TT_WMM_RECEIVE_DETAIL] SET "
                      + "[INSPECTION_STATUS]=" + item.CheckStatus
                      + ",[QUALIFIED_QTY]=" + (item.TotalNo.GetValueOrDefault() - item.UnqualifiedNo.GetValueOrDefault()) ///送检数量-不合格数量=合格数量
                        + " WHERE ID = " + maintainPartsInfo.Id + " ; ");
                sql.AppendLine(); 
            }
  
            if (dealedIds.Count > 0)
                ///中间表数据更新为已处理状态, 修改时间,修改人
                sql.Append("update [LES].[TI_IFM_QMIS_CHECK_RESULT] "
                    + "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + ",[PROCESS_TIME] = GETDATE() ,"+
                    " [MODIFY_DATE]=GETDATE(),[MODIFY_USER]='" + loginUser + "'"+
                    " where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");

            if (sql.ToString().Length > 0)
            {
                Log.WriteLogToFile(sql.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql.ToString());
            }

        }

    }
}
;
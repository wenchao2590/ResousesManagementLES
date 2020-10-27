namespace WS.MES.SyncInboundService
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

    /// <summary>
    /// MES-LES-007	产线级工艺顺序  --LES工位的顺序
    /// </summary>
    public partial class SyncProductionlineProcessOrderBLL
    {

        /// <summary>
        ///生产线以及工位信息同步
        /// </summary>
        /// <returns></returns>
        public static void SyncProductionlineProcessOrder(string loginUser)
        {

            ///sql 添加语句
            StringBuilder sql = new StringBuilder();

            ///获取未处理的工艺顺序中间表数据
            List<MesProductionlineProcessOrderInfo>  mesProductionlineProcessOrderInfos = new MesProductionlineProcessOrderBLL().GetListByPage("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;

            ///获取所有基础工位信息
            List<LocationInfo> locationInfos = new LocationBLL().GetList(string.Empty, "ID");
            List<long> dealedIds = new List<long>();

            /// 循环未处理状态的中间表信息
            foreach (var mesProductionlineProcessOrderInfo in mesProductionlineProcessOrderInfos)
            {
                ///判断是否存在该工位信息. 不存在跳出如果存在更新顺序号码
                LocationInfo maintainPartsInfo = locationInfos.FirstOrDefault(d => d.Plant == mesProductionlineProcessOrderInfo.Enterprise && d.Workshop==mesProductionlineProcessOrderInfo.SiteNo && d.AssemblyLine== mesProductionlineProcessOrderInfo.AreaNo &&d.Location== mesProductionlineProcessOrderInfo.Stationcode);
                if (maintainPartsInfo == null) continue;
                 
                /// 如果工位不为空, 进行工位顺序更新
                sql.Append("UPDATE [LES].[TM_BAS_LOCATION] SET  SEQUENCE_NO='"+ mesProductionlineProcessOrderInfo.SeqNo+ "' WHERE [ID]='" + maintainPartsInfo.Id + "';");
                dealedIds.Add(mesProductionlineProcessOrderInfo.Id);
                continue;
            }

            if (dealedIds.Count > 0)
                ///中间表数据更新为已处理状态, 修改时间,修改人
                sql.Append("update [LES].[TI_IFM_MES_PRODUCTIONLINE_PROCESS_ORDER] "
                    + "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + ",[PROCESS_TIME] = GETDATE() , [MODIFY_DATE]=GETDATE(),[MODIFY_USER]='" + loginUser   
                    + "' where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");

            if (sql.ToString().Length > 0)
            {
                Log.WriteLogToFile(sql.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql.ToString());
            }
        }
    }
}

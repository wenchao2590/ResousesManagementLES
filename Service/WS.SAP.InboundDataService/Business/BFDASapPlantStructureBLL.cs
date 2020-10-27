namespace WS.SAP.InboundDataService
{
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// BFDASapPlantStructureBLL
    /// 工厂布局基础数据接收
    /// </summary>
    public class BFDASapPlantStructureBLL : IBusiness<SapPlantStructureInfo, BFDASapPlantStructureInfo>
    {
       
        #region Common
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SAP.InboundDataService";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();
        /// <summary>
        /// log日志名称
        /// </summary>
        string interfaceCode = "SAP-LES-018-SAP工厂布局基础数据接收";
        #endregion

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="sapPlantStructureInfo"></param>
        /// <returns></returns>
        public SapPlantStructureInfo ConversionToCentreInfo(BFDASapPlantStructureInfo sapPlantStructureInfo)
        {
            SapPlantStructureInfo plantStructureInfo = new SapPlantStructureInfo();
            plantStructureInfo.Werks = sapPlantStructureInfo.WERKS;///工厂
            plantStructureInfo.Name1 = sapPlantStructureInfo.NAME1;///工厂描述
            plantStructureInfo.Zbm = sapPlantStructureInfo.ZBM;///部门
            plantStructureInfo.Zbmms = sapPlantStructureInfo.ZBMMS;///部门描述
            plantStructureInfo.Zcj = sapPlantStructureInfo.ZCJ;///车间
            plantStructureInfo.Zcjms = sapPlantStructureInfo.ZCJMS;///车间描述
            plantStructureInfo.LineNo = sapPlantStructureInfo.LINE_NO;///生产线
            plantStructureInfo.LineNoms = sapPlantStructureInfo.LINE_NOMS;///生产线描
            plantStructureInfo.Vlsch = sapPlantStructureInfo.VLSCH;///工位
            plantStructureInfo.Txt = sapPlantStructureInfo.TXT;///工位描述
            plantStructureInfo.Zsx = sapPlantStructureInfo.ZSX;///顺序
            return plantStructureInfo;
        }
        /// <summary>
        /// 将临时报文表转换成中间表
        /// </summary>
        /// <param name="sapPlantStructureInfos"></param>
        /// <returns></returns>
        public List<SapPlantStructureInfo> ConversionToCentreList(List<BFDASapPlantStructureInfo> sapPlantStructureInfos)
        {
            List<SapPlantStructureInfo> plantStructureInfos = new List<SapPlantStructureInfo>();
            foreach (var sapPlantStructureInfo in sapPlantStructureInfos)
            {
                plantStructureInfos.Add(ConversionToCentreInfo(sapPlantStructureInfo));
            }
            return plantStructureInfos;
        }
        /// <summary>
        /// 获取单条数据的关键字
        /// </summary>
        /// <param name="sapPlantStructureInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapPlantStructureInfo sapPlantStructureInfo)
        {
            return sapPlantStructureInfo.WERKS + "|" + sapPlantStructureInfo.ZBM + "|" + sapPlantStructureInfo.ZCJ + "|" + sapPlantStructureInfo.LINE_NO + "|" + sapPlantStructureInfo.VLSCH;
        }
        /// <summary>
        /// 获取多条数据的关键字
        /// </summary>
        /// <param name="sapPlantStructureInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapPlantStructureInfo> sapPlantStructureInfos)
        {
            return string.Join(",", sapPlantStructureInfos.Select(d => d.WERKS + "|" + d.ZBM + "|" + d.ZCJ + "|" + d.LINE_NO + "|" + d.VLSCH).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="sapPlantStructureInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASapPlantStructureInfo sapPlantStructureInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 插入集合信息
        /// 工厂布局基础数据
        /// </summary>
        /// <param name="sapPlantStructureInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapPlantStructureInfo> sapPlantStructureInfos, Guid logFid, string logSql)
        {
            List<SapPlantStructureInfo> plantStructureInfos = ConversionToCentreList(sapPlantStructureInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var plantStructureInfo in plantStructureInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_PLANT_STRUCTURE] "
                   + "([FID] ,[LOG_FID] ,[WERKS] ,[NAME1] ,[ZBM] ,[ZBMMS] ,[ZCJ] ,[ZCJMS] ,[LINE_NO] ,[LINE_NOMS] ,[VLSCH] ,[TXT] ,[ZSX] ,[CREATE_USER] ,[VALID_FLAG] ,[PROCESS_FLAG] ,[PROCESS_TIME] ,[CREATE_DATE]) "
                   + "values (NEWID()," ///FID - uniqueidentifier
                   + "N'{0}'," ///LOG_FID - uniqueidentifier
                   + "N'{1}'," ///WERKS - nvarchar(4)
                   + "N'{2}'," ///NAME1 - nvarchar(32)
                   + "N'{3}'," ///ZBM - nvarchar(24)
                   + "N'{4}'," ///ZBMMS - nvarchar(128)
                   + "N'{5}'," ///ZCJ - nvarchar(24)
                   + "N'{6}'," ///ZCJMS - nvarchar(128)
                   + "N'{7}'," ///LINE_NO - nvarchar(24)
                   + "N'{8}'," ///LINE_NOMS - nvarchar(128)
                   + "N'{9}'," ///VLSCH - nvarchar(8)
                   + "N'{10}'," ///TXT - nvarchar(128)
                   + "N'{11}'," ///ZSX - nvarchar(24)
                   + "N'{12}'," ///CREATE_USER - nvarchar(32)
                   + "1," ///VALID_FLAG - bit
                   + "{13}," ///PROCESS_FLAG - int
                   + "NULL," ///PROCESS_TIME - datetime         
                   + "GETDATE()); "  ///CREATE_DATE - datetime
                   , logFid
                   , plantStructureInfo.Werks
                   , plantStructureInfo.Name1
                   , plantStructureInfo.Zbm
                   , plantStructureInfo.Zbmms
                   , plantStructureInfo.Zcj
                   , plantStructureInfo.Zcjms
                   , plantStructureInfo.LineNo
                   , plantStructureInfo.LineNoms
                   , plantStructureInfo.Vlsch
                   , plantStructureInfo.Txt
                   , plantStructureInfo.Zsx
                   , loginUser
                   , (int)ProcessFlagConstants.Untreated);
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return plantStructureInfos.Count;
        }
    }
}
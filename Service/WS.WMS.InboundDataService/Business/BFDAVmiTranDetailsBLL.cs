using DAL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;


namespace WS.VMI.InboundDataService
{

    /// <summary>
    /// 中间表业务类
    /// WMS-LES-002	出入库事务数据
    /// </summary>
    public class BFDAVmiTranDetailsBLL : IBusiness<WmsVmiTranDetailInfo, BFDAVmiTranDetailsInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.VMI.InboundDataService";
        /// <summary>
        /// 将报文表转换成中间表
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public WmsVmiTranDetailInfo ConversionToCentreInfo(BFDAVmiTranDetailsInfo tranDetailsInfo)
        {
            WmsVmiTranDetailInfo info = new WmsVmiTranDetailInfo();
            ///物料编号
            info.PartNo = tranDetailsInfo.PartNo;
            ///供应商代码
            info.SupplierCode = tranDetailsInfo.SupplierCode;
            ///拉动单号
            info.OrderNo = tranDetailsInfo.OrderNo;
            ///数量
            info.Qty = tranDetailsInfo.Qty;
            ///交易类型
            info.TransactionType = tranDetailsInfo.TransactionType;
            ///时间
            info.Times = tranDetailsInfo.Times;
            ///VMI仓库代码
            info.VmiWarehouseCode = tranDetailsInfo.VmiWarehouseCode;
            return info;
        }
        /// <summary>
        /// 将报文表集合转换成中间表集合
        /// </summary>
        /// <param name="tranDetailsInfos"></param>
        /// <returns></returns>
        public List<WmsVmiTranDetailInfo> ConversionToCentreList(List<BFDAVmiTranDetailsInfo> tranDetailsInfos)
        {
            List<WmsVmiTranDetailInfo> list = new List<WmsVmiTranDetailInfo>();
            foreach (BFDAVmiTranDetailsInfo tranDetailsInfo in tranDetailsInfos)
            {
                list.Add(ConversionToCentreInfo(tranDetailsInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键词-物料编号(主键)
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAVmiTranDetailsInfo tranDetailsInfo)
        {
            return tranDetailsInfo.PartNo + "|" + tranDetailsInfo.SupplierCode + "|" + tranDetailsInfo.TransactionType + "|" + tranDetailsInfo.Qty.GetValueOrDefault().ToString("F0");
        }
        /// <summary>
        /// 获取所有关键词集合-物料编号(主键)
        /// </summary>
        /// <param name="tranDetailsInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAVmiTranDetailsInfo> tranDetailsInfos)
        {
            return string.Join(",", tranDetailsInfos.Select(d => d.PartNo + "|" + d.SupplierCode + "|" + d.TransactionType + "|" + d.Qty.GetValueOrDefault().ToString("F0")).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAVmiTranDetailsInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 插入集合信息
        /// </summary>
        /// <param name="tranDetailsInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAVmiTranDetailsInfo> tranDetailsInfos, Guid logFid, string logSql)
        {
            List<WmsVmiTranDetailInfo> list = ConversionToCentreList(tranDetailsInfos);
            StringBuilder @string = new StringBuilder(logSql);
            foreach (WmsVmiTranDetailInfo info in list)
            {
                ///LOG_FID
                info.LogFid = logFid;
                ///PROCESS_FLAG
                info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                ///CREATE_USER
                info.CreateUser = loginUser;
                ///
                @string.AppendLine(WmsVmiTranDetailDAL.GetInsertSql(info));
            }
            ///
            using (TransactionScope tran = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                tran.Complete();
            }
            return list.Count;
        }
    }
}
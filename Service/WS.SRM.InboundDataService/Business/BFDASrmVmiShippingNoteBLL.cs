using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Utilities;
using System.Text;
using System.Xml;
using DAL.LES;
using System.Transactions;

namespace WS.SRM.InboundDataService
{
    /// <summary>
    /// SRM-LES-003 - 物料发货单 
    /// </summary>
    public class BFDASrmVmiShippingNoteBLL : IBusiness<SrmVmiShippingNoteInfo, BFDASrmShippingNoteInfo>
    {

        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SRM.InboundDataService";
        /// <summary>
        /// LES时间格式
        /// </summary>
        private string lesDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 把报文表转换成 中间表数据
        /// </summary>
        /// <param name="noteInfo"></param>
        /// <returns></returns>
        public SrmVmiShippingNoteInfo ConversionToCentreInfo(BFDASrmShippingNoteInfo noteInfo)
        {
            ///
            SrmVmiShippingNoteInfo info = new SrmVmiShippingNoteInfo();
            ///工厂代码
            info.Plant = noteInfo.Plant;
            ///发货单号
            info.ShippingCode = noteInfo.ShippingCode;
            ///供应商代码
            info.SupplierCode = noteInfo.SupplierCode;
            ///到货时间
            info.DeliveryTime = CommonBLL.TryParseDatetime(noteInfo.DeliveryTime, lesDateTimeFormat);
            ///VMI仓库代码
            info.VmiWmNo = noteInfo.VmiWarehouseCode;
            ///物料明细
            info.listNoteDetails = new List<SrmVmiShippingNoteDetailInfo>();
            foreach (BFDASrmVmiShippingNotePartInfo srmVmiShippingNotePartInfo in noteInfo.listNotepartInfos)
            {
                SrmVmiShippingNoteDetailInfo noteDetailInfo = new SrmVmiShippingNoteDetailInfo();
                ///物料编号
                noteDetailInfo.Partno = srmVmiShippingNotePartInfo.PartNo;
                ///数量
                decimal.TryParse(srmVmiShippingNotePartInfo.PartQty, out decimal decQty);
                noteDetailInfo.Partqty = decQty;
                ///备注
                noteDetailInfo.Remark = srmVmiShippingNotePartInfo.Remark;
                ///
                info.listNoteDetails.Add(noteDetailInfo);
            }
            ///
            return info;
        }

        /// <summary>
        /// 把多条报文表集合整合到 中间表集合
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <returns></returns>
        public List<SrmVmiShippingNoteInfo> ConversionToCentreList(List<BFDASrmShippingNoteInfo> noteInfos)
        {
            List<SrmVmiShippingNoteInfo> list = new List<SrmVmiShippingNoteInfo>();
            foreach (var noteInfo in noteInfos)
            {
                list.Add(ConversionToCentreInfo(noteInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取单条实体的关键主键值
        /// </summary>
        /// <param name="noteInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASrmShippingNoteInfo noteInfo)
        {
            return noteInfo.ShippingCode;
        }
        /// <summary>
        /// 获取多条数据库的关键主键值
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASrmShippingNoteInfo> noteInfos)
        {
            return string.Join(",", noteInfos.Select(d => d.ShippingCode).ToArray());
        }
        /// <summary>
        /// 单条插入
        /// </summary>
        /// <param name="bFDASrmShippingNoteInfo"></param>
        /// <param name="logFid"></param>
        public void InsertInfoToCentreTable(BFDASrmShippingNoteInfo bFDASrmShippingNoteInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 把报文数据保存到中间表集合
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASrmShippingNoteInfo> noteInfos, Guid logFid, string logSql)
        {
            ///转换成中间表集合
            List<SrmVmiShippingNoteInfo> list = ConversionToCentreList(noteInfos);
            StringBuilder @string = new StringBuilder(logSql);
            foreach (SrmVmiShippingNoteInfo info in list)
            {
                ///FID
                info.Fid = Guid.NewGuid();
                ///PROCESS_FLAG
                info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                ///LOG_FID
                info.LogFid = logFid;
                ///DELETE_FLAG
                info.DeleteFlag = null;
                ///CREATE_USER
                info.CreateUser = loginUser;
                ///
                @string.AppendLine(SrmVmiShippingNoteDAL.GetInsertSql(info));
                foreach (SrmVmiShippingNoteDetailInfo detailInfo in info.listNoteDetails)
                {
                    ///NOTE_FID
                    detailInfo.NoteFid = info.Fid;
                    ///CREATE_USER
                    detailInfo.CreateUser = loginUser;
                    ///
                    @string.AppendLine(SrmVmiShippingNoteDetailDAL.GetInsertSql(detailInfo));
                }
            }
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
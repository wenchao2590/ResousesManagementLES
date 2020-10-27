using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DM.SYS;
using Infrustructure.Utilities;
using BLL.LES;
using DAL.LES;
using System.Transactions;

namespace WS.SRM.InboundDataService
{

    /// <summary>
    /// LES-SRM-006	物料送货单
    /// </summary>
    public class BFDASrmAsnRunsheetBLL : IBusiness<SrmDeliveryNoteInfo, BFDASrmVmiDeliveryNoteInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SRM.InboundDataService";

        private string lesDateTime = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 将中间表转换成临时表
        /// </summary>
        /// <param name="noteInfo"></param>
        /// <returns></returns>
        public SrmDeliveryNoteInfo ConversionToCentreInfo(BFDASrmVmiDeliveryNoteInfo noteInfo)
        {
            ///
            SrmDeliveryNoteInfo info = new SrmDeliveryNoteInfo();
            ///送货单号
            info.Ordercode = noteInfo.OrderCode;
            ///原始单据类型
            int.TryParse(noteInfo.SourceOrderType, out int orderType);
            info.Sourceordertype = orderType;
            ///道口
            info.Dock = noteInfo.Dock;
            ///发单时间
            info.Publishtime = CommonBLL.TryParseDatetime(noteInfo.PublishTime, lesDateTime);
            ///供应商代码
            info.Suppliercode = noteInfo.SupplierCode;
            ///供应商名称
            info.Suppliername = noteInfo.SupplierName;
            ///来源存储区代码
            info.Sourcezoneno = noteInfo.SourceZoneNo;
            ///目标存储区代码
            info.Targetzoneno = noteInfo.TargetZoneNo;
            ///保管员
            info.Keeper = noteInfo.Keeper;
            ///预计发货时间
            if (info.Planshippingtime != null)
            {
                info.Planshippingtime =  CommonBLL.TryParseDatetime(noteInfo.PlanShippingTime, lesDateTime);
            }
            if (info.Plandeliverytime != null)
            {
                ///预计到货时间
                info.Plandeliverytime = CommonBLL.TryParseDatetime(noteInfo.PlanDeliveryTime, lesDateTime);
            }
            
            ///备注
            info.Remark = noteInfo.Remark;
            ///是否紧急
            info.Emergencyflag = noteInfo.EmergencyFlag == "1" ? true : false;
            ///删除标记
            info.Deleteflag = noteInfo.DeleteFlag == "1" ? true : false;
            ///工厂
            info.Plant = noteInfo.Plant;

            info.DetailInfos = new List<SrmDeliveryNoteDetailInfo>();


            foreach (BFDASrmPartDetailInfo noteDetailInfo in noteInfo.DetailInfo)
            {
                SrmDeliveryNoteDetailInfo srmDeliveryNoteDetailInfo = GetSrmDeliveryNoteDetailInfo(noteDetailInfo);
                info.DetailInfos.Add(srmDeliveryNoteDetailInfo);
            }
            ///
            return info;
        }
        /// <summary>
        /// GetSrmDeliveryNoteDetailInfo
        /// </summary>
        /// <param name="noteDetailInfo"></param>
        /// <returns></returns>
        public SrmDeliveryNoteDetailInfo GetSrmDeliveryNoteDetailInfo(BFDASrmPartDetailInfo noteDetailInfo)
        {
            SrmDeliveryNoteDetailInfo detailInfo = new SrmDeliveryNoteDetailInfo();
            ///原始单据号
            detailInfo.Sourceordercode = noteDetailInfo.SourceOrderCode;
            ///物料编号
            detailInfo.Partno = noteDetailInfo.PartNo;
            ///物料描述
            detailInfo.Partcname = noteDetailInfo.PartCName;
            ///数量
            decimal.TryParse(noteDetailInfo.PartQty, out decimal partQty);
            detailInfo.Partqty = partQty;
            ///目标库位
            detailInfo.Targetslcode = noteDetailInfo.TargetSLCode;
            ///包装型号
            detailInfo.Packagecode = noteDetailInfo.PackageCode;
            ///收容数
            decimal.TryParse(noteDetailInfo.SNP, out decimal snp);
            detailInfo.Snp = snp;
            ///备注
            detailInfo.Remark = noteDetailInfo.Remark;
            ///
            return detailInfo;
        }
        /// <summary>
        /// ConversionToCentreList
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <returns></returns>
        public List<SrmDeliveryNoteInfo> ConversionToCentreList(List<BFDASrmVmiDeliveryNoteInfo> noteInfos)
        {
            List<SrmDeliveryNoteInfo> list = new List<SrmDeliveryNoteInfo>();
            foreach (BFDASrmVmiDeliveryNoteInfo noteInfo in noteInfos)
            {
                list.Add(ConversionToCentreInfo(noteInfo));
            }
            return list;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="noteInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASrmVmiDeliveryNoteInfo noteInfo)
        {
            return noteInfo.OrderCode;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASrmVmiDeliveryNoteInfo> noteInfos)
        {
            return string.Join(",", noteInfos.Select(d => d.OrderCode).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASrmVmiDeliveryNoteInfo noteInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="noteInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASrmVmiDeliveryNoteInfo> noteInfos, Guid logFid, string logSql)
        {
            List<SrmDeliveryNoteInfo> list = ConversionToCentreList(noteInfos);
            StringBuilder @string = new StringBuilder(logSql);
            foreach (SrmDeliveryNoteInfo info in list)
            {
                ///FID
                info.Fid = Guid.NewGuid();
                ///LOG_FID
                info.LogFid = logFid;
                ///PROCESS_FLAG
                info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                ///CREATE_USER
                info.CreateUser = loginUser;
                ///
                @string.AppendLine(SrmDeliveryNoteDAL.GetInsertSql(info));
                ///
                foreach (SrmDeliveryNoteDetailInfo detailInfo in info.DetailInfos)
                {
                    ///ORDER_FID
                    detailInfo.OrderFid = info.Fid;
                    ///CREATE_USER
                    detailInfo.CreateUser = loginUser;
                    ///
                    @string.AppendLine(SrmDeliveryNoteDetailDAL.GetInsertSql(detailInfo));
                }
            }
            ///分布式事务 执行写入数据库操作. 
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return list.Count;
        }
    }
}
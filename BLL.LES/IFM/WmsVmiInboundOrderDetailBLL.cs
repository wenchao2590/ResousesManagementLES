namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// WmsVmiInboundOrderDetailBLL
    /// </summary>
    public class WmsVmiInboundOrderDetailBLL
    {
        #region Common
        WmsVmiInboundOrderDetailDAL dal = new WmsVmiInboundOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsVmiInboundOrderDetailInfo></returns>
        public List<WmsVmiInboundOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public WmsVmiInboundOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }


        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiInboundOrderDetailInfo Collection </returns>
		public List<WmsVmiInboundOrderDetailInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        public long InsertInfo(WmsVmiInboundOrderDetailInfo info)
        {
            return dal.Add(info);
        }

        public bool UpdateInfo(WmsVmiInboundOrderDetailInfo info)
        {
            return dal.Update(info) > 0 ? true : false;
        }

        public bool DeleteInfo(long id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create WmsVmiInboundOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static WmsVmiInboundOrderDetailInfo CreateWmsVmiInboundOrderDetailInfo(string loginUser)
        {
            WmsVmiInboundOrderDetailInfo wmsVmiInboundOrderDetailInfo = new WmsVmiInboundOrderDetailInfo();
            ///FID
            wmsVmiInboundOrderDetailInfo.Fid = Guid.NewGuid();
            ///REMARK
            wmsVmiInboundOrderDetailInfo.Remark = null;
            ///COMMENTS
            wmsVmiInboundOrderDetailInfo.Comments = null;
            ///VALID_FLAG
            wmsVmiInboundOrderDetailInfo.ValidFlag = true;
            ///CREATE_DATE
            wmsVmiInboundOrderDetailInfo.CreateDate = DateTime.Now;
            ///CREATE_USER
            wmsVmiInboundOrderDetailInfo.CreateUser = loginUser;
            ///MODIFY_DATE
            wmsVmiInboundOrderDetailInfo.ModifyDate = null;
            ///MODIFY_USER
            wmsVmiInboundOrderDetailInfo.ModifyUser = null;
            ///
            return wmsVmiInboundOrderDetailInfo;
        }
        /// <summary>
        /// WmsVmiInboundOrderInfo -> WmsVmiInboundOrderDetailInfo
        /// </summary>
        /// <param name="wmsVmiInboundOrderInfo"></param>
        /// <param name="wmsVmiInboundOrderDetailInfo"></param>
        public static void GetWmsVmiInboundOrderDetailInfo(WmsVmiInboundOrderInfo wmsVmiInboundOrderInfo, ref WmsVmiInboundOrderDetailInfo wmsVmiInboundOrderDetailInfo)
        {
            if (wmsVmiInboundOrderInfo == null) return;
            ///ORDER_FID
            wmsVmiInboundOrderDetailInfo.OrderFid = wmsVmiInboundOrderInfo.Fid;
        }
        /// <summary>
        /// SrmVmiShippingNoteDetailInfo -> WmsVmiInboundOrderDetailInfo
        /// </summary>
        /// <param name="srmVmiShippingNoteDetailInfo"></param>
        /// <param name="wmsVmiInboundOrderDetailInfo"></param>
        public static void GetWmsVmiInboundOrderDetailInfo(SrmVmiShippingNoteDetailInfo srmVmiShippingNoteDetailInfo, ref WmsVmiInboundOrderDetailInfo wmsVmiInboundOrderDetailInfo)
        {
            if (srmVmiShippingNoteDetailInfo == null) return;
            ///PARTNO
            wmsVmiInboundOrderDetailInfo.Partno = srmVmiShippingNoteDetailInfo.Partno;
            ///PARTQTY
            wmsVmiInboundOrderDetailInfo.Partqty = srmVmiShippingNoteDetailInfo.Partqty;
        }
        /// <summary>
        /// PartsStockInfo -> WmsVmiInboundOrderDetailInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="wmsVmiInboundOrderDetailInfo"></param>
        public static void GetWmsVmiInboundOrderDetailInfo(PartsStockInfo partsStockInfo, ref WmsVmiInboundOrderDetailInfo wmsVmiInboundOrderDetailInfo)
        {
            if (partsStockInfo == null) return;
            ///SNP
            wmsVmiInboundOrderDetailInfo.Snp = partsStockInfo.InboundPackage;
            ///PACKAGECODE
            wmsVmiInboundOrderDetailInfo.Packagecode = partsStockInfo.InboundPackageModel;
        }
        #endregion
    }
}


namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// WmsVmiInboundOrderBLL
    /// </summary>
    public class WmsVmiInboundOrderBLL
    {
        #region Common
        /// <summary>
        /// WmsVmiInboundOrderDAL
        /// </summary>
        WmsVmiInboundOrderDAL dal = new WmsVmiInboundOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsVmiInboundOrderInfo></returns>
        public List<WmsVmiInboundOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public WmsVmiInboundOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiInboundOrderInfo Collection </returns>
		public List<WmsVmiInboundOrderInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        public long InsertInfo(WmsVmiInboundOrderInfo info)
        {
            return dal.Add(info);
        }

        public bool UpdateInfo(WmsVmiInboundOrderInfo info)
        {
            return dal.Update(info) > 0 ? true : false;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<WmsVmiInboundOrderInfo> wmsVmis = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated, string.Empty);
            if (wmsVmis.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000523");///仅同步状态为待处理时可以更新已取消
            string sql = "update [LES].[TI_IFM_WMS_VMI_INBOUND_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<WmsVmiInboundOrderInfo> wmsVmis = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend, string.Empty);
            if (wmsVmis.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

            //List<SapMaterialReservationInfo> sapMaterialReservations = dal.GetList("[MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "')) and [MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "'))", string.Empty);

            string sql = "update [LES].[TI_IFM_WMS_VMI_INBOUND_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        #region Interface
        /// <summary>
        /// Create WmsVmiInboundOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static WmsVmiInboundOrderInfo CreateWmsVmiInboundOrderInfo(int processFlag, string loginUser)
        {
            WmsVmiInboundOrderInfo wmsVmiInboundOrderInfo = new WmsVmiInboundOrderInfo();
            ///FID
            wmsVmiInboundOrderInfo.Fid = Guid.NewGuid();
            ///VALID_FLAG
            wmsVmiInboundOrderInfo.ValidFlag = true;
            ///CREATE_DATE
            wmsVmiInboundOrderInfo.CreateDate = DateTime.Now;
            ///CREATE_USER
            wmsVmiInboundOrderInfo.CreateUser = loginUser;
            ///LOG_FID
            wmsVmiInboundOrderInfo.LogFid = Guid.NewGuid();
            ///PROCESS_FLAG
            wmsVmiInboundOrderInfo.ProcessFlag = processFlag;
            ///ORDER_TYPE,WMS需求填写的固定值
            wmsVmiInboundOrderInfo.OrderType = "10";
            ///STATUS
            wmsVmiInboundOrderInfo.Status = string.Empty;
            ///PROCESS_TIME
            wmsVmiInboundOrderInfo.ProcessTime = null;
            ///MODIFY_DATE
            wmsVmiInboundOrderInfo.ModifyDate = null;
            ///MODIFY_USER
            wmsVmiInboundOrderInfo.ModifyUser = string.Empty;
            ///COMMENTS
            wmsVmiInboundOrderInfo.Comments = string.Empty;
            ///
            return wmsVmiInboundOrderInfo;
        }
        /// <summary>
        /// SrmVmiShippingNoteInfo -> WmsVmiInboundOrderInfo
        /// </summary>
        /// <param name="srmVmiShippingNoteInfo"></param>
        /// <param name="wmsVmiInboundOrderInfo"></param>
        public static void GetWmsVmiInboundOrderInfo(SrmVmiShippingNoteInfo srmVmiShippingNoteInfo, ref WmsVmiInboundOrderInfo wmsVmiInboundOrderInfo)
        {
            if (srmVmiShippingNoteInfo == null) return;
            ///SHIPPING_CODE
            wmsVmiInboundOrderInfo.ShippingCode = srmVmiShippingNoteInfo.ShippingCode;
            ///SUPPLIER_CODE
            wmsVmiInboundOrderInfo.SupplierCode = srmVmiShippingNoteInfo.SupplierCode;
            ///DELIVERY_TIME
            wmsVmiInboundOrderInfo.DeliveryTime = srmVmiShippingNoteInfo.DeliveryTime;
            ///VMI_WM_NO
            wmsVmiInboundOrderInfo.VmiWmNo = srmVmiShippingNoteInfo.VmiWmNo;
            ///WERKS
            wmsVmiInboundOrderInfo.Werks = srmVmiShippingNoteInfo.Plant;
        }
        #endregion
    }
}


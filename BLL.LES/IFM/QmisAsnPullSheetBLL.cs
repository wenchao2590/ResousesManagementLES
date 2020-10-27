namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public partial class QmisAsnPullSheetBLL
    {
        #region Common
        QmisAsnPullSheetDAL dal = new QmisAsnPullSheetDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<QmisAsnPullSheetInfo></returns>
        public List<QmisAsnPullSheetInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public QmisAsnPullSheetInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>QmisAsnPullSheetInfo Collection </returns>
		public List<QmisAsnPullSheetInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }

        public long InsertInfo(QmisAsnPullSheetInfo info)
        {
            return dal.Add(info);
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
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<QmisAsnPullSheetInfo> sapMaterials = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated, string.Empty);
            if (sapMaterials.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000523");///仅同步状态为待处理时可以更新已取消
            string sql = "update [LES].[TI_IFM_QMIS_ASN_PULL_SHEET] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
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
            List<QmisAsnPullSheetInfo> sapMaterials = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend, string.Empty);
            if (sapMaterials.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

            //List<SapMaterialReservationInfo> sapMaterialReservations = dal.GetList("[MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "')) and [MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "'))", string.Empty);

            string sql = "update [LES].[TI_IFM_QMIS_ASN_PULL_SHEET] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        #region Interface
        /// <summary>
        /// Create QmisAsnPullSheetInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>QmisAsnPullSheetInfo</returns>
        public static QmisAsnPullSheetInfo CreateQmisAsnPullSheetInfo(string loginUser)
        {
            QmisAsnPullSheetInfo info = new QmisAsnPullSheetInfo();
            ///FID,
            info.Fid = Guid.NewGuid();

            ///VALID_FLAG,是否逻辑删除
            info.ValidFlag = true;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///PROCESS_FLAG,处理状态
            info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            ///ZS_FLAG,是否直送工位
            info.ZsFlag = null;
            ///EPGRP,采购组
            info.Epgrp = null;
            ///TARGET_PLACE,目标库存地
            info.TargetPlace = null;
            ///PURCHASE_GROUP,采购组
            info.PurchaseGroup = null;

            return info;
        }
        /// <summary>
        /// ReceiveDetailInfo -> QmisAsnPullSheetInfo
        /// </summary>
        /// <param name="qmisAsnPullSheetInfo"></param>
        public static void GetQmisAsnPullSheetInfo(ReceiveDetailInfo receiveDetailInfo, ref QmisAsnPullSheetInfo info)
        {
            if (receiveDetailInfo == null) return;
            ///PLANT,工厂
            info.Plant = receiveDetailInfo.Plant;
            ///ASN_NO,ASN单号
            info.AsnNo = receiveDetailInfo.TranNo;
            ///ORDER_NO,拉动单号
            info.OrderNo = receiveDetailInfo.RunsheetNo;
            ///PART_NO,物料编号
            info.PartNo = receiveDetailInfo.PartNo;
            ///SUPPLIER_NO,供应商编码
            info.SupplierNo = receiveDetailInfo.SupplierNum;
            ///ARRIVAL_DATE,预计到达时间
            info.ArrivalDate = DateTime.Now;
        }
        /// <summary>
        /// -> QmisAsnPullSheetInfo
        /// </summary>
        /// <param name="qmisAsnPullSheetInfo"></param>
        public static void GetQmisAsnPullSheetInfo(ref QmisAsnPullSheetInfo qmisAsnPullSheetInfo)
        {

        }
        #endregion

    }
}


using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SrmPullingOrderBLL
    {
        #region Common
        SrmPullingOrderDAL dal = new SrmPullingOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<SrmPullingOrderInfo></returns>
        public List<SrmPullingOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public SrmPullingOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(SrmPullingOrderInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="loginUser">用户</param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<SrmPullingOrderInfo></returns>
        public List<SrmPullingOrderInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
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
            List<SrmPullingOrderInfo> srmPullingOrders = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated, string.Empty);
            if (srmPullingOrders.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000523");///仅同步状态为待处理时可以更新已取消
            string sql = "update [LES].[TI_IFM_SRM_PULLING_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
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
            List<SrmPullingOrderInfo> srmPullingOrders = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend, string.Empty);
            if (srmPullingOrders.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

            //List<SapMaterialReservationInfo> sapMaterialReservations = dal.GetList("[MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "')) and [MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "'))", string.Empty);

            string sql = "update [LES].[TI_IFM_SRM_PULLING_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        #region Interface
        /// <summary>
        /// Create SrmPullingOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>SrmPullingOrderInfo</returns>
        public static SrmPullingOrderInfo CreateSrmPullingOrderInfo(string loginUser)
        {
            SrmPullingOrderInfo info = new SrmPullingOrderInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_NO
            info.OrderNo = null;
            ///PLANT
            info.Plant = null;
            ///SUPPLIER_NUM
            info.SupplierNum = null;
            ///SOURCE_ZONE_NO
            info.SourceZoneNo = null;
            ///KEEPER
            info.Keeper = null;
            ///TARGET_ZONE_NO
            info.TargetZoneNo = null;
            ///DOCK
            info.Dock = null;
            ///PART_BOX_CODE
            info.PartBoxCode = null;
            ///ORDER_TYPE
            info.OrderType = null;
            ///PUBLISH_TIME
            info.PublishTime = null;
            ///PART_BOX_NAME
            info.PartBoxName = null;
            ///SUPPLIER_NAME
            info.SupplierName = null;
            ///PLAN_SHIPPING_TIME
            info.PlanShippingTime = null;
            ///PLAN_DELIVERY_TIME
            info.PlanDeliveryTime = null;
            ///REMARK
            info.Remark = null;
            ///ASN_FLAG
            info.AsnFlag = null;
            ///EMERGENCY_FLAG
            info.EmergencyFlag = null;
            ///INSPECT_FLAG
            info.InspectFlag = null;
            ///PROCESS_FLAG
            info.ProcessFlag = null;
            ///PROCESS_TIME
            info.ProcessTime = null;
            ///LOG_FID
            info.LogFid = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///COMMENTS
            info.Comments = null;
            return info;
        }

        #endregion
    }
}


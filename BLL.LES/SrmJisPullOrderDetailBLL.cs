using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SrmJisPullOrderDetailBLL
    {
        #region Common
        SrmJisPullOrderDetailDAL dal = new SrmJisPullOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<SrmJisPullOrderDetailInfo></returns>
        public List<SrmJisPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public SrmJisPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(SrmJisPullOrderDetailInfo info)
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
        /// <returns>List<SrmJisPullOrderDetailInfo></returns>
        public List<SrmJisPullOrderDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create SrmJisPullOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>SrmJisPullOrderDetailInfo</returns>
        public static SrmJisPullOrderDetailInfo CreateSrmJisPullOrderDetailInfo(string loginUser)
        {
            SrmJisPullOrderDetailInfo info = new SrmJisPullOrderDetailInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_FID
            info.OrderFid = null;
            ///ROW_NO
            info.RowNo = null;
            ///ORDER_CODE
            info.OrderCode = null;
            ///VEHICLE_SEQ_NO
            info.VehicleSeqNo = null;
            ///PART_NO
            info.PartNo = null;
            ///PART_QTY
            info.PartQty = null;
            ///VEHICLE_MODEL_NO
            info.VehicleModelNo = null;
            ///VINCODE
            info.Vincode = null;
            ///CHECK_MODE
            info.CheckMode = null;
            ///REMARK
            info.Remark = null;
            ///PROCESS_FLAG
            info.ProcessFlag = null;
            ///PROCESS_TIME
            info.ProcessTime = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///COMMENTS
            info.Comments = null;
            return info;
        }

        /// <summary>
        /// SrmJisPullOrderInfo-->SrmJisPullOrderDetailInfo
        /// </summary>
        /// <param name="srmJisPullOrderInfo"></param>
        /// <param name="info"></param>
        public static void GetSrmJisPullOrderDetailByOrder(SrmJisPullOrderInfo srmJisPullOrderInfo,ref SrmJisPullOrderDetailInfo info)
        {
            //ORDER_FID
            info.OrderFid = srmJisPullOrderInfo.Fid.GetValueOrDefault();
            ///ORDER_CODE
            info.OrderCode = srmJisPullOrderInfo.OrderCode;
        }

        /// <summary>
        /// MaterialPullingOrderDetailInfo-->SrmJisPullOrderDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetSrmJisPullOrderDetailByMaterial(MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo, ref SrmJisPullOrderDetailInfo info)
        {
            ///VEHICLE_SEQ_NO
            info.VehicleSeqNo = materialPullingOrderDetailInfo.DayVehicheSeqNo.GetValueOrDefault();
            ///PART_NO
            info.PartNo = materialPullingOrderDetailInfo.PartNo;
            ///PART_QTY
            info.PartQty = materialPullingOrderDetailInfo.RequirePartQty;
            ///VEHICLE_MODEL_NO
            info.VehicleModelNo = materialPullingOrderDetailInfo.VehicheModelNo;
            ///VINCODE
            info.Vincode = materialPullingOrderDetailInfo.Vin;
            ///CHECK_MODE TODO:CHECK_MODE?
            info.CheckMode = null;
            ///COMMENTS
            info.Comments = materialPullingOrderDetailInfo.Comments;
            ///PROCESS_FLAG
            info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
        }
        #endregion
    }
}


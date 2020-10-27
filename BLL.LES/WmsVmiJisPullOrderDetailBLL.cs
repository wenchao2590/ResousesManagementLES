using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class WmsVmiJisPullOrderDetailBLL
    {
        #region Common
        WmsVmiJisPullOrderDetailDAL dal = new WmsVmiJisPullOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsVmiJisPullOrderDetailInfo></returns>
        public List<WmsVmiJisPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public WmsVmiJisPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(WmsVmiJisPullOrderDetailInfo info)
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
        /// <returns>List<WmsVmiJisPullOrderDetailInfo></returns>
        public List<WmsVmiJisPullOrderDetailInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create WmsVmiJisPullOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>WmsVmiJisPullOrderDetailInfo</returns>
        public static WmsVmiJisPullOrderDetailInfo CreateWmsVmiJisPullOrderDetailInfo(string loginUser)
        {
            WmsVmiJisPullOrderDetailInfo info = new WmsVmiJisPullOrderDetailInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///ORDER_FID
            info.OrderFid = null;
            ///ORDER_CODE
            info.OrderCode = null;
            ///VEHICLE_SEQ_NO
            info.VehicleSeqNo = null;
            ///PARTNO
            info.Partno = null;
            ///SNP
            info.Snp = null;
            ///PART_QTY
            info.PartQty = null;
            ///VEHICLE_MODEL_NO
            info.VehicleModelNo = null;
            ///VINCODE
            info.Vincode = null;
            ///REMARK
            info.Remark = null;
            ///SUPERMARKET_REPOSITORY
            info.SupermarketRepository = null;
            ///EXTERN_LINE_No
            info.ExternLineNo = null;
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
            return info;
        }

        /// <summary>
        /// WmsVmiJisPullOrderInfo-->WmsVmiJisPullOrderDetailInfo
        /// </summary>
        /// <param name="wmsVmiJisPullOrderInfo"></param>
        /// <param name="info"></param>
        public static void GetWmsVmiJisPullOrderDetailByOrder(WmsVmiJisPullOrderInfo wmsVmiJisPullOrderInfo, ref WmsVmiJisPullOrderDetailInfo info)
        {
            ///ORDER_FID
            info.OrderFid = wmsVmiJisPullOrderInfo.Fid.GetValueOrDefault();
            ///ORDER_CODE
            info.OrderCode = wmsVmiJisPullOrderInfo.OrderCode;
        }

        /// <summary>
        /// MaterialPullingOrderDetailInfo-->WmsVmiJisPullOrderDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetWmsVmiJisPullOrderDetailByMaterial(MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo, ref WmsVmiJisPullOrderDetailInfo info)
        {
            ///VEHICLE_SEQ_NO
            info.VehicleSeqNo = materialPullingOrderDetailInfo.DayVehicheSeqNo.GetValueOrDefault();
            ///PART_NO
            info.Partno = materialPullingOrderDetailInfo.PartNo;
            ///PART_QTY
            info.PartQty = materialPullingOrderDetailInfo.RequirePartQty;
            ///VEHICLE_MODEL_NO
            info.VehicleModelNo = materialPullingOrderDetailInfo.VehicheModelNo;
            ///VINCODE
            info.Vincode = materialPullingOrderDetailInfo.Vin;
            ///COMMENTS
            info.Remark = materialPullingOrderDetailInfo.Comments;
            ///PROCESS_FLAG
            info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            ///SNP ///TODO:收容数是什么？
            info.Snp = materialPullingOrderDetailInfo.PackageQty;
            ///SUPERMARKET_REPOSITORY TODO:SupermarketRepository？
            info.SupermarketRepository = null;
        }
        #endregion
    }
}


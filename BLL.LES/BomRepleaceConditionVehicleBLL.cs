using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class BomRepleaceConditionVehicleBLL
    {
        #region Common
        BomRepleaceConditionVehicleDAL dal = new BomRepleaceConditionVehicleDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BomRepleaceConditionVehicleInfo></returns>
        public List<BomRepleaceConditionVehicleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public BomRepleaceConditionVehicleInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(BomRepleaceConditionVehicleInfo info)
        {
            BomRepleaceConditionInfo conditionInfo = new BomRepleaceConditionDAL().GetList(" [FID] = N'" + info.ConditionFid + "'", string.Empty).FirstOrDefault();

            if (conditionInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (conditionInfo.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000488");///已创建状态才可进行物料添加
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
            BomRepleaceConditionVehicleInfo vehicleInfo = dal.GetInfo(id);
            if (vehicleInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            BomRepleaceConditionInfo conditionInfo = new BomRepleaceConditionDAL().GetList(" [FID] = N'" + vehicleInfo.ConditionFid + "'", string.Empty).FirstOrDefault();

            if (conditionInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (conditionInfo.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000415");///已创建状态才可进行删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            BomRepleaceConditionVehicleInfo vehicleInfo = dal.GetInfo(id);
            if (vehicleInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            BomRepleaceConditionInfo conditionInfo = new BomRepleaceConditionDAL().GetList(" [FID] = N'" + vehicleInfo.ConditionFid + "'", string.Empty).FirstOrDefault();

            if (conditionInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (conditionInfo.Status != (int)BreakPointOrderStatusConstants.Created)
                throw new Exception("MC:0x00000084");///已创建状态才可进行修改

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<BomRepleaceConditionVehicleInfo></returns>
        public List<BomRepleaceConditionVehicleInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create BomRepleaceConditionVehicleInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>BomRepleaceConditionVehicleInfo</returns>
        public static BomRepleaceConditionVehicleInfo CreateBomRepleaceConditionVehicleInfo(string loginUser)
        {
            BomRepleaceConditionVehicleInfo info = new BomRepleaceConditionVehicleInfo();
            ///ID
            info.Id = new long();
            ///FID
            info.Fid = Guid.NewGuid();
            ///CONDITION_FID
            info.ConditionFid = null;
            ///CONDITION_CODE
            info.ConditionCode = null;
            ///PART_NO
            info.PartNo = null;
            ///MODEL_YEAR
            info.ModelYear = null;
            ///FARBAU
            info.Farbau = null;
            ///PNR_STRING
            info.PnrString = null;
            ///ZCOLORI
            info.Zcolori = null;
            ///REPLEACED_VEHICLE_QTY
            info.RepleacedVehicleQty = null;
            ///STATUS
            info.Status = null;
            ///COMMENTS
            info.Comments = null;
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

        #endregion
    }
}


using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class WmsVmiJisPullOrderBLL
    {
        #region Common
        WmsVmiJisPullOrderDAL dal = new WmsVmiJisPullOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsVmiJisPullOrderInfo></returns>
        public List<WmsVmiJisPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public WmsVmiJisPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(WmsVmiJisPullOrderInfo info)
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
        /// <returns>List<WmsVmiJisPullOrderInfo></returns>
        public List<WmsVmiJisPullOrderInfo> GetList(string textWhere, string textOrder)
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
            List<WmsVmiJisPullOrderInfo> wmsVmiJisPulls = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated, string.Empty);
            if (wmsVmiJisPulls.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000523");///仅同步状态为待处理时可以更新已取消
            string sql = "update [LES].[TI_IFM_WMS_VMI_JIS_PULL_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Cancel + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
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
            List<WmsVmiJisPullOrderInfo> wmsVmiJisPulls = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend, string.Empty);
            if (wmsVmiJisPulls.Count != rowsKeyValues.Count())
                throw new Exception("MC:0x00000524");///仅同步状态为挂起时可以更新已重发

            //List<SapMaterialReservationInfo> sapMaterialReservations = dal.GetList("[MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "')) and [MATNR] in ('" + string.Join("','", rowsKeyValues.ToArray()) + "'))", string.Empty);

            string sql = "update [LES].[TI_IFM_WMS_VMI_JIS_PULL_ORDER] WITH(ROWLOCK) set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Resend + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonBLL.ExecuteNonQueryBySql(sql);
        }
        #region Interface
        /// <summary>
        /// Create WmsVmiJisPullOrderInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>WmsVmiJisPullOrderInfo</returns>
        public static WmsVmiJisPullOrderInfo CreateWmsVmiJisPullOrderInfo(string loginUser)
        {
            WmsVmiJisPullOrderInfo info = new WmsVmiJisPullOrderInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///LOG_FID
            info.LogFid = Guid.NewGuid();
            ///PLANT
            info.Plant = null;
            ///ORDER_CODE
            info.OrderCode = null;
            ///DOCK
            info.Dock = null;
            ///SEQUENCENUMBE
            info.Sequencenumbe = null;
            ///PUBLISH_TIME
            info.PublishTime = null;
            ///PART_BOX_CODE
            info.PartBoxCode = null;
            ///PART_BOX_NAME
            info.PartBoxName = null;
            ///SUPPLIER_NUM
            info.SupplierNum = null;
            ///SUPPLIER_NAME
            info.SupplierName = null;
            ///SOURCE_ZONE_NO
            info.SourceZoneNo = null;
            ///TARGET_ZONE_NO
            info.TargetZoneNo = null;
            ///START_INFOPOIN_TTIME
            info.StartInfopoinTtime = null;
            ///PLAN_DELIVERY_TIME
            info.PlanDeliveryTime = null;
            ///START_VEHICLE_SEQ_NO
            info.StartVehicleSeqNo = null;
            ///END_VEHICLESEQ_NO
            info.EndVehicleseqNo = null;
            ///LOCATION
            info.Location = null;
            ///KEEPER
            info.Keeper = null;
            ///REMARK
            info.Remark = null;
            ///DELETEFLAG
            info.Deleteflag = null;
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
        /// MaterialPullingOrderInfo-->WmsVmiJisPullOrderInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="info"></param>
        public static void GetWmsVmiJisPullOrderByMaterial(MaterialPullingOrderInfo materialPullingOrderInfo, ref WmsVmiJisPullOrderInfo info)
        {
            if (materialPullingOrderInfo == null) return;
            ///PLANT
            info.Plant = materialPullingOrderInfo.Plant;
            ///ORDER_CODE
            info.OrderCode = materialPullingOrderInfo.OrderNo;
            ///DOCK
            info.Dock = materialPullingOrderInfo.TargetDock;
            ///SEQUENCENUMBE
            info.Sequencenumbe = materialPullingOrderInfo.DayVehicheSeqNo.ToString();
            ///PUBLISH_TIME
            info.PublishTime = materialPullingOrderInfo.PublishTime.GetValueOrDefault();
            ///PART_BOX_CODE
            info.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            ///PART_BOX_NAME
            info.PartBoxName = materialPullingOrderInfo.PartBoxName;
            ///SUPPLIER_NUM
            info.SupplierNum = materialPullingOrderInfo.SupplierNum;
            ///SUPPLIER_NAME
            info.SupplierName = materialPullingOrderInfo.SupplierName;
            ///SOURCE_ZONE_NO
            info.SourceZoneNo = materialPullingOrderInfo.SourceZoneNo;
            ///TARGET_ZONE_NO
            info.TargetZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///START_INFOPOINT_TIME TODO:开始过点时间？
            info.StartInfopoinTtime = null;
            ///PLAN_DELIVERY_TIME
            info.PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime.GetValueOrDefault();
            ///START_VEHICLE_SEQ_NO
            info.StartVehicleSeqNo = materialPullingOrderInfo.StartVehicheNo.GetValueOrDefault();
            ///END_VEHICLE_SEQ_NO
            info.EndVehicleseqNo = materialPullingOrderInfo.EndVehicheNo.GetValueOrDefault();
            ///LOCATION
            info.Location = materialPullingOrderInfo.Location;
            ///COMMENTS
            info.Comments = materialPullingOrderInfo.Comments;
            ///PROCESS_FLAG
            info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
        }
        #endregion
    }
}


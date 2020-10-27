namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    public class JisCounterLogBLL
    {
        #region Common
        JisCounterLogDAL dal = new JisCounterLogDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<JisCounterLogInfo></returns>
        public List<JisCounterLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public JisCounterLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(JisCounterLogInfo info)
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
        /// <returns>List<JisCounterLogInfo></returns>
        public List<JisCounterLogInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Private
        /// <summary>
        /// Create JisCounterLogInfo
        /// </summary>
        /// <param name="counterFid"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static JisCounterLogInfo CreateJisCounterLogInfo(Guid counterFid, string loginUser)
        {
            JisCounterLogInfo jisCounterLogInfo = new JisCounterLogInfo();
            ///COUNTER_FID 
            jisCounterLogInfo.CounterFid = counterFid;
            ///PART_VERSION TODO:暂时没有物料版本
            jisCounterLogInfo.PartVersion = string.Empty;
            ///CREATE_USER
            jisCounterLogInfo.CreateUser = loginUser;
            ///
            return jisCounterLogInfo;
        }
        /// <summary>
        /// VehiclePointStatusInfo -> JisCounterLogInfo
        /// </summary>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="jisCounterLogInfo"></param>
        public static void GetJisCounterLogInfo(VehiclePointStatusInfo vehiclePointStatusInfo, ref JisCounterLogInfo jisCounterLogInfo)
        {
            if (vehiclePointStatusInfo == null) return;
            ///SOURCE_DATA_FID 
            jisCounterLogInfo.SourceDataFid = vehiclePointStatusInfo.Fid;
            ///SOURCE_DATA_TYPE 
            ///TODO:枚举不正确
            jisCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.StatePoint;
            ///SOURCE_DATA 
            jisCounterLogInfo.SourceData = vehiclePointStatusInfo.OrderNo;
        }
        /// <summary>
        /// JisPartBoxInfo -> JisCounterLogInfo
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterLogInfo"></param>
        public static void GetJisCounterLogInfo(JisPartBoxInfo jisPartBoxInfo, ref JisCounterLogInfo jisCounterLogInfo)
        {
            if (jisPartBoxInfo == null) return;
            ///SUPPLIER_NUM 
            jisCounterLogInfo.SupplierNum = jisPartBoxInfo.SupplierNum;
            ///PLANT
            jisCounterLogInfo.Plant = jisPartBoxInfo.Plant;
            ///PLANT_ZONE 
            jisCounterLogInfo.PlantZone = jisPartBoxInfo.PlantZone;
            ///WORKSHOP
            jisCounterLogInfo.Workshop = jisPartBoxInfo.Workshop;
            ///ASSEMBLY_LINE
            jisCounterLogInfo.AssemblyLine = jisPartBoxInfo.AssemblyLine;
            ///WORKSHOP_SECTION
            jisCounterLogInfo.WorkshopSection = jisPartBoxInfo.WorkshopSection;
            ///LOCATION 
            jisCounterLogInfo.Location = jisPartBoxInfo.Location;
            ///COMMENTS 
            jisCounterLogInfo.Comments = jisPartBoxInfo.Comments;
        }
        /// <summary>
        /// MaintainInhouseLogisticStandardInfo -> JisCounterLogInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="jisCounterLogInfo"></param>
        public static void GetJisCounterLogInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, ref JisCounterLogInfo jisCounterLogInfo)
        {
            if (maintainInhouseLogisticStandardInfo == null) return;
            ///PART_NO 
            jisCounterLogInfo.PartNo = maintainInhouseLogisticStandardInfo.PartNo;
            ///PART_CNAME 
            jisCounterLogInfo.PartCname = maintainInhouseLogisticStandardInfo.PartCname;
            ///SUPPLIER_NUM 
            if (string.IsNullOrEmpty(jisCounterLogInfo.SupplierNum))
                jisCounterLogInfo.SupplierNum = maintainInhouseLogisticStandardInfo.SupplierNum;
            ///PLANT 
            if (string.IsNullOrEmpty(jisCounterLogInfo.Plant))
                jisCounterLogInfo.Plant = maintainInhouseLogisticStandardInfo.Plant;
            ///PLANT_ZONE 
            if (string.IsNullOrEmpty(jisCounterLogInfo.PlantZone))
                jisCounterLogInfo.PlantZone = maintainInhouseLogisticStandardInfo.PlantZone;
            ///WORKSHOP 
            if (string.IsNullOrEmpty(jisCounterLogInfo.Workshop))
                jisCounterLogInfo.Workshop = maintainInhouseLogisticStandardInfo.Workshop;
            ///ASSEMBLY_LINE 
            if (string.IsNullOrEmpty(jisCounterLogInfo.AssemblyLine))
                jisCounterLogInfo.AssemblyLine = maintainInhouseLogisticStandardInfo.AssemblyLine;
            ///WORKSHOP_SECTION 
            if (string.IsNullOrEmpty(jisCounterLogInfo.WorkshopSection))
                jisCounterLogInfo.WorkshopSection = maintainInhouseLogisticStandardInfo.WorkshopSection;
            ///LOCATION 
            if (string.IsNullOrEmpty(jisCounterLogInfo.Location))
                jisCounterLogInfo.Location = maintainInhouseLogisticStandardInfo.Location;
        }
        #endregion
    }
}


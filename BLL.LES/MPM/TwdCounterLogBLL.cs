using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class TwdCounterLogBLL
    {
        #region Common
        TwdCounterLogDAL dal = new TwdCounterLogDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdCounterLogInfo></returns>
        public List<TwdCounterLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public TwdCounterLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(TwdCounterLogInfo info)
        {
            return dal.Add(info);
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

        #region TwdCounterLogInfo
        /// <summary>
        /// 创建 TwdCounterLog
        /// </summary>
        /// <param name="counterFid"></param>
        /// <returns></returns>
        public static TwdCounterLogInfo CreateTwdCounterLogInfo(Guid counterFid, string loginUser)
        {
            TwdCounterLogInfo twdCounterLogInfo = new TwdCounterLogInfo();
            ///COUNTER_FID 
            twdCounterLogInfo.CounterFid = counterFid;
            ///PART_VERSION 
            ///TODO:暂时没有物料版本
            twdCounterLogInfo.PartVersion = string.Empty;
            ///CREATE_USER
            twdCounterLogInfo.CreateUser = loginUser;
            ///
            return twdCounterLogInfo;
        }
        /// <summary>
        /// TwdCounterInfo -> TwdCounterLogInfo
        /// </summary>
        /// <param name="twdCounterInfo"></param>
        /// <param name="info"></param>
        public static void GetTwdCounterLogInfo(TwdCounterInfo twdCounterInfo, ref TwdCounterLogInfo info)
        {
            if (twdCounterInfo == null) return;
            ///COUNTER_FID,计数器外键
            info.CounterFid = twdCounterInfo.Fid;
            ///REQUIREMENT_ACCUMULATE_MODE,需求累计方式
            info.RequirementAccumulateMode = twdCounterInfo.RequirementAccumulateMode;
            ///PART_NO,物料号
            info.PartNo = twdCounterInfo.PartNo;
            ///PART_CNAME,物料中文描述
            info.PartCname = twdCounterInfo.PartCname;
            ///SUPPLIER_NUM,供应商
            info.SupplierNum = twdCounterInfo.SupplierNum;
            ///PLANT,工厂
            info.Plant = twdCounterInfo.Plant;
            ///PLANT_ZONE,区域
            info.PlantZone = twdCounterInfo.PlantZone;
            ///WORKSHOP,车间
            info.Workshop = twdCounterInfo.Workshop;
            ///ASSEMBLY_LINE,生产线
            info.AssemblyLine = twdCounterInfo.AssemblyLine;
            ///WORKSHOP_SECTION,工段
            info.WorkshopSection = twdCounterInfo.WorkshopSection;
            ///LOCATION,工位
            info.Location = twdCounterInfo.Location;
            ///PACKAGE,箱内数量
            info.Package = twdCounterInfo.Package;
            ///PACKAGE_MODEL,包装容器
            info.PackageModel = twdCounterInfo.PackageModel;
        }
        /// <summary>
        /// VehiclePointStatusInfo -> TwdCounterLogInfo
        /// </summary>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="twdCounterLogInfo"></param>
        public static void GetTwdCounterLogInfo(VehiclePointStatusInfo vehiclePointStatusInfo, ref TwdCounterLogInfo twdCounterLogInfo)
        {
            if (vehiclePointStatusInfo == null) return;
            ///SOURCE_DATA_FID 
            twdCounterLogInfo.SourceDataFid = vehiclePointStatusInfo.Fid;
            ///SOURCE_DATA_TYPE 
            twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.StatePoint;
            ///SOURCE_DATA 
            twdCounterLogInfo.SourceData = vehiclePointStatusInfo.OrderNo;
        }
        /// <summary>
        /// MaintainInhouseLogisticStandardInfo -> TwdCounterLogInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="twdCounterLogInfo"></param>
        public static void GetTwdCounterLogInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, ref TwdCounterLogInfo twdCounterLogInfo)
        {
            if (maintainInhouseLogisticStandardInfo == null) return;
            ///PART_NO 
            twdCounterLogInfo.PartNo = maintainInhouseLogisticStandardInfo.PartNo;
            ///PART_CNAME 
            twdCounterLogInfo.PartCname = maintainInhouseLogisticStandardInfo.PartCname;
            ///SUPPLIER_NUM 
            twdCounterLogInfo.SupplierNum = maintainInhouseLogisticStandardInfo.SupplierNum;
            ///PLANT
            twdCounterLogInfo.Plant = maintainInhouseLogisticStandardInfo.Plant;
            ///PLANT_ZONE 
            twdCounterLogInfo.PlantZone = maintainInhouseLogisticStandardInfo.PlantZone;
            ///WORKSHOP
            twdCounterLogInfo.Workshop = maintainInhouseLogisticStandardInfo.Workshop;
            ///ASSEMBLY_LINE
            twdCounterLogInfo.AssemblyLine = maintainInhouseLogisticStandardInfo.AssemblyLine;
            ///WORKSHOP_SECTION
            twdCounterLogInfo.WorkshopSection = maintainInhouseLogisticStandardInfo.WorkshopSection;
            ///LOCATION 
            twdCounterLogInfo.Location = maintainInhouseLogisticStandardInfo.Location;
            ///PACKAGE
            twdCounterLogInfo.Package = maintainInhouseLogisticStandardInfo.InboundPackage.GetValueOrDefault();
            ///PACKAGE_MODEL
            twdCounterLogInfo.PackageModel = maintainInhouseLogisticStandardInfo.InboundPackageModel;
        }
        /// <summary>
        /// TwdPartBoxInfo -> TwdCounterLogInfo
        /// </summary>
        /// <param name="twdPartBoxInfo"></param>
        /// <param name="twdCounterLogInfo"></param>
        public static void GetTwdCounterLogInfo(TwdPartBoxInfo twdPartBoxInfo, ref TwdCounterLogInfo twdCounterLogInfo)
        {
            if (twdPartBoxInfo == null) return;
            ///REQUIREMENT_ACCUMULATE_MODE
            twdCounterLogInfo.RequirementAccumulateMode = twdPartBoxInfo.RequirementAccumulateMode;
        }
        #endregion
    }
}


using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class MesVehiclePointScanBLL
    {
        #region Common
        MesVehiclePointScanDAL dal = new MesVehiclePointScanDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<MesVehiclePointScanInfo></returns>
        public List<MesVehiclePointScanInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<MesVehiclePointScanInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        public List<MesVehiclePointScanInfo> GetListBySql()
        {
            return dal.GetListBySql();
        }
        public MesVehiclePointScanInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(MesVehiclePointScanInfo info)
        {
            if (info.SiteNo != null)
            {
                PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(info.DmsNo);
                if (pullOrdersInfo == null)
                    throw new Exception("MC:0x00000511");///生成订单号不存在

                MesVehiclePointScanInfo mesVehiclePointScanInfo = new MesVehiclePointScanInfo();
                mesVehiclePointScanInfo.Fid = Guid.NewGuid();
                ///ENTERPRISE 	工厂编号
                mesVehiclePointScanInfo.Enterprise = new PlantBLL().GetSapPlantByPlantCode(pullOrdersInfo.Werk);
                ///车间
                mesVehiclePointScanInfo.SiteNo = null;
                ///AREA_NO     生产线编号
                mesVehiclePointScanInfo.AreaNo = new AssemblyLineBLL().GetSapAssemblyLineByAssemblyLine(pullOrdersInfo.AssemblyLine);
                //UNIT_NO     采集点编号
                mesVehiclePointScanInfo.UnitNo = info.UnitNo;
                //DMS_SEQ     过点顺序号
                mesVehiclePointScanInfo.DmsSeq = Convert.ToInt32(pullOrdersInfo.VehicleOrder);
                //DMS_NO      计划订单号
                mesVehiclePointScanInfo.DmsNo = pullOrdersInfo.OrderNo;
                mesVehiclePointScanInfo.ProcessFlag = (int)ProcessFlagResuitsConstants.Created;
                //SEND_TIME 	发送时间
                mesVehiclePointScanInfo.SendTime = DateTime.Now;
                mesVehiclePointScanInfo.ValidFlag = true;
                mesVehiclePointScanInfo.CreateDate = DateTime.Now;
                mesVehiclePointScanInfo.CreateUser = "SimulationoOverPoint";
                return dal.Add(mesVehiclePointScanInfo);
            }
            else
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

        #endregion
    }
}


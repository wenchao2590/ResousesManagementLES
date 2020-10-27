using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class VehicleStatusBLL
    {
        #region Common
        VehicleStatusDAL dal = new VehicleStatusDAL();
        public List<VehicleStatusInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public VehicleStatusInfo SelectInfo(string plant,string vehicleStatus)
        {
            return dal.GetInfo(plant,vehicleStatus);
        }

        public bool InsertInfo(VehicleStatusInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(string plant,string vehicleStatus)
        {
            return dal.Delete(plant,vehicleStatus) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string plant,string vehicleStatus)
        {
            return dal.UpdateInfo(fields, plant,vehicleStatus) > 0 ? true : false;
        }

        #endregion
    }
}


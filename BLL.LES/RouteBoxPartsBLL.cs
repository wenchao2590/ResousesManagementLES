using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{

    public class RouteBoxPartsBLL
    {
        #region Common
        RouteBoxPartsDAL dal = new RouteBoxPartsDAL();
        public List<RouteBoxPartsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public RouteBoxPartsInfo SelectInfo(string plant, string assemblyLine, string boxParts)
        {
            return dal.GetInfo(plant, assemblyLine, boxParts);
        }

        public bool InsertInfo(RouteBoxPartsInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(RouteBoxPartsInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(string plant, string assemblyLine, string boxParts)
        {
            return dal.Delete(plant, assemblyLine, boxParts) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string plant, string assemblyLine, string boxParts)
        {
            return dal.UpdateInfo(fields, plant, assemblyLine, boxParts) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}



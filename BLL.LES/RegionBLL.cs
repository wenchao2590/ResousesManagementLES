using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class RegionBLL
    {
        #region Common
        RegionDAL dal = new RegionDAL();
        public List<RegionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public RegionInfo SelectInfo(int regionIdentity, string plant, string assemblyLine)
        {
            return dal.GetInfo(regionIdentity, plant, assemblyLine);
        }

        public bool InsertInfo(RegionInfo info)
        {
            return dal.Add(info) > 0 ? true : false;
        }

        //public bool UpdateInfo(RegionInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int regionIdentity, string plant, string assemblyLine)
        {
            return dal.Delete(regionIdentity, plant, assemblyLine) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int regionIdentity, string plant, string assemblyLine)
        {
            return dal.UpdateInfo(fields, regionIdentity, plant, assemblyLine) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


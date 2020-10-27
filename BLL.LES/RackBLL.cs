using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class RackBLL
    {
        #region Common
        RackDAL dal = new RackDAL();
        public List<RackInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public RackInfo SelectInfo(string plant, string assemblyLine, string rack)
        {
            return dal.GetInfo(plant, assemblyLine, rack);
        }

        public bool InsertInfo(RackInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(RackInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(string plant, string assemblyLine, string rack)
        {
            return dal.Delete(plant, assemblyLine, rack) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string plant, string assemblyLine, string rack)
        {
            return dal.UpdateInfo(fields, plant, assemblyLine, rack) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


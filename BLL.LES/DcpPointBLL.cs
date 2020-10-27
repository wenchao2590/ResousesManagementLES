using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class DcpPointBLL
    {
        #region Common
        DcpPointDAL dal = new DcpPointDAL();
        public List<DcpPointInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public DcpPointInfo SelectInfo(string plant,string assemblyLine,string dcpPoint)
        {
            return dal.GetInfo(plant,assemblyLine,dcpPoint);
        }

        public bool InsertInfo(DcpPointInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(string plant,string assemblyLine,string dcpPoint)
        {
            return dal.Delete(plant,assemblyLine,dcpPoint) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string plant,string assemblyLine,string dcpPoint)
        {
            return dal.UpdateInfo(fields, plant,assemblyLine,dcpPoint) > 0 ? true : false;
        }

        #endregion
    }
}


using BLL.BAS;
using DAL.BAS;
using DM.BAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BAS
{
    public class HxdmkBLL
    {
        #region Common
        HxdmkDAL dal = new HxdmkDAL();
        public List<HxdmkInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(HxdmkInfo info)
        {
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, int nid)
        {
            return dal.UpdateInfo(fields, nid) > 0 ? true : false;
        }
        public bool UpdateInfo(HxdmkInfo info)
        {
            return dal.Update(info) > 0 ? true : false;
        }
        public HxdmkInfo SelectInfo(int nid)
        {
            return dal.GetInfo(nid);
        }
        public bool DeleteInfo(int nid)
        {
            return dal.Delete(nid) > 0 ? true : false;
        }
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }
        #endregion
    }
}

using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageTranBLL
    {
        #region Common
        PackageTranDAL dal = new PackageTranDAL();
        public List<PackageTranInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageTranInfo SelectInfo(long tranId)
        {
            return dal.GetInfo(tranId);
        }

        public long InsertInfo(PackageTranInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(PackageTranInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(long tranId)
        {
            return dal.Delete(tranId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long tranId)
        {
            return dal.UpdateInfo(fields, tranId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


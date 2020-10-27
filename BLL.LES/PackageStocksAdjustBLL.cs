using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageStocksAdjustBLL
    {
        #region Common
        PackageStocksAdjustDAL dal = new PackageStocksAdjustDAL();
        public List<PackageStocksAdjustInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageStocksAdjustInfo SelectInfo(int adjustId)
        {
            return dal.GetInfo(adjustId);
        }

        public int InsertInfo(PackageStocksAdjustInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(PackageStocksAdjustInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int adjustId)
        {
            return dal.Delete(adjustId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int adjustId)
        {
            return dal.UpdateInfo(fields, adjustId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


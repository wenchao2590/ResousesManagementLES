using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class MaterialRequestsBLL
    {
        #region Common
        MaterialRequestsDAL dal = new MaterialRequestsDAL();
        public List<MaterialRequestsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public MaterialRequestsInfo SelectInfo(int interfaceId)
        {
            return dal.GetInfo(interfaceId);
        }

        public int InsertInfo(MaterialRequestsInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(MaterialRequestsInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int interfaceId)
        {
            return dal.Delete(interfaceId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int interfaceId)
        {
            return dal.UpdateInfo(fields, interfaceId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


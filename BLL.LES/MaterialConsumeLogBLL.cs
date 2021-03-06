using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class MaterialConsumeLogBLL
    {
        #region Common
        MaterialConsumeLogDAL dal = new MaterialConsumeLogDAL();
        public List<MaterialConsumeLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public MaterialConsumeLogInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }

        public int InsertInfo(MaterialConsumeLogInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(MaterialConsumeLogInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


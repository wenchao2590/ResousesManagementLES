using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class ButtonRequestBLL
    {
        #region Common
        ButtonRequestDAL dal = new ButtonRequestDAL();
        public List<ButtonRequestInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public ButtonRequestInfo SelectInfo(int requestSn)
        {
            return dal.GetInfo(requestSn);
        }

        public int InsertInfo(ButtonRequestInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(ButtonRequestInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int requestSn)
        {
            return dal.Delete(requestSn) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int requestSn)
        {
            return dal.UpdateInfo(fields, requestSn) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}


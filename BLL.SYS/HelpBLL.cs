using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class HelpBLL
    {
        #region Common
        HelpDAL dal = new HelpDAL();
        public List<HelpInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(HelpInfo info)
        {
            info.ValidFlag = true;
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, DateTime modifyDate, string modifyUser)
        {
            return dal.LogicDelete(id,  modifyUser) > 0 ? true : false;
        }
        public HelpInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

using DAL.GJS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class ClkBLL
    {
        #region Common
        ClkDAL dal = new ClkDAL();
        public List<ClkInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public ClkInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(ClkInfo info)
        {
            info.ValidFlag = true;
            ValidInfo(info);
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        private void ValidInfo(ClkInfo info)
        {
            ///代码不允许重复
            int cnt = dal.GetCounts("and [DM] = '" + info.Dm + "' and [VALID_FLAG] = 1");
            if (cnt > 0)
                throw new Exception("Err:代码不允许重复");
        }
    }
}


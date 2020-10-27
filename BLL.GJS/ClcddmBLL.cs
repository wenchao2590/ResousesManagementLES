using DAL.GJS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class ClcddmBLL
    {
        #region Common
        ClcddmDAL dal = new ClcddmDAL();
        public List<ClcddmInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public ClcddmInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(ClcddmInfo info)
        {
            info.ValidFlag = true;
            ///校验
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

        private void ValidInfo(ClcddmInfo info)
        {
            ///代码不允许重复
            int cnt = dal.GetCounts("and ([DM] = '" + info.Dm + "' or [MC] = '" + info.Mc + "') and [VALID_FLAG] = 1");
            if (cnt > 0)
                throw new Exception("代码不允许重复");
        }
    }
}


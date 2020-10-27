using DAL.GJS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class Db1BLL
    {
        #region Common
        Db1DAL dal = new Db1DAL();
        public List<Db1Info> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public Db1Info SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(Db1Info info)
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

        private void ValidInfo(Db1Info info)
        {
            ///代码或名称不允许重复
            int cnt = dal.GetCounts("and ([F9] = '" + info.F9 + "' or [F8] = '" + info.F8 + "') and [VALID_FLAG] = 1");
            if (cnt > 0)
                throw new Exception("Err:代码或名称不允许重复");
        }
    }
}


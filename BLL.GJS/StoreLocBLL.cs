using DAL.GJS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class StoreLocBLL
    {
        #region Common
        StoreLocDAL dal = new StoreLocDAL();
        public List<StoreLocInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public StoreLocInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(StoreLocInfo info)
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

        private void ValidInfo(StoreLocInfo info)
        {
            ///代码或名称不允许重复
            int cnt = dal.GetCounts("and ([STORE_LOC_CODE] = '" + info.StoreLocCode + "' or [STORE_LOC_NAME] = '" + info.StoreLocName + "') and [VALID_FLAG] = 1");
            if (cnt > 0)
                throw new Exception("Err:代码或名称不允许重复");
        }
    }
}


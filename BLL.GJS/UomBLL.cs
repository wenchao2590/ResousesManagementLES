using DAL.GJS;
using DM.GJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.GJS
{
    public class UomBLL
    {
        #region Common
        UomDAL dal = new UomDAL();
        public List<UomInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public UomInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(UomInfo info)
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

        private void ValidInfo(UomInfo info)
        {
            ///代码或名称不允许重复
            int cnt = dal.GetCounts("and ([CODE] = '" + info.Code + "' or [NAME] = '" + info.Name + "') and [VALID_FLAG] = 1");
            if (cnt > 0)
                throw new Exception("Err:代码或名称不允许重复");
        }
    }
}


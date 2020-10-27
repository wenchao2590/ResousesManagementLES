using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class UserLoginBLL
    {
        #region Common
        UserLoginDAL dal = new UserLoginDAL();
        public List<UserLoginInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public UserLoginInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }

        public bool DeleteInfo(int id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        #endregion
    }
}


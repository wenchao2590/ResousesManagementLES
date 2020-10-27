using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public partial class UserMobileBLL
    {
        #region 
        /// <summary>
        /// UserRoleRangeAuthDAL
        /// </summary>
        UserMobileDAL dal = new UserMobileDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<UserMobileInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(UserMobileInfo info)
        {
            int cnt = dal.GetCounts("[UUID] = N'" + info.Uuid + "' and [USER_FID] = N'" + info.UserFid.GetValueOrDefault() + "'");
            if (cnt > 0)
                throw new Exception("MC:3x00000026");///设备已注册
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        public UserMobileInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="whereText"></param>
        /// <returns></returns>
        public int GetCounts(string whereText)
        {
            return dal.GetCounts(whereText);
        }
        #endregion
    }
}

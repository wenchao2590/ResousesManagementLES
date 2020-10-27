using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// UserRoleRangeAuthBLL
    /// </summary>
    public class UserRoleRangeAuthBLL
    {
        #region 
        /// <summary>
        /// UserRoleRangeAuthDAL
        /// </summary>
        UserRoleRangeAuthDAL dal = new UserRoleRangeAuthDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<UserRoleRangeAuthInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(UserRoleRangeAuthInfo info)
        {
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
        public UserRoleRangeAuthInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// GetUserRangeAuths
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="rangeAuthConditionInfos"></param>
        /// <returns></returns>
        public List<UserRoleRangeAuthInfo> GetUserRangeAuths(Guid userFid, Guid roleFid, out List<RangeAuthConditionInfo> rangeAuthConditionInfos)
        {
            rangeAuthConditionInfos = new RangeAuthConditionDAL().GetList(string.Empty, string.Empty);
            return dal.GetList("[USER_FID] = N'" + userFid + "' and [ROLE_FID] = N'" + roleFid + "'", string.Empty);
        }
    }
}

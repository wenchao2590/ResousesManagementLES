using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace BLL.SYS
{
    public class UserRoleBLL
    {
        /// <summary>
        /// 
        /// </summary>
        UserRoleDAL dal = new UserRoleDAL();
        #region Common
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<UserRoleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            ///用户角色关系数据
            List<UserRoleInfo> list = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            ///角色用户例外条件数据
            List<UserRoleRangeAuthInfo> userRoleRangeAuthInfos = new UserRoleRangeAuthDAL().GetList(textWhere, string.Empty);
            ///例外条件基础数据
            List<RangeAuthConditionInfo> rangeAuthConditionInfos = new RangeAuthConditionDAL().GetList(string.Empty, string.Empty);
            foreach (UserRoleInfo info in list)
            {
                foreach (RangeAuthConditionInfo rangeAuthConditionInfo in rangeAuthConditionInfos)
                {
                    UserRoleRangeAuthInfo userRoleRangeAuthInfo
                        = userRoleRangeAuthInfos.FirstOrDefault(d => d.UserFid.GetValueOrDefault() == info.Fid.GetValueOrDefault());
                    if (userRoleRangeAuthInfo == null) continue;
                    PropertyInfo propertyInfo = info.GetType().GetProperty(rangeAuthConditionInfo.AttributeName);
                    if (propertyInfo == null) continue;
                    propertyInfo.SetValue(info, userRoleRangeAuthInfo.ConditionContext, null);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            UserRoleInfo userRoleInfo = dal.GetInfo(id);
            if (userRoleInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            string sql = "update [dbo].[TS_SYS_USER_ROLE] " +
                "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] = " + id + ";" +
                "update [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] " +
                "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [USER_FID] = N'" + userRoleInfo.UserFid.GetValueOrDefault() + "' and [ROLE_FID] = N'" + userRoleInfo.RoleFid.GetValueOrDefault() + "';";
            using (var trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserRoleInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion



        /// <summary>
        /// 判断是否存在同一组织结构上的授权信息
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="organizationFid"></param>
        /// <returns></returns>
        public bool IsExistUserRole(Guid userFid, Guid roleFid, Guid organizationFid)
        {
            int dataCnt = dal.GetCounts("" +
                "[USER_FID] = N'" + userFid + "' and " +
                "[ROLE_FID] = N'" + roleFid + "' and " +
                "[ORGANIZATION_FID] = N'" + organizationFid + "'");
            if (dataCnt > 0) return true;
            ///根据当前组织外键获取父级组织外键
            Guid parentOrganizationFid = new OrganizationDAL().GetParentFid(organizationFid);
            if (parentOrganizationFid == Guid.Empty) return false;
            return IsExistUserRole(userFid, roleFid, parentOrganizationFid);
        }

        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="organizationFid"></param>
        /// <param name="loginUser"></param>
        /// <param name="rangeAuthConditionInfos"></param>
        /// <returns></returns>
        public bool AddUserRole(long userId, UserRoleInfo userRoleInfo, string loginUser)
        {
            ///USER_FID
            userRoleInfo.UserFid = new UserDAL().GetFid(userId);
            ///参数有效性校验
            if (userRoleInfo.UserFid.GetValueOrDefault() == Guid.Empty)
                throw new Exception("MC:1x00000030");///参数获取错误
            if (userRoleInfo.RoleFid.GetValueOrDefault() == Guid.Empty)
                throw new Exception("MC:1x00000030");///参数获取错误
            if (userRoleInfo.OrganizationFid.GetValueOrDefault() == Guid.Empty)
                throw new Exception("MC:1x00000030");///参数获取错误

            if (IsExistUserRole(
                userRoleInfo.UserFid.GetValueOrDefault(),
                userRoleInfo.RoleFid.GetValueOrDefault(),
                userRoleInfo.OrganizationFid.GetValueOrDefault()))
                throw new Exception("MC:0x00000264");///已存在相同组织结构上的授权

            Guid userRoleFid = Guid.NewGuid();
            string sql = "insert into [dbo].[TS_SYS_USER_ROLE] " +
                "(FID, USER_FID, ROLE_FID, ORGANIZATION_FID, COMMENTS, " +
                "VALID_FLAG, CREATE_USER, CREATE_DATE) values " +
                "(" +
                "N'" + userRoleFid + "', " +///FID
                "N'" + userRoleInfo.UserFid.GetValueOrDefault() + "', " +///USER_FID
                "N'" + userRoleInfo.RoleFid.GetValueOrDefault() + "', " +///ROLE_FID
                "N'" + userRoleInfo.OrganizationFid.GetValueOrDefault() + "', " +///ORGANIZATION_FID                
                "1, N'" + loginUser + "', GETDATE());";///VALID_FLAG, CREATE_USER, CREATE_DATE

            using (var trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// GetOrganizationFid
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <returns></returns>
        public Guid GetOrganizationFid(Guid userFid, Guid roleFid)
        {
            return dal.GetOrganizationFid(userFid, roleFid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <returns></returns>
        public List<Guid> GetUserRoleOrganizationFids(Guid userFid, Guid roleFid)
        {
            return dal.GetUserRoleOrganizationFids(userFid, roleFid);
        }
    }
}

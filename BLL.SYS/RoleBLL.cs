using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.SYS
{
    public class RoleBLL
    {
        #region Common
        RoleDAL dal = new RoleDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<RoleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<RoleInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(RoleInfo info)
        {
            ///角色名①不能重复，必填项
            int cnt = dal.GetCounts("[ROLE_NAME] = N'" + info.RoleName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000267");///角色名称重复

            ///TODO:角色类型②为20.超级系统管理的权限默认为全选
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            ///TODO:角色类型②为20.超级系统管理的权限默认为全选
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyDate"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            ///未被用户继承的角色可以进行删除，即TS_SYS_USER_ROLE无对应数据
            int cnt = new UserRoleDAL().GetCounts("[ROLE_FID] in (select [FID] from dbo.[TS_SYS_ROLE] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000268");
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetDataSource()
        {
            return dal.GetDataSource();
        }
        /// <summary>
        /// 获取用户对应的角色列表
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetUserRoles(Guid userFid)
        {
            return dal.GetRolesByUserFid(userFid);
        }

        public List<Guid> GetAuthSourceFidList(Guid roleFid, int authType)
        {
            return new List<Guid>();
        }
        /// <summary>
        /// 获取所有需要授权的项目，其中IS_AUTH=TRUE表示已经授权
        /// </summary>
        /// <param name="roleFid"></param>
        /// <returns></returns>
        public List<RoleAuthInfo> GetRoleAuthList(Guid roleFid)
        {
            ///需要授权的菜单
            List<MenuInfo> menus = new MenuDAL().GetList("and [VALID_FLAG] <> 0", string.Empty);
            ///需要授权的功能项
            List<MenuActionInfo> menuactions = new MenuActionDAL().GetList("and [VALID_FLAG] <> 0", string.Empty);
            ///功能项
            List<ActionInfo> actions = new ActionDAL().GetList("and [VALID_FLAG] <> 0", string.Empty);
            ///需要授权的报表
            List<ReportInfo> reports = new ReportDAL().GetList("and [VALID_FLAG] <> 0", string.Empty);
            ///需要授权的图表
            List<ChartInfo> charts = new ChartDAL().GetList("and [VALID_FLAG] <> 0", string.Empty);
            ///角色对应已授权的项目
            List<RoleAuthInfo> roleauths = new RoleAuthDAL().GetList("and [ROLE_FID] = '" + roleFid + "' and [IS_AUTH] <> 0 and [VALID_FLAG] <> 0", string.Empty);

            List<RoleAuthInfo> list = new List<RoleAuthInfo>();
            ///菜单
            foreach (var item in menus)
            {
                RoleAuthInfo info = new RoleAuthInfo();
                info.AuthSourceFid = item.Fid;
                info.AuthSourceName = item.MenuName + "|" + item.MenuNameCn;
                info.AuthType = 1;
                info.AuthTypeName = "菜单";
                info.DisplayOrder = item.DisplayOrder.GetValueOrDefault();
                if (item.NeedAuth.GetValueOrDefault())
                {
                    var roleauth = roleauths.FirstOrDefault(d => d.AuthSourceFid == item.Fid && d.AuthType == 1);
                    info.IsAuth = roleauth == null ? false : true;
                }
                else
                    info.IsAuth = true;
                info.ParentSourceFid = item.ParentMenuFid.GetValueOrDefault();
                list.Add(info);
            }
            ///功能
            foreach (var item in menuactions)
            {
                RoleAuthInfo info = new RoleAuthInfo();
                info.AuthSourceFid = item.Fid;
                var action = actions.FirstOrDefault(d => d.Fid == item.ActionFid);
                if (action == null) continue;
                info.AuthSourceName = action.ActionName + "|" + action.ActionNameCn;
                info.AuthType = 2;
                info.AuthTypeName = "功能";
                info.DisplayOrder = item.ActionOrder.GetValueOrDefault();
                if (item.NeedAuth.GetValueOrDefault())
                {
                    var roleauth = roleauths.FirstOrDefault(d => d.AuthSourceFid == item.Fid && d.AuthType == 2);
                    info.IsAuth = roleauth == null ? false : true;
                }
                else
                    info.IsAuth = true;
                info.ParentSourceFid = item.MenuFid.GetValueOrDefault();
                var menuinfo = list.FirstOrDefault(d => d.AuthType == 1 && d.AuthSourceFid == item.MenuFid);
                if (menuinfo == null) continue;
                list.Add(info);
            }
            RoleAuthInfo reportauth = new RoleAuthInfo();
            reportauth.AuthSourceFid = Guid.Parse("2238F7AD-9196-4B53-A0D0-81460FDA1F4C");
            reportauth.AuthSourceName = "REPORT|报表";
            reportauth.AuthType = 1;
            reportauth.AuthTypeName = "菜单";
            reportauth.IsAuth = true;
            reportauth.DisplayOrder = int.MaxValue - 1;
            reportauth.ParentSourceFid = Guid.Empty;
            list.Add(reportauth);
            ///报表
            foreach (var item in reports)
            {
                RoleAuthInfo info = new RoleAuthInfo();
                info.AuthSourceFid = item.Fid;
                info.AuthSourceName = item.NameEn + "|" + item.Name;
                info.AuthType = 3;
                info.AuthTypeName = "报表";
                if (item.IsAuth.GetValueOrDefault())
                {
                    var roleauth = roleauths.FirstOrDefault(d => d.AuthSourceFid == item.Fid && d.AuthType == 3);
                    info.IsAuth = roleauth == null ? false : true;
                }
                else
                    info.IsAuth = true;
                info.ParentSourceFid = reportauth.AuthSourceFid.GetValueOrDefault();
                list.Add(info);
            }
            RoleAuthInfo chartauth = new RoleAuthInfo();
            chartauth.AuthSourceFid = Guid.Parse("6E714DD6-2D41-45AE-88C2-433EEF1973E9");
            chartauth.AuthSourceName = "CHART|图表";
            chartauth.AuthType = 1;
            chartauth.AuthTypeName = "菜单";
            chartauth.IsAuth = true;
            chartauth.DisplayOrder = int.MaxValue;
            chartauth.ParentSourceFid = Guid.Empty;
            list.Add(chartauth);
            ///图表
            foreach (var item in charts)
            {
                RoleAuthInfo info = new RoleAuthInfo();
                info.AuthSourceFid = item.Fid;
                info.AuthSourceName = item.NameEn + "|" + item.Name;
                info.AuthType = 4;
                info.AuthTypeName = "图表";
                if (item.IsAuth.GetValueOrDefault())
                {
                    var roleauth = roleauths.FirstOrDefault(d => d.AuthSourceFid == item.Fid && d.AuthType == 4);
                    info.IsAuth = roleauth == null ? false : true;
                }
                else
                    info.IsAuth = true;
                info.ParentSourceFid = chartauth.AuthSourceFid.GetValueOrDefault();
                list.Add(info);
            }
            return list.OrderBy(d => d.DisplayOrder).ToList();
        }
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="roleFid"></param>
        /// <param name="roleauthlist"></param>
        /// <returns></returns>
        public bool SetRoleAuth(Guid roleFid, List<RoleAuthInfo> roleauthlist)
        {
            ///授权项
            List<RoleAuthInfo> roleauths = roleauthlist.Where(d => d.IsAuth.GetValueOrDefault()).ToList();
            ///清除授权项
            List<RoleAuthInfo> roleunauths = roleauthlist.Where(d => !d.IsAuth.GetValueOrDefault()).ToList();
            ///当前角色对应的授权项信息
            List<RoleAuthInfo> roleauthnows = new RoleAuthDAL().GetList("and [VALID_FLAG] <> 0 "
                + "and [ROLE_FID] = '" + roleFid + "'", string.Empty);
            ///INSERT LIST
            List<RoleAuthInfo> insertlist = new List<RoleAuthInfo>();
            List<RoleAuthInfo> updatelist = new List<RoleAuthInfo>();
            List<RoleAuthInfo> updateReverselist = new List<RoleAuthInfo>();
            ///比对现有库中该角色的权限，提取需要插入和反转的权限
            foreach (var roleauth in roleauths)
            {
                var roleauthnow = roleauthnows.FirstOrDefault(d => d.AuthType == roleauth.AuthType
                && d.AuthSourceFid == roleauth.AuthSourceFid);
                if (roleauthnow == null)
                    insertlist.Add(roleauth);
                else
                {
                    if (!roleauthnow.IsAuth.GetValueOrDefault())
                        updatelist.Add(roleauthnow);
                }
            }
            ///比对现有库中该角色的权限，提取需要反转的权限
            foreach (var roleunauth in roleunauths)
            {
                var roleauthnow = roleauthnows.FirstOrDefault(d => d.AuthType == roleunauth.AuthType
                && d.AuthSourceFid == roleunauth.AuthSourceFid
                && d.IsAuth.GetValueOrDefault());
                if (roleauthnow != null)
                    updateReverselist.Add(roleauthnow);
            }
            using (var trans = new TransactionScope())
            {
                if (!new RoleAuthDAL().InsertList(insertlist, roleFid, ""))
                    throw new Exception("Role Auth Error");
                if (!new RoleAuthDAL().ReverseAuthList(updatelist))
                    throw new Exception("Role Auth Error");
                if (!new RoleAuthDAL().ReverseIsAuthList(updateReverselist))
                    throw new Exception("Role Auth Error");
                trans.Complete();
            }
            return true;
        }

        public bool SetRoleAuth(Guid roleFid, List<Guid> authSourceFids, bool setFlag, string modifyUser)
        {
            List<RoleAuthInfo> list = new RoleAuthDAL().GetList("and [VALID_FLAG] <> 0 "
                + "and [ROLE_FID] = '" + roleFid + "' "
                + "and [AUTH_SOURCE_FID] in ('" + string.Join("','", authSourceFids.ToArray()) + "')", string.Empty);
            string sql = string.Empty;
            foreach (var authSourceFid in authSourceFids)
            {
                var info = list.FirstOrDefault(d => d.AuthSourceFid == authSourceFid);
                if (info == null)
                {
                    if (!setFlag) continue;
                    int authType = GetAuthType(authSourceFid);
                    if (authType == 0) continue;
                    sql += "insert into dbo.[TS_SYS_ROLE_AUTH] "
                        + "([FID],[ROLE_FID],[AUTH_TYPE],[IS_AUTH],[AUTH_SOURCE_FID],[VALID_FLAG],[CREATE_USER],[CREATE_DATE]) "
                        + "values (NEWID(),'" + roleFid + "'," + authType + ",1,'" + authSourceFid + "',1,'" + modifyUser + "',GETDATE());";
                    continue;
                }
                if (info.IsAuth.GetValueOrDefault() == setFlag) continue;
                sql += "update dbo.[TS_SYS_ROLE_AUTH] "
                    + "set [IS_AUTH] = " + (setFlag ? 1 : 0) + ",[MODIFY_USER] = '" + modifyUser + "',[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + info.Id + ";";
            }
            if (string.IsNullOrEmpty(sql)) return true;
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        private int GetAuthType(Guid authSourceFid)
        {
            int dataCnt = new MenuDAL().GetCounts("and [VALID_FLAG] <> 0 and [NEED_AUTH] = 1 and [FID] = '" + authSourceFid + "'");
            if (dataCnt > 0) return 1;
            dataCnt = new MenuActionDAL().GetCounts("and [VALID_FLAG] <> 0 and [NEED_AUTH] = 1 and [FID] = '" + authSourceFid + "'");
            if (dataCnt > 0) return 2;
            dataCnt = new ReportDAL().GetCounts("and [VALID_FLAG] <> 0 and [IS_AUTH] = 1 and [FID] = '" + authSourceFid + "'");
            if (dataCnt > 0) return 3;
            dataCnt = new ChartDAL().GetCounts("and [VALID_FLAG] <> 0 and [IS_AUTH] = 1 and [FID] = '" + authSourceFid + "'");
            if (dataCnt > 0) return 4;
            return 0;
        }
        /// <summary>
        /// 根据USER_FID获取角色选项
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetRolesByUserFid(Guid userFid)
        {
            return dal.GetRolesByUserFid(userFid);
        }

        /// <summary>
        /// 根据USER_FID、PLANT_FID获取角色选项
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetRolesByUserFid(Guid userFid, Guid plantFid)
        {
            return dal.GetRolesByUserFid(userFid, plantFid);
        }

        public List<RoleInfo> GetRolesByUser(Guid userFid)
        {
            string sql = string.Format("select distinct a.* from [dbo].[TS_SYS_ROLE] a " +
                " INNER JOIN[dbo].[TS_SYS_USER_role] b ON a.FID = b.ROLE_FID" +
                " where b.USER_FID = '{0}' and b.valid_flag <> 0", userFid);
            return dal.GetList(sql);


        }

        public UserRoleInfo GetUserRoleInfo(Guid userRoleFid)
        {
            return new UserRoleDAL().GetUserRoleInfo(userRoleFid);
        }

        public List<UserRoleInfo> GetUserRoleList(Guid userRoleFid, Guid roleFid)
        {
            UserRoleDAL userRoleDal = new UserRoleDAL();
            List<UserRoleInfo> userRoleList = userRoleDal.GetUserRoleList(userRoleFid, roleFid);
            var userFid = userRoleList.FirstOrDefault().UserFid;
            //List<RoleUserConditionInfo> roleConditionList = new RoleUserConditionDAL().GetList("and [USER_FID] = '" + userFid + "' and [ROLE_FID] = '" + roleFid + "' and [VALID_FLAG] <> 0 ", string.Empty);
            //List<UserRoleConditionInfo> userroleConditionList = new UserRoleConditionDAL().GetList("and [VALID_FLAG] <> 0", "[EXTEND_FIELD_SEQ]");
            //foreach (var info in userRoleList)
            //{
            //    for (int i = 0; i < userroleConditionList.Count; i++)
            //    {
            //        var roleuserconditioninfo = roleConditionList.FirstOrDefault(d => d.ExtendFieldSeq == (i + 1));
            //        if (roleuserconditioninfo == null) continue;
            //        switch (i)
            //        {
            //            case 0: info.ExtendField1 = roleuserconditioninfo.ConditionContext; break;
            //            case 1: info.ExtendField2 = roleuserconditioninfo.ConditionContext; break;
            //            case 2: info.ExtendField3 = roleuserconditioninfo.ConditionContext; break;
            //            case 3: info.ExtendField4 = roleuserconditioninfo.ConditionContext; break;
            //            case 4: info.ExtendField5 = roleuserconditioninfo.ConditionContext; break;
            //            case 5: info.ExtendField6 = roleuserconditioninfo.ConditionContext; break;
            //            case 6: info.ExtendField7 = roleuserconditioninfo.ConditionContext; break;
            //            case 7: info.ExtendField8 = roleuserconditioninfo.ConditionContext; break;
            //            case 8: info.ExtendField9 = roleuserconditioninfo.ConditionContext; break;
            //            case 9: info.ExtendField10 = roleuserconditioninfo.ConditionContext; break;
            //            case 10: info.ExtendField11 = roleuserconditioninfo.ConditionContext; break;
            //            case 11: info.ExtendField12 = roleuserconditioninfo.ConditionContext; break;
            //            case 12: info.ExtendField13 = roleuserconditioninfo.ConditionContext; break;
            //            case 13: info.ExtendField14 = roleuserconditioninfo.ConditionContext; break;
            //            case 14: info.ExtendField15 = roleuserconditioninfo.ConditionContext; break;
            //            case 15: info.ExtendField16 = roleuserconditioninfo.ConditionContext; break;
            //            case 16: info.ExtendField17 = roleuserconditioninfo.ConditionContext; break;
            //            case 17: info.ExtendField18 = roleuserconditioninfo.ConditionContext; break;
            //            case 18: info.ExtendField19 = roleuserconditioninfo.ConditionContext; break;
            //            case 19: info.ExtendField20 = roleuserconditioninfo.ConditionContext; break;
            //        }
            //    }
            //}

            return userRoleList;
        }

        //public List<MobileRoleInfo> GetMobleRoles(string userName, string passWord, string plantCode)
        //{     
        //    return dal.GetList(sql);
        //}

        
    }
}

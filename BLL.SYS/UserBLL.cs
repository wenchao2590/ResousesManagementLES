namespace BLL.SYS
{
    using DAL.SYS;
    using DM.SYS;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Transactions;
    /// <summary>
    /// UserBLL
    /// </summary>
    public class UserBLL
    {
        /// <summary>
        /// 系统配置-默认密码为空时的密码
        /// </summary>
        private string emptyDefaultPassword = "Abc@123";

        #region Common
        /// <summary>
        /// UserDAL
        /// </summary>
        UserDAL dal = new UserDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<UserInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(UserInfo info)
        {
            ///系统默认密码
            string defaultPassword = new ConfigBLL().GetValueByCode("USER_DEFAULT_PASSWORD");
            if (string.IsNullOrEmpty(defaultPassword))
                defaultPassword = emptyDefaultPassword;
            info.Password = SignatureHelper.GetSignature(defaultPassword);
            ///默认状态为未启用
            info.UserStatus = (int)UserStatusConstants.Disable;

            int cnt = dal.GetCounts("[LOGIN_NAME] = N'" + info.LoginName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000202");///用户名重复

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
            string loginName = CommonBLL.GetFieldValue(fields, "LOGIN_NAME");
            int cnt = dal.GetCounts("[LOGIN_NAME] = N'" + loginName + "' and [ID] <> " + id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000202");///用户名重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id + " and [USER_STATUS] = " + (int)UserStatusConstants.Disable + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000174");///只有未启用状态下的用户方可删除
            Guid userFid = dal.GetFid(id);
            if (userFid == Guid.Empty)
                throw new Exception("MC:0x00000084");///数据错误

            string sql = "update [dbo].[TS_SYS_USER] " +
                "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] = " + id + ";" +
                "update [dbo].[TS_SYS_USER_ROLE] " +
                "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [USER_FID] = N'" + userFid + "';" +
                "update [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] " +
                "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [USER_FID] = N'" + userFid + "';";

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
        public UserInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 用户启用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id + " and [USER_STATUS] in (" + (int)UserStatusConstants.Disable + "," + (int)UserStatusConstants.Locked + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000265");///未启用或已锁定状态的用户可以启用
            Guid userFid = dal.GetFid(id);
            if (userFid == Guid.Empty)
                throw new Exception("MC:0x00000084");///数据错误

            cnt = new UserRoleDAL().GetCounts("[USER_FID] = N'" + userFid + "'");
            if (cnt == 0)
                throw new Exception("MC:0x00000266");///用户启用前需要先配置角色

            return dal.UpdateInfo("[USER_STATUS] = " + (int)UserStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }
        /// <summary>
        /// 用户停用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool DisableInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id + " and [USER_STATUS] = " + (int)UserStatusConstants.Enable + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000269");///已启用状态的用户才能停用

            return dal.UpdateInfo("[USER_STATUS] = " + (int)UserStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }

        /// <summary>
        /// 登录，获取对象
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public UserInfo Login(string userName, string passWord)
        {
            UserInfo info = dal.Login(userName, SignatureHelper.GetSignature(passWord));///PASSWORD加密
            if (info == null)
                throw new Exception("MC:0x00000129");///登录失败
            return info;
        }

        private List<Guid> GetUserRoleFids(long userId)
        {
            Guid userFid = dal.GetFid(userId);
            if (userFid == Guid.Empty)
                return new List<Guid>();
            List<Guid> roleFids = new UserRoleDAL().GetFids(userFid);
            return roleFids;
        }
        /// <summary>
        /// AUTH_TYPE:1.Menu,2.Action,3.Report
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        public List<Guid> GetAuthSourceFids(long userId, int authType, List<Guid> sourceFids)
        {
            List<Guid> userRoleFids = GetUserRoleFids(userId);
            string roleFids = string.Empty;
            foreach (var userRoleFid in userRoleFids)
            {
                roleFids += ",'" + userRoleFid + "'";
            }
            if (string.IsNullOrEmpty(roleFids))
                return new List<Guid>();
            string sourceFidsCondition = string.Empty;
            foreach (var sourceFid in sourceFids)
            {
                sourceFidsCondition += ",'" + sourceFid + "'";
            }
            if (!string.IsNullOrEmpty(sourceFidsCondition))
                sourceFidsCondition = sourceFidsCondition.Substring(1);
            return new RoleAuthDAL().GetAuthSourceFidsByRolesAuthTypeInSourceFids(roleFids.Substring(1), authType, sourceFidsCondition);
        }
        /// <summary>
        /// 根据用户FID获取已授权的ROLE集合
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<UserRoleInfo> GetRolesByUser(Guid userFid, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = new UserRoleDAL().GetCounts("and [USER_FID] = '" + userFid + "' and [VALID_FLAG] = 1");
            ///用户角色关系数据
            List<UserRoleInfo> list = new UserRoleDAL().GetListByPage("and [USER_FID] = '" + userFid + "' and [VALID_FLAG] = 1", textOrder, pageIndex, pageRow);
            ///角色用户例外条件数据
            List<UserRoleRangeAuthInfo> roleuserconditionlist = new UserRoleRangeAuthDAL().GetList("and [USER_FID] = '" + userFid + "' and [VALID_FLAG] = 1 ", string.Empty);
            ///例外条件基础数据
            List<RangeAuthConditionInfo> userroleconditionlist = new RangeAuthConditionDAL().GetList("and [VALID_FLAG] = 1", string.Empty);
            foreach (var info in list)
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (i > userroleconditionlist.Count) break;
                    List<UserRoleRangeAuthInfo> roleuserconditions
                        = roleuserconditionlist.Where(d => d.UserFid.GetValueOrDefault() == info.Fid.GetValueOrDefault()
                    && d.ConditionFid.GetValueOrDefault() == userroleconditionlist[i - 1].Fid.GetValueOrDefault()).ToList();
                    if (roleuserconditions.Count == 0) continue;
                    info.GetType().GetProperty("ExtendField" + i).SetValue(info, string.Join(",", roleuserconditions.Select(d => d.ConditionContext).ToArray()), null);
                }
            }
            return list;
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public bool RemoveRole(long id, string userLoginName)
        {
            return new UserRoleDAL().LogicDelete(id, userLoginName) > 0 ? true : false;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ResetPassword(string userId, string defaultPassword)
        {
            string passWord = defaultPassword == string.Empty ? emptyDefaultPassword : defaultPassword;
            return dal.ResetPassword(userId, SignatureHelper.GetSignature(passWord));
        }

        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<UserInfo> userExcelInfos = CommonDAL.DatatableConvertToList<UserInfo>(dataTable).ToList();
            if (userExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<UserInfo> userInfos = new UserDAL().GetList("[LOGIN_NAME] in ('" + string.Join("','", userExcelInfos.Select(d => d.LoginName).ToArray()) + "')", string.Empty);
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var userExcelInfo in userExcelInfos)
            {
                if (userExcelInfo.LoginName.ToLower() == "admin") continue;
                ///
                UserInfo userInfo = userInfos.FirstOrDefault(d => d.LoginName == userExcelInfo.LoginName);
                if (userInfo == null)
                {
                    ///物料号①、物料中文名称②为必填项
                    if (string.IsNullOrEmpty(userExcelInfo.LoginName) || string.IsNullOrEmpty(userExcelInfo.EmployeeName))
                        throw new Exception("MC:0x00000238");///用户名与用户姓名是必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql(userExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///默认密码
                    string defaultPassword = new ConfigBLL().GetValueByCode("USER_DEFAULT_PASSWORD");
                    if (string.IsNullOrEmpty(defaultPassword))
                        defaultPassword = emptyDefaultPassword;

                    sql += "if not exists (select * from dbo.TS_SYS_USER with(nolock) where [LOGIN_NAME] = N'" + userExcelInfo.LoginName + "' and [VALID_FLAG] = 1) "
                        + "insert into dbo.[TS_SYS_USER] ("
                        + "[FID],"
                        + insertFieldString
                        + "[PASSWORD],"
                        + "[USER_STATUS],"
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + insertValueString
                        + "N'" + SignatureHelper.GetSignature(defaultPassword) + "',"///PASSWORD
                        + "" + (int)UserStatusConstants.Disable + ","///USER_STATUS
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");";
                    continue;
                }
                ///物料中文名称②为必填项
                if (string.IsNullOrEmpty(userExcelInfo.EmployeeName))
                    throw new Exception("MC:0x00000238");///用户名与用户姓名是必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql(userExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update dbo.[TS_SYS_USER] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + userInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public Guid GetFid(string loginUser)
        {
            return dal.GetFid(loginUser);
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool VerifyInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<UserMobileInfo> userMobileInfos = new UserMobileDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (userMobileInfos.Count == 0)
                throw new Exception("0x00000084");///数据错误
            foreach (var userMobileInfo in userMobileInfos)
            {
                if (userMobileInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Enable)
                    throw new Exception("0x00000084");///TODO:该设备已启用
            }
            string sql = "update [dbo].[TS_SYS_USER_MOBILE] " +
                "set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<UserMobileInfo> userMobileInfos = new UserMobileDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (userMobileInfos.Count == 0)
                throw new Exception("0x00000084");///数据错误
            foreach (var userMobileInfo in userMobileInfos)
            {
                if (userMobileInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Disabled)
                    throw new Exception("0x00000084");///TODO:该设备已停用
            }
            string sql = "update [dbo].[TS_SYS_USER_MOBILE] " +
                "set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

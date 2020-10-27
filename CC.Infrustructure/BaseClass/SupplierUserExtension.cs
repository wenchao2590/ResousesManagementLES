using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Infrustructure.BaseClass
{
    public partial class SupplierUser
    {
        #region SQL语句定义
        private const string    GET_USER_BY_SUPPLIERNUM_AND_LOGINNAME =
                                "SELECT top 1  " +
                                "a.USER_ID," +
                                "USER_LOGIN_NAME," +
                                "USER_PASSWORD," +
                                "EMPLOYEE_NAME," +
                                "PASSWORD_EXPIRE_TIME," +
                                "USER_STATUS," +
                                "FAIL_LOGIN," +
                                "c.SUPPLIER_NUM," +
                                "c.SUPPLIER_NAME," +
                                "c.SUPPLIER_TYPE," +
                                "c.SUPPLIER_ADDRESS" +
                                "  from MES.TS_SYS_USER a, MES.TR_SYS_USER_SUPPLIER b, MES.TM_BAS_SUPPLIER c " +
                                "  where USER_LOGIN_NAME = @UserLoginName and USER_TYPE = 2 and a.USER_ID = b.USER_ID and b.SUPPLIER_NUM = c.SUPPLIER_NUM ";

        private const string UPDATE_USER_PASSWORD_BY_SUPPLIERNUM_AND_LOGINNAME =
                                "UPDATE MES.TS_SYS_USER " +
                                "set PASSWORD3 = @OldUserPassword, PASSWORD1 = PASSWORD2,PASSWORD2 = PASSWORD3, USER_PASSWORD= @NewUserPassword, UPDATE_DATE = GETDATE(),UPDATE_USER = @UserLoginName " +
                                "from MES.TS_SYS_USER a, MES.TR_SYS_USER_SUPPLIER b, MES.TM_BAS_SUPPLIER c " +
                                "where USER_LOGIN_NAME = @UserLoginName and USER_TYPE = 2 and b.SUPPLIER_NUM = @SupplierNum " +
                                        "and a.USER_ID = b.USER_ID and b.SUPPLIER_NUM = c.SUPPLIER_NUM ";

        private const string GET_USER_BY_SUPPLIERNUM =
                                "SELECT top 1  " +
                                "a.USER_ID," +
                                "USER_LOGIN_NAME," +
                                "USER_PASSWORD," +
                                "EMPLOYEE_NAME," +
                                "PASSWORD_EXPIRE_TIME," +
                                "USER_STATUS," +
                                "FAIL_LOGIN," +
                                "c.SUPPLIER_NUM," +
                                "c.SUPPLIER_NAME," +
                                "c.SUPPLIER_TYPE," +
                                "c.SUPPLIER_ADDRESS" +
                        "  from MES.TS_SYS_USER a, MES.TR_SYS_USER_SUPPLIER b, MES.TM_BAS_SUPPLIER c " +
                        "  where USER_TYPE = 2 and a.USER_ID = b.USER_ID and b.SUPPLIER_NUM = @SupplierNum and b.SUPPLIER_NUM = c.SUPPLIER_NUM ";


        #endregion

        #region 供应商用户相关方法

        /// <summary>
        /// 供应商用户登录
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static SupplierUser Sign(string userName, string password, out string message)
        {            
            SupplierUser user = GetUserByLoginName(userName);

            //供应商用户不存在包括: 1)用户不存在 2)用户类型表明非供应商用户 3)该供应商下无该用户
            if (user == null)
            {
                message = "该供应商用户不存在！";
                return null;
            }
            else if (user.UserStatus != 1)
            {
                message = "该用户已被锁定！";
                return null;
            }
            else if (user.Password != password)
            {
                if (++user.FailLogin == 3)
                    user.UserStatus = 2;

                message = "密码不正确！";
                return null;
            }
            else
            {
                message = "";
                if (user.FailLogin > 0)
                {
                    user.FailLogin = 0;
                }
                return user;
            }
        }

        /// <summary>
        /// 根据用户登录名获取关联信息
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        private static SupplierUser GetUserByLoginName(string userLoginName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(GET_USER_BY_SUPPLIERNUM_AND_LOGINNAME);

            db.AddInParameter(dbCommand, "@UserLoginName", DbType.String, userLoginName);


            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                {
                    return CreateSupplierUser(dr);
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 根据供应商编号获取供应商-用户关联信息
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public static SupplierUser GetUserBySupplierNum(string supplierNum)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(GET_USER_BY_SUPPLIERNUM);

            db.AddInParameter(dbCommand, "@SupplierNum", DbType.String, supplierNum);

            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                {
                    return CreateSupplierUser(dr);
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 更新供应商密码
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="supplierNum"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool UpdateSupplierPassword(string userLoginName,string supplierNum,string oldPassword,string newPassword)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(UPDATE_USER_PASSWORD_BY_SUPPLIERNUM_AND_LOGINNAME);
            db.AddInParameter(dbCommand, "@UserLoginName", DbType.String, userLoginName);
            db.AddInParameter(dbCommand, "@SupplierNum", DbType.String, supplierNum);
            db.AddInParameter(dbCommand, "@OldUserPassword", DbType.String, oldPassword);
            db.AddInParameter(dbCommand, "@NewUserPassword", DbType.String, newPassword);

            if(db.ExecuteNonQuery(dbCommand) > 0)
                return true;

            return false;
        }

        /// <summary>
        /// 创建供应商-用户关联实体
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private static SupplierUser CreateSupplierUser(IDataReader rdr)
        {
            SupplierUser info = new SupplierUser();

            
            info.UserID = rdr.GetInt32(0);
            info.LoginName = rdr.GetString(1);
            info.Password = rdr.GetString(2);
            info.EmployeeName = rdr.GetString(3);
            info.PasswordExpireTime = rdr.GetDateTime(4);
            info.UserStatus = rdr.GetInt32(5);
            info.FailLogin = rdr.GetInt32(6);
            info.SupplierNum = rdr.GetString(7);
            info.SupplierName = rdr.GetString(8);
            info.SupplierType = rdr.GetInt32(9);
            info.SupplierAddress = rdr.GetString(10);

            return info;
        }

        #endregion


        /// <summary>
        /// 更新供应商密码
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="supplierNum"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static string GetUserSupplyByLoginName(string loginName, string supply)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("select [MES].[Func_GetAssemblysByLoginName]('" + loginName + "','" + supply + "')");

                return db.ExecuteScalar(dbCommand).ToString();
            }
            catch (System.Exception)
            {
                return "";
            }
        }
    }
}

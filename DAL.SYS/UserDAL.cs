using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL.SYS
{
    public partial class UserDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Guid GetFid(long userId)
        {
            string sql = "select [FID] from dbo.[TS_SYS_USER] with(nolock) " +
                "where [ID] = @ID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, userId);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }
        /// <summary>
        /// 获取FID
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public Guid GetFid(string loginUser)
        {
            string sql = "select [FID] from dbo.[TS_SYS_USER] with(nolock) " +
                "where [LOGIN_NAME] = @LOGIN_NAME and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@LOGIN_NAME", DbType.AnsiString, loginUser);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }
        /// <summary>
        /// 根据用户密码获取用户FID
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public UserInfo Login(string userName, string passWord)
        {
            string sql = "select * from dbo.[TS_SYS_USER] with(nolock) " +
                "where [LOGIN_NAME] = @LOGIN_NAME and [PASSWORD] = @PASSWORD and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@LOGIN_NAME", DbType.AnsiString, userName);
            db.AddInParameter(cmd, "@PASSWORD", DbType.AnsiString, passWord);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateUserInfo(dr);
            }
            return null;
        }

        public bool ResetPassword(string userId, string passWord)
        {
            string pattern = @"^\d*$";
            bool isNum = Regex.IsMatch(userId, pattern);
            string isFid = isNum ? string.Empty : "F";

            string sql = "update dbo.[TS_SYS_USER] with(rowlock) "
                + "set [PASSWORD] = @PASSWORD "
                + "where [" + isFid + "ID] = @" + isFid + "ID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PASSWORD", DbType.AnsiString, passWord);
            DbType dsb = isNum ? DbType.Int64 : DbType.String;
            db.AddInParameter(cmd, "@" + isFid + "ID", dsb, userId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        public UserInfo GetUserInfo(Guid afid)
        {
            string sql = "select * from dbo.[TS_SYS_USER] with(nolock) "
                + "where [FID] = @FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, afid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateUserInfo(dr);
            }
            return null;
        }
    }
}

using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class UserTokenDAL
    {
        /// <summary>
        /// 判断超时
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timeOutMinute"></param>
        /// <returns></returns>
        public UserTokenInfo GetInfo(string token, int timeOutMinute)
        {
            string sql = "select * from dbo.[TT_SYS_USER_TOKEN] with(nolock) "
                + "where [VALID_FLAG] = 1 "
                + "and [TOKEN] = @TOKEN "
                + "and DATEDIFF(minute,[MODIFY_DATE],GETDATE())<=@TIME_OUT_MINUTE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TOKEN", DbType.AnsiString, token);
            db.AddInParameter(cmd, "@TIME_OUT_MINUTE", DbType.Int32, timeOutMinute);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateUserTokenInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据令牌获取数据对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserTokenInfo GetInfo(string token)
        {
            string sql = "select * from dbo.[TT_SYS_USER_TOKEN] with(nolock) " +
                "where [VALID_FLAG] = 1 and [TOKEN] = @TOKEN " +
                "order by [ID] desc;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TOKEN", DbType.AnsiString, token);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateUserTokenInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据用户获取令牌对象
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public UserTokenInfo GetInfo(Guid userFid)
        {
            string sql = "select * from dbo.[TT_SYS_USER_TOKEN] with(nolock) " +
                "where [VALID_FLAG] = 1 and [USER_FID] = @USER_FID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@USER_FID", DbType.Guid, userFid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateUserTokenInfo(dr);
            }
            return null;
        }
    }
}

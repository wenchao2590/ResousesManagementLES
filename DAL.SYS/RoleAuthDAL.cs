using DM.SYS;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class RoleAuthDAL
    {
        public List<Guid> GetAuthSourceFidsByRolesAuthTypeInSourceFids(string roleFids, int authType, string sourceFids)
        {
            string sql = string.Format("select distinct [AUTH_SOURCE_FID] from dbo.[TS_SYS_ROLE_AUTH] with(nolock) "
                + "where [AUTH_TYPE] = @AUTH_TYPE and [VALID_FLAG] <> 0 and [ROLE_FID] in ({0}) {1} ;"
                , roleFids
                , string.IsNullOrEmpty(sourceFids) ? string.Empty : " and [AUTH_SOURCE_FID] in (" + sourceFids + ")");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@AUTH_TYPE", DbType.Int32, authType);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("AUTH_SOURCE_FID")));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据角色获取指定类型的授权列表
        /// </summary>
        /// <param name="roleFid"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        public List<Guid> GetSourceFidsByRoleFid(Guid roleFid, int authType)
        {
            string sql = "select [AUTH_SOURCE_FID] from dbo.[TS_SYS_ROLE_AUTH] with(nolock) "
                + "where [AUTH_TYPE] = @AUTH_TYPE and [VALID_FLAG] = 1 and [IS_AUTH] = 1 and [ROLE_FID] = @ROLE_FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, roleFid);
            db.AddInParameter(dbCommand, "@AUTH_TYPE", DbType.Int32, authType);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("AUTH_SOURCE_FID")));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据用户获取指定类型的授权列表
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        public List<Guid> GetSourceFidsByUserFid(Guid userFid, int authType)
        {
            string sql = "select [AUTH_SOURCE_FID] from dbo.[TS_SYS_ROLE_AUTH] with(nolock) "
                + "where [AUTH_TYPE] = @AUTH_TYPE and [VALID_FLAG] = 1 and [IS_AUTH] = 1 "
                + "and [ROLE_FID] in (select [ROLE_FID] from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [USER_FID] = @USER_FID and [VALID_FLAG] = 1);";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(dbCommand, "@AUTH_TYPE", DbType.Int32, authType);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("AUTH_SOURCE_FID")));
                }
            }
            return list;
        }

        public List<Guid> GetAuthSourceFidsByRoleFidAuthTypeInSourceFids(Guid roleFid, int authType, string sourceFids)
        {
            string sql = string.Format("select [AUTH_SOURCE_FID] from dbo.[TS_SYS_ROLE_AUTH] with(nolock) "
                + "where [AUTH_TYPE] = @AUTH_TYPE and [VALID_FLAG] <> 0 and [ROLE_FID] = @ROLE_FID {0}  and [IS_AUTH] <> 0;"
                , string.IsNullOrEmpty(sourceFids) ? string.Empty : " and [AUTH_SOURCE_FID] in (" + sourceFids + ")");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, roleFid);
            db.AddInParameter(dbCommand, "@AUTH_TYPE", DbType.Int32, authType);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("AUTH_SOURCE_FID")));
                }
            }
            return list;
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <param name="roleFid"></param>
        /// <param name="createUser"></param>
        /// <returns></returns>
        public bool InsertList(List<RoleAuthInfo> list, Guid roleFid, string createUser)
        {
            if (list.Count == 0) return true;
            string sql = string.Empty;
            foreach (var info in list)
            {
                sql += "insert into dbo.[TS_SYS_ROLE_AUTH] "
                    + "([FID],[ROLE_FID],[AUTH_TYPE],[AUTH_SOURCE_FID],[IS_AUTH],[VALID_FLAG],[CREATE_USER],[CREATE_DATE]) "
                    + "values (NEWID(),'" + roleFid + "'," + info.AuthType + ",'" + info.AuthSourceFid + "',1,1,'" + createUser + "',GETDATE());";
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
        /// <summary>
        /// 逆转IS_AUTH
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ReverseAuthList(List<RoleAuthInfo> list)
        {
            if (list.Count == 0) return true;
            string ids = string.Empty;
            foreach (var info in list)
            {
                ids += "," + info.Id;
            }
            string sql = "update dbo.[TS_SYS_ROLE_AUTH] "
                + "set [IS_AUTH] = 1 "
                + "where [ID] in (" + ids.Substring(1) + ") and [IS_AUTH] = 0 and [VALID_FLAG] <> 0;";
            //+ "update dbo.[TS_SYS_ROLE_AUTH] "
            //+ "set [IS_AUTH] = 0 "
            //+ "where [ID] in (" + ids.Substring(1) + ") and [IS_AUTH] = 1 and [VALID_FLAG] <> 0;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
        /// <summary>
        /// 逆转IS_AUTH
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ReverseIsAuthList(List<RoleAuthInfo> list)
        {
            if (list.Count == 0) return true;
            string ids = string.Empty;
            foreach (var info in list)
            {
                ids += "," + info.Id;
            }
            string sql = "update dbo.[TS_SYS_ROLE_AUTH] "
                + "set [IS_AUTH] = 0 "
                + "where [ID] in (" + ids.Substring(1) + ") and [IS_AUTH] = 1 and [VALID_FLAG] <> 0;";
            //+ "update dbo.[TS_SYS_ROLE_AUTH] "
            //+ "set [IS_AUTH] = 0 "
            //+ "where [ID] in (" + ids.Substring(1) + ") and [IS_AUTH] = 1 and [VALID_FLAG] <> 0;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
    }
}

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
    public partial class UserRoleDAL
    {
        public List<Guid> GetFids(Guid userFid)
        {
            string sql = "select [ROLE_FID] from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [USER_FID] = @USER_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("ROLE_FID")));
                }
            }
            return list;
        }

        public List<UserRoleInfo> GetList(Guid userFid, Guid roleFid)
        {
            string sql = "select * from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [USER_FID] = @USER_FID and [ROLE_FID] = @ROLE_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, roleFid);
            List<UserRoleInfo> list = new List<UserRoleInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserRoleInfo(dr));
                }
            }
            return list;
        }

        public UserRoleInfo GetUserRoleInfo(Guid aFid)
        {
            string sql = "select * from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [FID] = @FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, aFid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateUserRoleInfo(dr);
            }
            return null;
        }

        public List<UserRoleInfo> GetUserRoleList(Guid userFid, Guid roleFid)
        {
            string sql = "select * from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [USER_FID] = @USER_FID and [ROLE_FID] = @ROLE_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, roleFid);
            List<UserRoleInfo> list = new List<UserRoleInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserRoleInfoToList(dr));
                }
            }
            return list;
        }

        private static UserRoleInfo CreateUserRoleInfoToList(IDataReader rdr)
        {
            UserRoleInfo info = new UserRoleInfo();

            info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));
            info.Fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));
            info.UserFid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("USER_FID"));
            info.RoleFid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("ROLE_FID"));
            info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));
            info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));
            info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));
            info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));
            info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));
            return info;
        }

        public Guid GetOrganizationFid(Guid userFid, Guid roleFid)
        {
            string sql = "select [ORGANIZATION_FID] from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [VALID_FLAG] <> 0 and [USER_FID] = @USER_FID and [ROLE_FID] = @ROLE_FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(cmd, "@ROLE_FID", DbType.Guid, roleFid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <returns></returns>
        public List<Guid> GetUserRoleOrganizationFids(Guid userFid, Guid roleFid)
        {
            string sql = "select [ORGANIZATION_FID] from dbo.[TS_SYS_USER_ROLE] with(nolock) "
                + "where [USER_FID] = @USER_FID and [ROLE_FID] = @ROLE_FID and [VALID_FLAG] =1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(cmd, "@ROLE_FID", DbType.Guid, roleFid);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("ORGANIZATION_FID")));
                }
            }
            return list;
        }
    }
}

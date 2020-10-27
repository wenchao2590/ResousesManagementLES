namespace DAL.SYS
{
    using DM.SYS;
    using Infrustructure.Utilities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// OrganizationDAL
    /// </summary>
    public partial class OrganizationDAL
    {
        /// <summary>
        /// 获取父级组织外键
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public Guid GetParentFid(Guid fid)
        {
            string sql = "select [PARENT_FID] from dbo.[TS_SYS_ORGANIZATION] with(nolock) where [FID] = @FID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }

        public long GetId(Guid fid)
        {
            string sql = "select top 1 [ID] from dbo.[TS_SYS_ORGANIZATION] with(nolock) "
             + "where [FID] = @FID and [VALID_FLAG] <> 0 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return long.Parse(result.ToString());
        }
        /// <summary>
        /// 根据父组织外键获取旗下所有子组织的外键
        /// </summary>
        /// <param name="parentFid"></param>
        /// <returns></returns>
        public List<Guid> GetOrganizationFidsByParentFid(Guid parentFid)
        {
            string sql = "select [FID] from dbo.[TS_SYS_ORGANIZATION] with(nolock) "
                + "where [PARENT_FID] = @PARENT_FID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@PARENT_FID", DbType.Guid, parentFid);
            List<Guid> list = new List<Guid>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetGuid(dr, dr.GetOrdinal("FID")));
                }
            }
            return list;
        }
        public List<string> GetCodesByFids(List<Guid> fids)
        {
            string sql = "select [CODE] from dbo.[TS_SYS_ORGANIZATION] with(nolock) "
                + "where [FID] in ('" + string.Join("','", fids.ToArray()) + "') and [VALID_FLAG] <> 0;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<string> list = new List<string>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetString(dr, dr.GetOrdinal("CODE")));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据FID获取对象
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public OrganizationInfo GetInfo(Guid fid)
        {
            string sql = "select * from dbo.[TS_SYS_ORGANIZATION] with(nolock) where [FID] = @FID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateOrganizationInfo(dr);
            }
            return null;
        }
    }
}

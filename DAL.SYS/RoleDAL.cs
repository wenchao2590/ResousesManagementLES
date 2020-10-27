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
    public partial class RoleDAL
    {
        public List<GuidValueDatasourceInfo> GetDataSource()
        {
            string sql = "select [FID],[ROLE_NAME] from dbo.[TS_SYS_ROLE] with(nolock) "
                + "where [VALID_FLAG] = @VALID_FLAG "
                + "order by [ROLE_NAME];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, true);
            List<GuidValueDatasourceInfo> list = new List<GuidValueDatasourceInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    GuidValueDatasourceInfo info = new GuidValueDatasourceInfo();
                    info.GuidValue = DBConvert.GetGuid(dr, dr.GetOrdinal("FID"));
                    info.StringDisplay = DBConvert.GetString(dr, dr.GetOrdinal("ROLE_NAME"));
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据USER_FID获取角色选项
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetRolesByUserFid(Guid userFid)
        {
            ///可以将不同权限范围的相同角色进行合并
            string sql = "select [FID],[ROLE_NAME] from dbo.[TS_SYS_ROLE] with(nolock) " +
                "where [FID] in (select [ROLE_FID] from dbo.[TS_SYS_USER_ROLE] with(nolock) " +
                "where [USER_FID] = @USER_FID and [VALID_FLAG] = 1) and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            List<GuidValueDatasourceInfo> list = new List<GuidValueDatasourceInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    GuidValueDatasourceInfo info = new GuidValueDatasourceInfo();
                    info.GuidValue = DBConvert.GetGuid(dr, dr.GetOrdinal("FID"));
                    info.StringDisplay = DBConvert.GetString(dr, dr.GetOrdinal("ROLE_NAME"));
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据USER_FID获取角色选项
        /// </summary>
        /// <param name="userFid"></param>
        /// <returns></returns>
        public List<GuidValueDatasourceInfo> GetRolesByUserFid(Guid userFid, Guid plantFid)
        {
            //ROLE_FID
            string sql = "select a.[ROLE_FID],[ROLE_NAME] from dbo.[TS_SYS_USER_ROLE] a with(nolock) "
                + "left join dbo.[TS_SYS_ROLE] b with(nolock) on a.[ROLE_FID] = b.[FID] "
                + "where [USER_FID] = @USER_FID AND [PLANT_FID] = @PLANT_FID "
                + "and a.[VALID_FLAG] <> 0 and b.[VALID_FLAG] <> 0 GROUP BY ROLE_FID,ROLE_NAME ORDER BY ROLE_NAME";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(dbCommand, "@PLANT_FID", DbType.Guid, plantFid);
            List<GuidValueDatasourceInfo> list = new List<GuidValueDatasourceInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    GuidValueDatasourceInfo info = new GuidValueDatasourceInfo();
                    info.GuidValue = DBConvert.GetGuid(dr, dr.GetOrdinal("ROLE_FID"));//ROLE_FID
                    info.StringDisplay = DBConvert.GetString(dr, dr.GetOrdinal("ROLE_NAME"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}

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
    public partial class EntityDAL
    {
        public EntityInfo GetInfo(string entityName, string tableName)
        {
            var fields = string.Empty;
            if (string.IsNullOrEmpty(tableName))
                fields = "and [TABLE_NAMES] = N'" + tableName + "' ";
            string sql = "select * from dbo.[TS_SYS_ENTITY] with(nolock) "
                + "where [ENTITY_NAME] = @ENTITY_NAME " + fields + "and [VALID_FLAG] = 1;";

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, entityName);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateEntityInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public EntityInfo GetInfo(string entityName)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY] with(nolock) "
               + "where [ENTITY_NAME] = @ENTITY_NAME and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, entityName);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateEntityInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public EntityInfo GetInfo(Guid fid)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY] with(nolock) "
               + "where [FID] = @FID "
               + "and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateEntityInfo(dr);
            }
            return null;
        }
    }
}

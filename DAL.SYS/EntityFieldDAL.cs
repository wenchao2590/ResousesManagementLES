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
    public partial class EntityFieldDAL
    {
        /// <summary>
        /// 获取Excel模板导出字段
        /// </summary>
        /// <param name="entityFid"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetExcelFieldList(Guid entityFid)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) where [ENTITY_FID] = @ENTITY_FID and [VALID_FLAG] =1 and [EXPORT_EXCEL_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_FID", DbType.Guid, entityFid);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
                }
            }
            return list;
        }

        public List<EntityFieldInfo> GetListByEntityName(string entityName)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) "
                + "where [ENTITY_FID] in (select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) where [VALID_FLAG] = 1 and [ENTITY_NAME] = @ENTITY_NAME) and [VALID_FLAG] =1 "
                + "order by [DISPLAY_ORDER];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, value: entityName);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 获取Excel模板导出字段
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetExcelFieldList(string entityName)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) "
                + "where [ENTITY_FID] in (select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) where [VALID_FLAG] = 1 and [ENTITY_NAME] = @ENTITY_NAME) and [VALID_FLAG] =1 and [EXPORT_EXCEL_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, value: entityName);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
                }
            }
            return list;
        }
        public List<EntityFieldInfo> GetInfoList(Guid entityFid)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) "
                + "where [ENTITY_FID] = @ENTITY_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_FID", DbType.AnsiString, entityFid);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
                }
            }
            return list;
        }
        public bool IsExistEditReaderonly(Guid entityFid)
        {
            string sql = "select count(1) from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) "
                + "where [ENTITY_FID] = @ENTITY_FID and [VALID_FLAG] <> 0 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_FID", DbType.Guid, entityFid);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return false;
            if (int.Parse(result.ToString()) == 0)
                return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public EntityFieldInfo GetInfo(string entityName, string fieldName)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) "
                + "where [ENTITY_FID] in (select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) "
                + "where [ENTITY_NAME] = @ENTITY_NAME and [VALID_FLAG] = 1) "
                + "and [FIELD_NAME] = @FIELD_NAME "
                + "and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, entityName);
            db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.AnsiString, fieldName);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateEntityFieldInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetStaticticsFields(string entityName)
        {
            string sql = "select * from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) " +
                "where [ENTITY_FID] in " +
                "(select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) where [VALID_FLAG] = 1 and [ENTITY_NAME] = @ENTITY_NAME) " +
                "and [VALID_FLAG] =1 and [STATISTICS_FLAG] = 1 " +
                "order by [DISPLAY_ORDER];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ENTITY_NAME", DbType.AnsiString, value: entityName);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
                }
            }
            return list;
        }
    }
}

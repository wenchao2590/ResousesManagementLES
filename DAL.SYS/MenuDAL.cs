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
    public partial class MenuDAL
    {
        /// <summary>
        /// 获取LINK_URL对应的FID
        /// </summary>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public Guid GetFid(string linkUrl)
        {
            string sql = "select TOP 1 [FID] from dbo.[TS_SYS_MENU] with(nolock) "
                + "where [LINK_URL] = @LINK_URL and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@LINK_URL", DbType.AnsiString, linkUrl);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }
        public Guid GetFidByMenuNameInType2(string menuName)
        {
            string sql = "select TOP 1 [FID] from dbo.[TS_SYS_MENU] with(nolock) "
                + "where [MENU_NAME] = @MENU_NAME and [VALID_FLAG] <> 0 and [MENU_TYPE] = 20";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@MENU_NAME", DbType.AnsiString, menuName);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }

        public Guid GetFidByMenuNameInType3(string LinkUrl)
        {
            string sql = "select TOP 1 [FID] from dbo.[TS_SYS_MENU] with(nolock) "
                + "where [Link_URL] = @LinkUrl and [VALID_FLAG] <> 0 and [MENU_TYPE] = 30";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@LinkUrl", DbType.AnsiString, LinkUrl);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }

        public MenuInfo GetInfo(Guid fid)
        {
            string sql = "select * from dbo.[TS_SYS_MENU] with(nolock) where FID = @FID and [VALID_FLAG] <> 0;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMenuInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据LINKURL获取对象
        /// </summary>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public MenuInfo GetInfo(string linkUrl)
        {
            string sql = "select * from dbo.[TS_SYS_MENU] with(nolock) "
                + "where [LINK_URL] = @LINK_URL and [VALID_FLAG] = 1 "
                + "order by [ID] desc;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@LINK_URL", DbType.AnsiString, linkUrl);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMenuInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 获取子菜单对象
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="parentMenuFid"></param>
        /// <returns></returns>
        public MenuInfo GetInfo(string menuName, Guid parentMenuFid)
        {
            string sql = "select * from dbo.[TS_SYS_MENU] with(nolock) "
                + "where [MENU_NAME] = @MENU_NAME and [PARENT_MENU_FID] = @PARENT_MENU_FID and [VALID_FLAG] = 1 "
                + "order by [ID] desc;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@MENU_NAME", DbType.AnsiString, menuName);
            db.AddInParameter(dbCommand, "@PARENT_MENU_FID", DbType.Guid, parentMenuFid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMenuInfo(dr);
            }
            return null;
        }
    }
}

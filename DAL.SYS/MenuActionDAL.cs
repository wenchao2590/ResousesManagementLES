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
    public partial class MenuActionDAL
    {
        public MenuActionInfo GetInfo(Guid menuFid, Guid actionFid)
        {
            string sql = "select * from dbo.[TS_SYS_MENU_ACTION] with(nolock) "
                + "where [MENU_FID] = @MENU_FID and [ACTION_FID] = @ACTION_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@MENU_FID", DbType.Guid, menuFid);
            db.AddInParameter(dbCommand, "@ACTION_FID", DbType.Guid, actionFid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateMenuActionInfo(dr);
            }
            return null;
        }

        public bool LogicDelete(Guid menuFid, Guid actionFid, string modifyUser)
        {
            string sql = "update dbo.[TS_SYS_MENU_ACTION] with(rowlock) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "where [MENU_FID] = @MENU_FID and [ACTION_FID] = @ACTION_FID and [VALID_FLAG] <> 0";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
            db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, modifyUser);
            db.AddInParameter(dbCommand, "@MENU_FID", DbType.Guid, menuFid);
            db.AddInParameter(dbCommand, "@ACTION_FID", DbType.Guid, actionFid);
            return db.ExecuteNonQuery(dbCommand) > 0 ? true : false;
        }
    }
}

namespace DAL.SYS
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// UserFavoritesDAL
    /// </summary>
    public partial class UserFavoritesDAL
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="menuFid"></param>
        /// <returns></returns>
        public bool LogicDelete(Guid userFid, Guid roleFid, Guid menuFid)
        {
            string sql = "update dbo.[TS_SYS_USER_FAVORITES] " +
                "set [VALID_FLAG] = 0 " +
                "where [USER_FID] = @USER_FID and [ROLE_FID] = @ROLE_FID and [MENU_FID] = @MENU_FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@USER_FID", DbType.Guid, userFid);
            db.AddInParameter(cmd, "@ROLE_FID", DbType.Guid, roleFid);
            db.AddInParameter(cmd, "@MENU_FID", DbType.Guid, menuFid);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
    }
}

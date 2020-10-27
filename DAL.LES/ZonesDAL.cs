namespace DAL.LES
{
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// ZonesDAL
    /// </summary>
    public partial class ZonesDAL
    {
        /// <summary>
        /// GetZonesInfo
        /// </summary>
        /// <param name="zoneNo"></param>
        /// <returns></returns>
        public ZonesInfo GetZonesInfo(string zoneNo, string wmNo)
        {
            string sql = "select * from [LES].[TM_WMM_ZONES] with(nolock) " +
                "where [VALID_FLAG] = 1 and [ZONE_NO] = @ZONE_NO and [WM_NO] = @WM_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateZonesInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wmNo"></param>
        /// <returns></returns>
        public List<ZonesInfo> GetZonesInfos(string wmNo)
        {
            string sql = "select * from [LES].[TM_WMM_ZONES] with(nolock) where [VALID_FLAG] = 1 and [WM_NO] = @WM_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            List<ZonesInfo> list = new List<ZonesInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateZonesInfo(dr));
                }
            }
            return list;
        }
    }
}

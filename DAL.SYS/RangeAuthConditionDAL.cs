namespace DAL.SYS
{
    using DM.SYS;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// RangeAuthConditionDAL
    /// </summary>
    public partial class RangeAuthConditionDAL
    {
        /// <summary>
        /// 获取外键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid GetFid(long id)
        {
            string sql = "select [FID] from dbo.[TS_SYS_RANGE_AUTH_CONDITION] with(nolock) where [VALID_FLAG] = 1 and [ID]=@ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ID", DbType.AnsiString, id);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public RangeAuthConditionInfo GetInfo(Guid fid)
        {
            string sql = "select * from dbo.[TS_SYS_RANGE_AUTH_CONDITION] with(nolock) where [VALID_FLAG] = 1 and [FID]=@FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateRangeAuthConditionInfo(dr);
            }
            return null;
        }
    }
}

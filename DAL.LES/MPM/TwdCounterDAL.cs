#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    //// <summary>
    /// TwdCounterDAL对应表[TT_MPM_TWD_COUNTER]
    /// </summary>
    public partial class TwdCounterDAL
    {
        public TwdCounterInfo GetInfoByFid(Guid Fid)
        {
            string sql = string.Format(@"select * from [LES].[TT_MPM_TWD_COUNTER] with(nolock) where [VALID_FLAG] = 1  AND [FID]='{0}'", Fid);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_TWD_COUNTER_SELECT_BY_ID);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateTwdCounterInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据物料拉动信息外键获取计数器
        /// </summary>
        /// <param name="Fid"></param>
        /// <returns></returns>
        public TwdCounterInfo GetInfoByPartPullFid(Guid partPullFid)
        {
            string sql = "select * from [LES].[TT_MPM_TWD_COUNTER] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PART_PULL_FID] = @PART_PULL_FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_PULL_FID", DbType.Guid, partPullFid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateTwdCounterInfo(dr);
            }
            return null;
        }
    }
}

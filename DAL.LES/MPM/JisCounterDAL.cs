using DM.LES;
namespace DAL.LES
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    public partial class JisCounterDAL
    {
        /// <summary>
        /// 根据零件类外键获取计数器
        /// 状态为正在累计
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <returns></returns>
        public JisCounterInfo GetInfoByPartBoxFid(Guid partBoxFid)
        {
            string sql = "select * from [LES].[TT_MPM_JIS_COUNTER] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PART_BOX_FID] = @PART_BOX_FID and [STATUS] = @STATUS;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_BOX_FID", DbType.Guid, partBoxFid);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, (int)JisCounterStatusConstants.Accumulating);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateJisCounterInfo(dr);
            }
            return null;
        }
    }
}

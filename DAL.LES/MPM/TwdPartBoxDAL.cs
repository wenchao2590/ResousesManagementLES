namespace DAL.LES
{
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// TwdPartBoxDAL
    /// </summary>
    public partial class TwdPartBoxDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public TwdPartBoxInfo GetInfo(string partBoxCode)
        {
            string sql = "select * from [LES].[TM_MPM_TWD_PART_BOX] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PART_BOX_CODE] = @PART_BOX_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_BOX_CODE", DbType.AnsiString, partBoxCode);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateTwdPartBoxInfo(dr);
            }
            return null;
        }
    }
}

using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    /// <summary>
    /// PlanPartBoxDAL
    /// </summary>
    public partial class PlanPartBoxDAL
    {
        /// <summary>
        /// 根据零件类代码获取对象
        /// </summary>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public PlanPartBoxInfo GetInfo(string partBoxCode)
        {
            string sql = "select * from [LES].[TM_MPM_PLAN_PART_BOX] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_BOX_CODE] = @PART_BOX_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_BOX_CODE", DbType.AnsiString, partBoxCode);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreatePlanPartBoxInfo(dr);
            }
            return null;
        }
    }
}

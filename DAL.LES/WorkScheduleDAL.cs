using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.LES
{
    /// <summary>
    /// 工作日历
    /// </summary>
    public partial class WorkScheduleDAL
    {
        /// <summary>
        /// GetListForInterfaceDataSync
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<WorkScheduleInfo> GetListForInterfaceDataSync(List<DateTime> date)
        {
            string sql = "select * from [LES].[TM_BAS_WORK_SCHEDULE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [DATE] in ('" + string.Join("','", date.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<WorkScheduleInfo> list = new List<WorkScheduleInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateWorkScheduleInfo(dr));
                }
            }
            return list;
        }

    }
}

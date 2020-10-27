using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class ProcessScheduleDAL
    {
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool EnableInfos(string ids, string loginUser)
        {
            string sql = "update [dbo].[TS_SYS_PROCESS_SCHEDULE] "
                + "set [LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Running + ",[MODIFY_USER] = '" + loginUser + "' "
                + "where [VALID_FLAG] = 1 and [ID] in (" + ids + ") and [LAST_RUN_STATUS] in (" + (int)ProcessRunStatusConstants.Init + "," + (int)ProcessRunStatusConstants.Pause + ")";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return int.Parse("0" + db.ExecuteNonQuery(cmd)) > 0 ? true : false;
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool PauseInfos(string ids, string loginUser)
        {
            string sql = "update [dbo].[TS_SYS_PROCESS_SCHEDULE] "
                + "set [LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Pause + " [MODIFY_USER] = '" + loginUser + "' "
                + "where [VALID_FLAG] = 1 and [ID] in (" + ids + ") and [LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Running + "";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return int.Parse("0" + db.ExecuteNonQuery(cmd)) > 0 ? true : false;
        }
    }
}


//---------------------------------------------------------------------------
//Name:	ProcessScheduleDAL
//Function:	DataAccess
//Author:	CodeSmith
//Date:    2011-7-5
//---------------------------------------------------------------------------
//Change History:
// Date				Who			Changes Made           Purpose         Comments
//---------------------------------------------------------------------------
//2011-7-5	CodeSmith	Initial creation
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Infrustructure.Utilities;

namespace Infrustructure.Thread
{
    /// <summary>
    /// DataAccess
    /// </summary>
    public partial class ProcessScheduleDAL
    {
        /// <summary>
        /// 获取服务配置
        /// </summary>
        /// <param name="serviceFid"></param>
        /// <returns></returns>
        public static ProcessScheduleInfo GetInfo(Guid serviceFid)
        {
            string sql = "select [PROCESS_NAME]"
                + ",[LAST_RUN_BEGIN_TIME]"
                + ",[LAST_RUN_END_TIME]"
                + ",[LAST_RUN_STATUS]"
                + ",[RUN_INTERVAL]"
                + ",[CHECK_INTERVAL]"
                + ",[SYSTEM_PARAMETER1]"
                + ",[SYSTEM_PARAMETER2]"
                + ",[SYSTEM_PARAMETER3]"
                + ",[SYSTEM_PARAMETER4]"
                + ",[SYSTEM_PARAMETER5]"
                + ",[FID] "
                + "from dbo.[TS_SYS_PROCESS_SCHEDULE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [FID] = @FID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, serviceFid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                {
                    return CreateProcessScheduleInfo(dr);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private static ProcessScheduleInfo CreateProcessScheduleInfo(IDataReader rdr)
        {
            ProcessScheduleInfo info = new ProcessScheduleInfo();
            info.System_name = DBConvert.GetString(rdr, rdr.GetOrdinal("PROCESS_NAME"));
            info.Last_run_begin_time = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("LAST_RUN_BEGIN_TIME"));
            info.Last_run_end_time = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("LAST_RUN_END_TIME"));
            info.Last_run_status = DBConvert.GetInt32(rdr, rdr.GetOrdinal("LAST_RUN_STATUS"));
            info.Run_interval = DBConvert.GetInt32(rdr, rdr.GetOrdinal("RUN_INTERVAL"));
            info.Check_interval = DBConvert.GetInt32(rdr, rdr.GetOrdinal("CHECK_INTERVAL"));
            info.System_parameter1 = DBConvert.GetString(rdr, rdr.GetOrdinal("SYSTEM_PARAMETER1"));
            info.System_parameter2 = DBConvert.GetString(rdr, rdr.GetOrdinal("SYSTEM_PARAMETER2"));
            info.System_parameter3 = DBConvert.GetString(rdr, rdr.GetOrdinal("SYSTEM_PARAMETER3"));
            info.System_parameter4 = DBConvert.GetString(rdr, rdr.GetOrdinal("SYSTEM_PARAMETER4"));
            info.System_parameter5 = DBConvert.GetString(rdr, rdr.GetOrdinal("SYSTEM_PARAMETER5"));
            info.Service_fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));
            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceFid"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        public static void UpdateRunTime(Guid serviceFid, DateTime beginTime, DateTime endTime)
        {
            string sql = "update dbo.[TS_SYS_PROCESS_SCHEDULE] with(rowlock)"
                + "set [LAST_RUN_BEGIN_TIME] = @LAST_RUN_BEGIN_TIME, [LAST_RUN_END_TIME] = @LAST_RUN_END_TIME, [MODIFY_DATE] = GETDATE(), [MODIFY_USER] = @MODIFY_USER "
                + "where FID = @FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@LAST_RUN_BEGIN_TIME", DbType.DateTime, beginTime);
            db.AddInParameter(dbCommand, "@LAST_RUN_END_TIME", DbType.DateTime, endTime);
            db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, "THREAD");
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, serviceFid);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}

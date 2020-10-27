using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class VehiclePointStatusDAL
    {
        /// <summary>
        /// 根据工厂、车间、生产线（可选）、状态点
        /// 获取最大LES序号
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="workshop"></param>
        /// <param name="statusPointCode"></param>
        /// <param name="assemlyLine"></param>
        /// <returns></returns>
        public long GetMaxSeqNo(string plant, string workshop, string statusPointCode, string assemlyLine)
        {
            string sql = "select max(SEQ_NO) from [LES].[TT_BAS_VEHICLE_POINT_STATUS] with(nolock) " +
                "where [PLANT] = @PLANT and [WORKSHOP] = @WORKSHOP and [ASSEMBLY_LINE] = @ASSEMBLY_LINE and [STATUS_POINT_CODE] = @STATUS_POINT_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLANT", DbType.AnsiString, plant);
            db.AddInParameter(cmd, "@WORKSHOP", DbType.AnsiString, workshop);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, assemlyLine);
            db.AddInParameter(cmd, "@STATUS_POINT_CODE", DbType.AnsiString, statusPointCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt64(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="workshop"></param>
        /// <param name="statusPointCode"></param>
        /// <param name="assemlyLine"></param>
        /// <returns></returns>
        public DateTime? GetLastTime(string plant, string workshop, string statusPointCode, string assemlyLine)
        {
            string sql = "select max([PASS_TIME]) from [LES].[TT_BAS_VEHICLE_POINT_STATUS] with(nolock) " +
                "where [PLANT] = @PLANT and [WORKSHOP] = @WORKSHOP and [ASSEMBLY_LINE] = @ASSEMBLY_LINE and [STATUS_POINT_CODE] = @STATUS_POINT_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLANT", DbType.AnsiString, plant);
            db.AddInParameter(cmd, "@WORKSHOP", DbType.AnsiString, workshop);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, assemlyLine);
            db.AddInParameter(cmd, "@STATUS_POINT_CODE", DbType.AnsiString, statusPointCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return null;
            return Convert.ToDateTime(result);
        }
        /// <summary>
        /// 获取当前维度时间最近的LES序号
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="workshop"></param>
        /// <param name="statusPointCode"></param>
        /// <param name="assemlyLine"></param>
        /// <returns></returns>
        public long GetLastTimeSeqNo(string plant, string workshop, string statusPointCode, string assemlyLine)
        {
            string sql = "select top 1 [SEQ_NO] from [LES].[TT_BAS_VEHICLE_POINT_STATUS] with(nolock) " +
                "where [PLANT] = @PLANT and [WORKSHOP] = @WORKSHOP and [ASSEMBLY_LINE] = @ASSEMBLY_LINE and [STATUS_POINT_CODE] = @STATUS_POINT_CODE " +
                "order by [PASS_TIME] desc;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLANT", DbType.AnsiString, plant);
            db.AddInParameter(cmd, "@WORKSHOP", DbType.AnsiString, workshop);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, assemlyLine);
            db.AddInParameter(cmd, "@STATUS_POINT_CODE", DbType.AnsiString, statusPointCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt64(result);
        }
    }
}

using DM.LES;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DAL.LES
{
    public partial class KanbanPartBoxDAL
    {
        /// <summary>
        /// 自定义逻辑删除方法
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public int CustomLogicDelete(long aId, string loginUser)
        {
            string sql = "update [LES].[TM_MPM_KANBAN_PART_BOX] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() ,[STATUS] = @STATUS "
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
            db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
            db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, (int)BasicDataStatusConstants.Disabled);

            return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
        }
        /// <summary>
        /// 根据看板零件类代码获取看板卡类型代码
        /// </summary>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public string GetCardTypeCodeByPartBoxCode(string partBoxCode)
        {
            string sql = "select [CARD_TYPE_CODE] from [LES].[TM_MPM_KANBAN_PART_BOX] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_BOX_CODE] = @PART_BOX_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_BOX_CODE", DbType.AnsiString, partBoxCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
    }
}

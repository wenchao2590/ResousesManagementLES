using DM.LES;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL.LES
{
    public partial class KanbanCardDAL
    {
        public int CustomLogicDelete(long aId, string loginUser)
        {
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE(),[STATUS] = @STATUS "
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
            db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
            db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, (int)BasicDataStatusConstants.Disabled);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
        }
        /// <summary>
        /// 根据看板卡获取看板卡信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public KanbanCardInfo SelectInfoByCardNo(string cardNo)
        {
            string sql = "select * from [LES].[TM_MPM_KANBAN_CARD] with(nolock) "
                + "where [VALID_FLAG] = 1 and [STATUS] = @STATUS and [USED_STATUS] IN (10,20) and [CARD_NO] = @CARD_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, (int)BasicDataStatusConstants.Enable);
            db.AddInParameter(cmd, "@CARD_NO", DbType.String, cardNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateKanbanCardInfo(dr);
            }
            return null;
        }

        /// <summary>
        /// 看办卡生成拉动单
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public bool CreateKanbanPullOrder(string sqlstr)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlstr);
            return int.Parse("0" + db.ExecuteScalar(dbCommand)) == 0;

        }
    }
}

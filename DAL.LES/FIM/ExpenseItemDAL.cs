using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class ExpenseItemDAL
    {
        /// <summary>
        /// GetExpenseType
        /// </summary>
        /// <param name="expenseCode"></param>
        /// <returns></returns>
        public int GetExpenseType(string expenseCode)
        {
            string sql = "select [EXPENSE_TYPE] from LES.[TM_BAS_EXPENSE_ITEM] with(nolock) " +
                "where [EXPENSE_CODE] = @EXPENSE_CODE and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@EXPENSE_CODE", DbType.AnsiString, expenseCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }
    }
}

using DM.SYS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class PrintConfigDAL
    {
        /// <summary>
        /// 通过配置代码获取打印配置
        /// </summary>
        /// <param name="printConfigCode"></param>
        /// <returns></returns>
        public PrintConfigInfo GetInfoByCode(string printConfigCode)
        {
            string sql = "select * from [dbo].[TS_SYS_PRINT_CONFIG] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PRINT_CONFIG_CODE] = @PRINT_CONFIG_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PRINT_CONFIG_CODE", DbType.AnsiString, printConfigCode);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreatePrintConfigInfo(dr);
            }
            return null;
        }

    }
}

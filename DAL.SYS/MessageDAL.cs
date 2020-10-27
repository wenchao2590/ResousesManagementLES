using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class MessageDAL
    {
        /// <summary>
        /// 获取中文提示信息
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public string GetMessageCn(string messageCode)

        {
            string sql = "select [MESSAGE_CN] from dbo.[TS_SYS_MESSAGE] with(nolock) "
             + "where [MESSAGE_CODE] = @MESSAGE_CODE and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MESSAGE_CODE", DbType.AnsiString, messageCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return messageCode;
            return result.ToString();
        }
        /// <summary>
        /// 获取英文提示信息
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public string GetMessageEn(string messageCode)
        {
            string sql = "select [MESSAGE_EN] from dbo.[TS_SYS_MESSAGE] with(nolock) "
             + "where [MESSAGE_CODE] = @MESSAGE_CODE and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MESSAGE_CODE", DbType.AnsiString, messageCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return messageCode;
            return result.ToString();
        }
    }
}

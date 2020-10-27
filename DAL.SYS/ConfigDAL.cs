using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class ConfigDAL
    {
        /// <summary>
        /// 根据代码获取值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetValueByCode(string code)
        {
            string sql = "select [CONFIG_VALUE] from dbo.[TS_SYS_CONFIG] with(nolock) "
                + "where [CODE]=@CODE and [VALID_FLAG] = 1 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@CODE", DbType.AnsiString, code);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 根据代码s获取值s
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValuesByCodes(string[] codes)
        {
            string sql = "select [CONFIG_VALUE],[CODE] from dbo.[TS_SYS_CONFIG] with(nolock) "
                + "where [CODE] in ('" + string.Join("','", codes) + "') and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    keyValuePairs.Add(
                        DBConvert.GetString(dr, dr.GetOrdinal("CODE")),
                        DBConvert.GetString(dr, dr.GetOrdinal("CONFIG_VALUE")));
                }
            }
            return keyValuePairs;
        }
    }
}

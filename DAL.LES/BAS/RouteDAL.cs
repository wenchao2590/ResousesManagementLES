using DM.LES;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class RouteDAL
    {
        public List<RouteInfo> GetListForInterfaceDataSync(List<string> routeCodes)
        {
            string sql = "select [ID],[ROUTE],[PLANT] "
                + "from [LES].[TM_BAS_ROUTE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [ROUTE] in ('" + string.Join("','", routeCodes.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<RouteInfo> list = new List<RouteInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    RouteInfo info = new RouteInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.Plant = DBConvert.GetString(dr, dr.GetOrdinal("PLANT"));
                    info.Route = DBConvert.GetString(dr, dr.GetOrdinal("ROUTE"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetNameByCode(string code)
        {
            string sql = "select [ROUTE_NAME] from [LES].[TM_BAS_ROUTE] with(nolock) where [VALID_FLAG] = 1 and [ROUTE] = @ROUTE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ROUTE", DbType.AnsiString, code);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return string.Empty;
            return result.ToString();
        }
    }
}

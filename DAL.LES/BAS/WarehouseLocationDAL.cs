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
    public partial class WarehouseLocationDAL
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchLogicDelete(List<long> ids, string loginUser)
        {
            string sql = "update [LES].[TM_BAS_WAREHOUSE_LOCATION] "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1  and [ID] in (" + string.Join(",", ids.ToArray()) + ");";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@VALID_FLAG", DbType.Boolean, false);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            return int.Parse("0" + db.ExecuteNonQuery(cmd)) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlocs"></param>
        /// <returns></returns>
        public List<WarehouseLocationInfo> GetListForInterfaceDataSync(List<string> dlocs)
        {
            string sql = "select [ID],[DLOC] "
                + "from [LES].[TM_BAS_WAREHOUSE_LOCATION] with(nolock) "
                + "where [VALID_FLAG] = 1 and [DLOC] in ('" + string.Join("','", dlocs.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<WarehouseLocationInfo> list = new List<WarehouseLocationInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    WarehouseLocationInfo info = new WarehouseLocationInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.Dloc = DBConvert.GetString(dr, dr.GetOrdinal("DLOC"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}

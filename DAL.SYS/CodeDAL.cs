using DM.SYS;
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
    public partial class CodeDAL
    {
        public List<CodeItemDatasourceInfo> GetDataSource(string codeName)
        {
            string sql = "select [ITEM_VALUE],[ITEM_NAME] from dbo.[TS_SYS_CODE_ITEM] with(nolock) "
                + "where [CODE_FID] in (select [FID] from dbo.[TS_SYS_CODE] where [CODE_NAME] = @CODE_NAME and [VALID_FLAG] <> 0) and [VALID_FLAG] <> 0 "
                + "order by [DISPLAY_ORDER];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@CODE_NAME", DbType.AnsiString, codeName);
            List<CodeItemDatasourceInfo> list = new List<CodeItemDatasourceInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    CodeItemDatasourceInfo info = new CodeItemDatasourceInfo();
                    info.ItemValue = DBConvert.GetInt32(dr, dr.GetOrdinal("ITEM_VALUE"));
                    info.ItemDisplay = DBConvert.GetString(dr, dr.GetOrdinal("ITEM_NAME"));
                    list.Add(info);
                }
            }

            return list;
        }
    }
}

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
    public partial class SearchModelDAL
    {
        public SearchModelInfo GetInfo(string searchName)
        {
            string sql = "select * from dbo.[TS_SYS_SEARCH_MODEL] with(nolock) "
                + "where [SEARCH_NAME] = @SEARCH_NAME and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@SEARCH_NAME", DbType.AnsiString, searchName);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateSearchModelInfo(dr);
            }
            return null;
        }
    }
}

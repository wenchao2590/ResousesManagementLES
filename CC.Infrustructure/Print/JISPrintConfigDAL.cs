using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace Infrustructure.Print
{
    class JISPrintConfigDAL
    {
        public List<JISPrintConfigInfo> GetList()
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbComm = db.GetSqlStringCommand("SELECT * FROM [MES].TS_JIS_PRINT_CONFIG ORDER BY PRINT_ID");

            IDataReader reader = db.ExecuteReader(dbComm);

            return ConvertToList(reader);
        }

        private List<JISPrintConfigInfo> ConvertToList(IDataReader reader)
        {
            var infos = new List<JISPrintConfigInfo>();

            while (reader.Read())
            {
                JISPrintConfigInfo info = new JISPrintConfigInfo();

                info.PrintID = reader.GetInt32(0);
                info.StartRow = reader.GetInt32(1);
                info.EndRow = reader.GetInt32(2);
                info.FontSize = reader.GetString(3);

                infos.Add(info);
            }

            return infos;
        }
    }

    
}

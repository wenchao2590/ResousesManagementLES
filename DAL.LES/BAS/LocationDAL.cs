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
    public partial class LocationDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public List<LocationInfo> GetListForInterfaceDataSync(List<string> locations)
        {
            string sql = "select [ID],[LOCATION] "
                + "from [LES].[TM_BAS_LOCATION] with(nolock) "
                + "where [VALID_FLAG] = 1 and [LOCATION] in ('" + string.Join("','", locations.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<LocationInfo> list = new List<LocationInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    LocationInfo info = new LocationInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.Location = DBConvert.GetString(dr, dr.GetOrdinal("LOCATION"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}

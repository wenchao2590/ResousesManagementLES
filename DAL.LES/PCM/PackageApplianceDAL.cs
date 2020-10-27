
#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    //// <summary>
    /// AssemblyLineDAL对应表[TM_BAS_PACKAGE_APPLIANCE]
    /// </summary>
    public partial class PackageApplianceDAL: BusinessObjectProvider<PackageApplianceInfo>
    {
       
        public List<PackageApplianceInfo> GetListForInterfaceDataSync(List<string> locations)
        {
            string sql = "select [ID],[PACKAGE_NO] "
                + "from [LES].[TM_BAS_PACKAGE_APPLIANCE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PACKAGE_NO] in ('" + string.Join("','", locations.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<PackageApplianceInfo> list = new List<PackageApplianceInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PackageApplianceInfo info = new PackageApplianceInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.PackageNo = DBConvert.GetString(dr, dr.GetOrdinal("PACKAGE_NO"));
                    list.Add(info);
                }
            }
            return list;
        }

    }
}

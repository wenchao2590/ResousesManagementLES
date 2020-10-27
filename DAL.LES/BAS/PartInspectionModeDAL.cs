
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
    public partial class PartInspectionModeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public List<PartInspectionModeInfo> GetListForInterfaceDataSync()
        {
            string sql = "select [ID],[SUPPLIER_NUM],[PART_NO],[INSPECTION_MODE] "
                + "from [LES].[TM_BAS_PART_INSPECTION_MODE] with(nolock) "
                + "where [VALID_FLAG] = 1 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<PartInspectionModeInfo> list = new List<PartInspectionModeInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PartInspectionModeInfo info = new PartInspectionModeInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.InspectionMode = DBConvert.GetInt32(dr, dr.GetOrdinal("INSPECTION_MODE"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}

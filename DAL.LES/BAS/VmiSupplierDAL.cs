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
    public partial class VmiSupplierDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierNums"></param>
        /// <returns></returns>
        public List<VmiSupplierInfo> GetListForInterfaceDataSync(List<string> supplierNums)
        {
            string sql = "select [ID],[SUPPLIER_NUM],[WM_NO] "
                + "from [LES].[TM_BAS_VMI_SUPPLIER] with(nolock) "
                + "where [VALID_FLAG] = 1 and [SUPPLIER_NUM] in ('" + string.Join("','", supplierNums.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<VmiSupplierInfo> list = new List<VmiSupplierInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    VmiSupplierInfo info = new VmiSupplierInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.WmNo = DBConvert.GetString(dr, dr.GetOrdinal("WM_NO"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="plant"></param>
        /// <returns></returns>
        public VmiSupplierInfo GetInfo(string supplierNum, string wmNo, string zoneNo)
        {
            string sql = "select * from [LES].[TM_BAS_VMI_SUPPLIER] with(nolock) " +
                "where [SUPPLIER_NUM] = @SUPPLIER_NUM and [WM_NO] = @WM_NO and [ZONE_NO] = @ZONE_NO  and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateVmiSupplierInfo(dr);
            }
            return null;
        }
    }
}

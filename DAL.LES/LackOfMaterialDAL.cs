using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.LES
{
    public partial class LackOfMaterialDAL
    {
        public LackOfMaterialInfo GetListInfo(Guid lackOrderFid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = " and [FID] = N'" + lackOrderFid + "'";
            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(TT_ATP_LACK_OF_MATERIAL_SELECT, sql));
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, lackOrderFid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateLackOfMaterialInfo(dr);
            }
            return null;
        }
    }
}

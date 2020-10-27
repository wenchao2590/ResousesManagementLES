#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    //// <summary>
    /// PartsBoxDAL对应表[V_ALL_PARTS_BOX]
    /// </summary>
    public partial class PartsBoxDAL : BusinessObjectProvider<PartsBoxInfo>
    {
        #region Access Methods
        /// <summary>
        /// 根据零件类代码获取零件类
        /// </summary>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public PartsBoxInfo GetInfoByPartBox(string partBoxCode)
        {
            string sql = string.Format(@"select * from LES.[V_ALL_PARTS_BOX] where [BOX_PARTS] = N'{0}'", partBoxCode);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreatePartsBoxInfo(dr);
            }
            return null;
        }

        #endregion      
    }
}

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
    public partial class PlantDAL
    {
        /// <summary>
        /// 根据SAP工厂代码获取LES工厂代码
        /// </summary>
        /// <param name="sapPlantCode"></param>
        /// <returns></returns>
        public string GetPlantBySapPlantCode(string sapPlantCode)
        {
            string sql = "select [PLANT] from [LES].[TM_BAS_PLANT] with(nolock) where [VALID_FLAG] = 1 and [SAP_PLANT_CODE] =@SAP_PLANT_CODE";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SAP_PLANT_CODE", DbType.AnsiString, sapPlantCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// WS.SAP.SyncInboundService.SyncMaintainParts
        /// </summary>
        /// <returns></returns>
        public List<PlantInfo> GetListForInterfaceDataSync()
        {
            string sql = "select [PLANT],[SAP_PLANT_CODE] "
                + "from [LES].[TM_BAS_PLANT] with(nolock) "
                + "where [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PlantInfo> list = new List<PlantInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    PlantInfo info = new PlantInfo();
                    info.Plant = DBConvert.GetString(dr, dr.GetOrdinal("PLANT"));
                    info.SapPlantCode = DBConvert.GetString(dr, dr.GetOrdinal("SAP_PLANT_CODE"));
                    list.Add(info);
                }
            }
            return list;
        }


        /// <summary>
        /// 根据les工厂代码获取sap工厂代码
        /// </summary>
        /// <param name="sapPlantCode"></param>
        /// <returns></returns>
        public string GetSapPlantByPlantCode(string PlantCode)
        {
            string sql = "select [SAP_PLANT_CODE] from [LES].[TM_BAS_PLANT] with(nolock) where [VALID_FLAG] = 1 and [PLANT] =@PLANT_CODE";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PLANT_CODE", DbType.AnsiString, PlantCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
    }
}

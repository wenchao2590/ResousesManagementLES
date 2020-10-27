using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DM.LES;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.LES
{
    public  partial class WorkshopDAL
    {
        /// <summary>
        ///  获取工厂数据，用于比对SAP代码与LES代码之间关系
        /// </summary>
        /// <returns></returns>
        public List<WorkshopInfo> GetListForInterfaceDataSync()
        {
            string sql = "select [PLANT],[WORKSHOP],[WORKSHOP_NAME] "
            + "from [LES].[TM_BAS_WORKSHOP] with(nolock) "
            + "where [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WorkshopInfo> list = new List<WorkshopInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    WorkshopInfo info = new WorkshopInfo();
                    info.Plant = DBConvert.GetString(dr, dr.GetOrdinal("PLANT"));
                    info.Workshop = DBConvert.GetString(dr, dr.GetOrdinal("WORKSHOP"));
                    info.WorkshopName = DBConvert.GetString(dr, dr.GetOrdinal("WORKSHOP_NAME"));
                    list.Add(info);
                }
            }
            return list;
        }


    }
}

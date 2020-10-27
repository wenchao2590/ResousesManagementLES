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
    public partial class InterfaceConfigDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public InterfaceConfigInfo GetInfoByInterfaceCode(string interfaceCode)
        {
            string sql = "select * from dbo.[TS_SYS_INTERFACE_CONFIG] with(nolock) "
                + "where [VALID_FLAG] = 1 and [INTERFACE_CODE] = @INTERFACE_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@INTERFACE_CODE", DbType.AnsiString, interfaceCode);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateInterfaceConfigInfo(dr);
            }
            return null;
        }

        public List<InterfaceConfigInfo> GetListBySysname(string sysName)
        {
            string sql = "select * from dbo.[TS_SYS_INTERFACE_CONFIG] with(nolock) "
                + "where [VALID_FLAG] <> 0 and [SYS_NAME] = @SYS_NAME;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@SYS_NAME", DbType.AnsiString, sysName);
            List<InterfaceConfigInfo> list = new List<InterfaceConfigInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateInterfaceConfigInfo(dr));
                }
            }
            return list;
        }
    }
}

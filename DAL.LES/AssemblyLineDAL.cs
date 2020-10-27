
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
    /// AssemblyLineDAL对应表[TM_BAS_ASSEMBLY_LINE]
    /// </summary>
    public partial class AssemblyLineDAL
    {
        public AssemblyLineInfo GetInfoByAssemblyLine(string assemblyLine)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM [LES].[TM_BAS_ASSEMBLY_LINE] WHERE ASSEMBLY_LINE='{0}'", assemblyLine);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateAssemblyLineInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据SAP产线代码获取LES产线代码
        /// </summary>
        /// <param name="sapAssemblyLine"></param>
        /// <returns></returns>
        public string GetAssemblyLineBySapAssemblyLine(string sapAssemblyLine)
        {
            string sql = "select [ASSEMBLY_LINE] from [LES].[TM_BAS_ASSEMBLY_LINE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [SAP_ASSEMBLY_LINE] = @SAP_ASSEMBLY_LINE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SAP_ASSEMBLY_LINE", DbType.AnsiString, sapAssemblyLine);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 根据LES产线代码获取SAP产线代码
        /// </summary>
        /// <param name="assemblyLine"></param>
        /// <returns></returns>
        public string GetSapAssemblyLineByAssemblyLine(string assemblyLine)
        {
            string sql = "select [SAP_ASSEMBLY_LINE] from [LES].[TM_BAS_ASSEMBLY_LINE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [ASSEMBLY_LINE] = @ASSEMBLY_LINE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ASSEMBLY_LINE", DbType.AnsiString, assemblyLine);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 获取流水线数据，用于比对SAP代码与LES代码之间关系
        /// </summary>
        /// <returns></returns>
        public List<AssemblyLineInfo> GetListForInterfaceDataSync()
        {
            string sql = "select [PLANT],[WORKSHOP],[ASSEMBLY_LINE],[ASSEMBLY_LINE_NAME]"
            + "from [LES].[TM_BAS_ASSEMBLY_LINE] with(nolock) "
            + "where [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<AssemblyLineInfo> list = new List<AssemblyLineInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    AssemblyLineInfo info = new AssemblyLineInfo();
                    info.Plant = DBConvert.GetString(dr, dr.GetOrdinal("PLANT"));
                    info.Workshop = DBConvert.GetString(dr, dr.GetOrdinal("WORKSHOP"));
                    info.AssemblyLine = DBConvert.GetString(dr, dr.GetOrdinal("ASSEMBLY_LINE"));
                    info.AssemblyLineName = DBConvert.GetString(dr, dr.GetOrdinal("ASSEMBLY_LINE_NAME"));

                    list.Add(info);
                }
            }
            return list;
        }
    }
}

namespace DAL.LES
{
    using DAL.SYS;
    using DM.LES;
    using Infrustructure.Utilities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// OutputDAL
    /// </summary>
    public partial class OutputDAL
    {
        /// <summary>
        /// 根据出库单号获取对象
        /// </summary>
        /// <param name="outputNo"></param>
        /// <returns></returns>
        public OutputInfo GetInfo(string outputNo)
        {
            string sql = "select * from [LES].[TT_WMM_OUTPUT] with(nolock) " +
                "where [VALID_FLAG] = 1 and [OUTPUT_NO] =@OUTPUT_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OUTPUT_NO", DbType.AnsiString, outputNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateOutputInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据外键获取对象
        /// </summary>
        /// <param name="outputNo"></param>
        /// <returns></returns>
        public OutputInfo GetInfo(Guid fid)
        {
            string sql = "select * from [LES].[TT_WMM_OUTPUT] with(nolock) where [VALID_FLAG] = 1 and [FID] =@FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateOutputInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据单号获取ID
        /// </summary>
        /// <param name="outputNos"></param>
        /// <returns></returns>
        public List<string> GetRowsKeyValues(List<string> outputNos)
        {
            string sql = "select [ID] from [LES].[TT_WMM_OUTPUT] with(nolock) " +
                "where [OUTPUT_NO] in ('" + string.Join("','", outputNos.ToArray()) + "') and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<string> list = new List<string>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetInt64(dr, dr.GetOrdinal("ID")).ToString());
                }
            }
            return list;
        }

        public bool LogicDeleteInfo(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_OUTPUT] WITH(ROWLOCK) "
               + "set [VALID_FLAG] = N'false' ,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE() "
               + "WHERE [VALID_FLAG] = 1  AND ID in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }


    }
}

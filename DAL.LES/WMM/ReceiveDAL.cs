using DM.LES;
using Infrustructure.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class ReceiveDAL
    {
        /// <summary>
        /// Get ReceiveInfo
        /// </summary>
        /// <param name="RECEIVE_ID">ReceiveInfo Primary key </param>
        /// <returns></returns> 
        public ReceiveInfo GetInfo(Guid fid)
        {
            string sql = "select * from [LES].[TT_WMM_RECEIVE] with(nolock) where [VALID_FLAG] = 1 and [FID] =@FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreateReceiveInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// Get ReceiveInfo
        /// </summary>
        /// <param name="receiveNo"></param>
        /// <returns></returns>
        public ReceiveInfo GetInfo(string receiveNo)
        {
            string sql = "select * from [LES].[TT_WMM_RECEIVE] with(nolock) " +
                "where [VALID_FLAG] = 1 and [RECEIVE_NO] =@RECEIVE_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@RECEIVE_NO", DbType.AnsiString, receiveNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateReceiveInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 根据单号获取主键
        /// </summary>
        /// <param name="receiveNo"></param>
        /// <returns></returns>
        public long GetIdByReceiveNo(string receiveNo)
        {
            string sql = "select [ID] from [LES].[TT_WMM_RECEIVE] with(nolock) where [VALID_FLAG] = 1 and [RECEIVE_NO] =@RECEIVE_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@RECEIVE_NO", DbType.AnsiString, receiveNo);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return Convert.ToInt64(result.ToString());
        }
    }
}

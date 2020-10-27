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
    public partial class CodeItemDAL
    {

        public List<CodeItemInfo> GetListByCodeName(string codeName)
        {
            string sql = "select * from dbo.[TS_SYS_CODE_ITEM] with(nolock) "
                + "where [VALID_FLAG] <> 0 "
                + "and [CODE_FID] in (select [FID] from dbo.[TS_SYS_CODE] with(nolock) "
                + "where [VALID_FLAG] <> 0 and [CODE_NAME] = @CODE_NAME) "
                + "order by [DISPLAY_ORDER]";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CODE_NAME", DbType.AnsiString, codeName);
            List<CodeItemInfo> list = new List<CodeItemInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateCodeItemInfo(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据CodeName和ItemName获取对应的值
        /// </summary>
        /// <param name="codeName"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public int GetValueByCodeItemName(string codeName, string itemName)
        {
            string sql = "select [ITEM_VALUE] from dbo.[TS_SYS_CODE_ITEM] with(nolock) "
                + "where [VALID_FLAG] = 1 and [ITEM_NAME] = @ITEM_NAME "
                + "and [CODE_FID] in (select [FID] from dbo.[TS_SYS_CODE] with(nolock) "
                + "where [VALID_FLAG] = 1 and [CODE_NAME] = @CODE_NAME)";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ITEM_NAME", DbType.AnsiString, itemName);
            db.AddInParameter(cmd, "@CODE_NAME", DbType.AnsiString, codeName);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }
        /// <summary>
        /// 获取最大的ITEM_VALUE
        /// </summary>
        /// <param name="codeFid"></param>
        /// <returns></returns>
        public int GetMaxValue(Guid codeFid)
        {
            string sql = "select max([ITEM_VALUE]) from dbo.[TS_SYS_CODE_ITEM] with(nolock) " +
                "where [CODE_FID] = @CODE_FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CODE_FID", DbType.Guid, codeFid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }
    }
}

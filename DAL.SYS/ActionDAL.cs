using DM.SYS;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.SYS
{
    public partial class ActionDAL
    {
        /// <summary>
        /// ActionName是否存在
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public bool IsExistActionName(string actionName)
        {
            string sql = "select count(1) from dbo.[TS_SYS_ACTION] with(nolock) "
                + "where [ACTION_NAME] = @ACTION_NAME and [VALID_FLAG] <> 0 ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ACTION_NAME", DbType.AnsiString, actionName);
            object result = db.ExecuteScalar(dbCommand);
            if (result == null || result == DBNull.Value)
                return false;
            if (int.Parse(result.ToString()) == 0)
                return false;
            return true;
        }
        /// <summary>
        /// MENU_ACTION维护时获取菜单对应的功能项，及已配置的功能项
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<ActionInfo> GetListByPage(Guid menuFid, string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where a.[VALID_FLAG] = 1  " + textWhere;
                else
                    whereText += " where " + textWhere;
            }
            else
                whereText += " where a.[VALID_FLAG] = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "b.ACTION_ORDER desc,a.ID asc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",a.*"
                + ",case when b.VALID_FLAG =1 then  CONVERT(bit,1) else   CONVERT(bit,0) end  as  IS_RELATIONED "
                + ",ISNULL(b.ACTION_ORDER,0) as ACTION_ORDER"
                + ",b.CLIENT_JS"
                + ",b.NEED_AUTH "
                + ",b.DETAIL_FLAG "
                + "from dbo.TS_SYS_ACTION a with(nolock) "
                + "left join dbo.TS_SYS_MENU_ACTION b with(nolock) "
                + "on a.FID = b.ACTION_FID and b.MENU_FID = @MENU_FID and b.VALID_FLAG=1 " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MENU_FID", DbType.Guid, menuFid);
            List<ActionInfo> list = new List<ActionInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    ActionInfo info = CreateActionInfo(dr);
                    info.IsRelationed = DBConvert.GetBool(dr, dr.GetOrdinal("IS_RELATIONED"));
                    info.DisplayOrder = DBConvert.GetInt32(dr, dr.GetOrdinal("ACTION_ORDER"));
                    info.ClientJs = DBConvert.GetString(dr, dr.GetOrdinal("CLIENT_JS"));
                    info.NeedAuth = DBConvert.GetBool(dr, dr.GetOrdinal("NEED_AUTH"));
                    info.DetailFlag = DBConvert.GetBool(dr, dr.GetOrdinal("DETAIL_FLAG"));
                    list.Add(info);
                }
            }
            return list;
        }


        public DataTable GetMenuActionByUser(string textWhere)
        {
            string sql = @"SELECT MENU_ID = XC.[ID],ACTION_NAME = XA.[ACTION_NAME] FROM dbo.TS_SYS_ACTION AS XA
                           LEFT JOIN [dbo].[TS_SYS_MENU_ACTION] AS XB ON XA.[FID] = XB.[ACTION_FID]
                           LEFT JOIN [dbo].[TS_SYS_MENU] AS XC ON XB.[MENU_FID] = XC.[FID]
                           WHERE XA.[VALID_FLAG] = 1 AND XB.[VALID_FLAG] = 1 AND XC.[VALID_FLAG] = 1 AND {0} ;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(string.Format(sql, textWhere));
            return db.ExecuteDataTable(cmd);
        }
    }
}


using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public partial class RangeAuthBLL
    {
        /// <summary>
        /// GetRangeAuthList
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="conditionFid"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<RangeAuthInfo> GetRangeAuthList(Guid userFid, Guid roleFid, Guid conditionFid, string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = 0;
            RangeAuthConditionInfo rangeAuthConditionInfo = new RangeAuthConditionDAL().GetInfo(conditionFid);
            if (rangeAuthConditionInfo == null)
                return new List<RangeAuthInfo>();
            List<UserRoleRangeAuthInfo> userRoleRangeAuthInfos = new UserRoleRangeAuthDAL().GetList("" +
                "[USER_FID] = N'" + userFid + "' and " +
                "[ROLE_FID] = N'" + roleFid + "' and " +
                "[CONDITION_FID] = N'" + conditionFid + "'", string.Empty);
            ///
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where [VALID_FLAG] = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and [VALID_FLAG] = 1";
            }
            else
                whereText += " where [VALID_FLAG] = 1 ";
            ///
            whereText = whereText.Replace("[ConditionFieldValue]", "[" + rangeAuthConditionInfo.FieldName + "]");
            whereText = whereText.Replace("[ConditionFieldDisplay]", "[" + rangeAuthConditionInfo.DisplayFieldName + "]");
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[ID] desc";
            ///DATA_CNT
            object cnt = CommonDAL.ExecuteScalar("select count(1) from " + rangeAuthConditionInfo.TableName + " with(nolock) " + whereText + ";");
            if (cnt == null || cnt == DBNull.Value) dataCount = 0;
            dataCount = Convert.ToInt32(cnt);
            ///DATA
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from " + rangeAuthConditionInfo.TableName + "  with(nolock) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            List<RangeAuthInfo> rangeAuthInfos = new List<RangeAuthInfo>();
            foreach (DataRow dr in dataTable.Rows)
            {
                RangeAuthInfo rangeAuthInfo = new RangeAuthInfo();
                rangeAuthInfo.Id = Convert.ToInt64(dr["ID"]);
                rangeAuthInfo.RoleFid = roleFid;
                rangeAuthInfo.ConditionFid = conditionFid;
                rangeAuthInfo.ConditionFieldValue = dr[rangeAuthConditionInfo.FieldName].ToString();
                rangeAuthInfo.ConditionFieldDisplay = dr[rangeAuthConditionInfo.DisplayFieldName].ToString();
                UserRoleRangeAuthInfo userRoleRangeAuthInfo = userRoleRangeAuthInfos.FirstOrDefault(d => d.ConditionContext == rangeAuthInfo.ConditionFieldValue);
                if (userRoleRangeAuthInfo == null) userRoleRangeAuthInfo = userRoleRangeAuthInfos.FirstOrDefault(d => d.ConditionContext == "1=1");
                rangeAuthInfo.AuthedFlag = userRoleRangeAuthInfo == null ? false : true;
                rangeAuthInfo.Comments = userRoleRangeAuthInfo == null ? string.Empty : userRoleRangeAuthInfo.Comments;
                rangeAuthInfos.Add(rangeAuthInfo);
            }
            return rangeAuthInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="conditionFid"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SetRangeAuth(string[] ids, Guid userFid, Guid roleFid, Guid conditionFid, string loginUser)
        {
            RangeAuthConditionInfo rangeAuthConditionInfo = new RangeAuthConditionDAL().GetInfo(conditionFid);
            if (rangeAuthConditionInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (ids.Length == 0)
            {
                return CommonDAL.ExecuteNonQueryBySql("insert into dbo.[TS_SYS_USER_ROLE_RANGE_AUTH] (" +
                    "FID, USER_FID, ROLE_FID, CONDITION_FID, CONDITION_CONTEXT, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) values (" +
                    "NEWID(), N'" + userFid + "', N'" + roleFid + "', N'" + conditionFid + "', N'1=1', N'', 1, N'" + loginUser + "', GETDATE());");
            }
            List<UserRoleRangeAuthInfo> userRoleRangeAuthInfos = new UserRoleRangeAuthDAL().GetList("" +
                    "[USER_FID] = N'" + userFid + "' and " +
                    "[ROLE_FID] = N'" + roleFid + "' and " +
                    "[CONDITION_FID] = N'" + conditionFid + "'", string.Empty);
            string sql = "select " + rangeAuthConditionInfo.FieldName + " from " + rangeAuthConditionInfo.TableName + " with(nolock) where [VALID_FLAG] = 1 and [ID] in (" + string.Join(",", ids) + ");";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            string insertSql = string.Empty;
            UserRoleRangeAuthInfo userRoleRangeAuthInfo = userRoleRangeAuthInfos.FirstOrDefault(d => d.ConditionContext == "1=1");
            if (userRoleRangeAuthInfo != null)
                insertSql += "update dbo.[TS_SYS_USER_ROLE_RANGE_AUTH] " +
                    "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [ID] = " + userRoleRangeAuthInfo.Id + ";";
            foreach (DataRow dr in dataTable.Rows)
            {
                string conditionContext = dr[rangeAuthConditionInfo.FieldName].ToString();
                userRoleRangeAuthInfo = userRoleRangeAuthInfos.FirstOrDefault(d => d.ConditionContext == conditionContext);
                if (userRoleRangeAuthInfo != null) continue;
                insertSql += "insert into dbo.[TS_SYS_USER_ROLE_RANGE_AUTH] (" +
                    "FID, USER_FID, ROLE_FID, CONDITION_FID, CONDITION_CONTEXT, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) values (" +
                    "NEWID(), N'" + userFid + "', N'" + roleFid + "', N'" + conditionFid + "', N'" + conditionContext + "', N'', 1, N'" + loginUser + "', GETDATE());";
            }
            if (insertSql.Length == 0) return true;
            return CommonDAL.ExecuteNonQueryBySql(insertSql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="conditionFid"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool DelRangeAuth(string[] ids, Guid userFid, Guid roleFid, Guid conditionFid, string loginUser)
        {
            RangeAuthConditionInfo rangeAuthConditionInfo = new RangeAuthConditionDAL().GetInfo(conditionFid);
            if (rangeAuthConditionInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (ids.Length == 0)
            {
                CommonDAL.ExecuteNonQueryBySql("update dbo.[TS_SYS_USER_ROLE_RANGE_AUTH] " +
                    "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [USER_FID] = N'" + userFid + "' and [ROLE_FID] = N'" + roleFid + "' and [CONDITION_FID] = N'" + conditionFid + "' and [VALID_FLAG] = 1;");
                return true;
            }
            List<UserRoleRangeAuthInfo> userRoleRangeAuthInfos = new UserRoleRangeAuthDAL().GetList("" +
                    "[USER_FID] = N'" + userFid + "' and " +
                    "[ROLE_FID] = N'" + roleFid + "' and " +
                    "[CONDITION_FID] = N'" + conditionFid + "'", string.Empty);
            string sql = "select " + rangeAuthConditionInfo.FieldName + " from " + rangeAuthConditionInfo.TableName + " with(nolock) where [VALID_FLAG] = 1 and [ID] in (" + string.Join(",", ids) + ");";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            string deleteSql = string.Empty;
            foreach (DataRow dr in dataTable.Rows)
            {
                string conditionContext = dr[rangeAuthConditionInfo.FieldName].ToString();
                UserRoleRangeAuthInfo userRoleRangeAuthInfo = userRoleRangeAuthInfos.FirstOrDefault(d => d.ConditionContext == conditionContext);
                if (userRoleRangeAuthInfo == null) continue;
                deleteSql += "update dbo.[TS_SYS_USER_ROLE_RANGE_AUTH] " +
                    "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [ID] = " + userRoleRangeAuthInfo.Id + ";";
            }
            if (deleteSql.Length == 0) return true;
            return CommonDAL.ExecuteNonQueryBySql(deleteSql);
        }
    }
}

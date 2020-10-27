using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public partial class OutputBLL
    {
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select  r.*,o.[NAME] as DEPT,c.[ITEM_NAME] as OUTPUT_TYPE_NAME,w.[WAREHOUSE_NAME],c2.[ITEM_NAME] as COST_CENTER_NAME "
                + "from LES.TT_WMM_OUTPUT r with(nolock) "
                + "left join TS_SYS_ORGANIZATION o with(nolock) on o.[FID] = r.[ORGANIZATION_FID] and o.[VALID_FLAG] = 1 "
                + "left join TS_SYS_CODE_ITEM c with(nolock) on c.[ITEM_VALUE] = r.[OUTPUT_TYPE] and c.[CODE_FID] = N'D3A126DC-E622-4760-8517-43C286E959BC' and c.[VALID_FLAG] = 1 "
                + "left join TS_SYS_CODE_ITEM c2 with(nolock) on c2.[ITEM_NAME_EN] = r.[COST_CENTER] and c2.[CODE_FID] = N'06FD3011-A967-40D2-8337-A80B78D9C137' and c2.[VALID_FLAG] = 1 "
                + "left join LES.TM_BAS_WAREHOUSE w with(nolock) on w.[WAREHOUSE] = r.[WM_NO] and w.[VALID_FLAG] = 1 "
                + "where r.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and r.[VALID_FLAG] = 1;"
                + "select * from LES.TT_WMM_OUTPUT_DETAIL with(nolock) "
                + "where [OUTPUT_FID] in (select [FID] from LES.TT_WMM_OUTPUT with(nolock) "
                + "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and [VALID_FLAG] = 1 ) and [VALID_FLAG] = 1;";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SubmitInfo(long id, string loginUser)
        {
            ///一般手工创建出库单会用到此功能，出库单必须为10.已创建状态
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000128");///出库单状态必须为已创建才能提交

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Published + ",[CONFIRM_USER] = N'" + loginUser + "' ,[CONFIRM_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 撤销提交
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoSubmitInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Published + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000130");///已提交的出库单才能撤销提交

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Created + ",[CONFIRM_USER] = NULL ,[CONFIRM_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ConfirmInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Published + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000131");///已提交的出库单才能进行确认操作

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Completed + ",[LIABLE_USER] = N'" + loginUser + "' ,[LIABLE_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            string sql = GetTranDetailsInsertSql(id, (int)WmmTranTypeConstants.Outbound, loginUser);
            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (!string.IsNullOrEmpty(sql))
                    DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 撤销确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoConfirmInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Completed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000132");///已确认的出库单才能撤销确认

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Published + ",[LIABLE_USER] = NULL ,[LIABLE_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            string sql = GetTranDetailsInsertSql(id, (int)WmmTranTypeConstants.UndoOutbound, loginUser);
            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (!string.IsNullOrEmpty(sql))
                    DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool VerifyInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Completed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000133");///已确认的出库单才能进行审核操作

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[FINANCE_USER] = N'" + loginUser + "' ,[FINANCE_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoVerifyInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Closed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000136");///已审核的出库单才能撤销审核

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Completed + ",[FINANCE_USER] = NULL ,[FINANCE_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
    }
}

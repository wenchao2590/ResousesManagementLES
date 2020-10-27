using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// BusinessExpenseOutBLL
    /// </summary>
    public partial class BusinessExpenseOutBLL
    {
        #region Common
        /// <summary>
        /// BusinessExpenseOutDAL
        /// </summary>
        BusinessExpenseOutDAL dal = new BusinessExpenseOutDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BusinessExpenseOutInfo></returns>
        public List<BusinessExpenseOutInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessExpenseOutInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(BusinessExpenseOutInfo info)
        {
            if (info.SourceBusinessFid == null)
            {
                info.SourceBusinessFid = new PlanPullOrderDAL().GetFid(info.OrderNo);
                info.SourceBusinessNo = info.OrderNo;
            }
            ///获取费用类型
            if (!string.IsNullOrEmpty(info.ExpenseCode))
                info.ExpenseType = new ExpenseItemDAL().GetExpenseType(info.ExpenseCode);
            ///流程标记默认为false，否则检索条件不能成功检索
            info.CheckFlag = false;///对账
            info.PaymentFlag = false;///销账
            info.ApprovalFlag = false;///审核

            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            BusinessExpenseOutInfo info = dal.GetInfo(id);
            if (info.CheckFlag.GetValueOrDefault())
                throw new Exception("MC:0x00000329");///已对账的费用不允许删除
            if (info.PaymentFlag.GetValueOrDefault())
                throw new Exception("MC:0x00000346");///已销账的费用不允许删除
            if (info.ApprovalFlag.GetValueOrDefault())
                throw new Exception("MC:0x00000347");///已审核的费用不允许删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            BusinessExpenseOutInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.ApprovalFlag.GetValueOrDefault())
                throw new Exception("MC:0x00000348");///已审核不允许进行修改

            ///销账->审核之间
            if (info.PaymentFlag.GetValueOrDefault())
            {
                ///审批编号
                string approvalNo = CommonBLL.GetFieldValue(fields, "APPROVAL_NO");
                if (string.IsNullOrEmpty(approvalNo)) approvalNo = string.Empty;
                ///
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
                return dal.UpdateInfo("[APPROVAL_NO] = N'" + approvalNo + "',[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "'", id) > 0 ? true : false;
            }
            ///对账->销账之间
            if (info.CheckFlag.GetValueOrDefault())
            {
                ///实付金额
                string actualAmount = CommonBLL.GetFieldValue(fields, "ACTUAL_AMOUNT");
                if (string.IsNullOrEmpty(actualAmount)) actualAmount = "NULL";
                ///财务凭证号
                string fiDocNo = CommonBLL.GetFieldValue(fields, "FI_DOC_NO");
                if (string.IsNullOrEmpty(fiDocNo)) fiDocNo = string.Empty;
                ///财务凭证号
                string noInvoiceFlag = CommonBLL.GetFieldValue(fields, "NO_INVOICE_FLAG");
                if (string.IsNullOrEmpty(noInvoiceFlag)) noInvoiceFlag = "0";
                ///审批编号
                string approvalNo = CommonBLL.GetFieldValue(fields, "APPROVAL_NO");
                if (string.IsNullOrEmpty(approvalNo)) approvalNo = string.Empty;
                ///
                string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
                return dal.UpdateInfo("" +
                    "[ACTUAL_AMOUNT] = " + actualAmount + "," +
                    "[FI_DOC_NO] = N'" + fiDocNo + "'," +
                    "[APPROVAL_NO] = N'" + approvalNo + "'," +
                    "[NO_INVOICE_FLAG] = " + noInvoiceFlag + "," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "'", id) > 0 ? true : false;
            }
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ConfirmInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                if (businessExpenseOutInfo.CheckFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000358");///已开票不能重复进行对账操作

                if (businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000359");///已销账不能进行对账操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000360");///已审核不能进行对账操作

                ///TODO:是否增加对账时发票号是否为空校验标记
                if (businessExpenseOutInfo.InvoiceNo == null)
                    throw new Exception("MC:0x00000352");///开票时发票号为必填项
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] " +
                "set [CHECK_FLAG] = 1,[CHECK_DATE] = GETDATE(),[CHECK_USER] = N'" + loginUser + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 销账
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                ///不开票收款时则不需要是否开票

                if (businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000353");///已销账不能重复进行销账操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000354");///已审核不能进行销账操作

                ///TODO:增加确认时直接将实收款等于应付款数额的系统配置标记
                if (businessExpenseOutInfo.ActualAmount == null)
                    throw new Exception("MC:0x00000355");///未录入实收不能进行销账操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] " +
                "set [PAYMENT_FLAG] = 1,[PAYMENT_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool VerifyInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                ///不开票收款时则不需要是否开票

                if (!businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000356");///未销账不能进行审核操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000357");///已审核不能重复进行审核操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                "[APPROVAL_FLAG] = 1,[APPROVE_DATE] = GETDATE(),[APPROVAL_USER] = N'" + loginUser + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            string sql = string.Empty;
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                ///撤销审核
                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                {
                    sql += "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                                "[APPROVAL_FLAG] = 0," +
                                "[APPROVE_DATE] = NULL," +
                                "[APPROVAL_USER] = NULL," +
                                "[APPROVAL_NO] = NULL," +
                                "[MODIFY_USER] = N'" + loginUser + "'," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] = " + businessExpenseOutInfo.Id + ";";
                    continue;
                }
                ///撤销销账
                if (businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                {
                    sql += "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                                "[PAYMENT_FLAG] = 0," +
                                "[PAYMENT_DATE] = NULL," +
                                "[PAYMENT_USER] = NULL," +
                                "[ACTUAL_AMOUNT] = NULL," +
                                "[FI_DOC_NO] = NULL," +
                                "[NO_INVOICE_FLAG] = NULL," +
                                "[MODIFY_USER] = N'" + loginUser + "'," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] = " + businessExpenseOutInfo.Id + ";";
                    continue;
                }
                ///撤销对账
                if (businessExpenseOutInfo.CheckFlag.GetValueOrDefault())
                {
                    sql += "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                                "[CHECK_FLAG] = 0," +
                                "[CHECK_DATE] = NULL," +
                                "[CHECK_USER] = NULL," +
                                "[CHECK_NO] = NULL," +
                                "[INVOICE_NO] = NULL," +
                                "[MODIFY_USER] = N'" + loginUser + "'," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] = " + businessExpenseOutInfo.Id + ";";
                    continue;
                }
            }
            ///
            using (var trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            ///
            return true;
        }
    }
}


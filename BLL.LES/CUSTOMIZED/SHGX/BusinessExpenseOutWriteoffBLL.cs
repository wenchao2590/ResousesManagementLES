namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseOutWriteoffBLL
    /// </summary>
    public partial class BusinessExpenseOutWriteoffBLL
    {
        /// <summary>
        /// 销账
        /// </summary>
        /// <param name="info"></param>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityWriteoffInfos(BusinessExpenseOutWriteoffInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = new BusinessExpenseOutDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///应付合计
            decimal amount = 0;
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                ///无票销账标记
                if (!info.NoInvoiceFlag.GetValueOrDefault())
                {
                    if (!businessExpenseOutInfo.CheckFlag.GetValueOrDefault())
                        throw new Exception("MC:0x00000382");///未对账不能进行销账操作
                }
                if (businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000353");///已销账不能重复进行销账操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000354");///已审核不能进行销账操作
            }
            string amountSql = string.Empty;
            ///未填写or填写金额与应付一致时按照应付金额进行销账
            if (info.ActualAmount == null || info.ActualAmount == amount)
                amountSql = "[ACTUAL_AMOUNT] = isnull([ACTUAL_AMOUNT],[AMOUNT]),";
            else
            {
                if (businessExpenseOutInfos.Count > 1)
                    throw new Exception("MC:0x00000391");///修改实付只能选中一条数据
                if (rowsKeyValues.Count > 1)
                    throw new Exception("MC:0x00000391");///修改实付只能选中一条数据
                amountSql = "[ACTUAL_AMOUNT] = " + info.ActualAmount.GetValueOrDefault() + ",";
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                "[PAYMENT_FLAG] = 1," +
                "[PAYMENT_DATE] = " + (info.PaymentDate == null ? "GETDATE()" : "N'" + info.PaymentDate.GetValueOrDefault() + "'") + "," +
                "[PAYMENT_USER] = N'" + loginUser + "'," +
                amountSql +
                "[FI_DOC_NO] = N'" + info.FiDocNo + "'," +
                "[NO_INVOICE_FLAG] = " + (info.NoInvoiceFlag.GetValueOrDefault() ? 1 : 0) + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

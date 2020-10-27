namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseInWriteoffBLL
    /// </summary>
    public partial class BusinessExpenseInWriteoffBLL
    {
        /// <summary>
        /// 销账(Write Off)
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityWriteoffInfos(BusinessExpenseInWriteoffInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseInInfo> businessExpenseInInfos = new BusinessExpenseInDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseInInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///应收合计
            decimal amount = 0;
            foreach (var businessExpenseInInfo in businessExpenseInInfos)
            {
                if (!businessExpenseInInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000385");///未审核不能进行销账操作

                ///无票销账标记
                if (!info.NoInvoiceFlag.GetValueOrDefault())
                {
                    if (!businessExpenseInInfo.CheckFlag.GetValueOrDefault())
                        throw new Exception("MC:0x00000386");///未开票不能进行销账操作
                }
                if (businessExpenseInInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000353");///已销账不能重复进行销账操作

                ///累加
                amount += businessExpenseInInfo.Amount.GetValueOrDefault();
            }
            string amountSql = string.Empty;
            ///未填写or填写金额与应收一致时按照应收金额进行销账
            if (info.ActualAmount == null || info.ActualAmount == amount)
                amountSql = "[ACTUAL_AMOUNT] = isnull([ACTUAL_AMOUNT],[AMOUNT]),";
            else
            {
                if (businessExpenseInInfos.Count > 1)
                    throw new Exception("MC:0x00000387");///修改实收只能选中一条数据
                if (rowsKeyValues.Count > 1)
                    throw new Exception("MC:0x00000387");///修改实收只能选中一条数据
                amountSql = "[ACTUAL_AMOUNT] = " + info.ActualAmount.GetValueOrDefault() + ",";
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_IN] set " +
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

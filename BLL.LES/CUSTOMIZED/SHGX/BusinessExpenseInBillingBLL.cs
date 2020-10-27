namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseInBillingBLL
    /// </summary>
    public partial class BusinessExpenseInBillingBLL
    {
        /// <summary>
        /// 开票
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityBillingInfos(BusinessExpenseInBillingInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseInInfo> businessExpenseInInfos = new BusinessExpenseInDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseInInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseInInfo in businessExpenseInInfos)
            {
                if (!businessExpenseInInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000388");///未审核不能进行开票操作

                if (businessExpenseInInfo.CheckFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000349");///已开票不能重复进行开票操作

                ///当时为无票销账状态的情况下可以再次进行开票
                if (businessExpenseInInfo.PaymentFlag.GetValueOrDefault() &&
                    !businessExpenseInInfo.NoInvoiceFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000350");///已销账不能进行开票操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_IN] set " +
                "[CHECK_FLAG] = 1," +
                "[CHECK_DATE] = " + (info.CheckDate == null ? "GETDATE()" : "N'" + info.CheckDate.GetValueOrDefault() + "'") + "," +
                "[CHECK_USER] = N'" + loginUser + "'," +
                "[INVOICE_NO] = N'" + info.InvoiceNo + "'," +
                "[INVOICE_TITLE] = N'" + info.InvoiceTitle + "'," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

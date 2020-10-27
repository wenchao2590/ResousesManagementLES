namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseOutCheckBLL
    /// </summary>
    public partial class BusinessExpenseOutCheckBLL
    {
        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="info"></param>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityCheckInfos(BusinessExpenseOutCheckInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = new BusinessExpenseOutDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                if (businessExpenseOutInfo.CheckFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000358");///已对账不能重复进行对账操作

                ///当时为无票销账状态的情况下可以再次进行对账
                if (businessExpenseOutInfo.PaymentFlag.GetValueOrDefault() &&
                    !businessExpenseOutInfo.NoInvoiceFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000359");///已销账不能进行对账操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000360");///已审核不能进行对账操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                "[CHECK_FLAG] = 1," +
                "[CHECK_DATE] = " + (info.CheckDate == null ? "GETDATE()" : "N'" + info.CheckDate.GetValueOrDefault() + "'") + "," +
                "[CHECK_USER] = N'" + loginUser + "'," +
                "[CHECK_NO] = N'" + info.CheckNo + "'," +
                "[INVOICE_NO] = N'" + info.InvoiceNo + "'," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

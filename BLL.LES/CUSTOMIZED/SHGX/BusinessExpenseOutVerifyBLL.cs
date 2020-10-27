namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseOutVerifyBLL
    /// </summary>
    public partial class BusinessExpenseOutVerifyBLL
    {
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="info"></param>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityVerifyInfos(BusinessExpenseOutVerifyInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseOutInfo> businessExpenseOutInfos = new BusinessExpenseOutDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseOutInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseOutInfo in businessExpenseOutInfos)
            {
                ///无票销账标记
                if (!info.NoInvoiceFlag.GetValueOrDefault())
                {
                    if (!businessExpenseOutInfo.CheckFlag.GetValueOrDefault())
                        throw new Exception("MC:0x00000383");///未对账不能进行审核操作
                }
                if (!businessExpenseOutInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000356");///未销账不能进行审核操作

                if (businessExpenseOutInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000357");///已审核不能重复进行审核操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_OUT] set " +
                "[APPROVAL_FLAG] = 1," +
                "[APPROVE_DATE] = " + (info.ApproveDate == null ? "GETDATE()" : "N'" + info.ApproveDate.GetValueOrDefault() + "'") + "," +
                "[APPROVAL_USER] = N'" + loginUser + "'," +
                "[APPROVAL_NO] = N'" + info.ApprovalNo + "'," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            ///
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

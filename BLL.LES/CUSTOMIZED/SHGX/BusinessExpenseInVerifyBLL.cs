namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// BusinessExpenseInVerifyBLL
    /// </summary>
    public partial class BusinessExpenseInVerifyBLL
    {
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EntityVerifyInfos(BusinessExpenseInVerifyInfo info, List<string> rowsKeyValues, string loginUser)
        {
            List<BusinessExpenseInInfo> businessExpenseInInfos = new BusinessExpenseInDAL().GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (businessExpenseInInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            foreach (var businessExpenseInInfo in businessExpenseInInfos)
            {
                if (businessExpenseInInfo.ApprovalFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000357");///已审核不能重复进行审核操作

                if (businessExpenseInInfo.CheckFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000389");///已开票不能进行审核操作

                if (businessExpenseInInfo.PaymentFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000390");///已销账不能进行审核操作
            }
            string sql = "update [LES].[TT_FIM_BUSINESS_EXPENSE_IN] set " +
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

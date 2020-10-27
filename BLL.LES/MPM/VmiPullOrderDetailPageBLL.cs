namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// VmiPullOrderDetailPageBLL
    /// </summary>
    public partial class VmiPullOrderDetailPageBLL
    {
        #region Common
        /// <summary>
        /// VmiPullOrderDetailDAL
        /// </summary>
        VmiPullOrderDetailDAL dal = new VmiPullOrderDetailDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdPullOrderDetailInfo></returns>
        public List<VmiPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            textWhere += "and isnull([ASN_CONFIRM_QTY],0) < isnull([REQUIRED_PART_QTY],0) " +
                "and [ORDER_FID] in (select [FID] from [LES].[TT_MPM_VMI_PULL_ORDER] with(nolock) " +
                "where [VALID_FLAG] = 1 and [ORDER_STATUS] = " + (int)PullOrderStatusConstants.Released + ")";
            dataCount = dal.GetCounts(textWhere);
            List<VmiPullOrderDetailInfo> vmiPullOrderDetailInfos = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            ///
            string vmi_asn_default_calculate_asn_qty = new ConfigDAL().GetValueByCode("VMI_ASN_DEFAULT_CALCULATE_ASN_QTY");
            if (!string.IsNullOrEmpty(vmi_asn_default_calculate_asn_qty) && vmi_asn_default_calculate_asn_qty.ToLower() == "true")
            {
                ///默认预发货数量
                vmiPullOrderDetailInfos.ForEach(delegate (VmiPullOrderDetailInfo info)
                {
                    info.AsnQty = info.RequiredPartQty.GetValueOrDefault() - info.AsnDraftQty.GetValueOrDefault() - info.AsnConfirmQty.GetValueOrDefault();
                });
            }
            return vmiPullOrderDetailInfos;
        }

        public VmiPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(VmiPullOrderDetailInfo info)
        {
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

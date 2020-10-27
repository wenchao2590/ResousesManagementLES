namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// VmiPullOrderDetailBLL
    /// </summary>
    public partial class VmiPullOrderDetailBLL
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
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
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

        #region Asn
        /// <summary>
        /// 预发货
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool AsnInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<VmiPullOrderDetailInfo> vmiPullOrderDetailInfos = new List<VmiPullOrderDetailInfo>();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                string[] keyValues = rowsKeyValue.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValues.Length == 0)
                    throw new Exception("MC:0x00000084");///数据错误 
                if (keyValues.Length == 1)
                    throw new Exception("MC:0x00000496");///预发货数量不能为空

                VmiPullOrderDetailInfo vmiPullOrderDetailInfo = new VmiPullOrderDetailInfo();
                vmiPullOrderDetailInfo.Id = Convert.ToInt64(keyValues[0]);
                vmiPullOrderDetailInfo.AsnQty = Convert.ToDecimal(keyValues[1]);
                vmiPullOrderDetailInfos.Add(vmiPullOrderDetailInfo);
            }
            VmiShippingPartBLL.AddCartVmiShippingPartInfo(vmiPullOrderDetailInfos, loginUser);
            return true;
        }
        #endregion

        #region Private
        /// <summary>
        /// 根据物料号获取VMI拉动单明细
        /// </summary>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public List<VmiPullOrderDetailViewInfo> GetVmiPullOrderDetailListByPartNo(string partNo)
        {
            string textWhere = "(isnull([REQUIRED_PART_QTY],0) - isnull([ASN_DRAFT_QTY],0) - isnull([ASN_CONFIRM_QTY],0)) > 0 ";
            if (!string.IsNullOrEmpty(partNo))
                textWhere += "and [PART_NO] = N'" + partNo + "' ";
            List<VmiPullOrderDetailViewInfo> vmiPullOrderDetailViewInfos = new VmiPullOrderDetailViewDAL().GetList(textWhere, string.Empty);
            if (vmiPullOrderDetailViewInfos.Count == 0)
                throw new Exception("MC:0x00000498");///没有编辑的拉动单

            return vmiPullOrderDetailViewInfos;
        }
        #endregion
    }
}

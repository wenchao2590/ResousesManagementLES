using DAL.LES;
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
    public partial class ReceiveBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetBarcodeReceivePrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select b.*,r.[CONTACTS_CODE],r.[CONTACTS2_CODE] " +
                "from [LES].[TT_WMM_BARCODE] b with(nolock) " +
                "left join [LES].[TT_WMM_RECEIVE_DETAIL] d with(nolock) on d.[FID] = b.[CREATE_SOURCE_FID] " +
                "left join [LES].[TT_WMM_RECEIVE] r with(nolock) on r.[FID] = d.[RECEIVE_FID] " +
                "where r.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and b.[VALID_FLAG] = 1;";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单
            List<ReceiveInfo> receiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (receiveInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            //int cnt = receiveInfos.Count(d => d.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Completed);
            //if (cnt > 0)
            //    throw new Exception("MC:0x00000344");///状态为已完成时才能进行发货
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (receiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<BarcodeInfo> barcodeInfos = new BarcodeDAL().GetList("" +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Inbound + "", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            string sql = string.Empty;
            foreach (var receiveInfo in receiveInfos)
            {
                List<ReceiveDetailInfo> receiveDetails = receiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).ToList();
                if (receiveDetails.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => receiveDetails.Select(r => r.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
                if (barcodes.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误
                sql += OutputBLL.CreateOutputByReceiveSql(
                    receiveInfo,///入库单
                    receiveDetails,///入库单明细
                    barcodes,///标签
                    string.Empty,///目标仓库
                    string.Empty,///目标存储区
                    null,///出库类型
                    loginUser,///操作用户
                    //receiveInfo.OrganizationFid,///操作机构
                    Guid.NewGuid(),
                    string.Empty,///承运人
                    string.Empty,///联系电话
                    null,///计划发货时间
                    null);///计划到达时间
                sql += "update [LES].[TT_WMM_RECEIVE] " +
                    "set [STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + receiveInfo.ReceiveId + ";";
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }

    }
}

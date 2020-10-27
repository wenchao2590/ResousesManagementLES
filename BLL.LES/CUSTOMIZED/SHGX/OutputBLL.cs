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
    public partial class OutputBLL
    {
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (outputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            //int cnt = outputInfos.Count(d => d.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Closed);
            //if (cnt > 0)
            //    throw new Exception("MC:0x00000342");///状态为已关闭时才能进行发货
            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] in ('" + string.Join("','", outputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (outputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<BarcodeInfo> barcodeInfos = new BarcodeDAL().GetList("" +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", outputDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Inbound + "", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            string sql = string.Empty;
            foreach (var outputInfo in outputInfos)
            {
                List<OutputDetailInfo> outputDetails = outputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == outputInfo.Fid.GetValueOrDefault()).ToList();
                if (outputDetails.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => outputDetails.Select(r => r.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
                if (barcodes.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误
                sql += CreateOutputByOutputSql(
                    outputInfo,
                    outputDetails,
                    barcodes,
                    "",
                    "",
                    null,
                    loginUser,
                    outputInfo.Fid,

                    "",
                    "",
                    null,
                    null);
                sql += "update [LES].[TT_WMM_OUTPUT] " +
                    "set [STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + outputInfo.OutputId + ";";
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
        #region Cancel
        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (outputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            //if (outputInfos.Count(d => d.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published) > 0)
            //    throw new Exception("MC:0x00000457");///状态必须为已发布

            ///入库单明细
            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] in ('" + string.Join("','", outputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (outputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            int cnt = new BarcodeDAL().GetCounts("" +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", outputDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.PickedUp + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000458");///条码已被扫描无法撤销

            string sql = "update [LES].[TT_WMM_OUTPUT] set " +
                                "[STATUS] = " + (int)WmmOrderStatusConstants.Created + "," +
                                "[MODIFY_USER] = N'" + loginUser + "' ," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            sql += "update [LES].[TT_WMM_OUTPUT_DETAIL] set " +
                        "[ROW_NO] = NULL," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' where " +
                        "[ID] in (" + string.Join(",", outputDetailInfos.Select(d => d.Id).ToArray()) + ");";
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion
    }
}

namespace WS.SRM.SyncInboundService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using BLL.LES;
    using DM.SYS;
    using System.Transactions;
    /// <summary>
    /// 送货单->RDC入库单
    /// </summary>
    public class SyncDeliveryNoteBLL
    {
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="loginUser"></param>
        public static void Sync(string loginUser)
        {
            ///获取没有处理的送货单数据
            List<SrmDeliveryNoteInfo> srmDeliveryNoteInfos = new SrmDeliveryNoteBLL().GetList("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmDeliveryNoteInfos.Count == 0) return;
            ///获取没有处理的送货单详情数据- 没有数据主表报错
            List<SrmDeliveryNoteDetailInfo> srmDeliveryNoteDetailInfos = new SrmDeliveryNoteDetailBLL().GetList("[ORDER_FID] in ('" + string.Join("','", srmDeliveryNoteInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (srmDeliveryNoteDetailInfos.Count == 0) return;
            ///获取相关物料供应商信息
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", srmDeliveryNoteInfos.Select(d => d.Suppliercode).ToArray()) + "') and " +
                "[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.MaterialSupplier + "", string.Empty);
            ///
            StringBuilder @string = new StringBuilder();
            if (supplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000475' where " +///送货单中供应商信息无效
                        "[ID] in (" + string.Join(",", srmDeliveryNoteInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取相关存储区信息
            List<string> zoneNos = srmDeliveryNoteInfos.Where(d => !string.IsNullOrEmpty(d.Targetzoneno)).Select(d => d.Targetzoneno).ToList();
            zoneNos.AddRange(srmDeliveryNoteInfos.Where(d => !string.IsNullOrEmpty(d.Sourcezoneno)).Select(d => d.Sourcezoneno).ToList());
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "') and " +
                "[PLANT] in ('" + string.Join("','", srmDeliveryNoteInfos.Select(d => d.Plant).ToArray()) + "')", string.Empty);
            if (zonesInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000485' where " +///送货单中存储区信息不存在
                        "[ID] in (" + string.Join(",", srmDeliveryNoteInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///
            List<long> dealedIds = new List<long>();
            ///循环写入数据
            foreach (SrmDeliveryNoteInfo srmDeliveryNoteInfo in srmDeliveryNoteInfos)
            {
                ///获取送货单明细
                List<SrmDeliveryNoteDetailInfo> srmDeliveryNoteDetails = srmDeliveryNoteDetailInfos.Where(d => d.OrderFid.GetValueOrDefault() == srmDeliveryNoteInfo.Fid.GetValueOrDefault()).ToList();
                if (srmDeliveryNoteDetails.Count == 0)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000473' where " +///送货单无物料明细
                        "[ID] = " + srmDeliveryNoteInfo.Id + ";");
                    continue;
                }
                ///供应商信息
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == srmDeliveryNoteInfo.Suppliercode);
                if (supplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000484' where " +///该送货单中供应商信息不存在
                        "[ID] = " + srmDeliveryNoteInfo.Id + ";");
                    continue;
                }
                ///校验供应商是否启用了ASN功能
                if (!supplierInfo.AsnFlag.GetValueOrDefault())
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000486' where " +///该供应商未启用ASN功能
                        "[ID] = " + srmDeliveryNoteInfo.Id + ";");
                    continue;
                }
                ///目标存储区
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == srmDeliveryNoteInfo.Targetzoneno && d.Plant == srmDeliveryNoteInfo.Plant);
                if (zonesInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000246' where " +///存储区数据错误
                        "[ID] = " + srmDeliveryNoteInfo.Id + ";");
                    continue;
                }
                ///单据衔接对象创建
                MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                ///SrmDeliveryNoteInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(srmDeliveryNoteInfo, ref materialPullingOrderInfo);
                ///SupplierInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(supplierInfo, ref materialPullingOrderInfo);
                ///
                materialPullingOrderInfo.TargetWmNo = zonesInfo.WmNo;
                ///PART_BOX_CODE,TODO:考虑由SRM增加字段？
                materialPullingOrderInfo.PartBoxCode = string.Empty;
                ///PART_BOX_NAME,11
                materialPullingOrderInfo.PartBoxName = string.Empty;
                ///
                materialPullingOrderInfo.Route = null;
                ///
                foreach (SrmDeliveryNoteDetailInfo srmDeliveryNoteDetail in srmDeliveryNoteDetails)
                {
                    ///单据衔接明细对象创建
                    MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                    ///SrmDeliveryNoteDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(srmDeliveryNoteDetail, ref materialPullingOrderDetailInfo);
                    ///SRM拉动单明细中的供应商必须为单据的供应商
                    materialPullingOrderDetailInfo.SupplierNum = srmDeliveryNoteInfo.Suppliercode;
                    ///
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Add(materialPullingOrderDetailInfo);
                }
                ///生成创建入库单的语句
                @string.Append(MaterialPullingCommonBLL.CreateReceiveSql(materialPullingOrderInfo, new List<PartsStockInfo>(), loginUser));
                ///执行成功的ID
                dealedIds.Add(srmDeliveryNoteInfo.Id);
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_SRM_DELIVERY_NOTE] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
        }
    }
}

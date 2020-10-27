namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// VmiReceiveBLL
    /// </summary>
    public class VmiReceiveBLL
    {
        #region Common
        /// <summary>
        /// ReceiveDAL
        /// </summary>
        VmiReceiveDAL dal = new VmiReceiveDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<VmiReceiveInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 通过ID进行查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VmiReceiveInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(VmiReceiveInfo info)
        {
            ///入库单号①系统自动根据规则创建
            info.ReceiveNo = new SeqDefineDAL().GetCurrentCode("VMI_RECEIVE_NO");
            ///
            info.Status = (int)WmmOrderStatusConstants.Created;
            ///填充供应商类型，只能是物料供应商
            if (!string.IsNullOrEmpty(info.SupplierNum))
                info.SupplierType = (int)SupplierTypeConstants.MaterialSupplier;
            return dal.Add(info);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<VmiReceiveInfo> GetList(string whereText, string orderText)
        {
            return dal.GetList(whereText, orderText);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///入库单必须为已创建状态
            VmiReceiveInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000683");///状态必须为已创建

            ///
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("update [LES].[TT_WMM_VMI_RECEIVE_DETAIL] set " +
                "[VALID_FLAG] = 0," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [RECEIVE_FID] = N'" + info.Fid.GetValueOrDefault() + "';");
            @string.AppendLine("update [LES].[TT_WMM_VMI_RECEIVE] set " +
                "[VALID_FLAG] = 0," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [ID] = " + id + ";");
            ///执行
            using (var trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            ///状态⑫为10.已创建的入库单可以进行修改
            VmiReceiveInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000683");///状态必须为已创建

            ///供应商，对于VMI来说明细中的供应商就是单据的供应商
            StringBuilder @string = new StringBuilder();
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
            if (!string.IsNullOrEmpty(supplierNum) && info.SupplierNum != supplierNum)
            {
                @string.AppendLine("" +
                    "update [LES].[TT_WMM_VMI_RECEIVE_DETAIL] set " +
                    "[SUPPLIER_NUM] = N'" + supplierNum + "'," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [RECEIVE_FID] = N'" + info.Fid.GetValueOrDefault() + "';");
            }
            ///执行
            using (var trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Release
        /// <summary>
        /// 发布(提交)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单
            List<VmiReceiveInfo> vmiReceiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (vmiReceiveInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///入库单明细
            List<VmiReceiveDetailInfo> vmiReceiveDetailInfos = new VmiReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", vmiReceiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (vmiReceiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000367");///入库单没有明细

            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "RELEASE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED",
                "MANUAL_VMI_RECEIVE_ORDER_RELEASE_CREATE_BARCODE",
                "RELEASE_VMI_RECEIVE_LOAD_PART_INSPECTION_MODE"
            });
            ///已生成的标签
            List<BarcodeInfo> barcodeInfos = new BarcodeDAL().GetList("" +
                "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Created + " and " +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", vmiReceiveDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);

            ///执行语句
            StringBuilder @string = new StringBuilder();
            foreach (var vmiReceiveInfo in vmiReceiveInfos)
            {
                ///一般手工创建入库单会用到此功能，入库单必须为10.已创建状态⑫
                if (vmiReceiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                    throw new Exception("MC:0x00000683");///状态必须为已创建

                ///VMI入库单明细
                List<VmiReceiveDetailInfo> vmiReceiveDetails = vmiReceiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == vmiReceiveInfo.Fid.GetValueOrDefault()).ToList();
                ///VMI入库单对应的标签数据
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => vmiReceiveDetails.Select(f => f.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
                ///将VMI入库单明细对象转换为普通入库单对象，方便后续函数处理
                List<ReceiveDetailInfo> receiveDetailInfos = ReceiveDetailBLL.GetReceiveDetailInfos(vmiReceiveDetails);
                ///手工创建VMI入库单时创建条码标签
                configs.TryGetValue("MANUAL_VMI_RECEIVE_ORDER_RELEASE_CREATE_BARCODE", out string manual_vmi_receive_order_release_create_barcode);
                if (!string.IsNullOrEmpty(manual_vmi_receive_order_release_create_barcode) && manual_vmi_receive_order_release_create_barcode.ToLower() == "true")
                    @string.AppendLine(MaterialPullingCommonBLL.GetCreateBarcodesSql(receiveDetailInfos, barcodes, loginUser));
                ///
                ReceiveInfo receiveInfo = ReceiveBLL.CreateReceiveInfo(loginUser);
                ///VmiReceiveInfo -> ReceiveInfo
                ReceiveBLL.GetReceiveInfo(vmiReceiveInfo, ref receiveInfo);
                ///VMI入库单发布时是否加载物料检验模式,TODO:此处函数内有与入库单共用的系统配置
                configs.TryGetValue("RELEASE_VMI_RECEIVE_LOAD_PART_INSPECTION_MODE", out string release_vmi_receive_load_part_inspection_mode);
                if (!string.IsNullOrEmpty(release_vmi_receive_load_part_inspection_mode) && release_vmi_receive_load_part_inspection_mode.ToLower() == "true")
                    @string.AppendLine(PartInspectionModeBLL.LoadInspectionMode(ref receiveInfo, ref receiveDetailInfos, loginUser));
                ///行号更新
                int rowNo = 0;
                ///发布VMI入库单时实收数量默认等于需求数量
                configs.TryGetValue("RELEASE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED", out string release_vmi_receive_actual_qty_equals_required);
                foreach (var receiveDetailInfo in receiveDetailInfos)
                {
                    if (!string.IsNullOrEmpty(release_vmi_receive_actual_qty_equals_required) && release_vmi_receive_actual_qty_equals_required.ToLower() == "true")
                    {
                        if (receiveDetailInfo.ActualBoxNum == null) receiveDetailInfo.ActualBoxNum = receiveDetailInfo.RequiredBoxNum;
                        if (receiveDetailInfo.ActualQty == null) receiveDetailInfo.ActualQty = receiveDetailInfo.RequiredQty;
                    }
                    ///更新入库单明细需要注意不能覆盖明细中原内容
                    @string.AppendLine("update [LES].[TT_WMM_VMI_RECEIVE_DETAIL] set " +
                        "[ROW_NO] = " + ++rowNo + "," +
                        (receiveDetailInfo.ActualBoxNum == null ? string.Empty : "[ACTUAL_BOX_NUM] = " + receiveDetailInfo.ActualBoxNum.GetValueOrDefault() + ",") +
                        (receiveDetailInfo.ActualQty == null ? string.Empty : "[ACTUAL_QTY] = " + receiveDetailInfo.ActualQty.GetValueOrDefault() + ",") +
                        (receiveDetailInfo.InspectionMode == null ? string.Empty : "[INSPECTION_MODE] = " + receiveDetailInfo.InspectionMode.GetValueOrDefault() + ",") +
                        (string.IsNullOrEmpty(receiveDetailInfo.SupplierNum) ? "[SUPPLIER_NUM] = N'" + receiveInfo.SupplierNum + "'," : string.Empty) +
                        "[WM_NO] = N'" + receiveInfo.WmNo + "'," +
                        "[ZONE_NO] = N'" + receiveInfo.ZoneName + "'," +
                        "[TARGET_WM] = N'" + receiveInfo.WmNo + "'," +
                        "[TARGET_ZONE] = N'" + receiveInfo.ZoneNo + "'," +
                        (string.IsNullOrEmpty(receiveDetailInfo.RunsheetNo) ? "[RUNSHEET_NO] = N'" + receiveInfo.RunsheetNo + "'," : string.Empty) +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' where " +
                        "[ID] = " + receiveDetailInfo.Id + ";");
                }
                ///更新入库单
                @string.AppendLine("update [LES].[TT_WMM_VMI_RECEIVE] set " +
                    "[SUM_PART_QTY] = " + vmiReceiveDetails.Sum(d => d.RequiredQty.GetValueOrDefault()) + "," +
                    "[SUM_OF_PRICE] = " + vmiReceiveDetails.Sum(d => d.PartPrice.GetValueOrDefault()) + "," +
                    "[SUM_WEIGHT] = " + vmiReceiveDetails.Sum(d => d.SumWeight.GetValueOrDefault()) + "," +
                    "[SUM_VOLUME] = " + vmiReceiveDetails.Sum(d => d.SumVolume.GetValueOrDefault()) + "," +
                    "[SUM_PACKAGE_QTY] = " + vmiReceiveDetails.Sum(d => d.RequiredBoxNum.GetValueOrDefault()) + "," +
                    //"[INSPECTION_FLAG] = " + (receiveInfo.InspectionFlag.GetValueOrDefault() ? 1 : 0) + "," +
                    "[STATUS] = " + (int)WmmOrderStatusConstants.Published + "," +
                    "[MODIFY_USER] = N'" + loginUser + "' ," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + vmiReceiveInfo.Id + ";");
            }
            ///删除已删除VMI入库单明细的标签
            @string.AppendLine("update [LES].[TT_WMM_BARCODE] " +
                "set [VALID_FLAG] = 0 " +
                "where [CREATE_SOURCE_FID] in (select [FID] from [LES].[TT_WMM_VMI_RECEIVE_DETAIL] with(nolock) " +
                "where [VALID_FLAG] = 0 and [RECEIVE_FID] in ('" + string.Join("','", vmiReceiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'));");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Complete
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CompleteInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///入库单必须为已发布状态⑫才可以进行完成操作
            ///根据单据中物料需求数量进行物料入库，并标记状态⑫为50.已完成
            List<VmiReceiveInfo> vmiReceiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (vmiReceiveInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiReceiveDetailInfo> vmiReceiveDetailInfos = new VmiReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", vmiReceiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (vmiReceiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000367");///入库单没有明细

            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG",
                "COMPLETE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED_WHEN_NULL"
            });
            ///完成VMI入库单时实收数量为空则等于需求数量
            configs.TryGetValue("COMPLETE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED_WHEN_NULL", out string complete_vmi_receive_actual_qty_equals_required_when_null);
            if (!string.IsNullOrEmpty(complete_vmi_receive_actual_qty_equals_required_when_null) && complete_vmi_receive_actual_qty_equals_required_when_null.ToLower() == "true")
            {
                foreach (var vmiReceiveDetailInfo in vmiReceiveDetailInfos)
                {
                    if (vmiReceiveDetailInfo.ActualBoxNum == null) vmiReceiveDetailInfo.ActualBoxNum = vmiReceiveDetailInfo.RequiredBoxNum;
                    if (vmiReceiveDetailInfo.ActualQty == null) vmiReceiveDetailInfo.ActualQty = vmiReceiveDetailInfo.RequiredQty;
                }
            }
            ///
            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///是否在客户端扫描标签条码后更新状态为已扫描
            configs.TryGetValue("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG", out string client_scaned_barcode_update_barcode_status_flag);
            if (!string.IsNullOrEmpty(client_scaned_barcode_update_barcode_status_flag) && client_scaned_barcode_update_barcode_status_flag.ToLower() == "true")
            {
                ///获取已扫描的标签
                barcodeInfos = new BarcodeDAL().GetList("[ASN_RUNSHEET_NO] in ('" + string.Join("','", vmiReceiveInfos.Select(d => d.ReceiveNo).ToArray()) + "') and [BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Scaned + "", string.Empty);
            }
            ///
            StringBuilder @string = new StringBuilder();
            foreach (var vmiReceiveInfo in vmiReceiveInfos)
            {
                if (vmiReceiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                    throw new Exception("MC:0x00000148");///已提交的入库单才能进行确认操作

                List<VmiReceiveDetailInfo> detailInfos = vmiReceiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == vmiReceiveInfo.Fid.GetValueOrDefault()).ToList();
                if (detailInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///获取已扫描的标签
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.AsnRunsheetNo == vmiReceiveInfo.ReceiveNo).ToList();
                ///拼接多张入库单
                @string.AppendLine(GetReceiveCompleteDealSql(vmiReceiveInfo, detailInfos, barcodes, loginUser));
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// GetReceiveCompleteDealSql
        /// </summary>
        /// <param name="vmiReceiveInfo"></param>
        /// <param name="vmiReceiveDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetReceiveCompleteDealSql(VmiReceiveInfo vmiReceiveInfo, List<VmiReceiveDetailInfo> vmiReceiveDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_VMI_RECEIVE_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG",
                "VMI_RECEIVE_MATERIAL_RECHECK_INSPECT_MODE",
                "INBOUND_SYNC_OUTBOUND_ENABLE_FLAG",
                "ENABLE_VMI_PACKAGE_MANAGEMENT_FLAG"
            });
            ///入库单客户端提交时处理语句
            ///WEB端为完成操作
            ///TODO:考虑增加是否支持单据多次收货的开关
            StringBuilder @string = new StringBuilder();
            ///更新入库单状态
            @string.AppendLine(GetReceiveStatusUpdateSql(vmiReceiveInfo.Id, WmmOrderStatusConstants.Completed, loginUser));
            ///
            foreach (VmiReceiveDetailInfo vmiReceiveDetailInfo in vmiReceiveDetailInfos)
            {
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == vmiReceiveDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(w => w.AsnRunsheetNo == vmiReceiveDetailInfo.TranNo
                    && w.PartNo == vmiReceiveDetailInfo.PartNo
                    && w.SupplierNum == vmiReceiveDetailInfo.SupplierNum
                    && w.RunsheetNo == vmiReceiveDetailInfo.RunsheetNo).ToList();
                }

                ///是否校验VMI实收数量等于扫描数量
                configs.TryGetValue("VALID_VMI_RECEIVE_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string valid_vmi_receive_actual_qty_equal_scaned_qty_flag);
                if (!string.IsNullOrEmpty(valid_vmi_receive_actual_qty_equal_scaned_qty_flag) && valid_vmi_receive_actual_qty_equal_scaned_qty_flag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != vmiReceiveDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != vmiReceiveDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }

                ///入库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///来源不为空时获取来源
                    if (!string.IsNullOrEmpty(vmiReceiveDetailInfo.WmNo) && !string.IsNullOrEmpty(vmiReceiveDetailInfo.ZoneNo))
                    {
                        @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Inbound,
                        vmiReceiveDetailInfo.WmNo,
                        vmiReceiveDetailInfo.ZoneNo,
                        vmiReceiveDetailInfo.Dloc,
                        vmiReceiveDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                    }
                    else
                        @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                            (int)BarcodeStatusConstants.Inbound,
                            vmiReceiveDetailInfo.TargetWm,
                            vmiReceiveDetailInfo.TargetZone,
                            vmiReceiveDetailInfo.TargetDloc,
                            vmiReceiveDetailInfo.TranNo,
                            barcodeInfo.Fid.GetValueOrDefault(),
                            loginUser));
                }

                ///更新入库单明细信息
                @string.AppendLine(GetReceiveDetailActualQtyUpdateSql(
                    vmiReceiveDetailInfo.Id,
                    vmiReceiveDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    vmiReceiveDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }
            ///转换为普通入库单进行处理
            List<ReceiveDetailInfo> receiveDetailInfos = ReceiveDetailBLL.GetReceiveDetailInfos(vmiReceiveDetailInfos);
            ReceiveInfo receiveInfo = ReceiveBLL.CreateReceiveInfo(loginUser);
            ReceiveBLL.GetReceiveInfo(vmiReceiveInfo, ref receiveInfo);
            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string les_tran_data_enable_flag);
            if (!string.IsNullOrEmpty(les_tran_data_enable_flag) && les_tran_data_enable_flag.ToLower() == "true")
                @string.AppendLine(ReceiveBLL.GetTranDetailsInsertSql(receiveInfo, receiveDetailInfos, (int)WmmTranTypeConstants.Inbound, loginUser));

            ///系统配置中RECEIVE_MATERIAL_RECHECK_INSPECT_MODE入库免检物料重新校验检验模式标记，默认为true
            ///若该标记为true时将入库明细中的㊺免检物料比对检验模式基础数据中物料的当前检验模式
            ///若检验模式有变化则需要将变化的物料提交至QMIS检验任务中间表，并生成同步数据任务，否则忽略此逻辑（此项逻辑可以考虑异步实现）
            ///VMI入库免检物料重新校验检验模式标记
            configs.TryGetValue("VMI_RECEIVE_MATERIAL_RECHECK_INSPECT_MODE", out string vmi_receive_material_recheck_inspect_mode);
            if (!string.IsNullOrEmpty(vmi_receive_material_recheck_inspect_mode) && vmi_receive_material_recheck_inspect_mode.ToLower() == "true")
                @string.AppendLine(PartInspectionModeBLL.ReloadInspectionMode(receiveInfo, ref receiveDetailInfos, loginUser));

            ///将入库明细中是否产生出库单标记㊵为true的数据过滤出来，系统配置中SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG相同存储区相同中转存储区验证标记，
            ///默认为true，控制了同一张入库单的明细中不会出现不同的出库目标存储区㊷，
            ///所以此时只需直接根据入库单及明细复制出相应的出库单及明细，并以出库目标存储区㊷作为出库单的目标存储区入库实际数量⑱作为出库需求数量，
            ///若系统配置标记为false，则将过滤出来的入库明细数据根据其出库目标存储区进行分组，并按分组情况生成多个出库单，出库单状态为已发布WMM - 011
            ///入库后同步生成出库指令启用标记
            configs.TryGetValue("INBOUND_SYNC_OUTBOUND_ENABLE_FLAG", out string inboundSyncOutboundEnableFlag);
            if (!string.IsNullOrEmpty(inboundSyncOutboundEnableFlag) && inboundSyncOutboundEnableFlag.ToLower() == "true")
                @string.AppendLine(OutputBLL.CreateOutputByReceiveSql(receiveInfo, receiveDetailInfos, barcodeInfos, loginUser));

            ///系统配置ENABLE_PACKAGE_MANAGEMENT_FLAG是否启用器具管理标记，默认为true
            ///若该标记为ture时需要根据实收包装数量⑰以及包装型号⑲等数据产生器具包装随货入库交易数据PCM-002
            ///是否启用VMI器具管理标记
            configs.TryGetValue("ENABLE_VMI_PACKAGE_MANAGEMENT_FLAG", out string enablePackageManagementFlag);
            if (!string.IsNullOrEmpty(enablePackageManagementFlag) && enablePackageManagementFlag.ToLower() == "true")
                @string.AppendLine(PackageTranDetailBLL.CreatePackageTranDetailsSql(receiveDetailInfos, loginUser));

            return @string.ToString();
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭(作废)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CloseInfo(long id, string loginUser)
        {
            ///入库单必须为已发布状态⑫才可以进行关单操作
            ///作完成后将已发布状态⑫更改为90.已关单状态
            VmiReceiveInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.Status != (int)WmmOrderStatusConstants.Published)
                throw new Exception("MC:0x00000735");///入库单必须为已发布状态
            string sql = "[STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(sql, id) > 0 ? true : false;
        }
        #endregion

        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintReceiveDatas(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select T1.*,T2.[ITEM_NAME] as RECEIVE_TYPE_NAME,T3.[ITEM_NAME] as INSPECTION_FLAG_NAME from [LES].[TT_WMM_VMI_RECEIVE] T1 with(nolock) " +
                "left join dbo.[TS_SYS_CODE_ITEM] T2 with(nolock) on T2.[CODE_FID] = N'E71E90A7-C157-4FAD-9D17-AD9B210AA5AF' and T1.[RECEIVE_TYPE] = T2.[ITEM_VALUE] and T2.[VALID_FLAG] = 1 " +
                "left join dbo.[TS_SYS_CODE_ITEM] T3 with(nolock) on T3.[CODE_FID] = N'40AC34FF-5B7F-4033-8344-07A658C4D907' and T1.[INSPECTION_FLAG] = T3.[ITEM_VALUE] and T3.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_WMM_VMI_RECEIVE_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [RECEIVE_FID] in (select [FID] from [LES].[TT_WMM_VMI_RECEIVE] with(nolock) " +
                "where [ID] in (" + string.Join(",", rowsKeyValues) + ") and [VALID_FLAG] = 1);";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 打印后回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        public bool PrintReceiveCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_VMI_RECEIVE] set " +
                "[PRINT_TIME] = GETDATE()," +
                "[PRINT_COUNT] = isnull([PRINT_COUNT],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 获取标签打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintBarcodeDatas(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select * from [LES].[TT_WMM_BARCODE] with(nolock) where " +
                "[CREATE_SOURCE_FID] in (select [FID] from [LES].[TT_WMM_VMI_RECEIVE_DETAIL] with(nolock) where " +
                "[VALID_FLAG] = 1 and " +
                "[RECEIVE_FID] in (select [FID] from [LES].[TT_WMM_VMI_RECEIVE] with(nolock) where " +
                "[VALID_FLAG] = 1 and " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + "))) and " +
                "[VALID_FLAG] = 1;";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 标签打印回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PrintBarcodeCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_BARCODE] set " +
                "[PRINT_DATE] = GETDATE()," +
                "[PRINT_TIMES] = isnull([PRINT_TIMES],0) + 1," +
                "[PRINTED_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            ///TODO:是否需要记录BARCODE_STATUS？
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion

        #region Private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outputOrderStatus"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetReceiveStatusUpdateSql(long id, WmmOrderStatusConstants receiveOrderStatus, string loginUser)
        {
            string receiveStatusUpdateSql = "update [LES].[TT_WMM_VMI_RECEIVE] set " +
                "[STATUS] = {0}," +
                "[TRAN_TIME] = GETDATE()," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'{1}' " +
                "where [ID] = {2};";
            return string.Format(receiveStatusUpdateSql, (int)receiveOrderStatus, loginUser, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actualBoxNum"></param>
        /// <param name="actualQty"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetReceiveDetailActualQtyUpdateSql(long id, decimal actualBoxNum, decimal actualQty, string loginUser)
        {
            string receiveDetailActualQtyUpdateSql = "update [LES].[TT_WMM_VMI_RECEIVE_DETAIL] set " +
                "[ACTUAL_BOX_NUM] = {0}," +
                "[ACTUAL_QTY] = {1}," +
                "[MODIFY_USER] = N'{2}'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [ID] = {3};";
            ///更新入库单明细实际箱数、实际数量、修改人、修改时间
            return string.Format(receiveDetailActualQtyUpdateSql, actualBoxNum, actualQty, loginUser, id);
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create VmiReceiveInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="receiveType"></param>
        /// <param name="status"></param>
        /// <returns>VmiReceiveInfo</returns>
        public static VmiReceiveInfo CreateVmiReceiveInfo(string loginUser, int receiveType, int status)
        {
            VmiReceiveInfo vmiReceiveInfo = new VmiReceiveInfo
            {
                ///FID
                Fid = Guid.NewGuid(),
                ///VALID_FLAG
                ValidFlag = true,
                ///CREATE_DATE
                CreateDate = DateTime.Now,
                ///CREATE_USER
                CreateUser = loginUser,
                ///RECEIVE_NO
                ReceiveNo = new SeqDefineDAL().GetCurrentCode("VMI_RECEIVE_NO"),
                ///RECEIVE_TYPE
                ReceiveType = receiveType,
                ///SEND_TIME
                SendTime = DateTime.Now,
                ///STATUS
                Status = status,
                ///TRUST_TIME
                TrustTime = DateTime.Now,
                ///SOURCE_WM_NO
                SourceWmNo = string.Empty,
                ///SOURCE_ZONE_NO
                SourceZoneNo = string.Empty,
                ///DOCK
                Dock = string.Empty,
                ///TRAN_TIME
                TranTime = null,
                ///RECEIVE_REASON
                ReceiveReason = string.Empty,
                ///BOOK_KEEPER
                BookKeeper = string.Empty,
                ///ASN_NO
                AsnNo = string.Empty,
                ///PRINT_COUNT
                PrintCount = null,
                ///PRINT_TIME
                PrintTime = null,
                ///LAST_PRINT_USER
                LastPrintUser = string.Empty,
                ///IS_OUTPUT
                IsOutput = null,
                ///ORGANIZATION_FID
                OrganizationFid = null,
                ///COST_CENTER
                CostCenter = string.Empty,
                ///CONFIRM_USER
                ConfirmUser = string.Empty,
                ///CONFIRM_DATE
                ConfirmDate = null,
                ///LIABLE_USER
                LiableUser = string.Empty,
                ///LIABLE_DATE
                LiableDate = null,
                ///FINANCE_USER
                FinanceUser = string.Empty,
                ///FINANCE_DATE
                FinanceDate = null,
                ///PART_BOX_CODE
                PartBoxCode = string.Empty,
                ///PART_BOX_NAME
                PartBoxName = string.Empty,
                ///INSPECTION_FLAG
                InspectionFlag = null,
                ///SUM_WEIGHT
                SumWeight = null,
                ///SUM_VOLUME
                SumVolume = null,
                ///CUST_CODE
                CustCode = string.Empty,
                ///CUST_NAME
                CustName = string.Empty,
                ///ROUTE
                Route = string.Empty,
                ///ROUTE_NAME
                RouteName = string.Empty,
                ///PLAN_SHIPPING_TIME
                PlanShippingTime = null,
                ///INSPECTION_MODE
                InspectionMode = null,
                ///SUM_PACKAGE_QTY
                SumPackageQty = null
            };
            ///
            return vmiReceiveInfo;
        }
        /// <summary>
        /// SrmVmiShippingNoteInfo -> VmiReceiveInfo
        /// </summary>
        /// <param name="srmVmiShippingNoteInfo"></param>
        /// <param name="vmiReceiveInfo"></param>
        public static void GetVmiReceiveInfo(SrmVmiShippingNoteInfo srmVmiShippingNoteInfo, ref VmiReceiveInfo vmiReceiveInfo)
        {
            if (srmVmiShippingNoteInfo == null) return;
            ///PLANT
            vmiReceiveInfo.Plant = srmVmiShippingNoteInfo.Plant;
            ///RUNSHEET_NO
            vmiReceiveInfo.RunsheetNo = srmVmiShippingNoteInfo.ShippingCode;
            ///SUPPLIER_NUM
            vmiReceiveInfo.SupplierNum = srmVmiShippingNoteInfo.SupplierCode;
            ///PLAN_DELIVERY_TIME
            vmiReceiveInfo.PlanDeliveryTime = srmVmiShippingNoteInfo.DeliveryTime;
            ///WM_NO
            vmiReceiveInfo.WmNo = srmVmiShippingNoteInfo.VmiWmNo;
        }
        /// <summary>
        /// SupplierInfo -> VmiReceiveInfo
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <param name="vmiReceiveInfo"></param>
        public static void GetVmiReceiveInfo(SupplierInfo supplierInfo, ref VmiReceiveInfo vmiReceiveInfo)
        {
            if (supplierInfo == null) return;
            ///SUPPLIER_TYPE
            vmiReceiveInfo.SupplierType = supplierInfo.SupplierType;
        }
        /// <summary>
        /// VmiSupplierInfo -> VmiReceiveInfo
        /// </summary>
        /// <param name="vmiSupplierInfo"></param>
        /// <param name="vmiReceiveInfo"></param>
        public static void GetVmiReceiveInfo(VmiSupplierInfo vmiSupplierInfo, ref VmiReceiveInfo vmiReceiveInfo)
        {
            if (vmiSupplierInfo == null) return;
            ///ZONE_NO
            vmiReceiveInfo.ZoneNo = vmiSupplierInfo.ZoneNo;
        }
        #endregion
    }
}


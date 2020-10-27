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
    /// VmiOutputBLL
    /// </summary>
    public partial class VmiOutputBLL
    {
        #region Common
        /// <summary>
        /// OutputDAL
        /// </summary>
        VmiOutputDAL dal = new VmiOutputDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<VmiOutputInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<VmiOutputInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 通过ID进行查询
        /// </summary>
        /// <param name="outputId"></param>
        /// <returns></returns>
        public VmiOutputInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(VmiOutputInfo info)
        {
            ///出库单号系统自动根据规则创建
            info.OutputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");
            ///状态默认为已创建
            info.Status = (int)WmmOrderStatusConstants.Created;

            if (info.OutputType.GetValueOrDefault() == (int)VmiOutputTypeConstants.PullingOutbound)
                throw new Exception("MC:0x00000503");///手工创建VMI出库单时出库类型不允许选择拉动出库

            ///填充供应商类型
            ///TODO:当前用户所对应的储运供应商信息，从来源仓库对应储运供应商信息
            if (!string.IsNullOrEmpty(info.SupplierNum))
                info.SupplierType = new SupplierDAL().GetSupplierType(info.SupplierNum);

            ///TODO:需要增加一个系统开关
            if (string.IsNullOrEmpty(info.RunsheetNo) && !string.IsNullOrEmpty(info.AsnNo))
                info.RunsheetNo = new PlanPullOrderDAL().GetOrderCode(info.AsnNo);

            return dal.Add(info);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///出库单必须为已创建状态
            VmiOutputInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000126");///出库单状态必须为已创建才能删除

            ///语句
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT_DETAIL] " +
                "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                "where [RECEIVE_FID] = N'" + info.Fid.GetValueOrDefault() + "';");
            @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT] " +
                "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
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
            ///状态为10.已创建的出库单可以进行修改
            VmiOutputInfo info = dal.GetInfo(id);
            if (info.Status != (int)WmmOrderStatusConstants.Created)
                throw new Exception("MC:0x00000127");///出库单状态必须为已创建才能更新

            ///供应商
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            if (!string.IsNullOrEmpty(supplierNum) && info.SupplierNum != supplierNum)
                fields += ",[SUPPLIER_TYPE] = " + new SupplierDAL().GetSupplierType(supplierNum) + "";

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Release
        /// <summary>
        /// 发布(提交按钮)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<VmiOutputInfo> vmiOutputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (vmiOutputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<VmiOutputDetailInfo> vmiOutputDetailInfos = new VmiOutputDetailDAL().GetList("" +
                "[OUTPUT_FID] in ('" + string.Join("','", vmiOutputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (vmiOutputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///仓库
            List<string> wmNos = vmiOutputDetailInfos.Where(d => !string.IsNullOrEmpty(d.WmNo)).Select(d => d.WmNo).ToList();
            wmNos.AddRange(vmiOutputDetailInfos.Where(d => !string.IsNullOrEmpty(d.TargetWm)).Select(d => d.TargetWm).ToList());
            ///存储区
            List<string> zoneNos = vmiOutputDetailInfos.Where(d => !string.IsNullOrEmpty(d.ZoneNo)).Select(d => d.ZoneNo).ToList();
            zoneNos.AddRange(vmiOutputDetailInfos.Where(d => !string.IsNullOrEmpty(d.TargetZone)).Select(d => d.TargetZone).ToList());
            ///物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", vmiOutputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", wmNos.ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", vmiOutputDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            ///供应商信息
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", vmiOutputDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            ///
            StringBuilder @string = new StringBuilder();
            ///
            foreach (var vmiOutputInfo in vmiOutputInfos)
            {
                if (vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                    throw new Exception("MC:0x00000436");///已创建状态的出库单才能进行此项操作

                List<VmiOutputDetailInfo> vmiOutputDetails = vmiOutputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == vmiOutputInfo.Fid.GetValueOrDefault()).ToList();
                if (vmiOutputDetails.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///拼接多张出库单
                @string.AppendLine(GetOutputReleaseSql(vmiOutputInfo, vmiOutputDetails, loginUser));

                ///来源目标包装型号不同时创建新条码
                string source_target_package_model_difference_create_barcode = new ConfigDAL().GetValueByCode("SOURCE_TARGET_PACKAGE_MODEL_DIFFERENCE_CREATE_BARCODE");
                if (!string.IsNullOrEmpty(source_target_package_model_difference_create_barcode) && source_target_package_model_difference_create_barcode.ToLower() == "true")
                {
                    foreach (VmiOutputDetailInfo vmiOutputDetail in vmiOutputDetails)
                    {
                        ///如果没有目标仓库，则不需要生成标签
                        if (string.IsNullOrEmpty(vmiOutputDetail.TargetWm)) continue;
                        ///目标物料仓储信息，校验是否存在
                        PartsStockInfo targetPartsStockInfo = partsStockInfos.FirstOrDefault(d =>
                        d.PartNo == vmiOutputDetail.PartNo &&
                        d.WmNo == vmiOutputDetail.TargetWm &&
                        d.ZoneNo == vmiOutputDetail.TargetZone &&
                        d.SupplierNum == vmiOutputDetail.SupplierNum);

                        ///来源物料仓储信息，校验是否存在
                        PartsStockInfo sourcePartsStockInfo = partsStockInfos.FirstOrDefault(d =>
                        d.PartNo == vmiOutputDetail.PartNo &&
                        d.WmNo == vmiOutputDetail.WmNo &&
                        d.ZoneNo == vmiOutputDetail.ZoneNo &&
                        d.SupplierNum == vmiOutputDetail.SupplierNum);

                        ///如果目标仓库存储区的入库包装与出库单明细中的包装不一致时，需要产生新的标签
                        if (targetPartsStockInfo.InboundPackageModel == sourcePartsStockInfo.InboundPackageModel &&
                            targetPartsStockInfo.InboundPackage == sourcePartsStockInfo.InboundPackage) continue;
                        SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == vmiOutputDetail.SupplierNum);
                        ///
                        OutputDetailInfo outputDetailInfo = OutputDetailBLL.CreateOutputDetailInfo(loginUser);
                        OutputDetailBLL.GetOutputDetailInfo(vmiOutputDetail, ref outputDetailInfo);
                        ///重新赋值目标的包装数据
                        outputDetailInfo.Package = targetPartsStockInfo.Package;
                        outputDetailInfo.PackageModel = targetPartsStockInfo.PackageModel;
                        ///
                        @string.AppendLine(MaterialPullingCommonBLL.GetCreateBarcodeSql(outputDetailInfo, targetPartsStockInfo, supplierInfo, loginUser));
                    }
                }
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 获取VMI出库单发布语句
        /// </summary>
        /// <param name="vmiOutputInfo"></param>
        /// <param name="vmiOutputDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputReleaseSql(VmiOutputInfo vmiOutputInfo, List<VmiOutputDetailInfo> vmiOutputDetailInfos, string loginUser)
        {
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "RELEASE_VMI_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED",
                "RELEASE_VMI_OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY"
            });
            ///交易类型,默认为物料移库
            int tranType = (int)WmmTranTypeConstants.Movement;
            ///如果没有目标仓库存储区则是物料出库
            if (string.IsNullOrEmpty(vmiOutputInfo.TWmNo) && string.IsNullOrEmpty(vmiOutputInfo.TZoneNo))
                tranType = (int)WmmTranTypeConstants.Outbound;
            ///出库校验可用库存满足需求数量
            ///TODO:对于多站点触发同一物料的情况，这样的计算方式会有缺陷，需要调整后适应云端计算
            List<StocksInfo> stocksInfos = new List<StocksInfo>();
            ///发布VMI出库单时校验是否足够的可用库存以满足需求数量
            configs.TryGetValue("RELEASE_VMI_OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY", out string release_vmi_output_valid_available_stock_satisfy_require_qty);
            if (!string.IsNullOrEmpty(release_vmi_output_valid_available_stock_satisfy_require_qty) && release_vmi_output_valid_available_stock_satisfy_require_qty.ToLower() == "true")
            {
                stocksInfos = new StocksDAL().GetList("" +
                            "[PART_NO] in ('" + string.Join("','", vmiOutputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                            "[WM_NO] = N'" + vmiOutputInfo.WmNo + "' and " +
                            "[ZONE_NO] = N'" + vmiOutputInfo.ZoneNo + "'", string.Empty);
            }
            ///
            StringBuilder @string = new StringBuilder();

            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            bool? settledFlag = null;
            int rowNo = 0;
            foreach (var vmiOutputDetailInfo in vmiOutputDetailInfos)
            {
                if (vmiOutputDetailInfo.RequiredQty == null)
                    throw new Exception("MC:0x00000502");///物料需求数量不能为空
                if (vmiOutputDetailInfo.RequiredQty.GetValueOrDefault() == 0)
                    throw new Exception("MC:0x00000502");///物料需求数量不能为空

                ///校验库存
                if (!string.IsNullOrEmpty(release_vmi_output_valid_available_stock_satisfy_require_qty) && release_vmi_output_valid_available_stock_satisfy_require_qty.ToLower() == "true")
                {
                    ///
                    OutputDetailInfo outputDetailInfo = OutputDetailBLL.CreateOutputDetailInfo(loginUser);
                    OutputDetailBLL.GetOutputDetailInfo(vmiOutputDetailInfo, ref outputDetailInfo);
                    ///
                    List<VmiOutputDetailInfo> vmiOutputDetails = vmiOutputDetailInfos.Where(d => d.PartNo == vmiOutputDetailInfo.PartNo).ToList();
                    List<OutputDetailInfo> outputDetails = OutputDetailBLL.GetOutputDetailInfos(vmiOutputDetails);
                    ///校验库存
                    List<StocksInfo> stocks = new StocksBLL().GetValidStocksInfo(
                        outputDetailInfo,
                        tranType,
                        stocksInfos,
                        outputDetails,
                        barcodeInfos,
                        settledFlag);
                    if (stocks == null || stocks.Count == 0)
                        throw new Exception("MC:0x00000259");///没有足够的可用库存数量完成本次出库
                }

                ///地点信息
                string targetSql = string.Empty;
                if (string.IsNullOrEmpty(vmiOutputDetailInfo.TargetWm) || string.IsNullOrEmpty(vmiOutputDetailInfo.TargetZone))
                    targetSql = ",[TARGET_WM] = N'" + vmiOutputInfo.TWmNo + "',[TARGET_ZONE] = N'" + vmiOutputInfo.TZoneNo + "'";
                string sourceSql = string.Empty;
                if (string.IsNullOrEmpty(vmiOutputDetailInfo.WmNo) || string.IsNullOrEmpty(vmiOutputDetailInfo.ZoneNo))
                    sourceSql = ",[WM_NO] = N'" + vmiOutputInfo.WmNo + "',[ZONE_NO] = N'" + vmiOutputInfo.ZoneNo + "'";

                ///发布VMI出库单时实发数量等于需求数量
                string actualSql = string.Empty;
                configs.TryGetValue("RELEASE_VMI_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED", out string release_vmi_output_actual_qty_equals_required);
                if (!string.IsNullOrEmpty(release_vmi_output_actual_qty_equals_required) && release_vmi_output_actual_qty_equals_required.ToLower() == "true")
                {
                    if (vmiOutputDetailInfo.ActualBoxNum == null)
                        vmiOutputDetailInfo.ActualBoxNum = vmiOutputDetailInfo.RequiredBoxNum;
                    if (vmiOutputDetailInfo.ActualBoxNum != null)
                        actualSql += ",[ACTUAL_BOX_NUM] = " + vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault() + "";
                    if (vmiOutputDetailInfo.ActualQty == null)
                        vmiOutputDetailInfo.ActualQty = vmiOutputDetailInfo.RequiredQty;
                    if (vmiOutputDetailInfo.ActualQty != null)
                        actualSql += ",[ACTUAL_QTY] = " + vmiOutputDetailInfo.ActualQty.GetValueOrDefault() + "";
                }
                ///更新出库单明细
                @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT_DETAIL] " +
                    "set [ROW_NO] = " + ++rowNo + targetSql + sourceSql + actualSql + "," +
                    "[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + vmiOutputDetailInfo.Id + ";");
            }
            ///更新SUM数量
            @string.AppendLine(GetOutputSumQtyUpdateSql(vmiOutputInfo.Id, vmiOutputDetailInfos, loginUser));
            ///更新单据状态与执行时间
            @string.AppendLine(GetOutputStatusUpdateSql(vmiOutputInfo.Id, WmmOrderStatusConstants.Published, loginUser));
            ///
            return @string.ToString();
        }
        #endregion

        #region Complete
        /// <summary>
        /// 拣料完成
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CompleteInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<VmiOutputInfo> vmiOutputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (vmiOutputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiOutputDetailInfo> vmiOutputDetailInfos = new VmiOutputDetailDAL().GetList("" +
                "[OUTPUT_FID] in ('" + string.Join("','", vmiOutputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (vmiOutputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///是否在VMI客户端扫描标签条码后更新状态为已扫描
            string vmi_client_scaned_barcode_update_barcode_status_flag = new ConfigDAL().GetValueByCode("VMI_CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            if (!string.IsNullOrEmpty(vmi_client_scaned_barcode_update_barcode_status_flag) && vmi_client_scaned_barcode_update_barcode_status_flag.ToLower() == "true")
            {
                ///获取已扫描的标签，扫描时需要更新ASN_RUNSHEET_NO字段的内容
                barcodeInfos = new BarcodeDAL().GetList("" +
                    "[CREATE_SOURCE_FID] in ('" + string.Join("','", vmiOutputDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                    "[BARCODE_STATUS] in (" + (int)BarcodeStatusConstants.Scaned + "," + (int)BarcodeStatusConstants.PickedUp + ")", string.Empty);
            }

            StringBuilder @string = new StringBuilder();
            foreach (var vmiOutputInfo in vmiOutputInfos)
            {
                if (vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                    throw new Exception("MC:0x00000261");///已发布状态的出库单才能进行此项操作

                List<VmiOutputDetailInfo> vmiOutputDetails = vmiOutputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == vmiOutputInfo.Fid.GetValueOrDefault()).ToList();
                if (vmiOutputDetails.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///拼接多张出库单
                @string.AppendLine(GetOutputCompleteDealSql(vmiOutputInfo, vmiOutputDetails, barcodeInfos, loginUser));
                ///如果不是拉动出库，则不产生单据衔接
                if (vmiOutputInfo.OutputType.GetValueOrDefault() != (int)VmiOutputTypeConstants.PullingOutbound) continue;
                ///
                MaterialPullingOrderInfo orderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(vmiOutputInfo, ref orderInfo);
                ///当前ASN_NO字段中无值时，系统认为是非ASN单据
                orderInfo.AsnFlag = string.IsNullOrEmpty(vmiOutputInfo.AsnNo) ? false : true;
                orderInfo.SourceCreateType = (int)ReceiveSourceCreateTypeConstants.Wms;
                foreach (var vmiOutputDetail in vmiOutputDetails)
                {
                    MaterialPullingOrderDetailInfo orderDetailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(vmiOutputDetail, ref orderDetailInfo);
                    orderInfo.MaterialPullingOrderDetailInfos.Add(orderDetailInfo);
                }
                ///生成入库单
                @string.AppendLine(MaterialPullingCommonBLL.CreateReceiveSql(orderInfo, new List<PartsStockInfo>(), loginUser));
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// GetOutputCompleteDealSql
        /// </summary>
        /// <param name="vmiOutputInfo"></param>
        /// <param name="vmiOutputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetOutputCompleteDealSql(VmiOutputInfo vmiOutputInfo, List<VmiOutputDetailInfo> vmiOutputDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG"
            });
            ///更新出库单状态
            @string.AppendLine(GetOutputStatusUpdateSql(vmiOutputInfo.Id, WmmOrderStatusConstants.Completed, loginUser));
            ///
            foreach (VmiOutputDetailInfo vmiOutputDetailInfo in vmiOutputDetailInfos)
            {
                ///依据出入库单据明细的外键关联，获取扫描的条码信息
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == vmiOutputDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(w => w.AsnRunsheetNo == vmiOutputDetailInfo.TranNo
                    && w.PartNo == vmiOutputDetailInfo.PartNo
                    && w.SupplierNum == vmiOutputDetailInfo.SupplierNum
                    && w.RunsheetNo == vmiOutputDetailInfo.RunsheetNo).ToList();
                }
                ///是否校验VMI实际数量等于扫描数量
                configs.TryGetValue("VALID_VMI_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string valid_vmi_output_actual_qty_equal_scaned_qty_flag);
                if (!string.IsNullOrEmpty(valid_vmi_output_actual_qty_equal_scaned_qty_flag) && valid_vmi_output_actual_qty_equal_scaned_qty_flag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != vmiOutputDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }

                ///扫箱更新条码信息
                ///出库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///拣选完成将条码更新为已发货状态
                    @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Shiped,
                        barcodeInfo.WmNo,
                        barcodeInfo.ZoneNo,
                        barcodeInfo.Dloc,
                        vmiOutputDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                }

                ///更新出库单明细信息
                @string.AppendLine(GetOutputDetailActualQtyUpdateSql(
                    vmiOutputDetailInfo.Id,
                    vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    vmiOutputDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }
            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string lesTranDataEnableFlag);
            if (!string.IsNullOrEmpty(lesTranDataEnableFlag) && lesTranDataEnableFlag.ToLower() == "true")
            {
                ///拣选完成时，对于需要生成交易的类型只有可能是物料封存
                CompleteWmmTranType(vmiOutputInfo.OutputType.GetValueOrDefault(), out int wmmTranTypeConstants);
                ///
                OutputInfo outputInfo = OutputBLL.CreateOutputInfo(loginUser);
                OutputBLL.GetOutputInfo(vmiOutputInfo, ref outputInfo);
                ///
                List<OutputDetailInfo> outputDetailInfos = OutputDetailBLL.GetOutputDetailInfos(vmiOutputDetailInfos);
                ///
                @string.AppendLine(OutputBLL.GetTranDetailsInsertSql(
                    outputInfo,
                    outputDetailInfos,
                    barcodeInfos,
                    null,
                    wmmTranTypeConstants,
                    true,
                    loginUser));
            }
            return @string.ToString();
        }
        #endregion

        #region Close
        /// <summary>
        /// 交接完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CloseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<VmiOutputInfo> vmiOutputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (vmiOutputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiOutputDetailInfo> vmiOutputDetailInfos = new VmiOutputDetailDAL().GetList("" +
                "[OUTPUT_FID] in ('" + string.Join("','", vmiOutputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (vmiOutputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///仓库
            List<string> wmNos = vmiOutputInfos.Where(d => !string.IsNullOrEmpty(d.WmNo)).Select(d => d.WmNo).ToList();
            wmNos.AddRange(vmiOutputInfos.Where(d => !string.IsNullOrEmpty(d.TWmNo)).Select(d => d.TWmNo).ToList());
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("[WAREHOUSE] in ('" + string.Join("','", wmNos.ToArray()) + "')", string.Empty);

            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///是否在VMI客户端扫描标签条码后更新状态为已扫描
            string vmi_client_scaned_barcode_update_barcode_status_flag = new ConfigDAL().GetValueByCode("VMI_CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            if (!string.IsNullOrEmpty(vmi_client_scaned_barcode_update_barcode_status_flag) && vmi_client_scaned_barcode_update_barcode_status_flag.ToLower() == "true")
            {
                ///获取已发运的标签，扫描时需要更新ASN_RUNSHEET_NO字段的内容
                barcodeInfos = new BarcodeDAL().GetList("" +
                    "[ASN_RUNSHEET_NO] in ('" + string.Join("','", vmiOutputInfos.Select(d => d.OutputNo).ToArray()) + "') and " +
                    "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Shiped + "", string.Empty);
            }
            ///语句
            StringBuilder @string = new StringBuilder();
            foreach (var vmiOutputInfo in vmiOutputInfos)
            {
                if (vmiOutputInfo.OutputType.GetValueOrDefault() == (int)VmiOutputTypeConstants.PullingOutbound)
                    throw new Exception("MC:0x00000504");///拉动出库时单据将在客户收货时关闭

                if (vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published
                    && vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Completed)
                    throw new Exception("MC:0x00000262");///已发布状态的出库单才能进行此项操作

                List<VmiOutputDetailInfo> vmiOutputDetails = vmiOutputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == vmiOutputInfo.Fid.GetValueOrDefault()).ToList();
                if (vmiOutputDetails.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///拼接多张出库单
                @string.AppendLine(GetOutputCloseDealSql(vmiOutputInfo, vmiOutputDetails, barcodeInfos, warehouseInfos, loginUser));
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 获取出库单提交的执行语句
        /// </summary>
        /// <param name="vmiOutputInfo"></param>
        /// <param name="vmiOutputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="settledFlag"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetOutputCloseDealSql(VmiOutputInfo vmiOutputInfo, List<VmiOutputDetailInfo> vmiOutputDetailInfos, List<BarcodeInfo> barcodeInfos, List<WarehouseInfo> warehouseInfos, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_VMI_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG",
                "OUTBOUND_SYNC_OUTBOUND_ENABLE_FLAG",
                "ENABLE_VMI_PACKAGE_MANAGEMENT_FLAG"
            });
            ///更新出库单状态
            @string.AppendLine(GetOutputStatusUpdateSql(vmiOutputInfo.Id, WmmOrderStatusConstants.Closed, loginUser));
            ///
            foreach (VmiOutputDetailInfo vmiOutputDetailInfo in vmiOutputDetailInfos)
            {
                ///依据出入库单据明细的外键关联，获取扫描的条码信息
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == vmiOutputDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(d => d.AsnRunsheetNo == vmiOutputDetailInfo.TranNo &&
                    d.PartNo == vmiOutputDetailInfo.PartNo &&
                    d.SupplierNum == vmiOutputDetailInfo.SupplierNum &&
                    d.RunsheetNo == vmiOutputDetailInfo.RunsheetNo).ToList();
                }
                ///是否校验实收数量等于扫描数量
                configs.TryGetValue("VALID_VMI_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string valid_vmi_output_actual_qty_equal_scaned_qty_flag);
                if (!string.IsNullOrEmpty(valid_vmi_output_actual_qty_equal_scaned_qty_flag) && valid_vmi_output_actual_qty_equal_scaned_qty_flag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != vmiOutputDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }
                ///扫箱更新条码信息
                ///出库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///拣选完成将条码更新为已出库状态
                    @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Outbound,
                        vmiOutputDetailInfo.WmNo,
                        vmiOutputDetailInfo.ZoneNo,
                        vmiOutputDetailInfo.Dloc,
                        vmiOutputDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                }
                ///更新出库单明细信息
                @string.AppendLine(GetOutputDetailActualQtyUpdateSql(
                    vmiOutputDetailInfo.Id,
                    vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    vmiOutputDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }
            ///
            OutputInfo outputInfo = OutputBLL.CreateOutputInfo(loginUser);
            OutputBLL.GetOutputInfo(vmiOutputInfo, ref outputInfo);
            ///
            List<OutputDetailInfo> outputDetailInfos = OutputDetailBLL.GetOutputDetailInfos(vmiOutputDetailInfos);
            ///
            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string les_tran_data_enable_flag);
            if (!string.IsNullOrEmpty(les_tran_data_enable_flag) && les_tran_data_enable_flag.ToLower() == "true")
            {
                ///目标仓库
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == vmiOutputInfo.TWmNo);
                ///关单时的交易类型
                CloseWmmTranType(
                    vmiOutputInfo.OutputType.GetValueOrDefault(),
                    vmiOutputInfo.Status.GetValueOrDefault(),
                    warehouseInfo,
                    out int wmmTranTypeConstants);
                @string.AppendLine(OutputBLL.GetTranDetailsInsertSql(
                    outputInfo,
                    outputDetailInfos,
                    barcodeInfos,
                    null,
                    wmmTranTypeConstants,
                    true,
                    loginUser));
            }

            ///出库后同步生成出库指令启用标记
            configs.TryGetValue("OUTBOUND_SYNC_OUTBOUND_ENABLE_FLAG", out string outbound_sync_outbound_enable_flag);
            if (!string.IsNullOrEmpty(outbound_sync_outbound_enable_flag) && outbound_sync_outbound_enable_flag.ToLower() == "true")
                @string.AppendLine(OutputBLL.CreateOutputByOutputSql(outputInfo, outputDetailInfos, barcodeInfos, loginUser));

            ///是否启用VMI器具管理标记,TODO:对于VMI的器具出入库还需要做重新的REVIEW
            configs.TryGetValue("ENABLE_VMI_PACKAGE_MANAGEMENT_FLAG", out string enable_vmi_package_management_flag);
            if (!string.IsNullOrEmpty(enable_vmi_package_management_flag) && enable_vmi_package_management_flag.ToLower() == "true")
                @string.AppendLine(PackageTranDetailBLL.CreatePackageTranDetailsSql(outputDetailInfos, loginUser));

            return @string.ToString();
        }
        #endregion

        #region Invalid
        /// <summary>
        /// 单据作废
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<VmiOutputInfo> vmiOutputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            StringBuilder @string = new StringBuilder();
            foreach (var vmiOutputInfo in vmiOutputInfos)
            {
                if (vmiOutputInfo.OutputType.GetValueOrDefault() == (int)VmiOutputTypeConstants.PullingOutbound)
                    throw new Exception("MC:0x00000505");///拉动出库单据不允许作废

                if (vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                    throw new Exception("MC:0x00000400");///状态为已发布时可以作废

                @string.AppendLine(GetOutputStatusUpdateSql(vmiOutputInfo.Id, WmmOrderStatusConstants.Invalid, loginUser));
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取出库单状态更新语句
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outputOrderStatus"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputStatusUpdateSql(long id, WmmOrderStatusConstants outputOrderStatus, string loginUser)
        {
            string timeFieldUpdateSql = string.Empty;
            switch (outputOrderStatus)
            {
                ///实际发货时间
                case WmmOrderStatusConstants.Published: timeFieldUpdateSql = "[SEND_TIME] = GETDATE(),"; break;
                ///实际到达时间
                case WmmOrderStatusConstants.Completed: timeFieldUpdateSql = "[CONFIRM_DATE] = GETDATE(),"; break;
                ///报关时间
                case WmmOrderStatusConstants.Closed: timeFieldUpdateSql = "[TRAN_TIME] = GETDATE(),"; break;
            }
            string outputStatusUpdateSql = "update [LES].[TT_WMM_VMI_OUTPUT] set " +
            "[STATUS] = {0}," +
            timeFieldUpdateSql +
            "[MODIFY_DATE] = GETDATE()," +
            "[MODIFY_USER] = N'{1}' " +
            "where [ID] = {2};";
            return string.Format(outputStatusUpdateSql, (int)outputOrderStatus, loginUser, id);
        }
        /// <summary>
        /// GetOutputSumQtyUpdateSql
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputSumQtyUpdateSql(long id, List<VmiOutputDetailInfo> outputDetailInfos, string loginUser)
        {
            string outputSumQtyUpdateSql = "update [LES].[TT_WMM_VMI_OUTPUT] set " +
                "[SUM_PART_QTY] = " + outputDetailInfos.Sum(d => d.RequiredQty.GetValueOrDefault()) + "," +
                "[SUM_OF_PRICE] = " + outputDetailInfos.Sum(d => d.PartPrice.GetValueOrDefault()) + "," +
                "[SUM_WEIGHT] = " + outputDetailInfos.Sum(d => d.PerpackageGrossWeight.GetValueOrDefault() * d.RequiredBoxNum.GetValueOrDefault()) + "," +
                "[SUM_VOLUME] = " + outputDetailInfos.Sum(d => d.RequiredBoxNum.GetValueOrDefault() * d.PackageLength.GetValueOrDefault() * d.PackageWidth.GetValueOrDefault() * d.PackageHeight.GetValueOrDefault() / 1000) + "," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "'" +
                "where [ID] = " + id + ";";
            return outputSumQtyUpdateSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actualBoxNum"></param>
        /// <param name="actualQty"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputDetailActualQtyUpdateSql(long id, decimal actualBoxNum, decimal actualQty, string loginUser)
        {
            string outputDetailActualQtyUpdateSql = "update [LES].[TT_WMM_VMI_OUTPUT_DETAIL] set " +
                "[ACTUAL_BOX_NUM] = {0}," +
                "[ACTUAL_QTY] = {1}," +
                "[MODIFY_USER] = N'{2}'," +
                "MODIFY_DATE = GETDATE() " +
                "where [ID] = {3};";
            ///更新出库单明细实际箱数、实际数量、修改人、修改时间
            return string.Format(outputDetailActualQtyUpdateSql, actualBoxNum, actualQty, loginUser, id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchdeletingInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0)
                throw new Exception("MC:0x00000053");///请选中行数据

            List<VmiOutputInfo> vmiOutputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (vmiOutputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///语句
            StringBuilder @string = new StringBuilder();
            foreach (var vmiOutputInfo in vmiOutputInfos)
            {
                if (vmiOutputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                    throw new Exception("MC:0x00000126");///出库单状态必须为已创建才能删除

                @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT_DETAIL] " +
                    "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [RECEIVE_FID] = N'" + vmiOutputInfo.Fid.GetValueOrDefault() + "';");
                @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT] " +
                    "set [VALID_FLAG] = 0,[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [ID] = " + vmiOutputInfo.Id + ";");
            }
            ///执行
            using (var trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// CompleteWmmTranType
        /// </summary>
        /// <param name="outputType"></param>
        /// <param name="wmmTranType"></param>
        /// <param name="settledFlag"></param>
        private static void CompleteWmmTranType(int outputType, out int wmmTranType)
        {
            switch (outputType)
            {
                ///库存调整
                case (int)VmiOutputTypeConstants.StockAdjustment:
                ///售后出库
                case (int)VmiOutputTypeConstants.CustomerServiceOutbound:
                ///拉动出库
                case (int)VmiOutputTypeConstants.PullingOutbound:
                ///生产出库
                case (int)VmiOutputTypeConstants.ProductOutbound:
                ///正常出库、移库，拣选需要状态冻结
                default: wmmTranType = (int)WmmTranTypeConstants.StateFreezing; break;
            }
        }
        /// <summary>
        /// CloseWmmTranType
        /// </summary>
        /// <param name="outputType"></param>
        /// <param name="wmmTranType"></param>
        private static void CloseWmmTranType(int outputType, int outputStatus, WarehouseInfo warehouseInfo, out int wmmTranType)
        {
            switch (outputType)
            {
                ///拉动出库
                case (int)VmiOutputTypeConstants.PullingOutbound:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    break;
                ///生产出库
                case (int)VmiOutputTypeConstants.ProductOutbound:
                ///库存调整
                case (int)VmiOutputTypeConstants.StockAdjustment:
                ///售后出库
                case (int)VmiOutputTypeConstants.CustomerServiceOutbound:
                ///正常出库
                default:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    ///如果单据已完成，则说明事先有原地冻结的操作，此时则生成物料解冻事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Completed)
                    {
                        ///如果没有目标仓库
                        if (warehouseInfo == null)
                            wmmTranType = (int)WmmTranTypeConstants.FrozenOutbound;
                        else
                        {
                            ///如果是RDC则不能产生移库
                            if (warehouseInfo.WarehouseType.GetValueOrDefault() == (int)WarehouseTypeConstants.RDC)
                                wmmTranType = (int)WmmTranTypeConstants.FrozenOutbound;
                            else
                                wmmTranType = (int)WmmTranTypeConstants.MaterialThawing;
                        }
                    }
                    ///如果单据仅为已发布状态则直接产生移库(出库)事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Published)
                    {
                        ///如果没有目标仓库
                        if (warehouseInfo == null)
                            wmmTranType = (int)WmmTranTypeConstants.Outbound;
                        else
                        {
                            ///如果是RDC则不能产生移库
                            if (warehouseInfo.WarehouseType.GetValueOrDefault() == (int)WarehouseTypeConstants.RDC)
                                wmmTranType = (int)WmmTranTypeConstants.Outbound;
                            else
                                wmmTranType = (int)WmmTranTypeConstants.Movement;
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintOutputData(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select T1.*,T2.[ITEM_NAME] as OUTPUT_TYPE_NAME from [LES].[TT_WMM_VMI_OUTPUT] T1 with(nolock) " +
                "left join dbo.[TS_SYS_CODE_ITEM] T2 with(nolock) on T2.[CODE_FID] = N'37C8AF92-D0F0-45FE-8164-48C43FC5EDF6' and T1.[OUTPUT_TYPE] = T2.[ITEM_VALUE] and T2.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_WMM_VMI_OUTPUT_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [OUTPUT_FID] in (select [FID] from [LES].[TT_WMM_VMI_OUTPUT] with(nolock) " +
                "where [ID] in (" + string.Join(",", rowsKeyValues) + ") and [VALID_FLAG] = 1);";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 打印后回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PrintOutputCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_VMI_OUTPUT] set " +
                "[PRINT_TIME] = GETDATE()," +
                "[PRINT_COUNT] = isnull([PRINT_COUNT],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create VmiOutputInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>VmiOutputInfo</returns>
        public static VmiOutputInfo CreateVmiOutputInfo(string loginUser)
        {
            VmiOutputInfo info = new VmiOutputInfo();
            ///FID,外键ID
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_USER,创建人
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;


            ///PLANT,工厂模型_工厂
            info.Plant = null;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = null;
            ///TRAN_TIME,出库时间
            info.TranTime = null;
            ///OUTPUT_REASON,出库原因
            info.OutputReason = null;
            ///BOOK_KEEPER,收货员
            info.BookKeeper = null;
            ///CONFIRM_FLAG,确认标志
            info.ConfirmFlag = null;
            ///PLAN_NO,计划行号
            info.PlanNo = null;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = null;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = null;
            ///PLANT_ZONE,工厂模型_厂区
            info.PlantZone = null;
            ///WORKSHOP,工厂模型_车间
            info.Workshop = null;
            ///TRANS_SUPPLIER_NUM,物流平台_运输供应商
            info.TransSupplierNum = null;
            ///PART_TYPE,零件类型
            info.PartType = null;
            ///SUPPLIER_TYPE,供应商类型
            info.SupplierType = null;
            ///RUNSHEET_CODE,单据号码
            info.RunsheetCode = null;
            ///ERP_FLAG,标志
            info.ErpFlag = null;
            ///LOGICAL_PK,本地主键
            info.LogicalPk = null;
            ///BUSINESS_PK,业务主键
            info.BusinessPk = null;
            ///REQUEST_TIME,请求时间
            info.RequestTime = null;
            ///CUST_CODE,客户代码
            info.CustCode = null;
            ///CUST_NAME,客户名称
            info.CustName = null;
            ///COST_CENTER,成本中心
            info.CostCenter = null;
            ///ORGANIZATION_FID,组织结构
            info.OrganizationFid = null;
            ///CONFIRM_USER,提交用户
            info.ConfirmUser = null;
            ///CONFIRM_DATE,提交时间
            info.ConfirmDate = null;
            ///LIABLE_USER,责任人
            info.LiableUser = null;
            ///LIABLE_DATE,责任人确认时间
            info.LiableDate = null;
            ///FINANCE_USER,财务
            info.FinanceUser = null;
            ///FINANCE_DATE,财务确认时间
            info.FinanceDate = null;
            ///SUM_PART_QTY,合计物料数量
            info.SumPartQty = null;
            ///SUM_OF_PRICE,合计金额
            info.SumOfPrice = null;
            ///CONVEYANCE,运输工具
            info.Conveyance = null;
            ///CARRIER_TEL,承运人电话
            info.CarrierTel = null;
            ///SUM_WEIGHT,毛重
            info.SumWeight = null;
            ///SUM_VOLUME,体积
            info.SumVolume = null;
            ///PLAN_SHIPPING_TIME,计划发货时间
            info.PlanShippingTime = null;
            ///PLAN_DELIVERY_TIME,计划到达时间
            info.PlanDeliveryTime = null;
            ///PRINT_COUNT,打印次数
            info.PrintCount = null;
            ///PRINT_TIME,打印时间
            info.PrintTime = null;
            ///COMMENTS,备注
            info.Comments = null;
            ///MODIFY_USER,最后修改人
            info.ModifyUser = null;
            ///MODIFY_DATE,最后修改时间
            info.ModifyDate = null;
            ///LAST_PRINT_USER,最后打印用户
            info.LastPrintUser = null;
            ///SUM_PACKAGE_QTY,合计箱数
            info.SumPackageQty = null;

            return info;
        }
        #endregion
    }
}

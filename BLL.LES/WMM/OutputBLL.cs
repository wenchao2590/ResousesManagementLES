namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using Infrustructure.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// OutputBLL
    /// </summary>
    public partial class OutputBLL
    {
        #region Common
        /// <summary>
        /// OutputDAL
        /// </summary>
        OutputDAL dal = new OutputDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<OutputInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
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
        public List<OutputInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 通过ID进行查询
        /// </summary>
        /// <param name="outputId"></param>
        /// <returns></returns>
        public OutputInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(OutputInfo info)
        {
            ///出库单号系统自动根据规则创建
            info.OutputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");
            ///状态默认为已创建
            //info.Status = (int)WmmOrderStatusConstants.Created;
            ///填充供应商类型
            if (!string.IsNullOrEmpty(info.SupplierNum))
                info.SupplierType = new SupplierDAL().GetSupplierType(info.SupplierNum);
            ///料废兑换和余料退库的目标仓库存储区为必填项
            if (info.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.MaterialWasteExchange ||
                info.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.ExcessStockWithDrawing)
            {
                if (string.IsNullOrEmpty(info.WmNo) || string.IsNullOrEmpty(info.ZoneNo))
                    throw new Exception("MC:0x00000443");///TODO：料废兑换与余料退库业务单据中目标仓库存储区为必填项
            }

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
            OutputInfo info = dal.GetInfo(id);
            //if (info.Status != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000126");///出库单状态必须为已创建才能删除

            string sql = "update [LES].[TT_WMM_OUTPUT_DETAIL] set " +
                "[VALID_FLAG] = 0," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [RECEIVE_FID] = N'" + info.OutputId + "';" +
                "update [LES].[TT_WMM_OUTPUT] set " +
                "[VALID_FLAG] = 0," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [ID] = " + id + ";";

            using (var trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
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
            OutputInfo info = dal.GetInfo(id);
            //if (info.Status != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000127");///出库单状态必须为已创建才能更新

            ///供应商
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            if (!string.IsNullOrEmpty(supplierNum) && info.SupplierNum != supplierNum)
                fields += ",[SUPPLIER_TYPE] = " + new SupplierDAL().GetSupplierType(supplierNum) + "";

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
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
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            string sql = string.Empty;
            foreach (var outputInfo in outputInfos)
            {
                //if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                //    throw new Exception("MC:0x00000400");///状态为已发布时可以作废
                sql += GetOutputStatusUpdateSql(outputInfo.OutputId, WmmOrderStatusConstants.Invalid, loginUser);
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
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
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] in ('" + string.Join("','", outputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///是否在客户端扫描标签条码后更新状态为已扫描
            string clientScanedBarcodeUpdateBarcodeStatusFlag = new ConfigDAL().GetValueByCode("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            if (!string.IsNullOrEmpty(clientScanedBarcodeUpdateBarcodeStatusFlag) &&
                clientScanedBarcodeUpdateBarcodeStatusFlag.ToLower() == "true")
            {
                ///获取已发运的标签，扫描时需要更新ASN_RUNSHEET_NO字段的内容
                barcodeInfos = new BarcodeDAL().GetList("" +
                    "[ASN_RUNSHEET_NO] in ('" + string.Join("','", outputInfos.Select(d => d.OutputNo).ToArray()) + "') and " +
                    "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Shiped + "", string.Empty);
            }
            ///获取所有目标存储区信息
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("[ZONE_NO] in ('" + string.Join("','", outputInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);

            string sql = string.Empty;
            foreach (var outputInfo in outputInfos)
            {
                //if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published
                //    && outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Completed)
                //    throw new Exception("MC:0x00000262");///已发布状态的出库单才能进行此项操作

                List<OutputDetailInfo> detailInfos = outputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == outputInfo.Fid.GetValueOrDefault()).ToList();
                if (detailInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.AsnRunsheetNo == outputInfo.OutputNo).ToList();
                ///拼接多张出库单
                sql += GetOutputCloseDealSql(outputInfo, detailInfos, barcodes, loginUser);
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 获取出库单提交的执行语句
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="settledFlag"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetOutputCloseDealSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser, bool emergencyFlag = true)
        {
            string outputSubmitDealSql = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG",
                "OUTBOUND_SYNC_OUTBOUND_ENABLE_FLAG",
                "ENABLE_PACKAGE_MANAGEMENT_FLAG"
            });
            ///更新出库单状态
            stringBuilder.AppendLine(GetOutputStatusUpdateSql(
                outputInfo.OutputId,
                WmmOrderStatusConstants.Closed,
                loginUser));
            ///
            foreach (OutputDetailInfo outputDetailInfo in outputDetailInfos)
            {
                ///依据出入库单据明细的外键关联，获取扫描的条码信息
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == outputDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(w => w.AsnRunsheetNo == outputDetailInfo.TranNo
                    && w.PartNo == outputDetailInfo.PartNo
                    && w.SupplierNum == outputDetailInfo.SupplierNum
                    && w.RunsheetNo == outputDetailInfo.RunsheetNo).ToList();
                }
                ///是否校验实收数量等于扫描数量
                configs.TryGetValue("VALID_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string validOutputActualQtyEqualScanedQtyFlag);
                if (!string.IsNullOrEmpty(validOutputActualQtyEqualScanedQtyFlag) &&
                    validOutputActualQtyEqualScanedQtyFlag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != outputDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != outputDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }

                ///扫箱更新条码信息
                ///出库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///拣选完成将条码更新为已出库状态
                    stringBuilder.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Inbound,
                        outputDetailInfo.TargetWm,
                        outputDetailInfo.TargetZone,
                        outputDetailInfo.TargetDloc,
                        outputDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                }

                ///更新出库单明细信息
                stringBuilder.AppendLine(GetOutputDetailActualQtyUpdateSql(
                    outputDetailInfo.Id,
                    outputDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    outputDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }
            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string lesTranDataEnableFlag);
            if (!string.IsNullOrEmpty(lesTranDataEnableFlag) &&
                lesTranDataEnableFlag.ToLower() == "true")
            {
                ///关单时的交易类型
                TranDetailsBLL.CloseWmmTranType(
                    outputInfo.OutputType.GetValueOrDefault(),
                    //outputInfo.Status.GetValueOrDefault(),
                    0,
                    out int wmmTranTypeConstants,
                    out bool? settledFlag);
                stringBuilder.AppendLine(GetTranDetailsInsertSql(
                    outputInfo,
                    outputDetailInfos,
                    barcodeInfos,
                    settledFlag,
                    wmmTranTypeConstants,
                    false,
                    loginUser));
            }

            ///入库后同步生成出库指令启用标记
            configs.TryGetValue("OUTBOUND_SYNC_OUTBOUND_ENABLE_FLAG", out string outboundSyncOutboundEnableFlag);
            if (!string.IsNullOrEmpty(outboundSyncOutboundEnableFlag) &&
                outboundSyncOutboundEnableFlag.ToLower() == "true")
                stringBuilder.AppendLine(CreateOutputByOutputSql(outputInfo, outputDetailInfos, barcodeInfos, loginUser));

            ///器具包装随货移库交易数据
            ///系统配置ENABLE_PACKAGE_MANAGEMENT_FLAG是否启用器具管理标记，默认为true
            ///若该标记为ture时需要根据实收包装数量以及包装型号等数据产生器具包装随货入库交易数据PCM-002
            configs.TryGetValue("ENABLE_PACKAGE_MANAGEMENT_FLAG", out string enablePackageManagementFlag);
            if (!string.IsNullOrEmpty(enablePackageManagementFlag) &&
                enablePackageManagementFlag.ToLower() == "true")
                stringBuilder.AppendLine(PackageTranDetailBLL.CreatePackageTranDetailsSql(outputDetailInfos, loginUser));

            ///不满足需求物料生成新拉动单
            if (emergencyFlag &&
                outputInfo.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.NormalOutbound
                )
            {

            }

            return stringBuilder.ToString();
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
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");

            //List<OutputInfo> outputs = outputInfos.Where(d => d.Status == (int)WmmOrderStatusConstants.Published).ToList();
            //if (outputs.Count != outputInfos.Count)
            //    throw new Exception("MC:0x00000261");///已发布状态的出库单才能进行此项操作

            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] in ('" + string.Join("','", outputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (outputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///是否在客户端扫描标签条码后更新状态为已扫描
            string clientScanedBarcodeUpdateBarcodeStatusFlag = new ConfigDAL().GetValueByCode("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG");
            if (!string.IsNullOrEmpty(clientScanedBarcodeUpdateBarcodeStatusFlag) &&
                clientScanedBarcodeUpdateBarcodeStatusFlag.ToLower() == "true")
            {
                ///获取已扫描的标签，扫描时需要更新ASN_RUNSHEET_NO字段的内容
                barcodeInfos = new BarcodeDAL().GetList("" +
                    "[CREATE_SOURCE_FID] in ('" + string.Join("','", outputDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                    "[BARCODE_STATUS] in (" + (int)BarcodeStatusConstants.Scaned + "," + (int)BarcodeStatusConstants.PickedUp + ")", string.Empty);
            }
            string sql = string.Empty;
            foreach (var outputInfo in outputInfos)
            {
                //if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                //    throw new Exception("MC:0x00000261");///已发布状态的出库单才能进行此项操作

                List<OutputDetailInfo> detailInfos = outputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == outputInfo.Fid.GetValueOrDefault()).ToList();
                if (detailInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.AsnRunsheetNo == outputInfo.OutputNo).ToList();
                ///拼接多张出库单
                sql += GetOutputCompleteDealSql(outputInfo, detailInfos, barcodes, loginUser);
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetOutputCompleteDealSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG"
            });
            ///更新出库单状态
            stringBuilder.AppendLine(GetOutputStatusUpdateSql(
                outputInfo.OutputId,
                WmmOrderStatusConstants.Completed,
                loginUser));
            ///
            foreach (OutputDetailInfo outputDetailInfo in outputDetailInfos)
            {
                ///依据出入库单据明细的外键关联，获取扫描的条码信息
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == outputDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(w => w.AsnRunsheetNo == outputDetailInfo.TranNo
                    && w.PartNo == outputDetailInfo.PartNo
                    && w.SupplierNum == outputDetailInfo.SupplierNum
                    && w.RunsheetNo == outputDetailInfo.RunsheetNo).ToList();
                }
                ///是否校验实收数量等于扫描数量
                configs.TryGetValue("VALID_OUTPUT_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string validOutputActualQtyEqualScanedQtyFlag);
                if (!string.IsNullOrEmpty(validOutputActualQtyEqualScanedQtyFlag) &&
                    validOutputActualQtyEqualScanedQtyFlag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != outputDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != outputDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }

                ///扫箱更新条码信息
                ///出库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///拣选完成将条码更新为已发货状态
                    stringBuilder.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Shiped,
                        barcodeInfo.WmNo,
                        barcodeInfo.ZoneNo,
                        barcodeInfo.Dloc,
                        outputDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                }

                ///更新出库单明细信息
                stringBuilder.AppendLine(GetOutputDetailActualQtyUpdateSql(
                    outputDetailInfo.Id,
                    outputDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    outputDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }
            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string lesTranDataEnableFlag);
            if (!string.IsNullOrEmpty(lesTranDataEnableFlag) &&
                lesTranDataEnableFlag.ToLower() == "true")
            {
                ///拣选完成时，对于需要生成交易的类型只有可能是物料封存
                TranDetailsBLL.CompleteWmmTranType(outputInfo.OutputType.GetValueOrDefault(), out int wmmTranTypeConstants, out bool? settledFlag);
                stringBuilder.AppendLine(GetTranDetailsInsertSql(
                    outputInfo,
                    outputDetailInfos,
                    barcodeInfos,
                    settledFlag,
                    wmmTranTypeConstants,
                    false,
                    loginUser));
            }
            return stringBuilder.ToString();
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
            List<OutputInfo> outputInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (outputInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] in ('" + string.Join("','", outputInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (outputDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///仓库
            List<string> wmNos = outputDetailInfos.Select(d => d.WmNo).ToList();
            wmNos.AddRange(outputDetailInfos.Where(d => !string.IsNullOrEmpty(d.TargetWm)).Select(d => d.TargetWm).ToList());
            ///存储区
            List<string> zoneNos = outputDetailInfos.Select(d => d.ZoneNo).ToList();
            zoneNos.AddRange(outputDetailInfos.Where(d => !string.IsNullOrEmpty(d.TargetZone)).Select(d => d.TargetZone).ToList());
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("[ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "')", string.Empty);
            string sql = string.Empty;
            ///来源目标包装型号不同时创建新条码
            string sourceTargetPackageModelDifferenceCreateBarcode = new ConfigDAL().GetValueByCode("SOURCE_TARGET_PACKAGE_MODEL_DIFFERENCE_CREATE_BARCODE");
            if (sourceTargetPackageModelDifferenceCreateBarcode.ToLower() == "true")
            {
                ///物料仓储信息
                List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                    "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                    "[WM_NO] in ('" + string.Join("','", wmNos.ToArray()) + "') and " +
                    "[ZONE_NO] in ('" + string.Join("','", zonesInfos.Select(d => d.ZoneNo).ToArray()) + "') and " +
                    "[SUPPLIER_NUM] in ('" + string.Join("','", outputDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
                ///供应商信息
                List<SupplierInfo> supplierInfos = new SupplierDAL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", outputDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
                foreach (OutputDetailInfo outputDetailInfo in outputDetailInfos)
                {
                    ///如果没有目标仓库，则不存在翻包
                    if (string.IsNullOrEmpty(outputDetailInfo.TargetWm)) continue;

                    ///目标物料仓储信息，校验是否存在
                    PartsStockInfo targetPartsStockInfo = partsStockInfos.FirstOrDefault(d =>
                    d.PartNo == outputDetailInfo.PartNo &&
                    d.WmNo == outputDetailInfo.TargetWm &&
                    d.ZoneNo == outputDetailInfo.TargetZone &&
                    d.SupplierNum == outputDetailInfo.SupplierNum);

                    ///如果目标仓库存储区的入库包装与出库单明细中的包装不一致时，需要产生新的标签
                    ///TODO:批次继承？
                    if (targetPartsStockInfo.InboundPackageModel == outputDetailInfo.PackageModel) continue;
                    SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == outputDetailInfo.SupplierNum);
                    sql += MaterialPullingCommonBLL.GetCreateBarcodeSql(outputDetailInfo, targetPartsStockInfo, supplierInfo, loginUser);
                }
            }
            ///
            foreach (var outputInfo in outputInfos)
            {
                //if (outputInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                //    throw new Exception("MC:0x00000436");///已创建状态的出库单才能进行此项操作

                List<OutputDetailInfo> detailInfos = outputDetailInfos.Where(d => d.OutputFid.GetValueOrDefault() == outputInfo.Fid.GetValueOrDefault()).ToList();
                if (detailInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///拼接多张出库单
                sql += GetOutputReleaseSql(outputInfo, detailInfos, zonesInfos, loginUser);
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputReleaseSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<ZonesInfo> zonesInfos, string loginUser)
        {
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED",
                "VALID_UNQUALIFIED_ZONE_WHEN_OUTPUT_THAW",
                "VALID_UNQUALIFIED_ZONE_WHEN_OUTPUT_FROZEN",
                "RELEASE_OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY"
            });
            ///交易类型,默认为物料移库
            int tranType = (int)WmmTranTypeConstants.Movement;
            ///如果没有目标仓库存储区则是物料出库
            if (string.IsNullOrEmpty(outputInfo.WmNo) && string.IsNullOrEmpty(outputInfo.ZoneNo))
                tranType = (int)WmmTranTypeConstants.Outbound;
            ///出库校验可用库存满足需求数量
            ///TODO:对于多站点触发同一物料的情况，这样的计算方式会有缺陷，需要调整后适应云端计算
            List<StocksInfo> stocksInfos = new List<StocksInfo>();
            string outputValidAvailableStockSatisfyRequireQty = string.Empty;
            ///发布出库单时校验是否足够的可用库存以满足需求数量
            configs.TryGetValue("RELEASE_OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY", out string releaseOutputValidAvailableStockSatisfyRequireQty);
            if (!string.IsNullOrEmpty(releaseOutputValidAvailableStockSatisfyRequireQty) && releaseOutputValidAvailableStockSatisfyRequireQty.ToLower() == "true")
                outputValidAvailableStockSatisfyRequireQty = "true";
            ///该出库类型出库单在提交时，需要校验物料号、供应商、来源仓库存储区库位中是否有足够数量的物料
            ///WMM-001在创建拉动单衔接创建的出库单时需要增加这个字段默认为10.正常出库
            if (outputInfo.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.MaterialWasteExchange)
            {
                ///校验可用库存
                outputValidAvailableStockSatisfyRequireQty = "true";
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.ZoneNo);
                if (zonesInfo == null)
                    throw new Exception("MC:0x00000246");///存储区数据错误
                if (zonesInfo.IsManage.GetValueOrDefault() != (int)WmsIsManageConstants.Unqualified)
                    throw new Exception("MC:0x00000271");///料废兑换的目标库区必须为不合格品区
            }
            ///提供OUTPUT_TYPE在出库单创建时的选择功能，其数据来源为系统代码中的出库类型OUTBOUND_TYPE，创建物料冻结单时仅需选中50.物料冻结即可
            ///该出库类型出库单在提交时，需要校验物料号、供应商、来源仓库存储区库位中是否有足够可用数量的物料
            if (outputInfo.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.MaterialFrozen)
            {
                configs.TryGetValue("VALID_UNQUALIFIED_ZONE_WHEN_OUTPUT_FROZEN", out string validUnqualifiedZoneWhenOutputFrozen);
                if (!string.IsNullOrEmpty(validUnqualifiedZoneWhenOutputFrozen) && validUnqualifiedZoneWhenOutputFrozen.ToLower() == "true")
                {
                    ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.ZoneNo);
                    if (zonesInfo == null)
                        throw new Exception("MC:0x00000246");///存储区数据错误
                    if (zonesInfo.IsManage.GetValueOrDefault() != (int)WmsIsManageConstants.Unqualified)
                        throw new Exception("MC:0x00000275");///物料冻结的目标库区必须为不合格品区
                }
                outputValidAvailableStockSatisfyRequireQty = "true";
                tranType = (int)WmmTranTypeConstants.MaterialFreezing;
            }
            ///提供OUTPUT_TYPE在出库单创建时的选择功能，其数据来源为系统代码中的出库类型OUTBOUND_TYPE，创建物料解冻单时仅需选中60.物料解冻即可
            ///该出库类型出库单在提交时，需要校验物料号、供应商、来源仓库存储区库位中是否有足够冻结数量的物料
            if (outputInfo.OutputType.GetValueOrDefault() == (int)OutboundTypeConstants.MaterialToThaw)
            {
                configs.TryGetValue("VALID_UNQUALIFIED_ZONE_WHEN_OUTPUT_THAW", out string validUnqualifiedZoneWhenOutputThaw);
                if (!string.IsNullOrEmpty(validUnqualifiedZoneWhenOutputThaw) && validUnqualifiedZoneWhenOutputThaw.ToLower() == "true")
                {
                    ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == outputInfo.ZoneNo);
                    if (zonesInfo == null)
                        throw new Exception("MC:0x00000246");///存储区数据错误
                    if (zonesInfo.IsManage.GetValueOrDefault() != (int)WmsIsManageConstants.Unqualified)
                        throw new Exception("MC:0x00000274");///物料解冻的来源库区必须为不合格品区
                }
                outputValidAvailableStockSatisfyRequireQty = "true";
                tranType = (int)WmmTranTypeConstants.MaterialThawing;
            }
            ///获取当前库存
            if (outputValidAvailableStockSatisfyRequireQty.ToLower() == "true")
            {
                stocksInfos = new StocksDAL().GetList("" +
                            "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                            "[WM_NO] = N'" + outputInfo.WmNo + "' and " +
                            "[ZONE_NO] = N'" + outputInfo.ZoneNo + "'", string.Empty);
            }

            TranDetailsBLL tranDetailsBLL = new TranDetailsBLL();
            string sql = string.Empty;

            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            bool? settledFlag = null;
            int rowNo = 0;
            foreach (var outputDetailInfo in outputDetailInfos)
            {
                sql += "update [LES].[TT_WMM_OUTPUT_DETAIL] set [ROW_NO] = " + ++rowNo + " where [ID] = " + outputDetailInfo.Id + ";";
                if (outputDetailInfo.RequiredQty.GetValueOrDefault() == 0) continue;
                ///校验库存
                if (outputValidAvailableStockSatisfyRequireQty.ToLower() == "true")
                {
                    List<OutputDetailInfo> outputDetails = outputDetailInfos.Where(d => d.PartNo == outputDetailInfo.PartNo).ToList();
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
                ///
                if (string.IsNullOrEmpty(outputDetailInfo.TargetWm) || string.IsNullOrEmpty(outputDetailInfo.TargetZone))
                {
                    sql += "update [LES].[TT_WMM_OUTPUT_DETAIL] set [TARGET_WM] = N'" + outputInfo.WmNo + "',[TARGET_ZONE] = N'" + outputInfo.ZoneNo + "' where [ID] = " + outputDetailInfo.Id + ";";
                }
                if (string.IsNullOrEmpty(outputDetailInfo.WmNo) || string.IsNullOrEmpty(outputDetailInfo.ZoneNo))
                {
                    sql += "update [LES].[TT_WMM_OUTPUT_DETAIL] set [WM_NO] = N'" + outputInfo.WmNo + "',[ZONE_NO] = N'" + outputInfo.ZoneNo + "' where [ID] = " + outputDetailInfo.Id + ";";
                }

                ///发布出库单时实发数量等于需求数量
                configs.TryGetValue("RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED", out string releaseOutputActualQtyEqualsRequired);
                if (!string.IsNullOrEmpty(releaseOutputActualQtyEqualsRequired) && releaseOutputActualQtyEqualsRequired.ToLower() == "true")
                {
                    sql += GetOutputDetailActualQtyUpdateSql(
                    outputDetailInfo.Id,
                    outputDetailInfo.RequiredBoxNum.GetValueOrDefault(),
                    outputDetailInfo.RequiredQty.GetValueOrDefault(),
                    loginUser);
                }
            }
            sql += GetOutputSumQtyUpdateSql(outputInfo.OutputId, outputDetailInfos, loginUser);
            return sql += GetOutputStatusUpdateSql(outputInfo.OutputId, WmmOrderStatusConstants.Published, loginUser);
        }
        #endregion

        #region Receive -> Output
        /// <summary>
        /// 拣配后提交生成出库单
        /// </summary>
        /// <param name="runsheetNos"></param>
        /// <param name="outputType"></param>
        /// <param name="organizationFid"></param>
        /// <param name="planShippingTime"></param>
        /// <param name="planDeliveryTime"></param>
        /// <param name="conveyance"></param>
        /// <param name="carrierTel"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="WmNo"></param>
        /// <param name="ZoneNo"></param>
        /// <returns></returns>
        public bool CreateOutputByPickupedBarcodes(string runsheetNos, int outputType, Guid organizationFid, DateTime planShippingTime, DateTime planDeliveryTime, string conveyance, string carrierTel
            , string wmNo, string zoneNo, string WmNo, string ZoneNo, string loginUser)
        {
            string[] runsheetNoArray = runsheetNos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            List<BarcodeInfo> barcodeInfos = new BarcodeDAL().GetList("[RUNSHEET_NO] in ('" + string.Join("','", runsheetNoArray) + "') and [BARCODE_STATUS] = " + (int)BarcodeStatusConstants.PickedUp + "", string.Empty);
            if (barcodeInfos.Count == 0)
                throw new Exception("MC:0x00000250");///未发现已拣配的物料标签

            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[FID] in ('" + string.Join("','", barcodeInfos.Select(d => d.CreateSourceFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (receiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000251");///入库单明细数据错误

            List<ReceiveInfo> receiveInfos = new ReceiveDAL().GetList("[FID] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.ReceiveFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            ///SQL
            string sql = string.Empty;
            foreach (var receiveInfo in receiveInfos)
            {
                receiveInfo.WmNo = wmNo;
                receiveInfo.ZoneNo = zoneNo;
                List<ReceiveDetailInfo> receiveDetails = receiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).ToList();
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => receiveDetails.Select(w => w.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
                sql += CreateOutputByReceiveSql(receiveInfo, receiveDetails, barcodes, WmNo, ZoneNo, outputType, loginUser, organizationFid, conveyance, carrierTel, planShippingTime, planDeliveryTime);
            }

            using (var trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(sql))
                    return false;
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 根据入库单生成创建出库单的语句
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="inheritOrderNoFlag"></param>
        /// <param name="targeWmNo"></param>
        /// <param name="targeZoneNo"></param>
        /// <param name="outputType"></param>
        /// <param name="loginUser"></param>
        /// <param name="organizationFid"></param>
        /// <param name="conveyance"></param>
        /// <param name="carrierTel"></param>
        /// <param name="planShippingTime"></param>
        /// <param name="planDeliveryTime"></param>
        /// <returns></returns>
        public static string CreateOutputByReceiveSql(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos, List<BarcodeInfo> barcodeInfos,
            string targeWmNo, string targeZoneNo, int? outputType, string loginUser, Guid? organizationFid, string conveyance, string carrierTel, DateTime? planShippingTime, DateTime? planDeliveryTime)
        {
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_RECEIVE_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG",
                "RECEIVE_MATERIAL_RECHECK_INSPECT_MODE",
                "ENABLE_PACKAGE_MANAGEMENT_FLAG",
                "INBOUND_SYNC_OUTBOUND_ENABLE_FLAG",
                "RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED",
                "SUPPLIER_STOCKS_DIMENSION",
                "PLANT_STOCKS_DIMENSION",
                "PACKAGE_MODEL_STOCKS_DIMENSION",
                "PART_CLS_STOCKS_DIMENSION",
                "ORIGIN_PLACE_STOCKS_DIMENSION",
                "RECEIVE_CONVERT_OUTPUT_GROUPBY_STOCKS_DIMENSION",
                "RECEIVE_AUTO_CONVERT_OUTPUT_WHEN_TARGET_ISNULL_SIGN_CREATED",
                "SYNC_OUTPUT_NO_INHERIT_RECEIVE_NO"
            });
            string sql = string.Empty;
            Guid outputFid = Guid.NewGuid();
            ///出库单号
            string outputNo = string.Empty;
            ///同步出库单号继承自入库单号
            configs.TryGetValue("SYNC_OUTPUT_NO_INHERIT_RECEIVE_NO", out string sync_output_no_inherit_receive_no);
            if (!string.IsNullOrEmpty(sync_output_no_inherit_receive_no) && sync_output_no_inherit_receive_no.ToLower() == "true")
                outputNo = receiveInfo.ReceiveNo;
            else
                outputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");
            int rowNo = 0;
            decimal sumPartQty = 0;
            decimal sumWeight = 0;
            decimal sumVolume = 0;
            int sumPackageQty = 0;
            ///拉动单号、委托编号
            string runsheetNo = string.Empty;
            ///单据状态默认为已发布，标签状态默认为已发货
            int orderStatus = (int)WmmOrderStatusConstants.Published;
            int barcodeStatus = (int)BarcodeStatusConstants.Shiped;

            ///入库单自动转出库单时无目标地点标记单据状态为已创建
            configs.TryGetValue("RECEIVE_AUTO_CONVERT_OUTPUT_WHEN_TARGET_ISNULL_SIGN_CREATED", out string receive_auto_convert_output_when_target_isnull_sign_created);
            if (!string.IsNullOrEmpty(receive_auto_convert_output_when_target_isnull_sign_created) && receive_auto_convert_output_when_target_isnull_sign_created.ToLower() == "true")
            {
                if (string.IsNullOrEmpty(targeWmNo) && string.IsNullOrEmpty(targeZoneNo))
                {
                    orderStatus = (int)WmmOrderStatusConstants.Created;
                    barcodeStatus = (int)BarcodeStatusConstants.PickedUp;
                }
            }

            ///入库单转出库单时是否根据库存维度进行数据合并
            configs.TryGetValue("RECEIVE_CONVERT_OUTPUT_GROUPBY_STOCKS_DIMENSION", out string receive_convert_output_groupby_stocks_dimension);
            if (!string.IsNullOrEmpty(receive_convert_output_groupby_stocks_dimension) && receive_convert_output_groupby_stocks_dimension.ToLower() == "true")
            {
                List<string> stockDimension = new List<string>();
                ///库存供应商维度标记
                configs.TryGetValue("SUPPLIER_STOCKS_DIMENSION", out string supplier_stocks_dimension);
                if (!string.IsNullOrEmpty(supplier_stocks_dimension) && supplier_stocks_dimension.ToLower() == "true")
                    stockDimension.Add("SUPPLIER_NUM");
                ///库存工厂维度标记
                configs.TryGetValue("PLANT_STOCKS_DIMENSION", out string plant_stocks_dimension);
                if (!string.IsNullOrEmpty(plant_stocks_dimension) && plant_stocks_dimension.ToLower() == "true")
                    stockDimension.Add("PLANT");
                ///库存包装型号维度标记
                configs.TryGetValue("PACKAGE_MODEL_STOCKS_DIMENSION", out string package_model_stocks_dimension);
                if (!string.IsNullOrEmpty(package_model_stocks_dimension) && package_model_stocks_dimension.ToLower() == "true")
                {
                    stockDimension.Add("PACKAGE_MODEL");
                    stockDimension.Add("PACKAGE");
                }
                ///库存物料类别维度标记
                configs.TryGetValue("PART_CLS_STOCKS_DIMENSION", out string part_cls_stocks_dimension);
                if (!string.IsNullOrEmpty(part_cls_stocks_dimension) && part_cls_stocks_dimension.ToLower() == "true")
                    stockDimension.Add("PART_CLS");
                ///库存产地维度标记
                configs.TryGetValue("ORIGIN_PLACE_STOCKS_DIMENSION", out string origin_place_stocks_dimension);
                if (!string.IsNullOrEmpty(origin_place_stocks_dimension) && origin_place_stocks_dimension.ToLower() == "true")
                    stockDimension.Add("ORIGIN_PLACE");
                receiveDetailInfos = new ReceiveDetailDAL().GetStockDimensionList(receiveDetailInfos.Select(d => d.Id).ToList(), stockDimension);
            }

            configs.TryGetValue("RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED", out string releaseOutputActualQtyEqualsRequired);
            foreach (var receiveDetailInfo in receiveDetailInfos)
            {

                ///明细包装数量
                int boxNum = barcodeInfos.Count;
                ///物料数量
                decimal partQty = barcodeInfos.Sum(d => d.CurrentQty.GetValueOrDefault());
                ///优先从扫描的条码进行获取，当没有扫描的条码时则根据入库单明细实收数量进行填充
                if (boxNum == 0) boxNum = receiveDetailInfo.ActualBoxNum.GetValueOrDefault();
                if (partQty == 0) partQty = receiveDetailInfo.ActualQty.GetValueOrDefault();
                ///单据合计数量
                sumPartQty += partQty;
                ///单据合计毛重
                sumWeight += boxNum * receiveDetailInfo.PerpackageGrossWeight.GetValueOrDefault();
                ///单据合计体积，长度单位为cm，体积单位为m³
                sumVolume += boxNum * receiveDetailInfo.PackageVolume.GetValueOrDefault();
                ///明细合计毛重
                decimal sumDetailWeight = boxNum * receiveDetailInfo.PerpackageGrossWeight.GetValueOrDefault();
                ///明细合计体积
                decimal sumDetailVolume = boxNum * receiveDetailInfo.PackageVolume.GetValueOrDefault();
                ///单据合计包装数量
                sumPackageQty += boxNum;

                #region TT_WMM_OUTPUT_DETAIL
                ///
                OutputDetailInfo outputDetailInfo = CreateOutputDetailInfo(outputFid, outputNo, targeWmNo, targeZoneNo, boxNum, partQty, sumDetailWeight, sumDetailVolume, loginUser);
                ///
                GetOutputDetailInfo(receiveDetailInfo, ref outputDetailInfo);
                ///
                if (!string.IsNullOrEmpty(releaseOutputActualQtyEqualsRequired) && releaseOutputActualQtyEqualsRequired.ToLower() == "true")
                {
                    outputDetailInfo.ActualBoxNum = outputDetailInfo.RequiredBoxNum;
                    outputDetailInfo.ActualQty = outputDetailInfo.RequiredQty;
                }
                ///行号
                outputDetailInfo.RowNo = ++rowNo;
                ///
                sql += OutputDetailDAL.GetInsertSql(outputDetailInfo);
                #endregion

                foreach (var barcodeInfo in barcodeInfos)
                {
                    sql += BarcodeDAL.GetBarcodeUpdateSql(
                        barcodeStatus,
                        barcodeInfo.WmNo,
                        barcodeInfo.ZoneNo,
                        barcodeInfo.Dloc,
                        outputNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser);
                }
            }
            #region TT_WMM_OUTPUT
            ///
            OutputInfo outputInfo = CreateOutputInfo(outputFid, outputNo, targeWmNo, targeZoneNo, outputType.GetValueOrDefault(), organizationFid, orderStatus, conveyance, carrierTel,
                sumPartQty, sumWeight, sumVolume, sumPackageQty, planShippingTime, planDeliveryTime, loginUser);
            ///
            GetOutputInfo(receiveInfo, ref outputInfo);
            ///
            sql += OutputDAL.GetInsertSql(outputInfo);
            #endregion
            return sql;
        }
        /// <summary>
        /// Create OutputDetailInfo
        /// </summary>
        /// <param name="outputFid"></param>
        /// <param name="outputNo"></param>
        /// <param name="targeWmNo"></param>
        /// <param name="targeZoneNo"></param>
        /// <param name="boxNum"></param>
        /// <param name="partQty"></param>
        /// <param name="sumDetailWeight"></param>
        /// <param name="sumDetailVolume"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static OutputDetailInfo CreateOutputDetailInfo(Guid outputFid, string outputNo, string targeWmNo, string targeZoneNo, int boxNum, decimal partQty, decimal sumDetailWeight, decimal sumDetailVolume, string loginUser)
        {
            OutputDetailInfo outputDetailInfo = new OutputDetailInfo
            {
                ///OUTPUT_FID
                OutputFid = outputFid,
                ///TRAN_NO
                TranNo = outputNo,
                ///TARGET_WM
                TargetWm = targeWmNo,
                ///TARGET_ZONE
                TargetZone = targeZoneNo,
                ///REQUIRED_BOX_NUM
                RequiredBoxNum = boxNum,
                ///REQUIRED_QTY
                RequiredQty = partQty,
                ///SUM_WEIGHT
                SumWeight = sumDetailWeight,
                ///SUM_VOLUME
                SumVolume = sumDetailVolume,
                ///CREATE_USER
                CreateUser = loginUser
            };
            ///
            return outputDetailInfo;
        }
        /// <summary>
        /// ReceiveInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="outputDetailInfo"></param>
        private void GetOutputDetailInfo(ReceiveInfo receiveInfo, ref OutputDetailInfo outputDetailInfo)
        {
            ///WM_NO
            outputDetailInfo.WmNo = receiveInfo.WmNo;
            ///ZONE_NO
            outputDetailInfo.ZoneNo = receiveInfo.ZoneNo;
        }
        /// <summary>
        /// ReceiveDetailInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="outputDetailInfo"></param>
        public static void GetOutputDetailInfo(ReceiveDetailInfo receiveDetailInfo, ref OutputDetailInfo outputDetailInfo)
        {
            ///FID
            outputDetailInfo.Fid = receiveDetailInfo.Fid == null ? Guid.NewGuid() : receiveDetailInfo.Fid.GetValueOrDefault();
            ///PLANT
            outputDetailInfo.Plant = receiveDetailInfo.Plant;
            ///SUPPLIER_NUM
            outputDetailInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            ///PART_NO
            outputDetailInfo.PartNo = receiveDetailInfo.PartNo;
            ///WM_NO
            outputDetailInfo.WmNo = receiveDetailInfo.TargetWm;
            ///ZONE_NO
            outputDetailInfo.ZoneNo = receiveDetailInfo.TargetZone;
            ///DLOC
            outputDetailInfo.Dloc = receiveDetailInfo.TargetDloc;
            /////PART_CNAME
            outputDetailInfo.PartCname = receiveDetailInfo.PartCname;
            ///PACKAGE
            outputDetailInfo.Package = receiveDetailInfo.Package;
            ///PACKAGE_MODEL
            ///TODO:考虑翻包逻辑
            outputDetailInfo.PackageModel = receiveDetailInfo.PackageModel;
            ///MEASURING_UNIT_NO
            outputDetailInfo.MeasuringUnitNo = receiveDetailInfo.MeasuringUnitNo;
            ///IDENTIFY_PART_NO
            outputDetailInfo.IdentifyPartNo = receiveDetailInfo.IdentifyPartNo;
            ///PART_ENAME
            outputDetailInfo.PartEname = receiveDetailInfo.PartEname;
            ///DOCK
            ///TODO:这里的道口不应该是入库道口，需要在物料仓储信息中配置出库道口
            outputDetailInfo.Dock = receiveDetailInfo.Dock;
            ///ASSEMBLY_LINE
            outputDetailInfo.AssemblyLine = receiveDetailInfo.AssemblyLine;
            ///BOX_PARTS
            outputDetailInfo.BoxParts = receiveDetailInfo.BoxParts;
            ///SEQUENCE_NO
            outputDetailInfo.SequenceNo = receiveDetailInfo.SequenceNo;
            ///RDC_DLOC
            outputDetailInfo.RdcDloc = receiveDetailInfo.RdcDloc;
            ///ORDER_NO
            outputDetailInfo.OrderNo = receiveDetailInfo.OrderNo;
            ///ITEM_NO
            outputDetailInfo.ItemNo = receiveDetailInfo.ItemNo;
            ///RUNSHEET_NO
            outputDetailInfo.RunsheetNo = receiveDetailInfo.RunsheetNo;
            ///ORIGIN_PLACE
            outputDetailInfo.OriginPlace = receiveDetailInfo.OriginPlace;
            ///PART_CLS
            outputDetailInfo.PartCls = receiveDetailInfo.PartCls;
            ///IS_SCAN_BOX
            outputDetailInfo.IsScanBox = receiveDetailInfo.IsScanBox;
            ///PACKAGE_LENGTH
            outputDetailInfo.PackageLength = receiveDetailInfo.PackageLength;
            ///PACKAGE_WIDTH
            outputDetailInfo.PackageWidth = receiveDetailInfo.PackageWidth;
            ///PACKAGE_HEIGHT
            outputDetailInfo.PackageHeight = receiveDetailInfo.PackageHeight;
            ///PERPACKAGE_GROSS_WEIGHT
            outputDetailInfo.PerpackageGrossWeight = receiveDetailInfo.PerpackageGrossWeight;
            ///PACKAGE_VOLUME
            outputDetailInfo.PackageVolume = receiveDetailInfo.PackageVolume;
        }
        /// <summary>
        /// ReceiveInfo -> OutputInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="outputInfo"></param>
        public static void GetOutputInfo(ReceiveInfo receiveInfo, ref OutputInfo outputInfo)
        {
            if (receiveInfo == null) return;
            ///PLANT
            outputInfo.Plant = receiveInfo.Plant;
            ///SUPPLIER_NUM
            outputInfo.SupplierNum = receiveInfo.SupplierNum;
            ///WM_NO
            outputInfo.WmNo = receiveInfo.WmNo;
            ///ZONE_NO
            outputInfo.ZoneNo = receiveInfo.ZoneNo;
            ///T_DOCK
            ///TODO:这里的道口不应该是入库道口，需要在物料仓储信息中配置出库道口
            //outputInfo.TDock = receiveInfo.Dock;
            ///PART_BOX_CODE
            //outputInfo.PartBoxCode = receiveInfo.PartBoxCode;
            ///BOOK_KEEPER
            ///TODO:应该有一个目标地点的保管员，需要在考虑一下
            outputInfo.BookKeeper = receiveInfo.BookKeeper;
            ///PLAN_NO
            outputInfo.PlanNo = receiveInfo.ReceiveNo;
            ///ASN_NO
            outputInfo.AsnNo = receiveInfo.AsnNo;
            ///RUNSHEET_NO
            outputInfo.RunsheetNo = receiveInfo.RunsheetNo;
            ///SUPPLIER_TYPE
            outputInfo.SupplierType = receiveInfo.SupplierType;
            ///ROUTE
            //outputInfo.Route = receiveInfo.Route;
            ///CUST_CODE
            //outputInfo.CustCode = receiveInfo.CustCode;
            ///CUST_NAME
            //outputInfo.CustName = receiveInfo.CustName;
            ///COST_CENTER
            //outputInfo.CostCenter = receiveInfo.CostCenter;
        }
        /// <summary>
        /// Create OutputInfo
        /// </summary>
        /// <param name="outputFid"></param>
        /// <param name="outputNo"></param>
        /// <param name="targeWmNo"></param>
        /// <param name="targeZoneNo"></param>
        /// <param name="outputType"></param>
        /// <param name="organizationFid"></param>
        /// <param name="orderStatus"></param>
        /// <param name="conveyance"></param>
        /// <param name="carrierTel"></param>
        /// <param name="sumPartQty"></param>
        /// <param name="sumWeight"></param>
        /// <param name="sumVolume"></param>
        /// <param name="sumPackageQty"></param>
        /// <param name="planShippingTime"></param>
        /// <param name="planDeliveryTime"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static OutputInfo CreateOutputInfo(Guid outputFid, string outputNo, string targeWmNo, string targeZoneNo, int outputType, Guid? organizationFid, int orderStatus, string conveyance, string carrierTel,
            decimal sumPartQty, decimal sumWeight, decimal sumVolume, int sumPackageQty, DateTime? planShippingTime, DateTime? planDeliveryTime, string loginUser)
        {
            OutputInfo outputInfo = new OutputInfo
            {
                ///OUTPUT_FID
                Fid = outputFid,
                ///OUTPUT_NO
                OutputNo = outputNo,
                ///T_WM_NO
                WmNo = targeWmNo,
                ///T_ZONE_NO
                ZoneNo = targeZoneNo,
                ///SEND_TIME
                SendTime = DateTime.Now,
                ///OUTPUT_TYPE
                OutputType = outputType,
                ///ORGANIZATION_FID
                //OrganizationFid = organizationFid,
                /////SUM_PART_QTY
                //SumPartQty = sumPartQty,
                /////SUM_WEIGHT
                //SumWeight = sumWeight,
                /////SUM_VOLUME
                //SumVolume = sumVolume,
                /////SUM_PACKAGE_QTY
                //SumPackageQty = sumPackageQty,
                ///STATUS
                //Status = orderStatus,
                ///CONVEYANCE
                //Conveyance = conveyance,
                /////CARRIER_TEL
                //CarrierTel = carrierTel,
                ///PLAN_SHIPPING_TIME
                //PlanShippingTime = planShippingTime,
                /////PLAN_DELIVERY_TIME
                //PlanDeliveryTime = planDeliveryTime,
                ///CREATE_USER
                CreateUser = loginUser
            };

            return outputInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateOutputByReceiveSql(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            ///当入库类型为物料预留时，按入库单产生出库单
            if (receiveInfo.ReceiveType.GetValueOrDefault() == (int)InboundTypeConstants.ReserveInbound)
            {
                SapPurchaseOrderInfo sapPurchaseOrderInfo = new SapPurchaseOrderDAL().GetInfo(receiveInfo.RunsheetNo);
                if (sapPurchaseOrderInfo == null)
                    throw new Exception("MC:0x00000433");///采购订单数据错误
                return CreateOutputByReceiveSql(
                    receiveInfo,
                    receiveDetailInfos,
                    barcodeInfos,
                    sapPurchaseOrderInfo.SWmNo,
                    sapPurchaseOrderInfo.SZoneNo,
                    (int)OutboundTypeConstants.ReserveOutbound,
                    loginUser,
                    null,
                    string.Empty,
                    string.Empty,
                    null,
                    null);
            }
            ///
            string sql = string.Empty;
            ///获取目标地点物料仓储信息，仅获取需要同步出库的数据
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] = N'" + receiveInfo.WmNo + "' and " +
                "[ZONE_NO] = N'" + receiveInfo.ZoneNo + "' and " +
                "[IS_OUTPUT] = 1", string.Empty);
            
            //var qPartsStockInfos = from p in partsStockInfos group p by new { p.SynchronousWmNo, p.SynchronousZoneNo } into g select new { g.Key };
            /////同步出库单号继承自入库单号
            //string sync_output_no_inherit_receive_no = new ConfigDAL().GetValueByCode("SYNC_OUTPUT_NO_INHERIT_RECEIVE_NO");
            /////当继承单号时需要校验同步出库配置基础数据不能跨越仓库存储区
            //if (!string.IsNullOrEmpty(sync_output_no_inherit_receive_no) && sync_output_no_inherit_receive_no.ToLower() == "true")
            //{
            //    if (qPartsStockInfos.Count() > 1)
            //        throw new Exception("MC:0x00000437");///相同入库单的物料同步出库基础配置不能越库
            //}

            //foreach (var qPartsStockInfo in qPartsStockInfos)
            //{
            //    List<PartsStockInfo> partsStocks = partsStockInfos.Where(d => d.SynchronousWmNo == qPartsStockInfo.Key.SynchronousWmNo && d.SynchronousZoneNo == qPartsStockInfo.Key.SynchronousZoneNo).ToList();
            //    List<ReceiveDetailInfo> receiveDetails = (from m in receiveDetailInfos join p in partsStocks on new { m.PartNo, m.SupplierNum } equals new { p.PartNo, p.SupplierNum } select m).ToList();
            //    if (receiveDetails == null || receiveDetails.Count == 0)
            //        throw new Exception("MC:0x00000241");///相同入库单的物料同步出库基础配置不能越库
            //    List<BarcodeInfo> barcodes = barcodeInfos.Where(d => receiveDetails.Select(w => w.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
            //    sql += CreateOutputByReceiveSql(
            //        receiveInfo,
            //        receiveDetails,
            //        barcodes,
            //        qPartsStockInfo.Key.SynchronousWmNo,
            //        qPartsStockInfo.Key.SynchronousZoneNo,
            //        (int)OutboundTypeConstants.NormalOutbound,///outputType
            //        loginUser,
            //        receiveInfo.OrganizationFid.GetValueOrDefault(),///organizationFid TODO:
            //        string.Empty,///conveyance 
            //        string.Empty,///carrierTel
            //        DateTime.Now,///planShippingTime  TODO:窗口时间 + 延迟时间
            //        DateTime.Now///planDeliveryTime TODO:窗口时间 
            //        );
            //}
            return sql;
        }
        #endregion

        #region Output -> Output
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateOutputByOutputSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            ///
            string sql = string.Empty;
            ///获取目标地点物料仓储信息，仅获取需要同步出库的数据
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] = N'" + outputInfo.WmNo + "' and " +
                "[ZONE_NO] = N'" + outputInfo.ZoneNo + "' and " +
                "[IS_OUTPUT] = 1", string.Empty);
            //var qPartsStockInfos = from p in partsStockInfos group p by new { p.SynchronousWmNo, p.SynchronousZoneNo } into g select new { g.Key };
            //foreach (var qPartsStockInfo in qPartsStockInfos)
            //{
            //    List<PartsStockInfo> partsStocks = partsStockInfos.Where(d => d.SynchronousWmNo == qPartsStockInfo.Key.SynchronousWmNo && d.SynchronousZoneNo == qPartsStockInfo.Key.SynchronousZoneNo).ToList();
            //    List<OutputDetailInfo> outputDetails = (
            //        from m in outputDetailInfos
            //        join p in partsStocks
            //        on new { m.PartNo, m.SupplierNum }
            //        equals new { p.PartNo, p.SupplierNum }
            //        select m).
            //        Where(d =>
            //        d.WmNo != qPartsStockInfo.Key.SynchronousWmNo &&
            //        d.ZoneNo != qPartsStockInfo.Key.SynchronousZoneNo).ToList();
            //    if (outputDetails.Count == 0) continue;
            //    List<BarcodeInfo> barcodes = barcodeInfos.Where(d => outputDetails.Select(w => w.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();
            //    sql += CreateOutputByOutputSql(
            //        outputInfo,
            //        outputDetails,
            //        barcodes,
            //        qPartsStockInfo.Key.SynchronousWmNo,
            //        qPartsStockInfo.Key.SynchronousZoneNo,
            //        (int)OutboundTypeConstants.NormalOutbound,///outputType
            //        loginUser,
            //        outputInfo.OrganizationFid.GetValueOrDefault(),///organizationFid TODO:
            //        string.Empty,///conveyance 
            //        string.Empty,///carrierTel
            //        DateTime.Now,///planShippingTime  TODO:
            //        DateTime.Now///planDeliveryTime TODO:
            //        );
            //}
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="targeWmNo"></param>
        /// <param name="targeZoneNo"></param>
        /// <param name="outputType"></param>
        /// <param name="loginUser"></param>
        /// <param name="organizationFid"></param>
        /// <param name="conveyance"></param>
        /// <param name="carrierTel"></param>
        /// <param name="planShippingTime"></param>
        /// <param name="planDeliveryTime"></param>
        /// <returns></returns>
        private static string CreateOutputByOutputSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos,
            string targeWmNo, string targeZoneNo, int? outputType, string loginUser, Guid? organizationFid, string conveyance, string carrierTel, DateTime? planShippingTime, DateTime? planDeliveryTime)
        {
            string sql = string.Empty;
            Guid outputFid = Guid.NewGuid();
            ///出库单号
            string outputNo = new SeqDefineDAL().GetCurrentCode("OUTPUT_NO");
            int rowNo = 0;
            decimal sumPartQty = 0;
            decimal sumWeight = 0;
            decimal sumVolume = 0;
            ///拉动单号、委托编号
            string runsheetNo = string.Empty;
            ///单据状态默认为已发布，标签状态默认为已发货
            int orderStatus = (int)WmmOrderStatusConstants.Published;
            int barcodeStatus = (int)BarcodeStatusConstants.Shiped;
            if (string.IsNullOrEmpty(targeWmNo) && string.IsNullOrEmpty(targeZoneNo))
            {
                orderStatus = (int)WmmOrderStatusConstants.Created;
                barcodeStatus = (int)BarcodeStatusConstants.PickedUp;
            }
            foreach (var outputDetailInfo in outputDetailInfos)
            {
                int boxNum = barcodeInfos.Count;
                decimal partQty = barcodeInfos.Sum(d => d.CurrentQty.GetValueOrDefault());
                ///优先从扫描的条码进行获取，当没有扫描的条码时则根据入库单明细实收数量进行填充
                if (boxNum == 0) boxNum = outputDetailInfo.ActualBoxNum.GetValueOrDefault();
                if (partQty == 0) partQty = outputDetailInfo.ActualQty.GetValueOrDefault();

                sumPartQty += partQty;
                rowNo++;
                sumWeight += boxNum * outputDetailInfo.PerpackageGrossWeight.GetValueOrDefault();
                ///长度单位为cm，体积单位为m³
                sumVolume += boxNum * outputDetailInfo.PackageLength.GetValueOrDefault() * outputDetailInfo.PackageWidth.GetValueOrDefault() * outputDetailInfo.PackageHeight.GetValueOrDefault() / 1000;
                ///
                runsheetNo = outputDetailInfo.RunsheetNo;
                ///
                #region TT_WMM_OUTPUT_DETAIL
                sql += "insert into LES.[TT_WMM_OUTPUT_DETAIL] (" +
                    "FID, OUTPUT_FID, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, DLOC, TRAN_NO, TARGET_WM, TARGET_ZONE, TARGET_DLOC, PART_NO, PART_CNAME, " +
                    "REQUIRED_BOX_NUM, REQUIRED_QTY, ACTUAL_BOX_NUM, ACTUAL_QTY, PACKAGE, PACKAGE_MODEL, BARCODE_DATA, MEASURING_UNIT_NO, " +
                    "IDENTIFY_PART_NO, PART_ENAME, DOCK, ASSEMBLY_LINE, BOX_PARTS, SEQUENCE_NO, PICKUP_SEQ_NO, RDC_DLOC, INHOUSE_PACKAGE, INHOUSE_PACKAGE_MODEL, " +
                    "SUPPLIER_NUM_SHEET, BOX_PARTS_SHEET, ORDER_NO, ITEM_NO, RUNSHEET_NO, REPACKAGE_FLAG, ROW_NO, ORIGIN_PLACE, SALE_UNIT_PRICE, PART_PRICE, PART_CLS, " +
                    "PICKUP_NUM, PICKUP_QTY, IS_SCAN_BOX, PACKAGE_LENGTH, PACKAGE_WIDTH, PACKAGE_HEIGHT, PERPACKAGE_GROSS_WEIGHT, COMMENTS, " +
                    "VALID_FLAG, CREATE_USER, CREATE_DATE) select " +
                    "N'" + outputDetailInfo.Fid.GetValueOrDefault() + "', " +///FID
                    "N'" + outputFid + "', " +///OUTPUT_FID
                    "PLANT, " +
                    "SUPPLIER_NUM, " +
                    "N'" + outputInfo.WmNo + "', " +///WM_NO
                    "N'" + outputInfo.ZoneNo + "', " +///ZONE_NO
                    "NULL, " +///DLOC
                    "N'" + outputNo + "', " +///TRAN_NO
                    "N'" + targeWmNo + "', " +///TARGET_WM
                    "N'" + targeZoneNo + "', " +///TARGET_ZONE
                    "NULL, " +///TARGET_DLOC
                    "PART_NO, " +
                    "PART_CNAME, " +////r/n
                    "" + boxNum + ", " +///REQUIRED_BOX_NUM
                    "" + partQty + ", " +///REQUIRED_QTY 
                    "NULL, " +///ACTUAL_BOX_NUM
                    "NULL, " +///ACTUAL_QTY
                    "PACKAGE, " +
                    "PACKAGE_MODEL, " +
                    "NULL, " +///BARCODE_DATA
                    "MEASURING_UNIT_NO, " +////r/n
                    "IDENTIFY_PART_NO, " +
                    "PART_ENAME, " +
                    "DOCK, " +
                    "ASSEMBLY_LINE, " +
                    "BOX_PARTS, " +
                    "SEQUENCE_NO, " +
                    "PICKUP_SEQ_NO, " +
                    "RDC_DLOC, " +
                    "INHOUSE_PACKAGE, " +
                    "INHOUSE_PACKAGE_MODEL, " +////r/n
                    "SUPPLIER_NUM_SHEET, " +
                    "BOX_PARTS_SHEET, " +
                    "ORDER_NO, " +
                    "ITEM_NO, " +
                    "N'" + runsheetNo + "', " +///RUNSHEET_NO
                    "NULL, " +///REPACKAGE_FLAG
                    "" + rowNo + ", " +///ROW_NO
                    "ORIGIN_PLACE, " +
                    "NULL, " +///SALE_UNIT_PRICE
                    "NULL, " +///PART_PRICE
                    "PART_CLS, " +////r/n
                    "NULL, " +///PICKUP_NUM
                    "NULL, " +///PICKUP_QTY
                    "IS_SCAN_BOX, " +///IS_SCAN_BOX
                    "PACKAGE_LENGTH, " +
                    "PACKAGE_WIDTH, " +
                    "PACKAGE_HEIGHT, " +
                    "PERPACKAGE_GROSS_WEIGHT, " +
                    "NULL, " +///COMMENTS，/r/n
                    "1, N'" + loginUser + "', GETDATE() " +///VALID_FLAG, CREATE_USER, CREATE_DATE
                    "from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) " +
                    "where [ID] = " + outputDetailInfo.Id + ";";
                #endregion
                foreach (var barcodeInfo in barcodeInfos)
                {
                    sql += BarcodeDAL.GetBarcodeUpdateSql(
                        barcodeStatus,
                        barcodeInfo.WmNo,
                        barcodeInfo.ZoneNo,
                        barcodeInfo.Dloc,
                        outputNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser);
                }
            }
            #region TT_WMM_OUTPUT
            sql += "insert into LES.[TT_WMM_OUTPUT] (" +
                "FID, OUTPUT_NO, PLANT, SUPPLIER_NUM, WM_NO, ZONE_NO, T_WM_NO, T_ZONE_NO, T_DOCK, PART_BOX_CODE, SEND_TIME, OUTPUT_TYPE, TRAN_TIME, " +
                "OUTPUT_REASON, BOOK_KEEPER, CONFIRM_FLAG, PLAN_NO, ASN_NO, RUNSHEET_NO, ASSEMBLY_LINE, PLANT_ZONE, WORKSHOP, TRANS_SUPPLIER_NUM, " +
                "PART_TYPE, SUPPLIER_TYPE, RUNSHEET_CODE, ERP_FLAG, LOGICAL_PK, BUSINESS_PK, ROUTE, REQUEST_TIME, CUST_CODE, CUST_NAME, COST_CENTER, " +
                "ORGANIZATION_FID, CONFIRM_USER, CONFIRM_DATE, LIABLE_USER, LIABLE_DATE, FINANCE_USER, FINANCE_DATE, SUM_PART_QTY, SUM_OF_PRICE, STATUS, " +
                "CONVEYANCE, CARRIER_TEL, SUM_WEIGHT, SUM_VOLUME, PLAN_SHIPPING_TIME, PLAN_DELIVERY_TIME, COMMENTS, " +
                "VALID_FLAG, CREATE_USER, CREATE_DATE) select " +
                "N'" + outputFid + "', " +///FID
                "N'" + outputNo + "', " +///OUTPUT_NO
                "PLANT, " +
                "SUPPLIER_NUM, " +
                "N'" + outputInfo.WmNo + "', " +///WM_NO
                "N'" + outputInfo.ZoneNo + "', " +///ZONE_NO
                "N'" + targeWmNo + "', " +///T_WM_NO
                "N'" + targeZoneNo + "', " +///T_ZONE_NO
                "T_DOCK, " +///T_DOCK
                "PART_BOX_CODE, " +///PART_BOX_CODE
                "GETDATE(), " +///SEND_TIME
                 "" + (outputType == null ? "NULL" : "" + outputType + "") + ", " +///OUTPUT_TYPE
                "NULL, " +///TRAN_TIME，/r/n
                "NULL, " +///OUTPUT_REASON
                "BOOK_KEEPER, " +
                "NULL, " +///CONFIRM_FLAG
                "PLAN_NO, " +///PLAN_NO
                "ASN_NO, " +///ASN_NO
                "N'" + runsheetNo + "', " +
                "NULL, " +///ASSEMBLY_LINE
                "NULL, " +///PLANT_ZONE
                "NULL, " +///WORKSHOP
                "NULL, " +///TRANS_SUPPLIER_NUM，/r/n
                "NULL, " +///PART_TYPE
                "SUPPLIER_TYPE, " +
                "NULL, " +///RUNSHEET_CODE
                "NULL, " +///ERP_FLAG
                "NULL, " +///LOGICAL_PK
                "NULL, " +///BUSINESS_PK
                "ROUTE, " +
                "NULL, " +///REQUEST_TIME
                "CUST_CODE, " +
                "CUST_NAME, " +
                "COST_CENTER, " +///，/r/n
                 "" + (organizationFid == null ? "NULL" : "N'" + organizationFid.GetValueOrDefault() + "'") + ", " +///ORGANIZATION_FID
                "NULL, " +///CONFIRM_USER
                "NULL, " +///CONFIRM_DATE
                "NULL, " +///LIABLE_USER
                "NULL, " +///LIABLE_DATE
                "NULL, " +///FINANCE_USER
                "NULL, " +///FINANCE_DATE
                "" + sumPartQty + ", " +///SUM_PART_QTY
                "NULL, " +///SUM_OF_PRICE
                "" + orderStatus + ", " +///STATUS
                "N'" + conveyance + "', " +///CONVEYANCE
                "N'" + carrierTel + "', " +///CARRIER_TEL
                "" + sumWeight + ", " +///SUM_WEIGHT
                "" + sumVolume + ", " +///SUM_VOLUME
                "" + (planShippingTime == null ? "NULL" : "N'" + planShippingTime.GetValueOrDefault() + "'") + ", " +///PLAN_SHIPPING_TIME
                "" + (planDeliveryTime == null ? "NULL" : "N'" + planDeliveryTime.GetValueOrDefault() + "'") + ", " +///PLAN_DELIVERY_TIME
                "NULL, " +///COMMENTS
                "1, N'" + loginUser + "', GETDATE() " +///VALID_FLAG, CREATE_USER, CREATE_DATE
                "from [LES].[TT_WMM_OUTPUT] with(nolock) " +
                "where [ID] = " + outputInfo.OutputId + ";";
            #endregion
            return sql;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取交易记录INSERT语句
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tranType"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetTranDetailsInsertSql(long id, int tranType, string loginUser)
        {
            OutputInfo outputInfo = dal.GetInfo(id);
            if (outputInfo == null) return string.Empty;
            List<OutputDetailInfo> outputDetailInfos = new OutputDetailDAL().GetList("[OUTPUT_FID] = N'" + outputInfo.Fid.GetValueOrDefault() + "'", "[ROW_NO]");
            if (outputDetailInfos.Count == 0) return string.Empty;
            List<StocksInfo> stocksInfos = new List<StocksInfo>();
            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///不关注是否结算
            bool? settledFlag = null;
            return GetTranDetailsInsertSql(outputInfo, outputDetailInfos, barcodeInfos, settledFlag, tranType, false, loginUser);
        }
        /// <summary>
        /// 通过出库单及明细获取交易记录语句
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="stocksInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="batchNo"></param>
        /// <param name="settledFlag"></param>
        /// <param name="tranType"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetTranDetailsInsertSql(OutputInfo outputInfo, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos,
             bool? settledFlag, int tranType, bool vmiFlag, string loginUser)
        {
            ///不产生交易记录
            if (tranType == (int)WmmTranTypeConstants.None) return string.Empty;

            #region 当前来源仓库存储区库存数据
            ///TODO:对于多站点触发同一物料的情况，这样的计算方式会有缺陷，需要调整后适应云端计算
            List<StocksInfo> stocksInfos = new List<StocksInfo>();
            ///出库校验可用库存满足需求数量
            string output_valid_available_stock_satisfy_require_qty = new ConfigDAL().GetValueByCode("OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY");
            if (!string.IsNullOrEmpty(output_valid_available_stock_satisfy_require_qty) && output_valid_available_stock_satisfy_require_qty.ToLower() == "true" && !vmiFlag)
            {
                stocksInfos = new StocksDAL().GetList("" +
                            "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                            "[WM_NO] = N'" + outputInfo.WmNo + "' and " +
                            "[ZONE_NO] = N'" + outputInfo.ZoneNo + "'", string.Empty);
            }
            ///VMI出库校验可用库存满足需求数量
            string vmi_output_valid_available_stock_satisfy_require_qty = new ConfigDAL().GetValueByCode("VMI_OUTPUT_VALID_AVAILABLE_STOCK_SATISFY_REQUIRE_QTY");
            if (!string.IsNullOrEmpty(vmi_output_valid_available_stock_satisfy_require_qty) && vmi_output_valid_available_stock_satisfy_require_qty.ToLower() == "true" && vmiFlag)
            {
                stocksInfos = new StocksDAL().GetList("" +
                            "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                            "[WM_NO] = N'" + outputInfo.WmNo + "' and " +
                            "[ZONE_NO] = N'" + outputInfo.ZoneNo + "'", string.Empty);
            }
            #endregion

            #region 物料拉动信息
            ///根据物料拉动信息中的库存锁定标记，决定哪些物料是由冻结数量出库
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                    "[PART_NO] in ('" + string.Join("','", outputDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                    "[PLANT] = N'" + outputInfo.Plant + "' and " +
                    "[WORKSHOP] = N'" + outputInfo.Workshop + "' and " +
                    "[ASSEMBLY_LINE] = N'" + outputInfo.AssemblyLine + "'", string.Empty);
            ///库存排查锁定物料是否同步更新库存
            string stockCheckLockMaterialSyncUpdatePartStockFlag = new ConfigDAL().GetValueByCode("STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG");
            #endregion

            #region 物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(outputDetailInfos.Select(d => d.PartNo).ToList());
            #endregion

            #region 供应商
            List<string> supplierNums = stocksInfos.Where(d => !string.IsNullOrEmpty(d.SupplierNum)).Select(d => d.SupplierNum).ToList();
            supplierNums.AddRange(outputDetailInfos.Where(d => !string.IsNullOrEmpty(d.SupplierNum)).Select(d => d.SupplierNum).ToList());
            ///
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(supplierNums);
            #endregion

            StringBuilder @string = new StringBuilder();
            foreach (var outputDetailInfo in outputDetailInfos)
            {
                if (outputDetailInfo.ActualQty.GetValueOrDefault() == 0) continue;
                TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo(tranType, (int)WmmTranStateConstants.Created, loginUser);
                ///OutputDetailInfo
                TranDetailsBLL.GetTranDetailsInfo(outputDetailInfo, ref tranDetailsInfo);
                ///OutputInfo
                TranDetailsBLL.GetTranDetailsInfo(outputInfo, ref tranDetailsInfo);
                ///是否关注结算
                TranDetailsBLL.GetTranDetailsInfo(settledFlag, ref tranDetailsInfo);
                ///根据物料拉动信息更新交易配置
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo
                    = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.PartNo == outputDetailInfo.PartNo && d.SupplierNum == outputDetailInfo.SupplierNum);
                TranDetailsBLL.GetTranDetailsInfo(maintainInhouseLogisticStandardInfo, stockCheckLockMaterialSyncUpdatePartStockFlag, ref tranDetailsInfo);
                ///不产生交易记录
                if (tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.None) continue;
                ///校验库存
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.PartNo == outputDetailInfo.PartNo).ToList();
                List<OutputDetailInfo> outputDetails = outputDetailInfos.Where(d => d.PartNo == outputDetailInfo.PartNo).ToList();
                List<StocksInfo> stocks = new StocksBLL().GetValidStocksInfo(
                    outputDetailInfo,
                    tranDetailsInfo.TranType.GetValueOrDefault(),
                    stocksInfos,
                    outputDetails,
                    barcodes,
                    settledFlag);
                if (output_valid_available_stock_satisfy_require_qty.ToLower() == "true" && (stocks == null ? 0 : stocks.Count) == 0)
                    throw new Exception("MC:0x00000259");///没有足够的可用库存数量完成本次出库
                string supplierNum = string.Empty;
                string dloc = string.Empty;
                foreach (var stock in stocks.OrderBy(d => d.SupplierNum).ToList())
                {
                    TranDetailsBLL.GetTranDetailsInfo(stock, ref tranDetailsInfo);
                    ///供应商
                    TranDetailsBLL.GetTranDetailsInfo(supplierInfos.FirstOrDefault(d => d.SupplierNum == tranDetailsInfo.SupplierNum), ref tranDetailsInfo);
                    ///
                    @string.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
                    supplierNum = stock.SupplierNum;
                    dloc = stock.Dloc;
                }
                ///如果库存中有多个供应商匹配，则不更新单据的供应商
                var q = from p in stocks group p by p.SupplierNum into g select g;
                if (q.Count() > 1) supplierNum = string.Empty;
                ///如果库存中有多个供应商匹配，则不更新单据的供应商
                q = from p in stocks group p by p.Dloc into g select g;
                if (q.Count() > 1) dloc = string.Empty;
                ///将校验库存通过后的库位信息填充到物料出库单上
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                           d.PartNo == tranDetailsInfo.PartNo &&
                           d.SupplierNum == tranDetailsInfo.SupplierNum &&
                           d.WmNo == tranDetailsInfo.TargetWm &&
                           d.ZoneNo == tranDetailsInfo.TargetZone);
                string targetDloc = (partsStockInfo == null ? string.Empty : partsStockInfo.Dloc);
                if (!string.IsNullOrEmpty(outputDetailInfo.TargetDloc))
                    targetDloc = outputDetailInfo.TargetDloc;
                ///如果是VMI标识调用
                if (vmiFlag)
                    @string.AppendLine("update [LES].[TT_WMM_VMI_OUTPUT_DETAIL] " +
                            "set [DLOC] = N'" + dloc + "',[TARGET_DLOC] = N'" + targetDloc + "',[SUPPLIER_NUM] = N'" + supplierNum + "' " +
                            "where [ID] = " + outputDetailInfo.Id + ";");
                else
                    @string.AppendLine("update [LES].[TT_WMM_OUTPUT_DETAIL] " +
                            "set [DLOC] = N'" + dloc + "',[TARGET_DLOC] = N'" + targetDloc + "',[SUPPLIER_NUM] = N'" + supplierNum + "' " +
                            "where [ID] = " + outputDetailInfo.Id + ";");
            }
            return @string.ToString();
        }
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
            string outputStatusUpdateSql = "update [LES].[TT_WMM_OUTPUT] set " +
            "[STATUS] = {0}," +
            timeFieldUpdateSql +
            "[MODIFY_DATE] = GETDATE()," +
            "[MODIFY_USER] = N'{1}' " +
            "where [ID] ={2};";
            return string.Format(outputStatusUpdateSql, (int)outputOrderStatus, loginUser, id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outputDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetOutputSumQtyUpdateSql(long id, List<OutputDetailInfo> outputDetailInfos, string loginUser)
        {
            string outputSumQtyUpdateSql = "update [LES].[TT_WMM_OUTPUT] set " +
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
            string outputDetailActualQtyUpdateSql = "update [LES].[TT_WMM_OUTPUT_DETAIL] set " +
                "[ACTUAL_BOX_NUM] = {0}," +
                "[ACTUAL_QTY] = {1}," +
                "[MODIFY_USER] = N'{2}'," +
                "MODIFY_DATE = GETDATE() " +
                "where [ID] = {3};";
            ///更新出库单明细实际箱数、实际数量、修改人、修改时间
            return string.Format(outputDetailActualQtyUpdateSql, actualBoxNum, actualQty, loginUser, id);
        }
        /// <summary>
        /// Get OutputInfo
        /// </summary>
        /// <param name="outputNo"></param>
        /// <returns></returns>
        public OutputInfo GetInfo(string outputNo)
        {
            return dal.GetInfo(outputNo);
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

            List<OutputInfo> planPullOrderDetailInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (planPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000053");///请选中行数据
            //List<OutputInfo> outputInfos = planPullOrderDetailInfos.Where(d => d.Status == (int)WmmOrderStatusConstants.Created).ToList();
            List<OutputInfo> outputInfos = planPullOrderDetailInfos.ToList();

            if (planPullOrderDetailInfos.Count != outputInfos.Count)
                throw new Exception("MC:0x00000126");///出库单状态必须为已创建才能删除

            return dal.LogicDeleteInfo(rowsKeyValues, loginUser);
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
            string sql = "select T1.*,T2.[ITEM_NAME] as OUTPUT_TYPE_NAME from [LES].[TT_WMM_OUTPUT] T1 with(nolock) " +
                "left join dbo.[TS_SYS_CODE_ITEM] T2 with(nolock) on T2.[CODE_FID] = N'37C8AF92-D0F0-45FE-8164-48C43FC5EDF6' and T1.[OUTPUT_TYPE] = T2.[ITEM_VALUE] and T2.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [OUTPUT_FID] in (select [FID] from [LES].[TT_WMM_OUTPUT] with(nolock) " +
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
            string sql = "update [LES].[TT_WMM_OUTPUT] set " +
                "[PRINT_TIME] = GETDATE()," +
                "[PRINT_COUNT] = isnull([PRINT_COUNT],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion

        #region Vmi单转非Vmi单
        /// <summary>
        /// Vmi出库单转非Vmi出库单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public OutputInfo GetOutputInfoByVmiOutPut(VmiOutputInfo info)
        {
            OutputInfo model = new OutputInfo
            {
                OutputId = info.Id,
                //Fid = info.Fid,
                OutputNo = info.OutputNo,
                Plant = info.Plant,
                SupplierNum = info.SupplierNum,
                WmNo = info.WmNo,
                ZoneNo = info.ZoneNo,
                
                
                
               
                SendTime = info.SendTime,
                OutputType = info.OutputType,
                TranTime = info.TranTime,
                OutputReason = info.OutputReason,
                BookKeeper = info.BookKeeper,
                ConfirmFlag = info.ConfirmFlag,
                PlanNo = info.PlanNo,
                AsnNo = info.AsnNo,
                RunsheetNo = info.RunsheetNo,
                AssemblyLine = info.AssemblyLine,
                PlantZone = info.PlantZone,
                Workshop = info.Workshop,
                TransSupplierNum = info.TransSupplierNum,
                PartType = info.PartType,
                SupplierType = info.SupplierType,
                RunsheetCode = info.RunsheetCode,
                ErpFlag = info.ErpFlag,
                LogicalPk = info.LogicalPk,
                BusinessPk = info.BusinessPk,
                Route = info.Route,
                RequestTime = info.RequestTime,
                //CustCode = info.CustCode,
                //CustName = info.CustName,
                //CostCenter = info.CostCenter,
                //OrganizationFid = info.OrganizationFid,
                //ConfirmUser = info.ConfirmUser,
                //ConfirmDate = info.ConfirmDate,
                //LiableUser = info.LiableUser,
                //LiableDate = info.LiableDate,
                //FinanceUser = info.FinanceUser,
                //FinanceDate = info.FinanceDate,
                //SumPartQty = info.SumPartQty,
                //SumOfPrice = info.SumOfPrice,
                //Status = info.Status,
                //Conveyance = info.Conveyance,
                //CarrierTel = info.CarrierTel,
                //SumWeight = info.SumWeight,
                //SumVolume = info.SumVolume,
                //PlanShippingTime = info.PlanShippingTime,
                //PlanDeliveryTime = info.PlanDeliveryTime,
                //PrintCount = info.PrintCount,
                //PrintTime = info.PrintTime,
                Comments = info.Comments,
                //ValidFlag = info.ValidFlag,
                CreateUser = info.CreateUser,
                CreateDate = info.CreateDate
                //ModifyUser = info.ModifyUser,
                //ModifyDate = info.ModifyDate,
                //LastPrintUser = info.LastPrintUser,
                //SumPackageQty = info.SumPackageQty,
                //PullMode = info.PullMode
            };

            return model;
        }

        /// <summary>
        /// Vmi出库单明细转非Vmi出库单明细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public OutputDetailInfo GetOutputDetailInfoByVmiOutputDetail(VmiOutputDetailInfo info)
        {
            OutputDetailInfo model = new OutputDetailInfo
            {
                Id = info.Id,
                Fid = info.Fid,
                OutputFid = info.OutputFid,
                Plant = info.Plant,
                SupplierNum = info.SupplierNum,
                WmNo = info.WmNo,
                ZoneNo = info.ZoneNo,
                Dloc = info.Dloc,
                TranNo = info.TranNo,
                TargetWm = info.TargetWm,
                TargetZone = info.TargetZone,
                TargetDloc = info.TargetDloc,
                PartNo = info.PartNo,
                PartCname = info.PartCname,
                RequiredBoxNum = info.RequiredBoxNum,
                RequiredQty = info.RequiredQty,
                ActualBoxNum = info.ActualBoxNum,
                ActualQty = info.ActualQty,
                Package = info.Package,
                PackageModel = info.PackageModel,
                BarcodeData = info.BarcodeData,
                MeasuringUnitNo = info.MeasuringUnitNo,
                IdentifyPartNo = info.IdentifyPartNo,
                PartEname = info.PartEname,
                Dock = info.Dock,
                AssemblyLine = info.AssemblyLine,
                BoxParts = info.BoxParts,
                SequenceNo = info.SequenceNo,
                PickupSeqNo = info.PickupSeqNo,
                RdcDloc = info.RdcDloc,
                InhousePackage = info.InhousePackage,
                InhousePackageModel = info.InhousePackageModel,
                SupplierNumSheet = info.SupplierNumSheet,
                BoxPartsSheet = info.BoxPartsSheet,
                OrderNo = info.OrderNo,
                ItemNo = info.ItemNo,
                RunsheetNo = info.RunsheetNo,
                RepackageFlag = info.RepackageFlag,
                RowNo = info.RowNo,
                OriginPlace = info.OriginPlace,
                SaleUnitPrice = info.SaleUnitPrice,
                PartPrice = info.PartPrice,
                PartCls = info.PartCls,
                PickupNum = info.PickupNum,
                PickupQty = info.PickupQty,
                IsScanBox = info.IsScanBox,
                PackageLength = info.PackageLength,
                PackageWidth = info.PackageWidth,
                PackageHeight = info.PackageHeight,
                PerpackageGrossWeight = info.PerpackageGrossWeight,
                Comments = info.Comments,
                ValidFlag = info.ValidFlag,
                CreateUser = info.CreateUser,
                CreateDate = info.CreateDate,
                ModifyUser = info.ModifyUser,
                ModifyDate = info.ModifyDate,
                PackageVolume = info.PackageVolume,
                SumWeight = info.SumWeight,
                SumVolume = info.SumVolume,
                FrozenStockFlag = info.FrozenStockFlag,
                TranTime = info.TranTime,
                CostCenter = info.CostCenter
            };

            return model;
        }
        /// <summary>
        /// Vmi出库单明细List转非Vmi出库单明细List
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<OutputDetailInfo> GetOutputDetailInfosByVmiOutputDetails(List<VmiOutputDetailInfo> infos)
        {
            List<OutputDetailInfo> list = new List<OutputDetailInfo>();
            infos.ForEach(delegate (VmiOutputDetailInfo temp)
            {
                list.Add(GetOutputDetailInfoByVmiOutputDetail(temp));
            });

            return list;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create OutputInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>OutputInfo</returns>
        public static OutputInfo CreateOutputInfo(string loginUser)
        {
            OutputInfo info = new OutputInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,数据有效标记
            //info.ValidFlag = true;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;

            return info;
        }
        /// <summary>
        /// VmiOutputInfo -> OutputInfo
        /// </summary>
        /// <param name="vmiOutputInfo"></param>
        /// <param name="info"></param>
        public static void GetOutputInfo(VmiOutputInfo vmiOutputInfo, ref OutputInfo info)
        {
            if (vmiOutputInfo == null) return;
            ///ID,出库流水号
            info.OutputId = vmiOutputInfo.Id;
            ///FID,外键ID
            info.Fid = vmiOutputInfo.Fid;
            ///OUTPUT_NO,出库单号
            info.OutputNo = vmiOutputInfo.OutputNo;
            ///PLANT,工厂模型_工厂
            info.Plant = vmiOutputInfo.Plant;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = vmiOutputInfo.SupplierNum;
            ///WM_NO,仓库编码
            info.WmNo = vmiOutputInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = vmiOutputInfo.ZoneNo;
            ///T_WM_NO,目标仓库代码
            info.WmNo = vmiOutputInfo.WmNo;
            ///T_ZONE_NO,目标存储区代码
            info.ZoneNo = vmiOutputInfo.ZoneNo;
            ///T_DOCK,目标道口代码
            //info.TDock = vmiOutputInfo.TDock;
            ///PART_BOX_CODE,零件类代码
            //info.PartBoxCode = vmiOutputInfo.PartBoxCode;
            ///SEND_TIME,发送时间
            info.SendTime = vmiOutputInfo.SendTime;
            ///OUTPUT_TYPE,出库类型
            info.OutputType = vmiOutputInfo.OutputType;
            ///TRAN_TIME,出库时间
            info.TranTime = vmiOutputInfo.TranTime;
            ///OUTPUT_REASON,出库原因
            info.OutputReason = vmiOutputInfo.OutputReason;
            ///BOOK_KEEPER,收货员
            info.BookKeeper = vmiOutputInfo.BookKeeper;
            ///CONFIRM_FLAG,确认标志
            info.ConfirmFlag = vmiOutputInfo.ConfirmFlag;
            ///PLAN_NO,计划行号
            info.PlanNo = vmiOutputInfo.PlanNo;
            ///ASN_NO,ASN编号
            info.AsnNo = vmiOutputInfo.AsnNo;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = vmiOutputInfo.RunsheetNo;
            ///ASSEMBLY_LINE,工厂模型_流水线
            info.AssemblyLine = vmiOutputInfo.AssemblyLine;
            ///PLANT_ZONE,工厂模型_厂区
            info.PlantZone = vmiOutputInfo.PlantZone;
            ///WORKSHOP,工厂模型_车间
            info.Workshop = vmiOutputInfo.Workshop;
            ///TRANS_SUPPLIER_NUM,物流平台_运输供应商
            info.TransSupplierNum = vmiOutputInfo.TransSupplierNum;
            ///PART_TYPE,零件类型
            info.PartType = vmiOutputInfo.PartType;
            ///SUPPLIER_TYPE,供应商类型
            info.SupplierType = vmiOutputInfo.SupplierType;
            ///RUNSHEET_CODE,单据号码
            info.RunsheetCode = vmiOutputInfo.RunsheetNo;
            ///ERP_FLAG,标志
            info.ErpFlag = vmiOutputInfo.ErpFlag;
            ///LOGICAL_PK,本地主键
            info.LogicalPk = vmiOutputInfo.LogicalPk;
            ///BUSINESS_PK,业务主键
            info.BusinessPk = vmiOutputInfo.BusinessPk;
            ///ROUTE,送货路径
            info.Route = vmiOutputInfo.Route;
            ///REQUEST_TIME,请求时间
            info.RequestTime = vmiOutputInfo.RequestTime;
            ///CUST_CODE,客户代码
            //info.CustCode = vmiOutputInfo.CustCode;
            /////CUST_NAME,客户名称
            //info.CustName = vmiOutputInfo.CustName;
            /////COST_CENTER,成本中心
            //info.CostCenter = vmiOutputInfo.CostCenter;
            /////ORGANIZATION_FID,组织结构
            //info.OrganizationFid = vmiOutputInfo.OrganizationFid;
            /////CONFIRM_USER,提交用户
            //info.ConfirmUser = vmiOutputInfo.ConfirmUser;
            /////CONFIRM_DATE,提交时间
            //info.ConfirmDate = vmiOutputInfo.ConfirmDate;
            /////LIABLE_USER,责任人
            //info.LiableUser = vmiOutputInfo.LiableUser;
            /////LIABLE_DATE,责任人确认时间
            //info.LiableDate = vmiOutputInfo.LiableDate;
            /////FINANCE_USER,财务
            //info.FinanceUser = vmiOutputInfo.FinanceUser;
            /////FINANCE_DATE,财务确认时间
            //info.FinanceDate = vmiOutputInfo.FinanceDate;
            /////SUM_PART_QTY,合计物料数量
            //info.SumPartQty = vmiOutputInfo.SumPartQty;
            /////SUM_OF_PRICE,合计金额
            //info.SumOfPrice = vmiOutputInfo.SumOfPrice;
            /////STATUS,出库单状态
            //info.Status = vmiOutputInfo.Status;
            /////CONVEYANCE,运输工具
            //info.Conveyance = vmiOutputInfo.Conveyance;
            /////CARRIER_TEL,承运人电话
            //info.CarrierTel = vmiOutputInfo.CarrierTel;
            /////SUM_WEIGHT,毛重
            //info.SumWeight = vmiOutputInfo.SumWeight;
            /////SUM_VOLUME,体积
            //info.SumVolume = vmiOutputInfo.SumVolume;
            /////PLAN_SHIPPING_TIME,计划发货时间
            //info.PlanShippingTime = vmiOutputInfo.PlanShippingTime;
            /////PLAN_DELIVERY_TIME,计划到达时间
            //info.PlanDeliveryTime = vmiOutputInfo.PlanDeliveryTime;
            /////PRINT_COUNT,打印次数
            //info.PrintCount = vmiOutputInfo.PrintCount;
            /////PRINT_TIME,打印时间
            //info.PrintTime = vmiOutputInfo.PrintTime;
            ///COMMENTS,备注
            info.Comments = vmiOutputInfo.Comments;
            ///VALID_FLAG,逻辑删除标记
            //info.ValidFlag = vmiOutputInfo.ValidFlag;
            ///CREATE_USER,创建人
            info.CreateUser = vmiOutputInfo.CreateUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = vmiOutputInfo.CreateDate;
            ///MODIFY_USER,最后修改人
            //info.ModifyUser = vmiOutputInfo.ModifyUser;
            ///MODIFY_DATE,最后修改时间
            //info.ModifyDate = vmiOutputInfo.ModifyDate;
            ///LAST_PRINT_USER,最后打印用户
            //info.LastPrintUser = vmiOutputInfo.LastPrintUser;
            ///SUM_PACKAGE_QTY,合计箱数
            //info.SumPackageQty = vmiOutputInfo.SumPackageQty;
            ///PULL_MODE,拉动方式
            //info.PullMode = vmiOutputInfo.PullMode;
        }
        #endregion

    }
}


namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DAL.SYS;
    using System.Data;
    using System.Transactions;
    using DM.SYS;

    /// <summary>
    /// ReceiveBLL
    /// </summary>
    public partial class ReceiveBLL
    {
        #region Common
        /// <summary>
        /// ReceiveDAL
        /// </summary>
        ReceiveDAL dal = new ReceiveDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<ReceiveInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 通过ID进行查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReceiveInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ReceiveInfo info)
        {
            ///入库单号①系统自动根据规则创建
            info.ReceiveNo = new SeqDefineDAL().GetCurrentCode("RECEIVE_NO");
            ///
            //info.Status = (int)WmmOrderStatusConstants.Created;
            ///采购订单或物料预留不允许手工创建
            if (info.ReceiveType.GetValueOrDefault() == (int)InboundTypeConstants.PurchaseOrder ||
                info.ReceiveType.GetValueOrDefault() == (int)InboundTypeConstants.ReserveInbound)
                throw new Exception("MC:0x00000461");///采购订单或物料预留不允许手工创建

            ///填充供应商类型
            if (!string.IsNullOrEmpty(info.SupplierNum))
                info.SupplierType = new SupplierDAL().GetSupplierType(info.SupplierNum);

            ///TODO:需要增加一个系统开关
            if (string.IsNullOrEmpty(info.RunsheetNo) && !string.IsNullOrEmpty(info.AsnNo))
                info.RunsheetNo = new PlanPullOrderDAL().GetOrderCode(info.AsnNo);

            ///如果没有拉动单,TODO:开关？
            //if (string.IsNullOrEmpty(info.RunsheetNo))
            //    info.RunsheetNo = info.ReceiveNo;

            return dal.Add(info);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<ReceiveInfo> GetList(string whereText, string orderText)
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
            ReceiveInfo info = dal.GetInfo(id);
            //if (info.Status != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000683");///状态必须为已创建
            string sql = "update [LES].[TT_WMM_RECEIVE_DETAIL] set " +
                "[VALID_FLAG] = 0," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [RECEIVE_FID] = N'" + info.Fid.GetValueOrDefault() + "';" +
                "update [LES].[TT_WMM_RECEIVE] set " +
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
            ///状态⑫为10.已创建的入库单可以进行修改
            ReceiveInfo info = dal.GetInfo(id);
            //if (info.Status != (int)WmmOrderStatusConstants.Created)
            //    throw new Exception("MC:0x00000683");///状态必须为已创建

            ///供应商
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            if (!string.IsNullOrEmpty(supplierNum) && info.SupplierNum != supplierNum)
                fields += ",[SUPPLIER_TYPE] = " + new SupplierDAL().GetSupplierType(supplierNum) + "";
            ///修改了供应商需要校验明细中的供应商数据是否一致?TODO:


            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CloseInfo(long id, string loginUser)
        {
            ///入库单必须为已发布状态⑫才可以进行关单操作
            ///作完成后将已发布状态⑫更改为90.已关单状态
            ReceiveInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            //if (info.Status != (int)WmmOrderStatusConstants.Published)
            //    throw new Exception("MC:0x00000735");///入库单必须为已发布状态
            string sql = "[STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(sql, id) > 0 ? true : false;
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
            List<ReceiveInfo> receiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            List<BarcodeInfo> barcodeInfos = new List<BarcodeInfo>();
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG",
                "COMPLETE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED_WHEN_NULL"
            });
            ///完成入库单时实收数量为空则等于需求数量
            configs.TryGetValue("COMPLETE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED_WHEN_NULL", out string completeReceiveActualQtyEqualsRequiredWhenNull);
            if (!string.IsNullOrEmpty(completeReceiveActualQtyEqualsRequiredWhenNull) &&
                completeReceiveActualQtyEqualsRequiredWhenNull.ToLower() == "true")
            {
                foreach (var receiveDetailInfo in receiveDetailInfos)
                {
                    if (receiveDetailInfo.ActualBoxNum == null) receiveDetailInfo.ActualBoxNum = receiveDetailInfo.RequiredBoxNum;
                    if (receiveDetailInfo.ActualQty == null) receiveDetailInfo.ActualQty = receiveDetailInfo.RequiredQty;
                }
            }
            ///是否在客户端扫描标签条码后更新状态为已扫描
            configs.TryGetValue("CLIENT_SCANED_BARCODE_UPDATE_BARCODE_STATUS_FLAG", out string clientScanedBarcodeUpdateBarcodeStatusFlag);
            if (!string.IsNullOrEmpty(clientScanedBarcodeUpdateBarcodeStatusFlag) &&
                clientScanedBarcodeUpdateBarcodeStatusFlag.ToLower() == "true")
            {
                ///获取已扫描的标签
                barcodeInfos = new BarcodeDAL().GetList("[ASN_RUNSHEET_NO] in ('" + string.Join("','", receiveInfos.Select(d => d.ReceiveNo).ToArray()) + "') and [BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Scaned + "", string.Empty);
            }
            ///获取所有目标存储区信息
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("[ZONE_NO] in ('" + string.Join("','", receiveInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);

            string sql = string.Empty;
            foreach (var receiveInfo in receiveInfos)
            {
                //if (receiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published)
                //    throw new Exception("MC:0x00000148");///已提交的入库单才能进行确认操作

                List<ReceiveDetailInfo> detailInfos = receiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).ToList();
                if (detailInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.AsnRunsheetNo == receiveInfo.ReceiveNo).ToList();
                ///拼接多张入库单
                sql += GetReceiveCompleteDealSql(receiveInfo, detailInfos, barcodes, loginUser);
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
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="barcodeInfos"></param>
        /// <param name="loginUser"></param>
        /// <param name="emergencyFlag"></param>
        /// <returns></returns>
        public string GetReceiveCompleteDealSql(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser, bool emergencyFlag = true)
        {
            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "VALID_RECEIVE_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG",
                "LES_TRAN_DATA_ENABLE_FLAG",
                "RECEIVE_MATERIAL_RECHECK_INSPECT_MODE",
                "ENABLE_PACKAGE_MANAGEMENT_FLAG",
                "INBOUND_SYNC_OUTBOUND_ENABLE_FLAG"
            });
            ///入库单客户端提交时处理语句
            ///WEB端为完成操作
            ///TODO:考虑增加是否支持单据多次收货的开关
            string receiveSubmitDealSql = string.Empty;
            StringBuilder @string = new StringBuilder();

            ///更新入库单状态
            @string.AppendLine(GetReceiveStatusUpdateSql(
                receiveInfo.ReceiveId,
                WmmOrderStatusConstants.Completed,
                loginUser));
            ///

            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == receiveDetailInfo.Fid.GetValueOrDefault()).ToList();
                if (barcodes.Count == 0)
                {
                    barcodes = barcodeInfos.Where(w => w.AsnRunsheetNo == receiveDetailInfo.TranNo
                    && w.PartNo == receiveDetailInfo.PartNo
                    && w.SupplierNum == receiveDetailInfo.SupplierNum
                    && w.RunsheetNo == receiveDetailInfo.RunsheetNo).ToList();
                }

                ///是否校验实收数量等于扫描数量
                configs.TryGetValue("VALID_RECEIVE_ACTUAL_QTY_EQUAL_SCANED_QTY_FLAG", out string validReceiveActualQtyEqualScanedQtyFlag);
                if (!string.IsNullOrEmpty(validReceiveActualQtyEqualScanedQtyFlag) &&
                    validReceiveActualQtyEqualScanedQtyFlag.ToLower() == "true")
                {
                    if (barcodes.Sum(d => d.CurrentQty.GetValueOrDefault()) != receiveDetailInfo.ActualQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                    if (barcodes.Count != receiveDetailInfo.ActualBoxNum.GetValueOrDefault())
                        throw new Exception("MC:0x00000258");///标签扫描数量与单据不一致
                }

                ///入库单号、零件号、供应商、单号
                foreach (BarcodeInfo barcodeInfo in barcodes)
                {
                    ///来源不为空时获取来源
                    if (!string.IsNullOrEmpty(receiveDetailInfo.WmNo) &&
                        !string.IsNullOrEmpty(receiveDetailInfo.ZoneNo))
                    {
                        @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                        (int)BarcodeStatusConstants.Inbound,
                        receiveDetailInfo.WmNo,
                        receiveDetailInfo.ZoneNo,
                        receiveDetailInfo.Dloc,
                        receiveDetailInfo.TranNo,
                        barcodeInfo.Fid.GetValueOrDefault(),
                        loginUser));
                    }
                    else
                        @string.AppendLine(BarcodeDAL.GetBarcodeUpdateSql(
                            (int)BarcodeStatusConstants.Inbound,
                            receiveDetailInfo.TargetWm,
                            receiveDetailInfo.TargetZone,
                            receiveDetailInfo.TargetDloc,
                            receiveDetailInfo.TranNo,
                            barcodeInfo.Fid.GetValueOrDefault(),
                            loginUser));
                }

                ///更新入库单明细信息
                @string.AppendLine(GetReceiveDetailActualQtyUpdateSql(
                    receiveDetailInfo.Id,
                    receiveDetailInfo.ActualBoxNum.GetValueOrDefault(),
                    receiveDetailInfo.ActualQty.GetValueOrDefault(),
                    loginUser));
            }

            ///是否启用LES交易记录创建
            configs.TryGetValue("LES_TRAN_DATA_ENABLE_FLAG", out string lesTranDataEnableFlag);
            if (!string.IsNullOrEmpty(lesTranDataEnableFlag) &&
                lesTranDataEnableFlag.ToLower() == "true")
                @string.AppendLine(GetTranDetailsInsertSql(receiveInfo, receiveDetailInfos, (int)WmmTranTypeConstants.Inbound, loginUser));

            ///系统配置中RECEIVE_MATERIAL_RECHECK_INSPECT_MODE入库免检物料重新校验检验模式标记，默认为true
            ///若该标记为true时将入库明细中的㊺免检物料比对检验模式基础数据中物料的当前检验模式
            ///若检验模式有变化则需要将变化的物料提交至QMIS检验任务中间表，并生成同步数据任务，否则忽略此逻辑（此项逻辑可以考虑异步实现）
            ///入库免检物料重新校验检验模式标记
            configs.TryGetValue("RECEIVE_MATERIAL_RECHECK_INSPECT_MODE", out string receiveMaterialRecheckInspectMode);
            if (!string.IsNullOrEmpty(receiveMaterialRecheckInspectMode) &&
                receiveMaterialRecheckInspectMode.ToLower() == "true")
                @string.AppendLine(PartInspectionModeBLL.ReloadInspectionMode(receiveInfo, ref receiveDetailInfos, loginUser));

            ///将入库明细中是否产生出库单标记㊵为true的数据过滤出来，系统配置中SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG相同存储区相同中转存储区验证标记，
            ///默认为true，控制了同一张入库单的明细中不会出现不同的出库目标存储区㊷，
            ///所以此时只需直接根据入库单及明细复制出相应的出库单及明细，并以出库目标存储区㊷作为出库单的目标存储区入库实际数量⑱作为出库需求数量，
            ///若系统配置标记为false，则将过滤出来的入库明细数据根据其出库目标存储区进行分组，并按分组情况生成多个出库单，出库单状态为已发布WMM - 011
            ///入库后同步生成出库指令启用标记
            configs.TryGetValue("INBOUND_SYNC_OUTBOUND_ENABLE_FLAG", out string inboundSyncOutboundEnableFlag);
            if (!string.IsNullOrEmpty(inboundSyncOutboundEnableFlag) && inboundSyncOutboundEnableFlag.ToLower() == "true")
                @string.AppendLine(OutputBLL.CreateOutputByReceiveSql(receiveInfo, receiveDetailInfos, barcodeInfos, loginUser));
            ///

            ///系统配置ENABLE_PACKAGE_MANAGEMENT_FLAG是否启用器具管理标记，默认为true
            ///若该标记为ture时需要根据实收包装数量⑰以及包装型号⑲等数据产生器具包装随货入库交易数据PCM-002
            configs.TryGetValue("ENABLE_PACKAGE_MANAGEMENT_FLAG", out string enablePackageManagementFlag);
            if (!string.IsNullOrEmpty(enablePackageManagementFlag) &&
                enablePackageManagementFlag.ToLower() == "true")
                @string.AppendLine(PackageTranDetailBLL.CreatePackageTranDetailsSql(receiveDetailInfos, loginUser));

            ///不满足需求物料生成新拉动单
            if (emergencyFlag)
            {
                ///TODO:系统配置，ENABLE_LACK_QTY_HANDOVER_AUTO_EMERGENCY，交接物料缺少时自动产生紧急拉动标记
                configs.TryGetValue("ENABLE_LACK_QTY_HANDOVER_AUTO_EMERGENCY", out string enable_lack_qty_handover_auto_emergency);
                if (!string.IsNullOrEmpty(enable_lack_qty_handover_auto_emergency) && enable_lack_qty_handover_auto_emergency.ToLower() == "true")
                {
                    ///触发紧急拉动
                    @string.AppendLine(planEmergencyPulling(receiveInfo, receiveDetailInfos, loginUser));                   
                }
            }
            else
            {
                ///TODO:日志记录，某用户取消了紧急拉动
            }
            return @string.ToString();
        }


        /// <summary>
        /// ///触发紧急拉动
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="loginUser"></param>
        public string planEmergencyPulling(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos,string loginUser)
        {
            ///对应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                " and [STATUS] =" + (int)BasicDataStatusConstants.Enable + "" +
                //" and [INHOUSE_SYSTEM_MODE]='"+ receiveInfo.PullMode.GetValueOrDefault() + "'" +
                //" and [INHOUSE_PART_CLASS]='"+ receiveInfo.PartBoxCode + "'" +
                " and [T_WM_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.TargetWm).ToArray()) + "')" +
                " and [T_ZONE_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.TargetZone).ToArray()) + "')" +
                " and [SUPPLIERNUM] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.SupplierNum).ToArray()) + "')" +
                " and [PART_NO] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("MC:0x00000213");///物料拉动信息数据错误
            if (receiveInfo == null) return string.Empty;
            if (receiveDetailInfos.Count == 0) return string.Empty;
            StringBuilder @string = new StringBuilder();
            foreach(ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                ///物料拉动信息
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d =>
                 d.TWmNo == receiveDetailInfo.TargetWm && d.TZoneNo == receiveDetailInfo.TargetZone && d.SupplierNum == receiveDetailInfo.SupplierNum && d.PartNo == receiveDetailInfo.PartNo);
                if (maintainInhouseLogisticStandardInfo == null) continue;
                ///新建购物车对象
                EmergencyPullingCartInfo emergencyPullingCartInfo = EmergencyPullingCartBLL.CreateEmergencyPullingCartInfo(loginUser);
                ///MaintainInhouseLogisticStandardInfo-->EmergencyPullingCartInfo
                EmergencyPullingCartBLL.GetEmergencyPullingCartInfo(maintainInhouseLogisticStandardInfo,ref emergencyPullingCartInfo);
                ///ReceiveInfo-->EmergencyPullingCartInfo
                EmergencyPullingCartBLL.GetEmergencyPullingCartByReceive(receiveInfo, ref emergencyPullingCartInfo);
                ///ReceiveDetailInfo-->EmergencyPullingCartInfo
                EmergencyPullingCartBLL.GetEmergencyPullingCartByReceiveDetail(receiveDetailInfo, ref emergencyPullingCartInfo);
                ///购物车添加sql
                @string.AppendLine(EmergencyPullingCartDAL.GetInsertSql(emergencyPullingCartInfo));
            }
            return @string.ToString();
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
            List<ReceiveInfo> receiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (receiveInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///获取系统配置
            Dictionary<string, string> configs = new ConfigDAL().GetValuesByCodes(new string[] {
                "RELEASE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED",
                "MANUAL_RECEIVE_ORDER_RELEASE_CREATE_BARCODE",
                "ENABLE_SRM_FLAG",
                "ENABLE_VMI_FLAG",
                "REBATE_INBOUND_RECEIVE_RELEASE_TO_SRM",
                "REBATE_INBOUND_RECEIVE_RELEASE_TO_VMI",
                "RELEASE_RECEIVE_LOAD_PART_INSPECTION_MODE"
            });
            ///获取供应商信息
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", receiveInfos.Select(d => d.SupplierNum).ToArray()) + "') ", string.Empty);
            ///存储区信息
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("[ZONE_NO] in ('" + string.Join("','", receiveInfos.Select(d => d.ZoneNo).ToArray()) + "') ", string.Empty);
            ///获取仓库信息
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("[WAREHOUSE] in ('" + string.Join("','", receiveInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///入库单明细
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (receiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000367");///入库单没有明细

            ///已生成的标签
            List<BarcodeInfo> barcodeInfos = new BarcodeDAL().GetList("" +
                "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Created + " and " +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            ///是否启用SRM系统标记、ENABLE_SRM_FLAG、默认为false
            configs.TryGetValue("ENABLE_SRM_FLAG", out string enableSrmFlag);
            ///是否启用WMS系统标记
            configs.TryGetValue("ENABLE_VMI_FLAG", out string enableVmiFlag);
            ///手工创建入库单时创建条码标签
            configs.TryGetValue("MANUAL_RECEIVE_ORDER_RELEASE_CREATE_BARCODE", out string manualReceiveOrderReleaseCreateBarcode);
            string sql = string.Empty;

            foreach (var receiveInfo in receiveInfos)
            {
                ///一般手工创建入库单会用到此功能，入库单必须为10.已创建状态⑫
                //if (receiveInfo.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Created)
                //    throw new Exception("MC:0x00000683");///状态必须为已创建

                List<ReceiveDetailInfo> detailInfos = receiveDetailInfos.Where(d => d.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault()).ToList();
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => detailInfos.Select(f => f.Fid.GetValueOrDefault()).Contains(d.CreateSourceFid.GetValueOrDefault())).ToList();

                #region 物料返利
                if (receiveInfo.ReceiveType.GetValueOrDefault() == (int)InboundTypeConstants.RebateInbound)
                {
                    ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == receiveInfo.ZoneNo);
                    if (zonesInfo == null)
                        throw new Exception("MC:0x00000246");///存储区数据错误

                    ///需要校验返利入库单对应的目标库区只能是结算库区
                    //if (!zonesInfo.SettlementFlag.GetValueOrDefault())
                    //    throw new Exception("MC:0x00000270");///非结算库区不能作为返利入库单的目标库区

                    SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == receiveInfo.SupplierNum);
                    if (supplierInfo == null)
                        throw new Exception("MC:0x00000239");///供应商数据错误

                    ///判定单据上的供应商是否为10.物料供应商且系统配置中
                    ///且单据供应商类型为物料供应商

                    ///返利入库单是否发布给SRM系统
                    configs.TryGetValue("REBATE_INBOUND_RECEIVE_RELEASE_TO_SRM", out string rebateInboundReceiveReleaseToSrm);
                    ///TODO:LES自身的SRM模块启用此处需要进行修改
                    if (enableSrmFlag.ToLower() == "true" &&
                        supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.MaterialSupplier &&
                        rebateInboundReceiveReleaseToSrm.ToLower() == "true")
                    {
                        ///TODO:考虑增加供应商的SRM开关
                        ///若该标记为true则需要将入库单数据转为单据类型为70.返利入库单且不能编辑ASN写入SRM物料拉动单接口数据
                        List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(detailInfos.Select(d => d.PartNo).ToList(), new List<string> { zonesInfo.ZoneNo });
                        ///创建交接对象
                        MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                        ///入库单信息匹配
                        MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(receiveInfo, ref materialPullingOrderInfo);
                        ///
                        MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(supplierInfo, ref materialPullingOrderInfo);
                        #region SRM单据属性特殊处理
                        ///SRM单据类型
                        materialPullingOrderInfo.OrderType = (int)SrmOrderTypeConstants.Purchase;
                        ///单据类型为70.返利入库单且不能编辑ASN
                        materialPullingOrderInfo.AsnFlag = false;
                        ///返利入库单都不紧急
                        materialPullingOrderInfo.EmergencyFlag = false;
                        #endregion
                        ///入库单明细
                        materialPullingOrderInfo.MaterialPullingOrderDetailInfos = MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfos(detailInfos);
                        ///生成语句
                        sql += MaterialPullingCommonBLL.CreateSrmPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser);
                    }

                    ///返利入库单是否发布给VMI系统
                    configs.TryGetValue("REBATE_INBOUND_RECEIVE_RELEASE_TO_VMI", out string rebateInboundReceiveReleaseToVmi);
                    ///且单据供应商类型为储运供应商
                    if (supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.LogisticsSupplier &&
                        rebateInboundReceiveReleaseToVmi.ToLower() == "true")
                    {
                        WarehouseInfo warehouseInfo = warehouseInfos.First(d => d.Warehouse == receiveInfo.WmNo);
                        if (warehouseInfo == null)
                            throw new Exception("MC:0x00000240");///仓库数据错误

                        ///传给LES的VMI模块
                        if (warehouseInfo.VmiEnable.GetValueOrDefault())
                        {
                            ///创建
                            MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                            ///入库单信息匹配
                            MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(receiveInfo, ref materialPullingOrderInfo);
                            ///
                            MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(supplierInfo, ref materialPullingOrderInfo);
                            ///TODO:VMI拉动单类型
                            materialPullingOrderInfo.OrderType = (int)SrmOrderTypeConstants.Purchase;
                            ///单据类型为70.返利入库单且不能编辑ASN
                            materialPullingOrderInfo.AsnFlag = false;
                            ///返利入库单都不紧急
                            materialPullingOrderInfo.EmergencyFlag = false;
                            ///入库单明细
                            materialPullingOrderInfo.MaterialPullingOrderDetailInfos = MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfos(detailInfos);
                            ///
                            sql += MaterialPullingCommonBLL.CreateVmiPullOrderSql(materialPullingOrderInfo, new List<PartsStockInfo>(), loginUser);
                        }
                        else
                        {
                            ///传给外部VMI的WMS
                            if (enableVmiFlag.ToLower() == "true")
                            {
                                ///创建
                                MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                                ///入库单信息匹配
                                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(receiveInfo, ref materialPullingOrderInfo);
                                ///
                                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(supplierInfo, ref materialPullingOrderInfo);
                                ///TODO:WMS拉动单类型
                                materialPullingOrderInfo.OrderType = (int)WmsOrderTypeConstants.Reserve;
                                ///单据类型为70.返利入库单且不能编辑ASN
                                materialPullingOrderInfo.AsnFlag = false;
                                ///返利入库单都不紧急
                                materialPullingOrderInfo.EmergencyFlag = false;
                                ///入库单明细
                                materialPullingOrderInfo.MaterialPullingOrderDetailInfos = MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfos(detailInfos);
                                ///
                                sql += MaterialPullingCommonBLL.CreateWmsPullOrderSql(materialPullingOrderInfo, new List<PartsStockInfo>(), loginUser);
                            }
                        }
                    }
                }
                #endregion

                ///生成条码
                if (manualReceiveOrderReleaseCreateBarcode.ToLower() == "true")
                    sql += MaterialPullingCommonBLL.GetCreateBarcodesSql(detailInfos, barcodes, loginUser);

                ///入库单发布时是否加载物料检验模式
                configs.TryGetValue("RELEASE_RECEIVE_LOAD_PART_INSPECTION_MODE", out string releaseReceiveLoadPartInspectionMode);
                bool inspectionFlag = false;
                if (!string.IsNullOrEmpty(releaseReceiveLoadPartInspectionMode) && releaseReceiveLoadPartInspectionMode.ToLower() == "true")
                {
                    ReceiveInfo receive = receiveInfo.Clone();
                    sql += PartInspectionModeBLL.LoadInspectionMode(ref receive, ref detailInfos, loginUser);
                    //receiveInfo.InspectionFlag = receive.InspectionFlag;
                }
                ///行号更新
                int rowNo = 0;
                ///发布入库单时实收数量等于需求数量
                configs.TryGetValue("RELEASE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED", out string releaseReceiveActualQtyEqualsRequired);
                foreach (var detailInfo in detailInfos)
                {
                    if (!string.IsNullOrEmpty(releaseReceiveActualQtyEqualsRequired) && releaseReceiveActualQtyEqualsRequired.ToLower() == "true")
                    {
                        if (detailInfo.ActualBoxNum == null) detailInfo.ActualBoxNum = detailInfo.RequiredBoxNum;
                        if (detailInfo.ActualQty == null) detailInfo.ActualQty = detailInfo.RequiredQty;
                    }
                    ///更新入库单明细需要注意不能覆盖明细中原内容
                    sql += "update [LES].[TT_WMM_RECEIVE_DETAIL] set " +
                        "[ROW_NO] = " + ++rowNo + "," +
                        (detailInfo.ActualBoxNum == null ? string.Empty : "[ACTUAL_BOX_NUM] = " + detailInfo.ActualBoxNum.GetValueOrDefault() + ",") +
                        (detailInfo.ActualQty == null ? string.Empty : "[ACTUAL_QTY] = " + detailInfo.ActualQty.GetValueOrDefault() + ",") +
                        (detailInfo.InspectionMode == null ? string.Empty : "[INSPECTION_MODE] = " + detailInfo.InspectionMode.GetValueOrDefault() + ",") +
                        (string.IsNullOrEmpty(detailInfo.SupplierNum) ? "[SUPPLIER_NUM] = N'" + receiveInfo.SupplierNum + "'," : string.Empty) +
                        //"[WM_NO] = N'" + receiveInfo.SourceWmNo + "'," +
                        //"[ZONE_NO] = N'" + receiveInfo.SourceZoneNo + "'," +
                        "[TARGET_WM] = N'" + receiveInfo.WmNo + "'," +
                        "[TARGET_ZONE] = N'" + receiveInfo.ZoneNo + "'," +
                        (string.IsNullOrEmpty(detailInfo.RunsheetNo) ? "[RUNSHEET_NO] = N'" + receiveInfo.RunsheetNo + "'," : string.Empty) +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' where " +
                        "[ID] = " + detailInfo.Id + ";";
                }
                ///更新入库单
                sql += "update [LES].[TT_WMM_RECEIVE] set " +
                "[SUM_PART_QTY] = " + detailInfos.Sum(d => d.RequiredQty.GetValueOrDefault()) + "," +
                "[SUM_OF_PRICE] = " + detailInfos.Sum(d => d.PartPrice.GetValueOrDefault()) + "," +
                "[SUM_WEIGHT] = " + detailInfos.Sum(d => d.SumWeight.GetValueOrDefault()) + "," +
                "[SUM_VOLUME] = " + detailInfos.Sum(d => d.SumVolume.GetValueOrDefault()) + "," +
                "[SUM_PACKAGE_QTY] = " + detailInfos.Sum(d => d.RequiredBoxNum.GetValueOrDefault()) + "," +
                "[INSPECTION_FLAG] = " + (inspectionFlag ? 1 : 0) + "," +
                "[STATUS] = " + (int)WmmOrderStatusConstants.Published + "," +
                "[MODIFY_USER] = N'" + loginUser + "' ," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + receiveInfo.ReceiveId + ";";
            }
            ///删除已删除入库单明细的标签
            sql += "update [LES].[TT_WMM_BARCODE] " +
                "set [VALID_FLAG] = 0 " +
                "where [CREATE_SOURCE_FID] in (select [FID] from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) " +
                "where [VALID_FLAG] = 0 and [RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'));";
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
            List<ReceiveInfo> receiveInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", "[ID]");
            if (receiveInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            //if (receiveInfos.Count(d => d.Status.GetValueOrDefault() != (int)WmmOrderStatusConstants.Published) > 0)
            //    throw new Exception("MC:0x00000457");///状态必须为已发布

            ///入库单明细
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", "[ID]");
            if (receiveDetailInfos.Count == 0)
                throw new Exception("MC:0x00000367");///入库单没有明细
            int cnt = new BarcodeDAL().GetCounts("" +
                "[CREATE_SOURCE_FID] in ('" + string.Join("','", receiveDetailInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and " +
                "[BARCODE_STATUS] <> " + (int)BarcodeStatusConstants.Created + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000458");///条码已被扫描无法撤销

            string sql = "update [LES].[TT_WMM_RECEIVE] set " +
                                "[STATUS] = " + (int)WmmOrderStatusConstants.Created + "," +
                                "[MODIFY_USER] = N'" + loginUser + "' ," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            sql += "update [LES].[TT_WMM_RECEIVE_DETAIL] set " +
                        "[ROW_NO] = NULL," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' where " +
                        "[ID] in (" + string.Join(",", receiveDetailInfos.Select(d => d.Id).ToArray()) + ");";
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

        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintReceiveDatas(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select T1.*,T2.[ITEM_NAME] as RECEIVE_TYPE_NAME,T3.[ITEM_NAME] as INSPECTION_FLAG_NAME from [LES].[TT_WMM_RECEIVE] T1 with(nolock) " +
                "left join dbo.[TS_SYS_CODE_ITEM] T2 with(nolock) on T2.[CODE_FID] = N'E71E90A7-C157-4FAD-9D17-AD9B210AA5AF' and T1.[RECEIVE_TYPE] = T2.[ITEM_VALUE] and T2.[VALID_FLAG] = 1 " +
                "left join dbo.[TS_SYS_CODE_ITEM] T3 with(nolock) on T3.[CODE_FID] = N'40AC34FF-5B7F-4033-8344-07A658C4D907' and T1.[INSPECTION_FLAG] = T3.[ITEM_VALUE] and T3.[VALID_FLAG] = 1 " +
                "where T1.[VALID_FLAG] = 1 and T1.[ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [RECEIVE_FID] in (select [FID] from [LES].[TT_WMM_RECEIVE] with(nolock) " +
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
            string sql = "update [LES].[TT_WMM_RECEIVE] set " +
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
                "[CREATE_SOURCE_FID] in (select [FID] from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) where " +
                "[VALID_FLAG] = 1 and " +
                "[RECEIVE_FID] in (select [FID] from [LES].[TT_WMM_RECEIVE] with(nolock) where " +
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
        /// 获取交易记录INSERT语句
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tranType"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetTranDetailsInsertSql(long id, int tranType, string loginUser)
        {
            ReceiveInfo receiveInfo = new ReceiveDAL().GetInfo(id);
            if (receiveInfo == null) return string.Empty;
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailDAL().GetList("[RECEIVE_FID] = N'" + receiveInfo.Fid.GetValueOrDefault() + "'", "[ROW_NO]");
            if (receiveDetailInfos.Count == 0) return string.Empty;
            return GetTranDetailsInsertSql(receiveInfo, receiveDetailInfos, tranType, loginUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="tranType"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetTranDetailsInsertSql(ReceiveInfo receiveInfo, List<ReceiveDetailInfo> receiveDetailInfos, int tranType, string loginUser)
        {
            TranDetailsBLL tranDetailsBLL = new TranDetailsBLL();
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(receiveDetailInfos.Select(d => d.SupplierNum).ToList());
            StringBuilder @string = new StringBuilder();
            foreach (var receiveDetailInfo in receiveDetailInfos)
            {
                if (receiveDetailInfo.ActualQty.GetValueOrDefault() == 0) continue;
                TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo(tranType, (int)WmmTranStateConstants.Created, loginUser);
                ///ReceiveDetailInfo
                TranDetailsBLL.GetTranDetailsInfo(receiveDetailInfo, ref tranDetailsInfo);
                ///ReceiveInfo
                TranDetailsBLL.GetTranDetailsInfo(receiveInfo, ref tranDetailsInfo);
                ///供应商
                TranDetailsBLL.GetTranDetailsInfo(supplierInfos.FirstOrDefault(d => d.SupplierNum == tranDetailsInfo.SupplierNum), ref tranDetailsInfo);
                ///
                if (tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.None) continue;
                @string.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
            }
            return @string.ToString();
        }
        /// <summary>
        /// 根据单号获取主键
        /// </summary>
        /// <param name="receiveNo"></param>
        /// <returns></returns>
        public long GetIdByReceiveNo(string receiveNo)
        {
            return dal.GetIdByReceiveNo(receiveNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="outputOrderStatus"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetReceiveStatusUpdateSql(long id, WmmOrderStatusConstants receiveOrderStatus, string loginUser)
        {
            string receiveStatusUpdateSql = "update [LES].[TT_WMM_RECEIVE] set " +
                "[STATUS] = {0}," +
                "[TRAN_TIME] = GETDATE()," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'{1}' " +
                "where [ID] ={2};";
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
            string receiveDetailActualQtyUpdateSql = "update [LES].[TT_WMM_RECEIVE_DETAIL] set " +
                "[ACTUAL_BOX_NUM] = {0}," +
                "[ACTUAL_QTY] = {1}," +
                "[MODIFY_USER] = N'{2}'," +
                "MODIFY_DATE = GETDATE() " +
                "where [ID] = {3};";
            ///更新入库单明细实际箱数、实际数量、修改人、修改时间
            return string.Format(receiveDetailActualQtyUpdateSql, actualBoxNum, actualQty, loginUser, id);
        }
        /// <summary>
        /// Get ReceiveInfo
        /// </summary>
        /// <param name="receiveNo"></param>
        /// <returns></returns>
        public ReceiveInfo GetInfo(string receiveNo)
        {
            return dal.GetInfo(receiveNo);
        }
        #endregion

        #region Interface
        /// <summary>
        /// 出库单转入库单
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <returns></returns>
        public ReceiveInfo GetReceiveInfoByOutputInfo(OutputInfo outputInfo)
        {
            ReceiveInfo info = new ReceiveInfo
            {
                ReceiveId = outputInfo.OutputId,
           
                ReceiveNo = outputInfo.OutputNo,
                Plant = outputInfo.Plant,
                SupplierNum = outputInfo.SupplierNum,
                SupplierType = outputInfo.SupplierType,
                //SourceWmNo = outputInfo.WmNo,
                //SourceZoneNo = outputInfo.ZoneNo,
                WmNo = outputInfo.WmNo,
                ZoneNo = outputInfo.ZoneNo,
                //Dock = outputInfo.TDock,
                ReceiveType = outputInfo.OutputType,
                SendTime = outputInfo.SendTime,
                TranTime = outputInfo.TranTime,
                ReceiveReason = outputInfo.OutputReason,
                BookKeeper = outputInfo.BookKeeper,
                //Status = outputInfo.Status,
                AsnNo = outputInfo.AsnNo,
                RunsheetNo = outputInfo.RunsheetNo,
                //PrintCount = outputInfo.PrintCount,
                //PrintTime = outputInfo.PrintTime,
                //LastPrintUser = string.Empty,///
                //IsOutput = null,///
                //OrganizationFid = outputInfo.OrganizationFid,
                //CostCenter = outputInfo.CostCenter,
                //ConfirmUser = outputInfo.ConfirmUser,
                //ConfirmDate = outputInfo.ConfirmDate,
                //LiableUser = outputInfo.LiableUser,
                //LiableDate = outputInfo.LiableDate,
                //FinanceUser = outputInfo.FinanceUser,
                //FinanceDate = outputInfo.FinanceDate,
                //SumPartQty = outputInfo.SumPartQty,
                //SumOfPrice = outputInfo.SumOfPrice,
                Comments = outputInfo.Comments,
                //PartBoxCode = outputInfo.PartBoxCode,
                //PartBoxName = string.Empty,///
                //InspectionFlag = null,///
                //TrustTime = null,///
                //SumWeight = outputInfo.SumWeight,
                //SumVolume = outputInfo.SumVolume,
                //CustCode = outputInfo.CustCode,
                //CustName = outputInfo.CustName,
                //Route = outputInfo.Route,
                //RouteName = string.Empty,///
                //ValidFlag = outputInfo.ValidFlag,
                //CreateUser = outputInfo.CreateUser,
                //CreateDate = outputInfo.CreateDate,
                //ModifyUser = outputInfo.ModifyUser,
                //ModifyDate = outputInfo.ModifyDate,
                //PlanShippingTime = outputInfo.PlanShippingTime,
                //PlanDeliveryTime = outputInfo.PlanDeliveryTime
            };
            return info;
        }
        /// <summary>
        /// Create ReceiveInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>ReceiveInfo</returns>
        public static ReceiveInfo CreateReceiveInfo(string loginUser)
        {
            ReceiveInfo info = new ReceiveInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_USER,创建人
            info.CreateUser = loginUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///PULL_MODE,拉动方式
            //info.PullMode = null;

            return info;
        }
        /// <summary>
        /// VmiReceiveInfo -> ReceiveInfo
        /// </summary>
        /// <param name="vmiReceiveInfo"></param>
        /// <param name="info"></param>
        public static void GetReceiveInfo(VmiReceiveInfo vmiReceiveInfo, ref ReceiveInfo info)
        {
            if (vmiReceiveInfo == null) return;
            ///ID
            info.ReceiveId = vmiReceiveInfo.Id;
            ///FID,
            info.Fid = vmiReceiveInfo.Fid;
            ///RECEIVE_NO,入库单号
            info.ReceiveNo = vmiReceiveInfo.ReceiveNo;
            ///PLANT,工厂模型_工厂
            info.Plant = vmiReceiveInfo.Plant;
            ///SUPPLIER_NUM,基础数据_供应商
            info.SupplierNum = vmiReceiveInfo.SupplierNum;
            ///SUPPLIER_TYPE,供应商类型
            info.SupplierType = vmiReceiveInfo.SupplierType;
            ///SOURCE_WM_NO,来源仓库代码
            //info.SourceWmNo = vmiReceiveInfo.SourceWmNo;
            /////SOURCE_ZONE_NO,来源存储区
            //info.SourceZoneNo = vmiReceiveInfo.SourceZoneNo;
            ///WM_NO,仓库编码
            info.WmNo = vmiReceiveInfo.WmNo;
            ///ZONE_NO,存贮区编码
            info.ZoneNo = vmiReceiveInfo.ZoneNo;
            ///DOCK,卸货区
            info.Dock = vmiReceiveInfo.Dock;
            ///SEND_TIME,发送时间
            info.SendTime = vmiReceiveInfo.SendTime;
            ///RECEIVE_TYPE,入库类型
            info.ReceiveType = vmiReceiveInfo.ReceiveType;
            ///TRAN_TIME,入库时间
            info.TranTime = vmiReceiveInfo.TranTime;
            ///RECEIVE_REASON,入库原因
            info.ReceiveReason = vmiReceiveInfo.ReceiveReason;
            ///BOOK_KEEPER,收货员
            info.BookKeeper = vmiReceiveInfo.BookKeeper;
            ///STATUS,状态
            //info.Status = vmiReceiveInfo.Status;
            ///ASN_NO,ASN编号
            info.AsnNo = vmiReceiveInfo.AsnNo;
            ///RUNSHEET_NO,拉动单号
            info.RunsheetNo = vmiReceiveInfo.RunsheetNo;
            ///PRINT_COUNT,打印次数
            //info.PrintCount = vmiReceiveInfo.PrintCount;
            /////PRINT_TIME,打印时间
            //info.PrintTime = vmiReceiveInfo.PrintTime;
            /////LAST_PRINT_USER,最后打印时间
            //info.LastPrintUser = vmiReceiveInfo.LastPrintUser;
            /////IS_OUTPUT,是否生成出库单
            //info.IsOutput = vmiReceiveInfo.IsOutput;
            /////ORGANIZATION_FID,组织结构
            //info.OrganizationFid = vmiReceiveInfo.OrganizationFid;
            /////COST_CENTER,成本中心
            //info.CostCenter = vmiReceiveInfo.CostCenter;
            /////CONFIRM_USER,提交人
            //info.ConfirmUser = vmiReceiveInfo.ConfirmUser;
            /////CONFIRM_DATE,提交时间
            //info.ConfirmDate = vmiReceiveInfo.ConfirmDate;
            /////LIABLE_USER,责任人
            //info.LiableUser = vmiReceiveInfo.LiableUser;
            /////LIABLE_DATE,责任人确认时间
            //info.LiableDate = vmiReceiveInfo.LiableDate;
            /////FINANCE_USER,财务
            //info.FinanceUser = vmiReceiveInfo.FinanceUser;
            /////FINANCE_DATE,财务确认时间
            //info.FinanceDate = vmiReceiveInfo.FinanceDate;
            /////SUM_PART_QTY,合计物料数量
            //info.SumPartQty = vmiReceiveInfo.SumPartQty;
            /////SUM_OF_PRICE,合计金额
            //info.SumOfPrice = vmiReceiveInfo.SumOfPrice;
            /////PART_BOX_CODE,零件类代码
            //info.PartBoxCode = vmiReceiveInfo.PartBoxCode;
            /////PART_BOX_NAME,零件类名称
            //info.PartBoxName = vmiReceiveInfo.PartBoxName;
            /////INSPECTION_MODE,检验模式
            //info.InspectionMode = vmiReceiveInfo.InspectionMode;
            /////TRUST_TIME,委托时间
            //info.TrustTime = vmiReceiveInfo.TrustTime;
            /////SUM_WEIGHT,合计重量
            //info.SumWeight = vmiReceiveInfo.SumWeight;
            /////SUM_VOLUME,合计体积
            //info.SumVolume = vmiReceiveInfo.SumVolume;
            /////CUST_CODE,客户代码
            //info.CustCode = vmiReceiveInfo.CustCode;
            /////CUST_NAME,客户名称
            //info.CustName = vmiReceiveInfo.CustName;
            /////ROUTE,物流路线代码
            //info.Route = vmiReceiveInfo.Route;
            /////ROUTE_NAME,物流路线名称
            //info.RouteName = vmiReceiveInfo.RouteName;
            ///COMMENTS,备注
            info.Comments = vmiReceiveInfo.Comments;
            ///VALID_FLAG,逻辑删除标记
            //info.Validflag = vmiReceiveInfo.ValidFlag;
            ///CREATE_USER,创建人
            info.CreateUser = vmiReceiveInfo.CreateUser;
            ///CREATE_DATE,创建时间
            info.CreateDate = vmiReceiveInfo.CreateDate;
            ///MODIFY_USER,最后修改用户
            //info.ModifyUser = vmiReceiveInfo.ModifyUser;
            /////MODIFY_DATE,最后修改时间
            //info.ModifyDate = vmiReceiveInfo.ModifyDate;
            /////PLAN_SHIPPING_TIME,预计发货时间
            //info.PlanShippingTime = vmiReceiveInfo.PlanShippingTime;
            /////PLAN_DELIVERY_TIME,预计到货时间
            //info.PlanDeliveryTime = vmiReceiveInfo.PlanDeliveryTime;
            /////INSPECTION_FLAG,检验标记
            //info.InspectionFlag = vmiReceiveInfo.InspectionFlag;
            /////SUM_PACKAGE_QTY,合计箱数
            //info.SumPackageQty = vmiReceiveInfo.SumPackageQty;
        }
        public static void GetReceiveInfo(ref ReceiveInfo info)
        {

        }
        #endregion


        public ReceiveInfo GetReceiveInfoByVmiReceive(VmiReceiveInfo info)
        {
            ReceiveInfo model = new ReceiveInfo
            {
                ReceiveId = info.Id,
                Fid = info.Fid,
                ReceiveNo = info.ReceiveNo,
                Plant = info.Plant,
                SupplierNum = info.SupplierNum,
                SupplierType = info.SupplierType,                
                //SourceWmNo = info.SourceWmNo,
                //SourceZoneNo = info.SourceZoneNo,
                WmNo = info.WmNo,
                ZoneNo = info.ZoneNo,
                Dock = info.Dock,
                ReceiveType = info.ReceiveType,
                SendTime = info.SendTime,
                TranTime = info.TranTime,
                ReceiveReason = info.ReceiveReason,
                BookKeeper = info.BookKeeper,
                Comments = info.Comments,
                AsnNo = info.AsnNo,
                RunsheetNo = info.RunsheetNo,
                //Status = info.Status,                
                //PrintCount = info.PrintCount,
                //PrintTime = info.PrintTime,
                //LastPrintUser = info.LastPrintUser,
                //IsOutput = info.IsOutput,
                //OrganizationFid = info.OrganizationFid,
                //CostCenter = info.CostCenter,
                //ConfirmUser = info.ConfirmUser,
                //ConfirmDate = info.ConfirmDate,
                //LiableUser = info.LiableUser,
                //LiableDate = info.LiableDate,
                //FinanceUser = info.FinanceUser,
                //FinanceDate = info.FinanceDate,
                //SumPartQty = info.SumPartQty,
                //SumOfPrice = info.SumOfPrice,
                //PartBoxCode = info.PartBoxCode,
                //PartBoxName = info.PartBoxName,
                //InspectionFlag = info.InspectionFlag,
                //TrustTime = info.TrustTime,
                //SumWeight = info.SumWeight,
                //SumVolume = info.SumVolume,
                //CustCode = info.CustCode,
                //CustName = info.CustName,
                //Route = info.Route,
                //RouteName = info.RouteName,
                //ValidFlag = info.ValidFlag,
                CreateUser = info.CreateUser,
                CreateDate = info.CreateDate,
                //ModifyUser = info.ModifyUser,
                //ModifyDate = info.ModifyDate,
                //PlanShippingTime = info.PlanShippingTime,
                //PlanDeliveryTime = info.PlanDeliveryTime,
                //InspectionMode = info.InspectionMode,
                //SumPackageQty = info.SumPackageQty,
                ReceiveTypeName = info.ReceiveTypeName,
                InspectionModeName = info.InspectionModeName,
                OrganizationName = info.OrganizationName,
                InoutFlag = info.InoutFlag,
                WmName = info.WmName,
                SourceWmName = info.SourceWmName,
                ZoneName = info.ZoneName,
                SourceZoneName = info.SourceZoneName
            };

            return model;
        }

        public ReceiveDetailInfo GetReceiveDetailInfoByVmiReceiveDetail(VmiReceiveDetailInfo info)
        {
            ReceiveDetailInfo model = new ReceiveDetailInfo
            {
                Id = info.Id,
                Fid = info.Fid,
                Plant = info.Plant,
                SupplierNum = info.SupplierNum,
                WmNo = info.WmNo,
                ZoneNo = info.ZoneNo,
                Dloc = info.Dloc,
                TargetWm = info.TargetWm,
                TargetZone = info.TargetZone,
                TargetDloc = info.TargetDloc,
                PartNo = info.PartNo,
                PartCname = info.PartCname,
                PartEname = info.PartEname,
                MeasuringUnitNo = info.MeasuringUnitNo,
                IdentifyPartNo = info.IdentifyPartNo,
                PackageModel = info.PackageModel,
                Package = info.Package,
                PartType = info.PartType,
                RequiredBoxNum = info.RequiredBoxNum,
                RequiredQty = info.RequiredQty,
                ActualBoxNum = info.ActualBoxNum,
                ActualQty = info.ActualQty,
                BarcodeData = info.BarcodeData,
                TranNo = info.TranNo,
                Dock = info.Dock,
                AssemblyLine = info.AssemblyLine,
                BoxParts = info.BoxParts,
                SequenceNo = info.SequenceNo,
                PickupSeqNo = info.PickupSeqNo,
                RdcDloc = info.RdcDloc,
                InhousePackage = info.InhousePackage,
                InhousePackageModel = info.InhousePackageModel,
                RunsheetNo = info.RunsheetNo,
                SupplierNumSheet = info.SupplierNumSheet,
                BoxPartsSheet = info.BoxPartsSheet,
                ReturnReportFlag = info.ReturnReportFlag,
                OrderNo = info.OrderNo,
                ItemNo = info.ItemNo,
                FinalWm = info.FinalWm,
                FinalZone = info.FinalZone,
                FinalDloc = info.FinalDloc,
                IsScanBox = info.IsScanBox,
                InspectionMode = info.InspectionMode,
                RowNo = info.RowNo,
                OriginPlace = info.OriginPlace,
                PurchaseUnitPrice = info.PurchaseUnitPrice,
                PartPrice = info.PartPrice,
                PartCls = info.PartCls,
                PackageLength = info.PackageLength,
                PackageWidth = info.PackageWidth,
                PackageHeight = info.PackageHeight,
                PerpackageGrossWeight = info.PerpackageGrossWeight,
                PackageVolume = info.PackageVolume,
                SumWeight = info.SumWeight,
                SumVolume = info.SumVolume,
                Comments = info.Comments,
                ValidFlag = info.ValidFlag,
                CreateUser = info.CreateUser,
                CreateDate = info.CreateDate,
                ModifyUser = info.ModifyUser,
                ModifyDate = info.ModifyDate,
                ReceiveFid = info.ReceiveFid,
                CurrentQty = info.CurrentQty,
                CurrentBoxNum = info.CurrentBoxNum,
                CostCenter = info.CostCenter,
                BookKeeper = info.BookKeeper,
                ReceiveType = info.ReceiveType,
                OrganizationFid = info.OrganizationFid,
                ContractNo = info.ContractNo,
                PartUnits = info.PartUnits,
                PartGroup = info.PartGroup
            };

            return model;
        }

        public List<ReceiveDetailInfo> GetReceiveDetailInfosByVmiReceiveDetails(List<VmiReceiveDetailInfo> infos)
        {
            List<ReceiveDetailInfo> list = new List<ReceiveDetailInfo>();
            infos.ForEach(delegate (VmiReceiveDetailInfo temp)
            {
                list.Add(GetReceiveDetailInfoByVmiReceiveDetail(temp));
            });

            return list;
        }


    }
}


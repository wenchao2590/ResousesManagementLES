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
    public class VmiShippingPartBLL
    {
        #region Common
        VmiShippingPartDAL dal = new VmiShippingPartDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<VmiShippingPartInfo></returns>
        public List<VmiShippingPartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            List<VmiShippingPartInfo> vmiShippingPartInfos = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            ///VMI发货时默认计算发货数量
            string vmi_shipping_default_calculate_confirm_qty = new ConfigDAL().GetValueByCode("VMI_SHIPPING_DEFAULT_CALCULATE_CONFIRM_QTY");
            if (!string.IsNullOrEmpty(vmi_shipping_default_calculate_confirm_qty) && vmi_shipping_default_calculate_confirm_qty.ToLower() == "true")
            {
                ///默认预发货数量
                vmiShippingPartInfos.ForEach(delegate (VmiShippingPartInfo info)
                {
                    info.AsnConfirmQty = info.AsnDraftQty.GetValueOrDefault();
                });
            }
            return vmiShippingPartInfos;
        }

        public VmiShippingPartInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(VmiShippingPartInfo info)
        {
            return dal.Add(info);
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Private
        /// <summary>
        /// 加入VMI物料购物车
        /// </summary>
        /// <returns></returns>
        public static void AddCartVmiShippingPartInfo(List<VmiPullOrderDetailInfo> vmiPullOrderDetailInfos, string loginUser)
        {
            if (vmiPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:3x00000027");///传入参数异常

            ///现有数据库中的VMI拉动单明细
            List<VmiPullOrderDetailInfo> vmiPullOrderDetails = new VmiPullOrderDetailDAL().GetList("" +
                "[ID] in (" + string.Join(",", vmiPullOrderDetailInfos.Select(w => w.Id).ToArray()) + ")", string.Empty);
            if (vmiPullOrderDetails.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiPullOrderInfo> vmiPullOrderInfos = new VmiPullOrderDAL().GetList("" +
                "[FID] in ('" + string.Join("','", vmiPullOrderDetails.Select(d => d.OrderFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (vmiPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///当前登录用户的预发货集合
            List<VmiShippingPartInfo> vmiShippingPartInfos = new VmiShippingPartDAL().GetList("[CREATE_USER] = N'" + loginUser + "'", string.Empty);
            StringBuilder @string = new StringBuilder();
            foreach (var vmiPullOrderDetail in vmiPullOrderDetails)
            {
                ///本次预发货数量
                VmiPullOrderDetailInfo orderDetailInfo = vmiPullOrderDetailInfos.FirstOrDefault(d => d.Id == vmiPullOrderDetail.Id);
                if (orderDetailInfo == null)
                    throw new Exception("MC:0x00000084");///数据错误

                VmiPullOrderInfo vmiPullOrderInfo = vmiPullOrderInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == vmiPullOrderDetail.OrderFid.GetValueOrDefault());
                if (vmiPullOrderInfo == null)
                    throw new Exception("MC:0x00000084");///数据错误

                if (vmiPullOrderDetail.RequiredPartQty.GetValueOrDefault() -
                    vmiPullOrderDetail.AsnDraftQty.GetValueOrDefault() -
                    vmiPullOrderDetail.AsnConfirmQty.GetValueOrDefault() -
                    orderDetailInfo.AsnQty < 0)
                    throw new Exception("MC:0x00000497");///需求数量扣除草稿数量以及确认数量后不够本次预发货数量

                ///更新VMI拉动单草稿数量
                @string.AppendLine("update [LES].[TT_MPM_VMI_PULL_ORDER_DETAIL] " +
                        "set [ASN_DRAFT_QTY] = isnull([ASN_DRAFT_QTY],0) + " + orderDetailInfo.AsnQty + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + orderDetailInfo.Id + ";");

                ///预发货集合中是否已存在拉动单明细，存在则累加数量
                VmiShippingPartInfo vmiShippingPartInfo = vmiShippingPartInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == vmiPullOrderDetail.Fid.GetValueOrDefault());
                if (vmiShippingPartInfo == null)
                {
                    ///
                    vmiShippingPartInfo = CreateVmiShippingPartInfo(loginUser);
                    GetVmiShippingPartInfo(vmiPullOrderDetail, ref vmiShippingPartInfo);
                    GetVmiShippingPartInfo(vmiPullOrderInfo, ref vmiShippingPartInfo);
                    ///ASN_DRAFT_QTY,ASN草稿物料数量
                    vmiShippingPartInfo.AsnDraftQty = orderDetailInfo.AsnQty;
                    @string.AppendLine(VmiShippingPartDAL.GetInsertSql(vmiShippingPartInfo));
                }
                else
                {
                    @string.AppendLine("update [LES].[TE_MPM_VMI_SHIPPING_PART] " +
                        "set [ASN_DRAFT_QTY] = isnull([ASN_DRAFT_QTY],0) + " + orderDetailInfo.AsnQty + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + vmiShippingPartInfo.Id + ";");
                }
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
        }
        /// <summary>
        /// 根据用户名获取购物车列表
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public List<VmiShippingPartInfo> GetVmiShippingPartInfosByLoginUser(string loginUser)
        {
            List<VmiShippingPartInfo> vmiShippingPartInfos = dal.GetList(string.Format("[CREATE_USER] = '{0}'", loginUser), string.Empty);
            ///客户端需要提示信息
            if (vmiShippingPartInfos.Count == 0)
                throw new Exception("MC:3x00000029");
            return vmiShippingPartInfos;
        }
        /// <summary>
        /// 批量从购物车移除
        /// </summary>
        public bool BatchdeletingInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiShippingPartInfo> vmiShippingPartInfos = new List<VmiShippingPartInfo>();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                string[] keyValues = rowsKeyValue.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValues.Length == 0)
                    throw new Exception("MC:0x00000084");///数据错误 
                if (keyValues.Length == 1)
                    throw new Exception("MC:0x00000496");///预发货数量不能为空

                VmiShippingPartInfo vmiShippingPartInfo = new VmiShippingPartInfo();
                vmiShippingPartInfo.Id = Convert.ToInt64(keyValues[0]);
                vmiShippingPartInfo.AsnDraftQty = Convert.ToDecimal(keyValues[1]);
                vmiShippingPartInfos.Add(vmiShippingPartInfo);
            }

            List<VmiShippingPartInfo> vmiShippingParts = dal.GetList("[ID] in (" + string.Join(",", vmiShippingPartInfos.Select(d => d.Id).ToArray()) + ")", string.Empty);
            if (vmiShippingParts.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            StringBuilder @string = new StringBuilder();
            foreach (var vmiShippingPart in vmiShippingParts)
            {
                ///从购物车删除
                @string.AppendLine("update [LES].[TE_MPM_VMI_SHIPPING_PART] " +
                    "set [VALID_FLAG] = 0,[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + vmiShippingPart.Id + ";");
                ///草稿数量退回拉动单
                @string.AppendLine("update [LES].[TT_MPM_VMI_PULL_ORDER_DETAIL] " +
                    "set [ASN_DRAFT_QTY] = isnull([ASN_DRAFT_QTY],0) - " + vmiShippingPart.AsnDraftQty.GetValueOrDefault() + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() " +
                    "where [FID] = N'" + vmiShippingPart.Fid.GetValueOrDefault() + "';");
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
        /// 发货(提交）
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误 

            List<VmiShippingPartInfo> vmiShippingPartInfos = new List<VmiShippingPartInfo>();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                string[] keyValues = rowsKeyValue.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValues.Length == 0)
                    throw new Exception("MC:0x00000084");///数据错误 
                if (keyValues.Length == 1)
                    throw new Exception("MC:0x00000496");///预发货数量不能为空

                VmiShippingPartInfo vmiShippingPartInfo = new VmiShippingPartInfo();
                vmiShippingPartInfo.Id = Convert.ToInt64(keyValues[0]);
                vmiShippingPartInfo.AsnConfirmQty = Convert.ToDecimal(keyValues[1]);
                vmiShippingPartInfos.Add(vmiShippingPartInfo);
            }

            List<VmiShippingPartInfo> vmiShippingParts = dal.GetList("[ID] in (" + string.Join(",", vmiShippingPartInfos.Select(d => d.Id).ToArray()) + ")", string.Empty);
            if (vmiShippingParts.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            List<VmiPullOrderInfo> vmiPullOrderInfos = new VmiPullOrderDAL().GetList("" +
                "[FID] in ('" + string.Join("','", vmiShippingParts.Select(d => d.OrderFid.GetValueOrDefault().ToString()).ToArray()) + "') and " +
                "[ORDER_STATUS] <> " + (int)PullOrderStatusConstants.Released + "", string.Empty);
            if (vmiPullOrderInfos.Count > 0)
                throw new Exception("MC:0x00000378");///状态为已发布时才能进行发货操作
                                                     ///TODO:此时对应购物车中的相关数据是否需要清理

            ///预执行SQL脚本
            StringBuilder @string = new StringBuilder();
            ///根据零件类、拉动模式等条件分组
            var vmiShippingPartBoxs = vmiShippingParts.
                GroupBy(d => new { d.PartBoxCode, d.PullMode, d.RouteCode, d.Plant, d.SWmNo, d.SZoneNo, d.TWmNo, d.TZoneNo, d.TDock }).
                Select(d => new { d.Key }).ToList();

            ///获取零件仓储信息集合
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("[PART_NO] in ('" + string.Join("','", vmiShippingParts.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            ///发布VMI出库单时实发数量等于需求数量
            string release_vmi_output_actual_qty_equals_required = new ConfigDAL().GetValueByCode("RELEASE_VMI_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
            ///遍历零件类
            foreach (var vmiShippingPartBox in vmiShippingPartBoxs)
            {
                ///
                List<VmiShippingPartInfo> partInfos = vmiShippingParts.Where(d =>
                d.PartBoxCode == vmiShippingPartBox.Key.PartBoxCode &&
                d.PullMode == vmiShippingPartBox.Key.PullMode &&
                d.RouteCode == vmiShippingPartBox.Key.RouteCode &&
                d.Plant == vmiShippingPartBox.Key.Plant &&
                d.SWmNo == vmiShippingPartBox.Key.SWmNo &&
                d.SZoneNo == vmiShippingPartBox.Key.SZoneNo &&
                d.TWmNo == vmiShippingPartBox.Key.TWmNo &&
                d.TZoneNo == vmiShippingPartBox.Key.TZoneNo &&
                d.TDock == vmiShippingPartBox.Key.TDock).ToList();
                if (partInfos.Count == 0)
                    throw new Exception("MC:0x00000084");///数据错误

                ///
                VmiOutputInfo vmiOutputInfo = VmiOutputBLL.CreateVmiOutputInfo(loginUser);
                ///OUTPUT_NO,出库单号
                vmiOutputInfo.OutputNo = new SeqDefineDAL().GetCurrentCode("VMI_OUTPUT_NO");
                ///ASN_NO,ASN编号,TODO:目前以是否写入了ASN_NO作为区别是否编辑ASN的分类，后续考虑增加字段ASN_FLAG？
                vmiOutputInfo.AsnNo = vmiOutputInfo.OutputNo;
                ///WM_NO,仓库编码
                vmiOutputInfo.WmNo = vmiShippingPartBox.Key.SWmNo;
                ///ZONE_NO,存贮区编码
                vmiOutputInfo.ZoneNo = vmiShippingPartBox.Key.SZoneNo;
                ///T_WM_NO,目标仓库代码
                vmiOutputInfo.TWmNo = vmiShippingPartBox.Key.TWmNo;
                ///T_ZONE_NO,目标存储区代码
                vmiOutputInfo.TZoneNo = vmiShippingPartBox.Key.TZoneNo;
                ///T_DOCK,目标道口代码
                vmiOutputInfo.TDock = vmiShippingPartBox.Key.TDock;
                ///PART_BOX_CODE,零件类代码
                vmiOutputInfo.PartBoxCode = vmiShippingPartBox.Key.PartBoxCode;
                ///ROUTE,送货路径
                vmiOutputInfo.Route = vmiShippingPartBox.Key.RouteCode;
                ///PULL_MODE,拉动方式
                vmiOutputInfo.PullMode = vmiShippingPartBox.Key.PullMode;
                ///OUTPUT_TYPE,出库类型
                vmiOutputInfo.OutputType = (int)VmiOutputTypeConstants.PullingOutbound;
                ///SEND_TIME,发送时间
                vmiOutputInfo.SendTime = DateTime.Now;
                ///STATUS,出库单状态
                vmiOutputInfo.Status = (int)WmmOrderStatusConstants.Published;
                ///
                @string.AppendLine(VmiOutputDAL.GetInsertSql(vmiOutputInfo));
                ///行号
                int rowNo = 0;
                foreach (var partInfo in partInfos)
                {
                    VmiShippingPartInfo shippingPartInfo = vmiShippingPartInfos.FirstOrDefault(d => d.Id == partInfo.Id);
                    if (shippingPartInfo == null)
                        throw new Exception("MC:0x00000084");///数据错误

                    if (partInfo.AsnDraftQty.GetValueOrDefault() < shippingPartInfo.AsnConfirmQty.GetValueOrDefault())
                        throw new Exception("MC:0x00000501");///发货数量不能超过预发货数量

                    ///获取对应零件仓储信息
                    PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                    d.WmNo == partInfo.TWmNo &&
                    d.ZoneNo == partInfo.TZoneNo &&
                    d.PartNo == partInfo.PartNo &&
                    d.SupplierNum == partInfo.SupplierNum);
                    if (partsStockInfo == null)
                        throw new Exception("MC:0x00000451");///物料仓储信息错误

                    ///页面提交来的发货数量
                    partInfo.RequiredPartQty = shippingPartInfo.AsnConfirmQty;
                    ///
                    VmiOutputDetailInfo vmiOutputDetailInfo = VmiOutputDetailBLL.CreateVmiOutputDetailInfo(loginUser);
                    ///ROW_NO,行号
                    vmiOutputDetailInfo.RowNo = ++rowNo;
                    VmiOutputDetailBLL.GetVmiOutputDetailInfo(partInfo, ref vmiOutputDetailInfo);
                    VmiOutputDetailBLL.GetVmiOutputDetailInfo(vmiOutputInfo, ref vmiOutputDetailInfo);
                    VmiOutputDetailBLL.GetVmiOutputDetailInfo(partsStockInfo, ref vmiOutputDetailInfo);
                    VmiOutputDetailBLL.GetVmiOutputDetailInfo(ref vmiOutputDetailInfo);
                    if (!string.IsNullOrEmpty(release_vmi_output_actual_qty_equals_required) && release_vmi_output_actual_qty_equals_required.ToLower() == "true")
                    {
                        ///ACTUAL_BOX_NUM,实际包装数
                        vmiOutputDetailInfo.ActualBoxNum = vmiOutputDetailInfo.RequiredBoxNum;
                        ///ACTUAL_QTY,实际数量
                        vmiOutputDetailInfo.ActualQty = vmiOutputDetailInfo.RequiredQty;
                    }
                    @string.AppendLine(VmiOutputDetailDAL.GetInsertSql(vmiOutputDetailInfo));
                    ///
                    string validFlagSql = string.Empty;
                    if (partInfo.AsnDraftQty.GetValueOrDefault() == shippingPartInfo.AsnConfirmQty.GetValueOrDefault())
                        validFlagSql = ",[VALID_FLAG] = 0";
                    ///
                    @string.AppendLine("update [LES].[TE_MPM_VMI_SHIPPING_PART] " +
                        "set [ASN_DRAFT_QTY] = isnull([ASN_DRAFT_QTY],0) - " + shippingPartInfo.AsnConfirmQty.GetValueOrDefault() + "" + validFlagSql + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID] = " + shippingPartInfo.Id + ";");
                    ///
                    @string.AppendLine("update [LES].[TT_MPM_VMI_PULL_ORDER_DETAIL] " +
                        "set [ASN_DRAFT_QTY] = isnull([ASN_DRAFT_QTY],0) - " + shippingPartInfo.AsnConfirmQty.GetValueOrDefault() + "," +
                        "[ASN_CONFIRM_QTY] = isnull([ASN_CONFIRM_QTY],0) + " + shippingPartInfo.AsnConfirmQty.GetValueOrDefault() + "," +
                        "[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [FID] = N'" + partInfo.Fid.GetValueOrDefault() + "';");
                }
            }
            ///
            using (var trans = new TransactionScope())
            {

                if (!CommonDAL.ExecuteNonQueryBySql(@string.ToString()))
                    throw new Exception("MC:0x00000173");///操作失败
                trans.Complete();
            }

            return true;

        }

        #endregion

        #region Interface
        /// <summary>
        /// Create VmiShippingPartInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>VmiShippingPartInfo</returns>
        public static VmiShippingPartInfo CreateVmiShippingPartInfo(string loginUser)
        {
            VmiShippingPartInfo info = new VmiShippingPartInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///MODIFY_DATE,最后修改时间
            info.ModifyDate = null;
            ///MODIFY_USER,最后修改用户
            info.ModifyUser = null;


            ///ASN_CONFIRM_QTY,ASN确认物料数量
            info.AsnConfirmQty = null;
            ///ACTUAL_PACKAGE_QTY,实际包装数
            info.ActualPackageQty = null;
            ///ACTUAL_PART_QTY,实际物料数量
            info.ActualPartQty = null;

            return info;
        }
        /// <summary>
        /// VmiPullOrderDetailInfo -> VmiShippingPartInfo
        /// </summary>
        /// <param name="vmiPullOrderDetailInfo"></param>
        /// <param name="vmiShippingPartInfo"></param>
        public static void GetVmiShippingPartInfo(VmiPullOrderDetailInfo vmiPullOrderDetailInfo, ref VmiShippingPartInfo info)
        {
            if (vmiPullOrderDetailInfo == null) return;
            ///FID,
            info.Fid = vmiPullOrderDetailInfo.Fid;
            ///ORDER_FID,拉动单外键
            info.OrderFid = vmiPullOrderDetailInfo.OrderFid;
            ///ORDER_CODE,拉动单号
            info.OrderCode = vmiPullOrderDetailInfo.OrderCode;
            ///ROW_NO,行号
            info.RowNo = vmiPullOrderDetailInfo.RowNo;
            ///SUPPLIER_NUM,供应商代码
            info.SupplierNum = vmiPullOrderDetailInfo.SupplierNum;
            ///WORKSHOP_SECTION,工段
            info.WorkshopSection = vmiPullOrderDetailInfo.WorkshopSection;
            ///LOCATION,工位
            info.Location = vmiPullOrderDetailInfo.Location;
            ///PART_NO,物料号
            info.PartNo = vmiPullOrderDetailInfo.PartNo;
            ///PART_VERSION,物料版本
            info.PartVersion = vmiPullOrderDetailInfo.PartVersion;
            ///PART_CNAME,物料中文描述
            info.PartCname = vmiPullOrderDetailInfo.PartCname;
            ///PART_ENAME,物料英文描述
            info.PartEname = vmiPullOrderDetailInfo.PartEname;
            ///MEASURING_UNIT_NO,单位
            info.MeasuringUnitNo = vmiPullOrderDetailInfo.MeasuringUnitNo;
            ///PACKAGE,单包装数量
            info.Package = vmiPullOrderDetailInfo.Package;
            ///PACKAGE_MODEL,包装编号
            info.PackageModel = vmiPullOrderDetailInfo.PackageModel;
            ///REQUIRED_PACKAGE_QTY,需求包装数
            info.RequiredPackageQty = vmiPullOrderDetailInfo.RequiredPackageQty;
            ///REQUIRED_PART_QTY,需求物料数量
            info.RequiredPartQty = vmiPullOrderDetailInfo.RequiredPartQty;
            ///COMMENTS,备注
            info.Comments = vmiPullOrderDetailInfo.Comments;
        }
        /// <summary>
        /// VmiPullOrderInfo -> VmiShippingPartInfo
        /// </summary>
        /// <param name="vmiPullOrderInfo"></param>
        /// <param name="info"></param>
        public static void GetVmiShippingPartInfo(VmiPullOrderInfo vmiPullOrderInfo, ref VmiShippingPartInfo info)
        {
            if (vmiPullOrderInfo == null) return;
            ///PULL_MODE,拉动模式
            info.PullMode = vmiPullOrderInfo.PullMode;
            ///PART_BOX_CODE,零件类代码
            info.PartBoxCode = vmiPullOrderInfo.PartBoxCode;
            ///ROUTE_CODE,路径代码
            info.RouteCode = vmiPullOrderInfo.RouteCode;
            ///PLANT,工厂
            info.Plant = vmiPullOrderInfo.Plant;
            ///S_WM_NO,来源仓库
            info.SWmNo = vmiPullOrderInfo.SWmNo;
            ///S_ZONE_NO,来源存储区
            info.SZoneNo = vmiPullOrderInfo.SZoneNo;
            ///T_WM_NO,目标仓库
            info.TWmNo = vmiPullOrderInfo.TWmNo;
            ///T_ZONE_NO,目标存储区
            info.TZoneNo = vmiPullOrderInfo.TZoneNo;
            ///T_DOCK,目标道口
            info.TDock = vmiPullOrderInfo.Dock;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public static void GetVmiShippingPartInfo(ref VmiShippingPartInfo info)
        {

        }
        #endregion

    }
}
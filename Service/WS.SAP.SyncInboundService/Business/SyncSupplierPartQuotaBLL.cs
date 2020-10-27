using BLL.LES;
using BLL.SYS;
using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WS.SAP.SyncInboundService
{
    /// <summary>
    /// SyncSupplierPartQuotaBLL
    /// </summary>
    public class SyncSupplierPartQuotaBLL
    {
        /// <summary>
        /// SAP供应商配额基础数据同步
        /// </summary>
        /// <returns></returns>
        public static void Sync(string loginUser)
        {
            List<SapSupplierQuotaInfo> sapSupplierQuotaInfos = new SapSupplierQuotaBLL().GetListByPage("" +
                "[PROCESS_FLAG] in (" + (int)ProcessFlagConstants.Untreated + "," + (int)ProcessFlagConstants.Resend + ")", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;
            ///执行的SQL语句
            StringBuilder @string = new StringBuilder();
            ///是否启用SRM系统标记
            string enable_srm_flag = new ConfigDAL().GetValueByCode("ENABLE_SRM_FLAG");
            ///是否启用WMS系统标记
            string enable_vmi_flag = new ConfigDAL().GetValueByCode("ENABLE_VMI_FLAG");
            ///同步供应商基础数据
            @string.AppendLine(GetSyncSupplierSql(sapSupplierQuotaInfos, loginUser));
            ///获取业务表中要变更的数据集合,准备对比
            List<SupplierPartQuotaInfo> supplierPartQuotaInfos = new SupplierPartQuotaBLL().GetListForInterfaceDataSync(sapSupplierQuotaInfos.Select(d => d.PartNo).ToList());
            ///物料信息
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(sapSupplierQuotaInfos.Select(d => d.PartNo).ToList());
            ///VMI供应商关系
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", sapSupplierQuotaInfos.Select(d => d.Lifnr).ToArray()) + "'", string.Empty);
            ///VMI仓库
            List<WarehouseInfo> warehouseInfos = new List<WarehouseInfo>();
            if (vmiSupplierInfos.Count > 0)
                warehouseInfos = new WarehouseBLL().GetList("" +
                    "[WAREHOUSE] in ('" + string.Join("','", vmiSupplierInfos.Select(d => d.WmNo).ToArray()) + "') and " +
                    "[WAREHOUSE_TYPE] = " + (int)WarehouseTypeConstants.VMI + "", string.Empty);
            ///获取工厂信息
            List<PlantInfo> plantInfos = new PlantBLL().GetListForInterfaceDataSync();
            ///已处理完成的ID
            List<long> dealedIds = new List<long>();
            ///逐条处理中间表数据
            foreach (var sapSupplierQuotaInfo in sapSupplierQuotaInfos)
            {
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.SapPlantCode == sapSupplierQuotaInfo.Werks);
                if (plantInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    @string.AppendLine("update [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'3x00000019'," +///工厂信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapSupplierQuotaInfo.Id + ";");
                    continue;
                }
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.Plant == plantInfo.Plant && d.PartNo == sapSupplierQuotaInfo.PartNo);
                if (maintainPartsInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    @string.AppendLine("update [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000417'," +///物料信息错误
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapSupplierQuotaInfo.Id + ";");
                    continue;
                }
                ///VMI供应商关系，且未启用LES的VMI模块的
                List<VmiSupplierInfo> vmiSuppliers = vmiSupplierInfos.Where(d => d.SupplierNum == sapSupplierQuotaInfo.Lifnr && !d.VmiFlag.GetValueOrDefault()).ToList();
                List<WarehouseInfo> warehouses = new List<WarehouseInfo>();
                if (vmiSuppliers.Count > 0) warehouses = warehouseInfos.Where(d => vmiSuppliers.Select(v => v.WmNo).Contains(d.Warehouse)).ToList();

                ///当前业务数据表中无此工厂代码+物料编号+供应商信息时需要新增
                SupplierPartQuotaInfo supplierPartQuotaInfo = supplierPartQuotaInfos.FirstOrDefault(d =>
                d.PartNo == maintainPartsInfo.PartNo &&
                d.Plant == plantInfo.Plant &&
                d.SupplierNum == sapSupplierQuotaInfo.Lifnr);
                ///标识该配额需要删除
                ///停供作为删除处理，ZSTOP = X时标识停供
                if (sapSupplierQuotaInfo.Flag.ToUpper() == "D" || sapSupplierQuotaInfo.Zstop.ToUpper() == "X")
                {
                    ///根据工厂代码+物料编号+供应商对配额信息进行逻辑删除
                    @string.AppendLine("update [LES].[TM_BAS_SUPPLIER_PART_QUOTA] " +
                        "set [VALID_FLAG] = 0," +
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [PART_NO] = N'" + sapSupplierQuotaInfo.PartNo + "' and " +
                        "[PLANT] = N'" + plantInfo.Plant + "' and " +
                        "[SUPPLIER_NUM] = N'" + sapSupplierQuotaInfo.Lifnr + "' and " +
                        "[VALID_FLAG] = 1;");
                    dealedIds.Add(sapSupplierQuotaInfo.Id);

                    if (supplierPartQuotaInfo == null)
                        supplierPartQuotaInfo = SupplierPartQuotaBLL.CreateSupplierPartQuotaInfo(loginUser);
                    ///SapSupplierQuotaInfo -> SupplierPartQuotaInfo
                    SupplierPartQuotaBLL.GetSupplierPartQuotaInfo(sapSupplierQuotaInfo, ref supplierPartQuotaInfo);

                    @string.AppendLine(SupplierPartQuotaBLL.GetSyncVmiSupplierPartSql(
                        sapSupplierQuotaInfo.SupplierName,
                        maintainPartsInfo.PartCname,
                        warehouses,
                        supplierPartQuotaInfo,
                        enable_srm_flag,
                        enable_vmi_flag,
                        true,
                        loginUser));
                    continue;
                }
                ///
                if (supplierPartQuotaInfo == null)
                {
                    supplierPartQuotaInfo = SupplierPartQuotaBLL.CreateSupplierPartQuotaInfo(loginUser);
                    ///SapSupplierQuotaInfo -> SupplierPartQuotaInfo
                    SupplierPartQuotaBLL.GetSupplierPartQuotaInfo(sapSupplierQuotaInfo, ref supplierPartQuotaInfo);
                    ///
                    @string.AppendLine(SupplierPartQuotaDAL.GetInsertSql(supplierPartQuotaInfo));
                    ///加入后以免影响下次判断
                    supplierPartQuotaInfos.Add(supplierPartQuotaInfo);
                    ///
                    dealedIds.Add(sapSupplierQuotaInfo.Id);
                    ///
                    @string.AppendLine(SupplierPartQuotaBLL.GetSyncVmiSupplierPartSql(
                        sapSupplierQuotaInfo.SupplierName,
                        maintainPartsInfo.PartCname,
                        warehouses,
                        supplierPartQuotaInfo,
                        enable_srm_flag,
                        enable_vmi_flag,
                        false,
                        loginUser));
                    continue;
                }
                ///
                if (supplierPartQuotaInfo.Id == 0) continue;
                ///
                @string.AppendLine("update [LES].[TM_BAS_SUPPLIER_PART_QUOTA] set "
                    + "[EFFECTIVE_DATE] = N'" + sapSupplierQuotaInfo.IDate + "',"
                    + "[INVALID_DATE] = N'" + sapSupplierQuotaInfo.EDate + "',"
                    + "[QUOTE] = " + sapSupplierQuotaInfo.Quote.GetValueOrDefault() + ","
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + " where [FID] = N'" + supplierPartQuotaInfo.Fid.GetValueOrDefault() + "';");
                ///
                @string.AppendLine(SupplierPartQuotaBLL.GetSyncVmiSupplierPartSql(
                        sapSupplierQuotaInfo.SupplierName,
                        maintainPartsInfo.PartCname,
                        warehouses,
                        supplierPartQuotaInfo,
                        enable_srm_flag,
                        enable_vmi_flag,
                        false,
                        loginUser));

                dealedIds.Add(sapSupplierQuotaInfo.Id);
            }
            ///
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] " +
                    "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE()," +
                    "[COMMENTS] = NULL," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");

            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
        }

        /// <summary>
        /// 同步供应商基础数据
        /// </summary>
        /// <param name="sapSupplierQuotaInfos"></param>
        /// <param name="loginUser"></param>
        private static string GetSyncSupplierSql(List<SapSupplierQuotaInfo> sapSupplierQuotaInfos, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///获取业务表中要变更的数据集合,准备对比
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetListForInterfaceDataSync(sapSupplierQuotaInfos.Select(d => d.Lifnr).ToList());
            ///
            foreach (var sapSupplierQuotaInfo in sapSupplierQuotaInfos)
            {
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == sapSupplierQuotaInfo.Lifnr);
                if (supplierInfo == null)
                {
                    supplierInfo = new SupplierInfo();
                    supplierInfo.Fid = Guid.NewGuid();
                    supplierInfo.SupplierNum = sapSupplierQuotaInfo.Lifnr;
                    supplierInfo.SupplierName = sapSupplierQuotaInfo.SupplierName;
                    supplierInfo.SupplierType = (int)SupplierTypeConstants.MaterialSupplier;
                    supplierInfo.CreateUser = loginUser;
                    supplierInfo.CreateDate = DateTime.Now;
                    @string.AppendLine(SupplierDAL.GetInsertSql(supplierInfo));
                    supplierInfos.Add(supplierInfo);
                    continue;
                }
                ///更新
                @string.AppendLine("update [LES].[TM_BAS_SUPPLIER] " +
                    "set [SUPPLIER_NAME] = N'" + sapSupplierQuotaInfo.SupplierName + "'," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [FID] = N'" + supplierInfo.Fid.GetValueOrDefault() + "';");
            }
            return @string.ToString();
        }
    }
}

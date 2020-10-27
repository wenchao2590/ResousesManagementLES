namespace WS.SRM.OutboundDataService
{
    /// <summary>
    /// LES-SRM-001 VMI供应商物料关系  
    /// 对应中间表  TI_IFM_SRM_VMI_SUPPLIER_PART
    /// </summary>
    public class BFDAVmiSupplierPartInfo
    {
        /// <summary>
        /// VMI仓库代码
        /// </summary>
        public string VmiWarehouseCode;

        /// <summary>
        ///  VMI仓库名称
        /// </summary>
        public string VmiWarehouseName;

        /// <summary>
        ///供应商代码
        /// </summary>
        public string SupplierCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName;

        /// <summary>
        ///  物料编号
        /// </summary>
        public string PartNo;

        /// <summary>
        /// 物料中文描述
        /// </summary>
        public string PartCName;

        /// <summary>
        ///   删除标记
        /// </summary>
        public string DeleteFlag;

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string Plant;
    }
}

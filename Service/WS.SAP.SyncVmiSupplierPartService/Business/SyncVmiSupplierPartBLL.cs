using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.LES;
using BLL.LES;

namespace WS.SAP.SyncVmiSupplierPartService.Business
{
    /// <summary>
    /// VMI供应商物料关系同步- 当物料供应商关系数据新增、修改、删除时，同步通知SRM系统
    /// </summary>
    public class SyncVmiSupplierPartBLL
    {
        //TM_BAS_VMI_SUPPLIER	VMI供应商关系表
        //TI_SRM_VMI_SUPPLIER_PART	VMI供应商物料关系SRM中间表

        ///当VMI供应商关系数据新增或修改时，根据供应商代码③从供应商关系中获取该供应商的所有物料关系，默认删除标记⑦为false

        public bool OperationSyncVmiSupplierPart()
        {
            bool SyncStatus = false;
            //获取没有处理的业务表的集合
            int dataCnt;
            List<SupplierPartQuotaInfo> SupplierPartQuotasList = new SupplierPartQuotaBLL().GetListByPage("", "[ID]", 1, 200, out dataCnt);
            if (dataCnt == 0) return true;


            return SyncStatus;
        }        





    }
}

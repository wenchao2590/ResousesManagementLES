using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class VmiTransBLL
    {
        #region Common
        VmiTransDAL dal = new VmiTransDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<VmiTransInfo></returns>
        public List<VmiTransInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public VmiTransInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(VmiTransInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="loginUser">用户</param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<VmiTransInfo></returns>
        public List<VmiTransInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Create VmiTransInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>VmiTransInfo</returns>
        public static VmiTransInfo CreateVmiTransInfo(string loginUser)
        {
            VmiTransInfo info = new VmiTransInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///OUTPUT_NO
            info.OutputNo = null;
            ///PLANT
            info.Plant = null;
            ///SUPPLIER_NUM
            info.SupplierNum = null;
            ///WM_NO
            info.WmNo = null;
            ///ZONE_NO
            info.ZoneNo = null;
            ///T_WM_NO
            info.TWmNo = null;
            ///T_ZONE_NO
            info.TZoneNo = null;
            ///T_DOCK
            info.TDock = null;
            ///PART_BOX_CODE
            info.PartBoxCode = null;
            ///SEND_TIME
            info.SendTime = null;
            ///TRANS_TYPE
            info.TransType = null;
            ///TRAN_TIME
            info.TranTime = null;
            ///OUTPUT_REASON
            info.OutputReason = null;
            ///BOOK_KEEPER
            info.BookKeeper = null;
            ///CONFIRM_FLAG
            info.ConfirmFlag = null;
            ///PLAN_NO
            info.PlanNo = null;
            ///ASN_NO
            info.AsnNo = null;
            ///RUNSHEET_NO
            info.RunsheetNo = null;
            ///ASSEMBLY_LINE
            info.AssemblyLine = null;
            ///PLANT_ZONE
            info.PlantZone = null;
            ///WORKSHOP
            info.Workshop = null;
            ///TRANS_SUPPLIER_NUM
            info.TransSupplierNum = null;
            ///PART_TYPE
            info.PartType = null;
            ///SUPPLIER_TYPE
            info.SupplierType = null;
            ///RUNSHEET_CODE
            info.RunsheetCode = null;
            ///ERP_FLAG
            info.ErpFlag = null;
            ///LOGICAL_PK
            info.LogicalPk = null;
            ///BUSINESS_PK
            info.BusinessPk = null;
            ///ROUTE
            info.Route = null;
            ///REQUEST_TIME
            info.RequestTime = null;
            ///CUST_CODE
            info.CustCode = null;
            ///CUST_NAME
            info.CustName = null;
            ///COST_CENTER
            info.CostCenter = null;
            ///ORGANIZATION_FID
            info.OrganizationFid = null;
            ///CONFIRM_USER
            info.ConfirmUser = null;
            ///CONFIRM_DATE
            info.ConfirmDate = null;
            ///LIABLE_USER
            info.LiableUser = null;
            ///LIABLE_DATE
            info.LiableDate = null;
            ///FINANCE_USER
            info.FinanceUser = null;
            ///FINANCE_DATE
            info.FinanceDate = null;
            ///SUM_PART_QTY
            info.SumPartQty = null;
            ///SUM_OF_PRICE
            info.SumOfPrice = null;
            ///STATUS
            info.Status = null;
            ///CONVEYANCE
            info.Conveyance = null;
            ///CARRIER_TEL
            info.CarrierTel = null;
            ///SUM_WEIGHT
            info.SumWeight = null;
            ///SUM_VOLUME
            info.SumVolume = null;
            ///PLAN_SHIPPING_TIME
            info.PlanShippingTime = null;
            ///PLAN_DELIVERY_TIME
            info.PlanDeliveryTime = null;
            ///PRINT_COUNT
            info.PrintCount = null;
            ///PRINT_TIME
            info.PrintTime = null;
            ///COMMENTS
            info.Comments = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///LAST_PRINT_USER
            info.LastPrintUser = null;
            ///SUM_PACKAGE_QTY
            info.SumPackageQty = null;
            ///PULL_MODE
            info.PullMode = null;
            return info;
        }
        #endregion
    }
}


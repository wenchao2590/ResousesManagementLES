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
    public class PlanPullOrderDetailBLL
    {
        #region Common
        PlanPullOrderDetailDAL dal = new PlanPullOrderDetailDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlanPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanPullOrderDetailInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PlanPullOrderDetailInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, int id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion


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

            List<PlanPullOrderDetailInfo> planPullOrderDetailInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (planPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000053");///请选中行数据

            ///对应计划拉动单状态必须是10.已创建
            int cnt = new PlanPullOrderDAL().GetCounts("[FID] in ('" + string.Join("','", planPullOrderDetailInfos.Select(d => d.OrderFid.GetValueOrDefault())) + "') and [ORDER_STATUS] <> " + (int)PullOrderStatusConstants.Created + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000000");///TODO 计划拉动单已创建状态才可以删除明细数据

            ///当同一张计划拉动单下所有明细都被删除时，更新拉动单状态为90.已作废
            StringBuilder sqlBilder = new StringBuilder();
            foreach (PlanPullOrderDetailInfo planPullOrderDetailInfo in planPullOrderDetailInfos)
            {
                sqlBilder.AppendLine("IF NOT EXISTS (SELECT 1 FROM [LES].[TT_MPM_PLAN_PULL_ORDER_DETAIL] with(nolock) WHERE [ID] <> " + planPullOrderDetailInfo.Id + " and [VALID_FLAG] = 1 and [ORDER_FID] = N'" + planPullOrderDetailInfo.OrderFid.GetValueOrDefault() + "')");
                sqlBilder.AppendLine("BEGIN");
                sqlBilder.AppendLine("    UPDATE [LES].[TT_MPM_PLAN_PULL_ORDER] ");
                sqlBilder.AppendLine("    SET [ORDER_STATUS] = " + (int)PullOrderStatusConstants.Invalid + ",");
                sqlBilder.AppendLine("           [MODIFY_USER] = N'" + loginUser + "',");
                sqlBilder.AppendLine("           [MODIFY_DATE] = GETDATE() ");
                sqlBilder.AppendLine("    WHERE [FID] = '" + planPullOrderDetailInfo.Fid.GetValueOrDefault() + "';");
                sqlBilder.AppendLine("END");
                sqlBilder.AppendLine("UPDATE [LES].[TT_MPM_PLAN_PULL_ORDER_DETAIL] ");
                sqlBilder.AppendLine("SET [VALID_FLAG] = 0,");
                sqlBilder.AppendLine("       [MODIFY_USER] = N'" + loginUser + "',");
                sqlBilder.AppendLine("       [MODIFY_DATE] = GETDATE() ");
                sqlBilder.AppendLine("WHERE [ID] = " + planPullOrderDetailInfo.Id + ";");
            }

            using (TransactionScope trans = new TransactionScope())
            {
                if (sqlBilder.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(sqlBilder.ToString());
                trans.Complete();
            }

            return true;
        }


        #region Interface
        /// <summary>
        /// Create PlanPullOrderDetailInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>PlanPullOrderDetailInfo</returns>
        public static PlanPullOrderDetailInfo CreatePlanPullOrderDetailInfo(string loginUser)
        {
            PlanPullOrderDetailInfo info = new PlanPullOrderDetailInfo
            {
                ///FID,
                Fid = Guid.NewGuid(),
                ///VALID_FLAG,逻辑删除标记
                ValidFlag = true,
                ///CREATE_USER,创建人
                CreateUser = loginUser,
                ///CREATE_DATE,创建时间
                CreateDate = DateTime.Now
            };
            return info;
        }
        /// <summary>
        /// ReceiveDetailInfo -> PlanPullOrderDetailInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="planPullOrderDetailInfo"></param>
        public static void GetPlanPullOrderDetailInfo(ReceiveDetailInfo receiveDetailInfo, ref PlanPullOrderDetailInfo planPullOrderDetailInfo)
        {
            if (receiveDetailInfo == null) return;
            ///SUPPLIER_NUM,供应商代码
            planPullOrderDetailInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            ///PART_NO,物料号
            planPullOrderDetailInfo.PartNo = receiveDetailInfo.PartNo;
            ///PART_CNAME,物料中文描述
            planPullOrderDetailInfo.PartCname = receiveDetailInfo.PartCname;
            ///PART_ENAME,物料英文描述
            planPullOrderDetailInfo.PartEname = receiveDetailInfo.PartEname;
            ///MEASURING_UNIT_NO,单位
            planPullOrderDetailInfo.MeasuringUnitNo = receiveDetailInfo.MeasuringUnitNo;
            ///INBOUND_PACKAGE_QTY,入库单包装数量
            planPullOrderDetailInfo.InboundPackageQty = receiveDetailInfo.Package;
            ///INBOUND_PACKAGE_MODEL,入库包装编号
            planPullOrderDetailInfo.InboundPackageModel = receiveDetailInfo.PackageModel;
            ///REQUIRED_PART_QTY,需求物料数量
            planPullOrderDetailInfo.RequiredPartQty = receiveDetailInfo.RequiredQty.GetValueOrDefault() - receiveDetailInfo.ActualQty.GetValueOrDefault();
            ///REQUIRED_PACKAGE_QTY,需求包装数
            if (receiveDetailInfo.Package.GetValueOrDefault() > 0)
                planPullOrderDetailInfo.RequiredPackageQty = Convert.ToInt32(Math.Ceiling(planPullOrderDetailInfo.RequiredPartQty.GetValueOrDefault() / receiveDetailInfo.Package.GetValueOrDefault()));
        }
        /// <summary>
        /// PlanPullOrderInfo -> PlanPullOrderDetailInfo
        /// </summary>
        /// <param name="planPullOrderInfo"></param>
        /// <param name="planPullOrderDetailInfo"></param>
        public static void GetPlanPullOrderDetailInfo(PlanPullOrderInfo planPullOrderInfo, ref PlanPullOrderDetailInfo planPullOrderDetailInfo)
        {
            if (planPullOrderInfo == null) return;
            ///ORDER_FID,拉动单外键
            planPullOrderDetailInfo.OrderFid = planPullOrderInfo.Fid;
            ///ORDER_STATUS,拉动单状态
            planPullOrderDetailInfo.OrderStatus = planPullOrderInfo.OrderStatus;
            ///ORDER_CODE,拉动单号
            planPullOrderDetailInfo.OrderCode = planPullOrderInfo.OrderCode;
        }
        #endregion


        #region 紧急拉动
        /// <summary>
        /// CreatePlanPullOrderDetail
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static PlanPullOrderDetailInfo CreatePlanPullOrderDetail(string loginUser)
        {
            PlanPullOrderDetailInfo info = new PlanPullOrderDetailInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///VALID_FLAG,逻辑删除标记
            info.ValidFlag = true;
            ///CREATE_DATE,创建时间
            info.CreateDate = DateTime.Now;
            ///CREATE_USER,创建用户
            info.CreateUser = loginUser;
            ///PART_VERSION,物料版本
            ///info.PartVersion = null;
            return info;
        }

        /// <summary>
        /// MaintainInhouseLogisticStandardInfo-->PlanPullOrderDetailInfo
        /// </summary>
        /// <param name="logisticStandardInfo"></param>
        /// <param name="info"></param>
        public static void GetPlanPullOrderDetail(MaintainInhouseLogisticStandardInfo logisticStandardInfo, ref PlanPullOrderDetailInfo info)
        {
            if (logisticStandardInfo == null) return;
            ///SupplierNum
            info.SupplierNum = logisticStandardInfo.SupplierNum;
            ///PartNo
            info.PartNo = logisticStandardInfo.PartNo;
            ///PartCname
            info.PartCname = logisticStandardInfo.PartCname;
            ///PartEname
            info.PartEname = logisticStandardInfo.PartEname;
            ///MeasuringUnitNo
            //info.MeasuringUnitNo = logisticStandardInfo.PartUnits;
            ///InboundPackageModel
            info.InboundPackageModel = logisticStandardInfo.InboundPackageModel;
            ///InboundPackageQty
            info.InboundPackageQty= logisticStandardInfo.InboundPackage.GetValueOrDefault();
        }
        #endregion

    }
}


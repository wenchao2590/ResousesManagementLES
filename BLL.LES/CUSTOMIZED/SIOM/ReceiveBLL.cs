using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public partial class ReceiveBLL
    {
        /// <summary>
        /// 入库数据导入
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<ReceiveDetailInfo> receiveDetailInfos = CommonDAL.DatatableConvertToList<ReceiveDetailInfo>(dataTable).ToList();
            if (receiveDetailInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsDAL().GetListForInterfaceDataSync(receiveDetailInfos.Select(d => d.PartNo).ToList());
            ///SUPPLIER_NUM.60、BOOK_KEEPER、RECEIVE_TYPE.70、TRAN_TIME.80、ORGANIZATION_FID.90、WM_NO.30、COST_CENTER.100、CONTRACT_NO.110合并
            var groupReceives = receiveDetailInfos
                    .GroupBy(b => new { b.SupplierNum, b.BookKeeper, b.ReceiveType, b.TranTime, b.OrganizationFid, b.WmNo, b.CostCenter, b.ContractNo })
                    .Select(p => new { p.Key }).ToList();
            string sql = string.Empty;
            foreach (var groupReceive in groupReceives)
            {
                string receiveNo = new SeqDefineDAL().GetCurrentCode("RECEIVE_NO");
                Guid receiveFid = Guid.NewGuid();
                List<ReceiveDetailInfo> receiveDetailList = receiveDetailInfos.Where(d =>
                d.SupplierNum == groupReceive.Key.SupplierNum
                && d.BookKeeper == groupReceive.Key.BookKeeper
                && d.ReceiveType == groupReceive.Key.ReceiveType
                && d.TranTime == groupReceive.Key.TranTime
                && d.OrganizationFid == groupReceive.Key.OrganizationFid
                && d.WmNo == groupReceive.Key.WmNo
                && d.CostCenter == groupReceive.Key.CostCenter
                && d.ContractNo == groupReceive.Key.ContractNo).ToList();
                int rowNo = 1;
                foreach (var receiveDetail in receiveDetailList)
                {
                    ///PART_NO.10、PART_ENAME.60、ZONE_NO.40、DLOC.50、ACTUAL_QTY.20
                    MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == receiveDetail.PartNo);
                    if (maintainPartsInfo == null)
                    {
                        if (string.IsNullOrEmpty(receiveDetail.PartNo))
                            throw new Exception("MC:0x00000227");///器材编号不能为空

                        sql += "insert into [LES].[TM_BAS_MAINTAIN_PARTS] "
                            + "(FID, PART_NO, PART_CNAME, PART_ENAME, PART_UNITS, PART_CLS, PART_GROUP, PURCHASE_UNIT_PRICE, VALID_FLAG, CREATE_USER, CREATE_DATE) values "
                            + "(NEWID(), "
                            + "N'" + receiveDetail.PartNo + "', "
                            + "N'" + receiveDetail.PartCname + "', "
                            + "N'" + receiveDetail.PartEname + "', "
                            + "N'" + receiveDetail.PartUnits + "', "
                            + "N'" + receiveDetail.PartCls + "', "
                            + "N'" + receiveDetail.PartGroup + "', "
                            + "" + receiveDetail.PurchaseUnitPrice.GetValueOrDefault() + ", "
                            + "1, N'" + loginUser + "', GETDATE());";
                    }
                    else
                    {
                        ///物料描述
                        receiveDetail.PartCname = maintainPartsInfo.PartCname;
                        ///规格型号
                        receiveDetail.PartCls = maintainPartsInfo.PartCls;
                        ///产地
                       // receiveDetail.OriginPlace = maintainPartsInfo.OriginPlace;
                        ///计量单位
                        receiveDetail.MeasuringUnitNo = maintainPartsInfo.PartUnits;
                        ///物料采购单价
                        //receiveDetail.PurchaseUnitPrice = maintainPartsInfo.PurchaseUnitPrice;
                    }

                    receiveDetail.PartPrice = receiveDetail.PurchaseUnitPrice.GetValueOrDefault() * receiveDetail.ActualQty.GetValueOrDefault();
                    sql += "insert into [LES].[TT_WMM_RECEIVE_DETAIL] "
                        + "(FID, RECEIVE_FID, SUPPLIER_NUM, TARGET_WM, TARGET_ZONE, TARGET_DLOC, PART_NO, PART_CNAME, PART_ENAME, MEASURING_UNIT_NO, ACTUAL_QTY, TRAN_NO, RUNSHEET_NO, ROW_NO, ORIGIN_PLACE, PURCHASE_UNIT_PRICE, PART_PRICE, PART_CLS, VALID_FLAG, CREATE_USER, CREATE_DATE) values "
                        + "(NEWID(), "
                        + "N'" + receiveFid + "', "
                        + "N'" + groupReceive.Key.SupplierNum + "', "
                        + "N'" + receiveDetail.WmNo + "', "
                        + "N'" + receiveDetail.ZoneNo + "', "
                        + "N'" + receiveDetail.Dloc + "', "
                        + "N'" + receiveDetail.PartNo + "', "
                        + "N'" + receiveDetail.PartCname + "', "
                        + "N'" + receiveDetail.PartEname + "', "
                        + "N'" + receiveDetail.PartUnits + "', "
                        + "" + receiveDetail.ActualQty.GetValueOrDefault() + ", "
                        + "N'" + receiveNo + "', "
                        + "N'" + groupReceive.Key.ContractNo + "', "
                        + "" + rowNo++ + ", "
                        + "N'" + receiveDetail.OriginPlace + "', "
                        + "" + receiveDetail.PurchaseUnitPrice.GetValueOrDefault() + ", "
                        + "" + receiveDetail.PartPrice.GetValueOrDefault() + ", "
                        + "N'" + receiveDetail.PartCls + "', "
                        + "1, N'" + loginUser + "', GETDATE());";
                }
                ///物料价格合计
                decimal sumOfPrice = receiveDetailList.Sum(d => d.PartPrice.GetValueOrDefault());
                ///物料合计数量
                decimal sumPartQty = receiveDetailList.Sum(d => d.ActualQty.GetValueOrDefault());

                if (!DateTime.TryParse(groupReceive.Key.TranTime.ToString("yyyy-MM-dd HH:mm:ss"), out DateTime tranTime))
                    throw new Exception("MC:0x00000328");///入库时间不能为空

                sql += "insert into [LES].[TT_WMM_RECEIVE] "
                    + "(FID, RECEIVE_NO, SUPPLIER_NUM, WM_NO, RECEIVE_TYPE, TRAN_TIME, BOOK_KEEPER, STATUS, RUNSHEET_NO, ORGANIZATION_FID, COST_CENTER, SUM_PART_QTY, SUM_OF_PRICE, VALID_FLAG, CREATE_USER, CREATE_DATE) values "
                    + "(N'" + receiveFid + "', "
                    + "N'" + receiveNo + "', "
                    + "N'" + groupReceive.Key.SupplierNum + "', "
                    + "N'" + groupReceive.Key.WmNo + "', "
                    + "" + groupReceive.Key.ReceiveType + ", "
                    + "N'" + tranTime.ToString("yyyy-MM-dd HH:mm:ss") + "', "
                    + "N'" + groupReceive.Key.BookKeeper + "', "
                    + "" + (int)WmmOrderStatusConstants.Created + ", "
                    + "N'" + groupReceive.Key.ContractNo + "', "
                    + "N'" + groupReceive.Key.OrganizationFid + "', "
                    + "N'" + groupReceive.Key.CostCenter + "', "
                    + "" + sumPartQty + ", "
                    + "" + sumOfPrice + ", "
                    + "1, N'" + loginUser + "', GETDATE());";
            }
            if (string.IsNullOrEmpty(sql)) return false;
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }

        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select  r.*,o.[NAME] as DEPT,s.[SUPPLIER_NAME],c.[ITEM_NAME] as RECEIVE_TYPE_NAME,w.[WAREHOUSE_NAME],c2.[ITEM_NAME] as COST_CENTER_NAME "
                + "from LES.TT_WMM_RECEIVE r with(nolock) "
                + "left join TS_SYS_ORGANIZATION o with(nolock) on o.[FID] = r.[ORGANIZATION_FID] and o.[VALID_FLAG] = 1 "
                + "left join LES.TM_BAS_SUPPLIER s with(nolock) on s.[SUPPLIER_NUM] = r.[SUPPLIER_NUM] and s.[VALID_FLAG] = 1 "
                + "left join TS_SYS_CODE_ITEM c with(nolock) on c.[ITEM_VALUE] = r.[RECEIVE_TYPE] and c.[CODE_FID] = N'67FA6B26-5C2D-4C26-B882-DFCB2B07FB6B' and c.[VALID_FLAG] = 1 "
                + "left join TS_SYS_CODE_ITEM c2 with(nolock) on c2.[ITEM_NAME_EN] = r.[COST_CENTER] and c2.[CODE_FID] = N'06FD3011-A967-40D2-8337-A80B78D9C137' and c2.[VALID_FLAG] = 1 "
                + "left join LES.TM_BAS_WAREHOUSE w with(nolock) on w.[WAREHOUSE] = r.[WM_NO] and w.[VALID_FLAG] = 1 "
                + "where r.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and r.[VALID_FLAG] = 1;"
                + "select * from LES.TT_WMM_RECEIVE_DETAIL with(nolock) "
                + "where [RECEIVE_FID] in (select [FID] from LES.TT_WMM_RECEIVE with(nolock) "
                + "where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")  and [VALID_FLAG] = 1 ) and [VALID_FLAG] = 1;";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }

        /// <summary>
        /// 提交(发布)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SubmitInfo(long id, string loginUser)
        {
            ///一般手工创建入库单会用到此功能，入库单必须为10.已创建状态⑫
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Created + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000683");///状态必须为已创建

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Published + ",[CONFIRM_USER] = N'" + loginUser + "' ,[CONFIRM_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 撤销提交
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoSubmitInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Published + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000147");///已提交的入库单才能撤销提交

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Created + ",[CONFIRM_USER] = NULL ,[CONFIRM_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 确认(完成)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ConfirmInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Published + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000148");///已提交的入库单才能进行确认操作

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Completed + ",[LIABLE_USER] = N'" + loginUser + "' ,[LIABLE_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            string sql = GetTranDetailsInsertSql(id, (int)WmmTranTypeConstants.Inbound, loginUser);
            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 撤销确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoConfirmInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Completed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000149");///已确认的入库单才能撤销确认

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Published + ",[LIABLE_USER] = NULL ,[LIABLE_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            string sql = GetTranDetailsInsertSql(id, (int)WmmTranTypeConstants.UndoInbound, loginUser);
            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (!string.IsNullOrEmpty(sql))
                    DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 审核(关闭)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool VerifyInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Completed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000150");///已确认的入库单才能进行审核操作

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Closed + ",[FINANCE_USER] = N'" + loginUser + "' ,[FINANCE_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UndoVerifyInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)WmmOrderStatusConstants.Closed + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000151");///已审核的入库单才能撤销审核

            string fields = "[STATUS] = " + (int)WmmOrderStatusConstants.Completed + ",[FINANCE_USER] = NULL ,[FINANCE_DATE] = NULL,[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE()";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
    }
}

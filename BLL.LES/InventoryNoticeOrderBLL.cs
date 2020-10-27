using BLL.SYS;
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
    public class InventoryNoticeOrderBLL
    {
        #region Common
        InventoryNoticeOrderDAL dal = new InventoryNoticeOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<InventoryNoticeOrderInfo></returns>
        public List<InventoryNoticeOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public InventoryNoticeOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(InventoryNoticeOrderInfo info)
        {
            info.OrderCode = new SeqDefineDAL().GetCurrentCode("ORDER_CODE");
            info.SapZldat = DateTime.Now;
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            InventoryNoticeOrderInfo info = dal.GetInfo(id);
            if (info.Status != (int)InventoryOrderStatusConstants.CREATED)
                throw new Exception("MC:0x00000683");///状态必须为已创建
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            InventoryNoticeOrderInfo info = dal.GetInfo(id);
            if (info.Status != (int)InventoryOrderStatusConstants.CREATED)
                throw new Exception("MC:0x00000683");///状态必须为已创建

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StartInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ") and [STATUS] = " + (int)InventoryOrderStatusConstants.CREATED + "", string.Empty).Count == 0)
                throw new Exception("MC:0x00000369");///状态为已创建的盘点单才允许进行发布
            List<InventoryNoticeOrderInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<PartsStockInfo> partsStocks = new PartsStockDAL().GetList("[WM_NO] in ('" + string.Join("','", info.Select(d => d.WmNo).ToArray()) + "') and [ZONE_NO] in ('" + string.Join("','", info.Select(d => d.ZoneNo).ToArray()) + "') and [KEEPER] in ('" + string.Join("','", info.Select(d => d.Keeper).ToArray()) + "')", string.Empty);
            if (partsStocks.Count == 0)
                throw new Exception("MC:0x00000370");///没有相关的物料仓储信息
            List<StocksInfo> stocks = new StocksDAL().GetList("[WM_NO] in ('" + string.Join("','", partsStocks.Select(d => d.WmNo).ToArray()) + "') and [ZONE_NO] in ('" + string.Join("','", partsStocks.Select(d => d.ZoneNo).ToArray()) + "') and [PACKAGE_MODEL] in ('" + string.Join("','", partsStocks.Select(d => d.InboundPackageModel).ToArray()) + "') and [PART_NO] in ('" + string.Join("','", partsStocks.Select(d => d.PartNo).ToArray()) + "') and [SUPPLIER_NUM] in ('" + string.Join("','", partsStocks.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            string check = new ConfigBLL().GetValueByCode("NO_STOCK_MATERIAL_NOT_RELEASE_TO_INVENTORY_ORDER");
            string stock = new ConfigBLL().GetValueByCode("MATERIALS_USED_IN_AVAILABLE_STOCK");
            string sql = string.Empty;
            foreach (var item in info)
            {
                List<PartsStockInfo> partsStockInfos = partsStocks.Where(d => d.Keeper == item.Keeper && d.WmNo == item.WmNo && d.ZoneNo == item.ZoneNo).ToList();
                if (partsStockInfos.Count == 0)
                    throw new Exception("MC:0x00000370");///没有相关的物料仓储信息
                foreach (var items in partsStockInfos)
                {
                    List<StocksInfo> stocksInfos = stocks.Where(d => d.Keeper == items.Keeper && d.WmNo == items.WmNo && d.ZoneNo == item.ZoneNo && d.PartNo == items.PartNo).ToList();
                    string Package = items.Package == null ? "null" : items.Package.ToString();
                    if (check == "true")
                    {
                        int sum = 0;
                        if (stock == "true")
                            sum = (int)stocksInfos.Select(d => d.AvailbleStocks).Sum();
                        else
                            sum = (int)stocksInfos.Select(d => d.StocksNum).Sum();

                        if (sum > 0)
                        {
                            sql += "insert into [LES].[TT_WMM_INVENTORY_ORDER_PART] "
                                + "([FID],[ORDER_CODE],[ORDER_FID],[WM_NO],[ZONE_NO],[DLOC],[PART_NO],[SUPPLIER_NUM],[PACKAGE_MODEL],[PACKAGE],[PART_CNAME],[PACKAGE_QTY],[REFERENCE_QTY],[VALID_FLAG],[CREATE_DATE],[CREATE_USER])"
                                + "values("
                                + "newid(),"
                                + "N'" + item.OrderCode + "',"
                                + "N'" + item.Fid + "',"
                                + "N'" + items.WmNo + "',"
                                + "N'" + items.ZoneNo + "',"
                                + "N'" + items.Dloc + "',"
                                + "N'" + items.PartNo + "',"
                                + "N'" + items.SupplierNum + "',"
                                + "N'" + items.InboundPackageModel + "',"
                                + Package + ","
                                + "N'" + items.PartCname + "',"
                                + "N'" + stocksInfos.Select(d => d.Stocks).Sum() + "',"
                                + "N'" + sum + "',"
                                + "1,"
                                + "GETDATE(),"
                                + "N'" + loginUser + "'"
                                + ")\n";
                        }
                    }
                    else
                    {
                        int sum = 0;
                        if (stock == "true")
                            sum = (int)stocksInfos.Select(d => d.AvailbleStocks).Sum();
                        else
                            sum = (int)stocksInfos.Select(d => d.StocksNum).Sum();
                        sql += "insert into [LES].[TT_WMM_INVENTORY_ORDER_PART] "
                                + "([FID],[ORDER_CODE],[ORDER_FID],[WM_NO],[ZONE_NO],[DLOC],[PART_NO],[SUPPLIER_NUM],[PACKAGE_MODEL],[PACKAGE],[PART_CNAME],[PACKAGE_QTY],[REFERENCE_QTY],[VALID_FLAG],[CREATE_DATE],[CREATE_USER])"
                                + "values("
                                + "newid(),"
                                + "N'" + item.OrderCode + "',"
                                + "N'" + item.Fid + "',"
                                + "N'" + items.WmNo + "',"
                                + "N'" + items.ZoneNo + "',"
                                + "N'" + items.Dloc + "',"
                                + "N'" + items.PartNo + "',"
                                + "N'" + items.SupplierNum + "',"
                                + "N'" + items.PackageModel + "',"
                                + Package + ","
                                + "N'" + items.PartCname + "',"
                                + "N'" + stocksInfos.Select(d => d.Stocks).Sum() + "',"
                                + "N'" + sum + "',"
                                + "1,"
                                + "GETDATE(),"
                                + "N'" + loginUser + "'"
                                + ")\n";
                    }
                }
            }


            using (TransactionScope trans = new TransactionScope())
            {
                if (string.IsNullOrEmpty(sql))
                    throw new Exception("MC:0x00000440");///没有相关物料明细
                sql += "update [LES].[TT_WMM_INVENTORY_NOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
                CommonDAL.ExecuteNonQueryBySql(sql);

                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InventoryendInfos(List<string> rowsKeyValues, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + " and [ID] IN (" + string.Join(",", rowsKeyValues) + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000375");///已发布状态才能进行确认操作

            string sql = "update [LES].[TT_WMM_INVENTORY_NOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
            using (TransactionScope trans = new TransactionScope())
            {
                DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
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
        public bool RepeatcheckInfos(List<string> rowsKeyValues, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + " and [ID] IN (" + string.Join(",", rowsKeyValues) + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000374");///已确认状态才能进行撤销操作

            string sql = "update [LES].[TT_WMM_INVENTORY_NOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
            using (TransactionScope trans = new TransactionScope())
            {
                DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool GenerationdifferenceInfos(List<string> rowsKeyValues, string loginUser)
        {

            List<InventoryNoticeOrderInfo> inventoryNoticeOrderInfos = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ") and [STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + "", string.Empty);
            if (inventoryNoticeOrderInfos.Count == 0)
                throw new Exception("MC:0x00000376");///已确认状态才能进行完成操作

            StringBuilder stringBuilder = new StringBuilder();
            string check = new ConfigBLL().GetValueByCode("ENABLE_SAP_INVENTORY_REPORT_FLAG");

            if (check == "true")
            {

                List<InventoryNoticeOrderInfo> infos = new List<InventoryNoticeOrderInfo>();

                List<InventoryOrderPartInfo> orderPartInfos = new InventoryOrderPartDAL().GetList("[ORDER_FID] IN ('" + string.Join("','", inventoryNoticeOrderInfos.Select(d => d.Fid).ToArray()) + "')", string.Empty);

                List<InventoryPartBarcodeInfo> inventoryPartBarcodes = new InventoryPartBarcodeDAL().GetList("[PART_FID] IN ('" + string.Join("','", orderPartInfos.Select(d => d.Fid).ToArray()) + "')", string.Empty);
                Dictionary<string, Guid> keyValues = new Dictionary<string, Guid>();

                List<MaintainPartsInfo> maintainParts = new MaintainPartsDAL().GetList("[PART_NO] IN ('" + string.Join("','", orderPartInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);

                foreach (var item in inventoryNoticeOrderInfos)
                {
                    ///生成SAP盘点报告中间表数据
                    Guid guid = Guid.NewGuid();
                    if (keyValues.Keys.Contains<string>(item.SapIblnr) == false)
                        keyValues.Add(item.SapIblnr, guid);

                    List<InventoryOrderPartInfo> partInfos = orderPartInfos.Where(d => d.OrderFid == item.Fid).ToList();
                    foreach (var items in partInfos)
                    {
                        string PartName = maintainParts.FirstOrDefault(d => d.PartNo == items.PartNo).PartCname;
                        stringBuilder.Append("insert into [LES].[TI_IFM_SAP_INVENTORY_CHECK_REPORT] ([FID],[LOG_FID],[IBLNR],[LGORT],[REMARKS],[MATNR],[MENGE],[ZLDAT],[AQTY],[DQTY],[PROCESS_FLAG],[PROCESS_TIME],[VALID_FLAG],[CREATE_USER],[CREATE_DATE])values(");
                        string partQty = string.Empty;
                        if (items.PartQty == null)
                            partQty = "NULL";
                        else
                            partQty = ((int)items.PartQty).ToString();
                        string sapDqty = string.Empty;
                        if (items.SapDqty == null)
                            sapDqty = "NULL";
                        else
                            sapDqty = ((int)items.SapDqty).ToString();

                        stringBuilder.Append("newid(),N'" + keyValues[item.SapIblnr] + "',N'" + item.SapIblnr + "',N'" + item.SapLgort + "',N'" + item.Comments + "',N'" + items.PartNo + "'," + partQty + ",N'" + item.SapZldat + "',N'" + items.SapMenge + "'," + sapDqty + ",N'" + item.Status + "',N'" + item.ModifyDate + "',1,N'" + loginUser + "',GETDATE());\n\n");

                        InventoryPartBarcodeInfo barcodeInfo = inventoryPartBarcodes.Where(d => d.PartFid == items.Fid).FirstOrDefault();
                        if (barcodeInfo != null)
                        {
                            if (barcodeInfo.PartQty != items.DifferenceQty)
                            {
                                stringBuilder.Append("update [LES].[TT_WMM_INVENTORY_PART_BARCODE] set [PART_QTY] = N'" + items.DifferenceQty + "',[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] = " + barcodeInfo.Id + ";\n\n");
                            }
                        }
                    }

                    ///新数据排除盘点计划号一致的
                    InventoryNoticeOrderInfo InventoryNoticeOrderInfos = infos.Where(d => d.SapIblnr == item.SapIblnr).FirstOrDefault();
                    if (InventoryNoticeOrderInfos == null)
                        infos.Add(item);
                }
                List<InventoryNoticeOrderInfo> inventoryNotices = dal.GetList(" [ID] not in  (" + string.Join(",", rowsKeyValues) + ") and [STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + " and [SAP_IBLNR] in ('" + string.Join("','", inventoryNoticeOrderInfos.Select(d => d.SapIblnr).ToArray()) + "')", string.Empty);
                foreach (var item in infos)
                {

                    List<InventoryNoticeOrderInfo> inventoryNoticeOrders = inventoryNotices.Where(d => d.SapIblnr == item.SapIblnr).ToList();
                    List<InventoryNoticeOrderInfo> inventories = inventoryNotices.Where(d => d.SapIblnr == item.SapIblnr && d.Status == item.Status).ToList();
                    if (inventoryNoticeOrders.Count == inventories.Count)
                    {
                        //生成盘点报告发送任务
                        string targetSystem = "SAP";
                        string methodCode = "LES-WMM-071";
                        string keyValue = item.SapIblnr;
                        stringBuilder.AppendFormat(BLL.LES.CommonBLL.GetCreateOutboundLogSql(targetSystem, keyValues[item.SapIblnr], methodCode, keyValue, loginUser));
                    }
                }
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(stringBuilder.ToString()))
                {
                    stringBuilder.Append("\n\n update [LES].[TT_WMM_INVENTORY_NOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.COMPLETED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ");
                    CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                }
                trans.Complete();
            }

            return true;
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            int cnt = dal.GetCounts("[STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + " and [ID] IN (" + string.Join(",", rowsKeyValues) + ")");
            if (cnt == 0)
                throw new Exception("MC:0x00000379");///已确认状态才能进行撤销操作

            string sql = "update [LES].[TT_WMM_INVENTORY_NOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.ABANDONED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
            using (TransactionScope trans = new TransactionScope())
            {
                DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }

    }
}


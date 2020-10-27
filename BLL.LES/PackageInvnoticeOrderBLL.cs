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
    public class PackageInvnoticeOrderBLL
    {
        #region Common
        PackageInvnoticeOrderDAL dal = new PackageInvnoticeOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageInvnoticeOrderInfo></returns>
        public List<PackageInvnoticeOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageInvnoticeOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(PackageInvnoticeOrderInfo info)
        {
            info.OrderCode = new SeqDefineDAL().GetCurrentCode("ORDER_CODE");
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            PackageInvnoticeOrderInfo info = dal.GetInfo(id);
            if (info.Status != (int)InventoryOrderStatusConstants.CREATED)
                throw new Exception("MC:0x00000683");///状态必须为已创建
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            PackageInvnoticeOrderInfo info = dal.GetInfo(id);
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
            List<PackageInvnoticeOrderInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
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
                            sql += "insert into [LES].[TT_PCM_INVENTORY_ORDER_PART] "
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
                        sql += "insert into [LES].[TT_PCM_INVENTORY_ORDER_PART] "
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
                sql += "update [LES].[TT_PCM_PACKAGE_INVNOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
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

            string sql = "update [LES].[TT_PCM_PACKAGE_INVNOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
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

            string sql = "update [LES].[TT_PCM_PACKAGE_INVNOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.PUBLISHED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
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
        public bool completeInfos(List<string> rowsKeyValues, string loginUser)
        {

            List<PackageInvnoticeOrderInfo> inventoryNoticeOrderInfos = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ") and [STATUS] = " + (int)InventoryOrderStatusConstants.CONFIRMED + "", string.Empty);
            if (inventoryNoticeOrderInfos.Count == 0)
                throw new Exception("MC:0x00000376");///已确认状态才能进行完成操作

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\n\n update [LES].[TT_PCM_PACKAGE_INVNOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.COMPLETED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ");
            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
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

            string sql = "update [LES].[TT_PCM_PACKAGE_INVNOTICE_ORDER] set [STATUS] = " + (int)InventoryOrderStatusConstants.ABANDONED + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
            using (TransactionScope trans = new TransactionScope())
            {
                DAL.SYS.CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }

    }
}


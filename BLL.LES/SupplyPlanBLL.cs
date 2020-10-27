using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SupplyPlanBLL
    {
        #region Common
        SupplyPlanDAL dal = new SupplyPlanDAL();
        public List<SupplyPlanInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public SupplyPlanInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(SupplyPlanInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(long id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 行数
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        public List<SupplyPlanInfo> GetList(string textwhere, string ordertext)
        {
            return dal.GetList(textwhere, ordertext);
        }

        /// <summary>
        /// 分页获取供货计划的DATATABLE
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DataTable GetSupplyPlanListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, List<string> dateColumns, out int dataCount)
        {
            dataCount = 0;
            if (dateColumns.Count == 0)
                throw new Exception("0x00000043");///无效的日期参数

            ///将供货计划中需求数量为零的数据过滤掉
            string qtyCondition = string.Empty;
            List<string> dColumns = new List<string>();
            foreach (var dateColumn in dateColumns)
            {
                qtyCondition += "or [" + dateColumn + "] > 0 ";
                ///日期列之前增加字母D
                dColumns.Add("Convert(int,isnull([" + dateColumn + "] , 0)) as D" + dateColumn + "");
            }
            textWhere += "and (" + qtyCondition.Substring(3) + ") ";

            string columns = "[ID] as Id," +
                "[FID] as Fid," +
                "[PART_NO] as PartNo," +
                "[PART_CNAME] as PartCname," +
                "[SUPPLIER_NUM] as SupplierNum," +
                "[SUPPLIER_NAME] as SupplierName," +
                "[PART_PURCHASER] as PartPurchaser," +
                "[PLANT] as Plant," +
                "" + string.Join(",", dColumns) + " ";

            dataCount = dal.GetCounts(textWhere);
            return dal.GetSupplyPlanDataTableByPage(textWhere, textOrder, pageIndex, pageRow, columns);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DataTable GetLackOfInspectionListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, List<string> dateColumns, out int dataCount)
        {
            DataTable supplyPlanData = GetSupplyPlanListByPage(textWhere, textOrder, pageIndex, pageRow, dateColumns, out dataCount);
            ///增加RDC数量和VMI数量
            foreach (var item in dateColumns)
            {

            }
            supplyPlanData.Columns.Add("RDC", typeof(decimal));
            supplyPlanData.Columns.Add("VMI", typeof(decimal));
            ///获取所有零件号
            List<string> partNos = new List<string>();
            foreach (DataRow dr in supplyPlanData.Rows)
            {
                string partNo = dr["PartNo"].ToString();
                if (partNos.Contains(partNo)) continue;
                partNos.Add(partNo);
            }
            List<StocksInfo> stocksInfos = new StocksDAL().GetList("[PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "')", string.Empty);
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("[WAREHOUSE_TYPE] in (" + (int)WarehouseTypeConstants.RDC + "," + (int)WarehouseTypeConstants.VMI + ")", string.Empty);
            foreach (DataRow dr in supplyPlanData.Rows)
            {
                string partNo = dr["PartNo"].ToString();
                string supplierNum = dr["SupplierNum"].ToString();
                string plant = dr["Plant"].ToString();
                ///
                decimal rdcQty = stocksInfos.Where(d => warehouseInfos.Where(w => w.WarehouseType.GetValueOrDefault() == (int)WarehouseTypeConstants.RDC).Select(w => w.Warehouse).Contains(d.WmNo) &&
                d.PartNo == partNo &&
                d.SupplierNum == supplierNum &&
                d.Plant == plant).Sum(d => d.AvailbleStocks.GetValueOrDefault());
                ///
                decimal vmiQty = stocksInfos.Where(d => warehouseInfos.Where(w => w.WarehouseType.GetValueOrDefault() == (int)WarehouseTypeConstants.VMI).Select(w => w.Warehouse).Contains(d.WmNo) &&
                d.PartNo == partNo &&
                d.SupplierNum == supplierNum &&
                d.Plant == plant).Sum(d => d.AvailbleStocks.GetValueOrDefault());
                dr["RDC"] = rdcQty;
                dr["VMI"] = vmiQty;
            }
            return supplyPlanData;
        }
        /// <summary>
        /// 获取数据库中有效的日期列
        /// </summary>
        /// <param name="dateColumns"></param>
        /// <returns></returns>
        public List<string> GetDatabaseExistsDateColumns(List<string> dateColumns)
        {
            return dal.GetDatabaseExistsDateColumns(dateColumns);
        }
        /// <summary>
        /// 获取数据库中有效的日期
        /// </summary>
        /// <param name="dateColumns"></param>
        /// <returns></returns>
        public string GetDatabaseExistsDateColumn(string dateColumn)
        {
            return dal.GetDatabaseExistsDateColumn(dateColumn);
        }

        public DataTable GetListBydateColumns(List<string> dateColumns)
        {
            return dal.GetListBySql(dateColumns);
        }


    }
}


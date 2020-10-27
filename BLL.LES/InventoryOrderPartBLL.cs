using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.LES
{
    public class InventoryOrderPartBLL
    {
        #region Common
        InventoryOrderPartDAL dal = new InventoryOrderPartDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<InventoryOrderPartInfo></returns>
        public List<InventoryOrderPartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public InventoryOrderPartInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(InventoryOrderPartInfo info)
        {
            throw new Exception("MC:0x00000371");///不允许手动添加
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            InventoryNoticeOrderInfo inventoryNoticeOrderInfo = new InventoryNoticeOrderDAL().GetList("[FID] = N'" + CommonBLL.GetFieldValue(fields, "ORDER_FID") + "'", string.Empty).FirstOrDefault();
            if (inventoryNoticeOrderInfo.Status != (int)InventoryOrderStatusConstants.PUBLISHED)
                throw new Exception("MC:0x00000372");///状态为20.已发布的盘点单只允许修改物料的物料盘点数量以及备注，此时的修改同时计算出两个差异值
            string partQty = CommonBLL.GetFieldValue(fields, "PART_QTY");
            bool regular = Regex.IsMatch(partQty, @"^(^-?|^\+?|\d)\d+$");
            if (regular == false)
            {
                regular = Regex.IsMatch(partQty, @"^(^-?|^\+?|^\d?)\d*\.\d+$");
                if (regular == false)
                    throw new Exception("MC:0x00000381");///物料盘点数量的数据格式不正确，请输入数字格式

            }
            InventoryOrderPartInfo inventory = dal.GetInfo(id);
            if (inventory == null)
                throw new Exception("MC:0x00000084");///数据错误
            int differenceQty = Convert.ToInt32(partQty) - Convert.ToInt32(inventory.ReferenceQty);
            int sapDqty = Convert.ToInt32(partQty) - Convert.ToInt32(inventory.SapMenge);
            fields += ",[DIFFERENCE_QTY] = N'" + differenceQty + "'";
            fields += ",[SAP_DQTY] = N'" + sapDqty + "'";
            return dal.UpdateInfo(fields, id) > 0 ? true : false;

        }

        #endregion
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser, string where)
        {
            List<InventoryOrderPartInfo> inventoryOrderPartInfos = CommonDAL.DatatableConvertToList<InventoryOrderPartInfo>(dataTable).ToList();
            if (inventoryOrderPartInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范
            StringBuilder stringBuilder = new StringBuilder();
            List<InventoryOrderPartInfo> orderPartInfos = dal.GetList(where, string.Empty);
            foreach (var item in inventoryOrderPartInfos)
            {
                InventoryOrderPartInfo inventory = orderPartInfos.Where(d => d.Dloc == item.Dloc && d.PartNo == item.PartNo && d.SupplierNum == item.SupplierNum && d.PackageModel == item.PackageModel && d.PackageQty == item.PackageQty).FirstOrDefault();
                if (inventory == null)
                    throw new Exception("MC:0x00000255");///数据格式不符合导入规范

                if (item.PartQty == null)
                    throw new Exception("MC:0x00000380");///物料盘点数量为必填项

                bool regular = Regex.IsMatch(item.PartQty.ToString(), @"^(^-?|^\+?|\d)\d+$");
                if (regular == false)
                {
                    regular = Regex.IsMatch(item.PartQty.ToString(), @"^(^-?|^\+?|^\d?)\d*\.\d+$");
                    if (regular == false)
                        throw new Exception("MC:0x00000381");///物料盘点数量的数据格式不正确，请输入数字格式

                }
                inventory.DifferenceQty = item.PartQty - (inventory.ReferenceQty == null ? 0 : inventory.ReferenceQty);
                inventory.SapDqty = item.PartQty - (inventory.SapMenge == null ? 0 : inventory.SapMenge);
                stringBuilder.Append("update [LES].[TT_WMM_INVENTORY_ORDER_PART] set [DIFFERENCE_QTY] = N'" + inventory.DifferenceQty + "',[SAP_DQTY] = N'" + inventory.SapDqty + "',[PART_QTY] = N'" + item.PartQty + "',[COMMENTS] = N'" + item.Comments + "',[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [FID] = N'" + inventory.Fid + "';\n\n");
            }



            if (string.IsNullOrEmpty(stringBuilder.ToString()))
                throw new Exception("MC:0x00000283");///:没有可导入更新的数据

            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            string sql = string.Empty;
            foreach (var item in rowsKeyValues)
            {
                string[] values = item.Split('^');
                if (values.Length == 2)
                    sql += " update[LES].[TT_WMM_INVENTORY_ORDER_PART] WITH(ROWLOCK) set [COMMENTS] = N'" + values[1] + "',[MODIFY_USER] = N'" + loginUser + "' ,[MODIFY_DATE] = GETDATE() where [ID] = " + values[0] + ";";

            }
            return CommonDAL.ExecuteNonQueryBySql(sql); ;
        }
    }
}


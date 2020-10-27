
using BLL.LES;
using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SupplierBLL
    {
        #region Common
        SupplierDAL dal = new SupplierDAL();
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SupplierInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<SupplierInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplierInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 验证并新增
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SupplierInfo info)
        {
            ///供应商代码①、邓白氏码②、供应商名称③、供应商简称④单字段全表范围不允许重复
            int cnt = dal.GetCounts("[SUPPLIER_NUM] = N'" + info.SupplierNum + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000689"); ///供应商代码重复
            cnt = dal.GetCounts("[SUPPLIER_NAME] = N'" + info.SupplierName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000691");///供应商名称重复
            if (!string.IsNullOrEmpty(info.Duns))
            {
                cnt = dal.GetCounts("[DUNS] = N'" + info.Duns + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000690");///邓白氏码重复
            }
            if (!string.IsNullOrEmpty(info.SupplierSname))
            {
                cnt = dal.GetCounts("[SUPPLIER_SNAME] = N'" + info.SupplierSname + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000692");///供应商简称重复
            }
            ///未选中供应商类型则默认为物料供应商
            if (info.SupplierType.GetValueOrDefault() == 0)
                info.SupplierType = (int)SupplierTypeConstants.MaterialSupplier;
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 验证和修改数据
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            int cnt = 0;
            ///供应商简称②不允许重复
            string supplierSname = CommonBLL.GetFieldValue(fields, "SUPPLIER_SNAME");
            if (!string.IsNullOrEmpty(supplierSname))
            {
                cnt = dal.GetCounts("[SUPPLIER_SNAME] = N'" + supplierSname + "'and [ID] <>" + id + "");
                if (cnt > 0)
                    throw new Exception("MC:0x00000692");  ///供应商简称不能重复
            }
            ///邓白氏码②不能重复
            string duns = CommonBLL.GetFieldValue(fields, "DUNS");
            if (!string.IsNullOrEmpty(duns))
            {
                cnt = dal.GetCounts("[DUNS] = N'" + duns + "'and [ID] <> " + id + "");
                if (cnt > 0)
                    throw new Exception("MC:0x00000690");///邓白氏码不能重复
            }

            ///TODO:需要测试一下在配置了这个字段时，但是不填写的情况会是什么样的反馈
            string supplierType = CommonBLL.GetFieldValue(fields, "SUPPLIER_TYPE");
            ///未选中供应商类型则默认为物料供应商
            if (supplierType == "0" || string.IsNullOrEmpty(supplierType))
                fields = CommonBLL.SetFieldValue(fields, "SUPPLIER_TYPE", "10", false);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
        /// <summary>
        /// 根据供应商代码获取ASN标记
        /// </summary>
        /// <param name="SupplierNum"></param>
        /// <returns></returns>
        public bool GetAsnFlag(string SupplierNum)
        {
            return dal.GetAsnFlag(SupplierNum);
        }
        /// <summary>
        /// 获取业务表中要变更的数据集合
        /// </summary>
        /// <param name="supplierNums"></param>
        /// <returns></returns>
        public List<SupplierInfo> GetListForInterfaceDataSync(List<string> supplierNums)
        {
            if (supplierNums.Count == 0) return new List<SupplierInfo>();
            return dal.GetListForInterfaceDataSync(supplierNums);
        }
        /// <summary>
        /// 根据仓库信息同步供应商信息
        /// </summary>
        /// <param name="warehouseInfo"></param>
        public void SyncSupplierByWarehouse(WarehouseInfo warehouseInfo, string loginUser)
        {
            ///类型为内部仓库时不同步供应商信息
            if (warehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.VMI) return;
            StringBuilder @string = new StringBuilder();
            ///是否存在供应商信息
            SupplierInfo supplierInfo = dal.GetSupplierInfo(warehouseInfo.Warehouse);
            if (supplierInfo == null)
            {
                supplierInfo = new SupplierInfo();
                supplierInfo.Fid = Guid.NewGuid();
                supplierInfo.SupplierNum = warehouseInfo.Warehouse;
                supplierInfo.SupplierName = warehouseInfo.WarehouseName;
                supplierInfo.SupplierType = (int)SupplierTypeConstants.LogisticsSupplier;
                supplierInfo.ValidFlag = true;
                supplierInfo.CreateUser = loginUser;
                supplierInfo.CreateDate = DateTime.Now;
                @string.AppendLine(SupplierDAL.GetInsertSql(supplierInfo));
            }
            else
            {
                ///当供应商类型为储运供应商时才更新其信息
                if (supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.LogisticsSupplier)
                    @string.AppendLine("update [LES].[TM_BAS_SUPPLIER] " +
                    "set [SUPPLIER_NAME] = N'" + warehouseInfo.WarehouseName + "'," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + supplierInfo.Id + ";");
            }
            ///同步创建或更新存储区信息
            ///获取相同代码的存储区
            ZonesInfo zonesInfo = new ZonesDAL().GetZonesInfo(warehouseInfo.Warehouse, warehouseInfo.Warehouse);
            if (zonesInfo == null)
            {
                zonesInfo = new ZonesInfo();
                zonesInfo.ZoneNo = warehouseInfo.Warehouse;
                zonesInfo.ZoneName = warehouseInfo.WarehouseName;
                zonesInfo.WmNo = warehouseInfo.Warehouse;
                zonesInfo.Plant = warehouseInfo.Plant;
                zonesInfo.Settlementflag = false;
                zonesInfo.CreateUser = loginUser;
                zonesInfo.CreateDate = DateTime.Now;
                @string.AppendLine(ZonesDAL.GetInsertSql(zonesInfo));
            }
            else
            {
                @string.AppendLine("update [LES].[TM_WMM_ZONES] " +
                    "set [ZONE_NAME] = N'" + warehouseInfo.WarehouseName + "'," +
                    "[WM_NO] = N'" + warehouseInfo.Warehouse + "'," +
                    "[PLANT] = N'" + warehouseInfo.Plant + "'," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + zonesInfo.Id + ";");
            }
            CommonDAL.ExecuteNonQueryBySql(@string.ToString());
        }

        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<SupplierInfo> supplierExcelInfos = CommonDAL.DatatableConvertToList<SupplierInfo>(dataTable).ToList();
            if (supplierExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(supplierExcelInfos.Select(d => d.SupplierNum).ToList());
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var supplierExcelInfo in supplierExcelInfos)
            {
                ///当前业务数据表中此工厂的该物流路线时需要新增
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == supplierExcelInfo.SupplierNum);
                if (supplierInfo == null)
                {
                    if (string.IsNullOrEmpty(supplierExcelInfo.SupplierNum)
                        || string.IsNullOrEmpty(supplierExcelInfo.SupplierName)
                        || supplierExcelInfo.SupplierType.GetValueOrDefault() == 0)
                        throw new Exception("MC:0x00000221");///供应商代码、名称、类型为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<SupplierInfo>(supplierExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    sql += "if not exists (select * from LES.TM_BAS_SUPPLIER with(nolock) where [SUPPLIER_NUM] = N'" + supplierExcelInfo.SupplierNum + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_SUPPLIER] ("
                        + "[FID],"
                        + insertFieldString
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + insertValueString
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");";
                    continue;
                }

                if (string.IsNullOrEmpty(supplierExcelInfo.SupplierName)
                        || supplierExcelInfo.SupplierType.GetValueOrDefault() == 0)
                    throw new Exception("MC:0x00000221");///供应商代码、名称、类型为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<SupplierInfo>(supplierExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_SUPPLIER] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + supplierInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        public SupplierInfo GetSupplierInfo(string supplierNum)
        {
            return dal.GetSupplierInfo(supplierNum);
        }
    }
}


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
    public class WarehouseLocationBLL
    {
        #region Common
        WarehouseLocationDAL dal = new WarehouseLocationDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WarehouseLocationInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WarehouseLocationInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WarehouseLocationInfo info)
        {
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///仓库信息不存在

            ZonesInfo zonesInfo = new ZonesDAL().GetZonesInfo(info.ZoneNo, info.WmNo);
            if (zonesInfo == null)
                throw new Exception("MC:0x00000500");///存储区不存在

            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///允许库位代码在不同的存储区中重复使用
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///数据校验
            ValidInfo(info, allow_zoneno_repeat_at_different_warehouse, allowDlocRepeatAtDifferentZone);
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
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            WarehouseLocationInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            ///仓库
            info.WmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///仓库信息不存在

            ///存储区
            info.ZoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            ZonesInfo zonesInfo = new ZonesDAL().GetZonesInfo(info.ZoneNo, info.WmNo);
            if (zonesInfo == null)
                throw new Exception("MC:0x00000500");///存储区不存在

            ///库位名称
            info.StorageLocationName = CommonBLL.GetFieldValue(fields, "STORAGE_LOCATION_NAME");
            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///允许库位代码在不同的存储区中重复使用
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///数据校验
            ValidInfo(info, allow_zoneno_repeat_at_different_warehouse, allowDlocRepeatAtDifferentZone);
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WarehouseLocationInfo Collection </returns>
		public List<WarehouseLocationInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchdeleteInfos(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return false;
            List<long> ids = rowsKeyValues.Select(d => long.Parse(d)).ToList();
            return dal.BatchLogicDelete(ids, loginUser);
        }
        /// <summary>
        /// 验证数据合法性
        /// </summary>
        /// <param name="info"></param>
        private void ValidInfo(WarehouseLocationInfo info, string allow_zoneno_repeat_at_different_warehouse, string allowDlocRepeatAtDifferentZone)
        {
            if (string.IsNullOrEmpty(info.Dloc))
                throw new Exception("MC:0x00000530");///库位代码不允许为空

            if (string.IsNullOrEmpty(info.ZoneNo))
                throw new Exception("MC:0x00000527");///存储区代码不能为空

            if (string.IsNullOrEmpty(info.WmNo))
                throw new Exception("MC:0x00000528");///仓库代码不能为空

            string wmNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allow_zoneno_repeat_at_different_warehouse) && allow_zoneno_repeat_at_different_warehouse.ToLower() == "true")
                wmNoCondition = "and [WM_NO] = N'" + info.WmNo + "' ";
            string zoneNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allowDlocRepeatAtDifferentZone) && allowDlocRepeatAtDifferentZone.ToLower() == "true")
                zoneNoCondition = "and [ZONE_NO] = N'" + info.ZoneNo + "' ";
            ///库位代码①不允许重复，单字段进行全表校验
            int cnt = dal.GetCounts("[DLOC] = N'" + info.Dloc + "' " + zoneNoCondition + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000301");///库位代码重复

            ///库位名称②在同一存储区范围内不允许重复
            cnt = dal.GetCounts("[STORAGE_LOCATION_NAME] = N'" + info.StorageLocationName + "' " + zoneNoCondition + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000300");///库位名称重复
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<WarehouseLocationInfo> warehouseLocationExcelInfos = CommonDAL.DatatableConvertToList<WarehouseLocationInfo>(dataTable).ToList();
            if (warehouseLocationExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<WarehouseLocationInfo> warehouseLocationInfos = new WarehouseLocationDAL().GetList("" +
                "[DLOC] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.Dloc).ToArray()) + "')", string.Empty);
            ///存储区
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);
            ///仓库
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("" +
                "[WAREHOUSE] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///允许库位代码在不同的存储区中重复使用
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///执行的SQL语句
            StringBuilder @string = new StringBuilder();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var warehouseLocationExcelInfo in warehouseLocationExcelInfos)
            {
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == warehouseLocationExcelInfo.WmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///仓库信息不存在

                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == warehouseLocationExcelInfo.ZoneNo && d.WmNo == warehouseLocationExcelInfo.WmNo);
                if (zonesInfo == null)
                    throw new Exception("MC:0x00000500");///存储区不存在

                ///数据校验
                ValidInfo(warehouseLocationExcelInfo, allow_zoneno_repeat_at_different_warehouse, allowDlocRepeatAtDifferentZone);

                ///当前业务数据表中此工厂的该物流路线时需要新增
                WarehouseLocationInfo warehouseLocationInfo = warehouseLocationInfos.FirstOrDefault(d =>
                d.Dloc == warehouseLocationExcelInfo.Dloc &&
                d.ZoneNo == warehouseLocationExcelInfo.ZoneNo &&
                d.WmNo == warehouseLocationExcelInfo.WmNo);
                if (warehouseLocationInfo == null)
                {
                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<WarehouseLocationInfo>(warehouseLocationExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///
                    warehouseLocationExcelInfo.Fid = Guid.NewGuid();
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    @string.AppendLine("if not exists (select * from [LES].[TM_BAS_WAREHOUSE_LOCATION] with(nolock) where [DLOC] = N'" + warehouseLocationExcelInfo.Dloc + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_WAREHOUSE_LOCATION] ("
                        + "[FID],"
                        + insertFieldString
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "N'" + warehouseLocationExcelInfo.Fid.GetValueOrDefault() + "',"///FID
                        + insertValueString
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");");
                    warehouseLocationInfos.Add(warehouseLocationExcelInfo);
                    continue;
                }
                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<WarehouseLocationInfo>(warehouseLocationExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                @string.AppendLine("update [LES].[TM_BAS_WAREHOUSE_LOCATION] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [FID] = N'" + warehouseLocationInfo.Fid.GetValueOrDefault() + "';");
            }
            ///执行
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
    }
}


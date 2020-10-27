namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    /// <summary>
    /// ZonesBLL
    /// </summary>
    public class ZonesBLL
    {
        #region Common
        /// <summary>
        /// ZonesDAL
        /// </summary>
        ZonesDAL dal = new ZonesDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<ZonesInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<ZonesInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ZonesInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ZonesInfo info)
        {
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///仓库信息不存在

            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///数据校验
            ValidInfo(info, allow_zoneno_repeat_at_different_warehouse);

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
            ///校验对应是否已经维护了库位TM_BAS_WAREHOUSE_LOCATION，已逻辑删除的库位不在校验范围内
            int cnt = new WarehouseLocationDAL().GetCounts("[ZONE_NO] in (select [ZONE_NO] from LES.[TM_WMM_ZONES] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000724");///存储区下有库位时不允许删除
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
            ZonesInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            ///存储区名称
            info.ZoneName = CommonBLL.GetFieldValue(fields, "ZONE_NAME");
            ///仓库代码
            info.WmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///仓库信息不存在

            ///是否结算
            string settlement_flag = CommonBLL.GetFieldValue(fields, "SETTLEMENT_FLAG");
            if (bool.TryParse(settlement_flag, out bool bool_settlement_flag))
                info.Settlementflag = bool_settlement_flag;
            ///SAP库存地点
            info.StockPlaceNo = CommonBLL.GetFieldValue(fields, "STOCK_PLACE_NO");
            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///数据校验
            ValidInfo(info, allow_zoneno_repeat_at_different_warehouse);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 根据仓库代码获取存储区
        /// </summary>
        /// <param name="wmNo"></param>
        /// <returns></returns>
        public List<ZonesInfo> GetZonesInfos(string wmNo)
        {
            return dal.GetZonesInfos(wmNo);
        }
        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="info"></param>
        /// <param name="allow_zoneno_repeat_at_different_warehouse"></param>
        private void ValidInfo(ZonesInfo info, string allow_zoneno_repeat_at_different_warehouse)
        {
            if (string.IsNullOrEmpty(info.ZoneNo))
                throw new Exception("MC:0x00000527");///存储区代码不能为空

            if (string.IsNullOrEmpty(info.WmNo))
                throw new Exception("MC:0x00000528");///仓库代码不能为空

            string wmNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allow_zoneno_repeat_at_different_warehouse) && allow_zoneno_repeat_at_different_warehouse.ToLower() == "true")
                wmNoCondition = "and [WM_NO] = N'" + info.WmNo + "' ";
            ///存储区代码①不允许重复，单字段进行全表校
            int cnt = dal.GetCounts("[ZONE_NO] = N'" + info.ZoneNo + "' " + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000162");///存储区代码重复

            if (string.IsNullOrEmpty(info.ZoneName))
                throw new Exception("MC:0x00000739");///存储区名称不能为空

            ///存储区名称②在同一仓库范围下不允许重复
            cnt = dal.GetCounts("[ZONE_NAME] = N'" + info.ZoneName + "' " + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000161");///存储区名称重复

            if (info.Settlementflag.GetValueOrDefault() && string.IsNullOrEmpty(info.StockPlaceNo))
                throw new Exception("MC:0x00000218");///结算存储区的库存地点不能为空
        }

        #region Excel Import
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<ZonesInfo> zonesExcelInfos = CommonDAL.DatatableConvertToList<ZonesInfo>(dataTable).ToList();
            if (zonesExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", zonesExcelInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);
            ///仓库
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("" +
                "[WAREHOUSE] in ('" + string.Join("','", zonesExcelInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///允许存储区代码在不同的仓库中重复使用
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///执行的SQL语句
            StringBuilder @string = new StringBuilder();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var zonesExcelInfo in zonesExcelInfos)
            {
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == zonesExcelInfo.WmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///仓库信息不存在

                ///数据校验
                ValidInfo(zonesExcelInfo, allow_zoneno_repeat_at_different_warehouse);

                ///当前业务数据表中此工厂的该物流路线时需要新增
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == zonesExcelInfo.ZoneNo && d.WmNo == zonesExcelInfo.WmNo);
                if (zonesInfo == null)
                {
                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<ZonesInfo>(zonesExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///
                    zonesExcelInfo.Fid = Guid.NewGuid();
                    ///判断业务主键是否重复，以防止EXCEL中有重复数据，适用于基础数据导入
                    @string.AppendLine("if not exists (select * from [LES].[TM_WMM_ZONES] with(nolock) where [ZONE_NO] = N'" + zonesExcelInfo.ZoneNo + "' and [VALID_FLAG] = 1) " +
                        "insert into [LES].[TM_WMM_ZONES] " +
                        "([FID]," + insertFieldString + "[CREATE_USER]," + "[CREATE_DATE]," + "[VALID_FLAG]) values " +
                        "(N'" + zonesExcelInfo.Fid.GetValueOrDefault() + "'," + insertValueString + "N'" + loginUser + "'," + "GETDATE()," + "1);");

                    zonesInfos.Add(zonesExcelInfo);
                    continue;
                }

                if (string.IsNullOrEmpty(zonesExcelInfo.ZoneName))
                    throw new Exception("MC:0x00000739");///存储区名称不能为空

                if (string.IsNullOrEmpty(zonesExcelInfo.WmNo))
                    throw new Exception("MC:0x00000528");///仓库代码不能为空

                if (zonesExcelInfo.Settlementflag == null)
                    throw new Exception("MC:0x00000529");///结算标记不能为空

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<ZonesInfo>(zonesExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                @string.AppendLine("update [LES].[TM_WMM_ZONES] " +
                    "set " + valueString + "[MODIFY_USER] = N'" + loginUser + "'," + "[MODIFY_DATE] = GETDATE() " +
                    "where [FID] = N'" + zonesInfo.Fid.GetValueOrDefault() + "';");
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
        #endregion
    }
}


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
                throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///����У��
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
            ///У���Ӧ�Ƿ��Ѿ�ά���˿�λTM_BAS_WAREHOUSE_LOCATION�����߼�ɾ���Ŀ�λ����У�鷶Χ��
            int cnt = new WarehouseLocationDAL().GetCounts("[ZONE_NO] in (select [ZONE_NO] from LES.[TM_WMM_ZONES] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000724");///�洢�����п�λʱ������ɾ��
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
                throw new Exception("MC:0x00000084");///���ݴ���

            ///�洢������
            info.ZoneName = CommonBLL.GetFieldValue(fields, "ZONE_NAME");
            ///�ֿ����
            info.WmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

            ///�Ƿ����
            string settlement_flag = CommonBLL.GetFieldValue(fields, "SETTLEMENT_FLAG");
            if (bool.TryParse(settlement_flag, out bool bool_settlement_flag))
                info.Settlementflag = bool_settlement_flag;
            ///SAP���ص�
            info.StockPlaceNo = CommonBLL.GetFieldValue(fields, "STOCK_PLACE_NO");
            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///����У��
            ValidInfo(info, allow_zoneno_repeat_at_different_warehouse);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// ���ݲֿ�����ȡ�洢��
        /// </summary>
        /// <param name="wmNo"></param>
        /// <returns></returns>
        public List<ZonesInfo> GetZonesInfos(string wmNo)
        {
            return dal.GetZonesInfos(wmNo);
        }
        /// <summary>
        /// ����У��
        /// </summary>
        /// <param name="info"></param>
        /// <param name="allow_zoneno_repeat_at_different_warehouse"></param>
        private void ValidInfo(ZonesInfo info, string allow_zoneno_repeat_at_different_warehouse)
        {
            if (string.IsNullOrEmpty(info.ZoneNo))
                throw new Exception("MC:0x00000527");///�洢�����벻��Ϊ��

            if (string.IsNullOrEmpty(info.WmNo))
                throw new Exception("MC:0x00000528");///�ֿ���벻��Ϊ��

            string wmNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allow_zoneno_repeat_at_different_warehouse) && allow_zoneno_repeat_at_different_warehouse.ToLower() == "true")
                wmNoCondition = "and [WM_NO] = N'" + info.WmNo + "' ";
            ///�洢������ٲ������ظ������ֶν���ȫ��У
            int cnt = dal.GetCounts("[ZONE_NO] = N'" + info.ZoneNo + "' " + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000162");///�洢�������ظ�

            if (string.IsNullOrEmpty(info.ZoneName))
                throw new Exception("MC:0x00000739");///�洢�����Ʋ���Ϊ��

            ///�洢�����Ƣ���ͬһ�ֿⷶΧ�²������ظ�
            cnt = dal.GetCounts("[ZONE_NAME] = N'" + info.ZoneName + "' " + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000161");///�洢�������ظ�

            if (info.Settlementflag.GetValueOrDefault() && string.IsNullOrEmpty(info.StockPlaceNo))
                throw new Exception("MC:0x00000218");///����洢���Ŀ��ص㲻��Ϊ��
        }

        #region Excel Import
        /// <summary>
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<ZonesInfo> zonesExcelInfos = CommonDAL.DatatableConvertToList<ZonesInfo>(dataTable).ToList();
            if (zonesExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", zonesExcelInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);
            ///�ֿ�
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("" +
                "[WAREHOUSE] in ('" + string.Join("','", zonesExcelInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///ִ�е�SQL���
            StringBuilder @string = new StringBuilder();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var zonesExcelInfo in zonesExcelInfos)
            {
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == zonesExcelInfo.WmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

                ///����У��
                ValidInfo(zonesExcelInfo, allow_zoneno_repeat_at_different_warehouse);

                ///��ǰҵ�����ݱ��д˹����ĸ�����·��ʱ��Ҫ����
                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == zonesExcelInfo.ZoneNo && d.WmNo == zonesExcelInfo.WmNo);
                if (zonesInfo == null)
                {
                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<ZonesInfo>(zonesExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///
                    zonesExcelInfo.Fid = Guid.NewGuid();
                    ///�ж�ҵ�������Ƿ��ظ����Է�ֹEXCEL�����ظ����ݣ������ڻ������ݵ���
                    @string.AppendLine("if not exists (select * from [LES].[TM_WMM_ZONES] with(nolock) where [ZONE_NO] = N'" + zonesExcelInfo.ZoneNo + "' and [VALID_FLAG] = 1) " +
                        "insert into [LES].[TM_WMM_ZONES] " +
                        "([FID]," + insertFieldString + "[CREATE_USER]," + "[CREATE_DATE]," + "[VALID_FLAG]) values " +
                        "(N'" + zonesExcelInfo.Fid.GetValueOrDefault() + "'," + insertValueString + "N'" + loginUser + "'," + "GETDATE()," + "1);");

                    zonesInfos.Add(zonesExcelInfo);
                    continue;
                }

                if (string.IsNullOrEmpty(zonesExcelInfo.ZoneName))
                    throw new Exception("MC:0x00000739");///�洢�����Ʋ���Ϊ��

                if (string.IsNullOrEmpty(zonesExcelInfo.WmNo))
                    throw new Exception("MC:0x00000528");///�ֿ���벻��Ϊ��

                if (zonesExcelInfo.Settlementflag == null)
                    throw new Exception("MC:0x00000529");///�����ǲ���Ϊ��

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<ZonesInfo>(zonesExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                @string.AppendLine("update [LES].[TM_WMM_ZONES] " +
                    "set " + valueString + "[MODIFY_USER] = N'" + loginUser + "'," + "[MODIFY_DATE] = GETDATE() " +
                    "where [FID] = N'" + zonesInfo.Fid.GetValueOrDefault() + "';");
            }
            ///ִ��
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


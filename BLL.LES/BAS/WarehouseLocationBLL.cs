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
                throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

            ZonesInfo zonesInfo = new ZonesDAL().GetZonesInfo(info.ZoneNo, info.WmNo);
            if (zonesInfo == null)
                throw new Exception("MC:0x00000500");///�洢��������

            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///�����λ�����ڲ�ͬ�Ĵ洢�����ظ�ʹ��
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///����У��
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
                throw new Exception("MC:0x00000084");///���ݴ���

            ///�ֿ�
            info.WmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(info.WmNo);
            if (warehouseInfo == null)
                throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

            ///�洢��
            info.ZoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            ZonesInfo zonesInfo = new ZonesDAL().GetZonesInfo(info.ZoneNo, info.WmNo);
            if (zonesInfo == null)
                throw new Exception("MC:0x00000500");///�洢��������

            ///��λ����
            info.StorageLocationName = CommonBLL.GetFieldValue(fields, "STORAGE_LOCATION_NAME");
            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///�����λ�����ڲ�ͬ�Ĵ洢�����ظ�ʹ��
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///����У��
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
        /// ����ɾ��
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
        /// ��֤���ݺϷ���
        /// </summary>
        /// <param name="info"></param>
        private void ValidInfo(WarehouseLocationInfo info, string allow_zoneno_repeat_at_different_warehouse, string allowDlocRepeatAtDifferentZone)
        {
            if (string.IsNullOrEmpty(info.Dloc))
                throw new Exception("MC:0x00000530");///��λ���벻����Ϊ��

            if (string.IsNullOrEmpty(info.ZoneNo))
                throw new Exception("MC:0x00000527");///�洢�����벻��Ϊ��

            if (string.IsNullOrEmpty(info.WmNo))
                throw new Exception("MC:0x00000528");///�ֿ���벻��Ϊ��

            string wmNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allow_zoneno_repeat_at_different_warehouse) && allow_zoneno_repeat_at_different_warehouse.ToLower() == "true")
                wmNoCondition = "and [WM_NO] = N'" + info.WmNo + "' ";
            string zoneNoCondition = string.Empty;
            if (!string.IsNullOrEmpty(allowDlocRepeatAtDifferentZone) && allowDlocRepeatAtDifferentZone.ToLower() == "true")
                zoneNoCondition = "and [ZONE_NO] = N'" + info.ZoneNo + "' ";
            ///��λ����ٲ������ظ������ֶν���ȫ��У��
            int cnt = dal.GetCounts("[DLOC] = N'" + info.Dloc + "' " + zoneNoCondition + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000301");///��λ�����ظ�

            ///��λ���Ƣ���ͬһ�洢����Χ�ڲ������ظ�
            cnt = dal.GetCounts("[STORAGE_LOCATION_NAME] = N'" + info.StorageLocationName + "' " + zoneNoCondition + wmNoCondition);
            if (cnt > 0)
                throw new Exception("MC:0x00000300");///��λ�����ظ�
        }
        /// <summary>
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<WarehouseLocationInfo> warehouseLocationExcelInfos = CommonDAL.DatatableConvertToList<WarehouseLocationInfo>(dataTable).ToList();
            if (warehouseLocationExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<WarehouseLocationInfo> warehouseLocationInfos = new WarehouseLocationDAL().GetList("" +
                "[DLOC] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.Dloc).ToArray()) + "')", string.Empty);
            ///�洢��
            List<ZonesInfo> zonesInfos = new ZonesDAL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.ZoneNo).ToArray()) + "')", string.Empty);
            ///�ֿ�
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetList("" +
                "[WAREHOUSE] in ('" + string.Join("','", warehouseLocationExcelInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///����洢�������ڲ�ͬ�Ĳֿ����ظ�ʹ��
            string allow_zoneno_repeat_at_different_warehouse = new ConfigDAL().GetValueByCode("ALLOW_ZONENO_REPEAT_AT_DIFFERENT_WAREHOUSE");
            ///�����λ�����ڲ�ͬ�Ĵ洢�����ظ�ʹ��
            string allowDlocRepeatAtDifferentZone = new ConfigDAL().GetValueByCode("ALLOW_DLOC_REPEAT_AT_DIFFERENT_ZONE");
            ///ִ�е�SQL���
            StringBuilder @string = new StringBuilder();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var warehouseLocationExcelInfo in warehouseLocationExcelInfos)
            {
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == warehouseLocationExcelInfo.WmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

                ZonesInfo zonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == warehouseLocationExcelInfo.ZoneNo && d.WmNo == warehouseLocationExcelInfo.WmNo);
                if (zonesInfo == null)
                    throw new Exception("MC:0x00000500");///�洢��������

                ///����У��
                ValidInfo(warehouseLocationExcelInfo, allow_zoneno_repeat_at_different_warehouse, allowDlocRepeatAtDifferentZone);

                ///��ǰҵ�����ݱ��д˹����ĸ�����·��ʱ��Ҫ����
                WarehouseLocationInfo warehouseLocationInfo = warehouseLocationInfos.FirstOrDefault(d =>
                d.Dloc == warehouseLocationExcelInfo.Dloc &&
                d.ZoneNo == warehouseLocationExcelInfo.ZoneNo &&
                d.WmNo == warehouseLocationExcelInfo.WmNo);
                if (warehouseLocationInfo == null)
                {
                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<WarehouseLocationInfo>(warehouseLocationExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///
                    warehouseLocationExcelInfo.Fid = Guid.NewGuid();
                    ///�ж�ҵ�������Ƿ��ظ����Է�ֹEXCEL�����ظ����ݣ������ڻ������ݵ���
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
                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<WarehouseLocationInfo>(warehouseLocationExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                @string.AppendLine("update [LES].[TM_BAS_WAREHOUSE_LOCATION] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [FID] = N'" + warehouseLocationInfo.Fid.GetValueOrDefault() + "';");
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
    }
}


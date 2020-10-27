using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public class WarehouseBLL
    {
        #region Common
        WarehouseDAL dal = new WarehouseDAL();

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<WarehouseInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WarehouseInfo Collection </returns>
		public List<WarehouseInfo> GetList(string sql)
        {
            return dal.GetList(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WarehouseInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WarehouseInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WarehouseInfo info)
        {
            ///�ֿ����١��ֿ����Ƣڲ������ظ����ұ�����ֶν���ȫ��У��
            int cnt = dal.GetCounts("[WAREHOUSE] = N'" + info.Warehouse + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000260");///�ֿ�����ظ�
            cnt = dal.GetCounts("[WAREHOUSE_NAME] = N'" + info.WarehouseName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000160");///�ֿ������ظ�

            ///��ѡ���ֿ�����Ϊ�ⲿ�ֿ�VMIʱ����������¹�Ӧ����Ϣ�еĴ��˹�Ӧ��
            new SupplierBLL().SyncSupplierByWarehouse(info, info.CreateUser);

            if (info.WarehouseType == (int)WarehouseTypeConstants.VMI)
            {
                var zoneInfo = new ZonesInfo()
                {
                    ZoneNo = info.Warehouse,
                    ZoneName = info.WarehouseName,
                    Plant = info.Plant,
                    WmNo = info.Warehouse,

                    CreateUser = info.CreateUser,
                    CreateDate = DateTime.Now,
                    Fid = Guid.NewGuid(),
                    ValidFlag = true,
                };
                new ZonesBLL().InsertInfo(zoneInfo);
            }
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
            ///У���Ӧ�Ƿ��Ѿ�ά���˴洢��TM_WMM_ZONES�����߼�ɾ���Ĵ洢������У�鷶Χ��
            int cnt = new ZonesDAL().GetCounts("[WM_NO] in (select [WAREHOUSE] from LES.[TM_BAS_WAREHOUSE] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000720");///�ֿ������д洢����������ɾ��
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            ///�ֿ����Ƣڲ������ظ�
            string warehouseName = CommonBLL.GetFieldValue(fields, "WAREHOUSE_NAME");
            if (string.IsNullOrEmpty(warehouseName))
                throw new Exception("MC:0x00000721");///�ֿ����Ʋ�����Ϊ��
            int cnt = dal.GetCounts("[ID] <> " + id + " and [WAREHOUSE_NAME] = N'" + warehouseName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000160");///�ֿ������ظ�

            ///��ѡ���ֿ�����Ϊ�ⲿ�ֿ�VMIʱ����������¹�Ӧ����Ϣ�еĴ��˹�Ӧ��
            string warehouse = CommonBLL.GetFieldValue(fields, "WAREHOUSE");
            string modifyUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
            string warehouseType = CommonBLL.GetFieldValue(fields, "WAREHOUSE_TYPE");
            WarehouseInfo warehouseInfo = new WarehouseInfo();
            warehouseInfo.Warehouse = warehouse;
            warehouseInfo.WarehouseName = warehouseName;
            int intWarehouseType = 0;
            int.TryParse(warehouseType, out intWarehouseType);

            warehouseInfo.WarehouseType = intWarehouseType;
            new SupplierBLL().SyncSupplierByWarehouse(warehouseInfo, modifyUser);
            ///
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion



        /// <summary>
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<WarehouseInfo> warehouseExcelInfos = CommonDAL.DatatableConvertToList<WarehouseInfo>(dataTable).ToList();
            if (warehouseExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetListForInterfaceDataSync(warehouseExcelInfos.Select(d => d.Warehouse).ToList());
            ///ִ�е�SQL���
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var warehouseExcelInfo in warehouseExcelInfos)
            {
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == warehouseExcelInfo.Warehouse);
                if (warehouseInfo == null)
                {
                    if (string.IsNullOrEmpty(warehouseExcelInfo.Warehouse)
                        || string.IsNullOrEmpty(warehouseExcelInfo.WarehouseName)
                        || warehouseExcelInfo.WarehouseType.GetValueOrDefault() == 0)
                        throw new Exception("MC:0x00000231");///�ֿ���롢���ơ�����Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<WarehouseInfo>(warehouseExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///
                    warehouseExcelInfo.Fid = Guid.NewGuid();
                    ///�ж�ҵ�������Ƿ��ظ����Է�ֹEXCEL�����ظ����ݣ������ڻ������ݵ���
                    sql += "if not exists (select * from LES.TM_BAS_WAREHOUSE with(nolock) "
                        + "where [WAREHOUSE] = N'" + warehouseExcelInfo.Warehouse + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_WAREHOUSE] ("
                        + "[FID],"
                        + insertFieldString
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "N'" + warehouseExcelInfo.Fid.GetValueOrDefault() + "',"///FID
                        + insertValueString
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");";
                    warehouseInfos.Add(warehouseExcelInfo);
                    continue;
                }

                if (string.IsNullOrEmpty(warehouseExcelInfo.Warehouse)
                        || string.IsNullOrEmpty(warehouseExcelInfo.WarehouseName)
                        || warehouseExcelInfo.WarehouseType.GetValueOrDefault() == 0)
                    throw new Exception("MC:0x00000231");///�ֿ���롢���ơ�����Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<WarehouseInfo>(warehouseExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_WAREHOUSE] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [FID] = N'" + warehouseExcelInfo.Fid.GetValueOrDefault() + "';";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;
            using (var trans = new TransactionScope())
            {
                foreach (var warehouseExcelInfo in warehouseExcelInfos)
                {
                    warehouseExcelInfo.CreateDate = DateTime.Now;
                    new SupplierBLL().SyncSupplierByWarehouse(warehouseExcelInfo, loginUser);
                }
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wmNos"></param>
        /// <returns></returns>
        public List<WarehouseInfo> GetListForInterfaceDataSync(List<string> wmNos)
        {
            return dal.GetListForInterfaceDataSync(wmNos);
        }


        public WarehouseInfo GetWarehouseInfo(string WmNo)
        {
            return dal.GetWarehouseInfo(WmNo);
        }
    }
}


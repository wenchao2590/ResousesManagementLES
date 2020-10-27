
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
        /// ��������ѯ
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
        /// ��֤������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SupplierInfo info)
        {
            ///��Ӧ�̴���١��˰�����ڡ���Ӧ�����Ƣۡ���Ӧ�̼�Ƣܵ��ֶ�ȫ��Χ�������ظ�
            int cnt = dal.GetCounts("[SUPPLIER_NUM] = N'" + info.SupplierNum + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000689"); ///��Ӧ�̴����ظ�
            cnt = dal.GetCounts("[SUPPLIER_NAME] = N'" + info.SupplierName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000691");///��Ӧ�������ظ�
            if (!string.IsNullOrEmpty(info.Duns))
            {
                cnt = dal.GetCounts("[DUNS] = N'" + info.Duns + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000690");///�˰������ظ�
            }
            if (!string.IsNullOrEmpty(info.SupplierSname))
            {
                cnt = dal.GetCounts("[SUPPLIER_SNAME] = N'" + info.SupplierSname + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000692");///��Ӧ�̼���ظ�
            }
            ///δѡ�й�Ӧ��������Ĭ��Ϊ���Ϲ�Ӧ��
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
        /// ��֤���޸�����
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            int cnt = 0;
            ///��Ӧ�̼�Ƣڲ������ظ�
            string supplierSname = CommonBLL.GetFieldValue(fields, "SUPPLIER_SNAME");
            if (!string.IsNullOrEmpty(supplierSname))
            {
                cnt = dal.GetCounts("[SUPPLIER_SNAME] = N'" + supplierSname + "'and [ID] <>" + id + "");
                if (cnt > 0)
                    throw new Exception("MC:0x00000692");  ///��Ӧ�̼�Ʋ����ظ�
            }
            ///�˰�����ڲ����ظ�
            string duns = CommonBLL.GetFieldValue(fields, "DUNS");
            if (!string.IsNullOrEmpty(duns))
            {
                cnt = dal.GetCounts("[DUNS] = N'" + duns + "'and [ID] <> " + id + "");
                if (cnt > 0)
                    throw new Exception("MC:0x00000690");///�˰����벻���ظ�
            }

            ///TODO:��Ҫ����һ��������������ֶ�ʱ�����ǲ���д���������ʲô���ķ���
            string supplierType = CommonBLL.GetFieldValue(fields, "SUPPLIER_TYPE");
            ///δѡ�й�Ӧ��������Ĭ��Ϊ���Ϲ�Ӧ��
            if (supplierType == "0" || string.IsNullOrEmpty(supplierType))
                fields = CommonBLL.SetFieldValue(fields, "SUPPLIER_TYPE", "10", false);

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
        /// <summary>
        /// ���ݹ�Ӧ�̴����ȡASN���
        /// </summary>
        /// <param name="SupplierNum"></param>
        /// <returns></returns>
        public bool GetAsnFlag(string SupplierNum)
        {
            return dal.GetAsnFlag(SupplierNum);
        }
        /// <summary>
        /// ��ȡҵ�����Ҫ��������ݼ���
        /// </summary>
        /// <param name="supplierNums"></param>
        /// <returns></returns>
        public List<SupplierInfo> GetListForInterfaceDataSync(List<string> supplierNums)
        {
            if (supplierNums.Count == 0) return new List<SupplierInfo>();
            return dal.GetListForInterfaceDataSync(supplierNums);
        }
        /// <summary>
        /// ���ݲֿ���Ϣͬ����Ӧ����Ϣ
        /// </summary>
        /// <param name="warehouseInfo"></param>
        public void SyncSupplierByWarehouse(WarehouseInfo warehouseInfo, string loginUser)
        {
            ///����Ϊ�ڲ��ֿ�ʱ��ͬ����Ӧ����Ϣ
            if (warehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.VMI) return;
            StringBuilder @string = new StringBuilder();
            ///�Ƿ���ڹ�Ӧ����Ϣ
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
                ///����Ӧ������Ϊ���˹�Ӧ��ʱ�Ÿ�������Ϣ
                if (supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.LogisticsSupplier)
                    @string.AppendLine("update [LES].[TM_BAS_SUPPLIER] " +
                    "set [SUPPLIER_NAME] = N'" + warehouseInfo.WarehouseName + "'," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + supplierInfo.Id + ";");
            }
            ///ͬ����������´洢����Ϣ
            ///��ȡ��ͬ����Ĵ洢��
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
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<SupplierInfo> supplierExcelInfos = CommonDAL.DatatableConvertToList<SupplierInfo>(dataTable).ToList();
            if (supplierExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(supplierExcelInfos.Select(d => d.SupplierNum).ToList());
            ///ִ�е�SQL���
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var supplierExcelInfo in supplierExcelInfos)
            {
                ///��ǰҵ�����ݱ��д˹����ĸ�����·��ʱ��Ҫ����
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == supplierExcelInfo.SupplierNum);
                if (supplierInfo == null)
                {
                    if (string.IsNullOrEmpty(supplierExcelInfo.SupplierNum)
                        || string.IsNullOrEmpty(supplierExcelInfo.SupplierName)
                        || supplierExcelInfo.SupplierType.GetValueOrDefault() == 0)
                        throw new Exception("MC:0x00000221");///��Ӧ�̴��롢���ơ�����Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<SupplierInfo>(supplierExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///�ж�ҵ�������Ƿ��ظ����Է�ֹEXCEL�����ظ����ݣ������ڻ������ݵ���
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
                    throw new Exception("MC:0x00000221");///��Ӧ�̴��롢���ơ�����Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<SupplierInfo>(supplierExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

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


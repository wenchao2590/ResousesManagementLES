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
    /// <summary>
    /// VmiSupplierBLL
    /// </summary>
    public class VmiSupplierBLL
    {
        #region Common
        /// <summary>
        /// VmiSupplierDAL
        /// </summary>
        VmiSupplierDAL dal = new VmiSupplierDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<VmiSupplierInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VmiSupplierInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(VmiSupplierInfo info)
        {
            ///�ֿ����١��洢������ڡ���Ӧ�̴�������Ψһ��У��
            int cnt = dal.GetCounts(string.Format(@"[SUPPLIER_NUM] = N'{0}' and [WM_NO] = N'{1}' and [ZONE_NO] = N'{2}'", info.SupplierNum, info.WmNo, info.ZoneNo));
            if (cnt > 0)
                throw new Exception("MC:0x00000098");///�ֿ⡢�洢������Ӧ�������Ψһ
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
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
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            string wmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            string zoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            ///�ֿ����١��洢������ڡ���Ӧ�̴�������Ψһ��У��
            int cnt = dal.GetCounts(string.Format(@"[SUPPLIER_NUM] = N'{0}' and [WM_NO] = N'{1}' and [ZONE_NO] = N'{2}' and [ID] <> {3}", supplierNum, wmNo, zoneNo, id));
            if (cnt > 0)
                throw new Exception("MC:0x00000098");///�ֿ⡢�洢������Ӧ�������Ψһ
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<VmiSupplierInfo> GetList(string whereText, string orderText)
        {
            return dal.GetList(whereText, orderText);
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
            List<VmiSupplierInfo> vmiSupplierExcelInfos = CommonDAL.DatatableConvertToList<VmiSupplierInfo>(dataTable).ToList();
            if (vmiSupplierExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierDAL().GetListForInterfaceDataSync(vmiSupplierExcelInfos.Select(d => d.SupplierNum).ToList());
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(vmiSupplierExcelInfos.Select(d => d.SupplierNum).ToList());
            List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetListForInterfaceDataSync(vmiSupplierExcelInfos.Select(d => d.WmNo).ToList());
            ///ִ�е�SQL���
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var vmiSupplierExcelInfo in vmiSupplierExcelInfos)
            {
                ///��ǰҵ�����ݱ��д˹����ĸ�����·��ʱ��Ҫ����
                VmiSupplierInfo vmiSupplierInfo = vmiSupplierInfos.FirstOrDefault(d => d.SupplierNum == vmiSupplierExcelInfo.SupplierNum
                && d.WmNo == vmiSupplierExcelInfo.WmNo);
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == vmiSupplierExcelInfo.SupplierNum);
                if (supplierInfo == null)
                    throw new Exception("MC:0x00000229");///��Ӧ����Ϣ������
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == vmiSupplierExcelInfo.WmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///�ֿ���Ϣ������

                vmiSupplierExcelInfo.SupplierName = supplierInfo.SupplierName;

                if (vmiSupplierInfo == null)
                {
                    if (string.IsNullOrEmpty(vmiSupplierExcelInfo.SupplierNum)
                        || string.IsNullOrEmpty(vmiSupplierExcelInfo.WmNo))
                        throw new Exception("MC:0x00000228");///��Ӧ����ֿ�Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<VmiSupplierInfo>(vmiSupplierExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }
                    ///�ж�ҵ�������Ƿ��ظ����Է�ֹEXCEL�����ظ����ݣ������ڻ������ݵ���
                    sql += "if not exists (select * from LES.TM_BAS_VMI_SUPPLIER with(nolock) "
                        + "where [SUPPLIER_NUM] = N'" + vmiSupplierExcelInfo.SupplierNum + "' and [WM_NO] = N'" + vmiSupplierExcelInfo.WmNo + "' and [VALID_FLAG] = 1)"
                        + " insert into [LES].[TM_BAS_VMI_SUPPLIER] ("
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

                if (string.IsNullOrEmpty(vmiSupplierExcelInfo.SupplierNum)
                       || string.IsNullOrEmpty(vmiSupplierExcelInfo.WmNo))
                    throw new Exception("MC:0x00000228");///��Ӧ����ֿ�Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<VmiSupplierInfo>(vmiSupplierExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_VMI_SUPPLIER] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + vmiSupplierInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}


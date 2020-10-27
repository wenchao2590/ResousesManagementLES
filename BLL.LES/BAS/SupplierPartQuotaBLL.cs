using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// SupplierPartQuotaBLL
    /// </summary>
    public class SupplierPartQuotaBLL
    {
        #region Common
        /// <summary>
        /// SupplierPartQuotaDAL
        /// </summary>
        SupplierPartQuotaDAL dal = new SupplierPartQuotaDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SupplierPartQuotaInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SupplierPartQuotaInfo Collection </returns>
		public List<SupplierPartQuotaInfo> GetList(string sql)
        {
            return dal.GetList(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplierPartQuotaInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SupplierPartQuotaInfo info)
        {
            ///���ϺŢ١���Ӧ�̴������ϲ������ظ�����ѡ��
            int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "'");
            if (cnt >= 1)
                throw new Exception("MC:0x00000097");///ͬһ��Ӧ�������ϺŲ������ظ�

            ///�����м��ͬ������
            string sql = GetSyncVmiSupplierPartSql(info, false, info.CreateUser);

            long id = 0;
            using (TransactionScope trans = new TransactionScope())
            {
                id = dal.Add(info);
                if (id > 0 && sql.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return id;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            SupplierPartQuotaInfo supplierPartQuotaInfo = dal.GetInfo(id);
            if (supplierPartQuotaInfo == null)
                throw new Exception("MC:0x00000084");///���ݴ���

            ///�����м��ͬ������
            string sql = GetSyncVmiSupplierPartSql(supplierPartQuotaInfo, true, loginUser);

            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.LogicDelete(id, loginUser) == 0)
                    return false;
                if (sql.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            SupplierPartQuotaInfo supplierPartQuotaInfo = dal.GetInfo(id);
            if (supplierPartQuotaInfo == null)
                throw new Exception("MC:0x00000084");///���ݴ���

            string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
            ///�����м��ͬ������
            string sql = GetSyncVmiSupplierPartSql(supplierPartQuotaInfo, false, loginUser);

            using (TransactionScope trans = new TransactionScope())
            {
                if (dal.UpdateInfo(fields, id) == 0)
                    return false;
                if (sql.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Private
        /// <summary>
        /// ��ȡͬ��VMI��Ӧ�����Ϲ�ϵ�ӿ�ͬ�����
        /// </summary>
        /// <param name="supplierPartQuotaInfo"></param>
        /// <param name="deleteFlag"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetSyncVmiSupplierPartSql(SupplierPartQuotaInfo supplierPartQuotaInfo, bool deleteFlag, string loginUser)
        {
            StringBuilder sql = new StringBuilder();
            ///��Ӧ������
            string supplierName = new SupplierDAL().GetSupplierName(supplierPartQuotaInfo.SupplierNum);
            ///������������
            string partCname = new MaintainPartsDAL().GetPartCname(supplierPartQuotaInfo.PartNo, supplierPartQuotaInfo.Plant);

            if (!string.IsNullOrEmpty(partCname))
            {
                partCname = partCname.Replace("'", "''");
            }

            ///��Ӧ�̶�Ӧ���ⲿ�ֿ�
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList("[WAREHOUSE] in (select [WM_NO] from [LES].[TM_BAS_VMI_SUPPLIER] with(nolock) " +
                "where [SUPPLIER_NUM] = N'" + supplierPartQuotaInfo.SupplierNum + "' and [VALID_FLAG] = 1) and " +
                "[WAREHOUSE_TYPE] = " + (int)WarehouseTypeConstants.VMI + "", string.Empty);
            if (warehouseInfos.Count == 0) return string.Empty;
            ///�Ƿ�����SRMϵͳ���
            string enable_srm_flag = new ConfigDAL().GetValueByCode("ENABLE_SRM_FLAG");
            if (enable_srm_flag.ToLower() == "true")
            {
                ///TI_IFM_SRM_VMI_SUPPLIER_PART
                SrmVmiSupplierPartInfo srmVmiSupplierPartInfo = new SrmVmiSupplierPartInfo();
                srmVmiSupplierPartInfo.LogFid = Guid.NewGuid();
                srmVmiSupplierPartInfo.Plant = supplierPartQuotaInfo.Plant;
                srmVmiSupplierPartInfo.SupplierCode = supplierPartQuotaInfo.SupplierNum;
                srmVmiSupplierPartInfo.SupplierName = supplierName;
                srmVmiSupplierPartInfo.PartNo = supplierPartQuotaInfo.PartNo;
                srmVmiSupplierPartInfo.PartCname = partCname;
                srmVmiSupplierPartInfo.DeleteFlag = deleteFlag;
                srmVmiSupplierPartInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                srmVmiSupplierPartInfo.CreateUser = loginUser;


                foreach (WarehouseInfo warehouseInfo in warehouseInfos)
                {
                    srmVmiSupplierPartInfo.VmiWarehouseCode = warehouseInfo.Warehouse;
                    srmVmiSupplierPartInfo.VmiWarehouseName = warehouseInfo.WarehouseName;
                    if (string.IsNullOrEmpty(srmVmiSupplierPartInfo.Plant)) srmVmiSupplierPartInfo.Plant = warehouseInfo.Plant;
                    ///
                    sql.AppendLine(SrmVmiSupplierPartDAL.GetInsertSql(srmVmiSupplierPartInfo));
                }
                sql.AppendLine(CommonBLL.GetCreateOutboundLogSql(
                    "SRM",
                    srmVmiSupplierPartInfo.LogFid.GetValueOrDefault(),
                    "LES-SRM-001",
                    srmVmiSupplierPartInfo.PartNo + "|" + srmVmiSupplierPartInfo.SupplierCode + "|" + string.Join("|", warehouseInfos.Select(d => d.Warehouse).ToArray()),
                    loginUser));
            }
            ///Log.WriteLogToFile("enable_srm_flag.ToLower();"+ enable_srm_flag.ToLower() +"; \r" + sql.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHH"));

            ///�Ƿ�����WMSϵͳ���
            string enable_vmi_flag = new ConfigDAL().GetValueByCode("ENABLE_VMI_FLAG");
            //if (enable_vmi_flag.ToLower() == "true")
            //{
            //    ///TI_IFM_SRM_VMI_SUPPLIER_PART
            //    WmsVmiSupplierPartInfo wmsVmiSupplierPartInfo = new WmsVmiSupplierPartInfo();
            //    wmsVmiSupplierPartInfo.LogFid = Guid.NewGuid();
            //    wmsVmiSupplierPartInfo.Werks = supplierPartQuotaInfo.Plant;///TODO:SAP��������?
            //    wmsVmiSupplierPartInfo.SupplierCode = supplierPartQuotaInfo.SupplierNum;
            //    wmsVmiSupplierPartInfo.SupplierName = supplierName;
            //    wmsVmiSupplierPartInfo.PartNo = supplierPartQuotaInfo.PartNo;
            //    wmsVmiSupplierPartInfo.PartCname = partCname;
            //    wmsVmiSupplierPartInfo.Cartoncode = supplierPartQuotaInfo.TransPackageModel;
            //    wmsVmiSupplierPartInfo.Cartonqty = supplierPartQuotaInfo.TransPackage;
            //    wmsVmiSupplierPartInfo.DeleteFlag = deleteFlag;
            //    wmsVmiSupplierPartInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            //    wmsVmiSupplierPartInfo.CreateUser = loginUser;
            //    foreach (WarehouseInfo warehouseInfo in warehouseInfos)
            //    {
            //        wmsVmiSupplierPartInfo.VmiWarehouseCode = warehouseInfo.Warehouse;
            //        wmsVmiSupplierPartInfo.VmiWarehouseName = warehouseInfo.WarehouseName;
            //        ///
            //        sql.AppendLine(WmsVmiSupplierPartDAL.GetInsertSql(wmsVmiSupplierPartInfo));
            //    }
            //    sql.AppendLine(CommonBLL.GetCreateOutboundLogSql(
            //        "VMI",
            //        wmsVmiSupplierPartInfo.LogFid.GetValueOrDefault(),
            //        "LES-WMS-008",
            //        wmsVmiSupplierPartInfo.PartNo + "|" + wmsVmiSupplierPartInfo.SupplierCode + "|" + string.Join("|", warehouseInfos.Select(d => d.Warehouse).ToArray()),
            //        loginUser));
            //}
            ///
            return sql.ToString();
        }

        /// <summary>
        /// ��ȡͬ��VMI��Ӧ�����Ϲ�ϵ�ӿ�ͬ�����
        /// </summary>
        /// <param name="supplierName"></param>
        /// <param name="partCname"></param>
        /// <param name="warehouseInfos"></param>
        /// <param name="supplierPartQuotaInfo"></param>
        /// <param name="enable_srm_flag"></param>
        /// <param name="enable_vmi_flag"></param>
        /// <param name="deleteFlag"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetSyncVmiSupplierPartSql(string supplierName, string partCname, List<WarehouseInfo> warehouseInfos, SupplierPartQuotaInfo supplierPartQuotaInfo, string enable_srm_flag, string enable_vmi_flag, bool deleteFlag, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///
            if (!string.IsNullOrEmpty(enable_srm_flag) && enable_srm_flag.ToLower() == "true")
            {
                ///TI_IFM_SRM_VMI_SUPPLIER_PART
                SrmVmiSupplierPartInfo srmVmiSupplierPartInfo = new SrmVmiSupplierPartInfo();
                srmVmiSupplierPartInfo.LogFid = Guid.NewGuid();
                srmVmiSupplierPartInfo.Plant = supplierPartQuotaInfo.Plant;
                srmVmiSupplierPartInfo.SupplierCode = supplierPartQuotaInfo.SupplierNum;
                srmVmiSupplierPartInfo.SupplierName = supplierName;
                srmVmiSupplierPartInfo.PartNo = supplierPartQuotaInfo.PartNo;
                srmVmiSupplierPartInfo.PartCname = partCname;
                srmVmiSupplierPartInfo.DeleteFlag = deleteFlag;
                srmVmiSupplierPartInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                srmVmiSupplierPartInfo.CreateUser = loginUser;

                if (warehouseInfos.Count > 0)
                    foreach (WarehouseInfo warehouseInfo in warehouseInfos)
                    {
                        srmVmiSupplierPartInfo.VmiWarehouseCode = warehouseInfo.Warehouse;
                        srmVmiSupplierPartInfo.VmiWarehouseName = warehouseInfo.WarehouseName;
                        ///
                        @string.AppendLine(SrmVmiSupplierPartDAL.GetInsertSql(srmVmiSupplierPartInfo));
                    }
                else
                    @string.AppendLine(SrmVmiSupplierPartDAL.GetInsertSql(srmVmiSupplierPartInfo));
                @string.AppendLine(CommonBLL.GetCreateOutboundLogSql(
                    "SRM",
                    srmVmiSupplierPartInfo.LogFid.GetValueOrDefault(),
                    "LES-SRM-001",
                    srmVmiSupplierPartInfo.PartNo + "|" + srmVmiSupplierPartInfo.SupplierCode + "|" + string.Join("|", warehouseInfos.Select(d => d.Warehouse).ToArray()),
                    loginUser));
            }
            ///���û�вֿ���Ϣ�򲻷��͸�WMS
            if (warehouseInfos.Count == 0)
                return @string.ToString();
            ///
            //if (!string.IsNullOrEmpty(enable_vmi_flag) && enable_vmi_flag.ToLower() == "true")
            //{
            //    ///TI_IFM_SRM_VMI_SUPPLIER_PART
            //    WmsVmiSupplierPartInfo wmsVmiSupplierPartInfo = new WmsVmiSupplierPartInfo();
            //    wmsVmiSupplierPartInfo.LogFid = Guid.NewGuid();
            //    wmsVmiSupplierPartInfo.Werks = supplierPartQuotaInfo.Plant;///TODO:SAP��������?
            //    wmsVmiSupplierPartInfo.SupplierCode = supplierPartQuotaInfo.SupplierNum;
            //    wmsVmiSupplierPartInfo.SupplierName = supplierName;
            //    wmsVmiSupplierPartInfo.PartNo = supplierPartQuotaInfo.PartNo;
            //    wmsVmiSupplierPartInfo.PartCname = partCname;
            //    wmsVmiSupplierPartInfo.Cartoncode = supplierPartQuotaInfo.TransPackageModel;
            //    wmsVmiSupplierPartInfo.Cartonqty = supplierPartQuotaInfo.TransPackage;
            //    wmsVmiSupplierPartInfo.DeleteFlag = deleteFlag;
            //    wmsVmiSupplierPartInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            //    wmsVmiSupplierPartInfo.CreateUser = loginUser;
            //    foreach (WarehouseInfo warehouseInfo in warehouseInfos)
            //    {
            //        wmsVmiSupplierPartInfo.VmiWarehouseCode = warehouseInfo.Warehouse;
            //        wmsVmiSupplierPartInfo.VmiWarehouseName = warehouseInfo.WarehouseName;
            //        ///
            //        @string.AppendLine(WmsVmiSupplierPartDAL.GetInsertSql(wmsVmiSupplierPartInfo));
            //    }
            //    @string.AppendLine(CommonBLL.GetCreateOutboundLogSql(
            //        "VMI",
            //        wmsVmiSupplierPartInfo.LogFid.GetValueOrDefault(),
            //        "LES-WMS-008",
            //        wmsVmiSupplierPartInfo.PartNo + "|" + wmsVmiSupplierPartInfo.SupplierCode + "|" + string.Join("|", warehouseInfos.Select(d => d.Warehouse).ToArray()),
            //        loginUser));
            //}
            ///
            return @string.ToString();
        }


        /// <summary>
        /// ��ȡͬ������ıȽϼ���
        /// </summary>
        /// <param name="partNos"></param>
        /// <returns></returns>
        public List<SupplierPartQuotaInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            if (partNos.Count == 0) return new List<SupplierPartQuotaInfo>();
            return dal.GetListForInterfaceDataSync(partNos);
        }
        /// <summary>
        /// ImportDataByExcel
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<SupplierPartQuotaInfo> supplierPartQuotaExcelInfos = CommonDAL.DatatableConvertToList<SupplierPartQuotaInfo>(dataTable).ToList();
            if (supplierPartQuotaExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<SupplierPartQuotaInfo> supplierPartQuotaInfos = new SupplierPartQuotaDAL().GetListForInterfaceDataSync(supplierPartQuotaExcelInfos.Select(d => d.PartNo).ToList());

            ///ִ�е�SQL���
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var supplierPartQuotaExcelInfo in supplierPartQuotaExcelInfos)
            {
                SupplierPartQuotaInfo supplierPartQuotaInfo = supplierPartQuotaInfos.FirstOrDefault(d =>
                d.PartNo == supplierPartQuotaExcelInfo.PartNo &&
                d.SupplierNum == supplierPartQuotaExcelInfo.SupplierNum);
                if (supplierPartQuotaInfo == null)
                {
                    if (string.IsNullOrEmpty(supplierPartQuotaExcelInfo.PartNo)
                        || string.IsNullOrEmpty(supplierPartQuotaExcelInfo.SupplierNum))
                        throw new Exception("MC:0x00000297");///���Ϻš���Ӧ��Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<SupplierPartQuotaInfo>(supplierPartQuotaExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from [LES].[TM_BAS_SUPPLIER_PART_QUOTA] with(nolock) where " +
                        "[PART_NO] = N'" + supplierPartQuotaExcelInfo.PartNo + "' and " +
                        "[SUPPLIER_NUM] = N'" + supplierPartQuotaExcelInfo.SupplierNum + "' and " +
                        "[VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_SUPPLIER_PART_QUOTA] ("
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
                ///
                if (string.IsNullOrEmpty(supplierPartQuotaExcelInfo.PartNo)
                        || string.IsNullOrEmpty(supplierPartQuotaExcelInfo.SupplierNum))
                    throw new Exception("MC:0x00000297");///���Ϻš���Ӧ��Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<SupplierPartQuotaInfo>(supplierPartQuotaExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_SUPPLIER_PART_QUOTA] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + supplierPartQuotaInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
        public List<SupplierPartQuotaInfo> GetListByPages(string textWhere, int pageIndex, int pageRow, string wmNo, out int dataCount)
        {
            List<SupplierPartQuotaInfo> supplierPartQuotaInfos = new List<SupplierPartQuotaInfo>();

            SupplierPartQuotaInfo supplierPart = dal.GetListByPages(textWhere, "[ID] asc", pageIndex, pageRow).FirstOrDefault();
            if (supplierPart == null)
            {
                dataCount = supplierPartQuotaInfos.Count();
                return supplierPartQuotaInfos;
            }

            WarehouseInfo warehouseInfo = new WarehouseDAL().GetList("[WAREHOUSE] = N'" + wmNo + "'", string.Empty).FirstOrDefault();
            SupplierPartQuotaInfo partQuotaInfo = new SupplierPartQuotaInfo();
           // partQuotaInfo.InboundPackageModel = supplierPart.InboundPackageModel;
            partQuotaInfo.Plant = "��׼��װ�ͺ�";
           //partQuotaInfo.InboundPackage = supplierPart.InboundPackage;
            supplierPartQuotaInfos.Add(partQuotaInfo);

            partQuotaInfo = new SupplierPartQuotaInfo();
            //partQuotaInfo.InboundPackageModel = supplierPart.TransPackageModel;
            //partQuotaInfo.Plant = "�����װ�ͺ�";
            //partQuotaInfo.InboundPackage = supplierPart.TransPackage;
            supplierPartQuotaInfos.Add(partQuotaInfo);

            partQuotaInfo = new SupplierPartQuotaInfo();
            //partQuotaInfo.InboundPackageModel = supplierPart.OnlinePackageModel;
            //partQuotaInfo.Plant = "���߰�װ�ͺ�";
            //partQuotaInfo.InboundPackage = supplierPart.OnlinePackage;
            supplierPartQuotaInfos.Add(partQuotaInfo);

            if (warehouseInfo.WarehouseType == (int)WarehouseTypeConstants.VMI)
            {
                SupplierPartQuotaInfo supplierPartQuota = new SupplierPartQuotaInfo();
                supplierPartQuota = supplierPartQuotaInfos[0];
                supplierPartQuotaInfos[0] = supplierPartQuotaInfos[1];
                supplierPartQuotaInfos[1] = supplierPartQuota;

            }
            dataCount = supplierPartQuotaInfos.Count();
            return supplierPartQuotaInfos;

        }

        #region Interface
        /// <summary>
        /// Create SupplierPartQuotaInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>SupplierPartQuotaInfo</returns>
        public static SupplierPartQuotaInfo CreateSupplierPartQuotaInfo(string loginUser)
        {
            SupplierPartQuotaInfo info = new SupplierPartQuotaInfo();
            ///FID,
            info.Fid = Guid.NewGuid();
            ///PLANT,����
            info.Plant = null;
            ///PART_NO,���Ϻ�
            info.PartNo = null;
            ///SUPPLIER_NUM,��Ӧ��
            info.SupplierNum = null;
            ///TRANS_PACKAGE_MODEL,�����װ�ͺ�
            //info.TransPackageModel = null;
            /////TRANS_PACKAGE,�����װ������
            //info.TransPackage = null;
            /////INBOUND_PACKAGE_MODEL,��׼����װ
            //info.InboundPackageModel = null;
            /////INBOUND_PACKAGE,��׼��װ������
            //info.InboundPackage = null;
            /////ONLINE_PACKAGE_MODEL,���߰�װ�ͺ�
            //info.OnlinePackageModel = null;
            /////ONLINE_PACKAGE,���߰�װ������
            //info.OnlinePackage = null;
            /////EFFECTIVE_DATE,��Ч����
            //info.EffectiveDate = null;
            /////INVALID_DATE,ʧЧ����
            //info.InvalidDate = null;
            ///QUOTE,���
            info.Quote = null;
            ///PROJECT,��Ŀ
            info.Project = null;
            ///AGREEMENT_NO,Э����
            info.AgreementNo = null;
            ///COMMENTS,��ע
            info.Comments = null;
            ///VALID_FLAG,
            info.ValidFlag = true;
            ///CREATE_USER,������
            info.CreateUser = loginUser;
            ///CREATE_DATE,����ʱ��
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER,MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE,MODIFY_DATE
            info.ModifyDate = null;
            return info;
        }
        /// <summary>
        /// SapSupplierQuotaInfo -> SupplierPartQuotaInfo
        /// </summary>
        /// <param name="sapSupplierQuotaInfo"></param>
        /// <param name="info"></param>
        public static void GetSupplierPartQuotaInfo(SapSupplierQuotaInfo sapSupplierQuotaInfo, ref SupplierPartQuotaInfo info)
        {
            if (sapSupplierQuotaInfo == null) return;
            ///FID,
            info.Fid = Guid.NewGuid();
            ///PART_NO,�����
            info.PartNo = sapSupplierQuotaInfo.PartNo;
            ///QUOTE,���
            info.Quote = sapSupplierQuotaInfo.Quote;
            ///EFFECTIVE_DATE,��Ч����
            //info.EffectiveDate = sapSupplierQuotaInfo.IDate;
            /////INVALID_DATE,ʧЧ����
            //info.InvalidDate = sapSupplierQuotaInfo.EDate;
            ///AGREEMENT_NO,Э����
            info.AgreementNo = sapSupplierQuotaInfo.Znrmm;
            ///SUPPLIER_NUM,��Ӧ��
            info.SupplierNum = sapSupplierQuotaInfo.Lifnr;
        }
        #endregion

    }
}


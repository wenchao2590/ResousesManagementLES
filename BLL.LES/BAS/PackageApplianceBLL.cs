namespace BLL.LES
{
    using BLL.SYS;
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Transactions;
    /// <summary>
    /// PackageApplianceBLL
    /// </summary>
    public class PackageApplianceBLL
    {
        #region Common
        /// <summary>
        /// PackageApplianceDAL
        /// </summary>
        PackageApplianceDAL dal = new PackageApplianceDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<PackageApplianceInfo></returns>
        public List<PackageApplianceInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<PackageApplianceInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PackageApplianceInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PackageApplianceInfo info)
        {
            int count = dal.GetCounts("[PACKAGE_NO] = N'" + info.PackageNo + "'");
            if (count > 0)
                throw new Exception("MC:0x00000296");///器具型号不能重复

            ///包装器具基础数据同步VMI系统标记
            string vmiBasPackageSyncFlag = new ConfigBLL().GetValueByCode("VMI_BAS_PACKAGE_SYNC_FLAG");
            string sql = string.Empty;
            if (vmiBasPackageSyncFlag.ToLower() == "true")
            {
                if (!string.IsNullOrEmpty(info.SupplierNum))
                {
                    Guid logFid = Guid.NewGuid();
                    sql += "insert into [LES].[TI_IFM_WMS_VMI_PACKAGE] (" +
                       "[FID],[LOG_FID],[SKU],[SKUDESCR],[SKUCLS],[SUPPLYCODE]," +
                       "[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG]) values (" +
                       "NEWID(),N'" + logFid + "',N'" + info.PackageNo + "',N'" + info.PackageCname + "',N'" + info.PackageType + "',N'" + info.SupplierNum + "'," +
                       "1,N'" + info.CreateUser + "',GETDATE()," + (int)ProcessFlagConstants.Untreated + ");";
                    sql += CommonBLL.GetCreateOutboundLogSql("VMI", logFid, "LES-WMS-015", info.PackageNo, info.CreateUser);
                }
            }
            using (var trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                info.Id = dal.Add(info);
                trans.Complete();
            }
            return info.Id;
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
            ///包装器具基础数据同步VMI系统标记
            string vmiBasPackageSyncFlag = new ConfigBLL().GetValueByCode("VMI_BAS_PACKAGE_SYNC_FLAG");
            string sql = string.Empty;
            if (vmiBasPackageSyncFlag.ToLower() == "true")
            {
                string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
                if (!string.IsNullOrEmpty(supplierNum))
                {
                    Guid logFid = Guid.NewGuid();
                    string packageNo = CommonBLL.GetFieldValue(fields, "PACKAGE_NO");
                    string packageCname = CommonBLL.GetFieldValue(fields, "PACKAGE_CNAME");
                    string packageType = CommonBLL.GetFieldValue(fields, "PACKAGE_TYPE");
                    string loginUser = CommonBLL.GetFieldValue(fields, "MODIFY_USER");
                    sql += "insert into [LES].[TI_IFM_WMS_VMI_PACKAGE] (" +
                   "[FID],[LOG_FID],[SKU],[SKUDESCR],[SKUCLS],[SUPPLYCODE]," +
                   "[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[PROCESS_FLAG]) values (" +
                   "NEWID(),N'" + logFid + "',N'" + packageNo + "',N'" + packageCname + "',N'" + packageType + "',N'" + supplierNum + "'," +
                   "1,N'" + loginUser + "',GETDATE()," + (int)ProcessFlagConstants.Untreated + ");";
                    sql += CommonBLL.GetCreateOutboundLogSql("VMI", logFid, "LES-WMS-015", packageNo, loginUser);
                }
            }
            using (var trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                dal.UpdateInfo(fields, id);
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Private
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<PackageApplianceInfo> packageApplianceExcelInfos = CommonDAL.DatatableConvertToList<PackageApplianceInfo>(dataTable).ToList();
            if (packageApplianceExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<PackageApplianceInfo> packageApplianceInfos = new PackageApplianceDAL().GetListForInterfaceDataSync(packageApplianceExcelInfos.Select(d => d.PackageNo).ToList());

            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var packageApplianceExcelInfo in packageApplianceExcelInfos)
            {
                PackageApplianceInfo packageApplianceInfo = packageApplianceInfos.FirstOrDefault(d => d.PackageNo == packageApplianceExcelInfo.PackageNo);
                if (packageApplianceInfo == null)
                {
                    if (string.IsNullOrEmpty(packageApplianceExcelInfo.PackageNo)
                        || string.IsNullOrEmpty(packageApplianceExcelInfo.PackageCname))
                        throw new Exception("MC:0x00000237");///包装器具代码，名称为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<PackageApplianceInfo>(packageApplianceExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from [LES].[TM_BAS_PACKAGE_APPLIANCE] with(nolock) where [LOCATION] = N'" + packageApplianceExcelInfo.PackageNo + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_PACKAGE_APPLIANCE] ("
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
                if (string.IsNullOrEmpty(packageApplianceExcelInfo.PackageNo)
                        || string.IsNullOrEmpty(packageApplianceExcelInfo.PackageCname))
                    throw new Exception("MC:0x00000237");///包装器具代码，名称为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<PackageApplianceInfo>(packageApplianceExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_PACKAGE_APPLIANCE] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + packageApplianceInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        #endregion
    }
}


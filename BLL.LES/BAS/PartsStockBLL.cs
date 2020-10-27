using BLL.SYS;
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
    public class PartsStockBLL
    {
        #region Common
        /// <summary>
        /// PartsStockDAL
        /// </summary>
        PartsStockDAL dal = new PartsStockDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PartsStockInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);

        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartsStockInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PartsStockInfo info)
        {
            ///入库包装数量⑧不能小于等于零，当入库包装型号⑦不为空时入库包装数量⑧为必填项
            if (!string.IsNullOrEmpty(info.InboundPackageModel) && info.InboundPackage.GetValueOrDefault() <= 0)
                throw new Exception("MC:0x00000089");///当入库包装型号不为空时入库包装数量必须大于零

            ///最小库存 小于等于 安全库存 小于 最大库存，最小库存必须大于等于零
            if (info.Min.GetValueOrDefault() > info.SafeStock.GetValueOrDefault())
                throw new Exception("MC:0x00000090");///最小库存不能大于安全库存

            //if (info.IsOutput == true)
            //{
            //    if (info.SynchronousWmNo == "" || info.SynchronousZoneNo == "")
            //        throw new Exception("MC:0x00000439");///当同步出库标记为是时,同步出库仓库,存储区为必填项
            //}

            if (info.SafeStock.GetValueOrDefault() >= 0
                && info.Max.GetValueOrDefault() > 0
                && info.SafeStock.GetValueOrDefault() >= info.Max.GetValueOrDefault())
                throw new Exception("MC:0x00000091");///安全库存不能大于等于最大库存

            ///相同物料号②、供应商代码①、仓库代码⑨、存储区代码⑩、库位代码⑪的数据不允许重复
            int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "' and [SUPPLIER_NUM] = N'" + info.SupplierNum + "' and [WM_NO] = N'" + info.WmNo + "' and [ZONE_NO] = N'" + info.ZoneNo + "'"
                + (string.IsNullOrEmpty(info.Dloc) ? string.Empty : " and [DLOC] = N'" + info.Dloc + "'"));
            if (cnt > 0)
                throw new Exception("MC:0x00000094");///相同物料号、供应商代码、仓库代码、存储区代码、库位代码的数据不允许重复


            ///若该标记为true时，需要校验该仓库存储区下所有物料的同步出库仓库、同步出库存储区必须相同，是否同步出库默认为false
            string sameZoneSameFinalZoneValidFlag = new ConfigDAL().GetValueByCode("SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG");

            //if (Convert.ToBoolean(sameZoneSameFinalZoneValidFlag) == true )
            //{
            //    //判断数据库中是否存在 仓库、存储区、是否同步出库 如果没有跳过如果有进行判断是否同步出库仓库、同步出库存储区一致
            //    int count = new PartsStockDAL().GetCounts("[WM_NO] = N'" + info.WmNo + "' and[ZONE_NO] = N'" + info.ZoneNo );
            //    if (count != 0)
            //    {
            //        count = new PartsStockDAL().GetCounts("[WM_NO] = N'" + info.WmNo + "' and[ZONE_NO] = N'" + info.ZoneNo + "' and [IS_OUTPUT] = N'" + info.IsOutput + "' and [SYNCHRONOUS_WM_NO] = N'" + info.SynchronousWmNo + "' and [SYNCHRONOUS_ZONE_NO] = N'" + info.SynchronousZoneNo + "'");
            //        if (count == 0)
            //            throw new Exception("MC:0x00000438");///需要校验该仓库存储区下所有物料的同步出库仓库、同步出库存储区必须相同
            //    }


            //}
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
            string inboundPackageModel = CommonBLL.GetFieldValue(fields, "INBOUND_PACKAGE_MODEL");
            string inboundPackage = CommonBLL.GetFieldValue(fields, "INBOUND_PACKAGE");
            ///入库包装数量⑧不能小于等于零，当入库包装型号⑦不为空时入库包装数量⑧为必填项
            if (!string.IsNullOrEmpty(inboundPackageModel) && int.Parse(inboundPackage) <= 0)
                throw new Exception("MC:0x00000089");///当入库包装型号不为空时入库包装数量必须大于零

            if (CommonBLL.GetFieldValue(fields, "IS_OUTPUT") == "1")
            {
                if (CommonBLL.GetFieldValue(fields, "SYNCHRONOUS_WM_NO") == "" || CommonBLL.GetFieldValue(fields, "SYNCHRONOUS_ZONE_NO") == "")
                    throw new Exception("MC:0x00000439");///当同步出库标记为是时,同步出库仓库,存储区为必填项
            }

            string min = CommonBLL.GetFieldValue(fields, "MIN");
            int intMin = 0;
            int.TryParse(min, out intMin);
            string safeStock = CommonBLL.GetFieldValue(fields, "SAFE_STOCK");
            int intSafeStock = 0;
            int.TryParse(safeStock, out intSafeStock);
            ///最小库存 小于等于 安全库存 小于 最大库存，最小库存必须大于等于零
            if (intMin > intSafeStock)
                throw new Exception("MC:0x00000090");///最小库存不能大于安全库存

            string max = CommonBLL.GetFieldValue(fields, "MAX");
            int intMax = 0;
            int.TryParse(max, out intMax);
            if (intSafeStock >= 0 && intMax > 0 && intSafeStock >= intMax)
                throw new Exception("MC:0x00000091");///安全库存不能大于等于最大库存

            string partNo = CommonBLL.GetFieldValue(fields, "PART_NO");
            string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
            string wmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            string zoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            string dloc = CommonBLL.GetFieldValue(fields, "DLOC");
            ///相同物料号②、供应商代码①、仓库代码⑨、存储区代码⑩、库位代码⑪的数据不允许重复
            int cnt = dal.GetCounts(" [ID] <> " + id + " and [PART_NO] = N'" + partNo + "' and [SUPPLIER_NUM] = N'" + supplierNum + "' and [WM_NO] = N'" + wmNo + "' and [ZONE_NO] = N'" + zoneNo + "'"
                + (string.IsNullOrEmpty(dloc) ? string.Empty : " and [DLOC] = N'" + dloc + "'"));
            if (cnt > 0)
                throw new Exception("MC:0x00000094");///相同物料号、供应商代码、仓库代码、存储区代码、库位代码的数据不允许重复
            string sameZoneSameFinalZoneValidFlag = new ConfigDAL().GetValueByCode("SAME_ZONE_SAME_FINAL_ZONE_VALID_FLAG");

            ///TODO:若该标记为true时，需要校验该仓库存储区下所有物料的同步出库仓库、同步出库存储区必须相同，是否同步出库默认为false
            if (Convert.ToBoolean(sameZoneSameFinalZoneValidFlag) == true && CommonBLL.GetFieldValue(fields, "IS_OUTPUT") == "1")
            {
                ///判断当前 仓库、存储区、是否同步出库 是否在表中存在，如果没有进行修改，如果有进行判断
                int count = new PartsStockDAL().GetCounts("[ID] <> " + id + " and [WM_NO] = N'" + CommonBLL.GetFieldValue(fields, "WM_NO") + "' and[ZONE_NO] = N'" + CommonBLL.GetFieldValue(fields, "ZONE_NO") + "' and [IS_OUTPUT] = N'" + CommonBLL.GetFieldValue(fields, "IS_OUTPUT") + "'");
                if (count != 0)
                {

                    count = new PartsStockDAL().GetCounts("[ID] <> " + id + " and [WM_NO] = N'" + CommonBLL.GetFieldValue(fields, "WM_NO") + "' and[ZONE_NO] = N'" + CommonBLL.GetFieldValue(fields, "ZONE_NO") + "' and [IS_OUTPUT] = N'" + CommonBLL.GetFieldValue(fields, "IS_OUTPUT") + "' and [SYNCHRONOUS_WM_NO] = N'" + CommonBLL.GetFieldValue(fields, "SYNCHRONOUS_WM_NO") + "' and [SYNCHRONOUS_ZONE_NO] = N'" + CommonBLL.GetFieldValue(fields, "SYNCHRONOUS_ZONE_NO") + "'");
                    if (count == 0)
                        throw new Exception("MC:0x00000438");///需要校验该仓库存储区下所有物料的同步出库仓库、同步出库存储区必须相同
                }

            }

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        public List<PartsStockInfo> GetList(string where, string order)
        {
            return dal.GetList(where, order);
        }

        #endregion

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <returns></returns>
        public PartsStockInfo GetStockInfo(string partNo, string supplierNum, string wmNo, string zoneNo)
        {
            return dal.GetStockInfo(partNo, supplierNum, wmNo, zoneNo);
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<PartsStockInfo> partsStockExcelInfos = CommonDAL.DatatableConvertToList<PartsStockInfo>(dataTable).ToList();
            if (partsStockExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(partsStockExcelInfos.Select(d => d.PartNo).ToList());
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsDAL().GetListForInterfaceDataSync(partsStockExcelInfos.Select(d => d.PartNo).ToList());
            ///执行的SQL语句
            string sql = string.Empty;

            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var partsStockExcelInfo in partsStockExcelInfos)
            {
                ///
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == partsStockExcelInfo.PartNo);
                if (maintainPartsInfo == null)
                    throw new Exception("MC:0x00000224");///物料基础信息数据错误

                ///物料简称、物料中文描述、物料英文描述由基础数据中同步
                partsStockExcelInfo.PartCname = maintainPartsInfo.PartCname;
                partsStockExcelInfo.PartEname = maintainPartsInfo.PartEname;
                partsStockExcelInfo.PartNickname = maintainPartsInfo.PartNickname;
                ///
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == partsStockExcelInfo.PartNo
                && d.SupplierNum == partsStockExcelInfo.SupplierNum
                && d.WmNo == partsStockExcelInfo.WmNo
                && d.ZoneNo == partsStockExcelInfo.ZoneNo);
                if (partsStockInfo == null)
                {
                    if (string.IsNullOrEmpty(partsStockExcelInfo.PartNo)
                        || string.IsNullOrEmpty(partsStockExcelInfo.WmNo)
                        || string.IsNullOrEmpty(partsStockExcelInfo.ZoneNo))
                        throw new Exception("MC:0x00000223");///物料号、仓库、存储区为必填项

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<PartsStockInfo>(partsStockExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_PARTS_STOCK with(nolock) "
                        + "where [PART_NO] = N'" + partsStockExcelInfo.PartNo + "' "
                        + "and [WM_NO] = N'" + partsStockExcelInfo.WmNo + "' "
                        + "and [ZONE_NO] = N'" + partsStockExcelInfo.ZoneNo + "' "
                        + "and [SUPPLIER_NUM] = N'" + partsStockExcelInfo.SupplierNum + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_PARTS_STOCK] ("
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
                if (string.IsNullOrEmpty(partsStockExcelInfo.PartNo)
                        || string.IsNullOrEmpty(partsStockExcelInfo.WmNo)
                        || string.IsNullOrEmpty(partsStockExcelInfo.ZoneNo))
                    throw new Exception("MC:0x00000223");///物料号、仓库、存储区为必填项

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<PartsStockInfo>(partsStockExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_PARTS_STOCK] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + partsStockInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }

    }
}


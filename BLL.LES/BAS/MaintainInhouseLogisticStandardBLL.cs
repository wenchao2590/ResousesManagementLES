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
using System.Transactions;

namespace BLL.LES
{
    public class MaintainInhouseLogisticStandardBLL
    {
        #region Common
        MaintainInhouseLogisticStandardDAL dal = new MaintainInhouseLogisticStandardDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MaintainInhouseLogisticStandardInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<MaintainInhouseLogisticStandardInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainInhouseLogisticStandardInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(MaintainInhouseLogisticStandardInfo info)
        {
            ///若拉动零件类⑥对应到生产线代码④，则所选工段代码⑤及工位代码⑥范围为该生产线下对应数据
            ///且工段代码⑤与工位代码⑥之间联动
            ///否则工段代码⑤可选范围为全部
            ///在数据保存时将工厂代码②、车间代码③、生产线代码④同时写入数据库
            PartsBoxInfo partsBoxInfo = new PartsBoxDAL().GetInfoByPartBox(info.InhousePartClass);
            if (partsBoxInfo == null)
                throw new Exception("MC:0x00000225");///拉动零件类数据错误
            info.Plant = partsBoxInfo.Plant;
            info.Workshop = partsBoxInfo.Workshop;
            info.AssemblyLine = partsBoxInfo.AssemblyLine;
            ///当所选拉动零件类⑥的拉动方式⑤为10时间窗且其配置为库存当量拉动时，MIN⑯和MAX⑰允许维护大于零的数据，且MIN⑯小于MAX⑰
            if (int.Parse(info.InhouseSystemMode) == (int)PullModeConstants.Twd)///TODO:缺少库存当量拉动的判断，等TWD表结构
            {
                if (info.Min.GetValueOrDefault() > info.Max.GetValueOrDefault())
                    throw new Exception("MC:0x00000404");///MIN值必须小于MAX
            }
            ///当其被选为是时，需要校验层级拉动仓库⑩、层级拉动存储区⑪为必选项
            if (info.IsTriggerPull.GetValueOrDefault() == true)
            {
                if (string.IsNullOrEmpty(info.WmNo))
                    throw new Exception("MC:0x00000405");///层级拉动仓库不允许为空
                if (string.IsNullOrEmpty(info.ZoneNo))
                    throw new Exception("MC:0x00000406");///层级拉动存储区不允许为空
            }
            ///物料号①、拉动方式⑤、拉动零件类⑥、供应商代码⑦组合唯一
            int cnt = dal.GetCounts(string.Format(@"[PART_NO] = N'{0}' and [INHOUSE_SYSTEM_MODE] = N'{1}' and [INHOUSE_PART_CLASS]= N'{2}' and [SUPPLIER_NUM] = N'{3}'", info.PartNo, info.InhouseSystemMode, info.InhousePartClass, info.SupplierNum));
            if (cnt > 0)
                throw new Exception("MC:0x00000407");///物料号、拉动方式、拉动零件类、供应商代码组合不唯一

            ///相同目标仓库存储区，同物料号同供应商，即使跨拉动方式也需要唯一
            cnt = dal.GetCounts(string.Format(@"[PART_NO] = N'{0}' and [T_WM_NO] = N'{1}' and [T_ZONE_NO] = N'{2}' and [SUPPLIER_NUM] = N'{3}'", info.PartNo, info.SZoneNo, info.TZoneNo, info.SupplierNum));
            if (cnt > 0)
                throw new Exception("MC:0x00000408");///目标仓库、目标存储区、物料号、供应商组合不唯一

            #region 看板相关
            ///拉动方式为看板时看板环数必须大于等于一
            if (int.Parse(info.InhouseSystemMode) == (int)PullModeConstants.Kanban)
            {
                if (info.KanbanCircleCnt.GetValueOrDefault() < 1)
                    throw new Exception("Err_:MC:0x00000307");///看板环数必须大于等于一
            }
            #endregion

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
            ///获取实体对象
            MaintainInhouseLogisticStandardInfo info = dal.GetInfo(id);

            if (info.Status != (int)BasicDataStatusConstants.Created)
                throw new Exception("Err_:MC:0x00000415");/// 已创建状态才可进行删除

            #region 当拉动方式为看板时进行校验
            if (int.Parse(info.InhouseSystemMode) == (int)PullModeConstants.Kanban)
            {
                ///零件拉动信息对应看板卡全部处于已作废状态或没有看板卡数据
                int cnt = new KanbanCardDAL().GetCounts(string.Format("[STATUS] in (" + (int)BasicDataStatusConstants.Created + "," + (int)BasicDataStatusConstants.Enable + ") "///看板卡创建时的状态也应该是BASIC_DATA_STATUS对应的值
                    + "and [PART_NO] = N'{0}' and [PART_BOX_CODE] = N'{1}'", info.PartNo, info.InhousePartClass));
                if (cnt > 0)
                    throw new Exception("Err_:MC:0x00000309");///存在有效看板不可删除
            }
            #endregion

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
            string inhousePartClass = CommonBLL.GetFieldValue(fields, "INHOUSE_PART_CLASS");
            ///在数据保存时将工厂代码②、车间代码③、生产线代码④同时写入数据库
            PartsBoxInfo partsBoxInfo = new PartsBoxDAL().GetInfoByPartBox(inhousePartClass);
            if (partsBoxInfo == null)
                throw new Exception("MC:0x00000225");///拉动零件类数据错误

            int inhouseSystemMode = int.Parse(CommonBLL.GetFieldValue(fields, "INHOUSE_SYSTEM_MODE"));
            string min = CommonBLL.GetFieldValue(fields, "MIN");
            string max = CommonBLL.GetFieldValue(fields, "MAX");
            ///当所选拉动零件类⑥的拉动方式⑤为10时间窗且其配置为库存当量拉动时，MIN⑯和MAX⑰允许维护大于零的数据，且MIN⑯小于MAX⑰
            if (inhouseSystemMode == (int)PullModeConstants.Twd)///TODO:缺少库存当量拉动的判断，等TWD表结构
            {
                if (int.Parse(min) > int.Parse(max))
                    throw new Exception("MC:0x00000404");///MIN值必须小于MAX
            }
            ///当其被选为是时，需要校验层级拉动仓库⑩、层级拉动存储区⑪为必选项
            int isTriggerPull = int.Parse(CommonBLL.GetFieldValue(fields, "IS_TRIGGER_PULL"));
            string wmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            string zoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            if (isTriggerPull == 1)
            {
                if (string.IsNullOrEmpty(wmNo))
                    throw new Exception("MC:0x00000405");///层级拉动仓库不允许为空
                if (string.IsNullOrEmpty(zoneNo))
                    throw new Exception("MC:0x00000406");///层级拉动存储区不允许为空
            }

            string sql = string.Empty;
            #region 看板相关验证
            if (inhouseSystemMode == (int)PullModeConstants.Kanban)
            {
                ///拉动方式为看板时看板环数必须大于等于一
                int kanbanCircleCnt = int.Parse(CommonBLL.GetFieldValue(fields, "KANBAN_CIRCLE_CNT"));
                if (kanbanCircleCnt < 1)
                    throw new Exception("MC:0x00000307");///看板环数必须大于等于一
                string partNo = CommonBLL.GetFieldValue(fields, "PART_NO");
                ///看板环数①必须大于等于一且大于等于对应状态为已创建或已启用看板卡数据量合计
                List<KanbanCardInfo> kanbanCardList = new KanbanCardDAL().GetList(string.Format("[STATUS] in (" + (int)BasicDataStatusConstants.Created + "," + (int)BasicDataStatusConstants.Enable + ") "///看板卡创建时的状态也应该是BASIC_DATA_STATUS对应的值
                    + "and [PART_NO] = N'{0}' and [PART_BOX_CODE] = N'{1}'", partNo, inhousePartClass), string.Empty);
                if (kanbanCardList.Count > 0 && kanbanCircleCnt < kanbanCardList.Count)
                    throw new Exception("MC:0x00000308");///看板环数必须大于等于对应状态为已创建或已启用看板卡数据量合计

                ///同步更新对应看板卡的物料描述③⑦、供应商信息④⑧⑨、物料数量⑤⑩、包装器具信息⑥⑪
                if (kanbanCardList.Count > 0)
                {
                    string partCname = CommonBLL.GetFieldValue(fields, "PART_CNAME");
                    string supplierNum = CommonBLL.GetFieldValue(fields, "SUPPLIER_NUM");
                    string supplierName = new SupplierDAL().GetSupplierName(supplierNum);
                    int inboundPackage = int.Parse(CommonBLL.GetFieldValue(fields, "INBOUND_PACKAGE"));
                    string inboundPackageModel = CommonBLL.GetFieldValue(fields, "INBOUND_PACKAGE_MODEL");
                    sql = "update [LES].[TM_MPM_KANBAN_CARD] "
                    + "set [PART_NAME] = N'" + partCname + "',[SUPPLIER_NAME] = N'" + supplierName + "',[PART_QTY] = " + inboundPackage + ",[PACKAGE_CODE] = N'" + inboundPackageModel + "' "
                    + "where [ID] in (" + string.Join(",", kanbanCardList.Select(d => d.Id).ToArray()) + ");";
                }
            }
            #endregion
            ///
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                if (dal.UpdateInfo(fields + ",[PLANT] = N'" + partsBoxInfo.Plant + "',[WORKSHOP] = N'" + partsBoxInfo.Workshop + "',[ASSEMBLY_LINE] = N'" + partsBoxInfo.AssemblyLine + "'", id) == 0)
                    return false;
                trans.Complete();
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (maintainInhouseLogisticStandards.Count == 0)
                throw new Exception("MC:0x00000213");///物料拉动信息数据错误
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogistics = maintainInhouseLogisticStandards.Where(d => d.Status == (int)BasicDataStatusConstants.Created).ToList();

            ///物料拉动信息必须为已创建状态
            if (maintainInhouseLogisticStandards.Count != maintainInhouseLogistics.Count)
                throw new Exception("MC:0x00000214");///已创建状态的数据才能进行启用操作

            string sql = "update[LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] set " +
                                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + "," +
                                "[MODIFY_USER] = N'" + loginUser + "'," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";

            return CommonDAL.ExecuteNonQueryBySql(sql);

        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (maintainInhouseLogisticStandards.Count == 0)
                throw new Exception("MC:0x00000213");///物料拉动信息数据错误

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogistics = maintainInhouseLogisticStandards.Where(d => d.Status == (int)BasicDataStatusConstants.Enable).ToList();

            ///物料拉动信息必须为已启用状态
            if (maintainInhouseLogisticStandards.Count != maintainInhouseLogistics.Count)
                throw new Exception("MC:0x00000730");///物料拉动信息必须为已启用状态

            ///零件拉动信息对应看板卡全部处于已作废状态⑫或没有看板卡数据
            int cnt = new KanbanCardDAL().GetCounts("" +
                "[PART_BOX_CODE]  IN ('" + string.Join("','", maintainInhouseLogisticStandards.Select(w => w.InhousePartClass).ToArray()) + "') and" +
                "[PART_NO] IN ('" + string.Join("','", maintainInhouseLogisticStandards.Select(w => w.PartNo).ToArray()) + "') and" +
                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000217");///启用看板卡数量大于零时不能作废

            ///更新已创建状态的看板卡数据到已作废
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] set " +
                                "[STATUS] = " + (int)BasicDataStatusConstants.Disabled + " where " +
                                "[PART_BOX_CODE] IN ('" + string.Join("','", maintainInhouseLogisticStandards.Select(w => w.InhousePartClass).ToArray()) + "') and " +
                                "[PART_NO] IN ('" + string.Join("','", maintainInhouseLogisticStandards.Select(w => w.PartNo).ToArray()) + "') and " +
                                "[STATUS] = " + (int)BasicDataStatusConstants.Created + ";" +
                                "update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] set " +
                                "[STATUS] = " + (int)BasicDataStatusConstants.Disabled + "," +
                                "[MODIFY_USER] = N'" + loginUser + "'," +
                                "[MODIFY_DATE] = GETDATE() where " +
                                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";

            using (TransactionScope trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }

        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardExcelInfos = CommonDAL.DatatableConvertToList<MaintainInhouseLogisticStandardInfo>(dataTable).ToList();
            if (maintainInhouseLogisticStandardExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            ///获取业务表中要变更的数据集合,准备对比
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetListForInterfaceDataSync(maintainInhouseLogisticStandardExcelInfos.Select(d => d.PartNo).ToList());
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsDAL().GetListForInterfaceDataSync(maintainInhouseLogisticStandardExcelInfos.Select(d => d.PartNo).ToList());
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(maintainInhouseLogisticStandardExcelInfos.Select(d => d.PartNo).ToList());
            List<PartsBoxInfo> partsBoxInfos = new PartsBoxDAL().GetList("", string.Empty);
            ///执行的SQL语句
            string sql = string.Empty;
            List<MaintainInhouseLogisticStandardInfo> standardInfos = new List<MaintainInhouseLogisticStandardInfo>();
            fieldNames.Add("SWmNo", "S_WM_NO");
            fieldNames.Add("SZoneNo", "S_ZONE_NO");
            fieldNames.Add("TWmNo", "T_WM_NO");
            fieldNames.Add("TZoneNo", "T_ZONE_NO");
            fieldNames.Add("Plant", "PLANT");
            fieldNames.Add("Workshop", "WORKSHOP");
            fieldNames.Add("AssemblyLine", "ASSEMBLY_LINE");
            List<string> fields = new List<string>(fieldNames.Keys);
            ///逐条处理中间表数据
            foreach (var maintainInhouseLogisticStandardExcelInfo in maintainInhouseLogisticStandardExcelInfos)
            {
                ///
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo);
                if (maintainPartsInfo == null)
                    throw new Exception("MC:0x00000224");///物料基础信息数据错误

                maintainInhouseLogisticStandardExcelInfo.Status = (int)BasicDataStatusConstants.Created;
                ///物料简称、物料中文描述、物料英文描述由基础数据中同步
                maintainInhouseLogisticStandardExcelInfo.PartCname = maintainPartsInfo.PartCname;
                maintainInhouseLogisticStandardExcelInfo.PartEname = maintainPartsInfo.PartEname;
                maintainInhouseLogisticStandardExcelInfo.PartNickname = maintainPartsInfo.PartNickname;
                ///
                PartsBoxInfo partsBoxInfo = partsBoxInfos.FirstOrDefault(d => d.PullMode.ToString() == maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode
                && d.BoxParts == maintainInhouseLogisticStandardExcelInfo.InhousePartClass);
                if (partsBoxInfo == null)
                    throw new Exception("MC:0x00000225");///拉动零件类数据错误
                maintainInhouseLogisticStandardExcelInfo.SWmNo = partsBoxInfo.SWmNo;
                maintainInhouseLogisticStandardExcelInfo.SZoneNo = partsBoxInfo.SZoneNo;
                maintainInhouseLogisticStandardExcelInfo.TWmNo = partsBoxInfo.TWmNo;
                maintainInhouseLogisticStandardExcelInfo.TZoneNo = partsBoxInfo.TZoneNo;
                maintainInhouseLogisticStandardExcelInfo.Plant = partsBoxInfo.Plant;
                maintainInhouseLogisticStandardExcelInfo.Workshop = partsBoxInfo.Workshop;
                maintainInhouseLogisticStandardExcelInfo.AssemblyLine = partsBoxInfo.AssemblyLine;
                ///目标地点
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                && d.WmNo == maintainInhouseLogisticStandardExcelInfo.TWmNo
                && d.ZoneNo == maintainInhouseLogisticStandardExcelInfo.TZoneNo);
                ///无维护先后要求
                if (partsStockInfo != null)
                {
                    maintainInhouseLogisticStandardExcelInfo.InboundPackageModel = partsStockInfo.InboundPackageModel;
                    maintainInhouseLogisticStandardExcelInfo.InboundPackage = partsStockInfo.InboundPackage;
                }
                ///当所选拉动零件类⑥的拉动方式⑤为10时间窗且其配置为库存当量拉动时，MIN⑯和MAX⑰允许维护大于零的数据，且MIN⑯小于MAX⑰
                if (int.Parse(maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode) == (int)PullModeConstants.Twd)///TODO:缺少库存当量拉动的判断，等TWD表结构
                {
                    if (maintainInhouseLogisticStandardExcelInfo.Min.GetValueOrDefault() > maintainInhouseLogisticStandardExcelInfo.Max.GetValueOrDefault())
                        throw new Exception("MC:0x00000404");///MIN值必须小于MAX
                }
                if (maintainInhouseLogisticStandardExcelInfo.IsTriggerPull.GetValueOrDefault() == true)
                {
                    if (string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.WmNo))
                        throw new Exception("MC:0x00000405");///层级拉动仓库不允许为空
                    if (string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.ZoneNo))
                        throw new Exception("MC:0x00000406");///层级拉动存储区不允许为空
                }




                ///
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d =>
                  d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                  && d.InhouseSystemMode == maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode
                  && d.InhousePartClass == maintainInhouseLogisticStandardExcelInfo.InhousePartClass);
                if (maintainInhouseLogisticStandardInfo == null)
                {
                    if (string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.PartNo)
                        || string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode)
                        || string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.InhousePartClass))
                        throw new Exception("MC:0x00000226");///物料号、拉动模式、零件类为必填项

                    ///相同目标仓库存储区，同物料号同供应商，即使跨拉动方式也需要唯一
                    int cnt = maintainInhouseLogisticStandardInfos.Where(d =>
                                                                      d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                                                                      && d.TWmNo == maintainInhouseLogisticStandardExcelInfo.TWmNo
                                                                      && d.TZoneNo == maintainInhouseLogisticStandardExcelInfo.TZoneNo
                                                                      && d.SupplierNum == maintainInhouseLogisticStandardExcelInfo.SupplierNum).Count();
                    if (cnt > 0)
                        throw new Exception("MC:0x00000408");///物料号、拉动方式、拉动零件类、供应商代码组合不唯一

                    ///物料号①、拉动方式⑤、拉动零件类⑥、供应商代码⑦组合唯一
                    cnt = maintainInhouseLogisticStandardInfos.Where(d =>
                                                                     d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                                                                     && d.InhouseSystemMode == maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode
                                                                     && d.InhousePartClass == maintainInhouseLogisticStandardExcelInfo.InhousePartClass
                                                                     && d.SupplierNum == maintainInhouseLogisticStandardExcelInfo.SupplierNum).Count();
                    if (cnt > 0)
                        throw new Exception("MC:0x00000407");///物料号、拉动方式、拉动零件类、供应商代码组合不唯一

                    ///字段
                    string insertFieldString = string.Empty;
                    ///值
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<MaintainInhouseLogisticStandardInfo>(maintainInhouseLogisticStandardExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///数据格式不符合导入规范
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD with(nolock) "
                        + "where [PART_NO] = N'" + maintainInhouseLogisticStandardExcelInfo.PartNo + "' and [INHOUSE_SYSTEM_MODE] = N'" + maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode + "' and [INHOUSE_PART_CLASS] = N'" + maintainInhouseLogisticStandardExcelInfo.InhousePartClass + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] ("
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
                    maintainInhouseLogisticStandardInfos.Add(maintainInhouseLogisticStandardExcelInfo);
                    continue;
                }
                ///
                if (string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.PartNo)
                         || string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode)
                         || string.IsNullOrEmpty(maintainInhouseLogisticStandardExcelInfo.InhousePartClass))
                    throw new Exception("MC:0x00000226");///物料号、拉动模式、零件类为必填项

                ///相同目标仓库存储区，同物料号同供应商，即使跨拉动方式也需要唯一
                int count = maintainInhouseLogisticStandardInfos.Where(d =>
                                                                  d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                                                                  && d.TWmNo == maintainInhouseLogisticStandardExcelInfo.TWmNo
                                                                  && d.TZoneNo == maintainInhouseLogisticStandardExcelInfo.TZoneNo
                                                                  && d.SupplierNum == maintainInhouseLogisticStandardExcelInfo.SupplierNum
                                                                  && d.Id != maintainInhouseLogisticStandardInfo.Id).Count();
                if (count > 0)
                    throw new Exception("MC:0x00000408");///物料号、拉动方式、拉动零件类、供应商代码组合不唯一

                ///物料号①、拉动方式⑤、拉动零件类⑥、供应商代码⑦组合唯一
                count = maintainInhouseLogisticStandardInfos.Where(d =>
                                                                 d.PartNo == maintainInhouseLogisticStandardExcelInfo.PartNo
                                                                 && d.InhouseSystemMode == maintainInhouseLogisticStandardExcelInfo.InhouseSystemMode
                                                                 && d.InhousePartClass == maintainInhouseLogisticStandardExcelInfo.InhousePartClass
                                                                 && d.SupplierNum == maintainInhouseLogisticStandardExcelInfo.SupplierNum
                                                                 && d.Id != maintainInhouseLogisticStandardInfo.Id).Count();
                if (count > 0)
                    throw new Exception("MC:0x00000407");///物料号、拉动方式、拉动零件类、供应商代码组合不唯一

                ///值
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<MaintainInhouseLogisticStandardInfo>(maintainInhouseLogisticStandardExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///数据格式不符合导入规范

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + maintainInhouseLogisticStandardInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}


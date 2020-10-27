namespace UI.WEB.COMMON
{
    using BLL.LES;
    using BLL.SYS;
    using DM.SYS;
    using DM.LES;
    using Infrustructure.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using Infrustructure.Utilities;
    using System.IO;
    public class LESHandler
    {
        #region 供货计划
        /// <summary>
        /// 供货计划字段获取
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetSupplyPlanEntityFields(HttpContext context)
        {
            ///实体名,可能是FORM的实体名
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///表名
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(tableName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///FILTER
            string strWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(strWhere))
                strWhere = string.Empty;
            if (strWhere.Trim().ToUpper() == "AND")
                strWhere = string.Empty;

            ///EntityFieldInfo  获取实体字段在页面显示的信息
            List<EntityFieldInfo> entityFieldList = GetSupplyPlanFields(strWhere, entityName);
            ///
            string resultEntityFields = JsonHelper.ToJson(entityFieldList);
            ///EntityInfo
            EntityInfo entityinfo = new EntityBLL().GetInfo(entityName, tableName);
            string resultEntityInfo = JsonHelper.ToJson(entityinfo);
            return @"{""entityfieldform"":" + resultEntityFields
                + @",""entityinfo"":" + resultEntityInfo + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereText"></param>
        /// <returns></returns>
        private List<EntityFieldInfo> GetSupplyPlanFields(string whereText, string entityName)
        {
            ///EntityFieldInfo  获取实体字段在页面显示的信息
            List<EntityFieldInfo> entityFieldList = new EntityBLL().GetGridFieldByEntityName(entityName, "TT_ATP_SUPPLY_PLAN");
            ///追加动态日期列
            ///供货计划默认展示最大天数，默认14
            string supplyPlanDefaultShowMaxDays = new ConfigBLL().GetValueByCode("SUPPLY_PLAN_DEFAULT_SHOW_MAX_DAYS");
            int intSupplyPlanDefaultShowMaxDays = GetSupplyPlanShowDays(supplyPlanDefaultShowMaxDays, whereText, out DateTime seedDate);

            List<string> dateColumns = GetValidDates(intSupplyPlanDefaultShowMaxDays, seedDate);
            ///种子顺序
            int seedDisplayOrder = 70;
            for (int i = 0; i < intSupplyPlanDefaultShowMaxDays; i++)
            {
                #region EntityFieldInfo
                EntityFieldInfo entityFieldInfo = new EntityFieldInfo();
                entityFieldInfo.FieldName = "D" + seedDate.ToString("yyyyMMdd");
                entityFieldInfo.TableFieldName = "D" + seedDate.ToString("yyyyMMdd");
                entityFieldInfo.DisplayNameCn = seedDate.ToString("MM月dd日");
                entityFieldInfo.DisplayNameEn = seedDate.ToString("yyyyMMdd");
                entityFieldInfo.DisplayOrder = seedDisplayOrder;
                entityFieldInfo.DataType = (int)DataTypeConstants.DECIMAL;
                entityFieldInfo.ControlType = (int)ControlTypeConstants.NUMBERBOX;
                entityFieldInfo.DataLength = 18;
                entityFieldInfo.Precision = 0;
                entityFieldInfo.DefaultValue = string.Empty;
                entityFieldInfo.Nullenable = null;
                entityFieldInfo.Regex = string.Empty;
                entityFieldInfo.ErrorMsg = string.Empty;
                entityFieldInfo.MinValue = null;
                entityFieldInfo.MaxValue = null;
                entityFieldInfo.Editable = false;
                entityFieldInfo.EditDisplayWidth = string.Empty;
                entityFieldInfo.Listable = true;
                entityFieldInfo.ListDisplayWidth = "80";
                entityFieldInfo.Extend1 = string.Empty;
                entityFieldInfo.Extend2 = string.Empty;
                entityFieldInfo.Extend3 = string.Empty;
                entityFieldInfo.EditReadonly = (int)ReadonlyTypeConstants.NOTREADONLY;
                entityFieldInfo.TabTitleCode = string.Empty;
                entityFieldInfo.Sortable = false;
                entityFieldInfo.ExportExcelFlag = true;
                entityFieldInfo.ExportExcelOrder = seedDisplayOrder;
                entityFieldInfo.TooltipHelperCn = string.Empty;
                entityFieldInfo.TooltipHelperEn = string.Empty;
                seedDisplayOrder += 10;
                if (dateColumns.Contains(seedDate.ToString("yyyyMMdd")))
                    entityFieldList.Add(entityFieldInfo);
                seedDate = seedDate.AddDays(1);
                #endregion
            }
            return entityFieldList;
        }
        /// <summary>
        /// 根据检索条件获取显示天数和起始日期
        /// </summary>E:\LES\CODE\UI.WEB\TEMPLATE\
        /// <param name="whereText"></param>
        /// <param name="seedDate"></param>
        /// <returns></returns>
        private int GetSupplyPlanShowDays(string supplyPlanDefaultShowMaxDays, string whereText, out DateTime seedDate)
        {
            if (!int.TryParse(supplyPlanDefaultShowMaxDays, out int intSupplyPlanDefaultShowMaxDays))
                throw new Exception("MC:1x00000031");///系统配置错误

            ///日期检索条件
            string startDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "CREATE_DATE");
            string endDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "MODIFY_DATE");

            ///种子日期
            seedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ///如果用户开始日期作为种子日期
            if (!string.IsNullOrEmpty(startDate))
            {
                seedDate = DateTime.Parse(startDate);
                if (!string.IsNullOrEmpty(endDate))
                {
                    TimeSpan ts = new TimeSpan();
                    ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
                    if (ts.Days < 0)
                        throw new Exception("MC:0x00000044");///开始日期不能小于结束日期
                    if (ts.Days > intSupplyPlanDefaultShowMaxDays)
                        throw new Exception("MC:0x00000054");///时间跨度不能大于系统配置，请联系管理员
                    if (intSupplyPlanDefaultShowMaxDays > ts.Days)
                        intSupplyPlanDefaultShowMaxDays = ts.Days + 1;
                }
            }
            else
            {
                ///用户只填写了结束日期未填写开始时间
                ///则需要从后先前推导X天
                if (!string.IsNullOrEmpty(endDate))
                    seedDate = DateTime.Parse(endDate).AddDays(0 - intSupplyPlanDefaultShowMaxDays);
            }
            return intSupplyPlanDefaultShowMaxDays;
        }
        /// <summary>
        /// 获取有效的时间
        /// </summary>
        /// <param name="intSupplyPlanDefaultShowMaxDays"></param>
        /// <param name="seedDate"></param>
        /// <returns></returns>
        private List<string> GetValidDates(int intSupplyPlanDefaultShowMaxDays, DateTime seedDate)
        {
            List<string> dateColumns = new List<string>();
            for (int i = 0; i < intSupplyPlanDefaultShowMaxDays; i++)
            {
                dateColumns.Add(seedDate.ToString("yyyyMMdd"));
                seedDate = seedDate.AddDays(1);
            }
            return new SupplyPlanBLL().GetDatabaseExistsDateColumns(dateColumns);
        }
        /// <summary>
        /// 获取供货计划数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetSupplyPlanPageList(HttpContext context)
        {
            ///page
            int pageIndex = 1;
            string indexPage = context.Request["page"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(indexPage))
                pageIndex = int.Parse(indexPage);
            ///rows
            int maxRow = int.MaxValue;
            string rowMax = context.Request["rows"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(rowMax))
                maxRow = int.Parse(rowMax);
            ///sort
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"]))
            {
                orderText += context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
            }
            ///order
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
                orderText += " " + context.Request["order"];
            ///FILTER
            string strWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(strWhere))
                strWhere = string.Empty;
            if (strWhere.Trim().ToUpper() == "AND")
                strWhere = string.Empty;
            ///ENTITY_AUTH
            string entityAuth = context.Request["ENTITY_AUTH"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(entityAuth))
                strWhere += entityAuth.Replace("^", "'");


            DataTable dt = GetSupplyPlanData(strWhere, orderText, pageIndex, maxRow, out int dataTotal);
            string result = JsonHelper.DataTableToJson(dt);
            return @"{""rows"":" + result + @",""total"":" + dataTotal + @"}";
        }
        /// <summary>
        /// 获取供货计划数据集合
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <param name="pageIndex"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        private DataTable GetSupplyPlanData(string whereText, string orderText, int pageIndex, int maxRow, out int dataTotal)
        {
            ///追加动态日期列
            ///供货计划默认展示最大天数，默认14
            string supplyPlanDefaultShowMaxDays = new ConfigBLL().GetValueByCode("SUPPLY_PLAN_DEFAULT_SHOW_MAX_DAYS");
            int intSupplyPlanDefaultShowMaxDays = GetSupplyPlanShowDays(supplyPlanDefaultShowMaxDays, whereText, out DateTime seedDate);

            ///日期检索条件
            string startDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "CREATE_DATE");
            string endDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "MODIFY_DATE");

            List<string> dateColumns = GetValidDates(intSupplyPlanDefaultShowMaxDays, seedDate);
            ///WHERE条件处理
            whereText = whereText.Replace("^", "'");

            if (!string.IsNullOrEmpty(startDate))
                whereText = BLL.LES.CommonBLL.ClearWhereField(whereText, "CREATE_DATE", (int)DataTypeConstants.DATE, (int)SearchTypeConstants.GREATEREQUAL);
            if (!string.IsNullOrEmpty(endDate))
                whereText = BLL.LES.CommonBLL.ClearWhereField(whereText, "MODIFY_DATE", (int)DataTypeConstants.DATE, (int)SearchTypeConstants.LESSEQUAL);
            ///
            return new SupplyPlanBLL().GetSupplyPlanListByPage(whereText, orderText, pageIndex, maxRow, dateColumns, out dataTotal);
        }
        /// <summary>
        /// 供货计划相关EXCEL导出
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string SupplyPlanExportExcel(HttpContext context)
        {
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///fileName
            string fileName = entityName + DateTime.Now.Ticks + ".xlsx";
            ///filePath
            string filePath = HttpContext.Current.Server.MapPath("/TEMP/EXPORTFILES");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            ///sort
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"]))
            {
                orderText += context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
            }
            ///order
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
                orderText += " " + context.Request["order"];
            ///FILTER
            string textWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(textWhere))
                textWhere = string.Empty;
            if (textWhere.Trim().ToUpper() == "AND")
                textWhere = string.Empty;
            ///创建标题行
            List<EntityFieldInfo> entityFieldInfos = new List<EntityFieldInfo>();
            switch (entityName)
            {
                ///供货计划
                case "SupplyPlan":
                    entityFieldInfos = GetSupplyPlanFields(textWhere, entityName)
  .OrderBy(d => d.DisplayOrder.GetValueOrDefault())
  .OrderBy(d => d.ExportExcelOrder.GetValueOrDefault()).ToList(); break;
                ///缺件检查
                case "LackOfInspection":
                    entityFieldInfos = GetSupplyPlanCheckFields(textWhere, entityName)
.OrderBy(d => d.DisplayOrder.GetValueOrDefault())
.OrderBy(d => d.ExportExcelOrder.GetValueOrDefault()).ToList(); break;
            }

            if (entityFieldInfos.Count == 0)
                throw new Exception("MC:1x00000033");///未设置导出内容

            ///CODE_ITEM下拉菜单集合
            Dictionary<string, Dictionary<string, string>> dicDropdowns = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> columnNames = SYSHandler.GetExcelColumnNames(entityFieldInfos, ref dicDropdowns);
            ///page
            int pageIndex = 1;
            ///rows
            int maxRow = int.MaxValue;

            int dataTotal;
            ///获取数据
            DataTable dataTable = new DataTable();
            switch (entityName)
            {
                ///供货计划
                case "SupplyPlan": dataTable = GetSupplyPlanData(textWhere, orderText, pageIndex, maxRow, out dataTotal); break;
                ///缺件检查
                case "LackOfInspection": dataTable = GetLackOfInspectionData(textWhere, orderText, pageIndex, maxRow, out dataTotal); break;
            }

            if (dataTable.Rows.Count > 65535)
                throw new Exception("MC:1x00000036");///导出数据不能多于65535条

            ///NPOI
            NpoiHelper.TableToExcel(dataTable, columnNames, dicDropdowns, entityName, filePath + @"\" + fileName);
            ///
            return "../TEMP/EXPORTFILES/" + fileName;
        }
        #endregion

        #region 缺件检查
        /// <summary>
        /// 获取缺件检查页面配置信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetLackOfInspectionEntityFields(HttpContext context)
        {
            ///实体名,可能是FORM的实体名
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///表名
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(tableName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///FILTER
            string strWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(strWhere))
                strWhere = string.Empty;
            if (strWhere.Trim().ToUpper() == "AND")
                strWhere = string.Empty;

            ///EntityFieldInfo  获取实体字段在页面显示的信息
            List<EntityFieldInfo> entityFieldList = GetSupplyPlanCheckFields(strWhere, entityName);

            ///
            string resultEntityFields = JsonHelper.ToJson(entityFieldList);
            ///EntityInfo
            EntityInfo entityinfo = new EntityBLL().GetInfo(entityName, tableName);
            string resultEntityInfo = JsonHelper.ToJson(entityinfo);
            return @"{""entityfieldform"":" + resultEntityFields
                + @",""entityinfo"":" + resultEntityInfo + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        private List<EntityFieldInfo> GetSupplyPlanCheckFields(string whereText, string entityName)
        {
            ///EntityFieldInfo  获取实体字段在页面显示的信息
            List<EntityFieldInfo> entityFieldList = new EntityBLL().GetGridFieldByEntityName(entityName, "TT_ATP_SUPPLY_PLAN");
            ///追加动态日期列
            ///缺件检查显示供货计划天数
            string lackOfInspectionDisplaySupplyPlanDays = new ConfigBLL().GetValueByCode("LACK_OF_INSPECTION_DISPLAY_SUPPLY_PLAN_DAYS");
            int intLackOfInspectionDisplaySupplyPlanDays = GetSupplyPlanShowDays(lackOfInspectionDisplaySupplyPlanDays, whereText, out DateTime seedDate);

            List<string> dateColumns = GetValidDates(intLackOfInspectionDisplaySupplyPlanDays, seedDate);
            ///生产计划锁定临近天数
            string productionPlanLockedDays = new ConfigBLL().GetValueByCode("PRODUCTION_PLAN_LOCKED_DAYS");
            if (!int.TryParse(productionPlanLockedDays, out int intProductionPlanLockedDays))
                throw new Exception("MC:1x00000031");///系统配置错误
            if (intProductionPlanLockedDays > intLackOfInspectionDisplaySupplyPlanDays)
                throw new Exception("MC:1x00000031");///系统配置错误

            ///种子顺序
            int seedDisplayOrder = 70;

            #region RDC
            EntityFieldInfo rdcEntityFieldInfo = new EntityFieldInfo();
            rdcEntityFieldInfo.FieldName = "RDC";
            rdcEntityFieldInfo.TableFieldName = "RDC";
            rdcEntityFieldInfo.DisplayNameCn = "RDC库存";
            rdcEntityFieldInfo.DisplayNameEn = "RDC Inventory";
            rdcEntityFieldInfo.DisplayOrder = seedDisplayOrder;
            rdcEntityFieldInfo.DataType = (int)DataTypeConstants.INT;
            rdcEntityFieldInfo.ControlType = (int)ControlTypeConstants.NUMBERBOX;
            rdcEntityFieldInfo.DataLength = 18;
            rdcEntityFieldInfo.Precision = 0;
            rdcEntityFieldInfo.DefaultValue = string.Empty;
            rdcEntityFieldInfo.Nullenable = null;
            rdcEntityFieldInfo.Regex = string.Empty;
            rdcEntityFieldInfo.ErrorMsg = string.Empty;
            rdcEntityFieldInfo.MinValue = null;
            rdcEntityFieldInfo.MaxValue = null;
            rdcEntityFieldInfo.Editable = false;
            rdcEntityFieldInfo.EditDisplayWidth = string.Empty;
            rdcEntityFieldInfo.Listable = true;
            rdcEntityFieldInfo.ListDisplayWidth = "65";
            rdcEntityFieldInfo.Extend1 = string.Empty;
            rdcEntityFieldInfo.Extend2 = string.Empty;
            rdcEntityFieldInfo.Extend3 = string.Empty;
            rdcEntityFieldInfo.EditReadonly = (int)ReadonlyTypeConstants.NOTREADONLY;
            rdcEntityFieldInfo.TabTitleCode = string.Empty;
            rdcEntityFieldInfo.Sortable = false;
            rdcEntityFieldInfo.ExportExcelFlag = true;
            rdcEntityFieldInfo.ExportExcelOrder = seedDisplayOrder;
            rdcEntityFieldInfo.TooltipHelperCn = string.Empty;
            rdcEntityFieldInfo.TooltipHelperEn = string.Empty;
            seedDisplayOrder += 10;
            entityFieldList.Add(rdcEntityFieldInfo);
            #endregion

            #region VMI
            EntityFieldInfo vmiEntityFieldInfo = new EntityFieldInfo();
            vmiEntityFieldInfo.FieldName = "VMI";
            vmiEntityFieldInfo.TableFieldName = "VMI";
            vmiEntityFieldInfo.DisplayNameCn = "VMI库存";
            vmiEntityFieldInfo.DisplayNameEn = "VMI Inventory";
            vmiEntityFieldInfo.DisplayOrder = seedDisplayOrder;
            vmiEntityFieldInfo.DataType = (int)DataTypeConstants.DECIMAL;
            vmiEntityFieldInfo.ControlType = (int)ControlTypeConstants.NUMBERBOX;
            vmiEntityFieldInfo.DataLength = 18;
            vmiEntityFieldInfo.Precision = 0;
            vmiEntityFieldInfo.DefaultValue = string.Empty;
            vmiEntityFieldInfo.Nullenable = null;
            vmiEntityFieldInfo.Regex = string.Empty;
            vmiEntityFieldInfo.ErrorMsg = string.Empty;
            vmiEntityFieldInfo.MinValue = null;
            vmiEntityFieldInfo.MaxValue = null;
            vmiEntityFieldInfo.Editable = false;
            vmiEntityFieldInfo.EditDisplayWidth = string.Empty;
            vmiEntityFieldInfo.Listable = true;
            vmiEntityFieldInfo.ListDisplayWidth = "65";
            vmiEntityFieldInfo.Extend1 = string.Empty;
            vmiEntityFieldInfo.Extend2 = string.Empty;
            vmiEntityFieldInfo.Extend3 = string.Empty;
            vmiEntityFieldInfo.EditReadonly = (int)ReadonlyTypeConstants.NOTREADONLY;
            vmiEntityFieldInfo.TabTitleCode = string.Empty;
            vmiEntityFieldInfo.Sortable = false;
            vmiEntityFieldInfo.ExportExcelFlag = true;
            vmiEntityFieldInfo.ExportExcelOrder = seedDisplayOrder;
            vmiEntityFieldInfo.TooltipHelperCn = string.Empty;
            vmiEntityFieldInfo.TooltipHelperEn = string.Empty;
            seedDisplayOrder += 10;
            entityFieldList.Add(vmiEntityFieldInfo);
            #endregion

            ///累计日期字符串
            string accumulativeDate = string.Empty;
            for (int i = 0; i < intLackOfInspectionDisplaySupplyPlanDays; i++)
            {
                EntityFieldInfo entityFieldInfo = new EntityFieldInfo();
                entityFieldInfo.FieldName = "D" + seedDate.ToString("yyyyMMdd");
                entityFieldInfo.TableFieldName = "D" + seedDate.ToString("yyyyMMdd");
                entityFieldInfo.DisplayNameCn = seedDate.ToString("MM月dd日");
                entityFieldInfo.DisplayNameEn = seedDate.ToString("yyyyMMdd");
                entityFieldInfo.DisplayOrder = seedDisplayOrder;
                entityFieldInfo.DataType = (int)DataTypeConstants.DECIMAL;
                entityFieldInfo.ControlType = (int)ControlTypeConstants.NUMBERBOX;
                entityFieldInfo.DataLength = 18;
                entityFieldInfo.Precision = 0;
                entityFieldInfo.DefaultValue = string.Empty;
                entityFieldInfo.Nullenable = null;
                entityFieldInfo.Regex = string.Empty;
                entityFieldInfo.ErrorMsg = string.Empty;
                entityFieldInfo.MinValue = null;
                entityFieldInfo.MaxValue = null;
                entityFieldInfo.Editable = false;
                entityFieldInfo.EditDisplayWidth = string.Empty;
                entityFieldInfo.Listable = true;
                entityFieldInfo.ListDisplayWidth = "65";
                entityFieldInfo.Extend1 = string.Empty;
                entityFieldInfo.Extend2 = string.Empty;
                entityFieldInfo.Extend3 = string.Empty;
                entityFieldInfo.EditReadonly = (int)ReadonlyTypeConstants.NOTREADONLY;
                entityFieldInfo.TabTitleCode = string.Empty;
                entityFieldInfo.Sortable = false;
                entityFieldInfo.ExportExcelFlag = true;
                entityFieldInfo.ExportExcelOrder = seedDisplayOrder;
                entityFieldInfo.TooltipHelperCn = string.Empty;
                entityFieldInfo.TooltipHelperEn = string.Empty;
                seedDisplayOrder += 10;
                seedDate = seedDate.AddDays(1);
                ///超过生产计划锁定临近天数时不再做数量差异判定
                if (i >= intProductionPlanLockedDays)
                {
                    entityFieldList.Add(entityFieldInfo);
                    continue;
                }
                ///从第二天起需要累加之前日期的数量
                if (i > 0)
                    accumulativeDate += "+row.D";
                accumulativeDate += seedDate.AddDays(-1).ToString("yyyyMMdd");
                entityFieldInfo.Extend3 = "styler^样式逻辑:(Math.round(row.RDC)+Math.round(row.VMI) >= (row.D" + accumulativeDate + ")? row.D" + seedDate.AddDays(-1).ToString("yyyyMMdd") + " : -1).toString(),背景颜色:#00FF00,字体颜色:black";
                entityFieldList.Add(entityFieldInfo);
            }
            return entityFieldList;
        }
        /// <summary>
        /// 缺件检查数据集合
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetLackOfInspectionPageList(HttpContext context)
        {
            ///page
            int pageIndex = 1;
            string indexPage = context.Request["page"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(indexPage))
                pageIndex = int.Parse(indexPage);
            ///rows
            int maxRow = int.MaxValue;
            string rowMax = context.Request["rows"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(rowMax))
                maxRow = int.Parse(rowMax);
            ///sort
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"]))
            {
                orderText += context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
            }
            ///order
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
                orderText += " " + context.Request["order"];
            ///FILTER
            string strWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(strWhere))
                strWhere = string.Empty;
            if (strWhere.Trim().ToUpper() == "AND")
                strWhere = string.Empty;

            DataTable dt = GetLackOfInspectionData(strWhere, orderText, pageIndex, maxRow, out int dataTotal);
            string result = JsonHelper.DataTableToJson(dt);
            return @"{""rows"":" + result + @",""total"":" + dataTotal + @"}";
        }
        /// <summary>
        /// 获取缺件检查数据集合
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <param name="pageIndex"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        private DataTable GetLackOfInspectionData(string whereText, string orderText, int pageIndex, int maxRow, out int dataTotal)
        {
            ///追加动态日期列
            ///缺件检查显示供货计划天数，默认14
            string lackOfInspectionDisplaySupplyPlanDays = new ConfigBLL().GetValueByCode("LACK_OF_INSPECTION_DISPLAY_SUPPLY_PLAN_DAYS");
            int intLackOfInspectionDisplaySupplyPlanDays = GetSupplyPlanShowDays(lackOfInspectionDisplaySupplyPlanDays, whereText, out DateTime seedDate);

            ///日期检索条件
            string startDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "CREATE_DATE");
            string endDate = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "MODIFY_DATE");

            List<string> dateColumns = GetValidDates(intLackOfInspectionDisplaySupplyPlanDays, seedDate);
            ///WHERE条件处理
            whereText = whereText.Replace("^", "'");

            if (!string.IsNullOrEmpty(startDate))
                whereText = BLL.LES.CommonBLL.ClearWhereField(whereText, "CREATE_DATE", (int)DataTypeConstants.DATE, (int)SearchTypeConstants.GREATEREQUAL);
            if (!string.IsNullOrEmpty(endDate))
                whereText = BLL.LES.CommonBLL.ClearWhereField(whereText, "MODIFY_DATE", (int)DataTypeConstants.DATE, (int)SearchTypeConstants.LESSEQUAL);
            ///
            return new SupplyPlanBLL().GetLackOfInspectionListByPage(whereText, orderText, pageIndex, maxRow, dateColumns, out dataTotal);
        }
        /// <summary>
        /// 创建缺件表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CreateLackOfMaterial(HttpContext context)
        {
            ///条件
            string whereText = context.Request["FILTER"];
            ///日期检索条件
            ///缺件检查显示供货计划天数
            string lackOfInspectionDisplaySupplyPlanDays = new ConfigBLL().GetValueByCode("LACK_OF_INSPECTION_DISPLAY_SUPPLY_PLAN_DAYS");
            int intLackOfInspectionDisplaySupplyPlanDays = GetSupplyPlanShowDays(lackOfInspectionDisplaySupplyPlanDays, whereText, out DateTime seedDate);

            List<string> dateColumns = GetValidDates(intLackOfInspectionDisplaySupplyPlanDays, seedDate);
            ///生产计划锁定临近天数
            string productionPlanLockedDays = new ConfigBLL().GetValueByCode("PRODUCTION_PLAN_LOCKED_DAYS");
            if (!int.TryParse(productionPlanLockedDays, out int intProductionPlanLockedDays))
                throw new Exception("MC:1x00000031");///系统配置错误
            if (intProductionPlanLockedDays > intLackOfInspectionDisplaySupplyPlanDays)
                throw new Exception("MC:1x00000031");///系统配置错误

            ///开始日期
            DateTime startDate = seedDate;
            ///结束日期
            DateTime endDate = startDate;
            if (dateColumns.Count > intProductionPlanLockedDays)
                endDate = startDate.AddDays(intProductionPlanLockedDays - 1);
            else
                endDate = startDate.AddDays(dateColumns.Count - 1);

            ///工厂
            string plant = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "PLANT");
            ///供应商
            string supplierNum = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "SUPPLIER_NUM");
            ///采购员
            string partPurchaser = BLL.LES.CommonBLL.GetWhereFieldValue(whereText, "PART_PURCHASER");

            ///因ATP-009为单线程计算，且计算量比较大，所以此处需要控制如果有状态⑦为10已创建或50处理中的数据时，不允许生成缺件表，提示用户有一份缺件表正在计算
            int cnt = new LackOfMaterialBLL().GetCounts("[STATUS] in (" + (int)LackOfMaterialStatusConstants.WaitForCalculation + "," + (int)LackOfMaterialStatusConstants.Calculating + ") ");
            if (cnt > 0)
                throw new Exception("MC:0x00000234");///有一份缺件表正在计算

            ///生成缺件表
            LackOfMaterialInfo lackOfMaterialInfo = new LackOfMaterialInfo();
            lackOfMaterialInfo.Fid = Guid.NewGuid();
            lackOfMaterialInfo.LackOrderNo = new SeqDefineBLL().GetCurrentCode("LACK_ORDER_NO");///TODO:需要修改编号规则
            lackOfMaterialInfo.SupplierNum = supplierNum;
            lackOfMaterialInfo.Plant = plant;
            lackOfMaterialInfo.PartPurchaser = partPurchaser;
            lackOfMaterialInfo.StartDate = startDate;
            lackOfMaterialInfo.EndDate = endDate;
            lackOfMaterialInfo.Status = (int)LackOfMaterialStatusConstants.WaitForCalculation;
            lackOfMaterialInfo.ValidFlag = true;
            lackOfMaterialInfo.CreateUser = HandlerCommon.LoginUser;
            lackOfMaterialInfo.Id = new LackOfMaterialBLL().InsertInfo(lackOfMaterialInfo);
            ///成功生成缺件表
            return "MC:1x00000049";
        }
        #endregion



        /// <summary>
        /// 获取物料库存数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetPartStockQty(HttpContext context)
        {
            ///物料号
            string partNo = context.Request["PART"];
            ///仓库代码
            string wmNo = context.Request["WM"];
            ///存储区代码
            string zoneNo = context.Request["ZONE"];
            ///库位
            string dloc = context.Request["DLOC"];
            ///合计库存
            decimal sumStocks = new StocksBLL().GetPartStocks(partNo, wmNo, zoneNo, dloc);
            ///
            if (sumStocks == 0)
                return string.Empty;
            return sumStocks.ToString("F2");
        }

        /// <summary>
        /// 获取物料仓储数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetPartsStockInfo(HttpContext context)
        {
            ///物料号
            string partNo = context.Request["PART"];
            ///供应商代码
            string supplierNum = context.Request["SUPPLIER"];
            ///存储区代码
            string zoneNo = context.Request["ZONE"];
            ///仓库代码
            string wmNo = context.Request["WM"];
            PartsStockInfo partsStockInfo = new PartsStockBLL().GetStockInfo(partNo, supplierNum, wmNo, zoneNo);
            if (partsStockInfo == null)
                throw new Exception("MC:0x00000241");///物料仓储信息数据错误

            return JsonHelper.ToJson(partsStockInfo);
        }

        public string SynchronizationKanBanCardInfos(HttpContext context)
        {
            bool result = new KanbanCardBLL().SynchronizationKanBanCardInfos(HandlerCommon.LoginUser);
            if (result)
                return "true";
            else return "false";
        }

        public string GetExcel(HttpContext context)
        {
            string address = HandlerCommon.ExportExcel(context);
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误
            return address + "&" + entityName;
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="context">ENTITY_NAME,AN,page,rows,sort,order,FILTER,SECOND_FILTER,AUTH_FILTER,URL_FILTER,COMBOGRID_FILTER,q</param>
        /// <returns></returns>
        /// SUPPLIER_PART_QUOTA
        public static string GetSupplierPartQuotaTableData(HttpContext context)
        {
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            entityName = entityName.Replace("Init", string.Empty);
            ///AN
            string assemblyName = context.Request["AN"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(assemblyName))
                assemblyName = "BLL.SYS";
            ///page
            int pageIndex = 1;
            string indexPage = context.Request["page"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(indexPage))
                pageIndex = int.Parse(indexPage);
            ///rows
            int maxRow = int.MaxValue;
            string rowMax = context.Request["rows"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(rowMax))
                maxRow = int.Parse(rowMax);
            ///sort
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"]))
            {
                orderText += context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
            }
            ///order
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
                orderText += " " + context.Request["order"];
            ///FILTER
            string strWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(strWhere))
                strWhere = string.Empty;
            if (strWhere.Trim().ToUpper() == "AND")
                strWhere = string.Empty;
            ///SECOND_FILTER
            string secondFilter = context.Request["SECOND_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(secondFilter))
                strWhere += " " + secondFilter;
            ///AUTH_FILTER
            string authFilter = context.Request["AUTH_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(authFilter))
                strWhere += " " + authFilter;
            ///ENTITY_AUTH
            string entityAuth = context.Request["ENTITY_AUTH"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(entityAuth))
                strWhere += entityAuth.Replace("^", "'");
            ///URL_FILTER
            string urlFilter = context.Request["URL_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(urlFilter))
            {
                string[] urlFilters = urlFilter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var urlFilterCondition in urlFilters)
                {
                    strWhere += " and " + urlFilterCondition + " ";
                }
            }
            ///COMBOGRID_FILTER
            string combogridFilter = context.Request["COMBOGRID_FILTER"];
            string cbogridFilterQ = context.Request["q"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(combogridFilter)
                && !HttpCommon.IsNullOrEmptyOrUndefined(cbogridFilterQ))
            {
                string[] combofilters = combogridFilter.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string sqlTempWhere = string.Empty;
                foreach (string combofilter in combofilters)
                {
                    sqlTempWhere += "or charindex('" + cbogridFilterQ + "'," + combofilter + ") > 0 ";
                }
                strWhere += " and (" + sqlTempWhere.Substring(2) + ") ";
            }
            string wmNo = context.Request["FILTERS"].ToString();
            ///
            int dataTotal = 0;

            object list = new object();
            switch (entityName)
            {
                case "SupplierPartQuota":
                    list = new SupplierPartQuotaBLL().GetListByPages(strWhere.Replace("^", "'"), pageIndex, maxRow, wmNo, out dataTotal); break;
                default:
                    break;
            }
            string result = JsonHelper.ToJson(list);
            return @"{""rows"":" + result + @",""total"":" + dataTotal + @"}";
        }
        public string GetpartsBox(HttpContext context)
        {
            string inhousePartClass = context.Request["INHOUSE"];
            string zoneNo = context.Request["ZONE"];
            string wmNo = context.Request["WM"];
            PartsBoxInfo info = new PartsBoxBLL().GetList(" [BOX_PARTS] = N'" + inhousePartClass + "' and [S_WM_NO] = N'" + wmNo + "' and [S_ZONE_NO] = N'" + zoneNo + "'", string.Empty).FirstOrDefault();
            return JsonHelper.ToJson(info);
        }

        public string GetWarehouse(HttpContext context)
        {
            string wmNo = context.Request["WM"];
            WarehouseInfo warehouseInfo = new WarehouseBLL().GetList("", string.Empty).FirstOrDefault();
            return JsonHelper.ToJson(warehouseInfo);
        }
    }
}
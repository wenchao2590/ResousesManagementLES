namespace UI.WEB.COMMON
{
    using BLL.SYS;
    using DM.SYS;
    using Infrustructure.Data;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Linq;
    using System.IO;
    using System.Data;
    using System.Reflection;
    public class HandlerCommon
    {
        #region 变量
        /// <summary>
        /// 角色主键
        /// </summary>
        public static Guid RoleFid
        {
            get
            {
                string roleFid = HttpContext.Current.Session["RoleFid"].ToString();
                if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                    throw new Exception("Err_:SessionIsNull");
                if (roleFid.Length != 36)
                    throw new Exception("Err_:SessionIsNull");
                return Guid.Parse(roleFid);
            }
        }
        /// <summary>
        /// 用户主键
        /// </summary>
        public static Guid UserFid
        {
            get
            {
                string userFid = HttpContext.Current.Session["UserFid"].ToString();
                if (HttpCommon.IsNullOrEmptyOrUndefined(userFid))
                    throw new Exception("Err_:SessionIsNull");
                if (userFid.Length != 36)
                    throw new Exception("Err_:SessionIsNull");
                return Guid.Parse(userFid);
            }
        }
        /// <summary>
        /// 组织主键
        /// </summary>
        /// <returns></returns>
        public static Guid OrganizationFid
        {
            get
            {
                return new UserRoleBLL().GetOrganizationFid(UserFid, RoleFid);
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public static string LoginUser
        {
            get
            {
                string loginUserName = HttpContext.Current.Session["loginUserName"].ToString();
                if (HttpCommon.IsNullOrEmptyOrUndefined(loginUserName))
                    throw new Exception("Err_:SessionIsNull");
                return loginUserName;
            }
        }
        /// <summary>
        /// 语言
        /// </summary>
        /// <returns></returns>
        public static string Language
        {
            get
            {
                if (HttpContext.Current.Session["nowlanguage"] == null)
                    throw new Exception("Err_:SessionIsNull");
                return HttpContext.Current.Session["nowlanguage"].ToString().ToLower();
            }
        }
        #endregion

        /// <summary>
        /// 通用函数
        /// select-Entity
        /// 用于默认的insert,select,update,delete
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string DefaultCommon(HttpContext context)
        {
            string strReturn = string.Empty;
            string methodType = context.Request["methods"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(methodType))
                methodType = context.Request["method"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(methodType))
                throw new Exception("method参数未成功获取");
            string[] action = methodType.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (action.Length < 2)
                throw new Exception("method参数错误");
            object result = DataCommon.InvokeAction(context);
            strReturn = JsonHelper.ToJson(result);
            string  returnString= strReturn.Replace("\":\"[{", "\":[{").Replace("}]\",\"", "}],\"");
            return returnString;
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="context">ENTITY_NAME,AN,page,rows,sort,order,FILTER,SECOND_FILTER,AUTH_FILTER,URL_FILTER,COMBOGRID_FILTER,q</param>
        /// <returns></returns>
        public static string GetTableData(HttpContext context)
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
            ///FOOTER_FLAG
            string footerFlagStr = context.Request["FOOTER_FLAG"];
            bool footerFlag = false;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(footerFlagStr))
                bool.TryParse(footerFlagStr, out footerFlag);
            ///TABLE_NAMES
            string tableName = context.Request["TABLE_NAMES"];
            ///
            object footerData = null;
            if (footerFlag)
            {
                ///
                string moduleName = assemblyName.Replace("BLL.", string.Empty);
                ///
                List<EntityFieldInfo> entityFieldInfos = new EntityFieldBLL().GetStaticticsFields(entityName);
                ///
                footerData = DataCommon.GetFooterData(moduleName, tableName, entityFieldInfos, strWhere.Replace("^", "'"));
            }
            ///
            int dataTotal = 0;
            ///视图
            if (!HttpCommon.IsNullOrEmptyOrUndefined(tableName) && tableName.ToUpper().StartsWith("V_"))
            {
                ///
                string moduleName = assemblyName.Replace("BLL.", string.Empty);

                DataTable dataTable = DataCommon.GetDataTableByPage(
                    moduleName,
                    tableName,
                    strWhere.Replace("^", "'"),
                    orderText,
                    pageIndex,
                    maxRow,
                    out dataTotal) as DataTable;
                if (footerFlag && footerData != null)
                    return @"{""rows"":" + JsonHelper.DataTableToJson(dataTable) + @",""footer"":" + JsonHelper.ToJson(footerData) + @",""total"":" + dataTotal + @"}";
                return @"{""rows"":" + JsonHelper.DataTableToJson(dataTable) + @",""total"":" + dataTotal + @"}";
            }
            ///
            string className;
            if (entityName.IndexOf("_") != -1)
            {
                className = entityName.Substring(0, entityName.IndexOf("_")) + "BLL";
            }
            else { className = entityName + "BLL"; }
            object list = DataCommon.GetListByPage(assemblyName
                , className
                , strWhere.Replace("^", "'")
                , orderText
                , pageIndex
                , maxRow
                , out dataTotal);
            if (footerFlag && footerData != null)
                return @"{""rows"":" + JsonHelper.ToJson(list) + @",""footer"":" + JsonHelper.ToJson(footerData) + @",""total"":" + dataTotal + @"}";
            return @"{""rows"":" + JsonHelper.ToJson(list) + @",""total"":" + dataTotal + @"}";
        }

        public static string GetTreeDataAsync(HttpContext context)
        {
            try
            {
                ///ENTITY_NAME
                string entityName = context.Request["ENTITY_NAME"];
                if (entityName.IndexOf("_") != -1)
                {
                    entityName = entityName.Substring(0, entityName.IndexOf("_"));
                }
                string idField = context.Request["ID"];
                string parentIdField = context.Request["PARENT_ID"];
                ///所选节点ID
                string nodeId = context.Request["NODE_ID"];
                string whereText = context.Request["FILTER"];
                if (!string.IsNullOrEmpty(nodeId))
                    whereText += "and [" + DataCommon.GetFieldName(parentIdField) + "] = '" + nodeId + "' ";
                else
                    whereText += "and ([" + DataCommon.GetFieldName(parentIdField) + "] is null or [" + DataCommon.GetFieldName(parentIdField) + "] = '" + Guid.Empty + "') ";
                string orderText = context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
                int rowIndex = 1;
                int maixRow = int.MaxValue;
                ///模块
                string assemblyName = context.Request["AN"];
                if (string.IsNullOrEmpty(assemblyName))
                    assemblyName = "BLL.SYS";
                string className = entityName + "BLL";
                object list = DataCommon.GetListByPage(assemblyName, className, whereText, orderText, rowIndex, maixRow, out int dataTotal);
                string jsonString = JsonHelper.ToJson(list);
                return jsonString.Replace(idField, "id").Replace(parentIdField, "Parentid");
            }
            catch (Exception ex)
            {
                return ExceptionMessage(ex);
            }
        }

        public static string GetSubListTables(HttpContext context)
        {
            try
            {
                var entityName = context.Request["ENTITY_NAME"];
                if (entityName.IndexOf("_") != -1)
                {
                    entityName = entityName.Substring(0, entityName.IndexOf("_"));
                }
                var tableName = context.Request["TABLE_NAMES"];
                var strWhere = context.Request["FILTER"];
                string assemblyName = context.Request["AN"];
                if (assemblyName == null || assemblyName == string.Empty)
                {
                    assemblyName = "BLL.SYS";
                }
                var rowIndex = 1;
                var maixRow = 15;
                var orderText = string.Empty;
                if (context.Request["page"] != null && context.Request["page"].ToString().Trim() != "")
                {
                    rowIndex = int.Parse(context.Request["page"].ToString());

                }
                if (context.Request["rows"] != null && context.Request["rows"].ToString().Trim() != "")
                {
                    maixRow = int.Parse(context.Request["rows"].ToString());

                }
                if (context.Request["sort"] != null && context.Request["order"] != null)
                {
                    orderText = context.Request["sort"];
                    if (!orderText.Contains("_"))
                        orderText = DataCommon.GetFieldName(orderText.Trim());
                    orderText += " " + context.Request["order"];
                }
                var className = entityName + "BLL";

                var roleFid = Guid.Parse(context.Session["RoleFid"].ToString());

                EntityBLL entityBll = new EntityBLL();
                MenuActionBLL menuActionBll = new MenuActionBLL();

                EntityInfo entityList = entityBll.GetInfo(entityName, tableName);
                var resultEntity = JsonHelper.ToJson(entityList);

                List<ActionInfo> actionInfoOfFormList = menuActionBll.GetCommonEditActionByMenuName(entityName, roleFid);// pageUrl,UserId
                var resultActionForm = JsonHelper.ToJson(actionInfoOfFormList);

                List<EntityFieldInfo> entityFieldInfoList = entityBll.GetGridFieldByEntityName(entityName, tableName);// pageUrl,UserId
                var resultColumns = JsonHelper.ToJson(entityFieldInfoList);

                return @"{""actionForm"":" + resultActionForm + @",""subColumns"":" + resultColumns + @",""entityData"":" + resultEntity + @"}";
            }
            catch (Exception ex)
            {
                return ExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 获取系统代码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetCodeList(HttpContext context)
        {
            CodeBLL codeBll = new CodeBLL();
            string codeName = context.Request["CODE_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(codeName))
                throw new Exception("MC:0x00000084");
            ///错误提示信息需改
            object codeList = codeBll.GetDataSource(codeName);
            return JsonHelper.ToJson(codeList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetLoadSetFormData(HttpContext context)
        {
            ///RoleFid
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");
            if (roleFid.Length != 36)
                throw new Exception("Err_:SessionIsNull");
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("参数获取错误");
            ///ActionInfo
            List<ActionInfo> menuActionList = new MenuActionBLL().GetCommonEditActionByMenuName(entityName, Guid.Parse(roleFid));
            string resultActionForm = JsonHelper.ToJson(menuActionList);
            ///SearchModelConditionInfo
            List<SearchModelConditionInfo> searchModelConditionList = new SearchModelBLL().GetSearchConditionsByName(entityName, out int columnLength);
            string resultSearchForm = JsonHelper.ToJson(searchModelConditionList);
            ///EntityFieldInfo
            List<EntityFieldInfo> entityFieldInfoList = new EntityBLL().GetGridFieldByEntityName(entityName);
            string resultEntityForm = JsonHelper.ToJson(entityFieldInfoList);
            ///EntityInfo
            EntityInfo entityInfo = new EntityBLL().GetInfo(entityName);
            string resultEntityInfo = JsonHelper.ToJson(entityInfo);
            ///
            return @"{""searchform"":" + resultSearchForm
                + @",""searchformcolumnlength"":" + columnLength
                + @",""actionform"":" + resultActionForm
                + @",""entityfieldform"":" + resultEntityForm
                + @",""entityinfo"":" + resultEntityInfo + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetSetStatusEntityFIeldsInfo(HttpContext context)
        {
            ///RoleFid
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");
            if (roleFid.Length != 36)
                throw new Exception("Err_:SessionIsNull");
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");
            ///ACTION_NAME
            string actionName = context.Request["actionName"];
            ///默认en-us
            string nowLangue = "en-us";
            if (HttpContext.Current.Session["nowLanguage"] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Session["nowLanguage"].ToString()))
                nowLangue = HttpContext.Current.Session["nowLanguage"].ToString();
            ///
            string actionConfirmMessage = SYSHandler.GetActionConfirmMessage(actionName, nowLangue);
            ///EntityFieldInfo
            List<EntityFieldInfo> entityFieldInfoList = new EntityBLL().GetGridFieldByEntityName(entityName);
            string resultEntityForm = JsonHelper.ToJson(entityFieldInfoList);
            ///EntityInfo
            EntityInfo entityInfo = new EntityBLL().GetInfo(entityName);
            string resultEntityInfo = JsonHelper.ToJson(entityInfo);
            ///
            return @"{""entityfieldform"":" + resultEntityForm
                + @",""entityinfo"":" + resultEntityInfo
                + @",""message"":" + JsonHelper.ToJson(actionConfirmMessage)
                + @",""actionname"":" + JsonHelper.ToJson(actionName) + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetLoadActionOrSearch(HttpContext context)
        {
            ///RoleFid
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");
            if (roleFid.Length != 36)
                throw new Exception("Err_:SessionIsNull");
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("参数获取错误");
            ///PAGE_URL
            string pageUrl = context.Request["PAGE_URL"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(pageUrl))
                throw new Exception("参数获取错误");
            if (pageUrl.LastIndexOf('/') > -1)
                pageUrl = pageUrl.Substring(pageUrl.LastIndexOf('/') + 1);
            ///ActionInfo
            List<ActionInfo> actionInfoList = new MenuActionBLL().GetActionByPageUrl(pageUrl, Guid.Parse(roleFid));
            string resultAction = JsonHelper.ToJson(actionInfoList);
            ///SearchModelConditionInfo
            List<SearchModelConditionInfo> searchModeConditionInfoList = new SearchModelBLL().GetSearchConditionsByName(entityName, out int columnLength);
            string resultSearch = JsonHelper.ToJson(searchModeConditionInfoList);
            ///return
            return @"{""action"":" + resultAction + @",""searchcolumnlength"":" + columnLength + @",""search"":" + resultSearch + @"}";
        }
        /// <summary>
        /// 根据PAGE_URL获取ACTION
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetActionByPageUrl(HttpContext context)
        {
            ///RoleFid
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");
            if (roleFid.Length != 36)
                throw new Exception("Err_:SessionIsNull");
            ///PAGE_URL
            string pageUrl = context.Request["PAGE_URL"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(pageUrl))
                throw new Exception("参数获取错误");
            if (pageUrl.LastIndexOf('/') > -1)
                pageUrl = pageUrl.Substring(pageUrl.LastIndexOf('/') + 1);
            ///ActionInfo
            List<ActionInfo> actionList = new MenuActionBLL().GetActionByPageUrl(pageUrl, Guid.Parse(roleFid));
            string resultAction = JsonHelper.ToJson(actionList);
            return @"{""action"":" + resultAction + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetCreateColumnsData(HttpContext context)
        {
            ///RoleFid
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");
            if (roleFid.Length != 36)
                throw new Exception("Err_:SessionIsNull");
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("参数获取错误");
            ///TABLE_NAMES
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("参数获取错误");
            ///ActionInfo此处要求LIST与FORM的MENU_NAME完全一致，否则加载不到
            List<ActionInfo> actionList = new MenuActionBLL().GetCommonEditActionByMenuName(entityName, Guid.Parse(roleFid));
            string resultActionForm = JsonHelper.ToJson(actionList);
            ///EntityFieldInfo
            List<EntityFieldInfo> entityFieldList = new EntityBLL().GetGridFieldByEntityName(entityName, tableName);
            string resultEntityFields = JsonHelper.ToJson(entityFieldList);
            ///EntityInfo
            EntityInfo entityInfo = new EntityBLL().GetInfo(entityName, tableName);
            string resultEntityInfo = JsonHelper.ToJson(entityInfo);
            ///
            return @"{""entityfieldform"":" + resultEntityFields + @",""actionform"":" + resultActionForm + @",""entityinfo"":" + resultEntityInfo + @"}";
        }
        /// <summary>
        /// 无SESSION可以直接调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetColumnsEntityData(HttpContext context)
        {
            ///实体名
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            EntityInfo entityinfo = new EntityBLL().GetInfo(entityName);
            if (entityinfo == null)
                throw new Exception("MC:1x00000030");///参数获取错误

            List<EntityFieldInfo> entityfields = new EntityBLL().GetGridFieldByEntityName(entityName);
            ///SearchModel
            List<SearchModelConditionInfo> searchModeConditionInfoList = new SearchModelBLL().GetSearchConditionsByName(entityName, out int columnLength);
            string resultSearch = JsonHelper.ToJson(searchModeConditionInfoList);

            return @"{""entityfields"":" + JsonHelper.ToJson(entityfields)
                + @",""entityinfo"":" + JsonHelper.ToJson(entityinfo)
                + @",""formSearchData"":" + resultSearch
                + @",""formSearchLength"":" + columnLength + @"}";
        }

        public static string GetActionSearchData(HttpContext context)
        {
            string strReturn = string.Empty;
            string pageUrl = context.Request["PAGE_URL"];
            if (!string.IsNullOrEmpty(pageUrl) && pageUrl != "NaN")
            {
                if (pageUrl.LastIndexOf('/') > -1)
                    pageUrl = pageUrl.Substring(pageUrl.LastIndexOf('/') + 1);
                SearchModelBLL searchModeBll = new SearchModelBLL();
                MenuActionBLL menuActionBll = new MenuActionBLL();
                ///ROLEFID==GUID.EMPTY时
                List<ActionInfo> actionInfoList = menuActionBll.GetActionByPageUrl(pageUrl, Guid.Empty);// pageUrl,UserId
                string resultAction = JsonHelper.ToJson(actionInfoList);
                ///
                List<SearchModelConditionInfo> searchModeConditionInfoList = searchModeBll.GetSearchConditionsByName(pageUrl, out int columnLength);
                string resultSearch = JsonHelper.ToJson(searchModeConditionInfoList);

                strReturn = @"{""action"":" + resultAction + @",""searchcolumnlength"":" + columnLength + @",""search"":" + resultSearch + @"}";
            }
            return strReturn;
        }
        /// <summary>
        /// 获取动作及数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAcionAndEntityData(HttpContext context)
        {
            ///菜单FID
            string menuFid = context.Request["MENU_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(menuFid))
                throw new Exception("参数获取错误");
            if (menuFid.Length != 36)
                throw new Exception("参数获取错误");
            ///实体名,可能是FORM的实体名
            string entityName = context.Request["ENTITY_NAME"];

            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("参数获取错误");
            ///表名
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(tableName))
                throw new Exception("参数获取错误");
            ///
            ///FORM的链接地址
            string formUrl = string.Empty;
            List<ActionInfo> actionInfoList = new MenuActionBLL().GetActionByMenuFid(Guid.Parse(menuFid), RoleFid, entityName, out int editFormWidth, out int editFormHeight, out formUrl);
            ///LIST的ACTION
            string resultAction = JsonHelper.ToJson(actionInfoList.Where(d => d.IsListAction).ToList());
            ///FORM的ACTION
            string resultActionForm = JsonHelper.ToJson(actionInfoList.Where(d => !d.IsListAction).ToList());
            ///SearchModelConditionInfo
            ///获取查询条件字段
            List<SearchModelConditionInfo> gridconditionlist = new SearchModelBLL().GetSearchConditionsByName(entityName, out int columnLength);
            string resultSearch = JsonHelper.ToJson(gridconditionlist);
            ///EntityFieldInfo  获取实体字段在页面显示的信息
            List<EntityFieldInfo> entityFieldList = new EntityBLL().GetGridFieldByEntityName(entityName, tableName);
            string resultEntityFields = JsonHelper.ToJson(entityFieldList);
            ///EntityInfo
            EntityInfo entityinfo = new EntityBLL().GetInfo(entityName, tableName);
            string resultEntityInfo = JsonHelper.ToJson(entityinfo);
            ///
            return @"{""action"":" + resultAction
                + @",""search"":" + resultSearch
                + @",""searchcolumnlength"":" + columnLength
                + @",""entityfieldform"":" + resultEntityFields
                + @",""actionform"":" + resultActionForm
                + @",""searchform"":" + resultSearch
                + @",""searchformcolumnlength"":" + columnLength
                + @",""entityinfo"":" + resultEntityInfo
                + @",""formUrl"":" + JsonHelper.ToJson(formUrl)
                + @",""formWidth"":" + editFormWidth
                + @",""formHeight"":" + editFormHeight + @"}";
        }


        #region 获取菜单、动作、数据模型、检索条件
        /// <summary>
        /// 获取Form弹出所需的项
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetFormOpenAttributes(HttpContext context)
        {
            ///菜单FID
            string menuFid = context.Request["MENU_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(menuFid))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///实体名,可能是FORM的实体名
            string entityName = context.Request["ENTITY_NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///角色
            string roleFid = context.Session["RoleFid"].ToString();
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("Err_:SessionIsNull");

            List<ActionInfo> actionInfoList = new MenuActionBLL().GetFormActions(Guid.Parse(menuFid), Guid.Parse(roleFid), entityName
                , out int editFormWidth, out int editFormHeight, out string formUrl);
            ///FORM的ACTION
            string resultActionForm = JsonHelper.ToJson(actionInfoList);
            ///
            return @"{""actionform"":" + resultActionForm
                + @",""formUrl"":" + JsonHelper.ToJson(formUrl)
                + @",""formWidth"":" + editFormWidth
                + @",""formHeight"":" + editFormHeight + @"}";
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetControlData(HttpContext context)
        {
            return GetControlDataToCombox(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetControlDataToCombox(HttpContext context)
        {
            try
            {
                ///assemblyName
                string assemblyName = context.Request["AN"];
                if (string.IsNullOrEmpty(assemblyName))
                    assemblyName = "BLL.SYS";
                ///page
                int rowIndex = 1;
                if (!string.IsNullOrEmpty(context.Request["page"]))
                    rowIndex = Convert.ToInt32(context.Request["page"]);
                ///rows
                int maxRow = int.MaxValue;
                if (!string.IsNullOrEmpty(context.Request["rows"]))
                    maxRow = Convert.ToInt32(context.Request["rows"]);
                ///orderText
                string orderText = string.Empty;
                if (!string.IsNullOrEmpty(context.Request["sort"])
                    && !string.IsNullOrEmpty(context.Request["order"]))
                {
                    orderText = context.Request["sort"];
                    if (!orderText.Contains("_"))
                        orderText = DataCommon.GetFieldName(orderText.Trim());
                    orderText += " " + context.Request["order"];
                }
                ///SQL_SORT
                string sqlSort = context.Request["SQLSORT"];
                if (!string.IsNullOrEmpty(sqlSort))
                {
                    if (string.IsNullOrEmpty(orderText))
                        orderText = sqlSort;
                    else
                        orderText = " " + sqlSort + "," + orderText;
                }
                ///whereText
                string whereText = string.Empty;
                if (!string.IsNullOrEmpty(context.Request["FILTER"]))
                    whereText = context.Request["FILTER"];
                ///AUTH_FILTER
                string authFilter = context.Request["AUTH_FILTER"];
                if (!string.IsNullOrEmpty(authFilter))
                    whereText += authFilter;
                ///COMBOGRID_FILTER
                string combogridFilter = context.Request["COMBOGRID_FILTER"];
                string cbogridFilterQ = context.Request["q"];
                if (!string.IsNullOrEmpty(combogridFilter) && !string.IsNullOrEmpty(cbogridFilterQ))
                {
                    string[] combofilters = combogridFilter.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    string sqlTempWhere = string.Empty;
                    foreach (string combofilter in combofilters)
                    {
                        sqlTempWhere += "or charindex('" + cbogridFilterQ + "'," + combofilter + ") > 0 ";
                    }
                    if (!string.IsNullOrEmpty(sqlTempWhere))
                        whereText += " and (" + sqlTempWhere.Substring(2) + ") ";
                }
                ///ENTITY_NAME
                string entityName = context.Request["ENTITY_NAME"];
                if (entityName.IndexOf("_") != -1)
                {
                    entityName = entityName.Substring(0, entityName.IndexOf("_"));
                }
                string className = entityName + "BLL";

                object infoList = DataCommon.GetListByPage(assemblyName
                    , className
                    , whereText.Replace("^", "'")
                    , orderText
                    , rowIndex
                    , maxRow
                    , out int dataTotal);
                string result = JsonHelper.ToJson(infoList);
                return result;
                ///  return @"{""rows"":" + result + @",""total"":" + dataTotal + @"}";
            }
            catch (Exception ex)
            {
                return ExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetControlDataToColumn(HttpContext context)
        {
            try
            {
                ///BLL
                string assemblyName = context.Request["AN"];
                if (string.IsNullOrEmpty(assemblyName))
                    assemblyName = "BLL.SYS";
                ///PAGE
                int rowIndex = 1;
                if (!string.IsNullOrEmpty(context.Request["page"]))
                    rowIndex = Convert.ToInt32(context.Request["page"]);
                ///MAX ROW
                int maxRow = int.MaxValue;
                if (!string.IsNullOrEmpty(context.Request["rows"]))
                    maxRow = Convert.ToInt32(context.Request["rows"]);
                ///ORDER BY
                string textOrder = string.Empty;
                if (!string.IsNullOrEmpty(context.Request["sort"])
                    && !string.IsNullOrEmpty(context.Request["order"]))
                {
                    textOrder = context.Request["sort"];
                    if (!textOrder.Contains("_"))
                        textOrder = DataCommon.GetFieldName(textOrder.Trim());
                    textOrder += " " + context.Request["order"];
                }
                ///WHERE
                string textWhere = string.Empty;
                if (!string.IsNullOrEmpty(context.Request["FILTER"]))
                    textWhere = context.Request["FILTER"];
                string authFilter = context.Request["AUTH_FILTER"];
                if (!string.IsNullOrEmpty(authFilter))
                    textWhere += authFilter;
                ///ENTITY_NAME
                string className = context.Request["ENTITY_NAME"];
                if (!string.IsNullOrEmpty(className))
                    className += "BLL";
                ///COLUMN_NAME
                string resultColumnName = context.Request["COLUMN_NAME"];
                ///获取数据
                object infoList = DataCommon.GetListByPage(assemblyName
                    , className
                    , textWhere.Replace("^", "'")
                    , textOrder
                    , rowIndex
                    , maxRow
                    , out int dataTotal);
                return @"{""rows"":" + JsonHelper.ToJson(infoList) + @",""controlName"":" + JsonHelper.ToJson(resultColumnName) + @"}";
            }
            catch (Exception ex)
            {
                return JsonHelper.ToJson(ExceptionMessage(ex));
            }
        }
        /// <summary>
        /// 批量下载文件
        /// 用于校验服务端文件是否存在，不输出流
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string DownLoadFiles(HttpContext context)
        {
            string filesPath = context.Request["FILE_PATH"];
            if (string.IsNullOrEmpty(filesPath))
                throw new Exception("MC:1x00000037");///文件路径为空

            string[] filePaths = filesPath.Split('|');
            string clientFilePaths = string.Empty;
            ///校验文件是否存在
            foreach (var filePath in filePaths)
            {
                string pathForFile = filePath;
                if (!pathForFile.StartsWith("/"))
                    pathForFile = "/" + pathForFile;
                if (!File.Exists(HttpContext.Current.Server.MapPath(pathForFile))) continue;
                clientFilePaths += "|" + filePath;
            }
            if (string.IsNullOrEmpty(clientFilePaths))
                throw new Exception("MC:1x00000038");///没有可下载的文件

            ///
            return clientFilePaths.Substring(1);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string UpLoadFiles(HttpContext context)
        {
            ///提取文件信息
            HttpFileCollection fileCollection = context.Request.Files;
            if (fileCollection.Count == 0)
                throw new Exception("MC:0x00000000");///TODO:无上传文件
            int uploadFileCnt = 0;
            string methodName = context.Request["methodName"];
            if (string.IsNullOrEmpty(methodName))
                methodName = "UploadFileFinished";
            ///流文件路径，只支持单个文件上传为流
            Stream inputFileStream = null;
            ///文件扩展名
            string fileExtensionName = string.Empty;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                if (fileCollection[i].ContentLength <= 0) continue;
                ///文件扩展名
                fileExtensionName = fileCollection[i].FileName.Substring(fileCollection[i].FileName.LastIndexOf('.') + 1);
                if (methodName.ToLower() == "savetodatabase")
                {
                    ///文件流
                    inputFileStream = fileCollection[i].InputStream;
                    uploadFileCnt++;
                    continue;
                }

                string filePath = context.Request.Form[i];
                if (!context.Request.Form.Keys[i].StartsWith("file"))
                    filePath = context.Request.Form[i + 1];

                ///未设置上传文件的路径则提供默认值
                if (string.IsNullOrEmpty(filePath) || filePath == "|")
                    filePath = @"..\TEMP\UPLOADFILES|" + DateTime.Now.Ticks.ToString() + "_" + fileCollection[i].FileName.Substring(fileCollection[i].FileName.LastIndexOf('\\') + 1);///TODO:系统配置中增加默认上传文件路径配置

                ///只有文件名没有路径
                if (filePath.StartsWith("|"))
                    filePath = @"..\TEMP\UPLOADFILES|" + filePath.Substring(1) + "." + fileExtensionName;///TODO:系统配置中增加默认上传文件路径配置

                ///只有路径没有文件名
                if (filePath.EndsWith("|"))
                    filePath = filePath + DateTime.Now.Ticks.ToString() + "_" + fileCollection[i].FileName.Substring(fileCollection[i].FileName.LastIndexOf('\\') + 1);
                ///文件夹是否存在
                if (!filePath.StartsWith("/"))
                    filePath = "/" + filePath;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath.Split('|')[0])))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath.Split('|')[0]));

                filePath = filePath.Replace("|", @"\") + "." + fileExtensionName;
                fileCollection[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));
                uploadFileCnt++;
            }
            if (uploadFileCnt == 0)
                throw new Exception("MC:0x00000000");///TODO:无上传文件

            string key = context.Request["key"];
            if (string.IsNullOrEmpty(key))
                return "MC:0x00000069";///TODO:上传成功

            ///BLL
            string assemblyName = context.Request["AN"];
            if (string.IsNullOrEmpty(assemblyName))
                assemblyName = "BLL.SYS";
            if (!assemblyName.StartsWith("BLL."))
                assemblyName = "BLL." + assemblyName;
            ///ENTITY_NAME
            string className = context.Request["ENTITY_NAME"];
            if (!string.IsNullOrEmpty(className))
                className += "BLL";

            ///图片保存到数据库
            if (methodName.ToLower() == "savetodatabase" && inputFileStream != null)
            {
                byte[] imageContent = new byte[inputFileStream.Length];
                inputFileStream.Read(imageContent, 0, imageContent.Length);
                inputFileStream.Seek(0, SeekOrigin.Begin);
                return JsonHelper.ToJson(HttpCommon.SaveImageToDatabase(assemblyName, className, imageContent, fileExtensionName, key, LoginUserName()));
            }
            return JsonHelper.ToJson(HttpCommon.UploadFileFinished(assemblyName, className, methodName, key, LoginUserName()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string ImageToHtml(HttpContext context)
        {
            ///
            string key = context.Request["key"];
            if (string.IsNullOrEmpty(key))
                return "MC:0x00000069";///TODO:没有可预览的图片

            ///BLL
            string assemblyName = context.Request["AN"];
            if (string.IsNullOrEmpty(assemblyName))
                assemblyName = "BLL.SYS";
            if (!assemblyName.StartsWith("BLL."))
                assemblyName = "BLL." + assemblyName;
            ///ENTITY_NAME
            string className = context.Request["ENTITY_NAME"];
            if (!string.IsNullOrEmpty(className))
                className += "BLL";

            object imageObject = HttpCommon.ReadImageFromDatabase(assemblyName, className, key);
            if (imageObject == null)
                throw new Exception("Err_:MC:0x00000000");///TODO:没有可预览的图片
            byte[] imageByte = (byte[])imageObject;
            Stream imageStream = new MemoryStream(imageByte);
            string imageString = string.Empty;
            using (var streamReader = new StreamReader(imageStream))
            {
                imageString = streamReader.ReadToEnd();
            }
            return JsonHelper.ToJson(imageString);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Login(HttpContext context)
        {
            ///设置SESSION中的语言
            string language = context.Request["language"];
            HttpContext.Current.Session["nowLanguage"] = language;
            string userName = context.Request["username"];
            string passWord = context.Request["password"];
            UserInfo userinfo = new UserBLL().Login(userName, passWord);
            HttpContext.Current.Session["UserFid"] = userinfo.Fid.GetValueOrDefault().ToString();
            MPSCache.Insert(userinfo.Fid.GetValueOrDefault().ToString(), userinfo, 1800);
            HttpContext.Current.Session["loginUserName"] = userinfo.LoginName;
            return JsonHelper.ToJson(userinfo.Fid.GetValueOrDefault());
        }
        /// <summary>
        /// 获取USER_FID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserFidBySession(HttpContext context)
        {
            if (context.Session["UserFid"] == null)
                return JsonHelper.ToJson("Err_:SessionIsNull");
            if (string.IsNullOrEmpty(context.Session["UserFid"].ToString()))
                return JsonHelper.ToJson("Err_:SessionIsNull");
            return JsonHelper.ToJson(context.Session["UserFid"]);
        }
        /// <summary>
        /// 获取Seesion值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetSessionValue(HttpContext context)
        {
            string key = context.Request["KEY"];
            if (context.Session[key] == null)
                return "SessionIsNull";
            return JsonHelper.ToJson(context.Session[key].ToString());
        }
        /// <summary>
        /// 获取登录用户名
        /// </summary>
        /// <returns></returns>
        public static string LoginUserName()
        {
            if (HttpContext.Current.Session["loginUserName"] == null)
                throw new Exception("Err_:SessionIsNull");
            return HttpContext.Current.Session["loginUserName"].ToString();
        }
        /// <summary>
        /// 获取当前语言
        /// </summary>
        /// <returns></returns>
        public static string CurrentLangage()
        {
            if (HttpContext.Current.Session["nowlanguage"] == null)
                throw new Exception("Err_:SessionIsNull");
            return HttpContext.Current.Session["nowlanguage"].ToString();
        }

        private string ChangeObjName(string objName)
        {
            string[] objnameItem = objName.Split('_');
            string result = string.Empty;
            for (int i = 0; i < objnameItem.Length; i++)
            {
                string TempResult = string.Empty;
                TempResult = objnameItem[i].Length > 1 ? objnameItem[i].Substring(0, 1) : string.Empty;
                string tolower = objnameItem[i].ToLower();
                TempResult = tolower.Replace(TempResult.ToLower(), TempResult);
                result += TempResult;
            }
            return result;
        }

        private string GetComboxJson(string textField, string valueFidle, string jsonStr)
        {
            string result = string.Empty;
            string Tsouce = "{'id':'@id','text':'@text'}";
            string TempJson = jsonStr.Replace("}]", "},").Replace("},", "∷");
            string[] jsonItem = TempJson.Split('∷');
            for (int i = 0; i < jsonItem.Length; i++)
            {
                string text = string.Empty;
                if (jsonItem[i].Trim() != string.Empty)
                {
                    string temp = jsonItem[i] + ",";
                    string[] texts = temp.Replace("}", string.Empty).Replace("\"" + textField + "\"", "⊙").Split('⊙');
                    if (texts.Length == 2)
                    {
                        int begin = texts[1].IndexOf(",");
                        text = texts[1].Substring(0, begin).Replace("\"", string.Empty).Replace(":", string.Empty); ;

                    }
                    string val = string.Empty;
                    temp = jsonItem[i] + ",";
                    string[] vals = temp.Replace("}", string.Empty).Replace("\"" + valueFidle + "\"", "⊙").Split('⊙');
                    if (vals.Length == 2)
                    {

                        int begin = vals[1].IndexOf(",");
                        val = vals[1].Substring(0, begin).Replace("\"", string.Empty).Replace(":", string.Empty);

                    }

                    result += Tsouce.Replace("'", "\"").Replace("@id", val).Replace("id", valueFidle).Replace("@text", text).Replace("text", textField) + ",";
                }

            }
            result = result.Length > 1 ? result.Substring(0, result.Length - 1) : result;
            return result;
        }

        private void GetChartData(HttpContext context)
        {
            var chartName = context.Request["chartName"];
            ChartBLL chartBll = new ChartBLL();

            //var strChart1 = chartBll.GetChartConfigBy();
            //strReturn = chartBll.GetChartDataByName(chartName);
        }

        public static Exception GetExceptionCode(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex;
            }
            else
            {
                return GetExceptionCode(ex.InnerException);
            }
        }
        /// <summary>
        /// 获取翻译后的错误信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string ExceptionMessage(Exception ex)
        {
            string exMessage = GetExceptionCode(ex).Message;
            if (string.IsNullOrEmpty(exMessage))
                return "Err_:ERROR";
            string nowLangue = "en-us";
            if (HttpContext.Current.Session["nowLanguage"] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Session["nowLanguage"].ToString()))
                nowLangue = HttpContext.Current.Session["nowLanguage"].ToString();
            if (exMessage.StartsWith("Err_:"))
                exMessage = exMessage.Replace("Err_:", string.Empty);
            if (exMessage.StartsWith("MC:"))
                exMessage = new MessageBLL().GetMessage(nowLangue, exMessage.ToUpper().Replace("MC:", string.Empty));
            return exMessage;
        }
        /// <summary>
        /// 记录日志到数据库
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void WriteOperationLog(HttpContext context)
        {
            object opData = context.Request.Params;
            int operationType = GetActionConstants(context);
            string tableName = context.Request["ENTITY_NAME"];
            string pageUrl = context.Request.UrlReferrer.AbsoluteUri.ToString();
            string browserInfo = context.Request.Browser.Type.ToString();
            string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            string createUser = LoginUserName();
            string an = context.Request["AN"];
            if (string.IsNullOrEmpty(an)) return;
            switch (an)
            {
                default: new OperationLogBLL().InsertLog(JsonHelper.ToJson(opData), operationType, tableName, pageUrl, browserInfo, ipAddress, createUser); break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static int GetActionConstants(HttpContext context)
        {
            string actionName = HttpCommon.GetActionName(context);
            ///根据不同的ACTION指向执行不同的函数
            switch (actionName.ToLower())
            {
                case "insert": return 10;
                case "update": return 20;
                case "delete": return 30;
                case "select": return 40;
                case "set": return 50;
                default: return 40;
            }
        }
        /// <summary>
        /// 根据entityfield配置导出excel
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string ExportExcel(HttpContext context)
        {
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///TABLE_NAMES
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(tableName))
                throw new Exception("MC:1x00000030");///参数获取错误

            context.Session[entityName + "ExportStatus"] = "正在生成EXCEL，请不要关闭此页面";
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
            ///URL_FILTER
            string urlFilter = context.Request["URL_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(urlFilter))
            {
                string[] urlFilters = urlFilter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var urlFilterCondition in urlFilters)
                {
                    textWhere += " and " + urlFilterCondition + " ";
                }
            }
            ///assemblyName
            string assemblyName = context.Request["AN"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(assemblyName))
                assemblyName = "BLL.SYS";
            /////创建标题行
            List<EntityFieldInfo> entityFieldInfos = new EntityBLL().GetExcelFieldList(entityName)
                .OrderBy(d => d.DisplayOrder.GetValueOrDefault())
                .OrderBy(d => d.ExportExcelOrder.GetValueOrDefault()).ToList();
            if (entityFieldInfos.Count == 0)
                throw new Exception("MC:1x00000033");///未设置导出内容

            ///CODE_ITEM下拉菜单集合
            Dictionary<string, Dictionary<string, string>> dicDropdowns = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> columnNames = SYSHandler.GetExcelColumnNames(entityFieldInfos, ref dicDropdowns);
            ///获取数据
            DataTable dataTable = DataCommon.GetDatatableForExcel(assemblyName, entityName, tableName, new List<string>(columnNames.Keys), textWhere.Replace("^", "'"), orderText);

            if (dataTable.Rows.Count > 65535)
                throw new Exception("MC:1x00000036");///导出数据不能多于65535条

            ///控制decimal类型的小数精度
            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var entityFieldInfo in entityFieldInfos)
                {
                    if (entityFieldInfo.DataType != (int)DataTypeConstants.DECIMAL) continue;
                    if (entityFieldInfo.Precision == null) continue;
                    string stringQty = dr[entityFieldInfo.TableFieldName].ToString();
                    if (!decimal.TryParse(stringQty, out decimal decimalQty)) continue;
                    dr[entityFieldInfo.TableFieldName] = decimalQty.ToString("F" + entityFieldInfo.Precision.GetValueOrDefault());
                }
            }

            ///NPOI
            NpoiHelper.TableToExcel(dataTable, columnNames, dicDropdowns, entityName, filePath + @"\" + fileName);
            ///返回路径
            context.Session.Remove(entityName + "ExportStatus");
            return "../TEMP/EXPORTFILES/" + fileName;
        }
        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string DownloadExcelTemplate(HttpContext context)
        {
            ////ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (entityName.IndexOf("_") != -1)
            {
                entityName = entityName.Substring(0, entityName.IndexOf("_"));
            }
            if (HttpCommon.IsNullOrEmptyOrUndefined(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///TABLE_NAMES
            string tableName = context.Request["TABLE_NAMES"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(tableName))
                throw new Exception("MC:1x00000030");///参数获取错误

            context.Session[entityName + "ExportStatus"] = "正在生成EXCEL，请不要关闭此页面";
            ///fileName
            string fileName = entityName + DateTime.Now.Ticks + ".xlsx";
            ///filePath
            string filePath = HttpContext.Current.Server.MapPath("/TEMP/DOWNLOADTEMPLATEFILES");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            ///assemblyName
            string assemblyName = context.Request["AN"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(assemblyName))
                assemblyName = "BLL.SYS";
            /////创建标题行
            List<EntityFieldInfo> entityFieldInfos = new EntityBLL().GetExcelFieldList(entityName)
                .OrderBy(d => d.DisplayOrder.GetValueOrDefault())
                .OrderBy(d => d.ExportExcelOrder.GetValueOrDefault()).ToList();
            if (entityFieldInfos.Count == 0)
                throw new Exception("MC:1x00000033");///未设置导出内容

            ///下拉菜单集合
            Dictionary<string, Dictionary<string, string>> dicDropdowns = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> columnNames = SYSHandler.GetExcelColumnNames(entityFieldInfos, ref dicDropdowns);
            ///NPOI
            NpoiHelper.TableToExcel(new DataTable(), columnNames, dicDropdowns, entityName, filePath + @"\" + fileName);
            ///返回路径
            context.Session.Remove(entityName + "ExportStatus");
            return "../TEMP/DOWNLOADTEMPLATEFILES/" + fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string ImportExcel(HttpContext context)
        {
            ///提取文件信息
            HttpFileCollection fileCollection = context.Request.Files;
            if (fileCollection.Count == 0)
                throw new Exception("MC:1x00000039");///无上传文件

            if (fileCollection[0].ContentLength <= 0)
                throw new Exception("MC:1x00000040");///上传的文件无内容

            ///文件名
            string fileName = fileCollection[0].FileName.Substring(fileCollection[0].FileName.LastIndexOf(@"\") + 1);
            ///文件路径
            string filePath = HttpContext.Current.Server.MapPath("/TEMP/UPLOADFILES");
            ///文件夹是否存在
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            ///保存文件
            fileCollection[0].SaveAs(filePath + @"\" + fileName);
            ///BLL
            string assemblyName = context.Request["AN"];
            if (string.IsNullOrEmpty(assemblyName))
                assemblyName = "BLL.SYS";
            if (!assemblyName.StartsWith("BLL."))
                assemblyName = "BLL." + assemblyName;
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (string.IsNullOrEmpty(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///FILTER
            string textWhere = context.Request["FILTER"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(textWhere))
                textWhere = string.Empty;
            if (textWhere.Trim().ToUpper() == "AND")
                textWhere = string.Empty;
            ///URL_FILTER
            string urlFilter = context.Request["URL_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(urlFilter))
            {
                string[] urlFilters = urlFilter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var urlFilterCondition in urlFilters)
                {
                    textWhere += " and " + urlFilterCondition + " ";
                }
            }

            ///创建标题行
            List<EntityFieldInfo> entityFieldInfos = new EntityBLL().GetExcelFieldList(entityName)
                .OrderBy(d => d.DisplayOrder.GetValueOrDefault())
                .OrderBy(d => d.ExportExcelOrder.GetValueOrDefault()).ToList();
            if (entityFieldInfos.Count == 0)
                throw new Exception("MC:1x00000033");///未设置导出内容

            ///属性,描述
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            ///属性,字段
            Dictionary<string, string> fieldNames = new Dictionary<string, string>();
            ///按当前语言环境获取标题
            foreach (EntityFieldInfo entityFieldInfo in entityFieldInfos)
            {
                int[] controlTypeArray = new int[] { (int)ControlTypeConstants.COMBOCODE
                    , (int)ControlTypeConstants.COMBOBOX
                    , (int)ControlTypeConstants.COMBOGRID
                    , (int)ControlTypeConstants.COMBOTREE };
                if (string.IsNullOrEmpty(entityFieldInfo.FieldName)) continue;
                fieldNames.Add(entityFieldInfo.FieldName, entityFieldInfo.TableFieldName);
                if (entityFieldInfo.ExportExcelOrder.GetValueOrDefault() == 0) continue;
                if (!controlTypeArray.Contains(entityFieldInfo.ControlType.GetValueOrDefault()))
                {
                    columnNames.Add(entityFieldInfo.FieldName, string.Empty);
                    continue;
                }
                if (entityFieldInfo.Extend2 == "GridNotFormatter")
                {
                    columnNames.Add(entityFieldInfo.FieldName, string.Empty);
                    continue;
                }
                if (CurrentLangage().ToLower() == "zh-cn")
                    columnNames.Add(entityFieldInfo.FieldName, entityFieldInfo.DisplayNameCn);
                else
                    columnNames.Add(entityFieldInfo.FieldName, string.IsNullOrEmpty(entityFieldInfo.DisplayNameEn) ? entityFieldInfo.FieldName : entityFieldInfo.DisplayNameEn);
            }
            ///获取数据
            DataTable dataTable = NpoiHelper.ExcelToTable(filePath + @"\" + fileName, columnNames);
            if (dataTable == null)
                throw new Exception("MC:1x00000042");///导入数据格式错误
            if (dataTable.Rows.Count > 65535)
                throw new Exception("MC:1x00000041");
            ///导入数据不能多于65535条
            DataCommon.ImportDataByExcel(assemblyName, entityName + "BLL", dataTable, fieldNames, LoginUser, textWhere.Replace("^", "'"));

            return "MC:0x00000156";///导入成功
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string PrintFiles(HttpContext context)
        {
            string printConfigCode = context.Request["PRINT_CONFIG_CODE"];
            ///根据打印配置代码①从打印配置TS_SYS_PRINT_CONFIG中获取对应的配置信息，如未能获取有效数据则提示<打印配置信息错误>，并记录系统日志
            PrintConfigInfo info = new PrintConfigBLL().GetInfoByCode(printConfigCode);
            if (info == null)
                throw new Exception("MC:0x00000731");///打印配置信息错误

            ///根据打印配置中的打印模板路径④及打印模板文件名称③获取服务端中的两个文件
            ///一个为打印模板文件名称③对应的文件，另一个为同名的xml字段匹配配置文件
            string templateFileType = "html";
            switch (info.TemplateFileType.GetValueOrDefault())
            {
                case (int)TemplateFileTypeConstants.xlsx: templateFileType = "xlsx"; break;
                case (int)TemplateFileTypeConstants.xls: templateFileType = "xls"; break;
                default: templateFileType = "html"; break;
            }
            ///根据打印文件路径名称在服务端获取相应的文件，如未能成功获取则提示<打印文件错误>，并记录系统日志TL_SYS_OPERATION_LOG
            if (!info.PrintTemplateUrl.StartsWith("/"))
                info.PrintTemplateUrl = "/" + info.PrintTemplateUrl;
            string templateFileName = HttpContext.Current.Server.MapPath(info.PrintTemplateUrl) + "/" + info.PrintTemplateFilename + "." + templateFileType;
            if (!File.Exists(templateFileName))
                throw new Exception("MC:0x00000061");///打印模板文件不存在

            string configFileName = HttpContext.Current.Server.MapPath(info.PrintTemplateUrl) + "/" + info.PrintTemplateFilename + ".xml";
            if (!File.Exists(configFileName))
                throw new Exception("MC:0x00000067");///打印配置文件不存在

            ///
            string tempPrintFilePathConfig = "/TEMP/PRINTFILES";
            ///
            string tempPrintFilePath = HttpContext.Current.Server.MapPath(tempPrintFilePathConfig);
            ///文件夹是否存在
            if (!Directory.Exists(tempPrintFilePath))
                Directory.CreateDirectory(tempPrintFilePath);
            ///BLL层的对象实例化
            object bllObject = HttpCommon.GetBusinessObject(context);
            ///对于UPDATE.DELETE.SELECT方法需要ID参数支持
            object[][] rowsKeyValues = HttpCommon.GetEntityKeyValues(context);
            ///loginUser
            string loginUserName = LoginUserName();
            ///
            string dataMethod = context.Request["DATA_METHOD"];
            if (string.IsNullOrEmpty(dataMethod))
                dataMethod = "GetPrintData";
            ///
            MethodInfo method = bllObject.GetType().GetMethod(dataMethod);
            if (method == null) method = bllObject.GetType().GetMethod("GetPrintData");
            if (method == null) method = bllObject.GetType().GetMethod("GetPrintDatas");

            List<object> objs = new List<object>();
            if (method.Name.Equals("GetPrintData"))
            {
                objs = rowsKeyValues[0].ToList();
                objs.Add(loginUserName);
            }
            else
            {
                if (method.Name.Contains("GetPrintDatas") || method.Name.Contains(dataMethod))
                {
                    List<string> rowKeys = new List<string>();
                    for (int i = 0; i < rowsKeyValues.Length; i++)
                    {
                        string rowKeyValues = string.Empty;
                        for (int j = 0; j < rowsKeyValues[i].Length; j++)
                        {
                            rowKeyValues += "^" + rowsKeyValues[i][j].ToString();
                        }
                        rowKeys.Add(rowKeyValues.Substring(1));
                    }
                    objs.Add(rowKeys);
                    objs.Add(loginUserName);
                }
            }

            ///
            DataSet printDataSet = method.Invoke(bllObject, objs.ToArray()) as DataSet;
            ///
            List<string> printFiles = new List<string>();
            string file = new PrintBLL().CreatePrintFileByPageEntity(templateFileName, configFileName, templateFileType, printDataSet, tempPrintFilePath, tempPrintFilePathConfig);

            if (!file.Equals(string.Empty))
            {
                printFiles.Add(file); ;
            }

            if (printFiles.Count == 0)
                throw new Exception("MC:0x00000072");///没有打印文件生成

            string updateMethod = context.Request["UPDATE_METHOD"];
            MethodInfo methodcallback = null;
            if (!string.IsNullOrEmpty(updateMethod))
            {
                if (updateMethod.Equals("undefined"))
                {
                    updateMethod = "";
                }
                methodcallback = bllObject.GetType().GetMethod(updateMethod);
            }

            if (methodcallback != null)
            {
                methodcallback.Invoke(bllObject, objs.ToArray());
                return @"{""files"":" + JsonHelper.ToJson(printFiles) + @"}";
            }
            else
            {
                return @"{""files"":" + JsonHelper.ToJson(printFiles) + @"}";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string SetStatusEntity(HttpContext context)
        {
            ///BLL
            string assemblyName = context.Request["AN"];
            if (string.IsNullOrEmpty(assemblyName))
                assemblyName = "BLL.SYS";
            if (!assemblyName.StartsWith("BLL."))
                assemblyName = "BLL." + assemblyName;
            ///ENTITY_NAME
            string entityName = context.Request["ENTITY_NAME"];
            if (string.IsNullOrEmpty(entityName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///动作
            string actionName = context.Request["actionName"];
            if (string.IsNullOrEmpty(actionName))
                throw new Exception("MC:1x00000030");///参数获取错误

            ///实例
            object objEntity = HttpCommon.GetEntityObject(context);
            ///BLL层的对象实例化
            object bllObject = HttpCommon.GetBusinessObject(context);
            ///对于UPDATE.DELETE.SELECT方法需要ID参数支持
            object[][] rowsKeyValues = HttpCommon.GetEntityKeyValues(context);

            return DataCommon.EntityStatusInfo(objEntity, bllObject, rowsKeyValues, actionName, LoginUser).ToString();
        }
    }
}
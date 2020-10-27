namespace UI.WEB.COMMON
{
    using BLL.SYS;
    using DM.SYS;
    using Infrustructure.Data;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Linq;
    using System.Reflection;
    public class SYSHandler
    {
        UserBLL userBll = new UserBLL();
        RoleBLL roleBll = new RoleBLL();
        MenuBLL menuBll = new MenuBLL();
        ActionBLL actionBll = new ActionBLL();
        MessageBLL messageBll = new MessageBLL();

        public string loginUserName
        {
            get
            {
                return HttpContext.Current.Session["loginUserName"].ToString();
            }
        }

        /// <summary>
        /// 获取用户对应的角色
        /// </summary>
        /// <param name="context"></param>
        public static string GetRoles(HttpContext context)
        {
            List<GuidValueDatasourceInfo> list = NewMethod();
            string language = context.Request["language"];
            HttpContext.Current.Session["nowLanguage"] = language;
            return JsonHelper.ToJson(list);
        }

        private static List<GuidValueDatasourceInfo> NewMethod()
        {
            return new RoleBLL().GetUserRoles(HandlerCommon.UserFid);
        }

        public string GetUserRoleByUserFidRoleFid(HttpContext context)
        {
            try
            {
                string userFid = context.Session["UserFid"].ToString();
                string rolesFid = context.Request["RolesFid"];

                List<UserRoleInfo> userRoleList = new List<UserRoleInfo>();
                if (userFid != Guid.Empty.ToString() && rolesFid != null && rolesFid.ToString() != "")
                {
                    userRoleList = roleBll.GetUserRoleList(Guid.Parse(userFid), Guid.Parse(rolesFid.ToString()));
                }
                return JsonHelper.ToJson(userRoleList); ;
            }
            catch (Exception ex)
            {
                return HandlerCommon.ExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetUserRole(HttpContext context)
        {
            ///USER_FID
            string userFid = context.Request["key"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(userFid))
                throw new Exception("参数获取错误");
            if (userFid.Length != 36)
                throw new Exception("参数获取错误");
            ///PAGE
            int rowIndex = 1;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["page"]))
                rowIndex = Convert.ToInt32(context.Request["page"]);
            ///ROWS
            int maxRow = int.MaxValue;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["rows"]))
                maxRow = Convert.ToInt32(context.Request["rows"]);
            ///sort & order
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"])
                && !HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
            {
                orderText = context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
                orderText += " " + context.Request["order"];
            }
            int dataTotal = 0;
            List<UserRoleInfo> userrolelist = userBll.GetRolesByUser(Guid.Parse(userFid), orderText, rowIndex, maxRow, out dataTotal);
            return @"{""rows"":" + JsonHelper.ToJson(userrolelist) + @",""total"":" + dataTotal + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string GetUserRoleInfos(HttpContext context)
        {
            ///USER_FID
            string filter = context.Request["FILTER"];
            string userFid = CommonBLL.GetFieldValue(filter, "USER_FID");
            if (HttpCommon.IsNullOrEmptyOrUndefined(userFid))
                throw new Exception("参数获取错误");
            if (userFid.Length != 36)
                throw new Exception("参数获取错误");
            List<GuidValueDatasourceInfo> roleInfos = new RoleBLL().GetUserRoles(Guid.Parse(userFid));
            return JsonHelper.ToJson(roleInfos);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetRoleAuthData(HttpContext context)
        {
            string roleFid = context.Request["key"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(roleFid))
                throw new Exception("参数获取错误");
            if (roleFid.Length != 36)
                throw new Exception("参数获取错误");
            List<RoleAuthInfo> roleauthlist = roleBll.GetRoleAuthList(Guid.Parse(roleFid));
            var resultData = JsonHelper.ToJson(roleauthlist);
            int dataTotal = roleauthlist.Count;
            return @"{""rows"":" + resultData + @",""total"":" + dataTotal + @"}";
        }

        public string SetRoleAuth(HttpContext context)
        {
            try
            {
                var roleFid = context.Request["key"];
                var selectFidItem = context.Request["SELECT_FIDITEM"];
                bool isSet = bool.Parse(context.Request["IS_SET"].ToString());

                var selFidItem = selectFidItem.Split(',');
                List<Guid> authSourceFids = new List<Guid>();
                for (int i = 0; i < selFidItem.Length; i++)
                {
                    authSourceFids.Add(Guid.Parse(selFidItem[i]));
                }
                UserInfo userinfo = MPSCache.Get(context.Session["UserFid"].ToString()) as UserInfo;
                string loginUser = userinfo.LoginName;
                var result = roleBll.SetRoleAuth(Guid.Parse(roleFid), authSourceFids, isSet, loginUser);
                return result == true ? "MC:0x00000069" : "Err_:MC:0x00000108";
            }
            catch (System.Exception ex)
            {
                return HandlerCommon.ExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string AddUserRole(HttpContext context)
        {
            UserRoleInfo userRoleInfo = HttpCommon.GetEntityObject(context) as UserRoleInfo;
            object[][] rowsKeyValues = HttpCommon.GetEntityKeyValues(context);
            return new UserRoleBLL().AddUserRole(
                long.Parse(rowsKeyValues[0][0].ToString()),
                userRoleInfo,
                HandlerCommon.LoginUser
                ).ToString();
        }
        /// <summary>
        /// 删除用户收藏夹内容
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string DeleteUserFavorite(HttpContext context)
        {
            ///菜单外键
            string menuFid = context.Request["menuFid"];
            ///用户外键
            Guid userFid = HandlerCommon.UserFid;
            ///角色外键
            Guid roleFid = HandlerCommon.RoleFid;
            return new UserFavoritesBLL().DelFavorite(userFid, roleFid, Guid.Parse(menuFid)).ToString();
        }
        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string AddUserFavorite(HttpContext context)
        {
            ///菜单外键
            string menuFid = context.Request["menuFid"];
            ///用户外键
            Guid userFid = HandlerCommon.UserFid;
            ///角色外键
            Guid roleFid = HandlerCommon.RoleFid;
            return new UserFavoritesBLL().AddFavorite(userFid, roleFid, Guid.Parse(menuFid)).ToString();
        }
        /// <summary>
        /// 加载用户收藏夹
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetUserFavorites(HttpContext context)
        {
            ///用户外键
            Guid userFid = HandlerCommon.UserFid;
            ///角色外键
            Guid roleFid = HandlerCommon.RoleFid;
            ///
            List<UserFavoritesInfo> userFavorites = new UserFavoritesBLL().GetList("" +
                "[USER_FID] = N'" + userFid + "' and [ROLE_FID] = N'" + roleFid + "'", string.Empty);
            ///
            return @"{""rows"":" + JsonHelper.ToJson(userFavorites) + @"}";
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string SetDefaultPassWord(HttpContext context)
        {
            string pwd = context.Request["password"];
            if (string.IsNullOrEmpty(pwd))
                pwd = string.Empty;////重置密码

            string userFid = context.Request["key"];
            if (string.IsNullOrEmpty(userFid))
                userFid = HandlerCommon.UserFid.ToString();

            return new UserBLL().ResetPassword(userFid, pwd).ToString();
        }
        /// <summary>
        /// 根据用户角色获取左侧菜单项数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetLoadMenu(HttpContext context)
        {
            string userRoleFid = context.Request["ROLE_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(userRoleFid))
                throw new Exception("Err_:SessionIsNull");
            List<MenuInfo> menulist = new List<MenuInfo>();
            ///写入SESSION，保存到缓存
            HttpContext.Current.Session["RoleFid"] = userRoleFid.ToString();
            MPSCache.Insert("RoleFid", userRoleFid.ToString(), 1800);
            menulist = menuBll.GetMenusByRoleFid(Guid.Parse(userRoleFid.ToString()));
            return JsonHelper.ToJson(menulist);
        }
        /// <summary>
        /// 获取MENU_ACTION的数据源，拼接ACTION部分属性作为显示用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetMenuAcionDataByFid(HttpContext context)
        {
            string textWhere = context.Request["FILTER"];
            ///MENU_FID
            string menuFid = context.Request["key"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(menuFid))
                throw new Exception("参数错误");
            if (menuFid.Length != 36)
                throw new Exception("参数错误");
            ///PAGE_INDEX
            int pageIndex = 1;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["page"]))
                int.TryParse(context.Request["page"].ToString(), out pageIndex);
            ///PAGE_ROW
            int pageRow = int.MaxValue;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["rows"]))
                int.TryParse(context.Request["rows"].ToString(), out pageRow);
            ///ORDER_TEXT
            string orderText = string.Empty;
            ///
            int dataTotal = 0;
            ///获取菜单对应的按钮信息
            List<ActionInfo> actionList = new ActionBLL().GetActionListByMenuFid(Guid.Parse(menuFid)
                , textWhere
                , orderText
                , pageIndex
                , pageRow
                , out dataTotal);
            return @"{""rows"":" + JsonHelper.ToJson(actionList) + @",""total"":" + dataTotal + @"}";
        }

        #region Menu Action
        /// <summary>
        /// 设置动作-设置
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string SetMenuAction(HttpContext context)
        {
            ///ACTION_FID
            string actionFid = context.Request["ACTION_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(actionFid))
                throw new Exception("MC:0x00000053");///请选中行数据

            ///MENU_FID
            string menutFid = context.Request["MENU_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(menutFid))
                throw new Exception("MC:0x00000053");///请选中行数据

            ///CLIENT_JS
            string clientJs = context.Request["ClientJs"];
            ///ACTION_ORDER
            string actionOrder = context.Request["DisplayOrder"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(actionOrder))
                throw new Exception("MC:0x00000084");///数据错误
            if (!int.TryParse(actionOrder, out int intActionOrder))
                throw new Exception("MC:0x00000084");///数据错误

            ///是否需要授权
            string needAuth = context.Request["NeedAuth"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(needAuth))
                throw new Exception("MC:0x00000084");///数据错误
            bool boolNeedAuth = false;
            if (needAuth == "30") boolNeedAuth = true;
            if (needAuth == "true") boolNeedAuth = true;

            ///详情标记
            string detailFlag = context.Request["DetailFlag"];
            //if (HttpCommon.IsNullOrEmptyOrUndefined(detailFlag))
            //    throw new Exception("MC:0x00000084");///数据错误
            bool boolDetailFlag = false;
            if (detailFlag == "30") boolDetailFlag = true;
            if (detailFlag == "true") boolDetailFlag = true;

            ///设置动作
            bool result = new ActionBLL().SetMenuAction(
                Guid.Parse(menutFid),
                Guid.Parse(actionFid),
                clientJs,
                intActionOrder,
                boolNeedAuth,
                boolDetailFlag,
                HandlerCommon.LoginUser);

            return result.ToString();
        }
        /// <summary>
        /// 设置动作-取消
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CancelMenuAction(HttpContext context)
        {
            ///ACTION_FID
            string actionFid = context.Request["ACTION_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(actionFid))
                throw new Exception("MC:0x00000053");///请选中行数据

            ///MENU_FID
            string menutFid = context.Request["MENU_FID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(menutFid))
                throw new Exception("MC:0x00000053");///请选中行数据

            ///取消动作
            bool result = actionBll.ClearMenuAction(Guid.Parse(menutFid)
                    , Guid.Parse(actionFid)
                    , HandlerCommon.LoginUser);
            return result.ToString();
        }
        #endregion

        public string GetOrganization(HttpContext context)
        {
            try
            {
                string strReturn = string.Empty;
                var entityName = context.Request["entity"];
                var isAll = context.Request["isall"];
                string Field = context.Request["fid"];

                switch (entityName)
                {
                    case "Role":

                        strReturn = Infrustructure.Data.JsonHelper.ToJson(roleBll.GetDataSource());

                        break;

                    default:
                        break;
                }
                return strReturn;
            }
            catch (System.Exception ex)
            {
                return HandlerCommon.ExceptionMessage(ex);
            }
        }

        public string GetMessage(HttpContext context)
        {
            //meeeage_EN
            //string strReturn = "{code0x000001:{cn:'动作名称已存在!',en:'Action Name is Existed!'},code0x0000002:{cn:'提示信息已存在.',en:'Message is Existed.'}}";
            string strReturn = messageBll.GetListMessage();
            return strReturn;
        }

        /// <summary>
        /// 校验正则表达式及字段长度
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetRegexResult(HttpContext context)
        {
            try
            {
                string regex = context.Request["REG"];
                if (string.IsNullOrEmpty(regex))
                    return "true";
                string inputValue = context.Request["value"];
                bool result = Regex.IsMatch(inputValue, regex);
                ///校验字段长度
                string dataLength = context.Request["DATA_LENGTH"];
                if (string.IsNullOrEmpty(dataLength))
                    dataLength = "0";
                ///0不校验字段长度
                if (inputValue.Length > Convert.ToInt32(dataLength) && !dataLength.Equals("0"))
                    result = false;
                return result.ToString().ToLower();
            }
            catch (Exception ex)
            {
                return HandlerCommon.ExceptionMessage(ex);
            }
        }

        public string getChart(HttpContext context)
        {

            string fid = context.Request["FID"];
            string filter = string.Empty;
            if (fid != null && fid != string.Empty)
            {
                filter = " AND FID='" + fid + "'";
            }
            string strReturn = new ChartBLL().GetChartData(filter);
            return strReturn;
        }
        public string getData(HttpContext context)
        {

            string sqlString = context.Request["sqlString"];

            string strReturn = new ChartBLL().getDataBySql(sqlString);
            return strReturn;
        }


        #region report
        public string getReport(HttpContext context)
        {

            string fid = context.Request["FID"];
            string filter = string.Empty;
            if (fid != null && fid != string.Empty)
            {
                filter = " AND FID='" + fid + "'";
            }
            string strReturn = new ReportBLL().GetReportData(filter);
            return strReturn;
        }
        public string GetTableData(HttpContext context)
        {
            string sqlString = context.Request["sqlString"];
            string isCol = context.Request["isCol"];
            if (isCol == null)
            {
                isCol = string.Empty;
            }
            return new ReportBLL().GetTableData(sqlString, isCol);
        }
        #endregion


        public string GetActionByMenuFid(HttpContext context)
        {
            try
            {
                string menuFid = context.Request["menuFid"];
                int rowIndex = 1;
                int maxRow = int.MaxValue;
                string orderText = string.Empty;
                int dataTotal = 0;
                List<ActionInfo> list = new ActionBLL().GetListByPage("and [VALID_FLAG] <> 0 "
                    + "and [FID] in (select [ACTION_FID] from dbo.[TS_SYS_MENU_ACTION] with(nolock) where [MENU_FID] = '" + menuFid + "' and [VALID_FLAG] <> 0)"
                    , "[ID] desc", rowIndex, maxRow, out dataTotal);

                return @"{""rows"":" + JsonHelper.ToJson(list) + @",""total"":" + dataTotal + @"}";
            }
            catch (Exception ex)
            {
                return HandlerCommon.ExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 根据FID获取MENU对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetMenuByFid(HttpContext context)
        {
            string menuFid = context.Request["menuFid"];
            if (string.IsNullOrEmpty(menuFid))
                throw new Exception("MC:0x00000084");
            MenuInfo menuinfo = new MenuBLL().GetInfo(Guid.Parse(menuFid));
            if (menuinfo == null)
                throw new Exception("MC:0x00000084");
            return JsonHelper.ToJson(menuinfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetMenuByPidName(HttpContext context)
        {
            string parentFid = context.Request["PID"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(parentFid))
                throw new Exception("需要父节点ID字段");
            if (parentFid.Length != 36)
                throw new Exception("需要父节点ID字段");
            string name = context.Request["NAME"];
            if (HttpCommon.IsNullOrEmptyOrUndefined(name))
                throw new Exception("需要菜单名称");
            MenuInfo menuinfo = new MenuBLL().GetInfo(name, Guid.Parse(parentFid));
            if (menuinfo == null)
                throw new Exception("MC:0x00000084");
            return JsonHelper.ToJson(menuinfo);
        }
        public static bool ValidateFields(object obj, string entityName)
        {
            List<EntityFieldInfo> entityFieldList = new EntityBLL().GetGridFieldByEntityName(entityName);
            bool isTrue = false;
            string bllName = "";
            string idField = "";
            string nameSpace = "";
            string sqlFilter = "";
            int i = 0;
            foreach (var entityField in entityFieldList)
            {
                foreach (var propertity in obj.GetType().GetProperties())
                {
                    if (propertity.Name == entityField.FieldName)
                    {
                        var value = propertity.GetValue(obj, null);
                        if (value != null && value.ToString().Contains(","))
                        {
                            continue;
                        }
                        if (entityField.Nullenable != true && value == null)
                        {
                            return false;
                        }
                        if (!String.IsNullOrEmpty(entityField.Regex))
                        {
                            Regex r = new Regex(entityField.Regex);
                            if (r.IsMatch(value.ToString()) == false)
                            {
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(entityField.Extend3) && entityField.Extend3.Contains("sql^对象名:"))
                        {
                            i++;
                            var extend3 = entityField.Extend3;
                            var paramValue = extend3.Split('|')[0];
                            var paramValueItem = paramValue.Split(',');
                            foreach (var param in paramValueItem)
                            {
                                if (param.Contains("sql^对象名:"))
                                {
                                    bllName = param.Replace("sql^对象名:", "").Trim();
                                    if (bllName.IndexOf("_") != -1)
                                    {
                                        bllName = bllName.Substring(0, bllName.IndexOf("_")) + "BLL";

                                    }
                                    else
                                    {
                                        bllName += "BLL";
                                    }
                                    continue;
                                }
                                if (param.Contains("绑定字段:"))
                                {
                                    idField = param.Replace("绑定字段:", "").Trim();
                                    continue;
                                }
                                if (param.Contains("命名空间名:"))
                                {
                                    nameSpace = param.Replace("命名空间名:", "").Trim();
                                    continue;
                                }
                                if (param.Contains("数据库条件:"))
                                {
                                    sqlFilter = param.Replace("数据库条件:", "").Trim();
                                }
                            }

                            object bllObject = DataCommon.GetClassObject(nameSpace, bllName);
                            int dataCount = 0;
                            MethodInfo met = bllObject.GetType().GetMethod("GetListByPage");
                            object[] pars = new object[] { sqlFilter, "", 1, 10000, dataCount };
                            object objList = met.Invoke(bllObject, pars);
                            IEnumerable<object> list = objList as IEnumerable<object>;

                            foreach (var entity in list)
                            {
                                foreach (PropertyInfo pro in entity.GetType().GetProperties())
                                {
                                    if (pro.Name == idField)
                                    {
                                        if (pro.GetValue(entity, null).ToString() == value.ToString())
                                        {
                                            isTrue = true;
                                            break;
                                        }
                                        break;
                                    }
                                }
                                if (isTrue == true)
                                {
                                    break;
                                }
                            }
                            bllName = "";
                            idField = "";
                            nameSpace = "";
                            sqlFilter = "";
                            if (isTrue == false)
                            {
                                return false;
                            }
                        }
                        break;
                    }

                    else continue;

                }
            }
            if (i == 0)
            {
                isTrue = true;
            }
            return isTrue;
        }
        /// <summary>
        /// 根据状态代码获取CONFIRM提示消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetStatusTypeConfirmMessage(HttpContext context)
        {
            //对于一个状态类型需要在系统代码SET_STATUS_TYPE中配置相应的项
            ///并将系统代码中的ITEM_VALUE与系统提示信息中的7x起始的消息代码最后几位进行匹配
            string actionName = context.Request["actionname"];
            ///默认en-us
            string nowLangue = "en-us";
            if (HttpContext.Current.Session["nowLanguage"] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Session["nowLanguage"].ToString()))
                nowLangue = HttpContext.Current.Session["nowLanguage"].ToString();
            ///
            string actionConfirmMessage = GetActionConfirmMessage(actionName, nowLangue);
            return @"{""message"":" + JsonHelper.ToJson(actionConfirmMessage) + @",""actionname"":" + JsonHelper.ToJson(actionName) + @"}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetActionConfirmMessage(string actionName, string nowLangue)
        {
            int codeValue = new CodeItemBLL().GetValueByCodeItemName("SET_STATUS_TYPE", actionName.Replace("#", string.Empty));
            ///未配置设定状态类型
            if (codeValue == 0)
                throw new Exception("MC:7x00000000");
            string codeValueString = codeValue.ToString();
            ///#开始说明是撤销
            string messageCodeFlag = "7x0";
            if (actionName.StartsWith("#"))
                messageCodeFlag = "7x1";///TODO:添加所有7x1的MESSAGE
            return new MessageBLL().GetMessage(nowLangue, messageCodeFlag + codeValueString.PadLeft(7, '0'));
        }
        /// <summary>
        /// 导入页面的元素构建
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetImportFormElement(HttpContext context)
        {
            ///FORM的ACTION
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.ActionName = "import";
            actionInfo.ActionNameCn = "导入";
            actionInfo.ActionType = (int)ActionTypeConstants.WebAction;
            actionInfo.ClientJs = "importExcel()";
            actionInfo.Comments = string.Empty;
            actionInfo.DisplayOrder = 10;
            actionInfo.IconUrl = "icon-0000011";
            actionInfo.IsListAction = false;
            actionInfo.IsRelationed = true;
            actionInfo.NeedAuth = false;
            actionInfo.Fid = Guid.NewGuid();
            List<ActionInfo> actionInfos = new List<ActionInfo>();
            actionInfos.Add(actionInfo);
            string resultActionForm = JsonHelper.ToJson(actionInfos.Where(d => !d.IsListAction).ToList());
            ///EntityFieldInfo  获取实体字段在页面显示的信息
            EntityFieldInfo entityFieldInfo = new EntityFieldInfo();
            entityFieldInfo.FieldName = "ExcelFileName";
            entityFieldInfo.TableFieldName = "EXCEL_FILE_NAME";
            entityFieldInfo.DisplayNameCn = "请选择导入文件";
            entityFieldInfo.DisplayNameEn = "Pls Choose Import File";
            entityFieldInfo.DisplayOrder = 10;
            entityFieldInfo.DataType = (int)DataTypeConstants.STRING;
            entityFieldInfo.ControlType = (int)ControlTypeConstants.UPLOAD;
            entityFieldInfo.Editable = true;
            entityFieldInfo.EditDisplayWidth = "600";
            entityFieldInfo.Extend3 = "application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            entityFieldInfo.EditReadonly = (int)ReadonlyTypeConstants.NOTREADONLY;
            List<EntityFieldInfo> entityFieldList = new List<EntityFieldInfo>();
            entityFieldList.Add(entityFieldInfo);
            string resultEntityFields = JsonHelper.ToJson(entityFieldList);
            ///
            EntityInfo entityInfo = new EntityInfo();
            entityInfo.EntityName = "Import";
            entityInfo.TableNames = string.Empty;
            entityInfo.EntityType = (int)EntityTypeConstants.Grid;
            entityInfo.ParentField = string.Empty;
            entityInfo.DefaultSort = string.Empty;
            entityInfo.AuthConfig = string.Empty;
            entityInfo.TabTitles = string.Empty;
            entityInfo.KeyFields = string.Empty;
            //entityInfo.DefaultPagesize = null;
            //entityInfo.FooterFlag = null;
            //entityInfo.CheckOnSelect = null;
            //entityInfo.SelectOnCheck = null;
            string resultEntity = JsonHelper.ToJson(entityInfo);
            ///
            return @"{""entityfieldform"":" + resultEntityFields
                + @",""entityinfo"":" + resultEntity
                + @",""actionform"":" + resultActionForm + @"}";
        }

        #region Entity
        /// <summary>
        /// 获取Execel列的名称集合
        /// </summary>
        /// <param name="entityFieldInfos"></param>
        /// <param name="dicDropdowns">下拉菜单集合</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetExcelColumnNames(List<EntityFieldInfo> entityFieldInfos, ref Dictionary<string, Dictionary<string, string>> dicDropdowns)
        {
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            ///按当前语言环境获取标题
            foreach (EntityFieldInfo entityFieldInfo in entityFieldInfos)
            {
                if (string.IsNullOrEmpty(entityFieldInfo.TableFieldName)) continue;
                if (entityFieldInfo.ExportExcelOrder.GetValueOrDefault() == 0) continue;
                string tableFieldName = entityFieldInfo.TableFieldName;
                if (HandlerCommon.Language == "zh-cn")
                    columnNames.Add(entityFieldInfo.TableFieldName, entityFieldInfo.DisplayNameCn);
                else
                    columnNames.Add(entityFieldInfo.TableFieldName, string.IsNullOrEmpty(entityFieldInfo.DisplayNameEn) ? entityFieldInfo.TableFieldName : entityFieldInfo.DisplayNameEn);
                ///combocode
                if (entityFieldInfo.ControlType == (int)ControlTypeConstants.COMBOCODE && !string.IsNullOrEmpty(entityFieldInfo.Extend1))
                {
                    List<CodeItemDatasourceInfo> codeitems = new CodeBLL().GetDataSource(entityFieldInfo.Extend1);
                    Dictionary<string, string> selectiondic = new Dictionary<string, string>();
                    foreach (var codeitem in codeitems)
                    {
                        if (entityFieldInfo.Extend1.ToUpper() == "BOOLEAN")
                            selectiondic.Add(codeitem.ItemValue == 30 ? "True" : "False", codeitem.ItemDisplay);
                        else
                            selectiondic.Add(codeitem.ItemValue.ToString(), codeitem.ItemDisplay);
                    }
                    dicDropdowns.Add(entityFieldInfo.TableFieldName, selectiondic);
                    continue;
                }
                ///combobox&combogrid
                if ((entityFieldInfo.ControlType == (int)ControlTypeConstants.COMBOBOX || entityFieldInfo.ControlType == (int)ControlTypeConstants.COMBOGRID)
                    && !string.IsNullOrEmpty(entityFieldInfo.Extend3))
                {
                    if (entityFieldInfo.Extend2 == "GridNotFormatter") continue;
                    if (!entityFieldInfo.Extend3.StartsWith("sql^")) continue;
                    string jsonString = ConvertExtendToJson(entityFieldInfo.Extend3.Replace("sql^", string.Empty));
                    EntityFieldExtendInfo extendInfo = JsonHelper.FormJson<EntityFieldExtendInfo>(jsonString);
                    Dictionary<string, string> selectiondic = DataCommon.GetComboxItems(extendInfo.AN, extendInfo.entityName, extendInfo.tableName, extendInfo.idField, extendInfo.textField);
                    dicDropdowns.Add(entityFieldInfo.TableFieldName, selectiondic);
                    continue;
                }
                ///combotree
                if (entityFieldInfo.ControlType == (int)ControlTypeConstants.COMBOTREE && !string.IsNullOrEmpty(entityFieldInfo.Extend3))
                {
                    if (!entityFieldInfo.Extend3.StartsWith("sql^")) continue;
                    string jsonString = ConvertExtendToJson(entityFieldInfo.Extend3.Replace("sql^", string.Empty));
                    EntityFieldExtendInfo extendInfo = JsonHelper.FormJson<EntityFieldExtendInfo>(jsonString);
                    Dictionary<string, string> selectiondic = DataCommon.GetComboTreeItems(extendInfo.AN, extendInfo.entityName, extendInfo.tableName, extendInfo.idField, extendInfo.textField, extendInfo.parentId);
                    dicDropdowns.Add(entityFieldInfo.TableFieldName, selectiondic);
                    continue;
                }
            }
            return columnNames;
        }
        /// <summary>
        /// Extend3 To Json
        /// </summary>
        /// <param name="extend3"></param>
        /// <returns></returns>
        public static string ConvertExtendToJson(string extend3)
        {
            if (extend3.IndexOf("|") > 0) extend3 = extend3.Substring(0, extend3.IndexOf("|"));
            ///把主体配置条件转换为JSON对象
            string jsonItem = extend3.Replace("对象名", "\"entityName\"");
            jsonItem = jsonItem.Replace("数据库表名", "\"tableName\"");///用于EXCEL模板
            jsonItem = jsonItem.Replace("绑定字段", "\"idField\"");
            jsonItem = jsonItem.Replace("显示字段", "\"textField\"");
            jsonItem = jsonItem.Replace("命名空间名", "\"AN\"");
            jsonItem = jsonItem.Replace("函数名", "\"ajaxMethod\"");
            jsonItem = jsonItem.Replace("控件类型", "\"controlType\"");
            jsonItem = jsonItem.Replace("属性名称", "\"attributeName\"");
            ///sqlFilter可以考虑合并至complexFilter
            jsonItem = jsonItem.Replace("数据库条件", "\"sqlFilter\"");
            jsonItem = jsonItem.Replace("复杂过滤条件", "\"complexFilter\"");
            ///2017-05-22添加数据库排序条件
            jsonItem = jsonItem.Replace("数据库默认排序", "\"sqlSort\"");
            ///2017-09-12 多选开关
            jsonItem = jsonItem.Replace("是否多选", "\"isMultiple\"");
            ///2018-04-17 有条件进行联动逻辑
            jsonItem = jsonItem.Replace("联动逻辑属性", "\"linkageAttribute\"");
            jsonItem = jsonItem.Replace("联动逻辑条件", "\"linkageLogic\"");
            jsonItem = jsonItem.Replace("联动比对值", "\"linkageCompareValue\"");
            ///Linkage专用属性
            jsonItem = jsonItem.Replace("数据属性名称", "\"dataAttributeName\"");
            jsonItem = jsonItem.Replace("字段名称", "\"fieldName\"");
            jsonItem = jsonItem.Replace("只读标记", "\"readonlyFlag\"");
            ///Tree专用属性
            jsonItem = jsonItem.Replace("父节点字段", "\"parentId\"");
            ///Grid专用属性
            jsonItem = jsonItem.Replace("排序名", "\"sortName\"");
            jsonItem = jsonItem.Replace("排序方式", "\"sortOrder\"");
            jsonItem = jsonItem.Replace("列", "\"columns\"");
            jsonItem = jsonItem.Replace("前台过滤条件", "\"comboFilter\"");
            ///日期控件
            jsonItem = jsonItem.Replace("日期时间格式", "\"datetimeFormat\"");
            jsonItem = jsonItem.Replace("日期格式", "\"dateFormat\"");
            jsonItem = jsonItem.Replace("时间格式", "\"timeFormat\"");
            jsonItem = jsonItem.Replace("日期字段", "\"dateFieldName\"");
            jsonItem = jsonItem.Replace("时间字段", "\"timeFieldName\"");
            jsonItem = jsonItem.Replace("保存文本字段", "\"textFieldName\"");
            jsonItem = jsonItem.Replace("是否显示秒数", "\"showSeconds\"");
            ///数值计算
            jsonItem = jsonItem.Replace("计算公式", "\"formular\"");
            jsonItem = jsonItem.Replace("公式参数", "\"formularParam\"");
            ///单元格样式
            jsonItem = jsonItem.Replace("样式值", "\"stylerValue\"");
            jsonItem = jsonItem.Replace("背景颜色", "\"stylerBackColor\"");
            jsonItem = jsonItem.Replace("字体颜色", "\"stylerColor\"");
            ///2018-5-9 扩展属性
            jsonItem = jsonItem.Replace("数据变更后函数", "\"changeMethodName\"");

            jsonItem = jsonItem.Replace(":", ":\"");
            jsonItem = jsonItem.Replace(",", "\",");
            return "{" + jsonItem + "\"}";
        }
        #endregion

        #region RangeAuth
        /// <summary>
        /// 获取权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRangeAuth(HttpContext context)
        {
            ///
            string textWhere = context.Request["FILTER"];
            if (string.IsNullOrEmpty(textWhere))
                textWhere = "and 1=0 ";
            ///URL_FILTER
            string urlFilter = context.Request["URL_FILTER"];
            if (!HttpCommon.IsNullOrEmptyOrUndefined(urlFilter))
            {
                string[] urlFilters = urlFilter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var urlFilterCondition in urlFilters)
                {
                    textWhere += " and " + urlFilterCondition.Replace("USER_FID=", "[USER_FID] = N") + " ";
                }
            }
            ///PAGE
            int rowIndex = 1;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["page"]))
                rowIndex = Convert.ToInt32(context.Request["page"]);
            ///ROWS
            int maxRow = int.MaxValue;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["rows"]))
                maxRow = Convert.ToInt32(context.Request["rows"]);
            ///sort & order
            string orderText = string.Empty;
            if (!HttpCommon.IsNullOrEmptyOrUndefined(context.Request["sort"])
                && !HttpCommon.IsNullOrEmptyOrUndefined(context.Request["order"]))
            {
                orderText = context.Request["sort"];
                if (!orderText.Contains("_"))
                    orderText = DataCommon.GetFieldName(orderText.Trim());
                orderText += " " + context.Request["order"];
            }
            textWhere = textWhere.Replace("^", "'");
            string roleFid = CommonBLL.GetFieldValue(textWhere.Replace("and ", ","), "ROLE_FID");
            if (string.IsNullOrEmpty(roleFid)) roleFid = Guid.Empty.ToString();
            string conditionFid = CommonBLL.GetFieldValue(textWhere.Replace("and ", ","), "CONDITION_FID");
            if (string.IsNullOrEmpty(conditionFid)) conditionFid = Guid.Empty.ToString();
            string userFid = CommonBLL.GetFieldValue(textWhere.Replace("and ", ","), "USER_FID");
            if (string.IsNullOrEmpty(userFid)) userFid = Guid.Empty.ToString();
            textWhere = textWhere.Replace("and [ROLE_FID] = N'" + roleFid + "'", string.Empty).Trim();
            textWhere = textWhere.Replace("[ROLE_FID] = N'" + roleFid + "'", string.Empty).Trim();
            textWhere = textWhere.Replace("and [CONDITION_FID] = N'" + conditionFid + "'", string.Empty).Trim();
            textWhere = textWhere.Replace("[CONDITION_FID] = N'" + conditionFid + "'", string.Empty).Trim();
            textWhere = textWhere.Replace("and [USER_FID] = N'" + userFid + "'", string.Empty).Trim();
            textWhere = textWhere.Replace("[USER_FID] = N'" + userFid + "'", string.Empty).Trim();
            List<RangeAuthInfo> rangeAuthInfos = new RangeAuthBLL().GetRangeAuthList(
                Guid.Parse(userFid),
                Guid.Parse(roleFid),
                Guid.Parse(conditionFid),
                textWhere,
                orderText,
                rowIndex,
                maxRow,
                out int dataCount);
            return @"{""rows"":" + JsonHelper.ToJson(rangeAuthInfos) + @",""total"":" + dataCount + @"}";
        }
        /// <summary>
        /// 添加权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string SetRangeAuth(HttpContext context)
        {
            string roleFid = context.Request["RoleFid"];
            if (string.IsNullOrEmpty(roleFid))
                throw new Exception("MC:0x00000084");///数据错误
            string conditionFid = context.Request["ConditionFid"];
            if (string.IsNullOrEmpty(conditionFid))
                throw new Exception("MC:0x00000084");///数据错误
            string userFid = context.Request["UserFid"];
            if (string.IsNullOrEmpty(userFid))
                throw new Exception("MC:0x00000084");///数据错误
            string keys = context.Request["key"];
            string[] keyArray = keys.Split(new string[] { "-|" }, StringSplitOptions.RemoveEmptyEntries);
            if (keyArray.Length == 0)
                throw new Exception("MC:0x00000084");///数据错误
            bool result = new RangeAuthBLL().SetRangeAuth(keyArray, Guid.Parse(userFid), Guid.Parse(roleFid), Guid.Parse(conditionFid), HandlerCommon.LoginUser);
            return result.ToString();
        }
        /// <summary>
        /// 设定全部权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string AllRangeAuth(HttpContext context)
        {
            string roleFid = context.Request["RoleFid"];
            if (string.IsNullOrEmpty(roleFid))
                throw new Exception("MC:0x00000084");///数据错误
            string conditionFid = context.Request["ConditionFid"];
            if (string.IsNullOrEmpty(conditionFid))
                throw new Exception("MC:0x00000084");///数据错误
            string userFid = context.Request["UserFid"];
            if (string.IsNullOrEmpty(userFid))
                throw new Exception("MC:0x00000084");///数据错误

            bool result = new RangeAuthBLL().SetRangeAuth(new string[] { }, Guid.Parse(userFid), Guid.Parse(roleFid), Guid.Parse(conditionFid), HandlerCommon.LoginUser);
            return result.ToString();
        }
        /// <summary>
        /// 删除权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string DelRangeAuth(HttpContext context)
        {
            string roleFid = context.Request["RoleFid"];
            if (string.IsNullOrEmpty(roleFid))
                throw new Exception("MC:0x00000084");///数据错误
            string conditionFid = context.Request["ConditionFid"];
            if (string.IsNullOrEmpty(conditionFid))
                throw new Exception("MC:0x00000084");///数据错误
            string userFid = context.Request["UserFid"];
            if (string.IsNullOrEmpty(userFid))
                throw new Exception("MC:0x00000084");///数据错误
            string keys = context.Request["key"];
            string[] keyArray = keys.Split(new string[] { "-|" }, StringSplitOptions.RemoveEmptyEntries);
            if (keyArray.Length == 0)
                throw new Exception("MC:0x00000084");///数据错误
            bool result = new RangeAuthBLL().DelRangeAuth(keyArray, Guid.Parse(userFid), Guid.Parse(roleFid), Guid.Parse(conditionFid), HandlerCommon.LoginUser);
            return result.ToString();
        }
        /// <summary>
        /// 撤销全部权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string NoneRangeAuth(HttpContext context)
        {
            string roleFid = context.Request["RoleFid"];
            if (string.IsNullOrEmpty(roleFid))
                throw new Exception("MC:0x00000084");///数据错误
            string conditionFid = context.Request["ConditionFid"];
            if (string.IsNullOrEmpty(conditionFid))
                throw new Exception("MC:0x00000084");///数据错误
            string userFid = context.Request["UserFid"];
            if (string.IsNullOrEmpty(userFid))
                throw new Exception("MC:0x00000084");///数据错误
            bool result = new RangeAuthBLL().DelRangeAuth(new string[] { }, Guid.Parse(userFid), Guid.Parse(roleFid), Guid.Parse(conditionFid), HandlerCommon.LoginUser);
            return result.ToString();
        }

        /// <summary>
        /// 获取权限条件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAuthCondition(HttpContext context)
        {
            string roleFid = context.Request["roleFid"];
            if (string.IsNullOrEmpty(roleFid))
                throw new Exception("MC:0x00000084");///数据错误
            HttpContext.Current.Session["RoleFid"] = roleFid;
            ///用户角色对应的组织结构
            List<Guid> organizationFids = new UserRoleBLL().GetUserRoleOrganizationFids(HandlerCommon.UserFid, HandlerCommon.RoleFid);
            organizationFids = new OrganizationBLL().GetOrganizationFidsByParentFids(organizationFids);
            string organizationFidCondition = "1=0";
            if (organizationFids.Count > 0)
                organizationFidCondition = "^" + string.Join("^,^", organizationFids.ToArray()) + "^";
            List<RangeAuthConditionInfo> rangeAuthConditionInfos = new List<RangeAuthConditionInfo>();
            List<UserRoleRangeAuthInfo> userRoleRangeAuthInfos = null;// new UserRoleRangeAuthBLL().GetUserRangeAuths(HandlerCommon.UserFid, HandlerCommon.RoleFid, out rangeAuthConditionInfos);
            string rangeAuthConditions = string.Empty;
            foreach (RangeAuthConditionInfo rangeAuthConditionInfo in rangeAuthConditionInfos)
            {
                List<UserRoleRangeAuthInfo> userRoleRangeAuths = userRoleRangeAuthInfos.Where(d => d.ConditionFid.GetValueOrDefault() == rangeAuthConditionInfo.Fid.GetValueOrDefault()).ToList();
                if (userRoleRangeAuthInfos.Count == 0)
                {
                    rangeAuthConditions += @",""" + rangeAuthConditionInfo.ConditionName + "" + @""":""1=0""";
                    continue;
                }
                if (userRoleRangeAuths.Count == 1)
                {
                    if (userRoleRangeAuths[0].ConditionContext == "1=1")
                    {
                        rangeAuthConditions += @",""" + rangeAuthConditionInfo.ConditionName + "" + @""":""1=1""";
                        continue;
                    }
                }
                switch (rangeAuthConditionInfo.DataType.GetValueOrDefault())
                {
                    case (int)DataTypeConstants.STRING:
                        rangeAuthConditions += @",""" + rangeAuthConditionInfo.ConditionName + "" + @""":""^" + string.Join("^,^", userRoleRangeAuths.Select(d => d.ConditionContext).ToArray()) + @"^""";
                        break;
                    case (int)DataTypeConstants.INT:
                        rangeAuthConditions += @",""" + rangeAuthConditionInfo.ConditionName + "" + @""":""" + string.Join(",", userRoleRangeAuths.Select(d => d.ConditionContext).ToArray()) + @"""";
                        break;
                    default:
                        rangeAuthConditions += @",""" + rangeAuthConditionInfo.ConditionName + "" + @""":""1=0""";
                        break;
                }
            }
            return @"{""LoginUser"":""^" + HandlerCommon.LoginUser + "^"
                + @""",""OrganizationFid"":""" + organizationFidCondition.Replace("'", "^")
                + @"""" + rangeAuthConditions
                + @",""RangeName"":" + JsonHelper.ToJson(rangeAuthConditionInfos.Select(d => d.ConditionName).ToList()) + @"}";
        }
        #endregion
    }
}
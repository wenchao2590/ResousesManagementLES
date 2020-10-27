var divActionMenuId = "divActionMenu";//页面MENU表单容器ID
var divActionDetailId = "divActionDetail";//页面MENU表单容器ID
var divSearchFormId = "divSearchForm";//页面FILTER表单容器ID
var divControlFormId = "divControlForm";//页面FILTER表单容器ID
var tblGridId = "tblGrid";//页面GRID表单容器ID
var arrLanguage = new Map();//国际化
///登录页面
var loginUrl = 'Login.aspx';

///获取URL地址：pathName ，实体名称：entityName ， 表名称：tableName ， 方法名称：methodName ， BLL命名空间名称：bllProjectName
function locationParameters() {
    ///实体名
    this.entityName = '';
    ///数据库表名
    this.tableName = '';
    ///获取实体的函数
    this.methodName = '';
    ///链接地址
    this.pathName = '';
    ///BLL名
    this.bllProjectName = '';
    ///是否弹出页面
    this.isEditPage = false;
    ///菜单的FID(非弹出窗体)
    this.menuFid = '';
    ///URL过滤条件
    this.urlFilter = '';
    ///主键字段名
    this.tableKeyField = '';
    ///主键字段长度
    this.tableKeyFieldLength = '32';
    ///逻辑删除字段
    this.logicDeleteField = '';
    ///弹出框主从表结构中的从表实体名
    this.detailEntityName = '';
    ///明细数据获取函数
    this.detailRelationKeyFieldName = '';
    ///明细数据获取函数
    this.detailGetDataMethod = '';

    ///解析URL中的参数
    this.getURL = function () {
        ///实体名&数据库表名&BLL名&主键a|主键b&主键a数据类型|主键b数据类型&逻辑删除字段
        var paramItems = window.location.search.replace('?', '').split('&');
        this.pathName = window.location.pathname.replace('/', '') + window.location.search;
        ///实体名
        if (paramItems.length > 0) {
            this.entityName = paramItems[0];
        }
        console.log(this.entityName);
        ///数据库表名
        if (paramItems.length > 1) {
            this.tableName = paramItems[1];
        }
        ///TM_BAS_PART表名的命名方式时,先将其以字符_分割成数组
        ///并获取其第二段的内容作为模块名称
        ///否则获取参数的第三段作为模块名称
        var arrayTableName = paramItems[1].split("_");
        if (paramItems.length > 2)
            ///指定BLL(可选)，如果是非标准格式表名则必选
            this.bllProjectName = paramItems[2];
        else
            this.bllProjectName = arrayTableName[1];
        ///主键字段名(可选)
        if (paramItems.length > 3) {
            this.tableKeyField = paramItems[3];
        }
        ///主键字段长度(可选)
        if (paramItems.length > 4) {
            this.tableKeyFieldLength = paramItems[4];
        }
        ///逻辑删除字段(可选)
        if (paramItems.length > 5) {
            this.logicDeleteField = paramItems[5];
        }
        ///URL过滤条件
        if (paramItems.length > 6) {
            this.urlFilter = paramItems[6];
        }
        ///获取ENTITY的特殊函数
        if (paramItems.length > 7) {
            this.methodName = paramItems[7];
        }
        ///
        if (parent.$('#tt').length > 0) {
            ///菜单对应的FID
            this.menuFid = parent.$('#tt').tabs('getSelected').panel('options').id;
        }
        ///是否弹出窗体
        if (window.location.pathname.indexOf('CommonEdit.aspx') > 0) {
            this.isEditPage = true;
            ///弹出窗体获取数据的函数
            if (paramItems.length > 1) {
                this.methodName = paramItems[1];
            }
            ///数据库表名
            if (paramItems.length > 2) {
                this.tableName = paramItems[2];
            }
            ///命名空间
            if (paramItems.length > 8) {
                this.bllProjectName = paramItems[8];
            }
        }
    }
    this.init = this.getURL();
}

function PageEntity() {
    //////URL.BEGIN/////////////////////////////////////////////
    var url = new locationParameters();
    ///实体名
    this.entityName = url.entityName;
    ///数据库表名
    this.tableName = url.tableName;
    ///获取实体的函数
    this.entityMethodName = url.methodName;
    ///链接地址
    this.pathName = url.pathName;
    ///BLL名
    this.bllProjectName = url.bllProjectName;
    ///是否弹出页面
    this.isEditPage = url.isEditPage;
    ///菜单的FID
    this.menuFid = url.menuFid;
    ///URL过滤条件
    this.urlFilter = url.urlFilter;
    ///主键字段名
    this.tableKeyField = url.tableKeyField;
    ///主键字段长度
    this.formDataKeyLength = url.tableKeyFieldLength;
    ///逻辑删除字段
    this.logicDeleteField = url.logicDeleteField;
    ///////////////////////////DETAIL/////////////////////////////
    ///弹出框主从表结构中的从表实体名
    this.detailEntityName = url.detailEntityName;
    ///主从关系字段
    this.detailRelationKeyFieldName = url.detailRelationKeyFieldName;
    ///明细数据获取函数
    this.detailGetDataMethod = url.detailGetDataMethod;
    ///明细数据主键
    this.detailTableKeyField = null;
    ///明细数据主键长度
    this.detailTableKeyLength = null;
    //////////////////////////DETAIL.END////////////////////////
    //////URL.END/////////////////////////////////////////////

    this.getDataToActionMethod = "ajaxActionOrSearch";
    this.getDataToControlMethod = "";
    this.getDataToUpdateMethod = "";

    ///默认获取数据函数
    this.getDataToGridMethod = "ajaxTables";
    ///默认获取数据网格的数据服务链接地址
    this.getDataToGridUrl = XR.defaultProcessUrl()
        + "method=" + this.getDataToGridMethod
        + "&ENTITY_NAME=" + this.entityName
        + "&AN=BLL." + this.bllProjectName
        + "&URL_FILTER=" + this.urlFilter;
    ///窗体数据主键值
    this.formDataKey = '';
    ///窗体数据条件SQL语句
    this.formParamKey = '';
    ///窗体数据对象
    this.formParamRowsData = '';
    ///触发弹出窗体的父实体
    this.parentEntityName = '';
    this.operationType = "";//操作类型:insert-TY_SYS_ACTION,update-TY_SYS_ACTION,select-TY_SYS_ACTION,delete-TY_SYS_ACTION
    this.formActionListData = "";
    this.formSearchListData = "";
    this.listSearchColumnLength = 0;
    this.formActionEditData = "";
    this.formSearchEditData = "";
    this.formSearchColumnLength = 0;
    this.formCreateData = "";
    this.formEntityInfo = "";
    this.formUpdateData = "";
    ///同EntityName弹出窗体的链接地址
    this.formUrl = '';

    this.editFormWidth = "";
    this.editFormHeight = "";
    this.gridCheckBox = false;
    ////////////////////////////////////////////////DETAIL/////////////////////////////////////////////
    ///明细表实体类对象
    this.detailEntityInfo = null;
    ///明细表实体属性集合
    this.detailEntityFields = [];
    ///明细表所选定的数据
    this.detailSelectRowData = null;
    ///创建弹出窗体中的从表数据网格
    this.FormCreateDetailGrid = function (obj) {
        if (!isValidData(obj.detailEntityName)) {
            var paramItems = window.location.search.replace('?', '').split('&');
            ///弹出框主从表结构中的从表实体名
            if (paramItems.length > 3) {
                obj.detailEntityName = paramItems[3];
            }
            ///主从关系字段
            if (paramItems.length > 4) {
                obj.detailRelationKeyFieldName = paramItems[4];
            }
            ///明细数据获取函数
            if (paramItems.length > 5) {
                obj.detailGetDataMethod = paramItems[5];
            }
            ///明细数据主键
            if (paramItems.length > 6) {
                obj.detailTableKeyField = paramItems[6];
            }
            ///明细数据主键长度
            if (paramItems.length > 7) {
                obj.detailTableKeyLength = paramItems[7];
            }
        }
        ///必须设置了关联字段
        if (!isValidData(obj.detailRelationKeyFieldName)) return;
        var dataParams = 'method=ajaxEntityFieldsData'
            + '&ENTITY_NAME=' + obj.detailEntityName;
        ///BLL命名空间
        var bllProject = "&AN=BLL." + obj.bllProjectName;
        ///数据地址
        var ajaxUrl = XR.defaultProcessUrl();
        ///如果获取函数为空则默认为ajaxTables
        if (!isValidData(obj.detailGetDataMethod))
            obj.detailGetDataMethod = 'ajaxTables';
        console.log("abc");
        $.ajax({
            url: ajaxUrl,
            async: false,
            type: "POST",
            data: dataParams + bllProject,
            dataType: 'json',
            success: function (data) {
                ///验证登录信息是否有效
                sessionIsNull(data);
                if (isSessionNull) return;
                ///ENTITY_FIELD
                if (isValidData(data.entityfields)) obj.detailEntityFields = data.entityfields;
                ///ENTITY
                if (isValidData(data.entityinfo)) obj.detailEntityInfo = data.entityinfo;
                ///SEARCH
                if (isValidData(data.formSearchData)) obj.formSearchEditData = data.formSearchData;
                else obj.formSearchEditData = [];
                ///SEARCH LENGTH
                if (isValidData(data.formSearchLength)) obj.formSearchColumnLength = data.formSearchLength;
                else obj.formSearchColumnLength = 0;

                var detailEntity = new PageEntity();
                detailEntity.entityName = obj.detailEntityName;
                detailEntity.tableName = obj.detailEntityInfo.TableNames;
                detailEntity.bllProjectName = obj.bllProjectName;
                detailEntity.formEntityInfo = obj.detailEntityInfo;
                detailEntity.formSearchEditData = obj.formSearchEditData;
                detailEntity.formSearchColumnLength = obj.formSearchColumnLength;
                detailEntity.isEditPage = true;

                ///更新LISTpage中的实体对象
                var listPageEntity = parent.arrGlobal.get(obj.entityName);
                listPageEntity.formSearchEditData = obj.formSearchEditData;
                listPageEntity.formSearchColumnLength = obj.formSearchColumnLength;

                ///GRID列配置
                var isCheckBox = isGridMuliCheck(obj.detailEntityInfo.ParentField);
                var gColumns = getFormatGridColumnsData(obj.detailEntityFields, isCheckBox, obj.detailEntityName);
                ///过滤条件
                var gFilter = '';
                ///detail字段|main属性
                var relationFields = obj.detailRelationKeyFieldName.split('|');
                if (relationFields.length == 1)
                    gFilter = obj.detailRelationKeyFieldName + "=^" + obj.formDataKey + "^";
                else {
                    if (!isValidData(obj.formParamRowsData))
                        obj.formParamRowsData = eval('(' + obj.formUpdateData + ')');
                    if (isValidData(obj.formParamRowsData)) {
                        var relaFields = relationFields[0].split(',');
                        var tionFields = relationFields[1].split(',');
                        if (relaFields.length <= tionFields.length) {
                            for (var i = 0; i < relaFields.length; i++) {
                                var rField = relaFields[i];
                                if (!isValidData(rField)) continue;
                                var tField = tionFields[i];
                                if (!isValidData(tField)) continue;
                                var tValue = eval('obj.formParamRowsData.' + tField);
                                if (isValidData(tValue))
                                    gFilter += '|' + rField + "=^" + tValue + "^";
                                else
                                    gFilter += '|1=0';
                            }
                        }
                    }
                }
                if (!isValidData(gFilter))
                    gFilter = '1=0';

                detailEntity.urlFilter = gFilter;
                ///加入全局变量
                parent.arrGlobal.put(obj.entityName, listPageEntity);
                parent.arrGlobal.put(obj.detailEntityName, detailEntity);
                ///数据地址
                var gDataUrl = XR.defaultProcessUrl()
                    + "method=" + obj.detailGetDataMethod
                    + "&ENTITY_NAME=" + obj.detailEntityName
                    + "&AN=BLL." + obj.bllProjectName
                    + "&URL_FILTER=" + filterEncodeURIComponent(gFilter);
                ///Search
                createSearchControlData(divSearchFormId, obj.formSearchEditData, obj.formSearchColumnLength);
                ///创建GRID
                createGrid(obj.detailEntityName, '', '', '', gColumns, gDataUrl, 10);
                ///添加明细编辑TAB
                createControlToPage(divControlFormId, obj.detailEntityFields, null, obj.formParamRowsData, obj.detailEntityInfo.TabTitles, 'D');
            }
        });
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    this.DefaultCreateAction = function () {
        var data = this.pathName.indexOf("CommonList.aspx") == -1 ? this.formActionEditData : this.formActionListData;
        ///主表功能按钮
        var actionData = [];
        ///从表功能按钮
        var actionDetailData = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].DetailFlag)
                actionDetailData.push(data[i]);
            else
                actionData.push(data[i]);
        }
        createActionData(divActionMenuId, actionData);
        createActionData(divActionDetailId, actionDetailData);
    }
    this.DefaultCreateSearch = function () {
        var data = this.pathName.indexOf("CommonList.aspx") == -1 ? this.formSearchEditData : this.formSearchListData;
        var columnlength = this.pathName.indexOf("CommonList.aspx") == -1 ? this.formSearchColumnLength : this.listSearchColumnLength;
        createSearchControlData(divSearchFormId, data, columnlength);
    }
    this.DefaultCreateControlForm = function () {
        createControlToPage(divControlFormId, this.formCreateData, this.formUpdateData, this.formParamRowsData, this.formEntityInfo.TabTitles, undefined, this.formEntityInfo.EditColumnLength);
    }
    this.DefaultCreateGridColumns = function () {
        return getFormatGridColumnsData(this.formCreateData, this.gridCheckBox, this.entityName);
    }
    this.DefaultCreateGrid = function () {
        var data = this.formEntityInfo;
        if (isValidData(data)) {
            ///根据模型配置获取单选or多选标记
            this.gridCheckBox = isGridMuliCheck(data.ParentField);
            createGrid(this.entityName
                , this.tableKeyField
                , ''
                , ''
                , this.DefaultCreateGridColumns()
                , this.getDataToGridUrl + '&FOOTER_FLAG=' + data.FooterFlag + '&TABLE_NAMES=' + data.TableNames
            );
        }
    }

    this.AjaxActionOrEntityData = function (medthodType, medthodName, menuName) {
        var obj = this;
        var dataParams = '';
        ///BLL命名空间
        var bllProject = "&AN=BLL." + this.bllProjectName;
        ///数据地址
        var ajaxUrl = XR.defaultProcessUrl();
        ///
        switch (medthodType.toLowerCase()) {
            ///all -> ajaxActionAndEntity
            case 'all':
                dataParams = 'method=ajaxActionAndEntity'
                    + '&MENU_FID=' + this.menuFid
                    + '&PAGE_URL=' + escape(this.pathName)
                    + '&ENTITY_NAME=' + this.entityName
                    + '&TABLE_NAMES=' + this.tableName;
                break;
            ///action -> ajaxActionOrSearch
            case 'action':
                dataParams = 'method=ajaxActionOrSearch'
                    + '&PAGE_URL=' + escape(this.pathName);
                break;
            ///entity -> ajaxCreateColumns
            ///GetCreateColumnsData返回action.url&entity&entityfield
            case 'entity':
                dataParams = 'method=ajaxCreateColumns'
                    + '&ENTITY_NAME=' + this.entityName
                    + '&TABLE_NAMES=' + this.tableName;
                break;
            ///menuaction -> ajaxLoadSetMenuAcionData
            case 'menuaction':
                dataParams = 'method=ajaxLoadSetMenuAcionData'
                    + '&PAGE_URL=' + escape(this.pathName)
                    + '&ENTITY_NAME=' + this.entityName;
                break;
            ///roleauth -> ajaxRoleAuthDataByFid
            case 'roleauth':
                dataParams = 'method=ajaxRoleAuthDataByFid'
                    + '&key=' + this.formDataKey;
                break;
            case 'import':
                dataParams = 'method=importFormElement'
                    + '&ENTITY_NAME=' + this.entityName;
                break;
            ///FORM打开所需参数GetFormOpenAttributes
            case 'formopen':
                dataParams = 'method=getFormOpenAttributes'
                    + '&MENU_FID=' + this.menuFid
                    + '&ENTITY_NAME=' + menuName;
                break;
            ///default
            default:
                ajaxUrl = medthodType;
                break;
        }
        $.ajax({
            url: ajaxUrl,
            async: false,
            type: "POST",
            data: dataParams + bllProject,
            dataType: 'json',
            success: function (data) {
                ///验证登录信息是否有效
                sessionIsNull(data);
                if (isSessionNull) return;
                ///LIST.action
                if (isValidData(data.action)) obj.formActionListData = data.action;
                ///LIST.search
                if (isValidData(data.search)) obj.formSearchListData = data.search;
                ///LIST.search.columnlength
                if (isValidData(data.searchcolumnlength)) obj.listSearchColumnLength = data.searchcolumnlength;
                ///FORM.action
                if (isValidData(data.actionform)) obj.formActionEditData = data.actionform;
                ///FORM.search
                if (isValidData(data.searchform)) obj.formSearchEditData = data.searchform;
                ///FORM.search.columnlength
                if (isValidData(data.searchformcolumnlength)) obj.formSearchColumnLength = data.searchformcolumnlength;
                ///ENTITY_FIELD
                if (isValidData(data.entityfieldform)) obj.formCreateData = data.entityfieldform;
                ///ENTITY
                if (isValidData(data.entityinfo)) obj.formEntityInfo = data.entityinfo;
                ///FORM.url
                if (isValidData(data.formUrl)) obj.formUrl = data.formUrl;
                ///FORM.width
                if (isValidData(data.formWidth)) obj.editFormWidth = data.formWidth;
                ///FORM.height
                if (isValidData(data.formHeight)) obj.editFormHeight = data.formHeight;
                ///加入全局变量
                parent.arrGlobal.put(obj.entityName, obj);
                ///执行参数JS方法
                if (isValidData(medthodName)) {
                    eval(medthodName);
                }
            }
        });
    }

    ///JS-弹出窗体生成页面的参数获取
    this.AjaxCreateFromDataByActionOrEntityData = function (paramEntityName, paramTableName, methodName) {
        var obj = new PageEntity();
        var bllProject = "&AN=BLL." + this.bllProjectName;
        var ajaxUrl = XR.defaultProcessUrl();
        var dataParams = "method=ajaxLoadSetMenuAcionData&ENTITY_NAME=" + paramEntityName + "&TABLE_NAMES=" + paramTableName;
        $.ajax({
            url: ajaxUrl,
            async: false,
            type: "POST",
            data: dataParams + bllProject,
            dataType: 'json',
            success: function (data) {
                sessionIsNull(data);
                if (isSessionNull) return;
                if (isValidData(data.actionform)) obj.formActionEditData = data.actionform;
                if (isValidData(data.searchform)) obj.formSearchEditData = data.searchform;
                if (isValidData(data.entityfieldform)) obj.formCreateData = data.entityfieldform;
                if (isValidData(data.entityinfo)) obj.formEntityInfo = data.entityinfo;
                obj.operationType = '';
                obj.entityName = paramEntityName;
                obj.tableName = paramTableName;
                parent.arrGlobal.put(paramEntityName, obj);
                if (isValidData(methodName)) eval(methodName);
            }
        });
    }
    ///获取界面配置后直接绘制界面
    this.DefaultAjaxCreateFormAndGridData = function (medthodType) {
        var obj = this;
        var dataParams = '';
        ///命名空间
        var bllProject = "&AN=BLL." + this.bllProjectName;
        ///默认POST地址
        var ajaxUrl = XR.defaultProcessUrl();
        ///
        switch (medthodType.toLowerCase()) {
            ///all -> ajaxActionAndEntity
            case 'all':
                dataParams = 'method=ajaxActionAndEntity'
                    + '&MENU_FID=' + this.menuFid
                    + '&PAGE_URL=' + escape(this.pathName)
                    + '&ENTITY_NAME=' + this.entityName
                    + '&TABLE_NAMES=' + this.tableName;
                break;
            ///action -> ajaxActionOrSearch
            case 'action':
                dataParams = 'method=ajaxActionOrSearch'
                    + '&PAGE_URL=' + escape(this.pathName)
                    + '&ENTITY_NAME=' + this.entityName;
                break;
            ///entity -> ajaxCreateColumns
            case 'entity':
                dataParams = 'method=ajaxCreateColumns'
                    + '&ENTITY_NAME=' + this.entityName
                    + '&TABLE_NAMES=' + this.tableName;
                break;
            ///default
            default:
                ajaxUrl = medthodType;
                break;
        }
        ///
        $.ajax({
            url: ajaxUrl,
            async: false,
            type: "POST",
            data: dataParams + bllProject,
            dataType: 'json',
            success: function (data) {
                ///验证登录信息是否有效
                sessionIsNull(data);
                if (isSessionNull) return;
                ///LIST.action
                if (isValidData(data.action)) obj.formActionListData = data.action;
                ///LIST.search
                if (isValidData(data.search)) obj.formSearchListData = data.search;
                ///LIST.search.columnlength
                if (isValidData(data.searchcolumnlength)) obj.listSearchColumnLength = data.searchcolumnlength;
                ///FORM.action
                if (isValidData(data.actionform)) obj.formActionEditData = data.actionform;
                ///FORM.search
                if (isValidData(data.searchform)) obj.formSearchEditData = data.searchform;
                ///FORM.search.columnlength
                if (isValidData(data.searchformcolumnlength)) obj.formSearchColumnLength = data.searchformcolumnlength;
                ///ENTITY_FIELD
                if (isValidData(data.entityfieldform)) obj.formCreateData = data.entityfieldform;
                ///ENTITY
                if (isValidData(data.entityinfo)) obj.formEntityInfo = data.entityinfo;
                ///FORM.url
                if (isValidData(data.formUrl)) obj.formUrl = data.formUrl;
                ///FORM.width
                if (isValidData(data.formWidth)) obj.editFormWidth = data.formWidth;
                ///FORM.height
                if (isValidData(data.formHeight)) obj.editFormHeight = data.formHeight;
                ///加入全局变量
                parent.arrGlobal.put(obj.entityName, obj);
                ///绘制界面
                obj.DefaultCreateList();
            }
        });
    }
    ///绘制界面
    this.DefaultCreateList = function () {
        /// <summary>
        /// 初始化页面容器
        /// </summary>
        createInitForm(this.formActionListData, this.formSearchListData);
        ///创建按钮和动作
        this.DefaultCreateAction();
        this.DefaultCreateSearch();
        if (this.formEntityInfo.ListpageEditFlag) {
            this.DefaultCreateControlForm();
        }
        this.DefaultCreateGrid();
    }
    ///传承
    this.GetListData = function (oldpageLoadParams) {
        if (isValidData(oldpageLoadParams)) {
            this.formActionEditData = oldpageLoadParams.formActionEditData;
            this.formSearchEditData = oldpageLoadParams.formSearchEditData;
            this.formSearchColumnLength = oldpageLoadParams.formSearchColumnLength;
            this.formCreateData = oldpageLoadParams.formCreateData;
            this.formUpdateData = oldpageLoadParams.formUpdateData;
            this.formEntityInfo = oldpageLoadParams.formEntityInfo;
            this.formDataKey = oldpageLoadParams.formDataKey;
            this.formDataKeyLength = oldpageLoadParams.formDataKeyLength;
            this.operationType = oldpageLoadParams.operationType;
            this.formParamKey = oldpageLoadParams.formParamKey;
            this.formParamRowsData = oldpageLoadParams.formParamRowsData;
            this.bllProjectName = oldpageLoadParams.bllProjectName;
            this.parentEntityName = oldpageLoadParams.parentEntityName;
            this.tableName = oldpageLoadParams.tableName;
            ///urlFilter
            this.urlFilter = oldpageLoadParams.urlFilter;
            this.tableKeyField = oldpageLoadParams.tableKeyField;
            this.tableKeyFieldLength = oldpageLoadParams.tableKeyFieldLength;
        }
    }
    ///
    this.DefaultCreateEdit = function (oldpageLoadParams) {
        pageLoadParams.GetListData(oldpageLoadParams);
        createInitForm(this.formActionEditData, this.formActionEditData);
        var searchDiv = $('#' + divSearchFormId);
        if (isValidData(searchDiv.length)) {
            searchDiv.hide();
        }
        this.DefaultCreateAction();
        this.DefaultCreateControlForm();
        this.FormCreateDetailGrid(this);
    }
}

(function ($) {
    window['XR'] = {};
    $.cookie = {
        set: function (name, value, expire) {
            var now = new Date();
            now.setTime(now.getTime() + expire * 24 * 3600 * 1000);
            document.cookie = name + '=' + value + '; path=/; expires=' + now.toUTCString();
        },
        get: function (name) {
            var arr = document.cookie.split('; ');
            for (var i = 0; i < arr.length; i++) {
                var kv = arr[i].split('=');
                if (kv[0] == name) {
                    return kv[1];
                }
            }
            return '';
        }
    };
    XR.reqPathName = window.location.pathname;//window.location.pathname;  //当前页面路径名
    XR.reqPathSearch = window.location.search;  //当前页面参数
    if ($.cookie.get('language') == "") {
        if (navigator.browserLanguage == undefined) {
            $.cookie.set('language', navigator.language.toLowerCase());
        } else {
            $.cookie.set('language', navigator.browserLanguage.toLowerCase());
        }
    }
    XR.language = $.cookie.get('language') == 'zh-cn' ? false : true;//navigator.browserLanguage.toLowerCase();取浏览器语言版本
    XR.operationType = ""; //操作类型:insert-TY_SYS_ACTION,update-TY_SYS_ACTION,select-TY_SYS_ACTION,delete-TY_SYS_ACTION
    //路径层级 --START
    XR.getPathLevel = function () {
        return getPathLevel();
    };
    //路径层级 --END

    //路径层级 --START
    XR.defaultProcessUrl = function () {
        var defultUrl = XR.getPathLevel() + '/HANDLER/mainHandler.ashx?';
        return defultUrl;
    };
    //路径层级 --END
    initLanguage();//初始化国际化
    initTheme();///主题
})(jQuery);

/////////////////////////////////////////////////////////////////////FORM.js/////////////begin/////////////////////////////////
window["FORM"] = {};


FORM.createInitForm = function (formAction, formSearch) {
    createInitForm(formAction, formSearch);
}
//创建combobox --START
FORM.createCombobox = function (cboId, cboData, valField, txtField, cboSelectMath, cboLoadSuccess, cboWidth, cboRequired) {
    createCombobox(cboId, cboData, valField, txtField, cboSelectMath, cboLoadSuccess, cboWidth, cboRequired);
}
//创建combobox --END
//创建textbox --START
FORM.createTextbox = function (tboId, tWidth, defaultValue, isReadonly, isDisplay) {
    createTextbox(tboId, tWidth, defaultValue, isReadonly, isDisplay);
}
//创建textbox --END
//创建动作按钮表单  --START
FORM.createActionData = function (controlId, dtAction) {
    createActionData(controlId, dtAction)
}
//创建动作按钮表单  --END

//创建搜索表单 -- STATE
FORM.createSearchControlData = function (controlId, dtSearch, columnLength) {
    createSearchControlData(controlId, dtSearch, columnLength);
}
//创建搜索表单 -- END

//创建控件到页面，表单创建
//corjSON：数据集， containerName：接收这些控件的标签可以传TagName或者HTML标签ID
FORM.createControlToPage = function (containerName, corJson, dataJson, parentRowsData, tabTitles, ctrlType) {
    createControlToPage(containerName, corJson, dataJson, parentRowsData, tabTitles, ctrlType);
}
//获取action ： 遍历EditPage创建Action数据
FORM.getActionFormData = function (dtActionForm) {
    return getActionFormData(dtActionForm);
}

//获取GRID列： 遍历dtColumns生成GRID列
FORM.getFormatGridColumnsData = function (dtColumns, gridCheckBox, entityName) {
    return getFormatGridColumnsData(dtColumns, gridCheckBox, entityName);
}

//获取查询表单数据 --START 
FORM.getSearchFormData = function () {
    return getSearchFormData();
};
//获取查询表单数据  --END

//创建按钮
FORM.createMenu = function (acceptObj, bId, bText, bIcon, bJavascript, bType) {
    createMenu(acceptObj, bId, bText, bIcon, bJavascript, bType);
};
//默认保存表单
FORM.defaultSave = function () {
    defaultSave();
};
//创建控件
FORM.createControl = function (acceptObj, tId, tText, tType, tVerifyType, tWidth, tHeight, tIconType, extend, codeName, tDefaultVal, readonly, arrControlHelp, parentRowsData, authExtend, regexErrorMsg, nullEnable, minValue, maxValue, dataLength, precision) {
    createControl(acceptObj, tId, tText, tType, tVerifyType, tWidth, tHeight, tIconType, extend, codeName, tDefaultVal, readonly, arrControlHelp, parentRowsData, authExtend, regexErrorMsg, nullEnable, minValue, maxValue, dataLength, precision);
};
FORM.bindControlData = function (tId, tType, extend, codeName) {
    bindControlData(tId, tType, extend, codeName);
};
FORM.defaultFormatterColumns = function (value, row, index, codeName) {
    return defaultFormatterColumns(value, row, index, codeName);
};
FORM.formatterCheckBoxByColumns = function (value, row, index) {
    return formatterCheckBoxByColumns(value, row, index);
};
FORM.getFormatterColumnsData = function (data) {
    return getFormatterColumnsData(data);
};
FORM.getContorlBindData = function (data) {
    return getContorlBindData(data);
};
FORM.createGrid = function (entityName, gIdField, gSortName, gSortOrder, gColumns, gUrl, defaultPageSize, containerId, isChildGrid, isDialog) {
    createGrid(entityName, gIdField, gSortName, gSortOrder, gColumns, gUrl, defaultPageSize, containerId, isChildGrid, isDialog);
};
// Grid日期列格式化 -START
FORM.formatDateByJson = function (datestr, format) {
    if (isValidData(datestr)) {
        var newdate;
        if (datestr.indexOf('/Date(') != -1) {
            newdate = new Date(parseInt(datestr.replace('/Date(', '').replace(')/', ''), 10));
        }
        else if (datestr.indexOf('-') == -1 && datestr.length == 8) {///yyyyMMdd
            var strdate = datestr.substring(0, 4) + '-' + datestr.substring(4, 6) + '-' + datestr.substring(6, 8);
            newdate = new Date(Date.parse(strdate.replace(/-/g, "/")));
        }
        else if (datestr.indexOf('-') == -1 && datestr.length == 6) {///yyMMdd
            var strdate = '20' + datestr.substring(0, 2) + '-' + datestr.substring(2, 4) + '-' + datestr.substring(4, 6);
            newdate = new Date(Date.parse(strdate.replace(/-/g, "/")));
        }
        else if (datestr.indexOf('-') == -1 && datestr.length == 11 && datestr.indexOf('T') != -1) {///yyMMddTHHmm
            var strdate = '20' + datestr.substring(0, 2) + '-' + datestr.substring(2, 4) + '-' + datestr.substring(4, 6) + ' ' + datestr.substring(7, 9) + ':' + datestr.substring(9, 11) + ':00';
            newdate = new Date(Date.parse(strdate.replace(/-/g, "/")));
        }
        else {
            newdate = new Date(Date.parse(datestr.replace(/-/g, "/")));
        }
        return formatDate(newdate, format);
    }
    return "";
};
// Grid日期列格式化 -END
/////////////////////////////////////////////////////////////////////FORM.js/////////////end/////////////////////////////////
















/////////////////////////////////////////////////////////////////////HELP.js/////////////begin/////////////////////////////////
window["HELP"] = {};
function clickButton(id) {
    if (document.all) {
        document.getElementById(id).click();
    }
    else {
        var evt = document.createEvent("MouseEvent");
        evt.initEvent("click", true, true);
        document.getElementById(id).dispatchEvent(evt);
    }
}
function EscAndEnterToMedthod(keyCode) {
    try {
        switch (keyCode) {
            case 27://esc
                if (parent.$('#dialogModal') != null) {
                    parent.$('#dialogModal').window('close');
                }

                break
            case 13://enter
                var linkButton = $(".l-btn");
                for (var i = 0; i < linkButton.length; i++) {
                    var linkHtml = linkButton[i].innerHTML;
                    if (linkHtml.indexOf(languageMessageTitle('1x00000016')) != -1) {
                        clickButton(linkButton[i].id);
                    }
                }
                break
            default:
                break;

        }
    } catch (e) {

    }
}

function banBackspace(e) {
    var ev = e || window.event;
    var obj = ev.target || ev.srcElement;
    var keyCode = ev.keyCode;
    EscAndEnterToMedthod(keyCode);

    var t = obj.type || obj.getAttribute('type');

    var vReadonly = obj.getAttribute('readonly');
    var vEnabled = obj.getAttribute('enabled');

    vReadonly = (vReadonly == null) ? false : vReadonly;
    vEnabled = (vEnabled == null) ? true : vEnabled;

    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text") && (vReadonly == 'readonly' || vEnabled != true)) ? true : false;

    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea" && obj.className != "wangEditor-txt") ? true : false; //|| t == "textarea"

    if (flag2) {
        return false;
    }

    if (flag1) {
        return false;
    }
}
function banBackspace2(e) {
    var ev = e || window.event;
    var obj = ev.target || ev.srcElement;
    var keyCode = ev.keyCode;


    var t = obj.type || obj.getAttribute('type');

    var vReadonly = obj.getAttribute('readonly');
    var vEnabled = obj.getAttribute('enabled');

    vReadonly = (vReadonly == null) ? false : vReadonly;
    vEnabled = (vEnabled == null) ? true : vEnabled;

    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text") && (vReadonly == 'readonly' || vEnabled != true)) ? true : false;

    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea" && obj.className != "wangEditor-txt") ? true : false; //|| t == "textarea"

    if (flag2) {
        return false;
    }

    if (flag1) {
        return false;
    }
}

document.onkeypress = banBackspace;
document.onkeydown = banBackspace2;

//Ajax请求
HELP.ajaxCommon = function (eurl, dataParams, callbackName, isAsync) {
    ajaxCommon(eurl, dataParams, callbackName, isAsync);
};
//弹窗 --START
HELP.tAlert = function (alertType, strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName) {
    tAlert(alertType, strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName);
};
//弹窗模态窗口
HELP.openDialogModal = function (strTitle, strUrl, width, height) {
    openDialogModal(strTitle, strUrl, width, height);
}
//弹窗IFRAME
HELP.openIframeModal = function (entityName, strTitle, strUrl, width, height) {
    openIframeModal(entityName, strTitle, strUrl, width, height);
}
//弹确认信息窗
HELP.openMsg = function (strTitle, strMsg, okFunctionName, closeFunctionName) {
    openMsg(strTitle, strMsg, okFunctionName, closeFunctionName);
}

//日期格式化 -START
HELP.formatDate = function (date, format) {
    return formatDate(date, format);
};
//数据验证
HELP.verifyResult = function (value, req, des) {
    verifyResult(value, req, des);
}
//切换语言
HELP.onChangeLanguage = function (langu) {
    onChangeLanguage(langu)
}

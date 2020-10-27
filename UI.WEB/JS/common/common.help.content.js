////////////////////////////////////////////////////////XR.js/ begin/////////////////////////////////////////////////////////////////////////////////////
//var data = languageMessageData('0x00000053');//请选中行数据
//var titleAlert = languageMessageTitle('1x00000001');//提示
///IIS中的部署层级 0为根目录部署，1为站点应用程序部署
var iisPathLevel = 0;
//国际化Message
function initLanguage() {
    var ajaxUrl = 'HANDLER/mainHandler.ashx?';
    var dataParams = 'method=ajaxTables&ENTITY_NAME=InitMessage&AN=BLL.SYS';
    $.ajax({
        url: ajaxUrl,
        async: false,
        type: 'POST',
        data: dataParams,
        dataType: 'json',
        success: function (data) {
            for (var i = 0; i < data.total; i++) {
                arrLanguage.put(data.rows[i].MessageCode, data.rows[i].MessageEn + '|' + data.rows[i].MessageCn);
            }
        }
    });
}
//根据代码及语言环境查找界面显示标签
function languageMessageData(code) {
    code = code.replace(/"/g, "");
    var cookoemessage = arrLanguage.get(code);
    if (isValidData(cookoemessage)) {
        var message = cookoemessage.split('|');
        var data = XR.language ? message[0] : message[1];
        return data;
    }
    return code;
}

function languageMessageTitle(code) {
    return languageMessageData(code);
}
///
function getPathLevel() {
    var thisPath = XR.reqPathName.split('/');
    ///pathSplitLength - iisPathLevel = 2 说明是当前为根目录
    var pathLevel = '';
    if (iisPathLevel > 0) {
        pathLevel = '/' + thisPath[iisPathLevel];
    }
    return pathLevel;
}

function arrayIconClsToTree(data, id, pid, iconCls)      //将ID、ParentID这种数据格式转换为树格式
{
    var g = this, p = this.options;
    var childrenName = "children";
    if (!data || !data.length) return [];
    var targetData = [];                    //存储数据的容器(返回) 
    var records = {};
    var itemLength = data.length;           //数据集合的个数
    for (var i = 0; i < itemLength; i++) {
        var o = data[i];
        var key = getKey(o[id]);
        records[key] = o;
    }
    for (var i = 0; i < itemLength; i++) {
        var currentData = data[i];
        currentData.iconCls = getKey(currentData[iconCls]);
        var key = getKey(currentData[pid]);
        var parentData = records[key];
        if (!parentData) {
            targetData.push(currentData);
            continue;
        }
        parentData[childrenName] = parentData[childrenName] || [];
        parentData[childrenName].push(currentData);
    }
    for (var i = 0; i < targetData.length; i++) {
        if (targetData[i].children != undefined) {
            targetData[i].state = "closed";
        }
    }
    return targetData;
    ///TODO:为什么要转小写?
    function getKey(key) {
        if (typeof (key) == "string") key = key.replace(/[.]/g, '');
        return key;
    }
}
function arrayToTree(data, id, pid)      //将ID、ParentID这种数据格式转换为树格式
{
    var g = this, p = this.options;
    var childrenName = "children";
    if (!data || !data.length) return [];
    var targetData = [];                    //存储数据的容器(返回) 
    var records = {};
    var itemLength = data.length;           //数据集合的个数
    for (var i = 0; i < itemLength; i++) {
        var o = data[i];
        var key = getKey(o[id]);
        records[key] = o;
    }
    for (var i = 0; i < itemLength; i++) {
        var currentData = data[i];
        var key = getKey(currentData[pid]);
        var parentData = records[key];
        if (!parentData) {
            targetData.push(currentData);
            continue;
        }
        parentData[childrenName] = parentData[childrenName] || [];
        parentData[childrenName].push(currentData);
    }
    for (var i = 0; i < targetData.length; i++) {
        if (targetData[i].children != undefined) {
            targetData[i].state = "closed";
        }
    }
    return targetData;

    function getKey(key) {
        if (typeof (key) == "string") key = key.replace(/[.]/g, '').toLowerCase();
        return key;
    }
}
function addPanel(menuFid, surl, stitle) {

    if ($('#tt').tabs('exists', stitle)) {
        $('#tt').tabs('select', stitle);
    } else {
        var urlParm = surl.split('?');
        if (urlParm.length > 1) {
            var iframeId = urlParm[1].split('&')[0];
            var content = '<iframe scrolling="auto" frameborder="0" id="Iframe_' + iframeId + '"  src="' + surl + '" style="width:100%;height:99%;"></iframe>';
            $('#tt').tabs('add', {
                id: menuFid,
                title: stitle,
                content: content,
                //href:surl,
                closable: true
                , tools: [{
                    toolPosition: 'left',
                    iconCls: 'icon-mini-refresh',
                    handler: function () {
                        var tab = $('#tt').tabs('getSelected');  // 获取选择的面板
                        tab[0].childNodes[0].src = surl; // 刷新页面
                    }
                }]
            });
        }
        else {
            var content = '<iframe scrolling="auto" frameborder="0" id="Iframe_' + menuFid
                + '"  src="' + surl + '" style="width:100%;height:99%;"></iframe>';
            $('#tt').tabs('add', {
                id: menuFid,
                title: stitle,
                content: content,
                //href:surl,
                closable: true
                , tools: [{
                    toolPosition: 'left',
                    iconCls: 'icon-mini-refresh',
                    handler: function () {
                        var tab = $('#tt').tabs('getSelected');  // 获取选择的面板
                        tab[0].childNodes[0].src = surl; // 刷新页面
                    }
                }]
            });
        }
    }
};

//调用AJAX方法
function ajaxCommon(eurl, dataParams, callbackName, isAsync) {
    if (isAsync + '' != 'false') {
        isAsync = true;
    }
    $.ajax({
        type: 'POST',
        url: eurl,
        async: isAsync,
        data: dataParams,
        datatype: 'json',
        success: function (data) {
            if (sessionIsNull(data)) { return; }
            var methodPar = callbackName.split(':');
            var methodType = methodPar[0];
            var methodName = 'alert';
            if (methodPar.length == 2) {
                methodName = callbackName.split(':')[1];
            }
            switch (methodType + '') {
                case 'js':
                    if ((methodName.indexOf(")") > -1) && ((methodName.indexOf("(") > -1))) {
                        methodName = methodName.replace(")", "");
                        eval(methodName + ",'" + data + "')");
                    }
                    else {
                        eval(methodName + "('" + data + "')");
                    }
                    break;
                case 'code':
                    var methodNameItem = methodName.split('-');
                    if (methodNameItem.length > 1) {
                        eval(methodNameItem[0] + "('" + data + "','" + methodNameItem[1] + "')");
                    }
                    break;
                default:
                    var titleAlert = languageMessageTitle('1x00000001');//提示
                    tAlert('alert', titleAlert, data, 500, 200);
                    break;
            }
        },
        error: function (errorData) {
            if (sessionIsNull(errorData)) return;
            var titleAlert = languageMessageTitle('1x00000001');//提示
            tAlert('alert', titleAlert, data, 500, 200);
        }
    });
}

//ajax & post 请求获取创建表单数据 --START
//ashxParam:
function ajaxCreateForm(pageUrl) {
    $.post(XR.defaultProcessUrl(), {
        'method': 'ajaxActionOrSearch', 'PAGE_URL': pageUrl
    },
        function (data) {
            sessionIsNull(data);
            if (isSessionNull) return;
            var dtAction = data.action;
            var dtSearch = data.search;
            var columnlength = data.searchcolumnlength;
            if (isValidData(dtAction)) {
                createActionData(divActionMenuId, dtAction);
            } else {
                $("#" + divActionMenuId).hide();
            }
            if (isValidData(dtSearch)) {
                createSearchControlData(divSearchFormId, dtSearch, columnlength);
            } else {
                $("#" + divSearchFormId).hide();
            }

        }, 'json');
}
function getActionFormData(dtActionForm) {

    var tmpAction = [];
    if (dtActionForm != undefined && dtActionForm.length != 0 && dtActionForm != null) {

        for (var j = 0; j < dtActionForm.length; j++) {
            var gText = "";
            var tmpClientJs = dtActionForm[j].ClientJs;

            if (XR.language) {
                gText = dtActionForm[j].ActionName;
            } else {
                gText = dtActionForm[j].ActionNameCn;

            }
            var config = "{ text:'" + gText + "', iconCls: '" + dtActionForm[j].IconUrl + "', handler: function () { " + tmpClientJs + ";} }";
            funConfig = eval('(' + config + ')');
            tmpAction.push(funConfig);
        }
    }
    return tmpAction;

}

//ajax & post 请求获取Grid-Columns数据 --START/entityName/gHeight：高/gridCheckBox：是否有Checkbox列/gSingleSelect：是否只可单选/gPagination：是否分页//gIdField：指定ID字段//gSortName：排序字段/gSortOrder：排序方式
function ajaxCreateGrid(entityName, gridCheckBox, gIdField, gSortName, gSortOrder) {

    $.post(XR.defaultProcessUrl(), {
        'method': 'ajaxCreateColumns', 'ENTITY_NAME': entityName
    },
        function (data) {
            var dtActionForm = data.actionform;
            var dtColumns = data.columns;
            var dtEntityInfo = data.entityinfo;
            parent.arrGlobal.put('formActionData' + entityName, dtActionForm);

            if (dtEntityInfo != undefined && dtEntityInfo.length != 0 && dtEntityInfo != null) {

                parent.arrGlobal.put('formEntityInfo' + entityName, dtEntityInfo);
            }

            var tmpColumnsData = getFormatGridColumnsData(dtColumns, gridCheckBox, entityName);

            parent.arrGlobal.put('formCreateData' + entityName, dtColumns);

            var gUrl = XR.defaultProcessUrl() + "method=ajaxTables&FOOTER_FLAG=" + dtEntityInfo.FooterFlag + "&ENTITY_NAME=" + entityName;
            createGrid(entityName, gIdField, gSortName, gSortOrder, tmpColumnsData, gUrl);
        }, 'json');
};

///获取查询表单数据 --START 
function getSearchFormData() {
    var dtWhere = '';
    var tempFormData = null;
    if (pageLoadParams.isEditPage) {
        var tempFormSearchEditData = pageLoadParams.formSearchEditData;
        if (isValidData(tempFormSearchEditData) && tempFormSearchEditData != []) {
            tempFormData = tempFormSearchEditData;
        }
    } else {
        var tempFormSearchListData = pageLoadParams.formSearchListData;
        if (isValidData(tempFormSearchListData) && tempFormSearchListData != []) {
            tempFormData = tempFormSearchListData;
        }
    }
    if (!isValidData(tempFormData)) return '';
    for (var i = 0; i < tempFormData.length; i++) {
        ///控件ID
        var cId = tempFormData[i].ControlType + '-S-' + tempFormData[i].ControlId;
        ///控件显示标题
        var cTitle = XR.language ? tempFormData[i].DisplayNameEn : tempFormData[i].DisplayNameCn;
        ///JSON格式配置
        var configJson;
        if (isValidData(tempFormData[i].Extend3) && tempFormData[i].Extend3.indexOf('^') > 0) {
            var paramItem = tempFormData[i].Extend3.split('^');
            if (paramItem[0] == 'sql') {
                var paramValue = paramItem[1];
                var paramControlItem = paramValue.split("|");
                configJson = GetComboExtendJson(paramControlItem[0]);
            }
        }
        ///用户输入值
        var inpValue = getControlValueByControlType(cId, tempFormData[i].ControlType, isValidData(configJson) ? configJson.isMultiple : false);

        if (isValidData(inpValue)) {
            var data = languageMessageData('0x00000062');
            ///正则表达式校验
            var verifyValue = verifyRegexResult(cId, cTitle, tempFormData[i].ControlType);
            ///校验通过，无报错信息
            if (!isValidData(verifyValue)) {
                ///表名
                var tableName = tempFormData[i].TableName;
                ///字段名
                var columnName = tempFormData[i].ColumnName;
                ///多行文本框单独处理
                if (tempFormData[i].ControlType + '' == '90') {
                    switch (tempFormData[i].ColumnType + '') {
                        case '10': inpValue = 'N\'' + inpValue.replace(/,/g, '\',N\'') + '\''; break;
                        case '20':
                        case '60': inpValue = inpValue; break;
                    }
                    dtWhere += " and [" + columnName + "] in (" + inpValue + ")";
                    continue;
                }
                ///数据类型
                switch (tempFormData[i].ColumnType + '') {
                    ///string
                    case '10': inpValue = "N'" + inpValue + "'"; break;
                    ///int
                    case '20': inpValue = (inpValue) * 1; break;
                    ///bool
                    case '30': inpValue = inpValue == "30" ? 1 : 0; break;
                    ///decimal
                    case '60': inpValue = eval(inpValue); break;
                    ///datetime
                    case '40':
                    ///date
                    case '50': inpValue = inpValue; break;
                    default: break;
                }

                ///检索类型
                switch (tempFormData[i].DatasearchType + '') {
                    ///<=
                    case "20":
                    case '30':
                    case '40':
                    case '60':
                        var logicFlag;
                        switch (tempFormData[i].DatasearchType + '') {
                            case '20': logicFlag = '<='; break;
                            case '30': logicFlag = '>='; break;
                            case '40': logicFlag = '<'; break;
                            case '60': logicFlag = '>'; break;
                        }
                        switch (tempFormData[i].ColumnType + '') {
                            case '20': case '60': dtWhere += " and [" + columnName + "] " + logicFlag + " " + inpValue; break;
                            case '50': dtWhere += " and DATEDIFF(DAY,N'" + inpValue + "',[" + columnName + "]) " + logicFlag + " 0"; break;
                            case '40': dtWhere += " and DATEDIFF(SS,N'" + inpValue + "',[" + columnName + "]) " + logicFlag + " 0"; break;
                            default: break;
                        }
                        break;
                    ///like
                    case '50': dtWhere += " and CHARINDEX(" + inpValue + ",[" + columnName + "]) > 0"; break;
                    default: dtWhere += " and [" + columnName + "] = " + inpValue + ""; break;
                }
            }
            else {
                var title = languageMessageTitle('1x00000001');
                tAlert("error", title, verifyValue, "", "");
                return;
            }
        }
    }
    return dtWhere;
};

function getSearchDataByColumnTypeAndDatasearchType(columnType, datasearchType) {

}

//弹窗 --START
function tAlert(alertType, strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName) {

    switch (alertType) {
        case "pageIframe": openIframeModal(strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName); break;
        case "pageModel": openDialogModal(strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName); break;
        case "choice":
            openMsg(strTitle, strUrlOrMsg, widthOrFunName, heightOrFunName);
            break;
        case "confirm":
            $.messager.confirm(strTitle, strUrlOrMsg, function (r) {
                if (r) {
                    eval(widthOrFunName);
                }
            });
            break;
        case "alert":
            $.messager.show({
                title: strTitle,
                msg: strUrlOrMsg,
                timeout: 2000,
                showType: 'slide'
            });
            break;
        case "error":
            $.messager.alert(strTitle, strUrlOrMsg, 'error');
            break;
        case "info":
            $.messager.alert(strTitle, strUrlOrMsg, 'info');
            break;
        case "warning":
            $.messager.alert(strTitle, strUrlOrMsg, 'warning');
            break;
        case "slide":
            $.messager.show({
                title: strTitle,
                msg: strUrlOrMsg,
                timeout: widthOrFunName,
                showType: 'slide'
            });
            break;
        case "fade":
            $.messager.show({
                title: strTitle,
                msg: strUrlOrMsg,
                timeout: 0,
                showType: 'fade'
            });
            break;

        default:
            break;
    }
};

function formatDate(date, format) {
    if (!date) return;
    if (!format) format = "yyyy-MM-dd";
    switch (typeof date) {
        case "string":
            date = new Date(date.replace(/-/, "/"));
            break;
        case "number":
            date = new Date(date);
            break;
    }
    if (!date instanceof Date) return;
    var dict = {
        "yyyy": date.getFullYear(),
        "YY": date.getFullYear().toString().substring(2, 4),
        "M": date.getMonth() + 1,
        "d": date.getDate(),
        "H": date.getHours(),
        "m": date.getMinutes(),
        "s": date.getSeconds(),
        "MM": ("" + (date.getMonth() + 101)).substr(1),
        "dd": ("" + (date.getDate() + 100)).substr(1),
        "HH": ("" + (date.getHours() + 100)).substr(1),
        "mm": ("" + (date.getMinutes() + 100)).substr(1),
        "ss": ("" + (date.getSeconds() + 100)).substr(1)
    };
    return format.replace(/(YY?|yyyy?|MM?|dd?|HH?|ss?|mm?)/g, function () {
        return dict[arguments[0]];
    });
};

///表单默认保存方法
function defaultSave() {
    var methodName = pageLoadParams.operationType;
    if (isValidData(methodName)) {
        ///
        var entityFields = pageLoadParams.formCreateData;
        var dataPar = defaultSaveContent(pageLoadParams.formCreateData);
        if (isValidData(dataPar) && dataPar.indexOf("error_") == -1) {
            var formDataKey = pageLoadParams.formDataKey;
            dataPar = formDataKey == null ? dataPar + "method=" + methodName
                : dataPar + "method=" + methodName + "&key=" + formDataKey + "&keylength=" + pageLoadParams.formDataKeyLength;
            dataPar = expandSaveData(dataPar);
            if (dataPar.indexOf("error_") != -1) {
                tAlert("error", "提示", dataPar.replace("error_", ""))
            } else {
                dataPar = dataPar + "&ENTITY_NAME=" + pageLoadParams.entityName + "&AN=BLL." + pageLoadParams.bllProjectName;
                var processUrl = XR.defaultProcessUrl();
                ajaxCommon(processUrl, dataPar, "js:saveResult");
            }
        } else {
            if (dataPar != undefined && dataPar.indexOf("error_") != -1) {
                dataPar = dataPar.replace("error_", "");
            }
        }
    }
    else {
        var title = languageMessageTitle('1x00000001');
        var data = languageMessageData('0x00000063');//没有获取当前页面的操作类型 
        tAlert("error", title, data, "", "");
    }
}
function defaultSaveCommonFields() {
    var title = languageMessageTitle('1x00000001');
    var processUrl = XR.defaultProcessUrl();
    var methodName = pageLoadParams.operationType;
    if (methodName != undefined) {
        if (methodName.toLowerCase().indexOf('update-') != -1)
            methodName = methodName.replace("update-", "updatefield-");
        var dataPar = defaultSaveContent(pageLoadParams.formCreateData);
        if (dataPar != "" && dataPar != undefined && dataPar.indexOf("error_") == -1) {
            var formDataKey = pageLoadParams.formDataKey;//parent.arrGlobal.get('formDataKey' + entityName);
            dataPar = formDataKey == null ? dataPar + "method=" + methodName
                : dataPar + "method=" + methodName + "&key=" + formDataKey + "&keylength=" + pageLoadParams.formDataKeyLength;
            dataPar = expandSaveData(dataPar);
            if (dataPar.indexOf("error_") != -1) {
                tAlert("error", "提示", dataPar.replace("error_", ""))
            } else {
                dataPar = dataPar + "&ENTITY_NAME=" + pageLoadParams.entityName + "&AN=BLL." + pageLoadParams.bllProjectName;
                ajaxCommon(processUrl, dataPar, "js:saveResult");
            }
        } else {
            if (dataPar != undefined && dataPar.indexOf("error_") != -1) {
                dataPar = dataPar.replace("error_", "");
            }
        }
    } else {
        var data = languageMessageData('0x00000063');//没有获取当前页面的操作类型 
        tAlert("error", title, data, "", "");
    }
}

function expandSaveData(dataPar) {
    ///urlFilter参数获取
    if (isValidData(pageLoadParams.urlFilter)) {
        dataPar = replaceEntityFieldNameByTableFieldNameUrlfilter(pageLoadParams.urlFilter) + '&' + dataPar;
    }
    return dataPar;
}
///保存前获取界面值
function defaultSaveContent(entityFields, ctrlType, pFields) {
    var dataPar = '';
    var control = entityFields;
    for (var i = 0; i < control.length; i++) {
        ///页面KEY中已包含了的字段不再拼接到URL，直接用原地址栏中的KEY值作为URL更新内容进行提交
        if (isValidData(pFields)) {
            if ($.inArray(control[i].FieldName, pFields) > -1)
                continue;
        }
        ///不出现在编辑界面的不作为组合依据
        if (!control[i].Editable) continue;
        ///控件ID
        var id = control[i].ControlType + "-" + control[i].FieldName;
        if (isValidData(ctrlType)) {
            id = control[i].ControlType + "-" + ctrlType + "-" + control[i].FieldName;
        }
        ///TITLE
        var labTitle = XR.language ? control[i].DisplayNameEn : control[i].DisplayNameCn;
        ///属性
        var bindName = control[i].FieldName;
        if (isValidData(bindName)) {
            ///正则表达式校验
            var verifyValue = verifyRegexResult(id, labTitle, control[i].ControlType);
            ///校验通过，无错误信息
            if (!isValidData(verifyValue)) {
                ///JSON格式配置
                var configJson = null;
                if (isValidData(control[i].Extend3) && control[i].Extend3.indexOf('^') > 0) {
                    var paramItem = control[i].Extend3.split('^');
                    if (paramItem[0] == 'sql') {
                        var paramValue = paramItem[1];
                        var paramControlItem = paramValue.split("|");
                        configJson = GetComboExtendJson(paramControlItem[0]);
                    }
                }
                ///原始控件上的内容
                var value = getControlValueByControlType(id, control[i].ControlType, isValidData(configJson) ? configJson.isMultiple : false);
                ///日期控件格式处理
                if (control[i].ControlType == '50' || control[i].ControlType == '60') {
                    ///日期格式
                    if (isValidData(control[i].Extend1) && control[i].Extend1.indexOf(':') != -1) {
                        configJson = GetComboExtendJson(control[i].Extend1);
                    }
                    ///日期处理方式
                    if (isValidData(configJson) && isValidData(configJson.dateFormat)) {
                        var dateValue = FORM.formatDateByJson(value, configJson.dateFormat);
                        if (isValidData(configJson.dateFieldName)) {
                            dataPar += configJson.dateFieldName + "=" + escape(dateValue) + "&";
                        }
                        else {
                            if (isValidData(dateValue)) {
                                dataPar += bindName + "=" + escape(dateValue) + "&";
                            } else {
                                dataPar += bindName + "=&";
                            }
                        }
                        continue;
                    }
                    ///时间处理方式
                    if (isValidData(configJson) && isValidData(configJson.timeFormat)) {
                        var timeValue = FORM.formatDateByJson(value, configJson.timeFormat);
                        if (isValidData(configJson.timeFieldName)) {
                            dataPar += configJson.timeFieldName + "=" + escape(timeValue) + "&";
                        }
                        else {
                            if (isValidData(timeValue)) {
                                dataPar += bindName + "=" + escape(timeValue) + "&";
                            } else {
                                dataPar += bindName + "=&";
                            }
                        }
                        continue;
                    }
                }
                if (isValidData(value)) {
                    dataPar += bindName + "=" + escape(value) + "&";
                } else {
                    dataPar += bindName + "=&";
                }
                //2017-03-08新增combobox控件可以同时保存TEXT
                if (isValidData(configJson) && isValidData(configJson.textFieldName)) {
                    switch (control[i].ControlType + '') {
                        case "20":
                        case "80":
                        case '100':
                            value = $('#' + id).combobox('getText');
                            break;
                        case "30":
                            value = $('#' + id).combotree('getText');
                            break;
                        case "40":
                            value = $('#' + id).combogrid('getText');
                            break;
                    }
                    if (isValidData(value)) {
                        dataPar += configJson.textFieldName + "=" + escape(value) + "&";
                    } else {
                        dataPar += configJson.textFieldName + "=&";
                    }
                }
            }
            else {
                closeMessagerProgress();
                var title = languageMessageTitle('1x00000001');
                tAlert("error", title, verifyValue, "", "");
                return;
            }
        }
    }
    return dataPar;
}

//绑定控件数据 --START
//tId:控件ID
//tType:控件类型
//extend:数据绑定方法
function bindControlData(tId, tType, extend, codeName, tDefaultVal) {
    if (extend != null && extend != undefined && extend != "") {
        var paramItem = extend.split(':');
        var paramType = "";
        if (paramItem.length > 1) {
            paramType = paramItem[0];
            var paramValue = paramItem[1];
            switch (paramType) {
                case "sql":
                    eval("$('#' + tId)." + tType + "({method:'post'})");
                    var dataParams = "method=ajaxControl&CONTROL_TYPE=" + tType + "&SQL=" + paramValue;
                    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:bindControlDataCallback', false);
                    break;
                case "json":
                    eval("$('#' + tId)." + tType + "({method:'get'})");
                    $.getJSON(XR.getPathLevel() + '/JS/data/common.data.json', function (data) {

                        eval("$('#' + tId)." + tType + "({data: eval('data.'+paramValue)})");

                        tDefaultVal = getFormatterComboxValue(tDefaultVal, null);
                        if (tDefaultVal != "") {
                            eval("$('#' + tId)." + tType + "('setValue',  tDefaultVal);");
                        }
                    });

                    break;
                case "method":
                    eval(paramValue);
                    break;
                default:

                    if (codeName != null && codeName != undefined && codeName != "") {
                        var dataParams = 'method=ajaxCodeList&CODE_NAME=' + codeName + "&ENTITY_NAME=Code&AN=BLL.SYS";
                        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:bindControlCodeDataCallback', false);
                    }
                    break;
            }
        }
    } else if (codeName != null && codeName != undefined && codeName != "") {

        var dataParams = 'method=ajaxCodeList&CODE_NAME=' + codeName + "&ENTITY_NAME=Code&AN=BLL.SYS";
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:bindControlCodeDataCallback', false);
    }
}

function bindControlDataCallback(data) {
    if (data != null && data != undefined && data != "") {
        var controlData = eval("(" + data + ")");
        eval("$('#' + tId)." + tType + "({data: controlData});");

        tDefaultVal = getFormatterComboxValue(tDefaultVal, null);
        if (tDefaultVal != "") {
            eval("$('#' + tId)." + tType + "('setValue',  tDefaultVal);");
        }
    }
}

function bindControlCodeDataCallback(data) {
    if (data != null && data != undefined && data != "") {
        data = eval("(" + data + ")");
        eval("$('#' + tId)." + tType + "({data: data.rows});");

        tDefaultVal = getFormatterComboxValue(tDefaultVal, codeName);

        if (tDefaultVal != "") {
            eval("$('#' + tId)." + tType + "('setValue',  tDefaultVal);");
        }
    }
}

//绑定控件数据 --END
function defaultFormatterColumns(value, row, index, codeName) {
    var itemDisplay = "";
    var controlData = parent.arrGlobal.get('controlData_' + codeName);
    if (controlData != undefined) {
        for (var i = 0; i < controlData.length; i++) {
            //false == 2, true == 1;  LXR.2016.6.2
            var convetVal = getFormatterComboxValue(value, codeName);
            if (controlData[i].ItemValue == convetVal) {
                return itemDisplay = controlData[i].ItemDisplay;
            }
        }
    }
    return itemDisplay;
}
function formatterDateBoxByColumns(value, row, index) {

    var itemDisplay = "";
    if (value) {
        //itemDisplay = "<input type='checkbox' checked='checked' disabled='true'/>"
    } else {
        //itemDisplay = "<input type='checkbox' disabled='true' />"
    }
    return itemDisplay;
}

function formatterComboBoxByColumns(value, row, index, columnName, extend) {
    var paramItem = extend.split('^');
    var paramType = "";
    if (paramItem.length > 1) {
        paramType = paramItem[0];
        switch (paramType) {
            case "sql":
                var configJson = GetComboExtendJson(paramItem[1].split('|')[0]);
                var controlData = parent.arrGlobal.get('controlData_' + columnName);
                if (isValidData(controlData)) {
                    for (var i = 0; i < controlData.length; i++) {
                        var ctlVal = eval("controlData[i]." + configJson.idField);
                        if (ctlVal == value) {
                            return itemDisplay = eval("controlData[i]." + configJson.textField);
                        }
                    }
                }
                break;
            case "json":
                var isShowItemIcon = false;
                ExtendJsonDataName = paramItem[1].split(',')[0];
                if (paramItem[1].split(',').length > 1)
                    isShowItemIcon = paramItem[1].split(',')[1] == 'true' ? true : false;
                var controlData = eval("commonJsonData." + ExtendJsonDataName);//jsonData.Icon_Item;
                if (isValidData(controlData)) {
                    for (var i = 0; i < controlData.length; i++) {
                        if (controlData[i].id == value) {
                            if (ExtendJsonDataName == "Icon_Item" && isShowItemIcon) {
                                return "<img height='16px' src='" + controlData[i].iconImg + "'>" + controlData[i].text + "</img>";
                            } else {
                                return controlData[i].text;
                            }
                        }
                    }
                }
                break;
            default: break;
        }
    }
}


function formatterComboGirdByColumnsValue(value, row, index, columnName, extend) {

    if (arrColumnsValueItem.get(columnName) != null && arrColumnsValueItem.get(columnName) != undefined && arrColumnsValueItem.get(columnName) != "") {

        var valueItem = arrColumnsValueItem.get(columnName);
        valueItem += "'" + value + "',";
        arrColumnsValueItem.put(columnName, valueItem);
    } else {
        arrColumnsValueItem.put(columnName, "'" + value + "',");
        arrColumnsValueItem.put(columnName + "^", extend);
    }
}

function formatterComboGridByColumns(value, columnName, configIdField, configTextField) {
    var itemDisplay = "";
    var controlData = parent.arrGlobal.get('controlData_' + columnName);
    if (controlData != undefined) {
        var textNameItem = configTextField.split('‖');
        for (var i = 0; i < controlData.length; i++) {
            var ctlVal = eval("controlData[i]." + configIdField);
            if (ctlVal == value) {
                if (textNameItem.length == 2) {
                    return itemDisplay = eval("controlData[i]." + textNameItem[1]) + "「" + eval("controlData[i]." + textNameItem[0]) + "」";
                } else {
                    return itemDisplay = eval("controlData[i]." + textNameItem[0]);
                }
            }
        }
    }
    return itemDisplay;
}

function formatterGridColumnsForComboGridData(thisGridId, data) {

    arrColumnsValueItem = arrColumnsValueItem.split('^');
    if (arrColumnsValueItem.length > 1) {
        var columnsItemsKey = arrColumnsValueItem[0];
        var columnsItemsValue = "";
        var entityExtendItem = arrColumnsValueItem[1].replace('sql^', '');

        var paramControlItem = entityExtendItem.split("⊙");
        var jsonItem = paramControlItem[0].replace("对象名", "\"entityName\"");
        jsonItem = jsonItem.replace("排序名", "\"sortName\"");
        jsonItem = jsonItem.replace("排序方式", "\"sortOrder\"");
        jsonItem = jsonItem.replace("绑定字段", "\"idField\"");
        jsonItem = jsonItem.replace("条件", "\"filter\"");
        jsonItem = jsonItem.replace("显示字段", "\"textField\"");
        jsonItem = jsonItem.replace("列", "\"columns\"");
        jsonItem = jsonItem.replace("命名空间名", "\"AN\"");
        jsonItem = jsonItem.replace(/:/g, ":\"");
        jsonItem = jsonItem.replace(/,/g, "\",");
        jsonItem = "{" + jsonItem + "\"}";
        var configJson = eval('(' + jsonItem + ')');

        for (var i = 0; i < data.rows.length; i++) {
            var val = eval("data.rows[i]." + columnsItemsKey);
            if (val != undefined) {
                columnsItemsValue += "'" + val + "',";
            }
        }

        if (columnsItemsValue.length > 0) {
            columnsItemsValue = (columnsItemsValue + "^").replace(",^", "").replace("^", "");

            var dataParams = "method=ajaxControlDataToColumn&CONTROL_EXTEND=" + entityExtendItem + "&CONTROL_TYPE=combogrid&COLUMN_NAME=" + columnsItemsKey + "&ENTITY_NAME=" + configJson.entityName + "&AN=" + configJson.AN + "&FILTER= AND " + convertColumnByFieldName(configJson.idField) + " IN (" + columnsItemsValue + ")";

            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:getFormatterColumnsData", false);

            $('#' + thisGridId).datagrid('getColumnOption', columnsItemsKey).formatter = function (value, row, index) { return formatterComboGridByColumns(value, columnsItemsKey, configJson.idField, configJson.textField); };
        }
    }

    window.arrColumnsValueItem = "";
}
///
function formatterComboTreeByColumns(value, row, index, columnName, extend) {
    var paramItem = extend.split('^');
    if (paramItem.length > 1) {
        var paramType = paramItem[0];
        var paramValue = paramItem[1];
        switch (paramType) {
            case "sql":
                var controlData = parent.arrGlobal.get('controlData_' + columnName);
                if (!isValidData(controlData)) break;
                var paramControlItem = paramValue.split("|");
                var configJson = GetComboExtendJson(paramControlItem[0]);
                for (var i = 0; i < controlData.length; i++) {
                    var ctlVal = eval("controlData[i]." + configJson.idField);
                    if (ctlVal != value) continue;
                    return eval("controlData[i]." + configJson.textField);
                }
                break;
            case "json": break;
            default: break;
        }
    }
}

function formatterCheckBoxByColumns(value, row, index) {

    var itemDisplay = "";
    if (value) {
        itemDisplay = "<input type='checkbox' checked='checked' disabled='true'/>"
    } else {
        itemDisplay = "<input type='checkbox' disabled='true' />"
    }
    return itemDisplay;
}

function getFormatterColumnsData(data) {
    data = eval('(' + data + ')');
    parent.arrGlobal.put('controlData_' + data.controlName, data.rows);
}

function getFormatterControlData(data, codeName) {
    data = eval('(' + data + ')');
    parent.arrGlobal.put('controlData_' + codeName, data);
}
///获取复杂过滤条件
function getFormatFilterByExtendData(paramFilterItem, parentRowsData, dataAttributeName) {
    var paramFilter = "";
    ///多个联动控件的配置
    var paramFilterCount = paramFilterItem.split('⊙');
    for (var i = 0; i < paramFilterCount.length; i++) {
        ///单个联动控件的配置
        var paramFilterType = paramFilterCount[i].split('、');
        ///固定联动条件
        if (paramFilterType[0] == "30") {
            ///and 字段 = '值'
            paramFilter += "AND " + paramFilterType[1] + "='" + paramFilterType[2] + "'";
        }
        ///复杂配置
        else if (paramFilterType[0] == "10") {
            ///父级控件.属性名 or 属性名
            var isParent = paramFilterType[2].split('.');
            var parentObj = "";
            var fieldName = "";
            if (isParent.length > 1) {
                parentObj = isParent[0] + ".";
                fieldName = isParent[1];
            } else {
                fieldName = paramFilterType[2];
            }
            ///控件名
            var tId = "#" + paramFilterType[1] + "-" + fieldName;
            var objValue = "";
            if (paramFilterType[1] == "10" || paramFilterType[1] == "90") {
                objValue = eval(parentObj + "$('" + tId + "').textbox('getValue')");
            }
            else if (paramFilterType[1] == "20" || paramFilterType[1] == "30" || paramFilterType[1] == "40" || paramFilterType[1] == "80" || paramFilterType[1] == "100") {
                objValue = eval(parentObj + "$('" + tId + "').combobox('getValue')");
            }
            ///获取页面内存中的集合数据
            if (paramFilterType.length > 4) {
                var nameItem = paramFilterType[4].split('-');
                var controlData = parent.arrGlobal.get('controlData_' + fieldName);
                if (!isValidData(controlData)) {
                    if (paramFilterType[1] == "20" || paramFilterType[1] == "30" || paramFilterType[1] == "40" || paramFilterType[1] == "80" || paramFilterType[1] == "100") {
                        ///获取下拉框类型控件的属性集合
                        var dataParams = eval(parentObj + "$('" + tId + "').combobox('options')");
                        if (isValidData(dataParams) && isValidData(dataParams.url)) {
                            HELP.ajaxCommon(dataParams.url, '', "js:getFormatterColumnsData", false);
                            controlData = parent.arrGlobal.get('controlData_' + fieldName);
                        }
                    }
                }
                if (isValidData(controlData) && nameItem.length > 1) {
                    for (var k = 0; k < controlData.length; k++) {
                        var ctlVal = eval("controlData[k]." + nameItem[0]);
                        if (ctlVal == objValue) {
                            objValue = eval("controlData[k]." + nameItem[1]);
                            break;
                        }
                    }
                }
            }
            if (isValidData(objValue)) {
                paramFilter += "AND " + paramFilterType[3] + "='" + objValue + "'";
            }
        }
        ///父级数据的某属性作为检索条件
        else if (paramFilterType[0] == "20") {
            if (isValidData(parentRowsData)) {
                var parVal = eval("parentRowsData." + paramFilterType[2]);
                if (isValidData(dataAttributeName)) {
                    ///联动时不产生重复条件?
                    if (paramFilterType[2] != dataAttributeName) {
                        paramFilter += "and [" + paramFilterType[1] + "] = N'" + parVal + "' ";
                    }
                }
                else {
                    paramFilter += "and [" + paramFilterType[1] + "] = N'" + parVal + "' ";
                }
            }
        }
        ///权限过滤
        else if (paramFilterType[0] == "40") {
            var filterParams = getDataAuthByEntityAuthConfig(paramFilterType[1]);
            if (isValidData(filterParams)) {
                paramFilter += filterParams;
            }
        }
    }
    return paramFilter;
}

function getContorlBindData(data) {
    var cboData = eval('(' + data + ')')
    var controlData = JSON.stringify(cboData.rows);
    controlData = controlData.replace(/ItemValue/g, "id").replace(/ItemDisplay/g, "text");
    eval("$('#' + tId)." + tType + "({data: eval(controlData)});");
}

///设置页面GRID表单容器宽度
function setGridWidth() {
    var gridWidthByForm = $("#formItem").width();
    var divActionWidth = $('#' + divActionMenuId).width();
    if (isValidData(divActionWidth)) {
        gridWidthByForm = divActionWidth * 1;
    }
    var pageType = $("#formItem").attr('nowpage')
    if (pageType == "edit") {
        gridWidthByForm = gridWidthByForm * 1;
    }
    else {
        gridWidthByForm = gridWidthByForm * 1;
    }
    return gridWidthByForm;
}

///设置页面GRID表单容器高度
function setGridHeight() {
    var divActionMenuHeight = $('#' + divActionMenuId).height();
    var divSearchFormHeight = $('#' + divSearchFormId).height();
    var divControlFormHeight = $('#' + divControlFormId).height();
    var divActionDetailHeight = $('#' + divActionDetailId).height();
    var dataGridHeight = $(window).height();
    if (isValidData(divActionMenuHeight)) {
        dataGridHeight -= divActionMenuHeight * 1;
        dataGridHeight -= 2;
    }
    if (isValidData(divSearchFormHeight)) {
        dataGridHeight -= divSearchFormHeight * 1;
        dataGridHeight -= 8;
    }
    if (isValidData(divActionDetailHeight)) {
        dataGridHeight -= divActionDetailHeight * 1;
        dataGridHeight -= 2;
    }
    if (isValidData(divControlFormHeight)) {
        dataGridHeight -= divControlFormHeight * 1;
        dataGridHeight -= 2;
    }
    var pageType = $("#formItem").attr('nowpage')
    if (pageType == "edit") {
        dataGridHeight -= 25;
    }
    else {
        dataGridHeight -= 22;
    }
    return dataGridHeight;
}


var choiceNowGridID = "";
//createDetailgrid(null, tblGridId + "_", entityName, gSortName, gSortOrder, gColumns);
function createDetailgrid(parentID, id, data, gIdField, gSortName, gSortOrder, gColumns, formEntityInfo) {

    var gParentField = formEntityInfo.ParentField == "" ? "" : formEntityInfo.ParentField;//name-parentId:name:treeField、parentId:父子段

    var gParentFieldList = gParentField.split('-');
    var FieldName = "";
    var gPIdField = "";
    if (gParentFieldList.length > 0) {
        gIdField = gParentField.split('-')[0];
        gPIdField = gParentField.split('-')[1];
        FieldName = gParentField.split('-')[2];
    }


    $('#' + id).datagrid({
        data: data,
        width: '99%',
        methon: 'POST',
        rownumbers: true,
        collapsible: true,
        animate: true,
        fitColumns: true,
        pagination: true,
        // height: height,
        idField: gIdField,
        singleSelect: true,
        sortName: gSortName,
        sortOrder: gSortOrder,
        columns: gColumns,//[[{ field: "productid", title: "id", width: 50 }, { field: "productname", title: "name", width: 50 }]],
        view: detailview,
        detailFormatter: function (index, row) {
            var nowID = "0⊙" + id + "_" + index;

            return '<div style="padding:2px;"><div id="' + nowID + '"></div></div>';

        },
        onExpandRow: function (index, row) {

            // var pid = "0⊙" + id + "_" + index;
            // var div1 = document.createElement("div");
            // var tid = pid.replace("0⊙", (i + 1) + "⊙");
            // div1.id = tid;
            // document.getElementById(pid).parentNode.appendChild(div1);
            //// var g = creategrid(id, div1.id, eval("json" + (i + 2)));
            //  createDetailgrid(parentID, id, data, gIdField, gSortName, gSortOrder, gColumns, formEntityInfo)

            //for (var i = 0; i < 2; i++) {
            //    var tid = pid.replace("0⊙", (i + 1) + "⊙");
            //    if (document.getElementById(tid) == null) {
            //        var div1 = document.createElement("div");
            //        div1.id = tid;
            //        document.getElementById(pid).parentNode.appendChild(div1);
            //        var g = creategrid(id, div1.id, eval("json" + (i + 2)));
            //        //if (choiceID.indexOf(div1.id + "张") == -1) {
            //        //    choiceID += div1.id + "张";
            //        //}
            //    }
            //}

        },
        onBeforeSelect: function (index, row) {

            if (choiceNowGridID != this.id) {
                $('#' + choiceNowGridID).datagrid("clearSelections");
            }

        },
        onSelect: function (index, row) {
            choiceNowGridID = (this.id);

        },
        onResize: function () {

            if (parentID != null) {
                $('#' + parentID).datagrid('fixDetailRowHeight', parentIndex);
            }
        },
        onLoadSuccess: function () {

            if (parentID != null) {
                setTimeout: (function () {
                    $('#' + parentID).datagrid('fixDetailRowHeight', parentIndex);
                }, 0);
            }
        }
    });
}



function setOnClickRowByGridToForm(row) {
    var entityFieldForm = pageLoadParams.formCreateData;

    //if (row.Fid != "") {
    //    pageLoadParams.formDataKey = row.Fid;
    //   // pageLoadParams.operationType = "update-" + parentArrMap.tableName;
    //} 
    setOnClickRowByGridToFormContent(row, entityFieldForm);
}

///选中的行数据对象，赋值到界面上
function setOnClickRowByGridToFormContent(row, entityFieldForm, ctrlType) {
    ///控件链接符号
    var linkSymbol = '-'
    if (isValidData(ctrlType)) linkSymbol = '-' + ctrlType + '-';
    ///
    if (!isValidData(entityFieldForm)) return;
    for (var i = 0; i < entityFieldForm.length; i++) {
        if (!entityFieldForm[i].Editable) continue;
        var controlName = '';
        var controlTextValue = '';
        var codeName = entityFieldForm[i].Extend1;
        ///数据对象属性值
        var controlValue = eval('row.' + entityFieldForm[i].FieldName);
        switch (entityFieldForm[i].ControlType + '') {
            case '10':
            case '90':
                controlName = 'textbox';
                break;
            case '20':
            case '80':
            case '100':
                controlName = 'combobox';
                controlValue = getFormatterComboxValue(controlValue, codeName);
                if (!isValidData(entityFieldForm[i].Extend3)) break;
                var paramItem = entityFieldForm[i].Extend3.split('^');
                if (paramItem.length < 2) break;
                ///
                var paramType = paramItem[0];
                var paramValue = paramItem[1];
                ///combogrid只支持sql方式
                if (paramType != "sql") break;
                ///联动控件分隔符，从第二段开始为多个联动控件
                var paramControlItem = paramValue.split("|");
                var configJson = GetComboExtendJson(paramControlItem[0]);
                ///是否多选
                var isMultiple = configJson.isMultiple == 'true' ? true : false;
                ///
                if (isMultiple) {
                    var arrayDefaultValues = [];
                    if (isValidData(controlValue)) {
                        arrayDefaultValues = controlValue.split(',');
                    }
                    $('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName).combobox('setValues', arrayDefaultValues);
                    continue;
                }
                break;
            case '30':
                controlName = 'combotree';
                ///
                controlValue = getFormatterComboxValue(controlValue, codeName);
                if (!isValidData(entityFieldForm[i].Extend3)) break;
                var paramItem = entityFieldForm[i].Extend3.split('^');
                if (paramItem.length < 2) break;
                ///
                var paramType = paramItem[0];
                var paramValue = paramItem[1];
                ///combogrid只支持sql方式
                if (paramType != "sql") break;
                ///联动控件分隔符，从第二段开始为多个联动控件
                var paramControlItem = paramValue.split("|");
                var configJson = GetComboExtendJson(paramControlItem[0]);
                ///是否多选
                var isMultiple = configJson.isMultiple == 'true' ? true : false;
                ///
                if (isMultiple) {
                    var arrayDefaultValues = [];
                    if (isValidData(controlValue)) {
                        arrayDefaultValues = controlValue.split(',');
                    }
                    $('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName).combotree('setValues', arrayDefaultValues);
                    continue;
                }
                break;
            case '50':
                controlName = 'datebox';
                var dateformat = XR.language ? 'MM/dd/yyyy' : 'yyyy-MM-dd';
                var configJson;
                if (isValidData(codeName)) {
                    configJson = GetComboExtendJson(codeName);
                    if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                        dateformat = configJson.datetimeFormat;
                }
                controlValue = FORM.formatDateByJson(controlValue, dateformat);
                break;
            case '60':
                controlName = 'datetimebox';
                var dateformat = XR.language ? 'MM/dd/yyyy HH:mm:ss' : 'yyyy-MM-dd HH:mm:ss';
                ///JSON格式配置
                var configJson;
                if (isValidData(codeName)) {
                    configJson = GetComboExtendJson(codeName);
                    if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                        dateformat = configJson.datetimeFormat;
                }
                controlValue = FORM.formatDateByJson(controlValue, dateformat);
                break;
            case '40':
                controlName = 'combogrid';
                if (!isValidData(entityFieldForm[i].Extend3)) break;
                var paramItem = entityFieldForm[i].Extend3.split('^');
                if (paramItem.length < 2) break;
                ///
                var paramType = paramItem[0];
                var paramValue = paramItem[1];
                ///combogrid只支持sql方式
                if (paramType != "sql") break;
                ///联动控件分隔符，从第二段开始为多个联动控件
                var paramControlItem = paramValue.split("|");
                var configJson = GetComboExtendJson(paramControlItem[0]);
                ///是否多选
                var isMultiple = configJson.isMultiple == 'true' ? true : false;
                ///
                if (isMultiple) {
                    var arrayDefaultValues = [];
                    if (isValidData(controlValue)) {
                        arrayDefaultValues = controlValue.split(',');
                    }
                    ///TODO:多选时此处代码有问题
                    $('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName).combogrid('setValues', arrayDefaultValues);
                    continue;
                }
                ///如果默认选中项不为空[且]当前没有选中项时
                ///设置默认选中项为当前选中项，且调用select方法进行赋值，可以触发onSelect事件
                if (isValidData(controlValue)) {
                    $('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName).combogrid('grid').datagrid('reload', { 'q': controlValue });
                    $('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName).combogrid('grid').datagrid('selectRecord', controlValue);
                }
                break;
            case '130':
                controlName = 'timespinner';
                var dateformat = "H:mm";
                var configJson;
                var showSeconds;
                if (isValidData(codeName)) {
                    configJson = GetComboExtendJson(codeName);
                    if (isValidData(configJson) && isValidData(configJson.showSeconds))
                        showSeconds = configJson.showSeconds == 'true' ? true : false;
                }
                if (showSeconds)
                    dateformat = "H:mm:ss";
                ///
                controlValue = FORM.formatDateByJson(controlValue, dateformat);
                break;
            default:
                controlName = "textbox";
                break;
        }
        if (entityFieldForm[i].EditReadonly == 20
            || entityFieldForm[i].EditReadonly == 40) {
            eval("$('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName)." + controlName + "({disabled:true});");
        }
        eval("$('#' + entityFieldForm[i].ControlType + linkSymbol + entityFieldForm[i].FieldName)." + controlName + "('setValue', controlValue);");
    }
}
///处理系统中特殊选项
function getFormatterComboxValue(controlValue, isBoolExtend) {
    ///checkbox中的true = 30,false or null = 20 需要做特殊处理
    if (isValidData(isBoolExtend) && (isBoolExtend == "BOOLEAN" || isBoolExtend == "SYMBOL_BOOLEAN")) {
        return (controlValue + '').replace('undefined', '20').replace('null', '20').replace('true', '30').replace('false', '20');
    } else {
        ///其它选项中 null or GUID.EMPTY 都留空
        return (controlValue + '').replace('undefined', '').replace('null', '').replace('00000000-0000-0000-0000-000000000000', '');
    }
}
function tgLoadData(data, gIdField, gPIdField) {
    //data = eval('(' + data + ')');
    var treeData = arrayToTree(data.rows, gIdField, gPIdField);
    return treeData;
    // $('#' + tblGridId).treegrid("loadData", treeData);
}


function onClickRowByGrid(row) {
}


function onCollapseRowByGrid(row, index) {

    //alert(index);

}
////////////////////////////////////////////////////////XR.js/ end/////////////////////////////////////////////////////////////////////////////////////





////////////////////////////////////////////////////////help.js/ begin/////////////////////////////////////////////////////////////////////////////////////
//键值对数组
//调用例子存：
//    var arr = new Map();
//    arr.put(key,value);
//    取：
//    value=arr.get(key)
//    key= arr.key[index]
function Map() {
    this.keys = new Array();
    this.data = new Array();
    this.key = new Array();
    this.put = function (key, value) {
        if (this.data[key] == null) {
            this.keys.push(value);
            this.key.push(key);
        }
        this.data[key] = value;
    };

    this.get = function (key) {
        return this.data[key];
    };

    this.remove = function (key) {
        this.keys.remove(key);
        this.data[key] = null;
    };

    this.isEmpty = function () {
        return this.keys.length == 0;
    };

    this.size = function () {
        return this.keys.length;
    };
}
///
function putEntity(entityName, entityInfo) {
    if (isValidData(parent.arrGlobal)) parent.arrGlobal.put(entityName, entityInfo);
    if (isValidData(arrTempMap)) arrTempMap.put(entityName, entityInfo);
}
///
function getEntity(entityName) {
    if (!isValidData(parent.arrGlobal))
        return undefined;
    return parent.arrGlobal.get(entityName);
}
///
function openDialogModal(strTitle, strUrl, width, height) {
    var strToolbar;
    var strButtos;
    width = width * 0 != 0 ? 800 : width;
    height = height * 0 != 0 ? 500 : height;

    $('#dialogModal').dialog({
        title: strTitle,
        width: width,
        height: height,
        closed: false,
        cache: false,
        href: strUrl,
        modal: true
    });
    //$('#dd').dialog('refresh', 'new_content.php');

    //parent.$("#sDialog").html(strTitle);
    //parent.$('#dialogModal').html('<iframe src="' + strUrl + '" frameborder="0" id="dialogIframe" class="adaptivewindowsize-inner"></iframe>');
    //parent.document.getElementById("dialogModal").parentNode.style.width = width + "px";
    //parent.document.getElementById("dialogModal").parentNode.style.height = height + "px";
    //parent.document.getElementById("dialogModal").style.width = width + "px";
    //parent.document.getElementById("dialogModal").style.height = height + "px";
    //parent.document.getElementById("sDialog").parentNode.parentNode.style.width = width + "px";
    //parent.$('#dialogModal').window('open');
}
function openIframeModal(strTitle, strUrl, width, height) {
    openMessagerProgress();
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    var strToolbar = pageLoadParams.formUpdateData; //parent.arrGlobal.get('formActionData' + entityName);//parent.XR.formActionData;
    if (strToolbar == undefined || strToolbar == null) {
        strToolbar = [];
    }
    var strButtos;
    var bodyW = parent.document.body.scrollWidth;
    var bodyH = parent.document.body.scrollHeight;
    if (width == 0 || height == 0) {
        width = bodyW * 0.76;
        height = bodyH * 0.86;
    }
    try {

        parent.$('#dialogModal').dialog({
            title: strTitle,
            width: width,
            height: height,
            closed: false,
            //fit: true,
            cache: false,
            content: '<iframe src="' + strUrl + '" frameborder="0" id="dialogIframe" style="width:' + (width - 15) + 'px;height:' + (height - 37) + 'px;"></iframe>',
            //href: strUrl,
            modal: true,
            //maximizable: true,
            //toolbar: strToolbar,
            onClose: function () {
                parent.$('#dialogModal').dialog("clear", "none");
                var dataParamUserFid = "method=ajaxUserFidBySession&ENTITY_NAME=User&AN=BLL.SYS";
                //openMessagerProgress();
                $.ajax({
                    url: XR.defaultProcessUrl(),
                    async: false,
                    type: "POST",
                    data: dataParamUserFid,
                    dataType: 'json',
                    success: function (data) {
                        //closeMessagerProgress();
                        sessionIsNull(data);
                        if (isSessionNull) return;

                        var paretIframe = parent.$('#Iframe_' + pageLoadParams.entityName);
                        if (paretIframe.length > 0) {
                            if (pageLoadParams.formEntityInfo != "" && pageLoadParams.formEntityInfo != null && pageLoadParams.formEntityInfo != undefined) {

                                var parentIframeGrid = paretIframe[0].contentWindow.$('#' + tblGridId);
                                if (parentIframeGrid.length > 0) {
                                    switch (pageLoadParams.formEntityInfo.EntityType + "") {
                                        case "2":
                                            parentIframeGrid.treegrid('reload');
                                            break;
                                        default:
                                            ///
                                            var row = parentIframeGrid.datagrid('getSelected');
                                            var rowIndex = parentIframeGrid.datagrid('getRowIndex', row);
                                            parentIframeGrid.datagrid({
                                                onLoadSuccess: function (data) {
                                                    $(this).datagrid('selectRow', rowIndex);
                                                }
                                            });
                                            parentIframeGrid.datagrid('reload');
                                            break;
                                    }
                                }
                            }
                            else {
                                paretIframe[0].contentWindow.$('#' + tblGridId).treegrid('reload');
                            }
                            pageLoadParams.formUpdateData = "";
                        }
                    }
                });
            }
        });
        parent.$('#dialogModal').dialog('center');
    } catch (e) {

    }
    closeMessagerProgress();
}
function openMsg(strTitle, strMsg, okFunctionName, closeFunctionName) {
    okFunction = okFunctionName;
    closeFunction = closeFunctionName;
    parent.document.getElementById("dialogLayout").childNodes[1].childNodes[1].childNodes[0].innerHTML = strMsg;
    parent.document.getElementById("sDialogLayout").innerHTML = strTitle;
    parent.$('#dialogLayout').window('open');

}
///获取正则表达式验证报错信息
function verifyRegexResult(controlId, labTitle, controlType) {
    var errorMessage = '';
    switch (controlType + '') {
        case "10":
        case "90":
            var isValid = $('#' + controlId).textbox('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).textbox('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            if (isValidData(ctlOptions.invalidMessage))
                errorMessage += labTitle + ctlOptions.invalidMessage;
            break;
        case '20':
        case '80':
        case '100':
            var isValid = $('#' + controlId).combobox('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).combobox('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            break;
        case '30':
            var isValid = $('#' + controlId).combotree('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).combotree('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            break;
        case '40':
            var isValid = $('#' + controlId).combogrid('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).combogrid('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            break;
        case '50':
        case '60':
            var isValid = $('#' + controlId).datebox('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).datebox('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            break;
        case '70':
            var isValid = $('#' + controlId).numberbox('isValid');
            if (isValid) return '';
            var ctlOptions = $('#' + controlId).numberbox('options');
            if (isValidData(ctlOptions.missingMessage))
                errorMessage += labTitle + ctlOptions.missingMessage;
            break;
        default: return '';
    }
    return errorMessage;
}

//验证类型。是否为空：notnull ，整型：int，长度限制：len=2，len<2,len>2
function verifyResult(value, req, des) {
    // var newRow = "\r\n";
    var newRow = "<br />";
    var result = "";
    var reqType = "";
    var reqTypeItem = "";
    if (req != undefined && req != null && req != "") {
        if (req.split(":").length > 1) {
            reqType = req.split(":")[0];
            reqTypeItem = req.split(":")[1];
        } else {
            reqType = req.split(":")[0];
        }
    } else {
        reqType = req;
    }
    switch (reqType) {
        case "notnull":
            if (value == null || value == undefined || value == "") {
                var data = languageMessageData('0x00000065');//不能为空
                result = des + "：" + data;
            } else {
                return "";
            }
            break;
        case "int":
            if (!isNaN(value * 1)) {
                return "";
            } else {
                var data = languageMessageData('0x00000066');//只能为整型
                result = des + "：" + data;
            }
            break;
        case "date":
            //验证配置： date:>、50、StartDate;  date 为日期格式，用：分隔取比较值，> 值比较符号， 50 控件类型，StartDate 控件实体名称
            if (reqTypeItem != "") {
                reqTypeItem = reqTypeItem.split("、");
                var startData = "";
                switch (reqTypeItem[1]) {
                    case "10":
                        startData = $("#" + reqTypeItem[1] + "-" + reqTypeItem[2]).textbox("getValue");
                        break;
                    case "50":
                        startData = $("#" + reqTypeItem[1] + "-" + reqTypeItem[2]).datebox("getValue");
                        break;
                    case "60":
                        startData = $("#" + reqTypeItem[1] + "-" + reqTypeItem[2]).datetimebox("getValue");
                        break;
                    default:
                        startData = $("#" + reqTypeItem[1] + "-" + reqTypeItem[2]).datebox("getValue");
                        break;
                }
                var startDate = new Date(startData.replace(/-/, "/"));
                var endDate = new Date(value.replace(/-/, "/"));
                var isConfim = endDate.getTime() + " " + reqTypeItem[0] + " " + startDate.getTime();
                if (!eval(isConfim)) {
                    var data = languageMessageData('0x00000067');//结束日期必须大于开始日期
                    result = des + ":" + data;
                }
            }
            break;
        case "[\\u4e00-\\u9fa5]":
            var re = new RegExp(reqType);
            if (re.test(value)) {
                return "";
            } else {
                var data = languageMessageData('0x00000066');//只能为中文字符
                result = des + "：" + data;
            }
            break;
        default:
            if (req.indexOf("len") != -1) {
                req = req.replace("len", "");
                if (eval("value.length" + req)) {
                    return "";
                } else {
                    var data = languageMessageData('0x00000068');//字符长度应该
                    result = des + "：" + data + req.replace("=", "");
                }
            }
            else {
                return "";
            }
            break;

    }
    result = result != "" ? result + newRow : result;
    return result;

}
//变更语言
function onChangeLanguage(langu) {
    //加载easyui的对应语言包
    var src = 'JS/locale/easyui-lang-' + langu.id + '.js';
    //通过动态注册javascript的方式
    var script = document.createElement("script");
    script.src = src;
    document.getElementsByTagName("head")[0].appendChild(script);
    //将语言设置保存在cookie中
    //设置有效期7天
    //目前仅支持en-us和zh-cn
    if (langu.id == 'en-us') {
        $.cookie.set('language', 'en-us', 7);
    }
    else {
        $.cookie.set('language', 'zh-cn', 7);
    }
    //最后刷新整个浏览器窗体
    window.location.reload();
}

//实体字段名-转换成 表字段名 XR。2016.5.31
function convertColumnByFieldName(parentFieldName) {
    var resultFileld = "";
    var tempgPIdField = parentFieldName.toLowerCase();
    for (var i = 0; i < parentFieldName.length; i++) {
        if (i > 0 && tempgPIdField[i] != parentFieldName[i]) {
            resultFileld += "_" + parentFieldName[i];
        } else {
            resultFileld += tempgPIdField[i];
        }
    }
    return resultFileld.toUpperCase();
}
///
function alertMessage(resultData) {
    ///关闭LOADING
    closeMessagerProgress();
    var titleAlert = languageMessageTitle('1x00000001');///提示
    var successMsg = languageMessageData('0x00000069');///操作成功
    var failMsg = languageMessageData('0x00000173');///操作失败
    var errorMsg = languageMessageData('0x00000084');///数据错误
    ///如果返回false
    if ((resultData + '').toLowerCase() == 'false') {
        ///提示-操作失败
        tAlert('alert', titleAlert, failMsg, 500, 200); return false;
    }
    ///如果返回false
    if ((resultData + '').toLowerCase() == 'true') {
        ///提示-操作成功
        tAlert('alert', titleAlert, successMsg, 500, 200); return false;
    }
    ///
    if (!isValidData(resultData)) {
        ///提示-数据错误
        tAlert('alert', titleAlert, errorMsg, 500, 200); return false;
    }
    ///
    if (isJsonFormat(resultData)) {
        ///提示-操作成功
        tAlert('alert', titleAlert, successMsg, 500, 200); return true;
    }
    ///错误信息处理
    if (resultData.indexOf("Err_:") != -1) {
        errorMsg = resultData.replace('Err_:', '');
        ///错误代码处理
        if (errorMsg.indexOf("MC:") != -1) {
            ///错误代码
            var errorMsgCode = errorMsg.replace('MC:', '');
            errorMsg = languageMessageData(errorMsgCode);
        }
        tAlert('error', titleAlert, errorMsg, 500, 200); return false;
    }
    ///返回数值型
    if (resultData * 0 == 0) {
        ///提示-操作成功
        tAlert('alert', titleAlert, successMsg, 500, 200); return true;
    }
    ///如果返回true
    if ((resultData + '').toLowerCase() == 'true') {
        ///提示-操作成功
        tAlert('alert', titleAlert, successMsg, 500, 200); return true;
    }
    ///返回消息代码
    if (resultData.indexOf('MC:') != -1) {
        var alertMsgCode = resultData.replace('MC:', '');
        successMsg = languageMessageData(alertMsgCode);
        tAlert('alert', titleAlert, successMsg, 500, 200); return true;
    }
    ///
    tAlert('alert', titleAlert, resultData, 500, 200);
    return true;
}
///combobox的清空按钮
function comboxIconClear(e) {
    var iconClearId = e.name.replace("-clear", "");
    $("#" + iconClearId).combobox("setValue", "");
}


function convertControlType(cotType) {
    return cotType / 10 + "" == "NaN" ? cotType : cotType / 10;
}
///获取控件中的值
function getControlValueByControlType(controlId, controlType, isMultiple) {
    var controlValue = '';
    var values = [];
    switch (controlType + '') {
        case "20":
        case "80":
        case '100':
            if (isMultiple) {
                values = $('#' + controlId).combobox('getValues');
                for (var i = 0; i < values.length; i++) {
                    controlValue += ',' + values[i];
                }
                if (isValidData(controlValue))
                    controlValue = controlValue.substring(1);
            }
            else
                controlValue = $('#' + controlId).combobox('getValue');
            break;
        case "30":
            if (isMultiple) {
                values = $('#' + controlId).combotree('getValues');
                for (var i = 0; i < values.length; i++) {
                    controlValue += ',' + values[i];
                }
                if (isValidData(controlValue))
                    controlValue = controlValue.substring(1);
            }
            else
                controlValue = $('#' + controlId).combotree('getValue');
            break;
        case "40":
            if (isMultiple) {
                values = $('#' + controlId).combogrid('getValues');
                for (var i = 0; i < values.length; i++) {
                    controlValue += ',' + values[i];
                }
                if (isValidData(controlValue))
                    controlValue = controlValue.substring(1);
            }
            else
                controlValue = $('#' + controlId).combogrid('getValue');
            break;
        case "50":
        case "60":
            controlValue = $('#' + controlId).datebox('getValue');
            break;
        case '70':
            controlValue = $('#' + controlId).numberbox('getValue');
            break;
        case '90':
            controlValue = $('#' + controlId).textbox('getText');
            break;
        case "110":
            controlValue = $('#' + controlId).filebox('getText');
            break;
        case "120":
            if (controlId == "120-HelpContextCn") {
                controlValue = escape(editorZn.$txt.html());
            } else if (controlId == "120-HelpContextEn") {
                controlValue = escape(editorEn.$txt.html());
            }
            break;
        default:
            controlValue = $('#' + controlId).val();
            break;
    }
    return controlValue;
}

function verifyFileContentType() {

}


function closeWebPage() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {

        window.open('', '_top');
        window.top.close();
    } else if (navigator.userAgent.indexOf("Firefox") > 0) {
        window.location.href = 'about:blank';
    } else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}


function getGridUrlParamsByUserRolesAuth(entityInfo) {
    var gridUrlParamsItem = "";
    if (entityInfo.Extend2 != null && entityInfo.Extend2 != undefined && entityInfo.Extend2 != "") {
        var extendItems = entityInfo.Extend2.split(',');
        if (extendItems.length > 1) {

        }
    }
    return;
}

///获取权限过滤条件
function getDataAuthByEntityAuthConfig(authConfig) {
    var authFilters = '';
    if (!isValidData(authConfig))
        return authFilters;
    if (authConfig + '' == 'GridNotFormatter')
        return '';
    var filterItems = authConfig.split('。');
    for (var i = 0; i < filterItems.length; i++) {
        var dataItems = filterItems[i].split("|");
        var authFilterString = '';
        for (var j = 0; j < dataItems.length; j++) {
            var dataItem = dataItems[j].split("-");
            var paramFieldData = parent.arrUserRoleDataAuth.get(dataItem[1]);
            if (!isValidData(paramFieldData)) continue;
            if (paramFieldData == '1=0') {
                authFilters = ' and 1=0 ';
                break;
            }
            if (paramFieldData == '1=1') continue;
            authFilterString += 'or [' + dataItem[0] + '] in (' + paramFieldData + ') ';
        }
        if (isValidData(authFilterString))
            authFilters += ' and (' + authFilterString.substring(3) + ') ';
    }
    return authFilters;
}

///转换为有效的URL地址
function filterEncodeURIComponent(filterData) {
    if (filterData.indexOf('#') > -1 || filterData.indexOf('/') > -1) {
        return encodeURIComponent(filterData);
    }
    return filterData;
}
///校验数据是否有效，null或undefined或空都视为无效
function isValidData(data) {
    if (data == null) return false;
    if (data == undefined) return false;
    if (data == '') return false;
    return true;
}
///combo类型控件配置的extend转换为json
function GetComboExtendJson(extend) {
    ///把主体配置条件转换为JSON对象
    var jsonItem = extend.replace("对象名", "\"entityName\"");
    jsonItem = jsonItem.replace("绑定字段", "\"idField\"");
    jsonItem = jsonItem.replace("显示字段", "\"textField\"");
    jsonItem = jsonItem.replace("命名空间名", "\"AN\"");
    jsonItem = jsonItem.replace("函数名", "\"ajaxMethod\"");
    jsonItem = jsonItem.replace("控件类型", "\"controlType\"");
    jsonItem = jsonItem.replace("属性名称", "\"attributeName\"");
    ///sqlFilter可以考虑合并至complexFilter
    jsonItem = jsonItem.replace("数据库条件", "\"sqlFilter\"");
    jsonItem = jsonItem.replace("复杂过滤条件", "\"complexFilter\"");
    ///2017-05-22添加数据库排序条件
    jsonItem = jsonItem.replace("数据库默认排序", "\"sqlSort\"");
    ///2017-09-12 多选开关
    jsonItem = jsonItem.replace("是否多选", "\"isMultiple\"");
    ///2018-04-17 有条件进行联动逻辑
    jsonItem = jsonItem.replace("联动逻辑属性", "\"linkageAttribute\"");
    jsonItem = jsonItem.replace("联动逻辑条件", "\"linkageLogic\"");
    jsonItem = jsonItem.replace("联动比对值", "\"linkageCompareValue\"");
    ///Linkage专用属性
    jsonItem = jsonItem.replace("数据属性名称", "\"dataAttributeName\"");
    jsonItem = jsonItem.replace("字段名称", "\"fieldName\"");
    jsonItem = jsonItem.replace("只读标记", "\"readonlyFlag\"");
    ///Tree专用属性
    jsonItem = jsonItem.replace("父节点字段", "\"parentId\"");
    ///Grid专用属性
    jsonItem = jsonItem.replace("排序名", "\"sortName\"");
    jsonItem = jsonItem.replace("排序方式", "\"sortOrder\"");
    jsonItem = jsonItem.replace("列", "\"columns\"");
    jsonItem = jsonItem.replace("前台过滤条件", "\"comboFilter\"");
    ///日期控件
    jsonItem = jsonItem.replace("日期时间格式", "\"datetimeFormat\"");
    jsonItem = jsonItem.replace("日期格式", "\"dateFormat\"");
    jsonItem = jsonItem.replace("时间格式", "\"timeFormat\"");
    jsonItem = jsonItem.replace("日期字段", "\"dateFieldName\"");
    jsonItem = jsonItem.replace("时间字段", "\"timeFieldName\"");
    jsonItem = jsonItem.replace("保存文本字段", "\"textFieldName\"");
    jsonItem = jsonItem.replace("是否显示秒数", "\"showSeconds\"");
    ///数值计算
    jsonItem = jsonItem.replace("计算公式", "\"formular\"");
    jsonItem = jsonItem.replace("公式参数", "\"formularParam\"");
    jsonItem = jsonItem.replace("校验不为零", "\"validNotZero\"");
    ///单元格样式
    jsonItem = jsonItem.replace("样式值", "\"stylerValue\"");
    jsonItem = jsonItem.replace("样式逻辑", "\"stylerLogic\"");
    jsonItem = jsonItem.replace("背景颜色", "\"stylerBackColor\"");
    jsonItem = jsonItem.replace("字体颜色", "\"stylerColor\"");
    ///2018-5-9 扩展属性
    jsonItem = jsonItem.replace("数据变更后函数", "\"changeMethodName\"");

    jsonItem = jsonItem.replace(/:/g, ":\"");
    jsonItem = jsonItem.replace(/,/g, "\",");
    jsonItem = jsonItem.replace(/:" -1/g, ': -1')
    jsonItem = "{" + jsonItem + "\"}";
    return eval('(' + jsonItem + ')');
}
///combo类型控件根据extend.json获取url
function GetDataUrlByExtendJson(extendJson, parentRowsData, selectedRecord, columnName, selectValue) {
    ///method，ajax函数，默认ajaxControl(defaultMethodName) or ajaxTables
    var ajaxMethod = isValidData(extendJson.columns) ? "ajaxTables" : "ajaxControlToCombox";
    if (isValidData(extendJson.ajaxMethod) && extendJson.ajaxMethod != "defaultMethodName") {
        ajaxMethod = extendJson.ajaxMethod;
    }
    ///SQLSORT，数据库默认排序
    var sqlSort = '';
    if (isValidData(extendJson.sqlSort)) {
        sqlSort = extendJson.sqlSort;
    }
    ///FILTER，数据过滤条件
    var sqlFilter = '';
    if (isValidData(extendJson.sqlFilter)) {
        sqlFilter += extendJson.sqlFilter + ' ';
    }
    if (isValidData(extendJson.complexFilter)) {
        sqlFilter += getFormatFilterByExtendData(extendJson.complexFilter, parentRowsData, extendJson.dataAttributeName);
    }
    ///联动控件数据过滤条件
    if (isValidData(extendJson.fieldName)) {
        var mainControlFieldName = extendJson.idField;
        if (isValidData(extendJson.dataAttributeName)) {
            mainControlFieldName = extendJson.dataAttributeName;
        }
        if (isValidData(selectedRecord)) {
            ///在被联动表里通过查询主联动表的主键拿到数据  如果是联动 类型为textbox的控件是无效的
            sqlFilter += 'and ' + extendJson.fieldName + " = ^" + eval("selectedRecord." + mainControlFieldName) + "^ ";
        }
        if (isValidData(selectValue) && !isValidData(selectedRecord)) {
            sqlFilter += 'and ' + extendJson.fieldName + " = ^" + selectValue + "^ ";
        }
    }
    ///URL
    return (XR.defaultProcessUrl()
        + "method=" + ajaxMethod
        + "&FILTER=" + filterEncodeURIComponent(sqlFilter)
        + "&SQLSORT=" + filterEncodeURIComponent(sqlSort)
        + "&COMBOGRID_FILTER=" + extendJson.comboFilter
        + "&ENTITY_NAME=" + extendJson.entityName
        + '&COLUMN_NAME=' + columnName
        + "&AN=" + extendJson.AN).replace(/undefined/g, '');
}

function jsonToExcel(data) {
    var title = GetExcelExportTiltleData();
    var data = GetExcelExportData(data);
    data.splice(0, 0, title);
    for (var index in data) {
        for (var ind in data[index]) {
            data[index][ind] = data[index][ind]["value"];
        }
    }
    var option = {};
    option.fileName = pageLoadParams.entityName;
    option.datas = [
        {
            sheetData: data,
            sheetName: pageLoadParams.entityName,
        }
    ];
    var toExcel = new ExportJsonExcel(option);
    toExcel.saveExcel();



}

function dateToStr(data, formatstr) {
    if (data != "" && data != "undefined" && data != null) {
        var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));

        var year = da.getFullYear();
        var month = da.getMonth() + 1;//js从0开始取 
        var date = da.getDate();
        var hour = da.getHours();
        var minutes = da.getMinutes();
        var second = da.getSeconds();

        if (month < 10) {
            month = "0" + month;
        }
        if (date < 10) {
            date = "0" + date;
        }
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minutes < 10) {
            minutes = "0" + minutes;
        }
        if (second < 10) {
            second = "0" + second;
        }

        var time = year + "-" + month + "-" + date + " " + hour + ":" + minutes + ":" + second; //2009-06-12 17:18:05
        if (formatstr == 'ymd') {
            time = year + "-" + month + "-" + date;
        } else if (formatstr == 'mdy') {
            time = month + "/" + date + "/" + year;
        }
        // alert(time);
        return time;
    }
    else { return null; }
}
function GetExcelExportData(data) {

    data = JSON.parse(data);
    var extend = '';
    var configJson;
    var dtColumns = pageLoadParams.formCreateData;
    for (var j = 0; j < data.length; j++) {
        for (var pro in data[j]) {
            for (var i = 0; i < dtColumns.length; i++) {
                if (i == (dtColumns.length - 1) && dtColumns[i].FieldName != pro) {
                    delete data[j][pro];
                    break;
                }
                if (dtColumns[i].FieldName == pro) {
                    data[j]["order"] = dtColumns[i].ExportExcelOrder;
                    switch (dtColumns[i].ControlType + '') {
                        case '50':
                            ///如果是字符不转换直接显示
                            if (dtColumns[i].DataType + '' == '10')
                                break;
                            else {
                                var dateformat = XR.language ? 'MM/dd/yyyy' : 'yyyy-MM-dd';
                                if (isValidData(dtColumns[i].Extend1)) {
                                    configJson = GetComboExtendJson(dtColumns[i].Extend1);
                                    if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                                        dateformat = configJson.datetimeFormat;
                                }
                                data[j][pro] = dateToStr(data[j][pro], dateformat);
                            }
                            break;
                        case '60':
                            if (dtColumns[i].DataType + '' == '10')
                                break;
                            else {
                                var dateformat = XR.language ? "MM/dd/yyyy HH:mm:ss" : "yyyy-MM-dd HH:mm:ss";
                                ///JSON格式配置
                                if (isValidData(dtColumns[i].Extend1)) {
                                    configJson = GetComboExtendJson(dtColumns[i].Extend1);
                                    if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                                        dateformat = configJson.datetimeFormat;
                                }
                                data[j][pro] = dateToStr(data[j][pro], dateformat);
                            }
                            break;
                        case '20':
                        case '30':
                        case '40':
                            extend = dtColumns[i].Extend3;
                            if (extend) {
                                if (dtColumns[i].Extend3.indexOf('sql^') > -1) {

                                    var paramItem = extend.split('^');
                                    var paramType = "";
                                    if (paramItem.length > 1) {
                                        paramType = paramItem[0];
                                        switch (paramType) {
                                            case "sql":
                                                configJson = GetComboExtendJson(paramItem[1].split('|')[0]);
                                                var controlData = parent.arrGlobal.get('controlData_' + dtColumns[i].FieldName);
                                                for (var p = 0; p < controlData.length; p++) {
                                                    var ctlVal = eval("controlData[p]." + configJson.idField);
                                                    if (ctlVal == data[j][pro]) {
                                                        data[j][pro] = eval("controlData[p]." + configJson.textField);
                                                        break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case '80':
                        case '100':
                            var codeName = dtColumns[i].Extend1;
                            if (dtColumns[i].ControlType == '100') {
                                codeName = 'BOOLEAN';
                            }
                            var controlData = parent.arrGlobal.get('controlData_' + codeName);

                            if (isValidData(controlData)) {
                                for (var i = 0; i < controlData.length; i++) {
                                    var convetVal = getFormatterComboxValue(data[j][pro], pro);

                                    if (controlData[i].ItemValue == convetVal) {
                                        data[j][pro] = controlData[i].ItemDisplay;
                                    }
                                }
                            }
                            break;
                    }
                    break;
                }
            }
        }
    }
    var arrdata = [];
    for (var i = 0; i < data.length; i++) {
        var arr = [];
        for (var pro in data[i]) {
            for (var o = 0; o < dtColumns.length; o++) {
                if (dtColumns[o].FieldName == pro && dtColumns[o].ExportExcelFlag == true) {
                    var str = "{\"value\":" + "\"" + (data[i][pro] == null ? "" : data[i][pro]) + "\",\"order\":\"" + dtColumns[o].ExportExcelOrder + "\"}";
                    arr.push(JSON.parse(str));
                    break;
                }
            }
        }
        arrdata.push(arr);
    }
    for (var i = 0; i < arrdata.length; i++) {
        arrdata[i].sort(compare('order'));
    }
    return arrdata;
}

function GetExcelExportTiltleData() {
    var dtColumns = pageLoadParams.formCreateData
    if (!isValidData(dtColumns)) return [];
    var dt = [];
    for (var i = 0; i < dtColumns.length; i++) {
        if (!dtColumns[i].ExportExcelFlag) continue;
        dt.push(XR.language ? { value: dtColumns[i].DisplayNameEn, order: dtColumns[i].ExportExcelOrder, name: dtColumns[i].FieldName } : { value: dtColumns[i].DisplayNameCn, order: dtColumns[i].ExportExcelOrder, name: dtColumns[i].FieldName });
    }
    dt.sort(compare('order'));
    return dt;

}
function compare(property) {
    return function (a, b) {
        var value1 = a[property];
        var value2 = b[property];
        return value1 - value2;
    }
}
///提交导出EXCEL
function excelExport(locParams) {
    var dataParams = 'method=ajaxExcelExport'
        + '&ENTITY_NAME=' + locParams.entityName
        + '&TABLE_NAMES=' + locParams.tableName
        + '&AN=BLL.' + locParams.bllProjectName
        + '&URL_FILTER=' + locParams.urlFilter
        + '&FILTER=' + filterEncodeURIComponent(getSearchFormData());
    tAlert('slide'
        , titleAlert
        , languageMessageData('1x00000034') ///正在生成Excel文件，请不要关闭此页面
        , 5000
        , 0);
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:downloadFile", true);
}
///向服务端提交下载模板请求
function templateDownload(locParams) {
    var dataParams = 'method=ajaxDownloadExcelTemplate'
        + '&ENTITY_NAME=' + locParams.entityName
        + '&TABLE_NAMES=' + locParams.tableName
        + '&AN=BLL.' + locParams.bllProjectName;
    tAlert('slide'
        , titleAlert
        , languageMessageData('1x00000035') ///正在生成模板文件，请不要关闭此页面
        , 3000
        , 0);
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:downloadFile", true);
}
///下载数据导入模板服务端返回
function resultDownloadTemplate(data) {
    if (!alertMessage(data)) return;
    var filePaths = data.split('|');
    for (var i = 0; i < filePaths.length; i++) {
        window.open(filePaths[i]);
    }
    search();
}
///新页面下载，同时刷新GRID
function downloadFile(data) {
    if (isValidData(data)) {
        window.open(data);
        search();
    }
}
////////////////////////////////////////////////////////help.js/ end/////////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////THEME start/////////////////////////////////////////////////////////////////////////////////////
///切换主题
function changeTheme(themeName) {
    $.cookie.set('themes', themeName.id, 7);
    initTheme();
};
///初始化主题
function initTheme() {
    if (!isValidData($.cookie.get('themes'))) {
        $.cookie.set('themes', 'bootstrap', 7);
    }
    var themeName = $.cookie.get('themes');
    var $easyuiTheme = $('#themesId');
    var url = $easyuiTheme.attr('href');
    var href = url.substring(0, url.indexOf('themes')) + 'themes/' + themeName + '/easyui.css';
    $easyuiTheme.attr('href', href);
    var $iframe = $('iframe');
    if ($iframe.length > 0) {
        for (var i = 0; i < $iframe.length; i++) {
            var ifr = $iframe[i];
            $(ifr).contents().find('#themesId').attr('href', href);
        }
    }
}
////////////////////////////////////////////////////////THEME end/////////////////////////////////////////////////////////////////////////////////////


function convertEntityNameByColumnName(columnName) {
    if (isValidData(columnName)) {
        columnName = columnName.toLowerCase().replace(/'/g, "");
        var EntityNameItem = columnName.split("=")[0].split("_");
        for (var i = 0; i < EntityNameItem.length; i++) {
            var frist = EntityNameItem[i][0];
            var isline = i == 0 ? "" : "_";
            columnName = columnName.replace(isline + frist, frist.toUpperCase());
        }
    }
    return columnName;
}
///将URL中的数据库字段名称转换为对象属性名称格式
function replaceEntityFieldNameByTableFieldNameUrlfilter(urlFilter) {
    if (!isValidData(urlFilter)) return urlFilter;
    ///去掉'(单引号)和^(Shift + 6)
    var urlField = urlFilter.replace(/'/g, '').replace(/\^/g, '');
    ///通常参数提供的格式是 CODE_FID='xxx-xxxx-xxxxxxxxx-xxx'
    var fieldName = urlField.split('=')[0].split('_');
    var fieldValue = urlField.split('=')[1];
    var attributeName = '';
    for (var i = 0; i < fieldName.length; i++) {
        attributeName += fieldName[i].substring(0, 1).toUpperCase() + fieldName[i].substring(1).toLowerCase();
    }
    return attributeName + '=' + fieldValue;
}
///从地址栏拼接KEYS中获取属性名的集合
function getPattributeNameArray(formKeys) {
    var attributes = [];
    var keyFields = formKeys.replace(/'/g, '').replace(/\^/g, '').split('&');
    for (var i = 0; i < keyFields.length; i++) {
        var fieldName = keyFields[i].split('=')[0].split('_');
        var attributeName = '';
        for (var j = 0; j < fieldName.length; j++) {
            if (fieldName[j].length == 0) continue;
            attributeName += fieldName[j].substring(0, 1).toUpperCase() + fieldName[j].substring(1).toLowerCase();
        }
        if (!isValidData(attributeName)) continue;
        attributes.push(attributeName);
    }
    return attributes;
}

/////////////////////////////////////////////////////////////JSON start/////////////////////////////////////
///校验是否JSON格式数据
function isJsonFormat(str) {
    try {
        $.parseJSON(str);
    }
    catch (e) {
        return false;
    }
    return true;
}
///字符串转JSON对象
function toJson(str) {
    if (typeof str == 'object') return str;
    if (isJsonFormat(str))
        return eval('(' + str + ')');
    return eval('(' + str.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t') + ')');
}
/////////////////////////////////////////////////////////////JSON end//////////////////////////////////////

////将字符串转数组,过滤掉非法字符项,返回过滤后新拼接字符串
function splitRemoveEmpty(data, splitChar) {
    var spData = data.split(splitChar);
    var rspData = '';
    for (var i = 0; i < spData.length; i++) {
        if (isValidData(spData[i]))
            rspData += ',' + spData[i];
    }
    return rspData.substring(1);
}
////////////////////////////////////////////////////////////
///获取对象的KEY值
function getDataKey(tableKeyField, paramRowsData) {
    var dataKey = '';
    if (!isValidData(tableKeyField)) return null;
    var arrayKeyFields = tableKeyField.split("|");
    for (var i = 0; i < arrayKeyFields.length; i++) {
        dataKey += '|' + eval("paramRowsData." + arrayKeyFields[i]);
    }
    if (dataKey.length > 1) {
        dataKey = dataKey.substring(1);
    }
    return dataKey;
}
///获取从表过滤条件
function getDetailFilter(detailRelationKeyFieldName, formDataKey, formParamRowsData) {
    var gFilter = '';
    ///detail字段|main属性
    var relationFields = detailRelationKeyFieldName.split('|');
    if (relationFields.length == 1)
        return detailRelationKeyFieldName + "=^" + formDataKey + "^";
    if (!isValidData(formParamRowsData)) return '1=0';
    var relaFields = relationFields[0].split(',');
    var tionFields = relationFields[1].split(',');
    if (relaFields.length > tionFields.length) return '1=0';
    for (var i = 0; i < relaFields.length; i++) {
        var rField = relaFields[i];
        if (!isValidData(rField)) continue;
        var tField = tionFields[i];
        if (!isValidData(tField)) continue;
        var tValue = eval('formParamRowsData.' + tField);
        if (isValidData(tValue))
            gFilter += '|' + rField + "=^" + tValue + "^";
        else
            gFilter += '|1=0';
    }
    if (isValidData(gFilter)) return gFilter;
    return '1=0';
}

////////////////////////////////////////////////////////////
///根据实体结构清空编辑界面对应内容
function cleanControlValue(formCreateData, ctrlType, parentRowsData) {
    var linkSymbol = '-';
    if (isValidData(ctrlType)) linkSymbol = '-' + ctrlType + '-';
    for (var i = 0; i < formCreateData.length; i++) {
        if (formCreateData[i].Editable) {
            var controlName = '';
            var controlType = formCreateData[i].ControlType + '';
            switch (controlType) {
                case '20':
                case '80':
                case '100':
                    controlName = "combobox";
                    break;
                case '30':
                    controlName = "combotree";
                    break;
                case '40':
                    controlName = "combogrid";
                    break;
                case '50':
                    controlName = "datebox";
                    break;
                case '60':
                    controlName = 'datetimebox';
                    break;
                case '130':
                    controlName = 'timespinner';
                    break;
                default:
                    controlName = "textbox";
                    break;
            }
            ///只读
            var readonlyFlag = formCreateData[i].EditReadonly;
            switch (readonlyFlag) {
                case 10:
                case 20: eval("$('#' + formCreateData[i].ControlType + linkSymbol + formCreateData[i].FieldName)." + controlName + "('enable');"); break;
                case 30:
                case 40: eval("$('#' + formCreateData[i].ControlType + linkSymbol + formCreateData[i].FieldName)." + controlName + "('disable');"); break;
            }
            ///默认值
            var sqlDefaultValue = formCreateData[i].DefaultValue;
            ///当默认值的起始字符为#时，需要获取父行项中的属性
            if (isValidData(sqlDefaultValue) && (sqlDefaultValue + '').indexOf('#') > -1 && isValidData(parentRowsData)) {
                sqlDefaultValue = eval('parentRowsData.' + sqlDefaultValue.replace('#', ''));
            }
            var codeName = formCreateData[i].Extend1;
            if (isValidData(sqlDefaultValue)) {
                switch (controlType) {
                    case '10':
                    case '20':
                    case '40':
                    case '70':///numberbox
                    case '80':
                    case '90':
                    case '100':
                        sqlDefaultValue = getFormatterComboxValue(sqlDefaultValue, codeName);

                        ///isMultiple,多选暂不支持
                        break;
                    case '30':

                        break;
                    case '50':
                        if (sqlDefaultValue == 'now')
                            sqlDefaultValue = new Date().toLocaleDateString();
                        var dateformat = XR.language ? "MM/dd/yyyy" : "yyyy-MM-dd";
                        var configJson;
                        if (isValidData(codeName)) {
                            configJson = GetComboExtendJson(codeName);
                            if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                                dateformat = configJson.datetimeFormat;
                        }
                        sqlDefaultValue = FORM.formatDateByJson(sqlDefaultValue, dateformat);
                        break;
                    case '60':
                        if (sqlDefaultValue == 'now')
                            sqlDefaultValue = new Date().toLocaleTimeString();
                        var dateformat = XR.language ? "MM/dd/yyyy HH:mm:ss" : "yyyy-MM-dd HH:mm:ss";
                        ///JSON格式配置
                        var configJson;
                        var showSeconds;
                        if (isValidData(codeName)) {
                            configJson = GetComboExtendJson(codeName);
                            if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                                dateformat = configJson.datetimeFormat;
                            if (isValidData(configJson) && isValidData(configJson.showSeconds))
                                showSeconds = configJson.showSeconds == 'true' ? true : false;
                        }
                        ///
                        sqlDefaultValue = FORM.formatDateByJson(sqlDefaultValue, dateformat);
                        break;
                    case '130':
                        if (sqlDefaultValue == 'now')
                            sqlDefaultValue = new Date().toLocaleTimeString();
                        var dateformat = "HH:mm:ss";
                        ///JSON格式配置
                        var configJson;
                        var showSeconds;
                        if (isValidData(codeName)) {
                            configJson = GetComboExtendJson(codeName);
                            if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                                dateformat = configJson.datetimeFormat;
                            if (isValidData(configJson) && isValidData(configJson.showSeconds))
                                showSeconds = configJson.showSeconds == 'true' ? true : false;
                        }
                        ///
                        sqlDefaultValue = FORM.formatDateByJson(sqlDefaultValue, dateformat);
                        break;
                }
                eval("$('#' + formCreateData[i].ControlType + linkSymbol + formCreateData[i].FieldName)." + controlName + "('setValue', '" + sqlDefaultValue + "');");
            }
            else {
                eval("$('#' + formCreateData[i].ControlType + linkSymbol + formCreateData[i].FieldName)." + controlName + "('setValue', '');");
            }
        }
    }
}

var pageLoadParams;
///LIST.PAGE键值对
var arrTempMap = new Map();
var titleAlert = languageMessageTitle('1x00000001');///提示
var titleAdd = languageMessageTitle('1x00000003');///添加
var titleEdit = languageMessageTitle('1x00000002');///编辑
///页面功能标题
var pageTitle = '';

$(function () {
    $(window).resize(function () {
        $('#tblGrid').datagrid('resize', {
            width: setGridWidth(),
            height: setGridHeight()
        });
    });
    pageTitle = parent.$(".tabs-selected")[0].getElementsByTagName("span")[0].innerHTML;
    ///2018-5-9TITLE增加功能标题
    if (isValidData(pageTitle)) {
        titleEdit = pageTitle + titleEdit;
        titleAdd = pageTitle + titleAdd;
    }
    pageLoadParams = new PageEntity();
    expandPage(pageLoadParams, pageTitle);
    arrTempMap.put(pageLoadParams.entityName, pageLoadParams);
    loadEntityJS(pageLoadParams.entityName, function () { loadEntityJS("Entrance", function () { }) });
});
///将扩展JS文件加载到PAGE.HEAD中
function loadEntityJS(entityName, callback) {
    var head = document.getElementsByTagName("head")[0];
    var script = document.createElement("script");
    var url = XR.getPathLevel() + "/JS/common/" + entityName + ".js";
    if (entityName != "Entrance") {
        url = XR.getPathLevel() + '/' + pageLoadParams.bllProjectName + "/JS/" + entityName + ".js";
    }
    script.type = "text/javascript";
    script.id = "expandJs";
    if (script.readyState) {
        script.onreadystatechange = function () {
            if (script.readyState == "loaded" || script.readyState == "complete") {
                script.onreadystatechange = null; callback();
            }
        }
    }
    else {
        script.onload = function () {
            callback();
        }
        script.onerror = function () {
            callback();
        }
    }
    script.src = url;
    head.appendChild(script);
};
///ACTION-add()
function add(menuName) {
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    pageLoadParams.operationType = "insert-" + pageLoadParams.tableName;
    pageLoadParams.entityMethodName = "createForm";

    if (isValidData(menuName)) {
        pageLoadParams.AjaxActionOrEntityData('formopen', 'openAddForm();', menuName);
    }
    else {
        openAddForm();
    }
}
function openAddForm() {
    ///清除选中项
    ///$('#' + tblGridId).datagrid("clearSelections");
    pageLoadParams.formDataKey = null;
    pageLoadParams.formUpdateData = null;
    pageLoadParams.formParamRowsData = null;
    var width = pageLoadParams.editFormWidth;
    var height = pageLoadParams.editFormHeight;
    if (width == null) width = 0;
    if (height == null) height = 0;
    if (!isValidData(pageLoadParams.formUrl) || pageLoadParams.formUrl == 'CommonEdit.aspx')
        pageLoadParams.formUrl = 'CommonEdit.aspx?' + pageLoadParams.entityName + "&" + pageLoadParams.entityMethodName;
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    tAlert('pageIframe', titleAdd, pageLoadParams.formUrl, width, height);
}

///弹出导入窗体
function openImportAlert() {
    HELP.tAlert('pageIframe'
        , languageMessageTitle('1x00000018')///导入
        , 'CommonEdit.aspx?' + pageLoadParams.entityName + "&importForm"
        , 800, 220);
}
///
function editrows(menuName) {
    pageLoadParams.operationType = "update-" + pageLoadParams.tableName;
    var data = languageMessageData('0x00000070');///不能对子项进行修改操作！
    if (tblGridId != "tblGrid") return tAlert("error", titleAlert, data);
    if (!loadSelection()) return;
    ///如果有指定的菜单名称且与主菜单名称不一样，则需要重新加载菜单、按钮、窗体属性、链接地址等
    ///利用这个特性，对于同一页面有多个editrow调用时必须都设置参数，否则来回切换不同对象则无法实现
    if (isValidData(menuName)) {
        pageLoadParams.AjaxActionOrEntityData('formopen', 'endEditRows();', menuName);
    }
    else {
        endEditRows();
    }
}
///
function endEditRows() {
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=select-' + pageLoadParams.tableName
        + '&key=' + pageLoadParams.formDataKey
        + "&keylength=" + pageLoadParams.formDataKeyLength
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + pageLoadParams.entityName
        , 'js:getformData');
}

function edit() {
    editrows();
}


function confirmDelete() {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl(), 'method=' + pageLoadParams.operationType + '&key=' + pageLoadParams.formDataKey + "&AN=BLL." + pageLoadParams.bllProjectName + "&ENTITY_NAME=" + pageLoadParams.entityName + "&keylength=" + pageLoadParams.formDataKeyLength, 'js:delResult');
}
function del() {
    pageLoadParams.operationType = "delete-" + pageLoadParams.tableName;
    var data = languageMessageData('0x00000071');//不能对子项进行删除操作！
    if (tblGridId != "tblGrid") return tAlert("error", titleAlert, data);
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        var entityInfo = pageLoadParams.formEntityInfo;
        if (entityInfo != null && entityInfo != undefined && entityInfo != "") {
            var defaultEntityType = true;
            if (entityInfo.EntityType == "3") {

                var parentFieldList = entityInfo.ParentField.split("-");
                if (parentFieldList.length > 2) {

                    var subFilter = "AND " + parentFieldList[1] + "='" + eval("row." + parentFieldList[0]) + "'";

                    var dataParams = "method=ajaxTables&ENTITY_NAME=" + parentFieldList[2] + "&FILTER=" + subFilter + "&AN=BLL." + pageLoadParams.bllProjectName;
                    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:subConfirmDelete', false);
                }
            }
            else if (entityInfo.EntityType == "2") {
                if ('children' in row) {
                    var data = languageMessageData('0x00000074');//该项存在子项不能删除！
                    tAlert("error", titleAlert, data);
                } else {
                    pageLoadParams.formDataKey = row.Id;
                    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
                    var data = languageMessageData('0x00000075');//是否确认删除！
                    tAlert("confirm", titleAlert, data, "confirmDelete()", "");
                }
            } else {
                pageLoadParams.formDataKey = '';
                if (isValidData(pageLoadParams.tableKeyField)) {
                    var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
                    for (var i = 0; i < arrayKeyFields.length; i++) {
                        pageLoadParams.formDataKey += '|' + eval("row." + arrayKeyFields[i]);
                    }
                }
                else {
                    if (!isValidData(pageLoadParams.formDataKey)) {
                        pageLoadParams.formDataKey = row.Id;
                        pageLoadParams.formDataKeyLength = '64';
                    }
                    if (!isValidData(pageLoadParams.formDataKey)) {
                        pageLoadParams.formDataKey = row.Nid;
                        pageLoadParams.formDataKeyLength = '32';
                    }
                }
                parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
                var data = languageMessageData('0x00000075');//是否确认删除?
                tAlert("confirm", titleAlert, data, "confirmDelete()", "");
            }
        } else {
            var data = languageMessageData('0x00000073');//该项存在子项不能删除！
            tAlert("error", titleAlert, data);
        }
    } else {
        var data = languageMessageData('0x00000053');//请选择需删除的行数据
        tAlert("error", titleAlert, data);
    }
}
///////////////////////////////////////////////////////////////////////////////////////////BEGIN.SET_STATUS//////////////////////////////////////////////////////////////////
///更新数据状态
///2018-6-28新增状态提交对象
function setStatusByEntity(actionname, statusEntityName) {
    ///对象名称未传值时与普通setStatus一样处理
    if (!isValidData(statusEntityName)) {
        setStatus(actionname);
        return;
    }
    var dataParam = 'method=getSetStatusEntityFIeldsInfo&ENTITY_NAME=' + statusEntityName + '&actionName=' + actionname + '&AN=SYS';
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:resultGetEntityFields');
}
///获取到窗体数据后
function resultGetEntityFields(data) {
    data = eval('(' + data + ')');
    ///验证登录信息是否有效
    sessionIsNull(data);
    if (isSessionNull) return;
    if (!loadCheckedRows(false)) {
        if (!loadSelection()) return;
    }
    ///MAIN.aspx中的对话框载体
    var ds = $('#dialogSetStatus');
    ///
    var divSetForm = document.getElementById("divSetForm");
    var divForm = document.createElement("div");
    divForm.id = 'dialogSetStatusForm';
    divForm.className = "easyui-panel panel-body panel-body-noheader";
    divForm.style.width = "100%";
    divForm.style.marginTop = "2px";
    divForm.style.marginBottom = "2px";
    divForm.style.paddingTop = "2px";
    divForm.style.paddingBottom = "2px";
    divForm.style.overflow = 'hidden';
    divForm.style.borderColor = 'black';
    divSetForm.appendChild(divForm);
    ///选中行数据对象
    var row = $("#" + tblGridId).datagrid('getSelected');
    ///创建控件
    createControlToPage('dialogSetStatusForm', data.entityfieldform, null, row, null, 'T', 1);
    ///2H140
    var dHeight = 84 + data.entityfieldform.length * 28;
    var tlYes = languageMessageData('1x00000046');///确定
    var tlNo = languageMessageData('1x00000048');///取消
    ds.dialog({
        title: data.message,
        width: 394,
        height: dHeight,
        closed: false,
        cache: false,
        modal: true,
        content: divForm,
        buttons: [{
            text: tlYes, iconCls: 'icon-back',
            handler: function () {
                var dataPar = defaultSaveContent(data.entityfieldform, 'T');
                tAlert('confirm', titleAlert, data.message, 'confirmSetStatusEntity("' + data.entityinfo.EntityName + '","' + dataPar + '","' + data.actionname + '")', '');
                $('#divSetForm').html('');
                $('#dialogSetStatus').html('');
                $('#dialogSetStatus').dialog('close');
            }
        },
        {
            text: tlNo, iconCls: 'icon-no', handler: function () {
                $('#divSetForm').html('');
                $('#dialogSetStatus').html('');
                $('#dialogSetStatus').dialog('close');
            }
        }]
    });
}
function setStatus(actionname) {
    endEditing();
    var dataParam = 'method=ajaxStatusTypeConfirmMessage&actionname=' + actionname + '&ENTITY_NAME=CodeItem&AN=BLL.SYS';
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:setStatusMessageResult');
}
///加载选中行
function loadSelection() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (!isValidData(row)) {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    ///清空FORM选中项，重新由GRID进行获取
    ///TODO:其实本人觉得这种方式并不优雅，后续需要改进，在选中时就构建formDataKey更为通用
    pageLoadParams.formDataKey = '';
    ///联合主键方式
    if (isValidData(pageLoadParams.tableKeyField)) {
        var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
        for (var j = 0; j < arrayKeyFields.length; j++) {
            var keyFValue = eval("row." + arrayKeyFields[j]);
            if (!isValidData(keyFValue)) continue;
            pageLoadParams.formDataKey += '|' + keyFValue;
        }
    }
    else {
        ///如果未设置主键则先尝试Id属性，因为在数据库表设计时已控制了ID字段为自增长主键
        ///所以程序多半会走这个逻辑
        if (isValidData(row.Id))
            pageLoadParams.formDataKey += row.Id;
        ///ID字段在数据库设计时就应为bigint
        pageLoadParams.formDataKeyLength = '64';
        ///部分公司数据库表的主键被设置为NID
        if (!isValidData(pageLoadParams.formDataKey)) {
            if (isValidData(row.Nid))
                pageLoadParams.formDataKey += row.Nid;
            ///NID字段在数据库设计时就应为int
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    if (!isValidData(pageLoadParams.formDataKey)) {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    return true;
}
///加载选中行
function loadCheckedRows(msgFlag) {
    var rows = $("#" + tblGridId).datagrid('getChecked');
    if (!isValidData(rows) || rows.length == 0) {
        if (msgFlag == true)
            tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    ///清空FORM选中项，重新由GRID进行获取
    ///TODO:其实本人觉得这种方式并不优雅，后续需要改进，在选中时就构建formDataKey更为通用
    pageLoadParams.formDataKey = '';
    ///联合主键方式
    if (isValidData(pageLoadParams.tableKeyField)) {
        var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
        for (var i = 0; i < rows.length; i++) {
            pageLoadParams.formDataKey += '-';
            for (var j = 0; j < arrayKeyFields.length; j++) {
                var keyFValue = eval("rows[i]." + arrayKeyFields[j]);
                if (!isValidData(keyFValue)) continue;
                pageLoadParams.formDataKey += '|' + keyFValue;
            }
        }
    }
    else {
        ///如果未设置主键则先尝试Id属性，因为在数据库表设计时已控制了ID字段为自增长主键
        ///所以程序多半会走这个逻辑
        for (var i = 0; i < rows.length; i++) {
            if (isValidData(rows[i].Id))
                pageLoadParams.formDataKey += '-' + rows[i].Id;
        }
        ///ID字段在数据库设计时就应为bigint
        pageLoadParams.formDataKeyLength = '64';
        ///部分公司数据库表的主键被设置为NID
        if (!isValidData(pageLoadParams.formDataKey)) {
            for (var i = 0; i < rows.length; i++) {
                if (isValidData(rows[i].Nid))
                    pageLoadParams.formDataKey += '-' + rows[i].Nid;
            }
            ///NID字段在数据库设计时就应为int
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    if (!isValidData(pageLoadParams.formDataKey) || pageLoadParams.formDataKey == '-') {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    return true;
}
///获取需要更新状态的选定数据行
function setStatusMessageResult(data) {
    data = eval('(' + data + ')');
    pageLoadParams.operationType = "status-" + pageLoadParams.tableName + "-" + data.actionname;
    ///TODO:这个验证是否考虑去除，之后的代码用的是变量，此处的校验就没有什么意义了
    if (tblGridId != "tblGrid")
        return tAlert("error", titleAlert, languageMessageData('0x00000185'));///不能对子项进行删除操作！
    if (!loadCheckedRows(false)) {
        if (!loadSelection()) return;
    }
    tAlert('confirm', titleAlert, data.message, 'confirmSetStatus()', '');
}
///更新数据状态请求提交
function confirmSetStatus() {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=' + pageLoadParams.operationType
        + '&key=' + pageLoadParams.formDataKey
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + pageLoadParams.entityName
        + "&keylength=" + pageLoadParams.formDataKeyLength
        , 'js:setStatusResult');
}
///
function confirmSetStatusEntity(statusEntityName, fields, actionName) {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , fields + 'method=setStatusEntity'
        + '&key=' + pageLoadParams.formDataKey
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + statusEntityName
        + "&keylength=" + pageLoadParams.formDataKeyLength
        + '&actionName=' + actionName
        , 'js:setStatusResult');
}
///更新数据状态执行完成
function setStatusResult(data) {
    alertMessage(data);
    editIndex = undefined;
    search();
}
///////////////////////////////////////////////////////////////////////////////////////////END.SET_STATUS//////////////////////////////////////////////////////////////////

function subConfirmDelete(data) {
    data = eval('(' + data + ')');
    if (data != null && data != undefined && data != "") {

        if (data.total == 0) {
            var row = $("#" + tblGridId).datagrid('getSelected');
            pageLoadParams.formDataKey = row.Id;
            parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
            var data = languageMessageData('0x00000075');//是否确认删除？
            tAlert("confirm", titleAlert, data, "confirmDelete()", "");
        } else {
            var data = languageMessageData('0x00000073');//该项存在子项不能删除！
            tAlert("error", titleAlert, data);
        }
    }
}

function delResult(data) {
    //closeMessagerProgress();
    if (pageLoadParams.formEntityInfo != null && pageLoadParams.formEntityInfo != undefined && pageLoadParams.formEntityInfo != "") {

        switch (pageLoadParams.formEntityInfo.EntityType + "") {
            case "2":
                $('#' + tblGridId).treegrid('reload');
                break;
            default:
                $('#' + tblGridId).datagrid('reload');
                break;
        }
    }
    alertMessage(data);
}
///
function search() {
    if (endEditing()) return;
    var isPublicCreate = pageLoadParams.formEntityInfo.EntityType;
    if (isPublicCreate + "" == "2") {
        $('#' + tblGridId).treegrid('load', {
            FILTER: getSearchFormData()
        });
    }
    else if (isPublicCreate + "" == "3") {
        $('#' + tblGridId).datagrid('load', {
            SECOND_FILTER: getSearchFormData()
        });
    }
    else {
        $('#' + tblGridId).datagrid('load', {
            FILTER: getSearchFormData()
        });
    }
}

//MAP 存从表的 EntityName ， 用于取从表的创建表单数据
function clearnRowByGrid(gridId, row, index) {
    var entity = window.location.search.split("?");
    if (entity.length >= 2) {
        var entityKey = "gridClearRows_" + entity[1];
        var nowGridId = parent.arrGlobal.get(entityKey);
        if (nowGridId != null && nowGridId != undefined && nowGridId != "" && gridId != nowGridId) {
            $('#' + nowGridId).datagrid("clearSelections");
        }
        parent.arrGlobal.put(entityKey, gridId);
    }
}
///默认入口方法add实体名();
function publicInit(entityName, tableName) {
    ///校验参数有效性
    if (!isValidData(entityName)) return;
    ///获取选中项
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (isValidData(row)) {
        ///先根据配置获取配置相关的KEY值
        if (isValidData(pageLoadParams.tableKeyField)) {
            var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
            pageLoadParams.formDataKey = '';
            for (var i = 0; i < arrayKeyFields.length; i++) {
                pageLoadParams.formDataKey += '|' + eval("row." + arrayKeyFields[i]);
            }
            if (pageLoadParams.formDataKey.length > 1) {
                pageLoadParams.formDataKey = pageLoadParams.formDataKey.substring(1);
            }
        }
        ///如果未获取到KEY值首先认为是ID（long）
        if (!isValidData(pageLoadParams.formDataKey)) {
            pageLoadParams.formDataKey = row.Id;
            pageLoadParams.formDataKeyLength = '64';
        }
        ///其次认为是NID（int）
        if (!isValidData(pageLoadParams.formDataKey)) {
            pageLoadParams.formDataKey = row.Nid;
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    ///
    var isPublicCreate = entityName.substring(entityName.length - 1) == "_" ? true : false;
    entityName = isPublicCreate ? entityName.substring(0, entityName.length - 1) : entityName;
    var defaultMedthodName = "create" + entityName;
    ///页面参数对象
    var entityInfo = arrTempMap.get(entityName);
    ///
    if (!isValidData(entityInfo)) entityInfo = new PageEntity();
    entityInfo.entityName = entityName;
    if (!isValidData(entityInfo.tableName))
        entityInfo.tableName = tableName;
    entityInfo.parentEntityName = pageLoadParams.entityName;

    ///
    entityInfo.AjaxActionOrEntityData('all');
    ///2018-4-12新增FORM编辑项
    entityInfo.formParamRowsData = pageLoadParams.formParamRowsData;
    entityInfo.formDataKey = pageLoadParams.formDataKey;

    arrTempMap.put(entityName, entityInfo);
    parent.arrGlobal.put(entityName, entityInfo);
    if (isPublicCreate) {
        defaultMedthodName = "publicCreate";
        ///如果没有选中项的KEY值，则返回
        if (!isValidData(pageLoadParams.formDataKey)) {
            var msg = languageMessageData('0x00000046');
            var title = languageMessageTitle('1x00000001');
            tAlert("warning", title, msg);
            return;
        }
        ///主从结构DETAIL的GRID
        var pEntityName = arrTempMap.get(gridChoiceId);
        var pEntity = arrTempMap.get(pEntityName);
        if (isValidData(pEntity)) {
            ///主从or属性结构
            if (isValidData(pEntity.formEntityInfo.ParentField)) {
                var eParentField = pEntity.formEntityInfo.ParentField;
                var cParentFields = eParentField.split(",");
                for (var i = 0; i < cParentFields.length; i++) {
                    var cEntityParentFields = cParentFields[i].split("-");
                    if (cEntityParentFields.length <= 3) break;
                    var cEntityName = cEntityParentFields[2];
                    if (entityName == cEntityName) {
                        var cTableName = cEntityParentFields[3];
                        tAlert('pageIframe'
                            , titleEdit
                            , 'CommonEdit.aspx?' + entityName + "&" + defaultMedthodName + '&' + cTableName + '&&&&&&' + pageLoadParams.bllProjectName
                            , entityInfo.editFormWidth
                            , entityInfo.editFormHeight);
                    }
                    else {
                        ///请选择对应的父项再点添加
                        var msg = languageMessageData('0x00000076');
                        tAlert("error", titleAlert, msg, "", "");
                        return;
                    }
                }
            }
            else {
                ///请确认数据库是否配置子项对象
                var msg = languageMessageData('0x00000077');
                tAlert("error", titleAlert, msg, "", "");
                return;
            }
        }

        ///如果LIST.GRID不是DETAIL
        else {
            tAlert('pageIframe'
                , titleEdit
                , 'CommonEdit.aspx?' + entityName + '&createForm'
                , entityInfo.editFormWidth
                , entityInfo.editFormHeight);
        }
    }
    else {
        ///普通编辑页面需要有KEY值
        if (!isValidData(pageLoadParams.formDataKey)) {
            if (pageLoadParams.operationType == "insert") {
                tAlert('pageIframe'
                    , titleEdit
                    , 'CommonEdit.aspx?' + entityName + "&" + defaultMedthodName
                    , entityInfo.editFormWidth
                    , entityInfo.editFormHeight);
            }
            else {
                ///请选中行数据！
                var msg = languageMessageData('0x00000053');
                tAlert("error", titleAlert, msg);
                return;
            }
        }
        if (isValidData(pageLoadParams.formEntityInfo)) {
            var tempParentField = pageLoadParams.formEntityInfo.ParentField;
            if (isValidData(tempParentField)) {
                var tempItems = tempParentField.split('-');
                if (tempItems.length > 1) {
                    entityInfo.formDataKey = eval("row." + tempItems[0]);
                    entityInfo.formParamKey = tempItems[1];
                    entityInfo.formParamRowsData = row;
                }
            }
        }
        ///不是默认方法直接调用弹窗
        tAlert('pageIframe'
            , titleEdit
            , 'CommonEdit.aspx?' + entityName + "&" + defaultMedthodName + '&' + tableName
            , entityInfo.editFormWidth
            , entityInfo.editFormHeight);
    }
}
///导出EXCEL
function exportExcel() {
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    excelExport(pageLoadParams);
}
///下载模板
function downloadTemplate(entityName) {
    if (isValidData(entityName))
        pageLoadParams = parent.arrGlobal.get(entityName);
    else
        pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    if (!isValidData(pageLoadParams)) return;
    templateDownload(pageLoadParams);
}
///
///下载模板
function downloadTemplateFile() {
    if (isValidData(entityName))
        pageLoadParams = parent.arrGlobal.get(entityName);
    else
        pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    var filesPath = 'TEMPLATE\\\\IMPORT\\\\' + pageLoadParams.entityName + '.xls'
        + '|TEMPLATE\\\\IMPORT\\\\' + pageLoadParams.entityName + '.xlsx';
    var dataParams = "method=ajaxDownLoadFiles"
        + '&AN=' + pageLoadParams.bllProjectName
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + '&FILE_PATH=' + filesPath;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultDownloadTemplate");
}
///

///
function imagePreview() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (!isValidData(row)) {
        closeMessagerProgress();
        var alertMsg = languageMessageData('0x00000733');///请选择需要预览的图片
        tAlert('error', titleAlert, alertMsg); return;
    }
    ///先根据配置获取配置相关的KEY值
    if (isValidData(pageLoadParams.tableKeyField)) {
        var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
        pageLoadParams.formDataKey = '';
        for (var i = 0; i < arrayKeyFields.length; i++) {
            pageLoadParams.formDataKey += '|' + eval("row." + arrayKeyFields[i]);
        }
        if (pageLoadParams.formDataKey.length > 1) {
            pageLoadParams.formDataKey = pageLoadParams.formDataKey.substring(1);
        }
    }
    ///如果未获取到KEY值首先认为是ID（long）
    if (!isValidData(pageLoadParams.formDataKey)) {
        pageLoadParams.formDataKey = row.Id;
        pageLoadParams.formDataKeyLength = '64';
    }
    ///其次认为是NID（int）
    if (!isValidData(pageLoadParams.formDataKey)) {
        pageLoadParams.formDataKey = row.Nid;
        pageLoadParams.formDataKeyLength = '32';
    }
    var par = 'AN=BLL.' + pageLoadParams.bllProjectName + '&ENTITY_NAME=' + pageLoadParams.entityName + '&key=' + pageLoadParams.formDataKey;
    HELP.ajaxCommon(XR.defaultProcessUrl() + "method=ajaxImageToHtml", par, "js:imagePreviewResult");
}
///
function imagePreviewResult(data) {
    window.open(data);
}
//////////////////////////////////////PRINT//////////////////////////////////////////
///打印文件生成
function printFiles(printConfigCode, dataMethod, updateMethod) {
    if (!loadCheckedRows()) return;
    if (!isValidData(dataMethod))
        dataMethod = '';
    var dataParam = 'AN=BLL.' + pageLoadParams.bllProjectName
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + '&key=' + pageLoadParams.formDataKey
        + "&keylength=" + pageLoadParams.formDataKeyLength
        + '&DATA_METHOD=' + dataMethod
        + '&UPDATE_METHOD=' + updateMethod
        + '&PRINT_CONFIG_CODE=' + printConfigCode;
    HELP.ajaxCommon(XR.defaultProcessUrl() + "method=ajaxPrintFiles", dataParam, "js:printFilesResult");
}
///打印文件生成后
function printFilesResult(data) {
    alertMessage(data);
    if (!isValidData(data) || !isJsonFormat(data)) return;
    data = eval('(' + data + ')');
    if (data.files.length == 0) return;
    for (var i = 0; i < data.files.length; i++) {
        window.open(data.files[i]);
    }
}
//////////////////////////////////////////////////////////////////////////////////////
function listSave() {
    var methodName = 'insert-' + pageLoadParams.tableName;
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
            ajaxCommon(processUrl, dataPar, "js:listSaveResult");
        }
    } else {
        if (dataPar != undefined && dataPar.indexOf("error_") != -1) {
            dataPar = dataPar.replace("error_", "");
        }
    }
}
///
function listSaveResult(data) {
    ///操作失败会返回false
    if (alertMessage(data)) {
        cleanControlValue(pageLoadParams.formCreateData, undefined, pageLoadParams.formParamRowsData);
    }
    ///有明细的情况下刷新列表
    search();
}

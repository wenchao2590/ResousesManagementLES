
function setUserRole() {
    var entityName = "UserRole";
    var tableName = 'TS_SYS_USER_ROLE'
    publicInit(entityName, tableName);
}

///TREE_GRID添加子节点
function treeSub() {
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    pageLoadParams.entityMethodName = "createTreeSubForm";
    var row = $("#" + tblGridId).datagrid('getSelected');
    var titleName = '';
    if (isValidData(row)) {
        ///页面操作方式
        var operationType = "select-" + pageLoadParams.tableName;
        var subFilter = '';
        var parentFieldValue = '';
        var parentFieldList = [];
        ///获取数据模型配置
        if (isValidData(pageLoadParams.formEntityInfo.ParentField)) {
            parentFieldList = pageLoadParams.formEntityInfo.ParentField.split("-");
        }
        if (parentFieldList.length > 2) {
            ///父ID字段
            var pidTableField = convertColumnByFieldName(parentFieldList[1]);
            parentFieldValue = eval("row." + parentFieldList[0]);
            titleName = eval("row." + parentFieldList[2]);
            subFilter = pidTableField + "='" + parentFieldValue + "'";
        }
        ///窗体数据主键值、窗体数据条件SQL语句、窗体数据对象
        pageLoadParams.formDataKey = parentFieldValue;
        pageLoadParams.formParamKey = subFilter;
        pageLoadParams.formParamRowsData = row;
        ///formUpdateData作为修改的对象，需要清空
        pageLoadParams.formUpdateData = '';
        ///
        parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    }
    else {
        pageLoadParams.formDataKey = '';
        pageLoadParams.formParamKey = '';
        pageLoadParams.formParamRowsData = null;
        pageLoadParams.formUpdateData = '';
        parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
        var data = languageMessageData('0x00000048');//请选择需要添加子项的行数据
        var title = languageMessageTitle('1x00000001');//提示
        tAlert("warning", title, data);
        return;
    }
    HELP.tAlert('pageIframe'
        , '[' + titleName + ']' + languageMessageTitle('1x00000017')
        , 'CommonEdit.aspx?' + pageLoadParams.entityName + '&' + pageLoadParams.entityMethodName
        , pageLoadParams.editFormWidth
        , pageLoadParams.editFormHeight);
}


function subAdd() {
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);

    pageLoadParams.entityMethodName = "createSubForm"; //createForm

    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        var operationType = "select-" + pageLoadParams.tableName;

        var subFilter = "";
        var parentFieldValue = "";
        var parentFieldList;
        if (pageLoadParams.formEntityInfo.ParentField != undefined) {
            parentFieldList = pageLoadParams.formEntityInfo.ParentField.split("-");
        }
        if (parentFieldList.length > 1) {
            parentFieldValue = eval("row." + parentFieldList[0]);
            subFilter = "AND " + parentFieldList[1] + "='" + parentFieldValue + "'";
        }

        pageLoadParams.formDataKey = parentFieldValue;
        pageLoadParams.formParamKey = subFilter;
        pageLoadParams.formUpdateData = "";
        parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);

    } else {
        pageLoadParams.formDataKey = "";
        pageLoadParams.formParamKey = "";
        pageLoadParams.formUpdateData = "";
        parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
        var data = languageMessageData('0x00000048');//请选择一个父节点
        var title = languageMessageTitle('1x00000001');//提示
        tAlert("warning", title, data);
        return;
    }

    HELP.tAlert('pageIframe', languageMessageTitle('1x00000003'), 'CommonEdit.aspx?' + pageLoadParams.entityName + "&" + pageLoadParams.entityMethodName, 800, 480);
}
///菜单管理->设置动作
function setMenuAction(data) {
    var Name=data.split('&');
    if (Name < 2)
        var data = languageMessageData('1x00000031')///系统配置有误
    var entityName = Name[0];
    var tableName = Name[1];
    var title = languageMessageTitle('1x00000001');///提示
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row == null) {
        var data = languageMessageData('0x00000050');///请选择需要设置动作的行数据
        tAlert("error", title, data);
        return;
    }
    if (row.MenuType == 40 || row.MenuType == 50) {
        var data = languageMessageData('0x00000049');///该项为系统模块项不能设置动作！
        tAlert("error", title, data);
        return;
    }
    publicInit(entityName, tableName);
}

function getformData(data) {
    pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);

    var width = pageLoadParams.editFormWidth;
    var height = pageLoadParams.editFormHeight;
    if (width == null) width = 0;
    if (height == null) height = 0;
    pageLoadParams.formUpdateData = data;
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    var title = languageMessageTitle('1x00000002');//修改
    if (!isValidData(pageLoadParams.formUrl) || pageLoadParams.formUrl == 'CommonEdit.aspx')
        pageLoadParams.formUrl = 'CommonEdit.aspx?' + pageLoadParams.entityName + "&" + pageLoadParams.entityMethodName;
    HELP.tAlert('pageIframe', title, pageLoadParams.formUrl, width, height);
}

function getMenuActionForm(entityName, data, methodName) {

    pageLoadParams.formActionEditData = data.actionform;
    pageLoadParams.formActionEditData = data.searchform;
    pageLoadParams.formActionEditData = data.entityfieldform;
    pageLoadParams.formActionEditData = data.entityinfo;
    pageLoadParams.entityMethodName = methodName;
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);

    HELP.tAlert('pageIframe', languageMessageTitle('1x00000002'), 'CommonEdit.aspx?' + pageLoadParams.entityName + "&" + methodName, 800, 480);
}

function resetPassWord() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    var data = languageMessageData('1x00000001');//提示
    if (row != null) {
        var title = languageMessageTitle('0x00000051');//是否确认重置密码？
        tAlert("confirm", data, title, "setDefaultPassWord()", "");
    } else {
        var title = languageMessageTitle('0x00000052');//请选择重置密码的行数据!
        tAlert("warning", data, title);
    }
}

function setDefaultPassWord() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        openMessagerProgress();
        var dataParam = "method=setDefaultPassWord&key=" + row.Id + "&ENTITY_NAME=User&AN=BLL.SYS";
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:SaveExpandResult');
    }
}

function setPermission() {
    publicInit('RoleAuth', 'TS_SYS_ROLE_AUTH');
}

function SaveExpandResult(par) {
    //closeMessagerProgress();
    alertMessage(par);

    try {
        $("#" + tblGridId).datagrid('reload');
    } catch (e) {

    }
}
function startService() {
    var entityName = "ProcessSchedule";
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        openMessagerProgress();
        var operationType = "update-" + pageLoadParams.tableName;
        var dataPar = "LastRunStatus=10";
        var dataParam = dataPar + "&method=" + operationType + "&key=" + row.Id + "&ENTITY_NAME=ProcessSchedule&AN=BLL.BAS";
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:SaveExpandResult');
    }
    else {
        var data = languageMessageData('0x00000053');//请选中行数据
        var title = languageMessageTitle('1x00000001');
        tAlert("error", title, data);
    }
}
function pauseService() {
    var entityName = "ProcessSchedule";
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        openMessagerProgress();
        var operationType = "update-" + pageLoadParams.tableName;
        var dataPar = "LastRunStatus=20";
        var dataParam = dataPar + "&method=" + operationType + "&key=" + row.Id + "&ENTITY_NAME=ProcessSchedule&AN=BLL.BAS";
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:SaveExpandResult');
    }
    else {
        var data = languageMessageData('0x00000053');//请选中行数据
        var title = languageMessageTitle('1x00000001');
        tAlert("error", title, data);
    }
}
/// List 绘制页面总入口
function expandPage(pageLoadParams, title) {
    switch (pageLoadParams.entityName) {
        case 'SupplyPlan': pageLoadParams.DefaultAjaxCreateFormAndGridData("action"); break;
        case 'LackOfInspection': pageLoadParams.DefaultAjaxCreateFormAndGridData("action"); break;
        default: pageLoadParams.DefaultAjaxCreateFormAndGridData("all"); break;
    }
}

function onClickRowByGrid(row) {
    pageLoadParams.formParamRowsData = row;
}



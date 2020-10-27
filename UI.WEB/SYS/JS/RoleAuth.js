
function createRoleAuth() {
    //加载LIST页面数据
    pageLoadParams.GetListData(oldpageLoadParams);
    //加载后台页面数据
    pageLoadParams.AjaxActionOrEntityData("All", "create" + pageLoadParams.entityName + "Init();");
}

//入口方法 
function createRoleAuthInit() {

    //页面初始化
    FORM.createInitForm(pageLoadParams.formActionEditData, pageLoadParams.formSearchEditData);
    $('#' + divSearchFormId).hide();
    $('#' + divControlFormId).hide();
    //创建按钮
    FORM.createActionData(divActionMenuId, pageLoadParams.formActionEditData);
    //创建GRID
    var gColumns = FORM.getFormatGridColumnsData(pageLoadParams.formCreateData, true, pageLoadParams.entityName);
    if (isValidData(gColumns)) {
        var gUrl = XR.defaultProcessUrl() + 'method=ajaxRoleAuthDataByFid'
            + '&key=' + pageLoadParams.formDataKey
            + '&ENTITY_NAME=' + pageLoadParams.entityName
            + '&AN=BLL.' + pageLoadParams.bllProjectName;
        FORM.createGrid(pageLoadParams.entityName, 'Fid', '', '', gColumns, gUrl);
    }
}

function setRoleAuth() {
    var selectRowData = $('#' + tblGridId).treegrid('getSelections');
    if (selectRowData != null && selectRowData.length > 0) {
        var selectRowIdItem = "";
        for (var i = 0; i < selectRowData.length; i++) {

            selectRowIdItem += selectRowData[i].AuthSourceFid + ",";
        }
        if (selectRowIdItem.length > 0) {
            openMessagerProgress();
            selectRowIdItem = selectRowIdItem.substring(0, selectRowIdItem.length - 1);
            var parentDataKey = pageLoadParams.formDataKey;
            var dataParam = 'method=ajaxSetRoleAuth&IS_SET=true&key=' + parentDataKey + '&SELECT_FIDITEM=' + selectRowIdItem + "&ENTITY_NAME=" + pageLoadParams.entityName + "&AN=BLL." + pageLoadParams.bllProjectName;
            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveRoleAuthResult', false);
        }
    }
}

function cancelRoleAuth() {
    var selectRowData = $('#' + tblGridId).treegrid('getSelections');
    if (selectRowData != null && selectRowData.length > 0) {
        var selectRowIdItem = "";
        for (var i = 0; i < selectRowData.length; i++) {

            selectRowIdItem += selectRowData[i].AuthSourceFid + ",";
        }
        if (selectRowIdItem.length > 0) {
            openMessagerProgress();
            selectRowIdItem = selectRowIdItem.substring(0, selectRowIdItem.length - 1);
            var parentDataKey = pageLoadParams.formDataKey;
            var dataParam = 'method=ajaxSetRoleAuth&IS_SET=false&key=' + parentDataKey + '&SELECT_FIDITEM=' + selectRowIdItem + "&ENTITY_NAME=" + pageLoadParams.entityName + "&AN=BLL." + pageLoadParams.bllProjectName;
            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveRoleAuthResult', false);
        }
    }
}

function saveRoleAuthResult(result) {
    $("#" + tblGridId).treegrid('reload');
    $('#' + tblGridId).treegrid('unselectAll');
    alertMessage(result);
}
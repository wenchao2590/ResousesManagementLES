///入口方法
function createMenuAction() {
    ///继承OLD，OLD来自于MAIN全部变量
    pageLoadParams.GetListData(oldpageLoadParams);
    //加载后台页面数据
    pageLoadParams.AjaxActionOrEntityData("menuaction", "create" + pageLoadParams.entityName + "Init();");
}
//页面加载 
function createMenuActionInit() {
    //页面初始化
    FORM.createInitForm(pageLoadParams.formActionEditData, pageLoadParams.formSearchEditData);
    //创建按钮
    FORM.createActionData(divActionMenuId, pageLoadParams.formActionEditData);
    //创建搜索控件
    FORM.createSearchControlData(divSearchFormId, pageLoadParams.formSearchEditData, pageLoadParams.formSearchColumnLength);
    //创建表单 
    FORM.createControlToPage(divControlFormId, pageLoadParams.formCreateData, pageLoadParams.formUpdateData, pageLoadParams.formParamRowsData, pageLoadParams.formEntityInfo.TabTitles);
    //创建grid
    var gColumns = FORM.getFormatGridColumnsData(pageLoadParams.formCreateData, false, pageLoadParams.entityName);
    var gUrl = XR.defaultProcessUrl() + 'method=ajaxMenuAcionDataByFid'
        + '&key=' + pageLoadParams.formDataKey
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + '&AN=BLL.' + pageLoadParams.bllProjectName;
    FORM.createGrid(pageLoadParams.entityName, 'Fid', '', '', gColumns, gUrl, 10, '', false);

}
///设置
function setMenuAction() {
    var row = $('#' + tblGridId).datagrid('getSelected');
    if (isValidData(row)) {
        openMessagerProgress();
        var menuDataKey = pageLoadParams.formDataKey;
        var actionDataKey = row.Fid;
        var strfield = defaultSaveContent(pageLoadParams.formCreateData);
        var dataParam = 'method=ajaxSetMenuAction'
            + '&MENU_FID=' + menuDataKey
            + '&ACTION_FID=' + actionDataKey
            + '&' + strfield
            + "&ENTITY_NAME=" + pageLoadParams.entityName
            + "&AN=BLL." + pageLoadParams.bllProjectName;
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveResult');
    }
}
///取消
function cancelMenuAction() {
    var row = $('#' + tblGridId).datagrid('getSelected');
    if (isValidData(row)) {
        openMessagerProgress();
        var menuDataKey = pageLoadParams.formDataKey;
        var actionDataKey = row.Fid;
        var dataParam = 'method=ajaxCancelMenuAction'
            + '&MENU_FID=' + menuDataKey
            + '&ACTION_FID=' + actionDataKey
            + "&ENTITY_NAME=" + pageLoadParams.entityName
            + "&AN=BLL." + pageLoadParams.bllProjectName;
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveResult');
    }
}
///查询
function searchMenuAction() {
    $('#' + tblGridId).datagrid('load', {
        FILTER: getSearchFormData()
    });
}
///选中
function onClickRowByGrid(row) {
    setOnClickRowByGridToForm(row);
}
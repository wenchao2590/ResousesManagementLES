function searchLackOfInspection() {
    getLackOfInspectionEntity('search()');
}

function getLackOfInspectionEntity(methodName) {
    ///默认POST地址
    var ajaxUrl = XR.defaultProcessUrl();
    ///POST参数
    var dataParams = 'method=ajaxLackOfInspectionEntityFields'
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + '&TABLE_NAMES=' + pageLoadParams.tableName
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + '&FILTER=' + filterEncodeURIComponent(getSearchFormData());
    ///命名空间
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , dataParams
        , 'js:resultLackOfInspectionEntity(data,' + methodName + ')');
}
function resultLackOfInspectionEntity(data, methodName) {
    ///验证登录信息是否有效
    sessionIsNull(data);
    if (isSessionNull) return;
    data = JSON.parse(data);
    ///ENTITY_FIELD
    if (isValidData(data.entityfieldform)) pageLoadParams.formCreateData = data.entityfieldform;
    ///ENTITY
    if (isValidData(data.entityinfo)) pageLoadParams.formEntityInfo = data.entityinfo;
    ///加入全局变量
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    ///更新页面参数变量
    arrTempMap.put(pageLoadParams.entityName, pageLoadParams);
    ///数据获取地址
    var dataUrl = XR.defaultProcessUrl()
        + "method=ajaxLackOfInspectionPageList"
        + "&ENTITY_NAME=" + pageLoadParams.entityName
        + "&AN=BLL." + pageLoadParams.bllProjectName;
    ///绘制界面的数据网格
    createGrid(pageLoadParams.entityName
        , pageLoadParams.tableKeyField
        , ''
        , ''
        , pageLoadParams.DefaultCreateGridColumns()
        , dataUrl);
    if (isValidData(methodName))
        eval(methodName);
}
///导出供货计划
function exportSupplyPlan() {
    ///默认POST地址
    var ajaxUrl = XR.defaultProcessUrl();
    ///POST参数
    var dataParams = 'method=exportSupplyPlanExcel'
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + '&FILTER=' + filterEncodeURIComponent(getSearchFormData());
    tAlert('slide'
        , titleAlert
        , languageMessageData('1x00000034') ///正在生成Excel文件，请不要关闭此页面
        , 5000
        , 0);
    ///命名空间
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:downloadFile');
}

///生成缺件表
function createLackOfMaterial() {
    ///POST参数
    var dataParams = 'method=ajaxLackOfMaterial'
        + '&ENTITY_NAME=' + pageLoadParams.entityName
        + '&TABLE_NAMES=' + pageLoadParams.tableName
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + '&FILTER=' + filterEncodeURIComponent(getSearchFormData());
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:setStatusResult');
}
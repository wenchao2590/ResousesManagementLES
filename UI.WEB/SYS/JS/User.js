///保存用户角色
function bindRole() {
    openMessagerProgress();
    var dataParams
        = 'method=addUserRole&'
        + 'key=' + pageLoadParams.formDataKey + '&keylength=64&'
        + 'ENTITY_NAME=UserRole&AN=SYS&'
        + defaultSaveContent(pageLoadParams.detailEntityFields, 'D');
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:detailSaveResult");
}
///查询
function searchRangeAuth() {
    var roleFid = $('#20-D-RoleFid').combobox('getValue');
    if (!isValidData(roleFid)) return;
    var conditionFid = $('#20-D-ConditionFid').combobox('getValue');
    if (!isValidData(conditionFid)) return;
    $('#' + tblGridId).datagrid('load', {
        FILTER: '[ROLE_FID] = N\'' + roleFid + '\' and [CONDITION_FID] = N\'' + conditionFid + '\'' + getSearchFormData()
    });
    $('#' + tblGridId).datagrid("clearSelections");
}
///设置
function setRangeAuth() {
    if (loadCheckedRows()) {
        tAlert('confirm', titleAlert, languageMessageData('0x00000289'), 'confirmRangeAuth()', '');
    }
}
function confirmRangeAuth() {
    var roleFid = $('#20-D-RoleFid').combobox('getValue');
    if (!isValidData(roleFid)) return;
    var conditionFid = $('#20-D-ConditionFid').combobox('getValue');
    if (!isValidData(conditionFid)) return;
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=setRangeAuth'
        + '&key=' + pageLoadParams.formDataKey
        + '&UserFid=' + pageLoadParams.formParamRowsData.Fid
        + '&RoleFid=' + roleFid
        + '&ConditionFid=' + conditionFid
        , 'js:searchRangeAuth');
}
///取消
function cancelRangeAuth() {
    if (loadCheckedRows()) {
        tAlert('confirm', titleAlert, languageMessageData('0x00000290'), 'delRangeAuth()', '');
    }
}
function delRangeAuth() {
    var roleFid = $('#20-D-RoleFid').combobox('getValue');
    if (!isValidData(roleFid)) return;
    var conditionFid = $('#20-D-ConditionFid').combobox('getValue');
    if (!isValidData(conditionFid)) return;
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=delRangeAuth'
        + '&key=' + pageLoadParams.formDataKey
        + '&UserFid=' + pageLoadParams.formParamRowsData.Fid
        + '&RoleFid=' + roleFid
        + '&ConditionFid=' + conditionFid
        , 'js:searchRangeAuth');
}
///全选
function allRangeAuth() {
    tAlert('confirm', titleAlert, languageMessageData('0x00000291'), 'confirmAllRangeAuth()', '');
}
function confirmAllRangeAuth() {
    var roleFid = $('#20-D-RoleFid').combobox('getValue');
    if (!isValidData(roleFid)) return;
    var conditionFid = $('#20-D-ConditionFid').combobox('getValue');
    if (!isValidData(conditionFid)) return;
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=allRangeAuth'
        + '&UserFid=' + pageLoadParams.formParamRowsData.Fid
        + '&RoleFid=' + roleFid
        + '&ConditionFid=' + conditionFid
        , 'js:searchRangeAuth');
}
///不选
function noneRangeAuth() {
    tAlert('confirm', titleAlert, languageMessageData('0x00000292'), 'confirmNoneRangeAuth()', '');
}
function confirmNoneRangeAuth() {
    var roleFid = $('#20-D-RoleFid').combobox('getValue');
    if (!isValidData(roleFid)) return;
    var conditionFid = $('#20-D-ConditionFid').combobox('getValue');
    if (!isValidData(conditionFid)) return;
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=noneRangeAuth'
        + '&UserFid=' + pageLoadParams.formParamRowsData.Fid
        + '&RoleFid=' + roleFid
        + '&ConditionFid=' + conditionFid
        , 'js:searchRangeAuth');
}
///////////////////////////////////////////////////////////////////////////////////
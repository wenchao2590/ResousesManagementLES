///获取物料仓储信息
function getWarehouse() {
    ///仓库
    var wmNo = $('#40-SWmNo').combogrid('getValue');
    if (!isValidData(wmNo)) return;

    var dataParams = 'method=ajaxWarehouse'
        + '&WM=' + wmNo;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultWarehouse", true);
}
///
function resultWarehouse(data) {
    if (!isValidData(data)) return;
    if (!isJsonFormat(data)) return;
    data = JSON.parse(data);
    $('#40-Plant').combogrid('setValue', data.Plant);
}
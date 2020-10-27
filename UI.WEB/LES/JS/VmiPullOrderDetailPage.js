///获取库存数据
function getDeliverGoods() {
    var jsonstr = "[";
    var rows = $("#" + tblGridId).datagrid('getChecked');
    var row = $("#" + tblGridId).datagrid('getSelected');
    var reg = new RegExp("^(-)?[0-9][0-9]*$");
    if (!isValidData(rows) || rows.length == 0) {
        if (!isValidData(row)) {
            tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
            return false;
        }
        else {
            jsonstr += "{ 'Id': " + row.Id + ", 'AsnQty': " + row.AsnQty + " }";
        }
    }
    else {
        for (var i = 0; i < rows.length; i++) {
            jsonstr += "{ 'Id': " + rows[i].Id + ", 'AsnQty': " + rows[i].AsnQty + " },"
        }
        jsonstr = jsonstr.substring(0, jsonstr.lastIndexOf(','));
    }
    jsonstr += "]";

    var dataParams = 'method=ajaxDeliverGoods'
        + '&controlTypeJsonStr=' + jsonstr;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultDeliverGoods", true);
}
function resultDeliverGoods(data) {
    if (!isValidData(data)) return;
    search();
    $("#" + tblGridId).datagrid("clearChecked");
}
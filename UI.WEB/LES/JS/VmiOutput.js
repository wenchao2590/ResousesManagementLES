///获取库存数据
function getPartStockQty() {
    ///物料编号
    var partNo = $('#40-D-PartNo').combogrid('getValue');
    if (!isValidData(partNo)) return;
    ///仓库代码
    var wmNo = $('#20-D-WmNo').combobox('getValue');
    if (!isValidData(wmNo)) return;
    ///存储区代码
    var zoneNo = $('#20-D-ZoneNo').combobox('getValue');
    if (!isValidData(zoneNo)) return;
    ///库位代码
    var dloc = $('#20-D-Dloc').combobox('getValue');
    if (!isValidData(dloc)) return;
    var dataParams = 'method=getPartStockQty'
        + '&PART=' + partNo
        + '&WM=' + wmNo
        + '&ZONE=' + zoneNo
        + '&DLOC=' + dloc;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultPartStockQty", true);
}
function resultPartStockQty(data) {
    if (!isValidData(data)) return;
    $('#70-D-ActualQty').numberbox('setValue', data);
}
///获取物料仓储信息
function getPartsStock() {
    ///物料号
    var partNo = $('#40-D-PartNo').combogrid('getValue');
    if (!isValidData(partNo)) return;
    ///供应商
    var supplierNum = $('#40-D-SupplierNum').combogrid('getValue');
    if (!isValidData(supplierNum)) return;
    ///存储区
    var zoneNo = $('#40-TZoneNo').combogrid('getValue');
    if (!isValidData(zoneNo)) return;
    ///仓库
    var wmNo = $('#40-TWmNo').combogrid('getValue');
    if (!isValidData(wmNo)) return;
    var dataParams = 'method=ajaxPartsStock'
        + '&PART=' + partNo
        + '&SUPPLIER=' + supplierNum
        + '&ZONE=' + zoneNo
        + '&WM=' + wmNo;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultPartsStock", true);

}
///
function resultPartsStock(data) {
    if (!isValidData(data)) return;
    if (!isJsonFormat(data)) return;
    data = JSON.parse(data);
    ///包装型号
    $('#10-D-PackageModel').textbox('setValue', data.InboundPackageModel);
    ///库位
    $('#40-D-TargetDloc').combogrid('setValue', data.Dloc);
    ///收容数
    $('#70-D-Package').numberbox('setValue', data.InboundPackage);
}

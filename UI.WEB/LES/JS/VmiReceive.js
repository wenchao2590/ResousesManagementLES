///获取物料仓储信息
function getPartsStock() {
    ///物料号
    var partNo = $('#40-D-PartNo').combogrid('getValue');
    if (!isValidData(partNo)) return;
    ///供应商
    var supplierNum = $('#40-SupplierNum').combogrid('getValue');
    if (!isValidData(supplierNum)) return;
    ///存储区
    var zoneNo = $('#40-ZoneNo').combogrid('getValue');
    if (!isValidData(zoneNo)) return;
    ///仓库
    var wmNo = $('#40-WmNo').combogrid('getValue');
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
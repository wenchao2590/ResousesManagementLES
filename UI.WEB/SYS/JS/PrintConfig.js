///下载模板
function downloadPrintConfigTemplate() {
    openMessagerProgress();
    var row = $('#tblGrid').datagrid('getSelected');
    if (!isValidData(row)) {
        closeMessagerProgress();
        var alertMsg = languageMessageData('0x00000729');///请选择需要下载模板的打印配置
        tAlert('error', titleAlert, alertMsg); return;
    }
    var filesPath = row.PrintTemplateUrl + '\\\\' + row.PrintTemplateFilename + '.xls'
        + '|' + row.PrintTemplateUrl + '\\\\' + row.PrintTemplateFilename + '.xlsx'
        + '|' + row.PrintTemplateUrl + '\\\\' + row.PrintTemplateFilename + '.html'
        + '|' + row.PrintTemplateUrl + '\\\\' + row.PrintTemplateFilename + '.xml';
    var par = 'AN=BLL.SYS&ENTITY_NAME=PrintConfigUpload&FILE_PATH=' + filesPath;
    HELP.ajaxCommon(XR.defaultProcessUrl() + "method=ajaxDownLoadFiles", par, "js:downloadPrintConfigTemplateResult");
}
///
function downloadPrintConfigTemplateResult(data) {
    if (!alertMessage(data)) return;
    var filePaths = data.split('|');
    for (var i = 0; i < filePaths.length; i++) {
        window.open(filePaths[i]);
    }
}
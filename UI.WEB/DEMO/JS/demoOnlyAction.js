$(function () {
    var dataParams = "method=ajaxGetActionByPageUrl&PAGE_URL=DEMO/DemoOnlyAction.aspx&ENTITY_NAME=Action&AN=BLL.SYS";
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:createActionToDiv", false);
});
var pageEntity = null;
function createActionToDiv(data) {
    var divActionMenu = document.getElementById("divAction")
    divActionMenu.className = "easyui-panel panel-body panel-body-noheader";
    divActionMenu.style.width = "97.6%";
    divActionMenu.style.position = "fixed";
    divActionMenu.style.zIndex = "9999999";
    divActionMenu.style.marginBottom = "2px";
    var objdata = JSON.parse(data);
    FORM.createActionData('divAction', objdata.action);
}
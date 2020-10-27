var gridCreateData = [];   //创建表格数据 

var shanks = "haoshuais";
$(function () {

    XR.createInitForm();//初始化页面
    XR.ajaxCreateForm('SYS/RoleList.html');//创建表单 --XR.reqPath:当前页面路径

    //var gridUrl = XR.getPathLevel() + "HANDLER/mainHandler.ashx?method=getTables&tableName=TS_SYS_ACTION";

    //XR.ajaxCreateColumns('TS_SYS_ACTION', 0, true, true, true, true, 'ID', 'ID', 'desc', gridUrl);

    XR.ajaxCreateGrid('Role', 480, false, false, false, true, 'Fid', 'Fid', 'desc');

    //XR.ajaxDefaultCreateGrid('Action', 'Fid');

});

function add() {
    //XR.tAlert('pageIframe', '添加', 'SYS/UserEdit.html', 800, 480);
    XR.tAlert('pageIframe', '添加', 'SYS/CommonEdit.html', 800, 480);
}
function setRole() {
    //XR.tAlert('pageIframe', '添加', 'SYS/UserEdit.html', 800, 480);
    alert("setrole");
}


function search() {
    alert("search");
    $('#tblGrid').datagrid('load', {
        tblfliter: XR.getSearchFormData()
    });
}

function editrows(e) {

    alert(e);

    //XR.tAlert('pageIframe', '修改', 'RoleEdit.html', 800, 480);
}

function del() {
    alert("delete");
}


function createform() {
    //var corJsonData = [
    //    { "bindName": "ACTION_NAME", "tText": "动作名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "ACTION_NAME_EN", "tText": "英文名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "ACTION_TYPE", "tText": "接收层id", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "COMMENTS", "tText": "是否授权", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "ICON_URL", "tText": "图表类型", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "VALID_FLAG", "tText": "是否禁用", "tType": "combobox", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "CREATE_USER", "tText": "标签名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "CREATE_DATE", "tText": "图表样式", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "MODIFY_USER", "tText": "类型格式", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "MODIFY_DATE", "tText": "图表宽度", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
    //    { "bindName": "ID", "tText": "其他备注", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" }
    //];
    setControlToPage(XR.formCreateData, 'BODY', null);
}
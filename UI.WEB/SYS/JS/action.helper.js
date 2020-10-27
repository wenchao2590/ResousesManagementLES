
var entityName = "Action";

$(function () {
    var pageName = XR.reqPath.replace('/', '');
    XR.createInitForm();//初始化页面
    XR.ajaxCreateForm(pageName);//创建表单 --XR.reqPath:当前页面路径

    //var gridUrl = XR.getPathLevel() + "HANDLER/mainHandler.ashx?method=getTables&tableName=TS_SYS_ACTION";

    //XR.ajaxCreateColumns('TS_SYS_ACTION', 0, true, true, true, true, 'ID', 'ID', 'desc', gridUrl);

    XR.ajaxCreateGrid(entityName, 480, false, false, true, true, 'Fid', 'Fid', 'desc');

    //XR.ajaxDefaultCreateGrid('Action', 'Fid');

});

function add() {
  
    XR.operationType = "insert-TS_SYS_ACTION";
    XR.formDataKey = null;
    XR.tAlert('pageIframe', '添加', 'SYS/CommonEdit.html', 800, 480);
}

function search() {
 
    $('#tblGrid').datagrid('load', {
        FILTER: XR.getSearchFormData()
    });
  
}

function editrows() {

    XR.operationType = "update-TS_SYS_ACTION";

    var row = $("#tblGrid").datagrid('getSelected');
    if (row != null) {
        var operationType = "select-TS_SYS_ACTION";
        XR.formDataKey = row.Id;
        XR.ajaxCommon(XR.defaultProcessUrl(), 'method=' + operationType + '&key=' + row.Id, 'getformData');
    }

}

function del() {

    XR.operationType = "delete-TS_SYS_ACTION";

    var row = $("#tblGrid").datagrid('getSelected');
    if (row != null) {
        XR.ajaxCommon(XR.defaultProcessUrl(), 'method=' + XR.operationType + '&key=' + row.Id, 'alert("成功")');
    }
}


function getformData(data) {
    parent.XR.formUpdateData = data;
    XR.tAlert('pageIframe', '修改', 'SYS/CommonEdit.html', 800, 480);
}

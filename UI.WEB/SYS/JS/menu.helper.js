$(function () { 
    XR.createInitForm();//初始化页面
    XR.ajaxCreateForm('SYS/MenuList.html');//创建表单 --XR.reqPath:当前页面路径

    XR.ajaxDefaultCreateGrid('Menu', 'Fid');
});

function add() {
    gridCreateData = [];
    XR.tAlert('pageIframe', '添加', 'CommonEdit.html', 800, 480);
}

function search() {

    $('#tblGrid').datagrid('load', {
        tblfliter: XR.getSearchFormData()
    });
}

function editrows(e) {

    alert(e);
    XR.tAlert('pageIframe', '修改', 'CommonEdit.html', 800, 480);
}

function del() {
    alert("delete");
}


function openDialog() {
    var url = 'SYS/MenuEdit.html';
    var corJsonData = [
        { "bindName": "NAME", "tText": "图表名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "NAME_EN", "tText": "英文名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "RECEIVER_LAYER", "tText": "接收层id", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "IS_AUTH", "tText": "是否授权", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_TYPE", "tText": "图表类型", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "MIX_CONDITION", "tText": "混合条件", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_LABEL_NAME", "tText": "标签名称", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_STYLE", "tText": "图表样式", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "TYPE_FORMAT", "tText": "类型格式", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_WIDTH", "tText": "图表宽度", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_HEIGHT", "tText": "图表高度", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_COLUMN_NAME", "tText": "横坐标名", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_ROW_NAME", "tText": "纵坐标名", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "SQL_STRING", "tText": "数据SQL", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "CHART_OTHER_FORMAT", "tText": "其他样式", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" },
        { "bindName": "REMARK", "tText": "其他备注", "tType": "Text", "tVerifyType": "", "tWidth": "120", "tHeight": "20", "isdisplay": "true" }
    ];
    XR.tAlert('page', '添加', url, 800, 560);
}

function collapseDev(e) {

    if ($('#divSearchForm')[0].style.display == "none") {

        $('#divSearchForm').show();
    } else {
        $('#divSearchForm').hide();//|| $('#divSearchForm')[0].style.display == "block"
    }
    //  alert('zhidie');
}
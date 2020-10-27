
var isSubPage = false;
///页面数据模型对象实例化
var pageLoadParams = new PageEntity();
///已加载过此数据模型时将会从页面存储中获取
var oldpageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
///提示
var titleAlert = languageMessageTitle('1x00000001');
///
$(function () {
    openMessagerProgress();
    loadEntityJS(pageLoadParams.entityName, function () { loadEntityJS("Entrance", function () { }) });
});


function loadEntityJS(entityName, callback) {
    var head = document.getElementsByTagName("head")[0];
    var script = document.createElement("script");
    var url = XR.getPathLevel() + "/JS/common/" + entityName + ".js";
    if (entityName != "Entrance") {
        url = XR.getPathLevel() + "/" + oldpageLoadParams.bllProjectName + "/JS/" + entityName + ".js";
    }
    script.type = "text/javascript";
    script.id = "expandJs";
    if (script.readyState) {
        script.onreadystatechange = function () {
            if (script.readyState == "loaded" || script.readyState == "complete") {
                script.onreadystatechange = null; callback();
            }

        }
    } else {
        script.onload = function () {
            callback();
        }
        script.onerror = function () {

            callback();
        }

    }
    script.src = url;
    head.appendChild(script);
}
///上传文件
function uploadFiles(methodFlag, methodName) {
    var loadingTitle = languageMessageData('1x00000044');
    openMessagerProgress(loadingTitle, 1000);
    var fileboxs = pageLoadParams.formCreateData;
    var formData = new FormData($("#formItem")[0]);
    for (var i = 0; i < fileboxs.length; i++) {
        if (fileboxs[i].ControlType + '' != '110') continue;
        ///路径|文件名
        var filePath = fileboxs[i].Extend1;
        if (isValidData(filePath) && (filePath + '').indexOf('#') > -1) {
            filePath = eval('pageLoadParams.formParamRowsData.' + filePath.replace('#', ''));
        }
        var fileName = fileboxs[i].Extend2;
        if (isValidData(fileName) && (fileName + '').indexOf('#') > -1) {
            fileName = eval('pageLoadParams.formParamRowsData.' + fileName.replace('#', ''));
        }
        formData.append('file' + fileboxs[i].FieldName, filePath + '|' + fileName);
    }
    var dataKey = '';
    ///是否需要执行后台函数
    if (isValidData(methodFlag) && (methodFlag + '').toLowerCase() == 'true') {
        if (isValidData(pageLoadParams.formDataKey))
            dataKey = pageLoadParams.formDataKey;
        ///
        if (!isValidData(dataKey)) {
            ///先根据配置获取配置相关的KEY值
            if (isValidData(pageLoadParams.tableKeyField)) {
                var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
                for (var i = 0; i < arrayKeyFields.length; i++) {
                    dataKey += '|' + eval("pageLoadParams.formParamRowsData." + arrayKeyFields[i]);
                }
                if (dataKey.length > 1) {
                    dataKey = dataKey.substring(1);
                }
            }
        }
        if (isValidData(methodName))
            dataKey += '&methodName=' + methodName;
    }
    $.ajax({
        url: XR.defaultProcessUrl() + "method=ajaxUpLoadFiles"
            + "&ENTITY_NAME=" + pageLoadParams.entityName
            + "&AN=" + pageLoadParams.bllProjectName
            + '&TABLE_NAMES=' + pageLoadParams.tableName
            + '&key=' + dataKey,
        type: 'POST',
        data: formData,
        async: true,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            //上传成功后将控件内容清空，并显示上传成功信息
            alertMessage(data);
        },
        error: function (data) {
            //上传失败时显示上传失败信息
        }
    });
}
///创建导入窗体
function importForm() {
    ///先加载LIST窗体的一些属性
    pageLoadParams.GetListData(oldpageLoadParams);
    pageLoadParams.AjaxActionOrEntityData('import', 'importFormInit()');
}
function importFormInit() {
    createInitForm(pageLoadParams.formActionEditData, null);
    pageLoadParams.DefaultCreateAction();
    pageLoadParams.DefaultCreateControlForm();
}
///导入
function importExcel() {
    var loadingTitle = languageMessageData('1x00000044');
    openMessagerProgress(loadingTitle, 1000);
    var formData = new FormData($("#formItem")[0]);
    var locParams = pageLoadParams;
    var searchCondtions = '';
    var urlFilter = '';
    if (isValidData(pageLoadParams.detailEntityName)) {
        locParams = parent.arrGlobal.get(pageLoadParams.detailEntityName);
        searchCondtions = filterEncodeURIComponent(getSearchFormData());
        urlFilter = locParams.urlFilter;
    }
    $.ajax({
        url: XR.defaultProcessUrl() + "method=ajaxImportData"
            + "&ENTITY_NAME=" + locParams.entityName
            + "&AN=" + locParams.bllProjectName
            + '&URL_FILTER=' + urlFilter
            + '&FILTER=' + searchCondtions,
        type: 'POST',
        data: formData,
        async: true,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            //上传成功后将控件内容清空，并显示上传成功信息
            alertMessage(data);
            ///成功后关闭窗体？
            search();
        },
        error: function (data) {
            //上传失败时显示上传失败信息
        }
    });
}
///创建默认编辑窗体
function createForm() {
    ///包含主从表方式的窗体
    pageLoadParams.DefaultCreateEdit(oldpageLoadParams);
}

////////////////////////////////////////////////////SubForm//////////////////////////////////////////////////
function createSubForm(ctrlType) {
    if (!isValidData(pageLoadParams)) return;
    if (!isValidData(pageLoadParams.formEntityInfo)) return;
    var subEntityConfigInfo = pageLoadParams.formEntityInfo.ParentField;
    ///此处需要多给出一个commonEdit.URL能获取的参数校验在多个平行关系从表的情况下，当前页面针对的是哪个从表
    if (!isValidData(subEntityConfigInfo)) return;
    var subEntityInfo = subEntityConfigInfo.split('-');
    if (subEntityInfo.length <= 2) return;
    var subEntityName = subEntityInfo[1];
    var subTableName = subEntityInfo[2];
    //加载后台页面数据
    pageLoadParams.AjaxCreateFromDataByActionOrEntityData(subEntityName, subTableName, "createSubList('" + subEntityName + "','" + ctrlType + "');");
}
///
function createSubList(subEntityName, ctrlType) {
    if (!isValidData(subEntityName)) return;
    parrSubEntity = parent.arrGlobal.get(subEntityName);
    ///TODO？
    window.parentArrMap = null;
    parentArrMap = parrSubEntity;
    ///初始化窗体
    if (ctrlType + '' != 'D') {
        FORM.createInitForm(parrSubEntity.formActionEditData, parrSubEntity.formSearchEditData);
        ///加载Action控件
        FORM.createActionData(divActionMenuId, parrSubEntity.formActionEditData);
        ///加载Search控件
        FORM.createSearchControlData(divSearchFormId, parrSubEntity.formSearchEditData, parrSubEntity.formSearchColumnLength);
    }
    ///加载EntityField表单
    FORM.createControlToPage(divControlFormId, parrSubEntity.formCreateData, parrSubEntity.formUpdateData, parrSubEntity.formParamRowsData, parrSubEntity.formEntityInfo.TabTitles, ctrlType);
    ///加载EntityField表格
    var isMuliCheck = isGridMuliCheck(parrSubEntity.formEntityInfo.ParentField);
    var gColumns = FORM.getFormatGridColumnsData(parrSubEntity.formCreateData, isMuliCheck, subEntityName);
    var gUrl = XR.defaultProcessUrl() + "FILTER=" + pageLoadParams.formParamKey + "&method=ajaxTables&AN=BLL." + pageLoadParams.bllProjectName + "&ENTITY_NAME=" + subEntityName;
    FORM.createGrid(subEntityName, '', '', '', gColumns, gUrl, 10);
}

///
function createTreeSubForm() {
    isSubPage = true;
    //加载LIST页面数据
    pageLoadParams.GetListData(oldpageLoadParams);
    if (!isValidData(pageLoadParams)) return;
    FORM.createInitForm(pageLoadParams.formActionEditData, pageLoadParams.formSearchEditData);
    FORM.createActionData(divActionMenuId, pageLoadParams.formActionEditData);
    FORM.createSearchControlData(divSearchFormId, pageLoadParams.formSearchEditData, pageLoadParams.formSearchColumnLength);
    FORM.createControlToPage(divControlFormId, pageLoadParams.formCreateData, pageLoadParams.formUpdateData, pageLoadParams.formParamRowsData, pageLoadParams.formEntityInfo.TabTitles);
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////


function publicCreate() {
    //openMessagerProgress();
    window.parentArrMap = null;
    isSubPage = true;
    // 重写onClickRowByGrid函数
    window.onClickRowByGrid = function (row) {

        var entityInfo = parentArrMap.formEntityInfo;
        if (row.Fid != "") {
            parentArrMap.formDataKey = row.Id;
            parentArrMap.operationType = "update-" + parentArrMap.tableName;
        }
        setOnClickRowByGridToFormContent(row, parentArrMap.formCreateData);
    }
    var parentObj = parent.arrGlobal.get(pageLoadParams.entityName);
    var parentEntityName = parentObj.parentEntityName;
    var isLoad = false;
    if (parentEntityName == null && parentEntityName == undefined) {
        isLoad = true;
    }
    else {
        parentArrMap = parent.document.getElementById("Iframe_" + parentEntityName).contentWindow.arrTempMap.get(pageLoadParams.entityName);
        if (parentArrMap != undefined && parentArrMap.formActionEditData != "") {
            parentArrMap.formDataKey = "";
            FORM.createInitForm(parentArrMap.formActionEditData, parentArrMap.formSearchEditData);
            FORM.createActionData(divActionMenuId, parentArrMap.formActionEditData);
            FORM.createSearchControlData(divSearchFormId, parentArrMap.formSearchEditData, parentArrMap.formSearchColumnLength);
            FORM.createControlToPage(divControlFormId, parentArrMap.formCreateData, parentArrMap.formUpdateData, parentArrMap.formParamRowsData, parentArrMap.formEntityInfo.TabTitles);
            parentArrMap.formParamKey = parentArrMap.formParamKey.indexOf("&") == -1 ? parentArrMap.formParamKey + "&" : parentArrMap.formParamKey;
            ///是否多选
            var isMuliCheck = isGridMuliCheck(parentArrMap.formEntityInfo.ParentField);
            var gColumns = FORM.getFormatGridColumnsData(parentArrMap.formCreateData, isMuliCheck, parentArrMap.entityName);
            var gUrl = XR.defaultProcessUrl() + "FILTER=AND " + parentArrMap.formParamKey + "method=ajaxTables&AN=BLL." + parentArrMap.bllProjectName + "&ENTITY_NAME=" + parentArrMap.entityName + "&TABLE_NAMES=" + parentArrMap.tableName;
            FORM.createGrid(parentArrMap.entityName, 'Fid', '', '', gColumns, gUrl, 10, '', false, true);
            expandControlConfig();
        } else {
            isLoad = true;
        }
    }
    if (isLoad) {
        var choiceNowGrid = parent.document.getElementById("Iframe_" + parentEntityName).contentWindow.gridChoiceId;
        var parentTempMap = parent.document.getElementById("Iframe_" + parentEntityName).contentWindow.arrTempMap;
        var parentEntityName = parentTempMap.get(choiceNowGrid);
        window.nowChoiceparentObj = parentTempMap.get(parentEntityName);
        //加载后台页面数据
        parentArrMap = parentTempMap.get(pageLoadParams.entityName);
        parentArrMap.formDataKey = "";
        parentArrMap.AjaxActionOrEntityData("All", "");
        createisLoad();
    }
    //pageLoadParams.entityName 添加默认的插入方法，这里的插入是在编辑页清空选中和文本初始值。创建插入环境
    var medthod = "window.insert" + pageLoadParams.entityName + "=function(){insertEntity('" + pageLoadParams.entityName + "');};\r\n";
    eval(medthod);
    expandControlConfig();

}

function expandControlConfig() {
    var isFristLoad = true;
    var menuName = parent.$(".tabs-selected")[0].getElementsByTagName("span")[0].innerHTML;
    switch (menuName) {
        default:
            break;
    }
}

var comgridValue = new Map();
function createisLoad() {

    //openMessagerProgress();
    //页面初始化
    FORM.createInitForm(parentArrMap.formActionEditData, parentArrMap.formSearchEditData);
    //创建按钮
    FORM.createActionData(divActionMenuId, parentArrMap.formActionEditData);
    //创建搜索控件
    FORM.createSearchControlData(divSearchFormId, parentArrMap.formSearchEditData, parentArrMap.formSearchColumnLength);
    //创建表单 
    FORM.createControlToPage(divControlFormId, parentArrMap.formCreateData, parentArrMap.formUpdateData, parentArrMap.formParamRowsData, parentArrMap.formEntityInfo.TabTitles);
    //创建grid 

    var strfilter = "FILTER=AND " + parentArrMap.formParamKey + "&";
    ///是否多选
    var isMuliCheck = isGridMuliCheck(parentArrMap.formEntityInfo.ParentField);
    var gColumns = FORM.getFormatGridColumnsData(parentArrMap.formCreateData, isMuliCheck, parentArrMap.entityName);
    parentArrMap.formEntityInfo.EntityType = 1;
    parentArrMap.tableName = parentArrMap.formEntityInfo.TableNames;
    var bll = parentArrMap.bllProjectName;

    var gUrl = XR.defaultProcessUrl() + strfilter + "method=ajaxTables&AN=BLL." + bll + "&ENTITY_NAME=" + parentArrMap.entityName;
    FORM.createGrid(parentArrMap.entityName, 'Fid', '', '', gColumns, gUrl, 10);
}
///根据实体名清空编辑界面对应内容
function insertEntity(entityName) {
    cleanControlValue(parentArrMap.formCreateData, '', parentArrMap.formParamRowsData);
    parentArrMap.formDataKey = '';
}

///重写行点击事件，用于主从明细加载
function onClickRowByGrid(row) {
    if (isValidData(pageLoadParams.detailEntityInfo)) {
        pageLoadParams.detailSelectRowData = row;
        setOnClickRowByGridToFormContent(pageLoadParams.detailSelectRowData, pageLoadParams.detailEntityFields, 'D');
    }
}
///明细表单保存数据
function defaultSaveSub() {
    openMessagerProgress();
    var dataPar = defaultSaveContent(parentArrMap.formCreateData
        , ''
        , getPattributeNameArray(parentArrMap.formParamKey));
    ///没有获取到界面上用户填写的参数
    if (!isValidData(dataPar)) {
        closeMessagerProgress();
        return;
    }
    ///主表的关联字段属性
    var parentParamKey = replaceEntityFieldNameByTableFieldNameUrlfilter(parentArrMap.formParamKey);
    var operationType = "insert-" + parentArrMap.tableName;
    var dataParam;
    ///编辑
    if (isValidData(parentArrMap.formDataKey)) {
        parentParamKey += 'key=' + parentArrMap.formDataKey + '&';
        operationType = "update-" + parentArrMap.tableName;
    }
    dataParam = parentParamKey + dataPar + "method=" + operationType + "&ENTITY_NAME=" + parentArrMap.entityName + "&AN=BLL." + parentArrMap.bllProjectName;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveSubResult');
}

function defaultSaveSubEntity() {

    openMessagerProgress();
    var dataPar = defaultSaveContent(parrSubEntity.formCreateData);
    if (dataPar != undefined && dataPar != null && dataPar != "") {
        var parentParamKey = convertEntityNameByColumnName(parrSubEntity.formParamKey);
        var operationType = "insert-" + parrSubEntity.tableName;
        if (parrSubEntity.formDataKey != "") {
            parentParamKey = 'key=' + parrSubEntity.formDataKey + '&';
            operationType = "update-" + parrSubEntity.tableName;
        }
        var dataParam = parentParamKey + dataPar + "method=" + operationType + "&ENTITY_NAME=" + parrSubEntity.entityName + "&AN=BLL." + parrSubEntity.bllProjectName;
        HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:saveSubResult');
    }
}
///明细表单保存返回结果
function saveSubResult(par) {
    alertMessage(par);
    try {
        if (parentArrMap.formEntityInfo.EntityType + "" == "2") {
            $("#" + tblGridId).treegrid("unselectAll");///unselectAll
            $("#" + tblGridId).treegrid('reload');
        }
        else {
            $("#" + tblGridId).datagrid("clearSelections");///unselectAll
            $("#" + tblGridId).datagrid('reload');
        }
        insertEntity(parentArrMap.entityName);
    } catch (e) {

    }
}

function confirmDelete() {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=' + pageLoadParams.operationType
        + '&key=' + pageLoadParams.formDataKey
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + pageLoadParams.tableName, 'js:delResult');
}

function del() {

    pageLoadParams.operationType = "delete-" + pageLoadParams.tableName;

    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        pageLoadParams.formDataKey = row.Id;

        var data = languageMessageTitle('0x00000075');//是否确认删除？
        tAlert("confirm", titleAlert, data, "confirmDelete()", "");
    }
}

function delResult(data) {
    alertMessage(data);
    if (pageLoadParams.formEntityInfo.EntityType + "" == "2") {
        $("#" + tblGridId).treegrid("unselectAll");///unselectAll
        $("#" + tblGridId).treegrid('reload');
    }
    else {
        $("#" + tblGridId).datagrid("clearSelections");///unselectAll
        $("#" + tblGridId).datagrid('reload');
    }
}

function confirmDeleteSub() {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl(), 'method=' + parentArrMap.operationType + '&key=' + parentArrMap.formDataKey + "&ENTITY_NAME=" + parentArrMap.entityName + "&AN=BLL." + parentArrMap.bllProjectName, 'js:delSubResult');
}

function delSub() {
    parentArrMap.operationType = "delete-" + parentArrMap.tableName;
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row != null) {
        parentArrMap.formDataKey = row.Id;
        parent.arrGlobal.put(parentArrMap.entityName, parentArrMap);
        var data = languageMessageTitle('0x00000075');
        tAlert("confirm", titleAlert, data, "confirmDeleteSub()", "");
    }
    else {
        var data = languageMessageData('0x00000053');//请选择需删除的行数据
        tAlert("error", titleAlert, data);
    }
    ///TODO:增加未选中行时的提示信息
}

function delSubResult(data) {
    alertMessage(data);
    if (parentArrMap.formEntityInfo.EntityType + "" == "2") {
        $("#" + tblGridId).treegrid("unselectAll");///unselectAll
        $("#" + tblGridId).treegrid('reload');
    }
    else {
        $("#" + tblGridId).datagrid("clearSelections");///unselectAll
        $("#" + tblGridId).datagrid('reload');
    }
    insertEntity(parentArrMap.entityName);
}

///添加[保存]TREE_GRID子项
function saveTreeEdit() {
    openMessagerProgress();
    ///
    if (isSubPage) {
        ///获取用户填写的值，组成地址栏参数
        var dataPar = defaultSaveContent(pageLoadParams.formCreateData);
        if (isValidData(dataPar)) {
            var parentParamKey = '';
            var operationType = "insert-" + pageLoadParams.tableName;
            if (isValidData(pageLoadParams.formParamKey)) {
                parentParamKey = replaceEntityFieldNameByTableFieldNameUrlfilter(pageLoadParams.formParamKey) + '&';
            }
            ///urlFilter参数获取
            if (isValidData(pageLoadParams.urlFilter)) {
                parentParamKey += replaceEntityFieldNameByTableFieldNameUrlfilter(pageLoadParams.urlFilter) + '&';
            }
            ///如果parentParamKey为空时则插入对象为根节点
            var dataParam = parentParamKey + dataPar
                + 'method=' + operationType
                + '&ENTITY_NAME=' + pageLoadParams.entityName
                + '&AN=BLL.' + pageLoadParams.bllProjectName;
            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:SaveTreeResult');
        }
    }
    else {
        defaultSave();
    }
}

///编辑窗体中仅支持DATAGRID
function search() {
    var gridControl = $('#' + tblGridId);
    if (!isValidData(gridControl.length)) return;
    gridControl.datagrid('load', {
        SECOND_FILTER: getSearchFormData()
    });
}

function SaveTreeResult(par) {
    alertMessage(par);
    try {
        $("#" + tblGridId).treegrid('reload');
    } catch (e) {

    }
}
///
function saveResult(data) {
    ///操作失败会返回false
    if (alertMessage(data)) {
        ///formUpdateData为Json字符串
        pageLoadParams.formUpdateData = data;
        ///formParamRowsData中为对象
        pageLoadParams.formParamRowsData = toJson(data);
        ///新增后返回的KEY值需要填充到界面上
        if (!isValidData(pageLoadParams.formDataKey))
            pageLoadParams.formDataKey = getDataKey(pageLoadParams.tableKeyField, pageLoadParams.formParamRowsData);
        ///将新增界面换为更新状态
        if (pageLoadParams.operationType.indexOf("insert-") != -1)
            pageLoadParams.operationType = pageLoadParams.operationType.replace("insert-", "update-");
        ///刷新界面
        setOnClickRowByGridToFormContent(pageLoadParams.formParamRowsData, pageLoadParams.formCreateData);
    }
    ///有明细的情况下刷新列表
    search();
}

function GetFieldValue(fields, fieldName) {
    var dataTemp = fields.substring(fields.indexOf(fieldName))
    return dataTemp.substring(dataTemp.indexOf("=") + 1, dataTemp.indexOf("&"));
}


///////////////////////////////////////////////////////////////////////////////Detail Data Operation Start////////////////////////
///表单中明细数据保存方法
function detailSave() {
    ///主表没有记录
    if (!isValidData(pageLoadParams.formUpdateData)) {
        tAlert('alert', titleAlert, '请先保存主表数据', 500, 200);
        return;
    }
    ///未配置明细模型关系
    if (!isValidData(pageLoadParams.detailRelationKeyFieldName)) {

        return;
    }
    ///未配置明细模型
    if (!isValidData(pageLoadParams.detailEntityFields)) return;
    ///获取明细的数据结构
    var entityFields = pageLoadParams.detailEntityFields;
    ///获取界面中用户填写的值
    var dataPar = defaultSaveContent(entityFields, 'D');
    if (!isValidData(dataPar)) return;
    var rKeyValue = '';
    ///detail字段|main属性
    var relationFields = pageLoadParams.detailRelationKeyFieldName.split('|');
    if (relationFields.length == 1) {
        ///关联外键拼接到提交参数内
        dataPar += convertEntityNameByColumnName(relationFields[0]) + '=' + pageLoadParams.formDataKey + '&';
    }
    else {
        if (isValidData(pageLoadParams.formUpdateData)) {
            var relaFields = relationFields[0].split(',');
            var tionFields = relationFields[1].split(',');
            if (relaFields.length <= tionFields.length) {
                for (var i = 0; i < relaFields.length; i++) {
                    var rField = relaFields[i];
                    if (!isValidData(rField)) continue;
                    var tField = tionFields[i];
                    if (!isValidData(tField)) continue;
                    var oData = toJson(pageLoadParams.formUpdateData);
                    if (!isValidData(oData)) continue;
                    var tValue = eval('oData.' + tField);
                    if (isValidData(tValue))
                        dataPar += convertEntityNameByColumnName(rField) + '=' + tValue + '&';
                    else
                        dataPar += convertEntityNameByColumnName(rField) + '=&';
                }
            }
        }
    }
    ///GRID中选定值
    if (isValidData(pageLoadParams.detailSelectRowData)) {
        var detailKey = '';
        var detailKeyLength = pageLoadParams.detailTableKeyLength;
        ///明细主键
        if (isValidData(pageLoadParams.detailTableKeyField)) {
            var arrayKeyFields = pageLoadParams.detailTableKeyField.split("|");
            for (var i = 0; i < arrayKeyFields.length; i++) {
                detailKey += '|' + eval("pageLoadParams.detailSelectRowData." + arrayKeyFields[i]);
            }
        }
        if (isValidData(detailKey))
            detailKey = detailKey.substring(1);
        if (!isValidData(detailKey)) {
            detailKey = pageLoadParams.detailSelectRowData.Id;
            detailKeyLength = '64';
        }
        dataPar += "method=update-" + pageLoadParams.detailEntityName + "&key=" + detailKey + '&keylength=' + detailKeyLength + '&';
    }
    else
        dataPar += "method=insert-" + pageLoadParams.detailEntityName + '&';
    dataPar += "ENTITY_NAME=" + pageLoadParams.detailEntityName + "&"
        + "AN=BLL." + pageLoadParams.bllProjectName;
    var processUrl = XR.defaultProcessUrl();
    ajaxCommon(processUrl, dataPar, "js:detailSaveResult");
}
///清空明细界面，用于【添加明细】
function detailClear() {
    if (!isValidData(pageLoadParams.detailEntityInfo)) return;
    cleanControlValue(pageLoadParams.detailEntityFields, 'D', pageLoadParams.formParamRowsData);
    pageLoadParams.detailSelectRowData = null;
    ///选中详情页卡的第一页
    var tabs = $('#tabEntityFields').tabs('tabs');
    for (var i = 0; i < tabs.length; i++) {
        if (tabs[i].panel('options').iconCls != 'icon-0000004') continue;
        $('#tabEntityFields').tabs('select', tabs[i].panel('options').title);
        break;
    }
    search();
}
///清空明细表界面赋值
function detailSaveResult(data) {
    alertMessage(data);
    ///过滤条件
    var gFilter = getDetailFilter(pageLoadParams.detailRelationKeyFieldName
        , pageLoadParams.formDataKey
        , pageLoadParams.formParamRowsData);
    ///数据地址
    var gDataUrl = XR.defaultProcessUrl()
        + "method=" + pageLoadParams.detailGetDataMethod
        + "&ENTITY_NAME=" + pageLoadParams.detailEntityName
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&URL_FILTER=" + filterEncodeURIComponent(gFilter);
    ///更新列表数据获取地址
    $("#" + tblGridId).datagrid('options').url = gDataUrl;
    detailClear();
}
///删除
function detailDel() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (row == null) {
        var data = languageMessageData('0x00000053');///请选中行数据
        tAlert('error', titleAlert, data);
        return;
    }
    var data = languageMessageTitle('0x00000075');///是否确认删除
    tAlert('confirm', titleAlert, data, 'detailDelConfirm()', '');
}
///
function detailDelConfirm() {
    openMessagerProgress();
    var detailKey = '';
    var detailKeyLength = pageLoadParams.detailTableKeyLength;
    ///明细主键
    if (isValidData(pageLoadParams.detailTableKeyField)) {
        var arrayKeyFields = pageLoadParams.detailTableKeyField.split("|");
        for (var i = 0; i < arrayKeyFields.length; i++) {
            detailKey += '|' + eval("pageLoadParams.detailSelectRowData." + arrayKeyFields[i]);
        }
    }
    if (isValidData(detailKey))
        detailKey = detailKey.substring(1);
    if (!isValidData(detailKey)) {
        detailKey = pageLoadParams.detailSelectRowData.Id;
        detailKeyLength = '64';
    }
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=delete-' + pageLoadParams.detailEntityName
        + '&key=' + detailKey
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + pageLoadParams.detailEntityName
        , 'js:detailSaveResult');
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////Set Status/////////////////////////////
///更新数据状态
function setStatus(actionname) {
    var dataParam = 'method=ajaxStatusTypeConfirmMessage&actionname=' + actionname + '&ENTITY_NAME=CodeItem&AN=BLL.SYS';
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:setStatusMessageResult');
}
///加载选中行
function loadSelection() {
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (!isValidData(row)) {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    ///清空FORM选中项，重新由GRID进行获取
    ///TODO:其实本人觉得这种方式并不优雅，后续需要改进，在选中时就构建formDataKey更为通用
    pageLoadParams.formDataKey = '';
    ///联合主键方式
    if (isValidData(pageLoadParams.tableKeyField)) {
        var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
        for (var j = 0; j < arrayKeyFields.length; j++) {
            var keyFValue = eval("row." + arrayKeyFields[j]);
            if (!isValidData(keyFValue)) continue;
            pageLoadParams.formDataKey += '|' + keyFValue;
        }
    }
    else {
        ///如果未设置主键则先尝试Id属性，因为在数据库表设计时已控制了ID字段为自增长主键
        ///所以程序多半会走这个逻辑
        if (isValidData(row.Id))
            pageLoadParams.formDataKey += row.Id;
        ///ID字段在数据库设计时就应为bigint
        pageLoadParams.formDataKeyLength = '64';
        ///部分公司数据库表的主键被设置为NID
        if (!isValidData(pageLoadParams.formDataKey)) {
            if (isValidData(row.Nid))
                pageLoadParams.formDataKey += row.Nid;
            ///NID字段在数据库设计时就应为int
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    if (!isValidData(pageLoadParams.formDataKey)) {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    return true;
}
///加载选中行
function loadCheckedRows(msgFlag) {
    var rows = $("#" + tblGridId).datagrid('getChecked');
    if (!isValidData(rows) || rows.length == 0) {
        if (!isValidData(msgFlag) || msgFlag == true)
            tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    ///清空FORM选中项，重新由GRID进行获取
    ///TODO:其实本人觉得这种方式并不优雅，后续需要改进，在选中时就构建formDataKey更为通用
    pageLoadParams.formDataKey = '';
    ///联合主键方式
    if (isValidData(pageLoadParams.tableKeyField)) {
        var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
        for (var i = 0; i < rows.length; i++) {
            pageLoadParams.formDataKey += '-';
            for (var j = 0; j < arrayKeyFields.length; j++) {
                var keyFValue = eval("rows[i]." + arrayKeyFields[j]);
                if (!isValidData(keyFValue)) continue;
                pageLoadParams.formDataKey += '|' + keyFValue;
            }
        }
    }
    else {
        ///如果未设置主键则先尝试Id属性，因为在数据库表设计时已控制了ID字段为自增长主键
        ///所以程序多半会走这个逻辑
        for (var i = 0; i < rows.length; i++) {
            if (isValidData(rows[i].Id))
                pageLoadParams.formDataKey += '-' + rows[i].Id;
        }
        ///ID字段在数据库设计时就应为bigint
        pageLoadParams.formDataKeyLength = '64';
        ///部分公司数据库表的主键被设置为NID
        if (!isValidData(pageLoadParams.formDataKey)) {
            for (var i = 0; i < rows.length; i++) {
                if (isValidData(rows[i].Nid))
                    pageLoadParams.formDataKey += '-' + rows[i].Nid;
            }
            ///NID字段在数据库设计时就应为int
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    if (!isValidData(pageLoadParams.formDataKey) || pageLoadParams.formDataKey == '-') {
        tAlert("error", titleAlert, languageMessageData('0x00000187'));///请选择行数据
        return false;
    }
    parent.arrGlobal.put(pageLoadParams.entityName, pageLoadParams);
    return true;
}
///获取需要更新状态的选定数据行
function setStatusMessageResult(data) {
    data = eval('(' + data + ')');
    pageLoadParams.operationType = "status-" + pageLoadParams.tableName + "-" + data.actionname;
    ///TODO:这个验证是否考虑去除，之后的代码用的是变量，此处的校验就没有什么意义了
    if (tblGridId != "tblGrid")
        return tAlert("error", titleAlert, languageMessageData('0x00000185'));///不能对子项进行删除操作！
    if (!loadCheckedRows(false)) {
        if (!loadSelection()) return;
    }
    tAlert('confirm', titleAlert, data.message, 'confirmSetStatus()', '');
}
///更新数据状态请求提交
function confirmSetStatus() {
    openMessagerProgress();
    HELP.ajaxCommon(XR.defaultProcessUrl()
        , 'method=' + pageLoadParams.operationType
        + '&key=' + pageLoadParams.formDataKey
        + "&AN=BLL." + pageLoadParams.bllProjectName
        + "&ENTITY_NAME=" + pageLoadParams.entityName
        + "&keylength=" + pageLoadParams.formDataKeyLength
        , 'js:setStatusResult');
}
///更新数据状态执行完成
function setStatusResult(data) {
    if (!alertMessage(data)) return;
    if (parentArrMap.formEntityInfo.EntityType + '' == '2') {
        $("#" + tblGridId).treegrid("unselectAll");
        $("#" + tblGridId).treegrid('reload');
    }
    else {
        $("#" + tblGridId).datagrid("clearSelections");
        $("#" + tblGridId).datagrid('reload');
    }
    insertEntity(parentArrMap.entityName);
}
///导出EXCEL
function exportExcel() {
    var locParams = parent.arrGlobal.get(pageLoadParams.entityName);
    if (isValidData(pageLoadParams.detailEntityName))
        locParams = parent.arrGlobal.get(pageLoadParams.detailEntityName);
    excelExport(locParams);
}
///下载模板
function downloadTemplate(entityName) {
    if (isValidData(entityName))
        pageLoadParams = parent.arrGlobal.get(entityName);
    else
        pageLoadParams = parent.arrGlobal.get(pageLoadParams.entityName);
    if (!isValidData(pageLoadParams)) return;
    templateDownload(pageLoadParams);
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
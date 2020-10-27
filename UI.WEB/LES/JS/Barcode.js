///默认入口方法add实体名();
function publicInit(entityName, tableName) {
    ///校验参数有效性
    if (!isValidData(entityName)) return;
    ///获取选中项
    var row = $("#" + tblGridId).datagrid('getSelected');
    if (isValidData(row)) {
        ///先根据配置获取配置相关的KEY值
        if (isValidData(pageLoadParams.tableKeyField)) {
            var arrayKeyFields = pageLoadParams.tableKeyField.split("|");
            pageLoadParams.formDataKey = '';
            for (var i = 0; i < arrayKeyFields.length; i++) {
                pageLoadParams.formDataKey += '|' + eval("row." + arrayKeyFields[i]);
            }
            if (pageLoadParams.formDataKey.length > 1) {
                pageLoadParams.formDataKey = pageLoadParams.formDataKey.substring(1);
            }
        }
        ///如果未获取到KEY值首先认为是ID（long）
        if (!isValidData(pageLoadParams.formDataKey)) {
            pageLoadParams.formDataKey = row.Id;
            pageLoadParams.formDataKeyLength = '64';
        }
        ///其次认为是NID（int）
        if (!isValidData(pageLoadParams.formDataKey)) {
            pageLoadParams.formDataKey = row.Nid;
            pageLoadParams.formDataKeyLength = '32';
        }
    }
    ///
    var isPublicCreate = entityName.substring(entityName.length - 1) == "_" ? true : false;
    entityName = isPublicCreate ? entityName.substring(0, entityName.length - 1) : entityName;
    var defaultMedthodName = "create" + entityName;
    ///页面参数对象
    var entityInfo = arrTempMap.get(entityName);
    ///
    if (!isValidData(entityInfo)) entityInfo = new PageEntity();
    entityInfo.entityName = entityName;
    if (!isValidData(entityInfo.tableName))
        entityInfo.tableName = tableName;
    entityInfo.parentEntityName = pageLoadParams.entityName;

    ///
    entityInfo.AjaxActionOrEntityData('all');
    ///2018-4-12新增FORM编辑项
    entityInfo.formParamRowsData = pageLoadParams.formParamRowsData;
    entityInfo.formDataKey = pageLoadParams.formDataKey;

    arrTempMap.put(entityName, entityInfo);
    parent.arrGlobal.put(entityName, entityInfo);
        ///普通编辑页面需要有KEY值
        if (!isValidData(pageLoadParams.formDataKey)) {
            
                tAlert('pageIframe'
                    , entityName =='pickedup'?'拣配编辑':'扫描编辑'
                    , 'CommonEdit.aspx?' + entityName + "&" + defaultMedthodName
                    , entityInfo.editFormWidth
                    , entityInfo.editFormHeight);
           
        }
        if (isValidData(pageLoadParams.formEntityInfo)) {
            var tempParentField = pageLoadParams.formEntityInfo.ParentField;
            if (isValidData(tempParentField)) {
                var tempItems = tempParentField.split('-');
                if (tempItems.length > 1) {
                    entityInfo.formDataKey = eval("row." + tempItems[0]);
                    entityInfo.formParamKey = tempItems[1];
                    entityInfo.formParamRowsData = row;
                }
            }
        }
        ///不是默认方法直接调用弹窗
        tAlert('pageIframe'
            , entityName == 'pickedup' ? '拣配编辑' : '扫描编辑'
            , 'CommonEdit.aspx?' + entityName + "&" + defaultMedthodName + '&' + tableName
            , entityInfo.editFormWidth
            , entityInfo.editFormHeight);
   
}


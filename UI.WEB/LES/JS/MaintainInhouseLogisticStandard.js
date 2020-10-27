function partsBox() {
    ///仓库代码
    var wmNo = $('#20-SWmNo').combobox('getValue');
    if (!isValidData(wmNo)) return;
    ///存储区代码
    var zoneNo = $('#20-SZoneNo').combobox('getValue');
    if (!isValidData(zoneNo)) return;
    ///拉动零件类
    var inhousePartClass = $('#20-InhousePartClass').combobox('getValue');
    if (!isValidData(inhousePartClass)) return;

    var dataParams = 'method=ajaxpartsBox'
        + '&INHOUSE=' + inhousePartClass
        + '&ZONE=' + zoneNo
        + '&WM=' + wmNo;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "js:resultPartsBox", true);
}
function resultPartsBox(data) {
    if (!isValidData(data)) return;
    var partsBox = eval('(' + data + ')');
    $('#40-SupplierNum').combogrid('setValue', partsBox.SupplierNum);
    if (!isValidData(partsBox.SupplierNum))
        $('#10-SupplierName').textbox('setValue', "");
}



///JS-005创建控件
function createControl(acceptObj///表单容器ID
    , tId///控件ID
    , tText///控件标题
    , tType///控件类型
    , regexExp///正则表达式
    , tWidth///控件宽度
    , tHeight///控件高度
    , tIconType
    , extend///Extend3
    , codeName///Extend1，系统代码
    , tDefaultVal///默认值
    , readonly///是否只读
    , arrControlHelp///帮助说明
    , parentRowsData
    , intervalWidth///标题显示宽度
    , authExtend///Extend2，权限
    , regexErrorMsg///正则表达式验证不通过错误提示
    , nullEnable///是否允许为空
    , minValue///数值最小值
    , maxValue///数值最大值
    , dataLength///数据长度
    , precision)///数值小数精度
{

    ///默认控件宽度、高度
    tWidth = isValidData(tWidth) ? tWidth : 120;
    tHeight = isValidData(tHeight) ? tHeight : 22;
    ///控件标题，默认为140
    intervalWidth = isValidData(intervalWidth) ? intervalWidth : 140;
    var dText = "<div style='margin-top: 2px; width:" + intervalWidth + "px; float:left;'><span style='margin-left:10px; margin-right:10px;font-family:微软雅黑;'>" + tText + ":</span></div>";
    ///当检索条件控件被设置为多行文本框时
    if (tType + '' == '90' && tId.indexOf('-S-') > -1) {
        var bId = 'B-' + tId;
        var bHref = $("<div style='margin-top: 2px; width:" + intervalWidth + "px; float:left;'><a id='" + bId + "' href='javascript:void(0)' style='text-align:left;margin-left:5px; margin-right:10px;font-family:微软雅黑;'></a></div>");
        $('#' + acceptObj).append(bHref);
        ///注册click事件
        $('#' + bId).bind('click', function () {
            var mtId = this.id.substring(2);
            var mtText = $('#' + mtId).textbox('getText');
            var bText = this.text;
            var ds = parent.$('#dialogMulText');
            var dcontent = '<input class="easyui-textbox" data-options="multiline:true" value=\"' + mtText + '\" style="width:100%;height:99%">';
            var tlYes = languageMessageData('1x00000046');///确定
            var tlClear = languageMessageData('1x00000047');///清空
            var tlNo = languageMessageData('1x00000048');///取消
            ds.dialog({
                title: bText, width: 480, height: 320, closed: false, cache: false, modal: true, content: dcontent,
                buttons: [{
                    text: tlYes, iconCls: 'icon-back',
                    handler: function () {
                        var dgContent = $($($($(parent.$('#dialogMulText').html()).get(1)).html()).get(1)).val().replace(/\n/g, ',');
                        dgContent = splitRemoveEmpty(dgContent, ',');
                        $('#' + mtId).textbox('setText', dgContent);
                        parent.$('#dialogMulText').html('');
                        parent.$('#dialogMulText').dialog('close');
                    }
                },
                {
                    text: tlClear, iconCls: 'icon-no', handler: function () {
                        $('#' + mtId).textbox('setText', '');
                        parent.$('#dialogMulText').html('');
                        parent.$('#dialogMulText').dialog('close');
                    }
                },
                {
                    text: tlNo, iconCls: 'icon-remove', handler: function () {
                        parent.$('#dialogMulText').html('');
                        parent.$('#dialogMulText').dialog('close');
                    }
                }]
            });
        });
        ///指定为按钮
        $('#' + bId).linkbutton({
            iconCls: 'icon-search',
            plain: true,
            text: tText,
            width: intervalWidth,
            height: 17,
            iconAlign: 'right'
        });
    }
    else
        $('#' + acceptObj).append(dText);
    ///控件容器
    var controlContainer = tType == 120 ? $("<div name='submitColumn' style='height:300px' ></div>") : $("<input name='submitColumn'/>");
    controlContainer.attr('id', tId);
    controlContainer.attr('req', regexExp);
    controlContainer.attr('des', tText);
    ///富文本框的特殊处理
    if (tType == 120) {
        var richTextDiv = $("<div style='width:" + tWidth + "px;margin:0 auto;position:relative;'></div>");
        richTextDiv.attr('id', "editContainer-" + tId);
        $('#' + acceptObj).append(richTextDiv);
        $('#' + "editContainer-" + tId).append(controlContainer);
    } else {
        $('#' + acceptObj).append(controlContainer);
    }
    ///是否允许为空
    nullEnable = isValidData(nullEnable) ? nullEnable : false;
    var required = (nullEnable ? false : true);
    ///不允许空时的提示信息
    var missingMessage = '';
    if (required) {
        missingMessage = XR.language ? 'Not Null' : '必填项';
    }
    ///是否只读
    var tReadonly = isValidData(readonly) ? readonly : false;
    ///控件类型
    var ctlType = tType;
    ///数据最大长度，0为不校验长度
    dataLength = isValidData(dataLength) ? dataLength : 0;
    ///正则表达式报错信息
    regexErrorMsg = isValidData(regexErrorMsg) ? regexErrorMsg : '';
    if (dataLength > 0) {
        regexErrorMsg += (XR.language ? ' or Data Length Over ' + dataLength + '' : ' 或 数据长度超过' + dataLength + '位');
    }
    ///当默认值的起始字符为#时，需要获取父行项中的属性
    if (isValidData(tDefaultVal) && (tDefaultVal + '').indexOf('#') > -1) {
        tDefaultVal = eval('parentRowsData.' + tDefaultVal.replace('#', ''));
    }
    ///分类创建控件
    switch (ctlType + "") {
        case "10":///textbox
        case "90":///textmul
            ///是否多行TEXTBOX，多行文本默认高度80px
            var mulText = ctlType == "90" ? true : false;
            ///检索时始终不显示多行文本框
            mulText = tId.indexOf('-S-') > -1 ? false : mulText;
            if (mulText) {
                if (isValidData(maxValue))
                    tHeight = maxValue;
                else
                    tHeight = 80;
            }

            ///生成textbox
            $('#' + tId).textbox({
                required: required,
                width: tWidth,
                value: tDefaultVal,
                multiline: mulText,
                disabled: tReadonly,
                height: tHeight,
                missingMessage: missingMessage
            });
            ///如果设置了正则表达式，则需要添加到控件
            if (isValidData(regexExp)) {
                $('#' + tId).textbox({
                    ///后台用正则表达式校验
                    validType: 'remote[\'' + XR.defaultProcessUrl() + 'method=getRegexResult&AN=BLL.SYS&DATA_LENGTH=' + dataLength + '&REG=' + encodeURIComponent(regexExp) + '\',\'value\']',
                    ///校验失败后的报错信息
                    invalidMessage: regexErrorMsg
                });
            }
            break;
        case "20":///combobox
        case "80":///combocode
        case "100":///checkbox
            var valueFieldName = "id";
            var textFieldName = "text";
            ///传统checkbox为勾选，系统中默认为是|否combocode
            if (ctlType + '' == '100') {
                codeName = 'BOOLEAN';
            }
            ///获取数据的URL地址
            var cboUrl = "";
            ///获取数据的参数
            var dataParams = "";
            ///是否显示图标
            var isShowItemIcon = false;
            ///
            var isExtendJsonData = false;
            var ExtendJsonDataName = "";
            ///联动子控件配置集合
            var linkageControlJson = [];
            ///是否多选，默认单选
            var isMultiple = false;
            ///主控件配置信息
            var configJson;
            ///CODE
            if (isValidData(codeName)) {
                valueFieldName = "ItemValue";
                textFieldName = "ItemDisplay";
                cboUrl = XR.defaultProcessUrl()
                    + 'method=ajaxCodeList'
                    + '&CODE_NAME=' + codeName;
                tDefaultVal = getFormatterComboxValue(tDefaultVal, codeName);
            }
            ///EXTEND3
            if (isValidData(extend)) {
                ///sql^json^
                var paramItem = extend.split('^');
                ///数据是否合法
                if (paramItem.length > 1) {
                    ///SQL
                    if (paramItem[0] == "sql") {
                        var paramValueItem = paramItem[1];
                        ///联动控件分隔符，从第二段开始为多个联动控件
                        var paramControlItem = paramValueItem.split("|");
                        var linkageControls;
                        if (paramControlItem.length > 1) {
                            linkageControls = paramValueItem.replace(paramControlItem[0] + '|', '').split('|');
                            ///联动控件
                            for (var i = 0; i < linkageControls.length; i++) {
                                ///配置转换为JSON对象
                                var linkageConfigJson = GetComboExtendJson(linkageControls[i]);
                                linkageControlJson.push(linkageConfigJson);
                            }
                        }
                        configJson = GetComboExtendJson(paramControlItem[0]);
                        ///2018-04-17如已落实了数据源为CODE，则此处不需要再进行赋值
                        if (!isValidData(codeName)) {
                            ///赋予绑定项
                            valueFieldName = configJson.idField;
                            textFieldName = configJson.textField;
                            ///是否多选
                            isMultiple = configJson.isMultiple == 'true' ? true : false;
                            ///URL
                            cboUrl = GetDataUrlByExtendJson(configJson, parentRowsData);
                            ///选项特殊处理
                            tDefaultVal = getFormatterComboxValue(tDefaultVal, null);
                        }
                    }
                    ///JSON，可以进行扩展
                    else if (paramItem[0] == 'json') {
                        isExtendJsonData = true;
                        ExtendJsonDataName = paramItem[1].split(',')[0];
                        if (paramItem[1].split(',').length > 1)
                            isShowItemIcon = paramItem[1].split(',')[1] == 'true' ? true : false;
                    }
                }
            }

            ///扩展JSON
            if (isExtendJsonData) {
                $.getJSON('JS/data/common.data.json', function (data) {
                    var jsonData = eval("data." + ExtendJsonDataName);
                    $('#' + tId).combobox({
                        method: 'get',
                        required: required,
                        width: tWidth,
                        disabled: tReadonly,
                        showItemIcon: isShowItemIcon,
                        valueField: valueFieldName,
                        textField: textFieldName,
                        data: jsonData,
                        multiple: isMultiple,
                        missingMessage: missingMessage
                    });
                    if (isValidData(tDefaultVal)) {
                        $('#' + tId).combobox('setValue', tDefaultVal);
                    }
                });
            }
            else {
                ///权限过滤条件，权限过滤条件也可以在复杂条件中实现
                var authFilters = getDataAuthByEntityAuthConfig(authExtend);
                if (isValidData(authFilters)) {
                    cboUrl += "&AUTH_FILTER=" + filterEncodeURIComponent(authFilters);
                }
                var artChanged = false;
                var selectRow = null;
                var itemCount = 0;
                ///生成combobox
                $('#' + tId).combobox({
                    method: 'post',
                    width: tWidth,
                    editable: false,
                    required: required,
                    disabled: tReadonly,
                    valueField: valueFieldName,
                    textField: textFieldName,
                    icons: [{
                        iconCls: 'icon-clear'
                    }],
                    url: cboUrl,
                    multiple: isMultiple,
                    missingMessage: missingMessage,
                    onSelect: function (record) {
                        if (isMultiple) return;
                        selectRow = record;
                        ///逻辑联动
                        if (isValidData(configJson) && isValidData(configJson.linkageAttribute) && isValidData(record)) {
                            var logicResult = eval('record.' + configJson.linkageAttribute + configJson.linkageLogic + configJson.linkageCompareValue);
                            if (logicResult + '' == 'false') return;
                        }
                        ///联动控件
                        for (var i = 0; i < linkageControlJson.length; i++) {
                            var linkageConfigJson = linkageControlJson[i];
                            ///URL
                            var subcboUrl = GetDataUrlByExtendJson(linkageConfigJson, parentRowsData, record);
                            ///2018-4-18实测转码后无法提交到后台
                            ///subcboUrl = filterEncodeURIComponent(subcboUrl);
                            ///控件名称
                            var ctlBindName;

                            var re = /-[A-Z]-/;
                            if (re.test(tId)) {
                                if (tId[tId.indexOf('-') + 1] == "D") {
                                    ctlBindName = linkageConfigJson.controlType + "-D-" + linkageConfigJson.attributeName;
                                }
                                else if (tId[tId.indexOf('-') + 1] == "S") {
                                    ctlBindName = linkageConfigJson.controlType + "-S-" + linkageConfigJson.attributeName;
                                }
                            }
                            else {
                                ctlBindName = linkageConfigJson.controlType + "-" + linkageConfigJson.attributeName;
                            }
                            switch (linkageConfigJson.controlType + '') {
                                case '10':///单行文本框
                                case '70':///数字文本框
                                case '90':///多行文本框
                                    if (isValidData(linkageConfigJson.dataAttributeName) && isValidData(record)) {
                                        var displayValue = eval("record." + linkageConfigJson.dataAttributeName);
                                        displayValue = displayValue.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t')
                                        if (!isValidData(displayValue)) displayValue = '';
                                        eval("$('#" + ctlBindName + "').textbox('setValue','" + displayValue + "')");
                                    }
                                    if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                        eval("$('#" + ctlBindName + "').textbox('disable');");
                                    break;
                                case '20':///combobox
                                case '80':///combocode 
                                case '100':///checkbox
                                    if (isValidData(linkageConfigJson.dataAttributeName) && isValidData(record)) {
                                        $('#' + ctlBindName).combobox('clear', "none");
                                        $('#' + ctlBindName).combobox('reload', subcboUrl);
                                    }
                                    if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                        eval("$('#" + ctlBindName + "').combobox('disable');");
                                    break;
                                case '40':///combogrid
                                    ///判断控件是否存在
                                    if ($('#' + ctlBindName).length > 0) {
                                        ///获取当前控件选定值
                                        var sdv = $('#' + ctlBindName).combogrid('getValue');
                                        ///清空已加载项
                                        $('#' + ctlBindName).combogrid('clear', "none");
                                        ///2018-4-10修改了COMBOGRID在被联动时无法分页加载的BUG
                                        subcboUrl = subcboUrl.replace('ajaxControlToCombox', 'ajaxTables')
                                        ///重新加载项
                                        $('#' + ctlBindName).combogrid("grid").datagrid('reload', subcboUrl);
                                        if (isValidData(sdv)) {
                                            $('#' + ctlBindName).combogrid('setValue', sdv);
                                        }
                                        if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag + 'true')
                                            $('#' + ctlBindName).combogrid('disable');
                                    }
                                    break;
                                default: break;
                            }
                        }
                        ///数据变化后函数调用
                        if (isValidData(configJson) && isValidData(configJson.changeMethodName)) {
                            eval(configJson.changeMethodName);
                        }
                    },
                    onLoadSuccess: function (data) {
                        itemCount = data.length;
                        if (isMultiple) return;
                        ///2017-09-07修正错误：首次加载combobox时setValue不触发onSelect
                        ///获取当前选中value
                        var sv = $(this).combobox('getValue');
                        ///如果默认选中项不为空[且]当前没有选中项时
                        ///设置默认选中项为当前选中项，且调用select方法进行赋值，可以触发onSelect事件
                        if (isValidData(tDefaultVal) && !isValidData(sv)) {
                            $(this).combobox('select', tDefaultVal);
                        }
                        ///如果无选中项且字段必选且只有一项，则默认选中
                        if (!isValidData(sv) && itemCount == 1 && required) {
                            $(this).combobox('select', eval('data[0].' + valueFieldName));
                        }
                    },
                    onChange: function (newValue, oldValue) {
                        artChanged = true;
                        ///从无到有时，需要产生联动
                        //if (!isValidData(oldValue) && isValidData(newValue)) {
                        //    $(this).combobox('select', newValue);
                        //}
                    },
                    onShowPanel: function () {
                        // 动态调整高度  
                        if (itemCount < 10) {
                            $(this).combobox('panel').height("auto");
                        } else {
                            $(this).combobox('panel').height(200);
                        }
                    },
                    onHidePanel: function () {
                        if (isMultiple) return;
                        ///获取当前控件中的值，如果是用户手动修改且不在选项中，则获得为界面显示内容
                        var cv = $(this).combobox('getValue');
                        ///由onChange判定内容是否产生变化
                        if (artChanged) {
                            ///如果没有再次的选中项 or 当前控件内容或值和选中项不符时
                            if (selectRow == null || cv != eval("selectRow." + valueFieldName)) {
                                ///将控件中内容或值清空
                                $(this).combobox('setValue', '');
                                selectRow = null;
                            }
                        }
                        artChanged = false;
                    }
                });
                ///非只读时给于一个清空按钮，方便用户操作
                if (!tReadonly) {
                    var clearObj = $('a.textbox-icon.icon-clear');
                    clearObj[clearObj.length - 1].setAttribute('name', tId + '-clear');
                    clearObj[clearObj.length - 1].setAttribute('onclick', "comboxIconClear(this)");
                }
                if (isMultiple) {
                    var arrayDefaultValues = [];
                    if (isValidData(tDefaultVal)) {
                        arrayDefaultValues = tDefaultVal.split(',');
                    }
                    $('#' + tId).combobox('setValues', arrayDefaultValues);
                }
            }
            break;
        case "30":///combotree
            if (!isValidData(extend)) break;
            var paramItem = extend.split('^');
            if (paramItem.length < 2) break;
            var paramType = paramItem[0];
            var paramValue = paramItem[1];
            if (paramType != "sql") break;
            ///
            var linkageControlJson = [];
            ///
            var paramControlItem = paramValue.split("|");
            var linkageControls;
            if (paramControlItem.length > 1) {
                linkageControls = paramValue.replace(paramControlItem[0] + '|', '').split('|');
                ///联动控件
                for (var i = 0; i < linkageControls.length; i++) {
                    ///配置转换为JSON对象
                    var linkageConfigJson = GetComboExtendJson(linkageControls[i]);
                    linkageControlJson.push(linkageConfigJson);
                }
            }
            var configJson = GetComboExtendJson(paramControlItem[0]);
            ///是否多选
            var isMultiple = configJson.isMultiple == 'true' ? true : false;
            ///URL
            cboUrl = GetDataUrlByExtendJson(configJson, parentRowsData)
            ///AUTH_FILTER
            var authFilters = getDataAuthByEntityAuthConfig(authExtend);
            if (isValidData(authFilters)) {
                cboUrl += '&AUTH_FILTER=' + filterEncodeURIComponent(authFilters);
            }
            ///
            $.getJSON(cboUrl, function (data) {
                ///数据转换成树形结构JSON
                var treeData = arrayToTree(data, configJson.idField, configJson.parentId);
                treeData = JSON.stringify(treeData);
                treeData = eval("treeData.replace(/" + configJson.idField + "/g, 'id').replace(/" + configJson.textField + "/g, 'text')");
                treeData = eval('(' + treeData + ')');
                var selectRow = null;
                var artChanged = false;
                ///生成combotree
                $('#' + tId).combotree({
                    width: tWidth,
                    data: treeData,
                    disabled: tReadonly,
                    multiple: isMultiple,
                    required: required,
                    missingMessage: missingMessage,
                    onLoadSuccess: function (node, data) {
                        if (isMultiple) return;
                        ///2018-4-29这里的$(this)直接就是 $('#' + tId).combotree('tree')
                        var sv = $(this).tree('getSelected');
                        ///如果默认选中项不为空[且]当前没有选中项时
                        ///设置默认选中项为当前选中项，且调用select方法进行赋值，可以触发onSelect事件
                        if (isValidData(tDefaultVal) && !isValidData(sv)) {
                            var sn = $(this).tree('find', tDefaultVal);
                            if (isValidData(sn)) {
                                $(this).tree('select', sn.target);
                            }
                            $('#' + tId).combotree('setValue', tDefaultVal);
                        }
                        ///如果无选中项且字段必选且只有一项，则默认选中
                        if (!isValidData(sv) && data.length == 1 && required) {
                            var sn = $(this).tree('find', eval('data[0].id'));
                            $(this).tree('select', sn.target);
                            $('#' + tId).combotree('setValue', eval('data[0].id'));
                        }
                    },
                    onSelect: function (node) {
                        selectRow = node;
                        ///联动控件
                        for (var i = 0; i < linkageControlJson.length; i++) {
                            var linkageConfigJson = linkageControlJson[i];
                            ///URL
                            var subcboUrl = GetDataUrlByExtendJson(linkageConfigJson, parentRowsData, node);
                            ///控件名称
                            var ctlBindName = linkageConfigJson.controlType + "-" + linkageConfigJson.attributeName;
                            switch (linkageConfigJson.controlType + '') {
                                case '10':///单行文本框
                                case '70':///数字文本框
                                case '90':///多行文本框
                                    if (isValidData(linkageConfigJson.dataAttributeName) && isValidData(node)) {
                                        var displayValue = eval("node." + linkageConfigJson.dataAttributeName);
                                        displayValue = displayValue.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t')
                                        if (!isValidData(displayValue)) displayValue = '';
                                        eval("$('#" + ctlBindName + "').textbox('setValue','" + displayValue + "')");
                                    }
                                    if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                        eval("$('#" + ctlBindName + "').textbox('disable');");
                                    break;
                                case '20':///combobox
                                case '80':///combocode 
                                case '100':///checkbox
                                    $('#' + ctlBindName).combobox('clear', "none");
                                    $('#' + ctlBindName).combobox('reload', subcboUrl);
                                    break;
                                case '40':///combogrid
                                    ///判断控件是否存在
                                    if ($('#' + ctlBindName).length > 0) {
                                        ///获取当前控件选定值
                                        var sdv = $('#' + ctlBindName).combogrid('getValue');
                                        ///清空已加载项
                                        $('#' + ctlBindName).combogrid('clear', "none");
                                        ///2018-4-10修改了COMBOGRID在被联动时无法分页加载的BUG
                                        subcboUrl = subcboUrl.replace('ajaxControlToCombox', 'ajaxTables')
                                        ///重新加载项
                                        $('#' + ctlBindName).combogrid("grid").datagrid('reload', subcboUrl);
                                        if (isValidData(sdv)) {
                                            $('#' + ctlBindName).combogrid('setValue', sdv);
                                        }
                                    }
                                    break;
                                default: break;
                            }
                        }
                        ///数据变化后函数调用
                        if (isValidData(configJson) && isValidData(configJson.changeMethodName)) {
                            eval(configJson.changeMethodName);
                        }
                    },
                    onChange: function (newValue, oldValue) {
                        artChanged = true;
                        ///从无到有时，需要产生联动
                        if (!isValidData(oldValue) && isValidData(newValue)) {
                            var sn = $(this).combotree('tree').tree('find', newValue);
                            $(this).combotree('tree').tree('select', sn.target);
                        }
                    },
                    onHidePanel: function () {
                    }
                });
                if (isMultiple) {
                    var arrayDefaultValues = [];
                    if (isValidData(tDefaultVal)) {
                        arrayDefaultValues = tDefaultVal.split(',');
                    }
                    $('#' + tId).combotree('setValues', arrayDefaultValues);
                }
            });
            break;
        case "40":///combogrid
            if (!isValidData(extend)) break;
            ///GRID宽度
            var grid_width = 0;
            ///
            var paramItem = extend.split('^');
            if (paramItem.length < 2) break;
            ///
            var paramType = paramItem[0];
            var paramValue = paramItem[1];
            ///联动子控件配置集合
            var linkageControlJson = [];
            ///处理特殊字符
            tDefaultVal = getFormatterComboxValue(tDefaultVal, null);
            ///combogrid只支持sql方式
            if (paramType != "sql") break;
            ///联动控件分隔符，从第二段开始为多个联动控件
            var paramControlItem = paramValue.split("|");
            var linkageControls;
            if (paramControlItem.length > 1) {
                linkageControls = paramValue.replace(paramControlItem[0] + '|', '').split('|');
                ///联动控件
                for (var i = 0; i < linkageControls.length; i++) {
                    ///配置转换为JSON对象
                    var linkageConfigJson = GetComboExtendJson(linkageControls[i]);
                    linkageControlJson.push(linkageConfigJson);
                }
            }
            var configJson = GetComboExtendJson(paramControlItem[0]);
            ///是否多选
            var isMultiple = configJson.isMultiple == 'true' ? true : false;
            ///多选checkbox
            var ck = '';
            ///
            if (isMultiple) {
                ck = '{ "field":"ck","checkbox":true },';
                grid_width += 30;
            }
            ///combogrid独有的多列显示
            var col = configJson.columns;
            var columnsItem = col.split("⊙");
            var columns = '';
            for (var j = 0; j < columnsItem.length; j++) {
                var colitems = columnsItem[j].split("-");
                var colitem_field = "'" + colitems[0] + "'";
                var colitem_title = "'" + colitems[1] + "'";
                var colitem_width = '';
                if (colitems.length > 2) {
                    colitem_width = colitems[2];
                    ///如果为0则视为无效宽度设置
                    if (colitem_width == '0')
                        colitem_width = '';
                }
                if (colitems.length > 3 && XR.language) {
                    colitem_title = "'" + colitems[3] + "'";
                }
                if (isValidData(colitem_width)) {
                    columns += "{field:" + colitem_field + ",title:" + colitem_title + ",width:" + colitem_width + "},";
                    grid_width += parseInt(colitem_width);
                }
                else
                    columns += "{field:" + colitem_field + ",title:" + colitem_title + ",fixed:true},";
            }
            columns = columns.length > 0 ? columns.substring(0, columns.length - 1) : columns;
            ///GRID宽度，至少450px
            if (grid_width < 450) grid_width = 450;
            ///
            var sortName = configJson.sortName;
            if (!isValidData(sortName)) {
                sortName = configJson.idField;
            }
            var sortOrder = configJson.sortOrder;
            if (!isValidData(sortOrder)) {
                sortOrder = "asc";
            }
            ///URL
            var cboGridUrl = GetDataUrlByExtendJson(configJson, parentRowsData);
            ///AUTH_FILTER
            var authFilters = getDataAuthByEntityAuthConfig(authExtend);
            if (isValidData(authFilters)) {
                cboGridUrl += '&AUTH_FILTER=' + filterEncodeURIComponent(authFilters);
            }

            var artChanged = false;
            var selectRow = null;
            var loaded = false;
            ///生成combogrid
            $('#' + tId).combogrid({
                url: cboGridUrl,
                width: tWidth,
                idField: configJson.idField,
                textField: configJson.textField,
                singleSelect: true,
                pagination: true,
                pageSize: 5,
                pageList: [5, 10, 20, 50],
                panelHeight: 185,
                panelWidth: grid_width,
                rownumbers: false,
                disabled: tReadonly,
                sortName: sortName,
                sortOrder: sortOrder,
                fitColumns: true,
                mode: "remote",
                multiple: isMultiple,
                required: required,
                missingMessage: missingMessage,
                columns: eval(" [[" + ck + columns + "]]"),
                onLoadSuccess: function (data) {
                    var i = cboGridUrl;
                    if (isMultiple) return;
                    ///获取当前选中value
                    var sv = $(this).combogrid('getValue');
                    ///如果默认选中项不为空[且]当前没有选中项时
                    ///设置默认选中项为当前选中项，且调用select方法进行赋值，可以触发onSelect事件
                    if (isValidData(tDefaultVal) && !isValidData(sv) && !loaded) {
                        $(this).combogrid('grid').datagrid('reload', { 'q': tDefaultVal });
                        //$(this).combogrid('grid').datagrid('selectRecord', tDefaultVal);
                        $(this).combogrid('setValue', tDefaultVal);
                        loaded = true;
                    }
                    ///如果无选中项且字段必选且只有一项，则默认选中
                    if (!isValidData(sv) && data.length == 1 && required) {
                        $(this).combogrid('setValue', eval('data[0].' + configJson.idField));
                    }
                },

                onSelect: function (index, row) {
                    if (isMultiple) return;
                    selectRow = row;
                    for (var i = 0; i < linkageControlJson.length; i++) {
                        var linkageConfigJson = linkageControlJson[i];
                        var subcboUrl = GetDataUrlByExtendJson(linkageConfigJson, parentRowsData, row);
                        ///控件名称
                        var ctlBindName;
                        var re = /-[A-Z]-/;
                        if (re.test(tId)) {
                            if (tId[tId.indexOf('-') + 1] == "D") {
                                ctlBindName = linkageConfigJson.controlType + "-D-" + linkageConfigJson.attributeName;
                            }
                            else if (tId[tId.indexOf('-') + 1] == "S") {
                                ctlBindName = linkageConfigJson.controlType + "-S-" + linkageConfigJson.attributeName;
                            }
                        }

                        else {
                            ctlBindName = linkageConfigJson.controlType + "-" + linkageConfigJson.attributeName;
                        }
                        switch (linkageConfigJson.controlType + '') {
                            case '10':///单行文本框
                            case '70':///数字文本框
                            case '90':///多行文本框
                                ///2018-04-17 当显示内容为null时需要更新为string.Empty
                                var displayValue = eval("selectRow." + linkageConfigJson.dataAttributeName);
                                if (!isValidData(displayValue)) displayValue = '';
                                if (linkageConfigJson.controlType != '70') {
                                    displayValue = displayValue.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t');
                                }
                                eval("$('#" + ctlBindName + "').textbox('setValue','" + displayValue + "')");
                                break;
                            case '20':///combobox
                            case '80':///combocode 
                            case '100':///checkbox
                                $('#' + ctlBindName).combobox('clear', "none");
                                $('#' + ctlBindName).combobox('reload', subcboUrl);
                                break;
                            case '40':///combogrid
                                ///判断控件是否存在
                                if ($('#' + ctlBindName).length > 0) {
                                    ///获取当前控件选定值
                                    var sdv = $('#' + ctlBindName).combogrid('getValue');
                                    ///清空已加载项
                                    $('#' + ctlBindName).combogrid('clear', "none");
                                    ///2018-4-10修改了COMBOGRID在被联动时无法分页加载的BUG
                                    subcboUrl = subcboUrl.replace('ajaxControlToCombox', 'ajaxTables')
                                    ///重新加载项
                                    $('#' + ctlBindName).combogrid("grid").datagrid('reload', subcboUrl);
                                    if (isValidData(sdv)) {
                                        $('#' + ctlBindName).combogrid('setValue', sdv);
                                    }
                                }
                                break;
                            default: break;
                        }
                    }
                    ///数据变化后函数调用
                    if (isValidData(configJson) && isValidData(configJson.changeMethodName)) {
                        eval(configJson.changeMethodName);
                    }
                },
                onChange: function (newValue, oldValue) {
                    artChanged = true;
                    ///从无到有时，需要产生联动
                    if (!isValidData(oldValue) && isValidData(newValue)) {
                        $(this).combogrid('grid').datagrid('selectRecord', newValue);
                    }
                },
                onHidePanel: function () {
                    ///多选
                    if (isMultiple) return;
                    ///获取当前控件中的值，如果是用户手动修改且不在选项中，则获得为界面显示内容
                    var cv = $(this).combogrid('getValue');
                    ///由onChange判定内容是否产生变化
                    if (artChanged) {
                        ///如果没有再次的选中项 or 当前控件内容或值和选中项不符时
                        if (selectRow == null || cv != eval("selectRow." + configJson.idField)) {
                            ///将控件中内容或值清空
                            $(this).combogrid('grid').datagrid('reload', { 'q': '' });
                            $(this).combogrid('grid').datagrid('unselectAll');
                            selectRow = null;
                        }
                    }
                    artChanged = false;
                },
                onShowPanel: function () {
                    ///多选
                    if (isMultiple) return;
                    $(this).combogrid('grid').datagrid('reload', { 'q': '' });
                    $(this).combogrid('grid').datagrid('unselectAll');
                    var arr = cboGridUrl.split('&');
                    var array = new Map();
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].split('=').length > 0)
                            array.put(arr[i].split('=')[0], arr[i].split('=')[1])
                    }
                    if (array.get('ENTITY_NAME') == "SupplierPartQuota") {
                        if (isMultiple) return;
                        var supplierNum = $('#40-SupplierNum').combogrid('getValue');
                        var partNo = $('#40-PartNo').combogrid('getValue');
                        var wmNo = $('#20-TWmNo').combogrid('getValue');
                        var subcboUrl = "";
                        for (var j = 0; j < array.key.length; j++) {
                            if (array.key[j] == "FILTER") {
                                if ($('#80-InhouseSystemMode').numberbox('getValue') == "20") {
                                    array.keys[j] = 'and PART_NO = ^' + partNo + '^ ';
                                }
                                else
                                    array.keys[j] = 'and PART_NO = ^' + partNo + '^ and [SUPPLIER_NUM] = ^' + supplierNum + '^';
                            }
                            subcboUrl += array.key[j] + "=" + array.keys[j];
                            if (j < array.key.length - 1)
                                subcboUrl += "&";
                        }
                        subcboUrl += "&FILTERS=" + wmNo;
                        ///控件名称
                        var ctlBindName = "#40-InboundPackageModel";
                        var re = /-[A-Z]-/;
                        ///判断控件是否存在
                        if ($(ctlBindName).length > 0) {
                            ///获取当前控件选定值
                            var sdv = $(ctlBindName).combogrid('getValue');
                            ///清空已加载项
                            $(ctlBindName).combogrid('clear', "none");
                            ///2018-4-10修改了COMBOGRID在被联动时无法分页加载的BUG
                            subcboUrl = subcboUrl.replace('ajaxTables', 'ajaxSupplierPartQuota')
                            ///重新加载项
                            $(ctlBindName).combogrid("grid").datagrid('reload', subcboUrl);
                            if (isValidData(sdv)) {
                                $(ctlBindName).combogrid('setValue', sdv);
                            }
                        }

                        ///数据变化后函数调用
                        if (isValidData(configJson) && isValidData(configJson.changeMethodName)) {
                            eval(configJson.changeMethodName);
                        }


                    }
                }

            });
            if (isMultiple) {
                var arrayDefaultValues = [];
                if (isValidData(tDefaultVal)) {
                    arrayDefaultValues = tDefaultVal.split(',');
                }
                $('#' + tId).combogrid('setValues', arrayDefaultValues);
            }
            break;
        case "50":///datebox
            if (tDefaultVal == 'now')
                tDefaultVal = new Date().toLocaleDateString();
            var dateformat = XR.language ? "MM/dd/yyyy" : "yyyy-MM-dd";
            var configJson;
            if (isValidData(codeName)) {
                configJson = GetComboExtendJson(codeName);
                if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                    dateformat = configJson.datetimeFormat;
            }
            tDefaultVal = FORM.formatDateByJson(tDefaultVal, dateformat);
            //指定为
            $('#' + tId).datebox({
                required: required,
                disabled: tReadonly,
                value: tDefaultVal,
                width: tWidth,
                missingMessage: missingMessage
            });
            break;
        case "60":///datetimebox
            if (tDefaultVal == 'now')
                tDefaultVal = new Date().toLocaleTimeString();
            var dateformat = XR.language ? "MM/dd/yyyy HH:mm:ss" : "yyyy-MM-dd HH:mm:ss";
            ///JSON格式配置
            var configJson;
            var showSeconds;
            if (isValidData(codeName)) {
                configJson = GetComboExtendJson(codeName);
                if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                    dateformat = configJson.datetimeFormat;
                if (isValidData(configJson) && isValidData(configJson.showSeconds))
                    showSeconds = configJson.showSeconds == 'true' ? true : false;
            }
            ///
            tDefaultVal = FORM.formatDateByJson(tDefaultVal, dateformat);
            //指定为
            $('#' + tId).datetimebox({
                required: required,
                missingMessage: missingMessage,
                showSeconds: showSeconds,
                disabled: tReadonly,
                value: tDefaultVal,
                width: tWidth
            });
            break;
        case '130':///timespinner
            if (tDefaultVal == 'now')
                tDefaultVal = new Date().toLocaleTimeString();
            var dateformat = "H:mm";
            ///JSON格式配置
            var configJson;
            var showSeconds;
            var timeSeparator;
            if (isValidData(codeName)) {
                configJson = GetComboExtendJson(codeName);
                if (isValidData(configJson) && isValidData(configJson.showSeconds))
                    showSeconds = configJson.showSeconds == 'true' ? true : false;
            }
            if (showSeconds)
                dateformat = "H:mm:ss";
            ///
            tDefaultVal = FORM.formatDateByJson(tDefaultVal, dateformat);
            //指定为
            $('#' + tId).timespinner({
                required: required,
                missingMessage: missingMessage,
                showSeconds: showSeconds,
                disabled: tReadonly,
                value: tDefaultVal,
                width: tWidth
            });
            break;
        case "70":
            if (!isValidData(minValue)) minValue = Number.MIN_VALUE;
            if (!isValidData(maxValue)) maxValue = Number.MAX_VALUE;
            //指定为数值输入框
            $('#' + tId).numberbox({
                value: tDefaultVal,
                disabled: tReadonly,
                min: minValue,// number 允许的最小值。  
                max: maxValue,// number 允许的最大值。 null 
                precision: precision,// number 在十进制分隔符之后显示的最大精度。（即小数点后的显示精度） 0 
                //decimalSeparator: '.',// string 使用哪一种十进制字符分隔数字的整数和小数部分。 . 
                //groupSeparator: ',',// string 使用哪一种字符分割整数组，以显示成千上万的数据。(比如：99,999,999.00中的','就是该分隔符设置。)  
                prefix: codeName,// string 前缀字符。(比如：金额的$或者￥)  
                suffix: authExtend,// string 后缀字符。(比如：后置的欧元符号€) 
                width: tWidth,
                required: required,
                missingMessage: missingMessage
            });
            if (isValidData(extend)) {
                $('#' + tId).numberbox({
                    onChange: function (newValue, oldValue) {
                        var configItems = extend.split('^');
                        ///联动子控件配置集合
                        var paramsJson = [];
                        ///配置JSON
                        var configJson;
                        ///是否数学计算
                        if (configItems[0] == 'math') {
                            var paramItems = configItems[1].split('|');
                            var formular = '';
                            for (var i = 0; i < paramItems.length; i++) {
                                if (i == 0) {
                                    configJson = GetComboExtendJson(paramItems[0]);
                                    formular = configJson.formular;
                                    continue;
                                }
                                configJson = GetComboExtendJson(paramItems[i]);
                                paramsJson.push(configJson);
                            }
                            ///是否有效公式
                            if (!isValidData(formular)) return;
                            ///结果变量
                            var resultParam = formular.split('=')[0];
                            ///计算公式，多个变量，包含计算符
                            var calFormular = formular.split('=')[1];
                            for (var i = 0; i < paramsJson.length; i++) {
                                ///变量
                                var paramStr = paramsJson[i].formularParam;
                                if (paramStr == resultParam) {
                                    var re = /-[A-Z]-/;
                                    if (re.test(tId)) {
                                        if (tId[tId.indexOf('-') + 1] == "D") {
                                            resultParam = '#' + paramsJson[i].controlType + '-D-' + paramsJson[i].attributeName;
                                        }
                                        else if (tId[tId.indexOf('-') + 1] == "S") {
                                            resultParam = '#' + paramsJson[i].controlType + '-D-' + paramsJson[i].attributeName;
                                        }
                                    }
                                    else {
                                        resultParam = '#' + paramsJson[i].controlType + '-' + paramsJson[i].attributeName;
                                    }
                                    continue;
                                }
                                var calParam;
                                var re = /-[A-Z]-/;
                                if (re.test(tId)) {
                                    if (tId[tId.indexOf('-') + 1] == "D") {
                                        calParam = $('#' + paramsJson[i].controlType + '-D-' + paramsJson[i].attributeName).numberbox('getValue');
                                        if (paramsJson[i].validNotZero == 'true' && calParam == 0) return;
                                    }
                                    else if (tId[tId.indexOf('-') + 1] == "S") {
                                        calParam = $('#' + paramsJson[i].controlType + '-S-' + paramsJson[i].attributeName).numberbox('getValue');
                                        if (paramsJson[i].validNotZero == 'true' && calParam == 0) return;
                                    }
                                }

                                else {
                                    calParam = $('#' + paramsJson[i].controlType + '-' + paramsJson[i].attributeName).numberbox('getValue');
                                    if (paramsJson[i].validNotZero == 'true' && calParam == 0) return;
                                }
                                if (!isValidData(calParam)) calParam = 0;
                                calFormular = calFormular.replace(paramStr, calParam);
                            }
                            ///计算
                            $(resultParam).numberbox('setValue', eval(calFormular));
                        }
                    }
                });
            }
            break;
        case "110":
            tText = languageMessageData('0x00000093'); //XR.language == true ? "Choose File" : "选择文件";
            $('#' + tId).filebox({
                buttonText: tText,///在文本框上附加的按钮显示的文本
                buttonIcon: 'icon-0000011',///在文本框上附加的按钮显示的图标
                buttonAlign: 'right',///附加按钮位置。可用值有："left", "right"
                accept: extend,///指定接受的文件类型
                multiple: false,///指定是否接受多文件选择
                separator: ',',///指定多个文件名称之间的分隔符
                width: tWidth
                , onChange: function (newValue, oldValue) {
                    verifyFileContentType();
                }
            });
            break;
        case "120":
            if (tId == "120-HelpContextCn") {
                editorZn = new wangEditor(tId);
                editorZn.config.uploadImgUrl = XR.defaultProcessUrl();
                editorZn.config.uploadParams = {
                    method: 'ajaxUpLoadFiles',
                    FILE_PATH: 'SYS.HELP',
                    ENTITY_NAME: 'HelpPhoto',
                    AN: 'BLL.SYS'
                }
                editorZn.create();
                if (tDefaultVal != "") {
                    editorZn.$txt.html(unescape(tDefaultVal));
                }
            } else if (tId == "120-HelpContextEn") {
                editorEn = new wangEditor(tId);
                editorEn.config.uploadImgUrl = XR.defaultProcessUrl();
                editorEn.config.uploadParams = {
                    method: 'ajaxUpLoadFiles',
                    FILE_PATH: 'SYS.HELP',
                    ENTITY_NAME: 'HelpPhoto',
                    AN: 'BLL.SYS'
                }
                editorEn.create();
                if (tDefaultVal != "") {
                    editorEn.$txt.html(unescape(tDefaultVal));
                }
            }
            break;
    }
};




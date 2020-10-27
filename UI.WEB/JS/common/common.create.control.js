///JS-001初始化页面容器
function createInitForm(formAction, formSearch) {
    ///CommonEdit.aspx中已有的form控件
    var body = document.getElementById("formItem");
    ///是否有动作，有则绘制div容器
    if (isValidData(formAction) && formAction != []) {
        ///ID = divActionMenuId 的 div
        var divActionMenu = document.createElement("div");
        divActionMenu.id = divActionMenuId;
        divActionMenu.className = "easyui-panel panel-body panel-body-noheader";
        divActionMenu.style.width = "100%";
        divActionMenu.style.marginBottom = "2px";
        body.appendChild(divActionMenu);
    }
    ///是否有检索条件，有则绘制div容器
    if (isValidData(formSearch) && formSearch != []) {
        ///ID = divSearchFormId 的 div
        var divSerchForm = document.createElement("div");
        divSerchForm.id = divSearchFormId;
        divSerchForm.className = "easyui-panel panel-body panel-body-noheader";
        divSerchForm.style.width = "100%";
        divSerchForm.style.marginTop = "2px";
        divSerchForm.style.marginBottom = "2px";
        divSerchForm.style.paddingTop = "2px";
        divSerchForm.style.paddingBottom = "2px";
        body.appendChild(divSerchForm);
    }
    ///是否为编辑页面
    if (pageLoadParams.pathName.lastIndexOf('CommonList.aspx') == -1 || pageLoadParams.formEntityInfo.ListpageEditFlag) {
        ///ID = divControlFormId 的 div
        var divControlForm = document.createElement("div");
        divControlForm.id = divControlFormId;
        divControlForm.className = "easyui-panel panel-body panel-body-noheader";
        divControlForm.style.width = "100%";
        divControlForm.style.marginTop = "2px";
        divControlForm.style.marginBottom = "2px";
        divControlForm.style.paddingTop = "2px";
        divControlForm.style.paddingBottom = "2px";
        divControlForm.style.overflow = 'hidden';
        divControlForm.style.borderColor = 'black';
        body.appendChild(divControlForm);
        if (isValidData(formAction)) {
            for (var i = 0; i < formAction.length; i++) {
                if (!formAction[i].DetailFlag) continue;
                ///ID = divActionDetail 的 div
                var divActionDetail = document.createElement("div");
                divActionDetail.id = divActionDetailId;
                divActionDetail.className = "easyui-panel panel-body panel-body-noheader";
                divActionDetail.style.width = "100%";
                divActionDetail.style.marginBottom = "2px";
                body.appendChild(divActionDetail);
                break;
            }
        }
    }

    ///ID = tblGridId 的 div
    var divGridForm = document.createElement("div");
    divGridForm.id = "divTableGridId";
    divGridForm.className = "easyui-panel panel-body panel-body-noheader";
    divGridForm.style.width = "100%";
    divGridForm.style.borderColor = 'white';
    body.appendChild(divGridForm);

    var tblGrid = document.createElement("div");
    tblGrid.id = tblGridId;
    tblGrid.style.width = "100%";
    var tblGridDiv = document.getElementById("divTableGridId");
    tblGridDiv.appendChild(tblGrid);
};
///JS-002判定用户登录状态
var isSessionNull = false;
function sessionIsNull(data) {
    isSessionNull = false;
    //session is null
    if (data == "Err_:SessionIsNull" || data == "\"Err_:SessionIsNull\"") {
        isSessionNull = true;
        var strTitle = languageMessageTitle('1x00000001');//提示
        var strMsg = languageMessageData('0x00000092');
        tAlert("error", strTitle, strMsg);
        setTimeout(function () {
            $.cookie.set('cookieUserRoleFid', "");
            if (parent != null) {
                parent.window.location = loginUrl;
            } else {
                window.location = loginUrl;
            }
        }, 1500);
        return true;
    }
    //仅错误信息
    var strData = JSON.stringify(data);
    if (strData.indexOf("Err_") != -1) {
        var errorData = strData.replace("Err_:", "");
        if (errorData.indexOf("MC:") != -1) {
            var dataMC = errorData.replace("MC:", "");
            tAlert('error', titleAlert, languageMessageData(dataMC), 500, 200);
        } else {
            tAlert('error', titleAlert, errorData, 500, 200);
        }
        closeMessagerProgress();
        return true;
    }
    return false;
}
///JS-003显示进度消息窗体
function openMessagerProgress(cMsg, cInterval) {
    var titlemsg = languageMessageData('0x00000116');
    if (isValidData(cMsg)) titlemsg = cMsg;
    var iInterval = 100;
    if (isValidData(cInterval)) iInterval = cInterval;
    $.messager.progress({
        msg: titlemsg,
        interval: iInterval
    });
}
///JS-004关闭进度消息窗体
function closeMessagerProgress() {
    $.messager.progress('close');
}

var editorZn;
var editorEn;

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
    var dText = "<div id='T-" + tId + "' style='margin-top: 2px; width:" + intervalWidth + "px; float:left;'><span style='margin-left:10px; margin-right:10px;font-family:微软雅黑;'>" + tText + ":</span></div>";
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
    ///帮助说明
    if (isValidData(arrControlHelp)) {
        $('#T-' + tId).tooltip({
            position: 'top',
            content: '<span style="color:#fff">' + arrControlHelp + '</span>',
            onShow: function () {
                $(this).tooltip('tip').css({
                    backgroundColor: '#666',
                    borderColor: '#666'
                });
            }
        });
    }
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
            ///json->string
            var sDefaultVal = (isValidData(tDefaultVal) ? tDefaultVal : '');
            if (typeof tDefaultVal == 'object' && isValidData(tDefaultVal))
                sDefaultVal = JSON.stringify(tDefaultVal);
            ///生成textbox
            $('#' + tId).textbox({
                required: required,
                width: tWidth,
                value: sDefaultVal,
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
                        ///联动父控件当前选中值
                        var psdv = $(this).combobox('getValue');
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
                            var subcboUrl = GetDataUrlByExtendJson(linkageConfigJson, parentRowsData, record, null, psdv);
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
                                        displayValue = displayValue.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t');
                                        if (!isValidData(displayValue)) displayValue = '';
                                        eval("$('#" + ctlBindName + "').textbox('setValue','" + displayValue + "')");
                                    }
                                    if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                        eval("$('#" + ctlBindName + "').textbox('disable');");
                                    break;
                                case '20':///combobox
                                case '100':///checkbox
                                    ///判断控件是否存在
                                    if ($('#' + ctlBindName).length > 0) {
                                        ///获取当前控件选定值
                                        var sdv = $('#' + ctlBindName).combobox('getValue');
                                        $('#' + ctlBindName).combobox('clear', "none");
                                        $('#' + ctlBindName).combobox('reload', subcboUrl);
                                        if (isValidData(sdv)) {
                                            $('#' + ctlBindName).combobox('select', sdv);
                                        }
                                        if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                            eval("$('#" + ctlBindName + "').combobox('disable');");
                                    }
                                    break;
                                case '80':///combocode
                                    if (isValidData(linkageConfigJson.dataAttributeName) && isValidData(record)) {
                                        var displayValue = eval("record." + linkageConfigJson.dataAttributeName);
                                        displayValue = displayValue.replace(/\n/g, '\\n').replace(/\r/g, '\\r').replace(/\f/g, '\\f').replace(/\t/g, '\\t');
                                        if (!isValidData(displayValue)) displayValue = '';
                                        eval("$('#" + ctlBindName + "').combocode('setValue','" + displayValue + "')");
                                    }
                                    if (isValidData(linkageConfigJson.readonlyFlag) && linkageConfigJson.readonlyFlag == 'true')
                                        eval("$('#" + ctlBindName + "').combocode('disable');");
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
                    if (!isValidData(sv) && data.rows.length == 1 && required) {
                        $(this).combogrid('setValue', eval('data.rows[0].' + configJson.idField));
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
                            case '100':///checkbox
                                $('#' + ctlBindName).combobox('clear', "none");
                                $('#' + ctlBindName).combobox('reload', subcboUrl);
                                break;
                            case '80':///combobox
                                var displayValue = eval("selectRow." + linkageConfigJson.dataAttributeName);
                                if (!isValidData(displayValue)) displayValue = '';
                                eval("$('#" + ctlBindName + "').combobox('setValue','" + displayValue + "')");
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
///JS-016绘制控件到div容器中
function createControlToPage(containerName///绘制控件的容器divId
    , corJson///EntityField集合
    , dataJson///数据
    , parentRowsData
    , tabTitles///选项卡
    , ctrlType
    , columnCnt) {
    ///判定是新增页面还是修改页面，凭借参数数据？
    var isEditPage = false;
    if (isValidData(dataJson)) {
        dataJson = toJson(dataJson);
        isEditPage = true;
    }
    ///如果没有可加载的控件，则直接返回
    if (!isValidData(corJson)) {
        return;
    }
    ///获取容器
    var container = $('#' + containerName);
    if (container.length > 0) {
        container = container[0];
    }
    else {
        ///未发现容器则返回
        return;
    }
    if (!isValidData(columnCnt))
        columnCnt = 2;
    ///选项卡，TODO:考虑也修改为JSON格式
    var titles = [];
    if (isValidData(tabTitles)) {
        titles = tabTitles.split('|');
    }
    if (titles.length > 0) {
        var tabEntityFields = $('#tabEntityFields');
        if (tabEntityFields.length > 0) {
            tabEntityFields = tabEntityFields[0];
        }
        else {
            ///创建选项卡容器
            tabEntityFields = document.createElement("div");
            tabEntityFields.id = 'tabEntityFields';
            container.appendChild(tabEntityFields);
        }
        $('#tabEntityFields').tabs({
            border: false,
            narrow: true
        });
        ///控制首个选中
        var selected = true;
        for (var j = 0; j < titles.length; j++) {
            ///格式目前为 代码-中文描述-英文描述
            if (titles[j].split('-').length < 2) continue;
            ///代码
            var titleCode = titles[j].split('-')[0];
            ///中文
            var titleName = titles[j].split('-')[1];
            ///英文为可选项
            if (titles[j].split('-').length == 3 && XR.language) {
                titleName = titles[j].split('-')[2];
            }
            ///div_titleCode
            var contentId = "div_" + titleCode + "";
            var content = '<div scrolling="auto" id="' + contentId + '" style="width:98%;height:98%;padding:5px;"></div>';
            var iconCls = null;
            if (isValidData(ctrlType)) {
                selected = false;
                iconCls = 'icon-0000004';
            }
            $('#tabEntityFields').tabs('add', {
                id: titleCode,
                title: titleName,
                content: content,
                selected: selected,
                iconCls: iconCls
            });
            if (selected) selected = false;
        }
    }
    ///加载EntityField
    for (var i = 0; i < corJson.length; i++) {
        ///TODO:考虑将非编辑属性直接过滤掉
        if (!corJson[i].Editable) continue;
        ///属性名
        var bindName = corJson[i].FieldName;
        ///标题
        var tText = XR.language ? corJson[i].DisplayNameEn : corJson[i].DisplayNameCn;
        ///控件类型
        var tType = corJson[i].ControlType;
        ///正则表达式
        var regexExp = corJson[i].Regex;
        ///宽度
        var tWidth = corJson[i].EditDisplayWidth;
        ///编辑显示
        var isdisplay = corJson[i].Editable;
        ///默认值
        var defaultVal = corJson[i].DefaultValue;
        ///扩展1，系统代码
        var tCodeName = corJson[i].Extend1;
        ///扩展3
        var extend = corJson[i].Extend3;
        ///扩展2，权限配置
        var authExtend = corJson[i].Extend2;
        ///控件只读方式
        var readonlyType = corJson[i].EditReadonly;
        ///正则校验错误时提示信息
        var regexErrorMsg = '';
        var regexErrorMsgs = corJson[i].ErrorMsg;
        if (isValidData(regexErrorMsgs)) {
            regexErrorMsgs = regexErrorMsgs.split('|');
            if (regexErrorMsgs.length > 1)
                regexErrorMsg = XR.language ? regexErrorMsgs[1] : regexErrorMsgs[0];
            else
                regexErrorMsg = regexErrorMsgs[0];
        }
        ///是否允许为空
        var nullEnable = corJson[i].Nullenable;
        ///单个控件容器
        var Container = createContainer(isValidData(ctrlType) ? ctrlType + "-" + bindName : bindName, "", isdisplay, tWidth, columnCnt);
        if (isValidData(corJson[i].TabTitleCode) && corJson[i].TabTitleCode != 'NULL') {
            $('#div_' + corJson[i].TabTitleCode)[0].appendChild(Container);
        }
        else {
            container.appendChild(Container);
        }
        var ContainerId = Container.id;
        ///控件ID
        var tId = tType + "-" + bindName;
        if (isValidData(ctrlType))
            tId = tType + "-" + ctrlType + "-" + bindName;
        ///如果有数据则将默认值变量设置为对应属性的值
        if (isValidData(dataJson)) {
            defaultVal = dataJson[bindName];
        }
        ///是否只读
        var tReadonly = false;
        ///编辑只读且是编辑模式，则只读
        if (readonlyType == 20 && isEditPage) tReadonly = true;
        ///添加只读且是新增模式，则只读
        else if (readonlyType == 30 && !isEditPage) tReadonly = true;
        ///始终只读
        else if (readonlyType == 40) tReadonly = true;
        ///最小值
        var minValue = corJson[i].MinValue;
        ///最大值
        var maxValue = corJson[i].MaxValue;
        ///小数精度
        var precision = corJson[i].Precision;
        ///数据长度
        var dataLength = corJson[i].DataLength;
        ///字段帮助信息
        var arrControlHelp = XR.language ? corJson[i].TooltipHelperEn : corJson[i].TooltipHelperCn;
        ///创建控件
        createControl(ContainerId, tId, tText, tType, regexExp
            , tWidth, "", "", extend, tCodeName, defaultVal, tReadonly, arrControlHelp, parentRowsData, ""
            , authExtend, regexErrorMsg, nullEnable, minValue, maxValue, dataLength, precision);
    }
};
///JS-017创建表单控件接受层
function createContainer(bindName, className, display, tWidth, columnCnt) {
    var divContainer = document.createElement('div');
    divContainer.id = 'div_' + bindName;
    display = display ? 'block' : 'none';
    divContainer.style.display = display;
    className = !isValidData(className) ? "containerDivCls" : className;
    var pWidth = 100 / columnCnt - 2;
    if (className == "containerDivCls") {
        if (tWidth < 360 || !isValidData(tWidth)) {
            divContainer.style.width = pWidth + '%';
        } else {
            if (isValidData(columnCnt) && columnCnt > 2)
                divContainer.style.width = pWidth * 2 + '%';
            else
                divContainer.style.width = '100%';
        }
    }
    divContainer.className = className;
    return divContainer;
};
///JS-018页面创建动作按钮
function createActionData(controlId, dtAction) {
    ///创建菜单按钮
    if (isValidData(dtAction) && dtAction != []) {
        for (var i = 0; i < dtAction.length; i++) {
            var bId = dtAction[i].Fid;
            var bText = XR.language ? dtAction[i].ActionName : dtAction[i].ActionNameCn;
            var bIcon = dtAction[i].IconUrl;
            var bJs = dtAction[i].ClientJs;
            var bType = dtAction[i].ActionName;
            createMenu(controlId, bId, bText, bIcon, bJs, bType);
        }
    }
    else {
        $('#' + controlId).hide();
    }
}
///JS-019创建动作按钮
function createMenu(acceptObj///容器div的ID
    , bId///控件ID
    , bText///标题
    , bIcon///图标
    , bJavascript///JavaScript代码
    , bType)///动作按钮名称
{
    var bHref = $("<a href='javascript:void(0)'></a>");
    bHref.attr('id', bId);
    $('#' + acceptObj).append(bHref);
    ///注册click事件
    $('#' + bId).bind('click', function () {
        eval(bJavascript);
    });
    ///动态生成add非主页面Entity的js函数
    var medthodType = bJavascript.split("add");
    if (medthodType.length == 2 && bJavascript != 'add()') {
        ///如果配置中有()，则需要去除
        var entityName = medthodType[1].replace("(", "").replace(")", "");
        var medthodContent = "publicInit('" + entityName + "')";
        var medthodName = bJavascript.replace("(", "").replace(")", "");
        var medthodDefaultInit = "window." + medthodName + "=function(){" + medthodContent + "};\r\n";
        try {
            eval(medthodDefaultInit);
        } catch (e) { }
    }
    ///指定为按钮
    $('#' + bId).linkbutton({
        iconCls: bIcon,
        plain: true,
        text: bText
    })
};
///JS-020页面创建搜索条件
function createSearchControlData(controlId, dtSearch, columnCnt) {
    var container = $('#' + controlId);
    if (!isValidData(container.length)) return;
    if (!isValidData(dtSearch)) {
        container.hide();
        return;
    }
    else
        container.show();
    if (!isValidData(columnCnt)) columnCnt = 3;
    ///
    for (var i = 0; i < dtSearch.length; i++) {
        ///
        var tId = dtSearch[i].ControlType + '-S-' + dtSearch[i].ControlId;
        var tText = XR.language ? dtSearch[i].ControlId : dtSearch[i].LabelText;
        var tType = dtSearch[i].ControlType;
        var tSearchType = dtSearch[i].DatasearchType;
        var tColName = dtSearch[i].ColumnName;
        var tColType = dtSearch[i].ColumnType;
        var tVerifyType = dtSearch[i].RegexExpression;
        var tDefault = dtSearch[i].DefaultValue;
        var tCodeName = dtSearch[i].CodeName;
        var tExtend = dtSearch[i].ExtendField3;
        var authExtend = dtSearch[i].ExtendField2;

        var regexErrorMsg = '';
        var regexErrorMsgs = dtSearch[i].ExtendField4;
        if (isValidData(regexErrorMsgs)) {
            regexErrorMsgs = regexErrorMsgs.split('|');
            if (regexErrorMsgs.length > 1)
                regexErrorMsg = XR.language ? regexErrorMsgs[1] : regexErrorMsgs[0];
            else
                regexErrorMsg = regexErrorMsgs[0];
        }

        var minValue = dtSearch[i].ExtendField5;
        var maxValue = dtSearch[i].ExtendField6;
        var precision = dtSearch[i].ExtendField7;
        var dataLength = dtSearch[i].MaxLength;

        var tReadonly = false;
        var bindId = tId + "_" + i;
        ///searchControlDivCls
        var Container = createContainer(bindId, '', true, 0, columnCnt);
        container.append(Container)
        var ContainerId = Container.id;

        var intervalWidth = XR.language ? 200 : 140;

        createControl(ContainerId, tId, tText, tType, tVerifyType, 220, 22, tColType, tExtend, tCodeName, tDefault, tReadonly, null, null, intervalWidth, authExtend, regexErrorMsg, true, minValue, maxValue, dataLength, precision);
    }
};

window.arrColumnsValueItem = "";
var commonJsonData = [];
var firstEditFieldName = undefined;
//获取GRID列： 遍历dtColumns生成GRID列
function getFormatGridColumnsData(dtColumns, gridCheckBox, entityName) {
    if (!isValidData(dtColumns)) return '[[]]';
    var tmpColumns = '';
    ///是否创建CheckBox
    if (gridCheckBox) {
        tmpColumns += '{ "field":"ck","checkbox":true },';
    }
    var totalFieldCnt = 0;
    for (var i = 0; i < dtColumns.length; i++) {
        if (!dtColumns[i].Listable) continue;
        totalFieldCnt++;
    }
    ///总宽度
    var columnWidthTotal = 0;
    for (var i = 0; i < dtColumns.length; i++) {
        ///是否列表显示
        if (!dtColumns[i].Listable) continue;
        ///
        totalFieldCnt--;
        ///列标题
        var gText = XR.language ? dtColumns[i].DisplayNameEn : dtColumns[i].DisplayNameCn;
        ///单列宽度
        var columnWidth = 0;
        ///默认120
        if (!isValidData(dtColumns[i].ListDisplayWidth) || dtColumns[i].ListDisplayWidth == 0) {
            columnWidth = '"width":120';
            columnWidthTotal += 120 * 1;
        }
        else {
            columnWidth = '"width":' + dtColumns[i].ListDisplayWidth;
            columnWidthTotal += dtColumns[i].ListDisplayWidth * 1;
        }
        ///最后一列自适应
        if (totalFieldCnt == 0) {
            if (columnWidthTotal > document.body.scrollWidth - 10)
                columnWidth = '"fixed":true';
            else
                columnWidth = '"width":' + (document.body.scrollWidth - 10 - columnWidthTotal);
        }
        ///Sortable
        var sortableFlag = dtColumns[i].Sortable;
        if (!isValidData(sortableFlag)) sortableFlag = false;
        ///Styler
        var stylerContent = '';
        if (isValidData(dtColumns[i].Extend3)) {
            var stylerConfig = dtColumns[i].Extend3.split('^');
            if (isValidData(stylerConfig) && stylerConfig[0] == 'styler' && stylerConfig.length == 2) {
                var stylerConfigs = stylerConfig[1].split('|');
                for (var j = 0; j < stylerConfigs.length; j++) {
                    var stylerJson = GetComboExtendJson(stylerConfigs[j]);
                    if (isValidData(stylerJson)) {
                        if (isValidData(stylerJson.stylerValue))
                            stylerContent += 'case "' + stylerJson.stylerValue + '": return "background-color:' + stylerJson.stylerBackColor + ';color:' + stylerJson.stylerColor + ';";';
                        if (isValidData(stylerJson.stylerLogic))
                            stylerContent += 'case ' + stylerJson.stylerLogic + ': return "background-color:' + stylerJson.stylerBackColor + ';color:' + stylerJson.stylerColor + ';";';
                    }
                }
                if (isValidData(stylerContent)) {
                    if (isValidData(stylerJson.attributeName))
                        stylerContent = ',"styler":function(value,row,index){ var valueStr = row.' + stylerJson.attributeName + '+"";switch(valueStr.toLowerCase()){' + stylerContent + '}}';
                    else
                        stylerContent = ',"styler":function(value,row,index){var valueStr = value+""; switch(valueStr.toLowerCase()){' + stylerContent + '}}';
                }
            }
        }
        ///Editor
        var editorContent = '';
        if (dtColumns[i].ListEditorFlag) {
            var listEditorControlType = 'text';
            switch (dtColumns[i].ListEditorControlType + '') {
                case '20': listEditorControlType = 'textarea'; break;
                case '30': listEditorControlType = 'checkbox'; break;
                case '40': listEditorControlType = 'numberbox'; break;
                case '50': listEditorControlType = 'validatebox'; break;
                case '60': listEditorControlType = 'datebox'; break;
                case '70': listEditorControlType = 'combobox'; break;
                case '80': listEditorControlType = 'combotree'; break;
                default: listEditorControlType = 'text'; break;
            }
            editorContent = ',editor:{type:"' + listEditorControlType + '"}';
            if (firstEditFieldName == undefined)
                firstEditFieldName = dtColumns[i].FieldName;
        }
        ///
        switch (dtColumns[i].ControlType + '') {
            case '50':
                ///如果是字符不转换直接显示
                if (dtColumns[i].DataType + '' == '10')
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' },';
                else {
                    var dateformat = XR.language ? 'MM/dd/yyyy' : 'yyyy-MM-dd';
                    var configJson;
                    if (isValidData(dtColumns[i].Extend1)) {
                        configJson = GetComboExtendJson(dtColumns[i].Extend1);
                        if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                            dateformat = configJson.datetimeFormat;
                    }
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = FORM.formatDateByJson(value,"' + dateformat + '"); return e;}  },';
                }
                break;
            case '60':
            case '130':
                if (dtColumns[i].DataType + '' == '10')
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' },';
                else {
                    var dateformat = XR.language ? "MM/dd/yyyy HH:mm:ss" : "yyyy-MM-dd HH:mm:ss";
                    ///JSON格式配置
                    var configJson;
                    if (isValidData(dtColumns[i].Extend1)) {
                        configJson = GetComboExtendJson(dtColumns[i].Extend1);
                        if (isValidData(configJson) && isValidData(configJson.datetimeFormat))
                            dateformat = configJson.datetimeFormat;
                    }
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = FORM.formatDateByJson(value,"' + dateformat + '"); return e;}  },';
                }
                break;
            case '20':
                if (isValidData(dtColumns[i].Extend3) && dtColumns[i].Extend2 != "GridNotFormatter") {
                    if (dtColumns[i].Extend3.indexOf('sql^') > -1) {
                        var paramItem = dtColumns[i].Extend3.replace('sql^', '');
                        ///联动控件分隔符，从第二段开始为多个联动控件
                        var paramControlItem = paramItem.split("|");
                        var configJson = GetComboExtendJson(paramControlItem[0]);
                        configJson.ajaxMethod = 'ajaxControlDataToColumn';
                        if (isValidData(configJson.sqlFilter))
                            configJson.sqlFilter = configJson.sqlFilter.replace('1=0', '');
                        var comboUrl = GetDataUrlByExtendJson(configJson, '', '', dtColumns[i].FieldName);
                        ///权限
                        var authFilters = getDataAuthByEntityAuthConfig(dtColumns[i].Extend2);
                        HELP.ajaxCommon(comboUrl, "&AUTH_FILTER=" + authFilters, "js:getFormatterColumnsData", false);
                    }
                    else if (dtColumns[i].Extend3.indexOf('json^') > -1) {
                        $.getJSON(XR.getPathLevel() + '/JS/data/common.data.json'
                            , function (jsonData) {
                                commonJsonData = jsonData;
                            });
                    }
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = formatterComboBoxByColumns(value,row,index, "' + dtColumns[i].FieldName + '","' + dtColumns[i].Extend3 + '"); return e;}  },';
                }
                else {
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' },';
                }
                break;
            case '80':
            case '100':
                var codeName = dtColumns[i].Extend1;
                if (dtColumns[i].ControlType == '100') {
                    codeName = 'BOOLEAN';
                }
                var dataParams = "method=ajaxCodeList&CODE_NAME=" + codeName + "&ENTITY_NAME=Code&AN=BLL.SYS";
                HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, "code:getFormatterControlData-" + codeName + "", false);
                tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = defaultFormatterColumns(value,row,index,"' + codeName + '"); return e;}  },';
                break;
            case '30':
                if (isValidData(dtColumns[i].Extend3)) {
                    if (dtColumns[i].Extend3.indexOf('sql^') > -1) {
                        var paramItem = dtColumns[i].Extend3.replace('sql^', '');
                        ///联动控件分隔符，从第二段开始为多个联动控件
                        var paramControlItem = paramItem.split("|");
                        var configJson = GetComboExtendJson(paramControlItem[0]);
                        configJson.ajaxMethod = 'ajaxControlDataToColumn';
                        if (isValidData(configJson.sqlFilter))
                            configJson.sqlFilter = configJson.sqlFilter.replace('1=0', '');
                        var comboUrl = GetDataUrlByExtendJson(configJson, '', '', dtColumns[i].FieldName);
                        ///权限
                        var authFilters = getDataAuthByEntityAuthConfig(dtColumns[i].Extend2);
                        HELP.ajaxCommon(comboUrl, "&AUTH_FILTER=" + authFilters, "js:getFormatterColumnsData", false);
                    }
                    else if (dtColumns[i].Extend3.indexOf('json^') > -1) {
                        $.getJSON(XR.getPathLevel() + '/JS/data/common.data.json'
                            , function (jsonData) {
                                commonJsonData = jsonData;
                            });
                    }
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = formatterComboTreeByColumns(value,row,index, "' + dtColumns[i].FieldName + '","' + dtColumns[i].Extend3 + '"); return e;}  },';
                }
                else {
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' },';
                }
                break;
            case '40':
                if (isValidData(dtColumns[i].Extend3) && dtColumns[i].Extend2 != "GridNotFormatter") {
                    if (dtColumns[i].Extend3.indexOf('sql^') > -1) {
                        var paramItem = dtColumns[i].Extend3.replace('sql^', '');
                        ///联动控件分隔符，从第二段开始为多个联动控件
                        var paramControlItem = paramItem.split("|");
                        var configJson = GetComboExtendJson(paramControlItem[0]);
                        configJson.ajaxMethod = 'ajaxControlDataToColumn';
                        if (isValidData(configJson.sqlFilter))
                            configJson.sqlFilter = configJson.sqlFilter.replace('1=0', '');
                        var comboUrl = GetDataUrlByExtendJson(configJson, '', '', dtColumns[i].FieldName);
                        ///权限
                        var authFilters = getDataAuthByEntityAuthConfig(dtColumns[i].Extend2);
                        HELP.ajaxCommon(comboUrl, "&AUTH_FILTER=" + authFilters, "js:getFormatterColumnsData", false);
                    }
                    else if (dtColumns[i].Extend3.indexOf('json^') > -1) {
                        $.getJSON(XR.getPathLevel() + '/JS/data/common.data.json'
                            , function (jsonData) {
                                commonJsonData = jsonData;
                            });
                    }
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' ,"formatter":function(value,row,index){ var e = formatterComboGridByColumns(value, "' + dtColumns[i].FieldName + '","' + configJson.idField + '","' + configJson.textField + '"); return e;}  },';
                }
                else {
                    tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + '  },';
                }
                break;
            default: tmpColumns += '{ "field":"' + dtColumns[i].FieldName + '","title":"' + gText + '",' + columnWidth + stylerContent + editorContent + ',"sortable" : ' + sortableFlag + ' },'; break;
        }
    }

    return tmpColumns = '[[' + tmpColumns.substring(0, tmpColumns.lastIndexOf(',')) + ']]';

};

///创建Grid --START
//gTitle:标题
//gHeight:高度
//gSingleSelect:是否只允许选择一行 true\false
//gPagination:分页工具栏 true\false
//gIdField:标识字段
//gSortName:排序列
//gSortOrder:排序顺序'asc'\'desc'
//gUrl:远程站点请求
//gColumns:列配置

///JS-022 页面创建数据网格
function createGrid(entityName, gIdField, gSortName, gSortOrder, gColumns, dataUrl, defaultPageSize, containerId, isChildGrid, isDialog) {
    if (gColumns == '[[]]') return;
    ///width
    var dataGridWidth = setGridWidth();
    ///height
    var dataGridHeight = setGridHeight();
    ///根据参数entityName从页面内存中获取ENTITY对象
    var entityInfo = getEntity(entityName);
    if (!isValidData(entityInfo)) return;
    ///真正ENTITY_INFO数据
    var formEntityInfo = entityInfo.formEntityInfo;
    if (!isValidData(formEntityInfo)) return;
    ///网格类型
    var gType = formEntityInfo.EntityType == '' ? '1' : formEntityInfo.EntityType;
    ///关联配置
    var gParentField = formEntityInfo.ParentField;
    if (!isValidData(gParentField)) gParentField = '';
    ///2018-6-8数据模型中的默认页行数
    var defaultSize = formEntityInfo.DefaultPagesize;
    if (isValidData(defaultSize)) {
        if (defaultSize > 0) {
            defaultPageSize = defaultSize;
        }
    }
    ///排序，优先获取ENTITY配置中的排序依据
    if (isValidData(formEntityInfo.DefaultSort)) {
        var defaultSortItem = formEntityInfo.DefaultSort.split('-');
        if (defaultSortItem.length > 1) {
            gSortName = gSortName == '' ? defaultSortItem[0] : gSortName;
            gSortOrder = gSortOrder == '' ? defaultSortItem[1] : gSortOrder;
        }
    }
    gSortName = gSortName == '' ? 'Id' : gSortName;
    gSortOrder = gSortOrder == '' ? 'desc' : gSortOrder;
    ///主键,ENTITY.KEY_FIELDS，默认Id
    if (isValidData(gIdField)) {
        var keyFields = gIdField.split('|');
        gIdField = keyFields[0];
    }
    else {
        if (isValidData(formEntityInfo.KeyFields)) {
            var keyFields = formEntityInfo.KeyFields.split('|');
            gIdField = keyFields[0];
        }
        else {
            gIdField = 'Id';
        }
    }
    ///获取模型配置中的权限
    var authFilters = getDataAuthByEntityAuthConfig(formEntityInfo.AuthConfig);
    if (isValidData(authFilters)) {
        dataUrl += '&ENTITY_AUTH=' + authFilters;
    }
    ///网格寄生者
    if (!isValidData(containerId))
        containerId = tblGridId;
    if (isDialog == true) {
        gType = "1";
    }
    ///创建不同类型的数据网格
    switch (gType + '') {
        ///tree
        case '2': createTreeGrid(gParentField, containerId, dataGridWidth, dataGridHeight, gSortName, gSortOrder, gColumns, dataUrl, isChildGrid); break;
        ///detail
        case "3": createDetailGrid(entityName, gParentField, containerId, dataGridWidth, dataGridHeight, gSortName, gSortOrder, gColumns, dataUrl, defaultPageSize, isChildGrid); break;
        ///datagrid
        default: createDataGrid(gIdField, gParentField, containerId, dataGridWidth, dataGridHeight, gSortName, gSortOrder, gColumns, dataUrl, defaultPageSize, isChildGrid, formEntityInfo.FooterFlag, formEntityInfo.CheckOnSelect, formEntityInfo.SelectOnCheck); break;
    }
};
///JS-023 页面创建数据网格
function createGridByEntityName(entityName, containerId, dataUrl, methodName) {
    $.post(XR.defaultProcessUrl(), { 'method': 'ajaxColumnsEntity', 'ENTITY_NAME': entityName },
        function (data) {
            ///验证登录信息是否有效
            sessionIsNull(data);
            if (isSessionNull) return;
            var dtColumns = data.columns;
            var dtEntityInfo = data.entityinfo;
            ///非法数据返回
            if (!isValidData(dtEntityInfo)) return;
            ///是否多选
            var isMuliCheck = isGridMuliCheck(dtEntityInfo.ParentField);
            var gColumns = getFormatGridColumnsData(dtColumns, isMuliCheck, entityName);
            ///生成datagrid控件，JS-022
            createGrid(entityName, '', '', '', gColumns, dataUrl, 0, containerId, false);
            if (isValidData(methodName)) {
                eval(methodName);
            }
        }, 'json');
};
///JS-024 创建DATAGRID控件
function createDataGrid(IdField, eParentField, containerId, width, height, sortName, sortOrder, eColumns, dataUrl, defaultPageSize, isChildGrid, footerFlag, checkOnSelectFlag, selectOnCheckFlag) {
    ///依据PARENT_FIELD配置信息，判断是否多选
    var isCheckBox = isGridMuliCheck(eParentField);
    if (isChildGrid) isCheckBox = false;
    ///if (isCheckBox) width -= 16;
    ///未设置则默认为20
    if (!isValidData(defaultPageSize))
        defaultPageSize = isChildGrid ? 10 : 20;
    if (defaultPageSize * 1 <= 0)
        defaultPageSize = isChildGrid ? 10 : 20;
    ///默认为ID主键
    if (!isValidData(IdField))
        IdField = 'Id';
    if (isChildGrid) { height = 'auto'; width -= 46; }
    ///注脚是否显示
    if (!isValidData(footerFlag)) footerFlag = false;
    ///选中时勾选
    if (!isValidData(checkOnSelectFlag)) checkOnSelectFlag = false;
    ///勾选时选中
    if (!isValidData(selectOnCheckFlag)) selectOnCheckFlag = false;
    ///创建datagrid
    $('#' + containerId).datagrid({
        width: width,
        height: height,
        rownumbers: !isCheckBox,
        fitColumns: true,
        pageNumber: 1,
        pageSize: defaultPageSize * 1,
        pageList: [5, 10, 20, 50, 100, 500],
        singleSelect: true,
        pagination: true,
        idField: IdField,
        sortName: sortName,
        sortOrder: sortOrder,
        columns: eval(eColumns),
        url: dataUrl,
        checkOnSelect: checkOnSelectFlag,
        selectOnCheck: selectOnCheckFlag,
        striped: true,
        showFooter: footerFlag,
        onClickRow: function (index, row) {
            if (firstEditFieldName == undefined) return;
            if (editIndex == undefined) editIndex = -1;
            if (editIndex != index) {
                $("#" + gridChoiceId).datagrid('loading');
                if (endEditing()) {
                    $("#" + gridChoiceId).datagrid('selectRow', index).datagrid('beginEdit', index);
                    var ed = $("#" + gridChoiceId).datagrid('getEditor', { index: index, field: firstEditFieldName });
                    editIndex = index;
                } else {
                    $("#" + gridChoiceId).datagrid('selectRow', editIndex);
                }
                $('.datagrid-editable-input').select();
                setTimeout(function () { $("#" + gridChoiceId).datagrid('loaded'); }, 300);
            }
        },
        onLoadSuccess: function (data) {
            if (isChildGrid) {
                fixGridRowHeight(containerId);
                formatterGridColumnsForComboGridData(containerId, data);
            }
        },
        onSelect: function (index, row) {
            onClickRowByGrid(row);
            gridChoiceId = this.id;
            tblGridId = this.id;
        },
        onBeforeSelect: function (index, row) {
            if (this.id != gridChoiceId && isValidData(gridChoiceId)) {
                $("#" + gridChoiceId).datagrid("clearSelections");
            }
        },
        onUnselect: function (index, row) {
            onClickRowByGrid(null);
        }
    });
};
///Start Editing Tables Script
var editIndex = undefined;
function endEditing() {
    if (firstEditFieldName == undefined) return false;
    if (editIndex == undefined) return false;
    if (editIndex == -1) return true;
    if ($("#" + gridChoiceId).datagrid('validateRow', editIndex)) {
        $("#" + gridChoiceId).datagrid('endEdit', editIndex);
        editIndex = -1;
        return true;
    } else {
        return false;
    }
}

///JS-025 创建TREEGRID控件
function createTreeGrid(eParentField, containerId, width, height, sortName, sortOrder, eColumns, dataUrl, isChildGrid) {
    ///对于树形结构网格必须有关联配置信息，且信息量大于等于3
    var eParentFields = eParentField.split('-');
    if (eParentFields.length < 3) return;
    var IdField = eParentFields[0];
    var PidField = eParentFields[1];
    var treeFieldName = eParentFields[2];
    ///是否多选
    var isCheckBox = isGridMuliCheck(eParentField);
    if (isCheckBox) width -= 16;
    if (isChildGrid) { height = 'auto'; width -= 26; }
    var pidFieldName = '';
    if (eParentFields.length > 4) {
        pidFieldName = eParentFields[4];
    }
    $('#' + containerId).treegrid({
        width: width,
        height: height,
        methon: 'POST',
        rownumbers: false,
        collapsible: true,
        ///定义当节点展开或折叠时是否显示动画效果
        animate: true,
        singleSelect: !isCheckBox,
        ///定义标识树节点的键名字段。必需
        idField: IdField,
        ///定义树节点的字段。必需
        treeField: treeFieldName,
        sortName: sortName,
        sortOrder: sortOrder,
        columns: eval(eColumns),
        url: dataUrl,
        queryParams: { ID: IdField, PARENT_ID: PidField },
        ///返回要显示的过滤数据
        loadFilter: function (data, parentId) {
            return arrayToTree(data.rows, IdField, PidField);
        },
        onClickRow: function (row) {
            $(this).treegrid('cascadeCheck', {
                id: eval("row." + IdField),
                deepCascade: false
            });
        },
        onExpand: function (row) {
            $('#' + containerId).treegrid('resize', {
                width: setGridWidth(),
                height: setGridHeight()
            });
        },
        onLoadSuccess: function (row, data) {
            $(this).treegrid('collapseAll');
            var selectedrow = $(this).treegrid('getSelected');
            if (isValidData(selectedrow)) {
                var selectedrowid = eval("selectedrow." + IdField);
                $(this).treegrid('expandTo', selectedrowid);
            }
            if (isChildGrid) {
                fixGridRowHeight(containerId);
                formatterGridColumnsForComboGridData(containerId, data);
            }
        },
        onBeforeSelect: function (index, row) {
            if (this.id != gridChoiceId && isValidData(gridChoiceId)) {
                $("#" + gridChoiceId).datagrid("clearSelections");
            }
        },
        onSelect: function (node) {
            gridChoiceId = this.id;
            tblGridId = this.id;
            if (!isValidData(node)) return;
            var entityInfo = getEntity(pageLoadParams.entityName);
            var parentFieldName = convertColumnByFieldName(PidField);
            entityInfo.formParamKey = parentFieldName + "='" + eval("node." + IdField) + "'&"
            entityInfo.formParamRowsData = node;
            //putEntity(pageLoadParams.entityName, entityInfo);
        },
        onCollapseRow: function (index, row) {
            fixGridRowHeight(containerId + "_" + index);
        }
    });
};

var gridChoiceId = "";
///JS-026 创建DETAILGRID控件
function createDetailGrid(entityName, eParentField, containerId, width, height, sortName, sortOrder, eColumns, dataUrl, defaultPageSize, isChildGrid) {

    ///加载从表的数据模型配置    
    if (!isValidData(eParentField)) return;
    ///解析PARENT_FIELD
    var eParentFields = eParentField.split(',')[0].split("-");
    if (eParentFields.length <= 3) return;
    var subPageEntity = new PageEntity();
    ///主表主键属性
    var eIdField = eParentFields[0];
    ///从表关联主表时的字段名
    var parentFieldName = eParentFields[1];
    ///从表实体名
    subPageEntity.entityName = eParentFields[2];
    ///从表表名
    subPageEntity.tableName = eParentFields[3];
    ///加载entity.field.formaction
    subPageEntity.AjaxActionOrEntityData('entity'
        , 'createDetailGridControl(\'' + subPageEntity.entityName
        + '\',\'' + containerId
        + '\',\'' + eIdField
        + '\',\'' + eParentField
        + '\',\'' + parentFieldName
        + '\',' + width
        + ',' + height
        + ',\'' + sortName
        + '\',\'' + sortOrder
        + '\',\'' + eColumns
        + '\',\'' + dataUrl
        + '\',' + defaultPageSize
        + ',' + isChildGrid + ')');
    arrTempMap.put(containerId, entityName);
}
///
function createDetailGridControl(entityName, containerId, eIdField, eParentField, parentFieldName, width, height, sortName, sortOrder, eColumns, dataUrl, defaultPageSize, isChildGrid) {

    ///是否多选
    var isCheckBox = isGridMuliCheck(eParentField);
    if (isCheckBox) width -= 16;
    ///是否从表网格
    if (!isValidData(isChildGrid)) isChildGrid = false;
    isChildGrid = isChildGrid + ''.toLowerCase() == 'true' ? true : false;
    ///默认每页显示行数为20
    if (!isValidData(defaultPageSize))
        defaultPageSize = isChildGrid ? 10 : 20;
    if (defaultPageSize * 1 <= 0)
        defaultPageSize = isChildGrid ? 10 : 20;
    ///创建DETAILGRID
    $('#' + containerId).datagrid({
        methon: 'POST',
        fitColumns: true,
        autoRowHeight: true,
        singleSelect: true,
        pagination: true,
        pageNumber: 1,
        pageSize: defaultPageSize,
        pageList: [5, 10, 20, 50, 100, 500],
        idField: eIdField,
        sortName: sortName,
        sortOrder: sortOrder,
        columns: eval(eColumns),
        url: dataUrl,
        checkOnSelect: false,
        selectOnCheck: false,
        striped: true,
        view: detailview,
        detailFormatter: function (index, row) {
            arrTempMap.put(containerId + "_" + index, entityName);
            ///为每行建立一个div寄生者，主寄生者ID+index
            return '<div style="padding:0px;"><table id="' + containerId + "_" + index + '"></table></div>';
        },
        onExpandRow: function (index, row) {
            ///更新当前选中GRID.ID
            gridChoiceId = this.id;
            tblGridId = this.id;
            if (!isValidData(row)) return;
            var cEntityName = arrTempMap.get(containerId + "_" + index);
            var cEntity = getEntity(cEntityName);
            var rowValue = eval("row." + eIdField);
            cEntity.formParamKey = parentFieldName + "=^" + rowValue + "^";
            cEntity.formParamRowsData = row;
            putEntity(cEntityName, cEntity);
            ///2018-4-26
            if (isValidData(pageLoadParams))
                pageLoadParams.formParamRowsData = row;
            ///是否多选
            var isMuliCheck = isGridMuliCheck(cEntity.formEntityInfo.ParentField);
            ///列集合json，DETAIL不显示checkbox
            var cColumns = getFormatGridColumnsData(cEntity.formCreateData, false, cEntityName);
            ///生成数据获取链接
            var cUrl = XR.defaultProcessUrl()
                + "method=ajaxTables"
                + "&ENTITY_NAME=" + cEntityName
                + "&FOOTER_FLAG=" + cEntity.formEntityInfo.FooterFlag
                + "&AN=BLL." + cEntity.bllProjectName
                + "&TABLE_NAMES=" + cEntity.tableName
                + "&FILTER=" + filterEncodeURIComponent(cEntity.formParamKey);

            ///生成datagrid控件，JS-022
            createGrid(cEntityName, cEntity.formEntityInfo.KeyFields, '', '', cColumns, cUrl, 0, containerId + "_" + index, true);
        },
        onLoadSuccess: function (data) {
            if (isChildGrid) {
                fixGridRowHeight(containerId);
                formatterGridColumnsForComboGridData(containerId, data);
            }
        },
        onBeforeSelect: function (index, row) {
            if (isValidData(gridChoiceId) && this.id != gridChoiceId) {
                ///清除非当前选中GRID的选中行项目
                $("#" + gridChoiceId).datagrid("clearSelections");
            }
        },
        onSelect: function (index, row) {
            gridChoiceId = this.id;
            tblGridId = this.id;
            if (!isValidData(row)) return;
            var cEntityName = arrTempMap.get(containerId + "_" + index);
            var cEntity = getEntity(cEntityName);
            var rowValue = eval("row." + eIdField);
            cEntity.formParamKey = parentFieldName + "=^" + rowValue + "^";
            cEntity.formParamRowsData = row;
            putEntity(cEntityName, cEntity);
            ///2018-4-26
            if (isValidData(pageLoadParams))
                pageLoadParams.formParamRowsData = row;
        },
        onCollapseRow: function (index, row) {
            fixGridRowHeight(containerId + "_" + index);
        }
    });
    if (containerId == "tblGrid") {
        $('#' + containerId).datagrid({
            height: setGridHeight()
        });
    }
}

function createDetailGrid1(entityInfo, parentContainerId, containerId, parentFieldValue, dataUrl) {

    var subEntityName = entityInfo.EntityName;
    var subTableName = entityInfo.TableNames;

    var parentIndex = 0;
    var parentFieldName = "";
    var subFilter = "";
    var gIdField = "Fid";

    if (parentContainerId != "") {
        var parentIdItem = containerId.replace(parentContainerId, "").split('_');
        if (parentIdItem.length > 1) {
            parentIndex = parentIdItem[1];
        }
        ///第一个子表
        var eParentFields = entityInfo.ParentField.split(',')[0].split("-");
        if (eParentFields.length > 3) {
            gIdField = eParentFields[0];
            parentFieldName = eParentFields[1];
            subEntityName = eParentFields[2];
            subTableName = eParentFields[3]
            subFilter = "and " + parentFieldName + "= '" + parentFieldValue + "' ";
        }
    }
    var dataparams = {
        "method": "ajaxSublistTables"
        , "ENTITY_NAME": subEntityName
        , "TABLE_NAMES": subTableName
        , "FILTER": filterEncodeURIComponent(subFilter)
        , "AN": "BLL." + pageLoadParams.bllProjectName///这里表明主从关系表还必须在同一个模块下
    };
    var gSortName = "Id";
    var gSortOrder = "desc";

    $.post(XR.defaultProcessUrl(), dataparams, function (data) {
        if (data != undefined && data != null && data != "") {
            sessionIsNull(data);
            if (isSessionNull) return;
            var subActionForm = data.actionForm;
            var subColumns = data.subColumns;
            var subEntityInfo = data.entityData;
            var subPageEntity = arrTempMap.get(subEntityName);
            if (subPageEntity == null || subPageEntity == undefined) {
                subPageEntity = new PageEntity();
            }
            subPageEntity.entityName = subEntityName;
            subPageEntity.tableName = subEntityInfo.TableNames;
            subPageEntity.bllProjectName = pageLoadParams.bllProjectName;
            subPageEntity.formParamKey = parentFieldName + "='" + parentFieldValue + "'&";
            subPageEntity.formCreateData = subColumns;
            subPageEntity.formEntityInfo = subEntityInfo;
            subPageEntity.formActionEditData = subActionForm;
            arrTempMap.put(subEntityName, subPageEntity);
            arrTempMap.put(containerId, subEntityName);
            var dataColumns = getFormatGridColumnsData(subColumns, false, subEntityName);

            if (subEntityInfo == undefined || subEntityInfo.length == 0 || subEntityInfo == null) return;

            ///类型
            var gType = subEntityInfo.EntityType == "" ? "1" : subEntityInfo.EntityType;
            var gParentField = subEntityInfo.ParentField;
            if (gParentField == undefined || gParentField == null) {
                gParentField = "";
            }
            ///排序
            if (subEntityInfo.DefaultSort == undefined || subEntityInfo.DefaultSort == null || subEntityInfo.DefaultSort == "") {
                gSortName = gSortName == "" ? "Id" : gSortName;
                gSortOrder = gSortOrder == "" ? "desc" : gSortOrder;
            } else {
                var defaultSortItem = subEntityInfo.DefaultSort.split("-");
                if (defaultSortItem.length > 1) {
                    gSortName = gSortName == "" ? defaultSortItem[0] : gSortName;
                    gSortOrder = gSortOrder == "" ? defaultSortItem[1] : gSortOrder;
                } else {
                    gSortName = gSortName == "" ? "Id" : gSortName;
                    gSortOrder = gSortOrder == "" ? "desc" : gSortOrder;
                }
            }
            ///数据地址
            if (dataUrl == "")
                dataUrl = XR.defaultProcessUrl() + "method=ajaxTables"
                    + "&ENTITY_NAME=" + subEntityName
                    + "&AN=BLL." + pageLoadParams.bllProjectName
                    + "&TABLE_NAMES=" + subTableName
                    + "&FOOTER_FLAG=" + subEntityInfo.FooterFlag
                    + "&FILTER=" + filterEncodeURIComponent(subFilter);
            ///权限
            var authFilters = getDataAuthByEntityAuthConfig(subEntityInfo.AuthConfig);
            if (authFilters != "") {
                dataUrl += "&AUTH_FILTER=" + authFilters;
            }

            if (gType == 3) {
                var IdField = "";
                if (gParentField == "")
                    IdField = "Fid";
                else
                    IdField = gParentField.split('-')[0];
                $('#' + containerId).datagrid({
                    methon: 'POST',
                    fitColumns: true,
                    autoRowHeight: true,
                    singleSelect: true,
                    pagination: true,
                    pageNumber: 1,
                    pageSize: 20,
                    pageList: [10, 20, 50, 100, 500],
                    idField: IdField,
                    sortName: gSortName,
                    sortOrder: gSortOrder,
                    columns: eval(dataColumns),
                    url: dataUrl,
                    view: detailview,
                    detailFormatter: function (index, row) {
                        return '<div style="padding:0px;"><table id="' + containerId + "_" + index + '"></table></div>';
                    },
                    onExpandRow: function (index, row) {
                        gridChoiceId = this.id;
                        tblGridId = this.id;
                        var expandRowValue = eval("row." + IdField);
                        createDetailGrid(subEntityInfo, containerId, containerId + "_" + index, expandRowValue, '');
                    },
                    onLoadSuccess: function (data) {
                        if (parentContainerId == "") return;
                        setTimeout(function () {
                            $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                            $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                            if (parentContainerId.split('_').length > 1) {
                                window.arr = new Array();
                                getParentIdAndIndexByParentId(parentContainerId);
                                if (arr.length > 0) {
                                    for (var i = 0; i < arr.length; i++) {
                                        var temparentItems = arr[i].split("-");
                                        $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                        $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                    }
                                }
                            }
                        }, 0);
                        formatterGridColumnsForComboGridData(containerId, data);
                    },
                    onBeforeSelect: function (index, row) {
                        if (this.id != gridChoiceId && gridChoiceId != "") {
                            $("#" + gridChoiceId).datagrid("clearSelections");
                        }
                    },
                    onSelect: function (index, row) {
                        gridChoiceId = this.id;
                        tblGridId = this.id;
                        var parentEntityName = arrTempMap.get(this.id);
                        var parentEntity = arrTempMap.get(parentEntityName);
                        var filed = parentEntity.formEntityInfo.ParentField;
                        var FieldListItem = filed.split(",");
                        for (var i = 0; i < FieldListItem.length; i++) {
                            var FieldList = FieldListItem[i].split("-");
                            if (FieldList.length > 2) {
                                var parentField = FieldList[0];
                                var subFileld = FieldList[1];
                                var subEntityName = FieldList[2];
                                var subTableName = FieldList[3];
                                var tempSubEntityName = arrTempMap.get(subEntityName);
                                if (tempSubEntityName == null || tempSubEntityName == undefined) {
                                    tempSubEntityName = new PageEntity();
                                    tempSubEntityName.entityName = subEntityName;
                                    tempSubEntityName.tableName = subTableName;
                                }
                                if (!isValidData(row)) return;

                                var rowValue = eval("row." + parentField);
                                tempSubEntityName.formParamKey = subFileld + "='" + rowValue + "'";
                                tempSubEntityName.formParamRowsData = row;
                                arrTempMap.put(subEntityName, tempSubEntityName);


                            } else {
                                var data = languageMessageData('0x00000064');//数据库没有配置当前点击GRID的子项数据，请前去配置
                                var title = languageMessageTitle('1x00000001');
                                tAlert("error", title, data);
                            }
                        }
                    },
                    onCollapseRow: function (index, row) {
                        if (parentContainerId != "") {
                            $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                            $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                            if (parentContainerId.split('_').length > 1) {
                                window.arr = new Array();
                                getParentIdAndIndexByParentId(parentContainerId);
                                if (arr.length > 0) {
                                    for (var i = 0; i < arr.length; i++) {
                                        var temparentItems = arr[i].split("-");

                                        $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                        $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                    }
                                }
                            }
                        }
                    }
                });
                if (containerId == "tblGrid") {
                    $('#' + containerId).datagrid({
                        height: setGridHeight()
                    });
                }
            }
            else if (gType == 2) {
                var gParentFields = gParentField.split('-');
                var treeFieldName = "";
                var PidField = "ParentFid";
                var IdField = "Fid";
                if (gParentFields.length > 2) {
                    IdField = gParentFields[0];
                    PidField = gParentFields[1];
                    treeFieldName = gParentFields[2];
                }

                $('#' + containerId).treegrid({
                    methon: 'POST',
                    rownumbers: true,
                    fitColumns: true,
                    collapsible: true,
                    animate: true,
                    autoRowHeight: true,
                    idField: IdField,
                    treeField: treeFieldName,
                    sortName: gSortName,
                    sortOrder: gSortOrder,
                    columns: eval(dataColumns),
                    url: dataUrl,
                    onLoadSuccess: function () {
                        if (parentContainerId != "") {
                            setTimeout(function () {
                                $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                                $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                                if (parentContainerId.split('_').length > 1) {
                                    window.arr = new Array();
                                    getParentIdAndIndexByParentId(parentContainerId);
                                    if (arr.length > 0) {
                                        for (var i = 0; i < arr.length; i++) {
                                            var temparentItems = arr[i].split("-");
                                            $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                            $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                        }
                                    }
                                }
                            }, 0);
                        }
                    },
                    onBeforeSelect: function (index, row) {
                        if (this.id != gridChoiceId && gridChoiceId != "") {
                            $("#" + gridChoiceId).datagrid("clearSelections");
                        }
                    },
                    onSelect: function (row, index) {
                        gridChoiceId = this.id;
                        tblGridId = this.id;
                        var gridSubEntityName = arrTempMap.get(this.id);
                        var sublistEntityList = arrTempMap.get(gridSubEntityName);
                        pageLoadParams.parentEntityName = gridSubEntityName;
                        sublistEntityList.formDataKey = row.Fid;
                        sublistEntityList.formParamKey = subFilter;
                        sublistEntityList.formParamRowsData = row;
                    },
                    loadFilter: function (data, parentId) {
                        return arrayToTree(data.rows, IdField, PidField);
                    },
                    onCollapseRow: function (index, row) {
                        if (parentContainerId != "") {
                            $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                            $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                            if (parentContainerId.split('_').length > 1) {
                                window.arr = new Array();
                                getParentIdAndIndexByParentId(parentContainerId);
                                if (arr.length > 0) {
                                    for (var i = 0; i < arr.length; i++) {
                                        var temparentItems = arr[i].split("-");

                                        $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                        $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                    }
                                }
                            }
                        }
                    }
                });
            }
            else {
                $('#' + containerId).datagrid({
                    methon: 'POST',
                    rownumbers: false,
                    fitColumns: true,
                    collapsible: true,
                    animate: true,
                    height: 'auto',
                    autoRowHeight: true,
                    singleSelect: true,
                    pagination: true,
                    pageNumber: 1,
                    pageSize: 20,
                    pageList: [10, 20, 50, 100, 500],
                    idField: 'Fid',
                    sortName: gSortName,
                    sortOrder: gSortOrder,
                    columns: eval(dataColumns),
                    url: dataUrl,
                    onLoadSuccess: function (data) {
                        if (parentContainerId != "") {
                            setTimeout(function () {
                                $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                                $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                                if (parentContainerId.split('_').length > 1) {
                                    window.arr = new Array();
                                    getParentIdAndIndexByParentId(parentContainerId);
                                    if (arr.length > 0) {
                                        for (var i = 0; i < arr.length; i++) {
                                            var temparentItems = arr[i].split("-");

                                            $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                            $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                        }
                                    }
                                }
                            }, 0);
                        }

                        formatterGridColumnsForComboGridData(containerId, data);

                    },
                    onBeforeSelect: function (index, row) {
                        if (this.id != gridChoiceId && gridChoiceId != "") {
                            $("#" + gridChoiceId).datagrid("clearSelections");
                        }
                    },
                    onSelect: function (index, row) {
                        gridChoiceId = this.id;
                        tblGridId = this.id;
                        var parentEntityName = arrTempMap.get(this.id);
                        var parentEntity = arrTempMap.get(parentEntityName);
                        var filed = parentEntity.formEntityInfo.ParentField;
                        if (filed != null && filed != undefined && filed != "") {
                            var FieldListItem = filed.split(",");
                            for (var i = 0; i < FieldListItem.length; i++) {
                                var FieldList = FieldListItem[i].split("-");
                                if (FieldList.length > 2) {
                                    var parentField = FieldList[0];
                                    var subFileld = FieldList[1];
                                    var subEntityName = FieldList[2];
                                    var subTableName = FieldList[3];
                                    var tempSubEntityName = arrTempMap.get(subEntityName);
                                    if (tempSubEntityName == null || tempSubEntityName == undefined) {
                                        tempSubEntityName = new PageEntity();
                                        tempSubEntityName.entityName = subEntityName;
                                        tempSubEntityName.tableName = subTableName;
                                    }
                                    var rowValue = eval("row." + parentField);
                                    tempSubEntityName.formParamKey = subFileld + "='" + rowValue + "'";
                                    tempSubEntityName.formParamRowsData = row;
                                    arrTempMap.put(subEntityName, tempSubEntityName);


                                } else {
                                    var data = languageMessageData('0x00000064');//数据库没有配置当前点击GRID的子项数据，请前去配置
                                    var title = languageMessageTitle('1x00000001');
                                    tAlert("error", title, data);
                                }
                            }
                        }
                    },
                    onCollapseRow: function (index, row) {
                        if (parentContainerId != "") {
                            $('#' + parentContainerId).datagrid('fixDetailRowHeight', parentIndex);
                            $('#' + parentContainerId).datagrid('fixRowHeight', parentIndex);
                            if (parentContainerId.split('_').length > 1) {
                                window.arr = new Array();
                                getParentIdAndIndexByParentId(parentContainerId);
                                if (arr.length > 0) {
                                    for (var i = 0; i < arr.length; i++) {
                                        var temparentItems = arr[i].split("-");

                                        $('#' + temparentItems[0]).datagrid('fixDetailRowHeight', temparentItems[1]);
                                        $('#' + temparentItems[0]).datagrid('fixRowHeight', temparentItems[1]);
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }
    }, 'json');
};

///JS-027加载左侧用户菜单
function createLeftMenuOfTree() {
    var roleFid = $.cookie.get('cookieUserRoleFid');
    var dataParams = "method=ajaxMenu&ROLE_FID=" + roleFid;
    ///开始数据加载
    openMessagerProgress();
    $.ajax({
        url: XR.defaultProcessUrl(),
        async: false,
        type: "POST",
        data: dataParams,
        dataType: 'json',
        success: function (data) {
            window.menuMap = new Map();
            ///完成数据加载
            closeMessagerProgress();
            sessionIsNull(data);
            if (isSessionNull) return;
            if ($('#accordion').accordion('panels') != '') { }
            var taregeData = arrayIconClsToTree(data, 'Fid', 'ParentMenuFid', 'ImageUrl');
            for (var i = 0; i < taregeData.length; i++) {
                if (!taregeData || !taregeData.length) return [];
                var tUl = '<ul class="easyui-tree" id="tree' + taregeData[i].Fid + '"></ul>';
                var sel = i == 0 ? true : false;
                var nText = XR.language ? taregeData[i].MenuName : taregeData[i].MenuNameCn;
                $('#accordion').accordion('add', {
                    title: nText,
                    content: tUl,
                    selected: sel
                });
                $('#tree' + taregeData[i].Fid).tree({
                    data: taregeData[i].children,
                    formatter: function (node) {
                        var nText = XR.language ? node.MenuName : node.MenuNameCn;
                        menuMap.put(node.Fid, node);
                        return nText;
                    },
                    onLoadSuccess: function (node, data) {
                        ///加载菜单完成后显示欢迎页面
                        document.getElementById('homePageIframe').src = "welcom.html";
                    },
                    onClick: function (node) {
                        ///没有子节点时
                        if (!isValidData(node.children)) {
                            var nText = XR.language ? node.MenuName : node.MenuNameCn;
                            addPanel(node.Fid, node.LinkUrl, nText);
                        }
                        ///否则展开
                        else {
                            $(this).tree('expand', node.target);
                        }
                    }
                });
            }
        }
    });
};
///JS-028创建COMBOBOX控件
function createCombobox(cboId, cboData, valField, txtField, cboSelectMath, cboLoadSuccess, cboWidth, cboRequired) {
    $('#' + cboId).combobox({
        data: cboData,
        width: cboWidth,
        required: cboRequired,
        valueField: valField,
        textField: txtField,
        onSelect: eval(cboSelectMath),
        onLoadSuccess: function (data) {
            eval(cboLoadSuccess + "(data)")
        }
    });
};
///JS-029创建TEXTBOX控件
function createTextbox(tId, tWidth, tDefaultVal, isReadonly, isHide) {
    $('#' + tId).textbox({
        width: tWidth,
        disabled: isReadonly,
        value: tDefaultVal,
    });
    if (isHide) {
        $('#' + tId).hide();
    }
}
///JS-030 获取是否多选网格
function isGridMuliCheck(parentField) {
    if (!isValidData(parentField)) return false;
    var parentFields = parentField.split('-');
    var isCheckBox = 'false';
    ///如果配置条目大于3项，则获取第四项作为是否多选的标记，否则第一项即为该标记
    if (parentFields.length > 3) {
        isCheckBox = parentFields[3];
    }
    else if (parentFields.length > 0) {
        isCheckBox = parentFields[0];
    }
    ///只有true才能作为真正的多选标记
    return (isCheckBox + '').toLowerCase() == 'true' ? true : false;
}
///
function fixGridRowHeight(cContainerId) {
    var pContainerId = cContainerId;
    ///含有子项
    while (pContainerId.lastIndexOf('_') > 0) {
        var pContainerIdIndex = getParentContainerId(pContainerId);
        var pContainerIds = pContainerIdIndex.split('-');
        if (pContainerIds.length == 1) break;
        ///
        pContainerId = pContainerIds[0];
        ///行index
        var pContainerIndex = pContainerIds[1];
        $('#' + pContainerId).datagrid('fixDetailRowHeight', pContainerIndex);
        $('#' + pContainerId).datagrid('fixRowHeight', pContainerIndex);
    }
    $('#' + pContainerId).datagrid('fixDetailRowHeight');
    $('#' + pContainerId).datagrid('fixRowHeight');
}
///获取上一层ParentContainerId
function getParentContainerId(cContainerId) {
    ///无效直接返回
    if (!isValidData(cContainerId)) return '';
    ///p_1_1格式解析
    var cContainerIds = cContainerId.split('_');
    if (cContainerIds.length == 1) return cContainerId;
    ///加上^是为防止多层情况下index相同
    var parentIndex = cContainerIds[cContainerIds.length - 1];
    cContainerId += '^';
    var pContainerId = cContainerId.replace('_' + parentIndex + '^', '');
    return pContainerId + '-' + parentIndex;
}

function getParentIdAndIndexByParentId(parentIds) {
    var parentItems = parentIds.split('_');
    var parentIndex = parentItems[parentItems.length - 1];
    parentIds += "^";
    var parentId = parentIds.replace("_" + parentIndex + "^", "");
    arr.push(parentId + "-" + parentIndex);
    if (parentId.split('_').length > 1) { getParentIdAndIndexByParentId(parentId); }
}
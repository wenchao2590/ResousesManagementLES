var pageLoadParams = new PageEntity();
$(function () {
    //加载后台页面数据
    pageLoadParams.AjaxCreateFromDataByActionOrEntityData("Help", "TS_SYS_HELP", "createHelpInit();");
});

//入口方法 
function createHelpInit() {
    var parentArrMap = parent.arrGlobal.get("Help");
    //页面初始化
    FORM.createInitForm(parentArrMap.formActionEditData, parentArrMap.formSearchEditData);
    $('#' + divActionMenuId).hide();
    $('#' + divSearchFormId).hide();
    $('#' + divControlFormId).hide();
    $('#divTableGridId').hide();
    //创建页面
    var tabsNone = parent.$('#tt').tabs('getSelected');
    var tabsMenuFid = tabsNone[0].id;
    var dataParams = "method=ajaxTables&FILTER= AND MENU_FID='" + tabsMenuFid + "'&TABLE_NAMES=TS_SYS_HELP&ENTITY_NAME=Help&AN=BLL.SYS";
    openMessagerProgress();
    $.ajax({
        url: XR.defaultProcessUrl(),
        async: false,
        type: "POST",
        data: dataParams,
        dataType: 'json',
        success: function (data) {
            closeMessagerProgress();
            if (data != null && data != undefined) {
                createSystemHelpViewPage("formItem", parentArrMap.formCreateData, JSON.stringify(data.rows[0]));
            }
        }
    });
}


function createSystemHelpViewPage(containerName, corJson, dataJson) {
    if (dataJson != "" && dataJson != undefined && dataJson != null) {
        dataJson = eval('(' + dataJson + ')');
    }
    if (corJson == "" || corJson == undefined || corJson == null) {
        corJson = corJsonDemo;
    }
    var i = XR.language == true ? 2 : 1;
    var bindName = corJson[i].FieldName;
    var tText = XR.language == true ? corJson[i].DisplayNameEn : corJson[i].DisplayNameCn;
    var tType = corJson[i].ControlType;
    var tWidth = corJson[i].EditDisplayWidth;
    var isdisplay = corJson[i].Editable;
    var defaultVal = corJson[i].DefaultValue;

    var formWidth = $("#formItem").width();
    tWidth = (formWidth * 1) - 20;

    var windowHeight = $(window).height();
    var tHeight = windowHeight - 30;

    var tId = tType + "-" + bindName;

    if (dataJson != "" && dataJson != undefined && dataJson != null) {
        defaultVal = dataJson[bindName];
    }
    var tDemo = $("<div name='submitColumn' style='height:" + tHeight + "px' ></div>");
    tDemo.attr('id', tId);
    tDemo.attr('des', tText);

    var tDemoDiv = $("<div style='width:" + tWidth + "px;height:" + tHeight + "px;margin:0 auto;position:relative;'></div>");
    tDemoDiv.attr('id', "editContainer-" + tId);
    $('#' + containerName).append(tDemoDiv);
    $('#' + "editContainer-" + tId).append(tDemo);

    tText = languageMessageData('0x00000093');
    if (tId == "120-HelpContextCn") {
        if (defaultVal != "") {
            $('#' + tId).html(unescape(defaultVal));
        }
    } else if (tId == "120-HelpContextEn") {
        if (defaultVal != "") {
            $('#' + tId).html(unescape(defaultVal));
        }
    }
}
///MAIN.PAGE键值对
var arrGlobal = new Map();
var arrUserRole = new Map();
var arrUserRoleDataAuth = new Map();
//构造函数
$(function () {
    var languageData = [];
    $.getJSON('JS/data/common.data.json', function (data) {
        languageData = data.langu;
        FORM.createCombobox('cboLanguage', languageData, 'id', 'text', 'onChangeLanguage', 'onLanguSuccess', '80', false);
    });
    $.getJSON('JS/data/common.data.json', function (data) {
        FORM.createCombobox('cboThemes', data.themes, 'id', 'text', 'changeTheme', 'onThemesSuccess', '100', true);
    });
});
//语言选项加载成功
function onLanguSuccess(data) {
    if ($.cookie.get('language') != '') {
        $('#cboLanguage').combobox('setValue', $.cookie.get('language'));// zh - cn
    }
    else {
        $.cookie.set('language', 'zh-cn', 7);
        $('#cboLanguage').combobox('setValue', $.cookie.get('language'));
    }
    //加载角色选项
    var dataParamRoles = "method=ajaxRoles&ENTITY_NAME=Roles&AN=BLL.SYS&language=" + $.cookie.get('language');
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParamRoles, 'js:formCreateRoles', false);
}
///主题从cookie.themes中获取，首次默认default，时长7天
function onThemesSuccess(data) {
    if (!isValidData($.cookie.get('themes'))) {
        $.cookie.set('themes', 'bootstrap', 7);
    }
    $('#cboThemes').combobox('setValue', $.cookie.get('themes'));
}
//加载角色选项
function formCreateRoles(data) {
    data = eval('(' + data + ')');
    arrUserRole.put("UserRoleList", data);
    FORM.createCombobox('cboRole', data, 'GuidValue', 'StringDisplay', 'onChanageRole', 'onRoleSuccess', '120', false);
    $('#accordion').accordion({
        animate: true
    });
    createLeftMenuOfTree();
    languageInit();
}
//角色选项加载成功
function onRoleSuccess(data) {
    if (data.length == 0) return;
    if (isValidData($.cookie.get('cookieUserRoleFid'))) {
        for (var i = 0; i < data.length; i++) {
            if ($.cookie.get('cookieUserRoleFid') != data[i].GuidValue) continue;
            $('#cboRole').combobox('select', $.cookie.get('cookieUserRoleFid'));
        }
    }
    if (!isValidData($('#cboRole').combobox('getValue'))) {
        $.cookie.set('cookieUserRoleFid', data[0].GuidValue);
        $('#cboRole').combobox('select', data[0].GuidValue);
    }
}
//角色选项变更时
function onChanageRole(roledata) {
    var dataParamUserRoles = "method=getAuthCondition&AN=BLL.SYS&roleFid=" + roledata.GuidValue;
    HELP.ajaxCommon(XR.defaultProcessUrl(), dataParamUserRoles, 'js:putArrUserRoles', false);
    if (isValidData($.cookie.get('cookieUserRoleFid')) && $.cookie.get('cookieUserRoleFid') != roledata.GuidValue) {
        $.cookie.set('cookieUserRoleFid', roledata.GuidValue);
        window.location.reload();
    } else {
        $.cookie.set('cookieUserRoleFid', roledata.GuidValue);
    }
}
//USER_ROLE相关数据存放到页面缓存中
function putArrUserRoles(data) {
    //用户角色数据
    data = eval('(' + data + ')');
    //所属组织FID、CODE
    arrUserRoleDataAuth.put("OrganizationFid", data.OrganizationFid);
    ///
    arrUserRoleDataAuth.put("LoginUser", data.LoginUser);
    for (var i = 0; i < data.RangeName.length; i++) {
        var rangeName = data.RangeName[i];
        arrUserRoleDataAuth.put(rangeName, eval("data." + rangeName));
    }
}
//界面初始化
function languageInit() {
    var loginTime = languageMessageData('1x00000005');
    var help = languageMessageData('1x00000006');
    var exit = languageMessageData('1x00000007');
    var homePage = languageMessageData('1x00000008');
    var updatePassWord = languageMessageData('1x00000009');
    var favorites = languageMessageData('1x00000010');
    var closeAll = languageMessageData('1x00000011');

    $('#loginTime').html(loginTime + ":" + getNowDate());
    $('#a_Favorites').html(favorites);
    $('#a_CloseAll').html(closeAll);
    $('#homePage').html(homePage);

    $('#btnUpdatePassWord').linkbutton({
        iconCls: 'icon-save',
        text: updatePassWord,
        width: 88
    }).bind('click', function () {
        var tlYes = languageMessageData('1x00000046');///确定
        var tlNo = languageMessageData('1x00000048');///取消
        $('#winPassWord').dialog({
            title: updatePassWord,
            width: 330,
            height: 160,
            closed: false,
            cache: false,
            modal: true,
            buttons: [{
                text: tlYes,
                iconCls: 'icon-back',
                handler: function () {
                    var pwd1 = $('#psw1').textbox('getValue');
                    var pwd2 = $('#psw2').textbox('getValue');
                    var psw = "";
                    var msg1 = languageMessageData('1x00000014');
                    var msg2 = languageMessageData('1x00000015');
                    var title = languageMessageData('1x00000001');
                    if (isValidData(pwd1) && isValidData(pwd2)) {
                        if (pwd1 == pwd2) {
                            var dataParam = "method=setDefaultPassWord&password=" + pwd1 + "&AN=SYS";
                            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParam, 'js:updatePswResult');
                        } else {
                            tAlert("error", title, msg1);
                        }
                    } else {
                        tAlert("error", title, msg2);
                    }
                }
            },
            {
                text: tlNo,
                iconCls: 'icon-remove',
                handler: function () {
                    parent.$('#winPassWord').dialog('close');
                }
            }]
        });
    });
    $('#btnhelp').linkbutton({
        iconCls: 'icon-search',
        text: help,
        width: 60
    }).bind('click', function () {
        var tabsNone = $('#tt').tabs('getSelected');
        var tabsMenuFid = tabsNone[0].id;
        if (tabsMenuFid != "") {
            var dataParams = "method=getMenu&MenuFid=" + tabsMenuFid + "&ENTITY_NAME=Menu&AN=BLL.SYS";
            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:bindbtnHelpOnClick', false);
        }
    });
    //BTN_EXIT
    $('#btnexit').linkbutton({
        iconCls: 'icon-back',
        text: exit,
        width: 60
    }).bind('click', function () {
        $.cookie.set('cookiePlantFid', "");
        $.cookie.set('cookiePlantCode', "");
        $.cookie.set('cookieUserRoleFid', "");
        window.location = loginUrl;
    });
}
///
function updatePswResult(data) {
    alertMessage(data);
    $('#winPassWord').dialog('close');
}
//获取当前时间
function getNowDate() {
    var dt = new Date();
    var m = dt.getMonth() + 1;
    var d = dt.getDate();
    return dt.getFullYear() + "-" + m + "-" + d + " " + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
}
//帮助按钮点击事件
function bindbtnHelpOnClick(data) {
    data = eval('(' + data + ')');
    if (isValidData(data) && data.MenuName != "Help") {
        var nText = XR.language == true ? data.MenuName + "Help" : data.MenuNameCn + "帮助";
        var rowsLinkUrl = "SystemHelp.aspx?help&TS_SYS_HELP";
        addPanel(data.Fid, rowsLinkUrl, nText);
    }
}
//关闭全部选项卡
function removePanel() {
    var tab = $('#tt').tabs('getSelected');
    if (tab) {
        var index = $('#tt').tabs('getTabIndex', tab);
        if (index > 0) {
            $('#tt').tabs('close', index);
            removePanel();
        }
    }
}
///添加收藏事件
function addFavorites() {
    var tab = $('#tt').tabs('getSelected');
    if (tab) {
        var menuFid = tab[0].id;
        if (isValidData(menuFid)) {
            var dataParams = 'method=addUserFavorite&ENTITY_NAME=UserFavorites&AN=SYS&menuFid=' + menuFid;
            HELP.ajaxCommon(XR.defaultProcessUrl(), dataParams, 'js:favoriteAdded', false);
        }
    }
}
///添加收藏完成时
function favoriteAdded(data) {
    alertMessage(data);
}

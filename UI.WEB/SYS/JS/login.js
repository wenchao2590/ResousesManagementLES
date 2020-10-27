//login界面初始化
$(function () {
    var languageData = [];
    $.getJSON('JS/data/common.data.json', function (data) {
        languageData = data.langu;
        FORM.createCombobox('language', languageData, 'id', 'text', 'onChangeLanguage', 'onLanguSuccess', 200, false);
    });
    $('#username').textbox({
        prompt: languageMessageTitle('0x00000110'),
        iconCls: 'icon-man',
        iconWidth: 38,
        width: 200
    });
    $('#username').textbox('textbox').keydown(function (e) {
        if (e.keyCode == 13) {
            login();
        }
    });
    $('#password').textbox({
        prompt: languageMessageTitle('0x00000111'),
        iconCls: 'icon-lock',
        iconWidth: 38,
        width: 200
    });
    $('#password').textbox('textbox').keydown(function (e) {
        if (e.keyCode == 13) {
            login();
        }
    });
    $('#login').linkbutton({
        iconCls: 'icon-0000009',
        text: languageMessageTitle('0x00000112'),
        width: 200,
        height: 40
    }).bind('click', function () {
        login();
    });
});
///登录界面相关控件显示部分在语言选项加载完成时触发加载
function onLanguSuccess(data) {
    if (isValidData($.cookie.get('language'))) {
        $('#language').combobox('setValue', $.cookie.get('language'));
    }
    else {
        $.cookie.set('language', 'zh-cn', 7);
        $('#language').combobox('setValue', $.cookie.get('language'));
    }
}
//登录按钮事件
function login() {
    var user = $('#username').textbox('getValue');
    var pass = $('#password').textbox('getValue');
    var lang = $('#language').combobox('getValue');
    var dataParam = "method=ajaxUserLogin&username=" + user + "&password=" + pass + "&language=" + lang;
    $.ajax({
        url: XR.defaultProcessUrl(),
        async: false,
        type: "POST",
        data: dataParam,
        dataType: 'json',
        success: function (data) {
            if (isValidData(data))
                window.location = 'Main.aspx';
        },
        error: function (err) {
            tAlert('error', languageMessageTitle('1x00000004'), err.responseText, 500, 200);
        }
    });
    return false;
};
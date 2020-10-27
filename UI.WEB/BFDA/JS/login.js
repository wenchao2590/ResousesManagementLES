//login界面初始化
$(function () {
    var languageData = [];
    $.getJSON('JS/data/common.data.json', function (data) {
        languageData = data.langu;
        FORM.createCombobox('language', languageData, 'id', 'text', 'onChangeLanguage', 'onLanguSuccess', 200, false);
    });
    FORM.createTextbox('username', 200, '', false, false);
    FORM.createTextbox('password', 200, '', false, false);
});
//登录界面相关控件显示部分在语言选项加载完成时触发加载
function onLanguSuccess(data) {
    if ($.cookie.get('language') != '') {
        $('#language').combobox('setValue', $.cookie.get('language'));
    }
    else {
        $.cookie.set('language', 'zh-cn', 7);
        $('#language').combobox('setValue', $.cookie.get('language'));
    }
    $('#cboLanguage').combobox('setValue', $.cookie.get('language'));
    $('#username').textbox({ prompt: languageMessageTitle('0x00000110') });
    $('#password').textbox({ prompt: languageMessageTitle('0x00000111') });
    $('#login').linkbutton({
        iconCls: 'icon-0000009',
        text: languageMessageTitle('0x00000112')
    });
    $('#cboLanguage').combobox({
        onShowPanel: function () {
            $(this).combobox('panel').height("auto");
        }
    });
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
//回车触发登录事件
function keyPressLogin() {
    if (window.event.keyCode == 13) {
        $('#login').click();
        return false;
    }
}
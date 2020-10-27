<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title></title>
    <link id="themesId" rel="stylesheet" type="text/css" href="JS/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="JS/themes/icon.css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <script type="text/javascript" src="JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="JS/locale/easyui-lang-zh_CN.js"></script>
    <script src="JS/common/common.create.control.js" type="text/javascript"></script>
    <script src="JS/common/common.help.content.js" type="text/javascript"></script>
    <script src="JS/common/common.helper.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/common/main.helper.js"></script>
</head>
<body class="easyui-layout">
    <div data-options="region:'north',border:false" style="height: 50px; background-image: url('css/img/top.jpg'); padding: 1px">
        <div style="float: right; margin-top: 10px">
            <span>
                <input id="cboThemes" type="text" /></span>
            <span>
                <input id="cboRole" type="text" /></span>
            <span>
                <input id="cboLanguage" type="text" /></span>
            <span>
                <a id="btnUpdatePassWord" href="#" style="width: 50px" data-options="plain:true"></a></span>
            <span>
                <a id="btnhelp" href="#" data-options="plain:true"></a></span>
            <span>
                <a id="btnexit" href="#" data-options="plain:true"></a></span>
        </div>
        <div>
            <img src="css/img/bfda.png" style="width: 161px; height: 48px;" />
        </div>
    </div>
    <div data-options="region:'west',split:true,title:''" style="width: 150px;">
        <div id="accordion" class="easyui-accordion" data-options="fit:true,border:false">
        </div>
    </div>
    <div data-options="region:'center'" style="border: none">
        <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="width: 100%; height: 100%;">
            <div title="<span id='homePage'></span>" id="">
                <iframe src="" frameborder="0" id="homePageIframe" name="iframepage" style="width: 100%; height: 99%;"></iframe>
            </div>
        </div>
        <div id="tab-tools">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-tip'" onclick="addFavorites()"><span id="a_Favorites">收藏</span>&nbsp;&nbsp;</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-clear'" onclick="removePanel()"><span id="a_CloseAll">关闭</span>&nbsp;&nbsp;</a>
        </div>

    </div>

    <div data-options="region:'south',border:false" style="height: 30px; background-image: url('css/img/top.jpg'); padding: 5px;" align="right">
        <div style="float: left" id="loginTime"></div>
        Copyright Microsoft Corp.2018
    </div>

    <%-- 模态窗体 --%>
    <div id="dialogModal"></div>
    <%-- 对话框 --%>
    <div id="dialogLayout" class="easyui-window" title="<span id='sDialogLayout'></span>" data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,iconCls:'icon-save'" style="width: 500px; height: 250px; padding: 10px; display: none;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center'" style="padding: 10px;">
            </div>
            <div id="dd" data-options="region:'south',border:false" style="text-align: right; padding: 5px 0 0;">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" href="javascript:void(0)" onclick="javascript:ok()" style="width: 80px">Ok</a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" href="javascript:void(0)" onclick="javascript:cancel()" style="width: 80px">Close</a>
            </div>
        </div>
    </div>
    <%-- 多行文本 --%>
    <div id="dialogMulText"></div>

    <div id="winPassWord" style="display: none; font-family: '微软雅黑';">
        <br />
        &nbsp;&nbsp; 新密码：       
        <input class="easyui-textbox" id="psw1" style="width: 220px" type="password" /><br />
        <br />
        &nbsp;&nbsp; 再输入：
        <input class="easyui-textbox" id="psw2" style="width: 220px" type="password" /><br />
    </div>
</body>
</html>

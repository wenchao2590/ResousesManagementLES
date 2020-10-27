<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html style="height: 100%">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link id="themesId" rel="stylesheet" type="text/css" href="JS/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="JS/themes/icon.css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <script type="text/javascript" src="JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="JS/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="JS/common/common.create.control.js"></script>
    <script type="text/javascript" src="JS/common/common.help.content.js"></script>
    <script type="text/javascript" src="JS/common/common.helper.js"></script>
    <link rel="Stylesheet" type="text/css" href="SIOM/Css/login.css" />
    <script type="text/javascript" src="SYS/JS/login.js"></script>
</head>
<body onkeydown="return keyPressLogin();">
    <img src="SIOM/Images/login.png" style="width: 908px; height: 321px; position: relative; top: 180px; left: 160px; z-index: -1" />
    <div class="loginImage">
        <div class="loginBox">
            <div class="loginDiv">
                <table>
                    <tr>
                        <td>
                            <div class="loginLogo"></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="language" type="text" /></td>
                    </tr>
                    <tr>
                        <td>
                            <input id="username" type="text" data-options="prompt:'Username',iconCls:'icon-man',iconWidth:38" /></td>
                    </tr>
                    <tr>
                        <td>
                            <input id="password" type="password" data-options="prompt:'Password',iconCls:'icon-lock',iconWidth:38" /></td>
                    </tr>
                    <tr>
                        <td>
                            <a href="#" id="login" class="easyui-linkbutton" style="width: 200px; height: 40px;" onclick="return login();"></a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>


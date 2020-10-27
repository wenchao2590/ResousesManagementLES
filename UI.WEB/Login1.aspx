<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>MES系统登录界面</title>
    <meta charset="utf-8" />
    <link rel="stylesheet" type="text/css" href="JS/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="JS/themes/icon.css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <script type="text/javascript" src="JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="JS/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="JS/common/common.create.comtrol.js"></script>
    <script src="JS/common/common.help.content.js" type="text/javascript"></script>
    <script src="JS/common/common.helper.js" type="text/javascript"></script>
    <link rel="Stylesheet" type="text/css" href="CSS/login.css" />
    <script src="SYS/JS/login.js" type="text/javascript"></script>
</head>
<body>
    <div class="loginImage">
        <div class="loginBox">
            <div class="loginDiv">
                <table>
                    <tr>
                            <td>
                               <label id="lbLanguare"></label></td>
                            <td>
                                  <input id="cboLanguage" type="text" /></td>
                        </tr>
                    <tr>
                        <td>
                            <label id="lbFactory"></label>
                        </td>
                        <td>
                            <input id="cboPlant" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <label id="lbUserName"></label>
                        </td>
                        <td>
                            <input id="username" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <label id="lbPassword"></label>
                        </td>
                        <td>
                            <input id="password" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <a id="btn" href="#"></a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>

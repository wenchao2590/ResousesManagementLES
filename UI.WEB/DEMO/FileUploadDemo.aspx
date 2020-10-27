<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link id="themesId" rel="stylesheet" type="text/css" href="../JS/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../JS/themes/icon.css" />
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../JS/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../JS/common/common.create.control.js"></script>
    <script type="text/javascript" src="../JS/common/common.help.content.js"></script>
    <script type="text/javascript" src="../JS/common/common.helper.js"></script>
    <script src="../JS/common/commonEdit.helper.js"></script>
</head>
<body>
    <div style="margin-bottom: 20px">
        <div>File1:</div>
        <form id="uploadFiless" method="post" enctype="multipart/form-data">
            <input class="easyui-filebox" name="file1" data-options="prompt:'Choose a file...'" style="width: 100%" />
        </form>
    </div>
    <div>
        <a href="#" class="easyui-linkbutton" style="width: 100%" onclick="uploadFiles();">Upload</a>
    </div>

</body>
</html>

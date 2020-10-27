<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="../JS/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../JS/themes/icon.css" />


    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../JS/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../JS/common/common.help.content.js"></script>
    <script type="text/javascript" src="../JS/common/common.helper.js"></script>



    <script type="text/javascript">
        var splitChar = "-";
        var nowChioceObj = null;
        function createFilterControl(filterControlStr) {
            if (filterControlStr != "") {
                var filterControlItem = filterControlStr.split("|");
                var filterControl = document.getElementById("Div_filterControl");
                Div_filterControl.parentNode.style.display = "block";
                filterControl.innerHTML = "<br/>";
                var D = new Date();
                var y = D.getFullYear();
                var m = D.getMonth() + 1;
                m = m * 1 < 10 ? "0" + m : m;
                //  m = m - 1;
                var d = D.getDate();
                var dateNow = y + '-' + m + '-' + d;
                var controlIdItem = "";
                for (var i = 0; i < filterControlItem.length; i++) {
                    var filterItem = filterControlItem[i].split(",");
                    var title = filterItem[0];
                    var filed = filterItem[1];
                    var type = filterItem[2];
                    var symbol = filterItem[3];

                    symbol = symbol.replace("=", "a⊙").replace("<=", "b⊙").replace(">=", "c⊙").replace("<", "d⊙").replace(">", "e⊙");
                    var controlType = filterItem[4];
                    var ContainerDiv = document.createElement("div");
                    ContainerDiv.className = "containerDivCls";
                    var spanTitle = document.createElement("span");
                    spanTitle.innerHTML = title + "&nbsp;:&nbsp;";
                    var input = document.createElement("input");
                    controlType = controlType == "text" ? "textbox" : controlType;
                    input.id = type + splitChar + filed + splitChar + symbol + splitChar + controlType;
                    controlIdItem += input.id + "^";
                    ContainerDiv.appendChild(spanTitle);
                    ContainerDiv.appendChild(input);
                    filterControl.appendChild(ContainerDiv);
                    switch (controlType) {
                        case "datebox":
                            $('#' + input.id).datebox({
                                required: true,
                                width: 150
                            });
                            $('#' + input.id).datebox('setValue', dateNow);
                            break;
                        case "textbox":
                            $('#' + input.id).textbox({
                                width: 150
                            });
                            break;
                        default:
                            break;

                    }
                }
                var div = document.createElement("div");
                div.className = "containerDivCls";
                filterControl.appendChild(div);
                controlIdItem = controlIdItem.length > 0 ? controlIdItem.substring(0, controlIdItem.length - 1) : controlIdItem;
                var button = document.createElement("a");
                button.id = "btn";
                button.setAttribute("onclick", "selectMedthod('" + controlIdItem + "')");
                button.innerHTML = "查询"
                div.appendChild(button);
                $('#btn').linkbutton({
                    iconCls: 'icon-search'
                });
            }
        }
        function selectMedthod(controlIdItem) {
            var filterStr = "";
            var valueItem = controlIdItem.split("^");
            for (var i = 0; i < valueItem.length; i++) {
                var filter = valueItem[i].split(splitChar);
                var type = filter[0];
                var filed = filter[1];
                var symbol = filter[2];
                symbol = symbol.replace("a⊙", "=").replace("b⊙", "<=").replace("c⊙", ">=").replace("d⊙", "<").replace("e⊙", ">");
                var controlType = filter[3];
                var val = eval("$('#" + valueItem[i] + "')." + controlType + "('getValue')");
                // val = controlType == "text" ? val[0].value : val;
                if (val != "") {
                    filterStr += " AND " + filed + "   " + symbol + " @" + val + "@    ";
                    if (symbol == "like") {
                        filterStr = filterStr.replace("@", "'%").replace("@", "%'");
                    } else {
                        var isStr = type == "string" ? "'" : "";
                        filterStr = filterStr.replace(/@/g, isStr);
                    }
                }

            }
            if (nowChioceObj != null && nowChioceObj != undefined && nowChioceObj != "") {
                var sqlString = nowChioceObj.SQL_STRING;

                sqlString = sqlString + " where 1=1 " + filterStr + nowChioceObj.SORT_FIELD;
                var dataParams = "ENTITY_NAME=Report&AN=BLL.SYS&sqlString=" + sqlString + "&method=GetTableData";
                $.ajax({
                    url: "../HANDLER/mainHandler.ashx?",
                    async: false,
                    type: "POST",
                    data: dataParams,
                    success: function (data) {
                        if (data.indexOf("SessionIsNull") != -1) {
                            tAlert("alert", "提示", "Login timeOut/登录超时");
                            parent.window.location = "../login.aspx";
                        } else if (data.indexOf("Err_") != -1) {
                            tAlert("error", "提示", data[0].replace("Err_"));
                        } else {
                            data = data.split("⊙");
                            if (data.length == 2) {
                                var gridCol = data[1];
                                var gridData = data[0]
                                gridData = eval('(' + gridData + ')');
                                gridCol = eval('(' + gridCol + ')');
                                createGrid(gridData, gridCol);

                            }
                        }
                    }
                });

            }
        }
        function createGrid(gridData, gridCol) {
            var span = document.getElementById("msg");
            span.innerHTML = "";
            document.getElementById("container").parentNode.style.display = "block";
            var title = nowChioceObj.NAME;
            document.getElementById("title").innerHTML = nowChioceObj.NAME;
            if ((gridData.total + "") == "0") {

                span.innerHTML = "没有查询到数据";
                span.style.color = "red";

            }
            $('#grid').datagrid({
                columns: [gridCol],
                pagination: true,
                pageNumber: 1,
                pageSize: 12,
                pageList: [12, 24, 36, 48, 60],
                data: gridData.rows.slice(0, 12)
            });
            var pager = $('#grid').datagrid("getPager");
            pager.pagination({
                total: gridData.rows.length,
                onSelectPage: function (pageNo, pageSize) {
                    var start = (pageNo - 1) * pageSize;
                    var end = start + pageSize;
                    $('#grid').datagrid("loadData", gridData.rows.slice(start, end));
                    pager.pagination("refresh", { total: gridData.rows.length, pageNumber: pageNo });
                }
            })

            // $('#grid').datagrid('loadData', gridData);


        }
        function LoadAllData(data) {
            data = eval('(' + data + ')');
            $('#cc').combogrid("grid").datagrid('loadData', data);

        }
        function pageInit() {
            $('#cc').combogrid({

                mode: 'remote',
                idField: 'FID',
                textField: 'NAME',
                onChange: function (newValue, oldValue) {
                    var g = $('#cc').combogrid('grid');
                    var data = g.datagrid('getSelected');
                    if (data != null) { 
                    createFilterControl(data.KEY_FIELD, data);
                    nowChioceObj = data;
                    }
                },

                columns: [[
                    { field: 'FID', title: 'Code', width: 180, sortable: true },
                    { field: 'NAME', title: 'Name', width: 400, sortable: true }
                ]]
            });
            getTable("", "LoadAllData");
            document.getElementById("Div_filterControl").parentNode.style.display = "none";
            document.getElementById("container").parentNode.style.display = "none";
        }
        function getTable(fid, jsMedthodName) {
            var dataParams = "method=getReport&ENTITY_NAME=Report&AN=BLL.SYS&FID=" + fid;
            $.ajax({
                url: "../HANDLER/mainHandler.ashx?",
                async: false,
                type: "POST",
                data: dataParams,
                datatype: 'json',
                success: function (data) {
                    if (data.indexOf("SessionIsNull") != -1) {
                        tAlert("alert", "提示", "Login timeOut/登录超时");
                        parent.window.location = "../login.aspx";
                    } else if (data.indexOf("Err_") != -1) {
                        tAlert("error", "提示", data[0].replace("Err_"));
                    } else {
                        data = data.replace(/'/g, "\"");
                        var medthod = jsMedthodName + "('" + data + "')";
                        eval(medthod);
                    }
                }
            });

        }
    </script>
</head>
<body onload="pageInit()">

    <div id="Div_control" class="easyui-panel panel-body panel-body-noheader">

        <span style="margin-left: 10px">选择图表： </span>
        <input id="cc" name="dept" value="请选择" style="width: 400px" />


    </div>
    <br />
    <div id="Div_filterControl" class="easyui-panel panel-body panel-body-noheader" style="display: none">
    </div>
    <br />

    <div id="container" class="easyui-panel panel-body panel-body-noheader" style="overflow: hidden; display: none">
        <div id="title" style="font-family: 微软雅黑; font-size: 16px; margin: 5px; text-align: center"></div>
        <div id="grid"></div>

        <div id="msg"></div>
    </div>


</body>
</html>

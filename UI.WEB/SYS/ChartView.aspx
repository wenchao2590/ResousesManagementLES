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
    <script>var $easyUi = $.noConflict();</script>
    <script src="JS/HCHenryGantt.js"></script>
    <script src="JS/HighChartsConfig.js"></script>

    <script type="text/javascript">
        //规则：
        //1、横坐标chartColumnName 如果是'sql:时间'，代表横坐标是从数据库的 时间字段查询出来的数据作为横坐标。没有则根据用户自定义
        //2、图形展示类型分：柱状图：column，线性图：spline
        //3、数据源sqlString 如果是js:则代表从JS函数里取值。默认从SQL取值 
        //4、mixCondition 混合图形条件，如果chartType是线性。这里放字段名例：mixCondition:"张三，王五"，则出来的结果就是柱状，凡则线性。
        var dataObj = {
            "Name": "车间生产统计图",//NAME
            "ReceiverLayer": "container",//RECEIVER_LAYER
            "chartColumnName": "sql:时间",//CHART_COLUMN_NAME
            "chartType": "column",//CHART_TYPE
            "chartLabelName": "车间生产统计图",//CHART_LABEL_NAME
            "chartWidth": "800",//CHART_WIDTH
            "chartHight": "600",//CHART_HIGHT
            "sqlString": "js:getData()",//SQL_STRING
            "mixCondition": "合格",//MIX_CONDITION
            "chartRowName": "数量个数",//CHART_ROW_NAME
            "yMark": "[0,20,23,44,66]",//CHART_OTHER_FORMAT
            "Remark": "开始日期,beginTime,string,=,datebox|结束日期,endTime,string,=,datebox"
        };
        var splitChar = "-";
        function getData() {
            var chartData = [
                 { "时间": "1", "合格": "20", "不合格": "3" },
                 { "时间": "2", "合格": "33", "不合格": "1" },
                 { "时间": "3", "合格": "43", "不合格": "4" },
                 { "时间": "4", "合格": "25", "不合格": "11" },
                 { "时间": "5", "合格": "13", "不合格": "2" },
                 { "时间": "6", "合格": "63", "不合格": "6" },
                 { "时间": "7", "合格": "90", "不合格": "8" }
            ];

            return chartData;

        }

        function createChart(dataObj, chartData) {
            document.getElementById("container").parentNode.style.display = "block";
            //X轴数据处理和数据集合
            var x = dataObj.chartColumnName;
            var series = "";
            var seriesData = "";
            var chartType = dataObj.chartType;
            var unchartType = chartType == "column" ? "spline" : "column";
            var mixCondition = dataObj.mixCondition.split(",");
            if (x.indexOf("sql:") != -1) {
                var key = x.replace("sql:", "");
                var xValue = "";
                var jsonStr = JSON.stringify(chartData[0]).split(",");
                //获取Key和长度
                var jsonLen = jsonStr.length;
                var seriesKeyItem = new Array();
                for (var j = 0; j < jsonLen; j++) {
                    var seriesKey = jsonStr[j].replace("{", "").split(":")[0].replace(/"/g, "");
                    var type = chartType;
                    for (var k = 0; k < mixCondition.length; k++) {
                        if (seriesKey == mixCondition[k]) {
                            type = unchartType;
                        }
                    }
                    if (seriesKey != key) {
                        series += "{type: '" + type + "',name: '" + seriesKey + "',data: [_" + seriesKey + "data]},";
                        seriesKeyItem.push(seriesKey);
                    }
                }
                series = series.length > 1 ? series.substring(0, series.length - 1) : series;
                for (var i = 0; i < chartData.length; i++) {

                    xValue += "'" + eval("chartData[i]." + key) + "',";
                    for (var u = 0; u < seriesKeyItem.length; u++) {
                        var sKey = seriesKeyItem[u].replace(/"/g, "");
                        var value = eval("chartData[i]." + sKey);
                        var replacePar = (i == chartData.length - 1) ? "" : ",_" + sKey + "data";
                        series = series.replace("_" + sKey + "data", value + replacePar);
                    }

                }
                xValue = xValue.length > 1 ? xValue.substring(0, xValue.length - 1) : xValue;
                x = xValue;
            }
            x = eval("[" + x + "]");
            series = eval("[" + series + "]");
            setChart(dataObj.ReceiverLayer, dataObj.Name, x, dataObj.chartLabelName, series, dataObj.chartRowName, eval(dataObj.yMark));
        }
        function setChart(ReceiverLayer, Name, x, chartLabelName, series, chartRowName, yMark) {

            $('#' + ReceiverLayer).highcharts({
                chart: {
                },
                title: {
                    text: Name
                },
                xAxis: {
                    categories: x
                }, credits: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                yAxis: {
                    title: { text: chartRowName },
                    min: 0,
                    gridLineWidth: 1,
                    tickPositions: yMark,
                    maxTickInterval: 15,
                    minTickInterval: 15,
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]


                },
                tooltip: {
                    formatter: function () {
                        var s;
                        if (this.point.name) { // the pie chart                   
                            s = '' +
                           this.series.name + this.point.name + ': ' + this.y + ' fruits';
                        } else {
                            s = '' +
                               this.series.name + ":" + "X=" + this.x + '  Y=' + this.y;
                        }
                        return s;
                    }
                },
                labels: {
                    items: [{
                        html: chartLabelName,
                        style: {
                            left: '40px',
                            top: '8px',
                            color: 'black'
                        }
                    }]
                },
                series: series
            });
        }
        function getTable(fid, jsMedthodName) {


            var dataParams = "method=getChartByName&ENTITY_NAME=Chart&AN=BLL.SYS&FID=" + fid;
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
        function chartInit(dataObj, controlIdItem) {

            //var sql="select * from table where createTime>=@beginTime and createTime<=@endTime"
            var filterStr = "";
            var valueItem = controlIdItem.split("^");
            for (var i = 0; i < valueItem.length; i++) {
                var filter = valueItem[i].split(splitChar);
                var type = filter[0];
                var filed = filter[1];
                var symbol = filter[2];
                symbol = symbol.replace("a⊙", "=").replace("b⊙", "<=").replace("c⊙", ">=").replace("d⊙", "<").replace("e⊙", ">");
                var controlType = filter[3];
                var val = eval("$easyUi('#" + valueItem[i] + "')." + controlType + "('getValue')");
                // val = controlType == "text" ? val[0].value : val;
                if (val != "") {
                    filterStr += " AND " + filed + "   " + symbol + " @" + val + "@  ";
                    if (symbol == "like") {
                        filterStr = filterStr.replace("@", "'%").replace("@", "%'");
                    } else {
                        var isStr = type == "string" ? "'" : "";
                        filterStr = filterStr.replace(/@/g, isStr);
                    }

                }
            }
            //添加sql
            if (dataObj != null && dataObj != undefined && dataObj != "") {
                var sqlString = dataObj.sqlString;
                if (sqlString.indexOf("js:") != -1) {
                    var data = eval(sqlString.replace("js:", ""));
                    createChart(dataObj, data);
                } else {

                    sqlString = sqlString + " where 1=1 " + filterStr;
                    var dataParams = "ENTITY_NAME=Chart&AN=BLL.SYS&sqlString=" + sqlString + "&method=getData";
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
                                data = eval('(' + data + ')');
                                createChart(dataObj, data);
                            }
                        }
                    });

                }




            }
        }
        //创建查询条件控件
        function createFilterControl(filterControlStr, dataObj) {
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
                            $easyUi('#' + input.id).datebox({
                                required: true,
                                width: 150
                            });
                            $easyUi('#' + input.id).datebox('setValue', dateNow);
                            break;
                        case "textbox":
                            $easyUi('#' + input.id).textbox({
                                width: 150
                            });
                            break;
                        default:
                            break;

                    }
                }
                //测试数据
                $easyUi('#' + input.id).textbox('setValue', "c10756e2-f20d-4596-8c0c-f0a8429bcc8a");

                var div = document.createElement("div");

                div.className = "containerDivCls";
                filterControl.appendChild(div);

                controlIdItem = controlIdItem.length > 0 ? controlIdItem.substring(0, controlIdItem.length - 1) : controlIdItem;
                var button = document.createElement("a");
                button.id = "btn";
                button.setAttribute("onclick", "selectMedthod('" + controlIdItem + "')");
                button.innerHTML = "查询"
                div.appendChild(button);
                $easyUi('#btn').linkbutton({
                    iconCls: 'icon-search'
                });
            }






        }
        var nowChioceObj = null;
        ///////////////////////////////////////////////////////////////////////////
        function pageInit() {
            $easyUi('#cc').combogrid({

                mode: 'remote',
                idField: 'FID',
                textField: 'NAME',
                onChange: function (newValue, oldValue) {
                    var g = $easyUi('#cc').combogrid('grid');
                    var data = g.datagrid('getSelected');
                    if (data != null) {
                        dataObj.Name = data.NAME;
                        dataObj.ReceiverLayer = data.RECEIVER_LAYER;
                        dataObj.chartColumnName = data.CHART_COLUMN_NAME;
                        dataObj.chartType = data.CHART_TYPE;
                        dataObj.ReceiverLayer = data.RECEIVER_LAYER;
                        dataObj.chartLabelName = data.CHART_LABEL_NAME;
                        dataObj.chartWidth = data.CHART_WIDTH;
                        dataObj.chartHight = data.CHART_HIGHT;
                        dataObj.sqlString = data.SQL_STRING;
                        dataObj.mixCondition = data.MIX_CONDITION;
                        dataObj.chartRowName = data.CHART_ROW_NAME;
                        dataObj.yMark = data.CHART_OTHER_FORMAT;
                        dataObj.Remark = data.REMARK;

                        createFilterControl(dataObj.Remark, dataObj)
                        nowChioceObj = dataObj;
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
        function LoadAllData(data) {
            data = eval('(' + data + ')');
            $easyUi('#cc').combogrid("grid").datagrid('loadData', data);

        }
        function selectMedthod(controlIdItem) {
            chartInit(nowChioceObj, controlIdItem);
        }

    </script>
</head>
<body onload="pageInit()">

    <div id="Div_control" class="easyui-panel panel-body panel-body-noheader">

        <span style="margin-left: 10px">选择图表： </span>
        <input id="cc" name="dept" value="请选择" style="width: 400px" />


    </div>
    <br />
    <div id="Div_filterControl" class="easyui-panel panel-body panel-body-noheader" style="padding-left">


        <%--  开始时间
       <div id="div_beginTime" style="width: 180px;"></div>

        结束时间
       <div id="div_endTime" style="width: 180px;"></div>
        <a id="btn" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="selectMedthod()">查询</a>--%>
    </div>
    <br />

    <div id="container" style="overflow: hidden" class="easyui-panel panel-body panel-body-noheader"></div>


</body>
</html>










































//甘特图数据格式
var Souce = {
    "灯罩": [
        {
            "beginTime": "2015-10-10 05:00:00",
            "endTime": "2015-10-16 07:00:00",
            "total": "80"
        },
        {
            "beginTime": "2015-11-10 05:00:00",
            "endTime": "2015-12-01 00:00:00",
            "total": "60"
        }
    ],
    "饰圈": [
        {
            "beginTime": "2015-10-16 05:00:00",
            "endTime": "2015-11-11 07:00:00",
            "total": "60"
        }
    ],
    "衬框": [
        {
            "beginTime": "2015-11-10 05:00:00",
            "endTime": "2015-12-11 07:00:00",
            "total": "23"
        }
    ],
    "反射镜": [
        {
            "beginTime": "2015-11-16 05:00:00",
            "endTime": "2015-11-21 07:00:00",
            "total": "60"
        }
    ],
    "壳体": [
        {
            "beginTime": "2015-11-10 05:00:00",
            "endTime": "2015-12-01 00:00:00",
            "total": "60"
        }
    ]
}
//图表对象
var charts;
 
//图表配置属性
var option;
//记录点击当前选中的柱状图数据下标
var updateSeriesIndex = 1;
//记录点击当前选中的柱状图点下标
var updatePointIndex = 0;
//颜色配置
var Hcolor = ['#DDDF00', '#64E572', '#24CBE5', '#058DC7', '#50B432', '#ED561B', '#FF9655',
'#FFF263', '#6AF9C4'];
//时间转化
function ConvertDate(date) {
    date = date.replace(new RegExp("-", "g"), "/").replace(new RegExp(/( )/g), '-').split('-');
    date = date.length == 1 ? date.replace(new RegExp("/", "g"), "/").replace(new RegExp(/( )/g), '/').split('/') : date;
    var dTime = new Date(date);
    var lTime = parseInt(dTime.getTime() + 8 * 3600 * 1000);
    return lTime;
}
//加载Charts
function LoadCharts(divId,width, height, title, columnClickName, TSouce) {
    if (TSouce == "") {
        TSouce = Souce;
    }
    var seriesAllData = "[";
    var cateGoriesData = "";
    var seriesData = "";
    var seriesTempData = "";
    var min = 100000000000000;
    var max = 0;
    var data = new Array();
    for (var key in Souce) {
        cateGoriesData += "'" + key + "'" + ",";
        var j = 0;
        for (var i = 0; i < Souce[key].length; i++) {

            var beginTime = Souce[key][i].beginTime;
            var numBeginTime = ConvertDate(beginTime);
            if (min > numBeginTime) { min = numBeginTime; }
            var endTime = Souce[key][i].endTime;
            var numEndTime = ConvertDate(endTime);
            if (max < numEndTime) { max = numEndTime; }
            if (data[i] == "" || data[i] == null || data[i] == undefined) {
                data.push("[" + numBeginTime + "," + numEndTime + "],");
            } else {
                data[i] += "[" + numBeginTime + "," + numEndTime + "],";
            }
        }
    }
    for (var i = 0; i < data.length; i++) {
        data[i] = data[i].length > 1 ? data[i].substring(0, data[i].length - 1) : data[i];
        var colorNum = i + 1 > 9 ? colorNum % 9 : i + 1;
        seriesData += "{color:'" + Hcolor[colorNum] + "',events: {click: function (e) {" + columnClickName + "(e)}},data:[" + data[i] + "]},";
    }
    var DefultTimeminNum = min - 777600000;
    var DefultTimemaxNum = max + 777600000;
    seriesData = "{data:[[" + DefultTimeminNum + "," + DefultTimeminNum + "],[" + DefultTimemaxNum + "," + DefultTimemaxNum + "]]}," + seriesData;
    seriesData = seriesData.length > 1 ? seriesData.substring(0, seriesData.length - 1) : seriesData;
    seriesAllData = seriesAllData + seriesData + "]";

    var series = eval(seriesAllData);
    cateGoriesData = cateGoriesData.length > 1 ? cateGoriesData.substring(0, cateGoriesData.length - 1) : cateGoriesData;
    cateGoriesData = eval("[" + cateGoriesData + "]");

    if (width == "" || width == "auto" || width == null) {
        width = document.body.clientWidth - 200;}
    if (height == "" || height == "auto" || height == null) {
          height = window.screen.height - 240; }
    option = {
        chart: {
            renderTo: divId,
            type: 'columnrange',
            inverted: true,
            zoomType: 'xy',
            width: width,
            height: height
        },
        credits: {
            enabled: false
        },
        title: {
            text: title
        },
        subtitle: {
            // text: '甘特图Demo'
        },
        xAxis: {
            gridLineWidth: 1,
            pointPadding: 12,
            pointWidth: 15,
            lineWidth: 2,
            categories: cateGoriesData
        },
        yAxis: {
            lineWidth: 2,
            title: {
                text: ''
            },
            //  tickPixelInterval: 500,
            labels: {
                step: 3,
                rotation: -30,
                formatter: function () {
                    return Highcharts.dateFormat('%Y-%m-%d', this.value);
                }
            }
        },
        tickInterval: 150,
        tooltip: {
            labels: {
                formatter: function () {
                    // return Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.value);
                    // return Highcharts.dateFormat('%Y-%m-%d', this.value);
                }
            }
        },
        plotOptions: {
            columnrange: {
                grouping: false,
                dataLabels: {
                    enabled: true,
                    rotation: 0,
                    //  align: 'right',
                    formatter: function () {
                        if (DefultTimeminNum != this.y && DefultTimemaxNum!=this.y) {
                            return Highcharts.dateFormat('%Y.%m.%d %H:%M:%S', this.y);
                        }
                    },
                    style: {
                        left: '-10px',
                        top: '-30px',
                        fontSize: '10px',
                        // color: 'red',
                        fontWeight: 'bold',
                        width: 12

                    }
                }
            }
        },

        legend: {
            enabled: false,
        },
        exporting: {
            enabled: false
            //设置导出按钮不可用     
        },
        tooltip: {
            enabled: false,
            headerFormat: '{point.x}',
            pointFormat: ':{point.y:%Y-%m-%d %H:%M:%S}<br>',
            crosshairs: { //显示网格线
                width: 4,
                color: "#CCC",
                dashStyle: "longdash" //虚线
            },
            width: 400
        },
        series: series

    }
    charts = new Highcharts.Chart(option);
}

 
function ColumnClick(e) {
    updateSeriesIndex = e.point.series.index;
    updatePointIndex = e.point.index;
    $("#title").textbox('setValue', e.point.category);
    $("#beginTime").textbox('setValue', Highcharts.dateFormat('%m/%d/%Y %H:%M:%S', e.point.low));
    $("#endTime").textbox('setValue', Highcharts.dateFormat('%m/%d/%Y %H:%M:%S', e.point.high));
    $("#sum").textbox('setValue', e.point.low / 100000);


}


function UpdateChartDate() {
    var beginTime = $("#beginTime").textbox('getValue');
    var endTime = $("#endTime").textbox('getValue');
    var data = (charts.series[updateSeriesIndex].yData);
    data[updatePointIndex][0] = ConvertDate(beginTime);
    data[updatePointIndex][1] = ConvertDate(endTime);
    charts.series[updateSeriesIndex].setData(data);
    charts.series[updateSeriesIndex].setVisible(true, true);
}











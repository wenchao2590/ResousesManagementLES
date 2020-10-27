var createHighStr = "$('#@container').highcharts({";
createHighStr += "chart: {},";
createHighStr += "title: {text: '@name'},";
createHighStr += "xAxis: {categories: [@xAxis]},";
createHighStr += "credits: {enabled: false},";
createHighStr += "yAxis: {title: {text:'@yAxis'}},";
createHighStr += "tooltip: {formatter: function () {return this.x + ': ' + this.y; }},";
createHighStr += "labels: {items: [{html: '@label',style: {left: '40px',top: '8px',color: 'black'}}]},";
createHighStr += "series: [@series";
createHighStr += "]});";

var chartConfigJson;
var DataJson;
function setChart(chartConfigJson) {
    var chartWidth = chartConfigJson[0].CHART_WIDTH;
    var chartHeight = chartConfigJson[0].CHART_HIGHT;
    var containerObj = document.createElement("div");
    containerObj.id = chartConfigJson[0].RECEIVER_LAYER;
    containerObj.style.width = chartWidth + "px";
    containerObj.style.height = chartHeight + "px";
    document.getElementsByTagName("body")[0].appendChild(containerObj);
    var container = chartConfigJson[0].RECEIVER_LAYER;
    var name = chartConfigJson[0].NAME;
    var name_en = chartConfigJson[0].NAME_EN;
    var tipFormat = chartConfigJson[0].TIP_FORMAT;
    var label = chartConfigJson[0].CHART_LABEL_NAME;
    var xAxis = chartConfigJson[0].CHART_COLUMN_NAME;
    var yAxis = chartConfigJson[0].CHART_ROW_NAME;
     var sql = chartConfigJson[0].SQL_STRING;
 
    var series = "";
    if (sql.indexOf("js:") != -1) {
        DataJson = eval(sql.replace("js:", ""));//ChartJon();
    } 
    if (DataJson != null) {
        var cData = DataJson.dataItem;
        if (xAxis == "") {
            xAxis = DataJson.xAxis;
        }
        for (var i = 0; i < cData.length; i++) {
            series += "{ type: '" + cData[i].type + "',name: '" + cData[i].name + "', data: [" + cData[i].data + "]},";
        }
        series = series.length > 1 ? series.substring(0, series.length - 1) : series;
        createHighStr = createHighStr.replace("@container", container).replace("@name", name).replace("@xAxis", xAxis).replace("@yAxis", yAxis);
        createHighStr = createHighStr.replace("@label", label).replace("@series", series);
        eval(createHighStr);

    }
}

function ChartJon() {
    DataJson = {
        "xAxis": "'车灯2', '灯罩2', '饰圈2', '衬框2', '反射镜2'",
        "dataItem": [
         {
             "name": "小糸车灯车间100",
             "data": "3000, 2222, 11111, 3233, 12345",
             "rowID": "1",
             "type": "column"
         },
         {
             "name": "小糸车灯车间200",
             "data": "3232, 2231, 1233, 6333, 34335",
             "rowID": "1",
             "type": "column"
         },

        ]
    };

    return DataJson;
}

function getChartJosn(eurl, chartName) {

    $.ajax({
        type: "POST",
        url: eurl,
        data: "method=getChartData&chartName=" + chartName,
        dataType: 'json',
         success: function (data) {
            eval(data);
        },
        error: function () {
        }
    });

}

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="StudentAttendance.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Styles -->
    <style type="text/css">
        #chartdiv {
            width: 60%;
            height: 500px;
        }
         #chartdivuser {
            width: 50%;
            height: 350px;
        }
         .barchart{
             text-align:center;
             margin-left:24%
         }
        .btn-primary {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
        }

        .jumbotron {
            padding-top: 0.5px;
            padding-bottom: 1px;
            background-color: white;
        }

        .container .jumbotron, .container-fluid .jumbotron {
            padding-right: 60px;
            padding-left: 10px;
        }

        #chartdiv {
            height: 350px;
        }
         /*#chartdivuser {
            height: 350px;
        }*/
        .btn-primary {
            color: #fff;
            background-color: #DE6262;
            border-color: #DE6262;
        }

        .login-sec {
            margin-bottom: 10px;
            font-weight: 600;
            font-size: 30px;
            color: #DE6262;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            /*background-color: black;*/
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            width: 200px;
            height: 200px;
            display: none;
            position: absolute;
            /*background-color: White;*/
            z-index: 999;
        }
    </style>

    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <%--//old 1--%>
    <%-- <!-- Resources -->
    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>

    <!-- Chart code -->
    <script>
        am4core.ready(function () {
            var total = '<%=totalstregth %>';
            var presents = '<%=present %>';
            var absents = '<%=absent %>';
            //var currentDate = '<%=fromDt %>';
            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("chartdiv", am4charts.PieChart);
            // Add data
            chart.data = [
                //{
                //"country": "Total Strength",
                //"litres": total
                //},
                {
                    "country": "Present",
                    "litres": presents
                }, {
                    "country": "Absent",
                    "litres": absents
                }];

            // Add and configure Series
            var pieSeries = chart.series.push(new am4charts.PieSeries());
            pieSeries.dataFields.value = "litres";
            pieSeries.dataFields.category = "country";
            pieSeries.slices.template.stroke = am4core.color("#fff");
            pieSeries.slices.template.strokeWidth = 2;
            pieSeries.slices.template.strokeOpacity = 1;

            // This creates initial animation
            pieSeries.hiddenState.properties.opacity = 1;
            pieSeries.hiddenState.properties.endAngle = -90;
            pieSeries.hiddenState.properties.startAngle = -90;

        }); // end am4core.ready()
    </script>--%>




    <%--//old 2--%>

   <%-- <!-- Resources -->
    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/dataviz.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>

    <!-- Chart code -->
    <script>
        am4core.ready(function () {
            var totalstpuli = '<%=totalstregthpulivendula %>';
            var totalstbad = '<%=totalstregthbadvel %>';
            var presentspuli = '<%=presentpulivendula %>';
            var absentspuli = '<%=absentpulivendula %>';
            
            var presentsbad = '<%=presentbadvel%>';
            var absentsbad = '<%=absentbadvel%>';
            // Themes begin
            am4core.useTheme(am4themes_dataviz);
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("chartdiv", am4charts.XYChart);

            // Add data
            chart.data = [{
                "year": "Pulivendula",
                "strength": totalstpuli,
                "present": presentspuli,
                "absent": absentspuli
            }, {
                "year": "Badvel",
                "strength": totalstbad,
                "present": presentsbad,
                "absent": absentsbad
            }];

            // Create axes
            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            categoryAxis.dataFields.category = "year";
            categoryAxis.title.text = "Mandal Wise Attendance Bar charts";
            categoryAxis.renderer.grid.template.location = 0;
            categoryAxis.renderer.minGridDistance = 20;
            categoryAxis.renderer.cellStartLocation = 0.1;
            categoryAxis.renderer.cellEndLocation = 0.9;

            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
            //valueAxis.min = 0;
            //valueAxis.title.text = "Expenditure (M)";

            // Create series
            function createSeries(field, name, stacked) {
                var series = chart.series.push(new am4charts.ColumnSeries());
                series.dataFields.valueY = field;
                series.dataFields.categoryX = "year";
                series.name = name;
                series.columns.template.tooltipText = "{name}: [bold]{valueY}[/]";
                series.stacked = stacked;
                series.columns.template.width = am4core.percent(95);
            }

            createSeries("strength", "TotalStrength", true);
            createSeries("present", "Present", true);
            createSeries("absent", "Absent", true);

            // Add legend
            chart.legend = new am4charts.Legend();

        }); // end am4core.ready()
    </script>


    <!-- Chart code -->
    <script>
        am4core.ready(function () {
            var presents = '<%=present %>';
            var absents = '<%=absent%>';
            var totalstrengths = '<%=totalstregth %>';
            // Themes begin
            am4core.useTheme(am4themes_dataviz);
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("chartdivuser", am4charts.XYChart);

            // Add data
            chart.data = [{
                "year": "Pulivendula",
                "strength": totalstrengths,
                "present": presents,
                "absent": absents
            }];

            // Create axes
            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            categoryAxis.dataFields.category = "year";
            categoryAxis.title.text = "Mandal Wise Attendance Bar charts";
            categoryAxis.renderer.grid.template.location = 0;
            categoryAxis.renderer.minGridDistance = 20;
            categoryAxis.renderer.cellStartLocation = 0.1;
            categoryAxis.renderer.cellEndLocation = 0.9;

            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
            //valueAxis.min = 0;
            //valueAxis.title.text = "Expenditure (M)";

            // Create series
            function createSeries(field, name, stacked) {
                var series = chart.series.push(new am4charts.ColumnSeries());
                series.dataFields.valueY = field;
                series.dataFields.categoryX = "year";
                series.name = name;
                series.columns.template.tooltipText = "{name}: [bold]{valueY}[/]";
                series.stacked = stacked;
                series.columns.template.width = am4core.percent(95);
            }

            createSeries("strength", "TotalStrength", false);
            createSeries("present", "Present", true);
            createSeries("absent", "Absent", true);

            // Add legend
            chart.legend = new am4charts.Legend();

        }); // end am4core.ready()
    </script>--%>



    
<!-- Resources -->
<script src="https://www.amcharts.com/lib/4/core.js"></script>
<script src="https://www.amcharts.com/lib/4/charts.js"></script>
<script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>

<!--admin Chart code -->
<script>
    am4core.ready(function () {
        var totalstpuli = '<%=totalstregthpulivendula %>';
        var totalstbad = '<%=totalstregthbadvel %>';
        var presentspuli = '<%=presentpulivendula %>';
        var absentspuli = '<%=absentpulivendula %>';

        var presentsbad = '<%=presentbadvel%>';
        var absentsbad = '<%=absentbadvel%>';
        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end


        var chart = am4core.create("chartdiv", am4charts.XYChart);

        // some extra padding for range labels
        chart.paddingBottom = 50;

        chart.cursor = new am4charts.XYCursor();
        chart.scrollbarX = new am4core.Scrollbar();

        // will use this to store colors of the same items
        var colors = {};

        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "category";
        categoryAxis.renderer.minGridDistance = 60;
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.dataItems.template.text = "{realName}";
        categoryAxis.adapter.add("tooltipText", function (tooltipText, target) {
            return categoryAxis.tooltipDataItem.dataContext.realName;
        })

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.tooltip.disabled = true;
        valueAxis.min = 0;

        // single column series for all data
        var columnSeries = chart.series.push(new am4charts.ColumnSeries());
        columnSeries.columns.template.width = am4core.percent(80);
        columnSeries.tooltipText = "{provider}: {realName}, {valueY}";
        columnSeries.dataFields.categoryX = "category";
        columnSeries.dataFields.valueY = "value";

        // second value axis for quantity
        var valueAxis2 = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis2.renderer.opposite = true;
        valueAxis2.syncWithAxis = valueAxis;
        valueAxis2.tooltip.disabled = true;

        // quantity line series
        var lineSeries = chart.series.push(new am4charts.LineSeries());
        lineSeries.tooltipText = "{valueY}";
        lineSeries.dataFields.categoryX = "category";
        lineSeries.dataFields.valueY = "Strength";
        lineSeries.yAxis = valueAxis2;
        lineSeries.bullets.push(new am4charts.CircleBullet());
        lineSeries.stroke = chart.colors.getIndex(13);
        lineSeries.fill = lineSeries.stroke;
        lineSeries.strokeWidth = 2;
        lineSeries.snapTooltip = true;

        // when data validated, adjust location of data item based on count
        lineSeries.events.on("datavalidated", function () {
            lineSeries.dataItems.each(function (dataItem) {
                // if count divides by two, location is 0 (on the grid)
                if (dataItem.dataContext.count / 2 == Math.round(dataItem.dataContext.count / 2)) {
                    dataItem.setLocation("categoryX", 0);
                }
                // otherwise location is 0.5 (middle)
                else {
                    dataItem.setLocation("categoryX", 0.5);
                }
            })
        })

        // fill adapter, here we save color value to colors object so that each time the item has the same name, the same color is used
        columnSeries.columns.template.adapter.add("fill", function (fill, target) {
            var name = target.dataItem.dataContext.realName;
            if (!colors[name]) {
                colors[name] = chart.colors.next();
            }
            target.stroke = colors[name];
            return colors[name];
        })


        var rangeTemplate = categoryAxis.axisRanges.template;
        rangeTemplate.tick.disabled = false;
        rangeTemplate.tick.location = 0;
        rangeTemplate.tick.strokeOpacity = 0.6;
        rangeTemplate.tick.length = 60;
        rangeTemplate.grid.strokeOpacity = 0.5;
        rangeTemplate.label.tooltip = new am4core.Tooltip();
        rangeTemplate.label.tooltip.dy = -10;
        rangeTemplate.label.cloneTooltip = false;

        ///// DATA
        var chartData = [];
        var lineSeriesData = [];

        var data =
        {
            "Pulivendula": {
                "Present": presentspuli,
                "Absent": absentspuli,
                "Strength": totalstpuli
            },
            "Badvel": {
                "Present": presentsbad,
                "Absent": absentsbad,
                "Strength": totalstbad
            }
        }

        // process data ant prepare it for the chart
        for (var providerName in data) {
            var providerData = data[providerName];

            // add data of one provider to temp array
            var tempArray = [];
            var count = 0;
            // add items
            for (var itemName in providerData) {
                if (itemName != "quantity") {
                    count++;
                    // we generate unique category for each column (providerName + "_" + itemName) and store realName
                    tempArray.push({ category: providerName + "_" + itemName, realName: itemName, value: providerData[itemName], provider: providerName })
                }
            }
            //// sort temp array
            //tempArray.sort(function (a, b) {
            //    if (a.value > b.value) {
            //        return 1;
            //    }
            //    else if (a.value < b.value) {
            //        return -1
            //    }
            //    else {
            //        return 0;
            //    }
            //})

            // add quantity and count to middle data item (line series uses it)
            var lineSeriesDataIndex = Math.floor(count / 2);
            tempArray[lineSeriesDataIndex].quantity = providerData.quantity;
            tempArray[lineSeriesDataIndex].count = count;
            // push to the final data
            am4core.array.each(tempArray, function (item) {
                chartData.push(item);
            })

            // create range (the additional label at the bottom)
            var range = categoryAxis.axisRanges.create();
            range.category = tempArray[0].category;
            range.endCategory = tempArray[tempArray.length - 1].category;
            range.label.text = tempArray[0].provider;
            range.label.dy = 30;
            range.label.truncate = true;
            range.label.fontWeight = "bold";
            range.label.tooltipText = tempArray[0].provider;

            range.label.adapter.add("maxWidth", function (maxWidth, target) {
                var range = target.dataItem;
                var startPosition = categoryAxis.categoryToPosition(range.category, 0);
                var endPosition = categoryAxis.categoryToPosition(range.endCategory, 1);
                var startX = categoryAxis.positionToCoordinate(startPosition);
                var endX = categoryAxis.positionToCoordinate(endPosition);
                return endX - startX;
            })
        }

        chart.data = chartData;


        // last tick
        var range = categoryAxis.axisRanges.create();
        range.category = chart.data[chart.data.length - 1].category;
        range.label.disabled = true;
        range.tick.location = 1;
        range.grid.location = 1;

    }); // end am4core.ready()
</script>

    <!-- Non Admin Chart code -->
<script>
    am4core.ready(function () {
        var presents = '<%=present %>';
        var absents = '<%=absent%>';
        var totalstrengths = '<%=totalstregth %>';
        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end


        var chart = am4core.create("chartdivuser", am4charts.XYChart);

        // some extra padding for range labels
        chart.paddingBottom = 50;

        chart.cursor = new am4charts.XYCursor();
        chart.scrollbarX = new am4core.Scrollbar();

        // will use this to store colors of the same items
        var colors = {};

        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "category";
        categoryAxis.renderer.minGridDistance = 60;
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.dataItems.template.text = "{realName}";
        categoryAxis.adapter.add("tooltipText", function (tooltipText, target) {
            return categoryAxis.tooltipDataItem.dataContext.realName;
        })

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.tooltip.disabled = true;
        valueAxis.min = 0;

        // single column series for all data
        var columnSeries = chart.series.push(new am4charts.ColumnSeries());
        columnSeries.columns.template.width = am4core.percent(80);
        columnSeries.tooltipText = "{provider}: {realName}, {valueY}";
        columnSeries.dataFields.categoryX = "category";
        columnSeries.dataFields.valueY = "value";

        // second value axis for quantity
        var valueAxis2 = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis2.renderer.opposite = true;
        valueAxis2.syncWithAxis = valueAxis;
        valueAxis2.tooltip.disabled = true;

        // quantity line series
        var lineSeries = chart.series.push(new am4charts.LineSeries());
        lineSeries.tooltipText = "{valueY}";
        lineSeries.dataFields.categoryX = "category";
        lineSeries.dataFields.valueY = "Strength";
        lineSeries.yAxis = valueAxis2;
        lineSeries.bullets.push(new am4charts.CircleBullet());
        lineSeries.stroke = chart.colors.getIndex(13);
        lineSeries.fill = lineSeries.stroke;
        lineSeries.strokeWidth = 2;
        lineSeries.snapTooltip = true;

        // when data validated, adjust location of data item based on count
        lineSeries.events.on("datavalidated", function () {
            lineSeries.dataItems.each(function (dataItem) {
                // if count divides by two, location is 0 (on the grid)
                if (dataItem.dataContext.count / 2 == Math.round(dataItem.dataContext.count / 2)) {
                    dataItem.setLocation("categoryX", 0);
                }
                // otherwise location is 0.5 (middle)
                else {
                    dataItem.setLocation("categoryX", 0.5);
                }
            })
        })

        // fill adapter, here we save color value to colors object so that each time the item has the same name, the same color is used
        columnSeries.columns.template.adapter.add("fill", function (fill, target) {
            var name = target.dataItem.dataContext.realName;
            if (!colors[name]) {
                colors[name] = chart.colors.next();
            }
            target.stroke = colors[name];
            return colors[name];
        })


        var rangeTemplate = categoryAxis.axisRanges.template;
        rangeTemplate.tick.disabled = false;
        rangeTemplate.tick.location = 0;
        rangeTemplate.tick.strokeOpacity = 0.6;
        rangeTemplate.tick.length = 60;
        rangeTemplate.grid.strokeOpacity = 0.5;
        rangeTemplate.label.tooltip = new am4core.Tooltip();
        rangeTemplate.label.tooltip.dy = -10;
        rangeTemplate.label.cloneTooltip = false;

        ///// DATA
        var chartData = [];
        var lineSeriesData = [];

        var data =
        {
            "Attendance": {
                "Present": presents,
                "Absent": absents,
                "Strength": totalstrengths
            }
        }

        // process data ant prepare it for the chart
        for (var providerName in data) {
            var providerData = data[providerName];

            // add data of one provider to temp array
            var tempArray = [];
            var count = 0;
            // add items
            for (var itemName in providerData) {
                if (itemName != "quantity") {
                    count++;
                    // we generate unique category for each column (providerName + "_" + itemName) and store realName
                    tempArray.push({ category: providerName + "_" + itemName, realName: itemName, value: providerData[itemName], provider: providerName })
                }
            }
            //// sort temp array
            //tempArray.sort(function (a, b) {
            //    if (a.value > b.value) {
            //        return 1;
            //    }
            //    else if (a.value < b.value) {
            //        return -1
            //    }
            //    else {
            //        return 0;
            //    }
            //})

            // add quantity and count to middle data item (line series uses it)
            var lineSeriesDataIndex = Math.floor(count / 2);
            tempArray[lineSeriesDataIndex].quantity = providerData.quantity;
            tempArray[lineSeriesDataIndex].count = count;
            // push to the final data
            am4core.array.each(tempArray, function (item) {
                chartData.push(item);
            })

            // create range (the additional label at the bottom)
            var range = categoryAxis.axisRanges.create();
            range.category = tempArray[0].category;
            range.endCategory = tempArray[tempArray.length - 1].category;
            range.label.text = tempArray[0].provider;
            range.label.dy = 30;
            range.label.truncate = true;
            range.label.fontWeight = "bold";
            range.label.tooltipText = tempArray[0].provider;

            range.label.adapter.add("maxWidth", function (maxWidth, target) {
                var range = target.dataItem;
                var startPosition = categoryAxis.categoryToPosition(range.category, 0);
                var endPosition = categoryAxis.categoryToPosition(range.endCategory, 1);
                var startX = categoryAxis.positionToCoordinate(startPosition);
                var endX = categoryAxis.positionToCoordinate(endPosition);
                return endX - startX;
            })
        }

        chart.data = chartData;


        // last tick
        var range = categoryAxis.axisRanges.create();
        range.category = chart.data[chart.data.length - 1].category;
        range.label.disabled = true;
        range.tick.location = 1;
        range.grid.location = 1;

    }); // end am4core.ready()
</script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        var fDate = '<%=fromDt %>';
        $(function () {
            $("#fromdatepicker").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#fromdatepicker").datepicker("setDate", fDate);
        });


    </script>

    <div class="jumbotron">
        <h3 class="login-sec">Attendance Report</h3>
    </div>
    <div class="row">
        <div class="col-md-2">
            Select Date
    <input type="text" name="fromdatepicker" id="fromdatepicker" class="form-control" runat="server" clientidmode="static" />
        </div>
        <img src="Images/lodingfinal.gif" alt="" class="loading" />
        <div class="col-md-2">
            <br />
            <asp:Button ID="btngeneratereport" runat="server" Text="Generate Report" class="btn btn-primary" OnClick="btngeneratereport_Click" />
        </div>
    </div>
    <br>
    <div style="text-align: center;">
        <h2>Attendance Chart</h2>
        <h5>
            <asp:Label ID="Label1" runat="server"></asp:Label></h5>
    </div>
    <asp:Panel ID="Panel1" runat="server" CssClass="barchart">
        <div id="chartdiv"></div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="barchart">
        <div id="chartdivuser"></div>
    </asp:Panel>
</asp:Content>

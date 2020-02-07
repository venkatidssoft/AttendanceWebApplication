<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="StudentAttendance.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--date time picker--%>
    <%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
     <link rel="stylesheet" href="/resources/demos/style.css" />
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $('#fromdatepicker').datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#fromdatepicker").datepicker("setDate", fromdt);
        });
</script>--%>
    
    
    <!-- Styles -->
    <style type="text/css">
        #chartdiv {
            width: 100%;
            height: 500px;
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

    <!-- Resources -->
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
    <div style="text-align: center;">
        <h2>Attendance Chart</h2>
        <h5>
            <asp:Label ID="Label1" runat="server"></asp:Label></h5>
    </div>
    <div id="chartdiv"></div>
</asp:Content>

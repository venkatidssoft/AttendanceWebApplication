<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="StudentAttendance.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
    <style type="text/css">
        label {
            text-align: left;
        }

        .text-align {
            text-align: left !important;
        }

        .navbar-inverse {
            background-color: #02639F;
            border-color: #080808;
        }
        .navbar-inverse .navbar-brand {
    color: white;
}
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <style type="text/css">
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
            height: 100px;
            display: none;
            position: fixed;
            /*background-color: White;*/
            z-index: 999;
        }

        header .content-wrapper {
            padding-top: 0px;
        }

        .jumbotron {
            padding-top: 1px;
            padding-bottom: 2px;
        }

        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
            width: 450px;
            margin-bottom: -1px;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }

        .form-controls {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        }
    </style>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#fromdatepicker").datepicker();
            var currentDate = new Date();
            $("#fromdatepicker").datepicker("setDate", currentDate);
        });

        $(function () {
            $("#todatepicker").datepicker();
            var currentDate = new Date();
            $("#todatepicker").datepicker("setDate", currentDate);
        });
    </script>



    <div class="jumbotron">
        <h3>Attendance Report</h3>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-2">
                    From Date
    <input type="text" name="fromdatepicker" id="fromdatepicker" class="form-control" runat="server" clientidmode="static" />
                </div>
                <div class="col-md-2">
                    To Date
        <input type="text" id="todatepicker" runat="server" clientidmode="static" class="form-control" />
                </div>
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

            <div class="row">
                <div class="col-md-2">
                    Select District
            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-controls" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Kadapa</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select Mandal
            <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pulivendula</asp:ListItem>
                <asp:ListItem>Badvel</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    Select School
            <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-controls" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Government High School Ramanappa Satram</asp:ListItem>
                <asp:ListItem>ZP High School Badvel</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select Class
            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Class 1</asp:ListItem>
                <asp:ListItem>Class 2</asp:ListItem>
                <asp:ListItem>Class 3</asp:ListItem>
                <asp:ListItem>Class 4</asp:ListItem>
                <asp:ListItem>Class 5</asp:ListItem>
                <asp:ListItem>Class 6</asp:ListItem>
                <asp:ListItem>Class 7</asp:ListItem>
                <asp:ListItem>Class 8</asp:ListItem>
                <asp:ListItem>Class 9</asp:ListItem>
                <asp:ListItem>Class 10</asp:ListItem>
            </asp:DropDownList>
                </div>


                <div class="col-md-2">
                    Select Gender
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:DropDownList>
                </div>
            </div>


            <br />
            <%--<div class="row">
                <div class="col-md-2">
            Select Location
            <asp:DropDownList ID="ddlLocation" runat="server" class="form-control">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Pulivendula</asp:ListItem>
                <asp:ListItem>Badvel</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div class="col-md-2">
            Select Medium
            <asp:DropDownList ID="ddlMedium" runat="server" class="form-control">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Telugu</asp:ListItem>
                <asp:ListItem>English</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            Select Section
            <asp:DropDownList ID="ddlSection" runat="server" class="form-control">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>B</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
                <asp:ListItem>D</asp:ListItem>
                <asp:ListItem>E</asp:ListItem>
                <asp:ListItem>F</asp:ListItem>
                <asp:ListItem>G</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>--%>
            <%--<br />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row" style="text-align: center;">
        <asp:Button ID="btngeneratereport" runat="server" Text="Generate Report" class="btn btn-primary" OnClick="btngeneratereport_Click" />
    </div>
    <br />
    <div class="row">
        <asp:GridView ID="grdreport" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" EmptyDataText="No Record Found." Width="100%">
            <Columns>
                <asp:BoundField DataField="District" HeaderText="District Name" />
                <asp:BoundField DataField="Mandal" HeaderText="No of Mandals" />
                <asp:BoundField DataField="NoofSchools" HeaderText="No of Schools" />
                <asp:BoundField DataField="Total Students" HeaderText="Total Students" />
                <asp:BoundField DataField="Total Present" HeaderText="Total Present" />
                <asp:BoundField DataField="Total Absent" HeaderText="Total Absent" />
            </Columns>
        </asp:GridView>
    </div>

    <br />
    <div class="loading" align="center">
        <br />
        <img src="Images/lodingfinal.gif" alt="" />
    </div>
</asp:Content>

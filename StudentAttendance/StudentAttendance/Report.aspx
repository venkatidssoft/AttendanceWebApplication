<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="StudentAttendance.Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-1.8.3.min.js"></script>
    <%--<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>--%>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Content/bootstrap.min.css" media="screen" />
    <%--<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' media="screen" />--%>
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

        .navbar-inverse {
            background-color: white;
            border-color: #080808;
        }

            .navbar-inverse .navbar-brand {
                color: black;
            }

            .navbar-inverse .navbar-nav > li > a {
                color: black;
            }

        button, html input[type="button"], input[type="reset"], input[type="submit"] {
            -webkit-appearance: button;
            cursor: pointer;
        }

        .btn-primary {
            color: #fff;
            background-color: #DE6262;
            border-color: #DE6262;
            margin-left: 97px;
            margin-top: 10px;
            height: 38px;
        }

        .jumbotron {
            padding: 30px;
            margin-bottom: 30px;
            font-size: 21px;
            font-weight: 200;
            line-height: 2.1428571435;
            color: inherit;
            background-color: white;
        }

        .login-sec {
            margin-bottom: 10px;
            font-weight: 600;
            font-size: 30px;
            color: #DE6262;
        }
    </style>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
            height: 200px;
            display: none;
            position: absolute;
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

        .auto-style1 {
            border-collapse: collapse;
            max-width: 100%;
            margin-bottom: 20px;
        }
    </style>




    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        var fDate = '<%=sDate %>';
        var tDate = '<%=eDate %>';
        $(function () {
            $("#fromdatepicker").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#fromdatepicker").datepicker("setDate", fDate);
        });

        $(function () {
            $("#todatepicker").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#todatepicker").datepicker("setDate", tDate);
        });
    </script>

    <div class="jumbotron">
        <h3 class="login-sec">Attendance Report</h3>
    </div>

    <div class="row">
        <div class="col-md-2">
            From Date
    <input type="text" name="fromdatepicker" id="fromdatepicker" class="form-control" runat="server" clientidmode="static" />
        </div>
        <%--  <div class="col-md-2">
            To Date
        <input type="text" id="todatepicker" runat="server" clientidmode="static" class="form-control" />
        </div>--%>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

            <br />
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


            <%--<div class="loading" align="center">--%>
            <br />
            <img src="Images/lodingfinal.gif" alt="" class="loading" />
            <%--</div>--%>

            <div class="row">
                <div class="col-md-2">
                    Select District
            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-controls" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                <asp:ListItem>Kadapa</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select Mandal
            <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-controls" AutoPostBack="True" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    Select School
            <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-controls" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select Class
            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>
                <%--<div class="col-md-2">
                    Select Gender
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>--%>
            </div>
            <br />
            <br />
            <div class="row" style="text-align: center;">
                <asp:Button ID="Button1" runat="server" Text="Generate Report" class="btn btn-primary" OnClick="btngeneratereport_Click" />
            </div>
            <br />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" Text="From Date : "></asp:Label><asp:Label ID="lblfromdate" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label7" runat="server" Text="To Date : "></asp:Label><asp:Label ID="lbltodate" runat="server"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="District : "></asp:Label><asp:Label ID="lbldistrict" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" Text="Mandal : "></asp:Label><asp:Label ID="lblmandal" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label3" runat="server" Text="School : "></asp:Label>
                    <asp:Label ID="lblschool" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Class : "></asp:Label>
                    <asp:Label ID="lblclass" runat="server"></asp:Label>
                </div>
                <%--<div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Selected Gender : "></asp:Label>
                    <asp:Label ID="lblgender" runat="server"></asp:Label>
                </div>--%>
            </div>
            <br />
            <%--<div class="row" style="text-align: right; visibility:hidden">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/excel.png" OnClick="ImageButton1_Click"  />
            </div>--%>
            <div class="row">
                <asp:GridView ID="grdreport" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" EmptyDataText="No Record Found." Width="100%" OnRowCommand="grdreport_RowCommand"
                    DataKeyNames="mandalid">
                    <Columns>
                        <asp:BoundField DataField="mandalid" HeaderText="ID" />
                        <asp:BoundField DataField="District" HeaderText="District Name" />
                        <asp:BoundField DataField="Mandal" HeaderText="Mandal Name" />
                        <%-- <asp:TemplateField HeaderText="No of Mandals">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblmandal" runat="server" Text='<%# Eval("Mandal") %>'
                                    CommandName="mandal" commandargument="<%# Container.DataItemIndex %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                        <%-- <asp:TemplateField HeaderText="No of Schools">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblnoofschools" runat="server" Text='<%# Eval("NoofSchools") %>'
                                    CommandName="noofschools" CommandArgument='<%#Bind("NoofSchools") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Total Students">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalstudents" runat="server" Text='<%# Eval("TotalStudents") %>'
                                    CommandName="totalstudents" CommandArgument="<%# Container.DataItemIndex %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Present">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalpresents" runat="server" Text='<%# Eval("TotalPresent") %>'
                                    CommandName="totalpresent" CommandArgument="<%# Container.DataItemIndex %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Absent">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalabsent" runat="server" Text='<%# Eval("TotalAbsent") %>'
                                    CommandName="totalAbsent" CommandArgument="<%# Container.DataItemIndex %>">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <%--// Associated Controls--%>
                <asp:HiddenField ID="hfAction" runat="server" />
                <asp:HiddenField ID="hfRow" runat="server" />
                <asp:HiddenField ID="hfColumn" runat="server" />
            </div>
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
                <div class="row">
                    <h3>Detail Report</h3>
                </div>
                <%--  <div class="row" style="text-align: right;visibility:hidden">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/excel.png" OnClick="ImageButton2_Click" />
                </div>--%>

                <div class="row">
                    <div style="height: 30px; width: 100%; margin: 0; padding: 0">
                        <table rules="all" border="1" id="tblHeader"
                            style="font-family: Arial; font-size: 10pt; width: 100%; color: black; border-collapse: collapse; height: 100%;">

                            <tr>
                                <td style="width: 89px; text-align: center">ID</td>
                                <td style="width: 298px; text-align: center">Name</td>
                                <td style="width: 65px; text-align: center">Class</td>
                                <td style="width: 22px; text-align: center">Medium</td>
                                <td style="width: 228px; text-align: center">School</td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical">

                        <asp:GridView ID="grdDetailReport" runat="server" Width="1000%" CssClass="auto-style1" ShowHeader="false"></asp:GridView>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

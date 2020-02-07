<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="StudentAttendance.Report" EnableEventValidation="false" %>

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

        .btn-primary {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
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
        <h3>Attendance Report</h3>
    </div>

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
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select Mandal
            <asp:DropDownList ID="ddlMandal" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlMandal_SelectedIndexChanged">
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
                <div class="col-md-2">
                    Select Gender
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-controls" Enabled="false" AutoPostBack="True">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
                </div>
            </div>
            <br />
            <br />
            <div class="row" style="text-align: center;">
                <asp:Button ID="Button1" runat="server" Text="Generate Report" class="btn btn-primary" OnClick="btngeneratereport_Click" OnClientClick="jsfunction(); return false;" />
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
                    <asp:Label ID="Label1" runat="server" Text="Selected District : "></asp:Label><asp:Label ID="lbldistrict" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Selected Mandal : "></asp:Label><asp:Label ID="lblmandal" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Selected School : "></asp:Label>
                    <asp:Label ID="lblschool" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Selected Class : "></asp:Label>
                    <asp:Label ID="lblclass" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Selected Gender : "></asp:Label>
                    <asp:Label ID="lblgender" runat="server"></asp:Label>
                </div>

            </div>
            <br />
            <div class="row">
                <asp:GridView ID="grdreport" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" EmptyDataText="No Record Found." Width="100%" OnRowCommand="grdreport_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="District" HeaderText="District Name" />
                        <asp:TemplateField HeaderText="No of Mandals">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblmandal" runat="server" Text='<%# Eval("Mandal") %>'
                                    CommandName="mandal" CommandArgument='<%#Bind("Mandal") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="No of Schools">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblnoofschools" runat="server" Text='<%# Eval("NoofSchools") %>'
                                    CommandName="noofschools" CommandArgument='<%#Bind("NoofSchools") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Students">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalstudents" runat="server" Text='<%# Eval("TotalStudents") %>'
                                    CommandName="totalstudents" CommandArgument='<%#Bind("TotalStudents") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Present">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalpresents" runat="server" Text='<%# Eval("TotalPresent") %>'
                                    CommandName="totalpresent" CommandArgument='<%#Bind("TotalPresent") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Absent">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltotalabsent" runat="server" Text='<%# Eval("TotalAbsent") %>'
                                    CommandName="totalAbsent" CommandArgument='<%#Bind("TotalAbsent") %>'>
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
            <div class="row" >
                <h3>Detail Report</h3>
            </div>
            <div class="row">
                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical">
                <asp:GridView ID="grdDetailReport" runat="server" Width="1841px" CssClass="table"></asp:GridView>
                    </asp:Panel>
            </div>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

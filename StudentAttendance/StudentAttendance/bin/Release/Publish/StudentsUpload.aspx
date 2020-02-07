<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsUpload.aspx.cs" Inherits="StudentAttendance.StudentsUpload" %>

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
                color: black;
            }

            .navbar-inverse .navbar-nav > li > a {
                color: black;
            }
    </style>


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
            background-color: white;
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

        .login-sec {
            margin-bottom: 10px;
            font-weight: 600;
            font-size: 30px;
            color: #DE6262;
        }

        .container .jumbotron, .container-fluid .jumbotron {
            padding-right: 60px;
            padding-left: 10px;
        }

      .btn-primary {
            color: #fff;
            background-color: #DE6262;
            border-color: #DE6262;
            margin-left: 97px;
            margin-top: 10px;
            height: 38px;
        }
       .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 10px;
        }

            .fileUpload input.upload {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                padding: 0;
                font-size: 20px;
                cursor: pointer;
                opacity: 0;
                filter: alpha(opacity=0);
            }

        p {
            margin: 17px 0 10px;
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
    <div class="jumbotron">
        <h3 class="login-sec">Student Details Upload</h3>
    </div>
    <img src="Images/lodingfinal.gif" alt="" class="loading" />
    <div class="row">
        <div class="col-md-2">
            <p style="text-align: right;">Select File :</p>
        </div>
        <div class="col-md-2">
            <asp:FileUpload ID="FileUpload1" runat="server" class="fileUpload btn btn-primary"/>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnUpload" runat="server" Text="Upload Attendance" OnClick="btnUpload_Click" class="btn btn-primary" />
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-2">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data in the data source."
                AutoGenerateColumns="False" Width="100%" CssClass="table">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="student_unique_id" HeaderText="ID" ItemStyle-Width="150px">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_name" HeaderText="Name" ItemStyle-Width="150px">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_sex" HeaderText="Gender" ItemStyle-Width="150px">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_class" HeaderText="Class" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_medium" HeaderText="Medium" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_mandal" HeaderText="Mandal" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_Udise" HeaderText="Udise" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_school" HeaderText="School" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="student_location" HeaderText="Location" ItemStyle-Width="100px">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>

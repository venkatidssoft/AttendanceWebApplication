<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceUpload.aspx.cs" Inherits="StudentAttendance.AttendanceUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
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
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        header .content-wrapper {
            padding-top: 0px;
        }

        .jumbotron {
            padding-top: 1px;
            padding-bottom: 2px;
        }
            body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            width: 450px;
            margin-bottom: -1px;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
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
    <div class="jumbotron">
        <h3>Student Attendance Upload</h3>
    </div>
    <div class="row">
        <div class="col-md-2">
            <p style="text-align: right;">Select File :</p>
        </div>
        <div class="col-md-2">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnUpload" runat="server" Text="Upload Attendance" OnClick="Button1_Click" />
        </div>
    </div>
    <div class="row">

        </div>
    <div class="row">
        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" emptydatatext="No data in the data source."></asp:GridView>
    </div>
</asp:Content>

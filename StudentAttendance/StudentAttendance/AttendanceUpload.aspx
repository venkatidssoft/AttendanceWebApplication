<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceUpload.aspx.cs" Inherits="StudentAttendance.AttendanceUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style type="text/css">
    .modal
    {
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
    .loading
    {
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
        <h1>Student Attendance Upload</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            Select File :
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnUpload" runat="server" Text="Upload Attendance" OnClick="Button1_Click" />
        </div>
        <div class="col-md-4">
            
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>

</asp:Content>

<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="StudentAttendance.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        
        .login-sec {
            margin-bottom: 10px;
            font-weight: 600;
            font-size: 30px;
            color: #DE6262;
        }

        .container .jumbotron, .container-fluid .jumbotron {
            padding-right: 60px;
            padding-left: 10px;
            background-color: white;
        }
    </style>
    <h2 class="login-sec"><%: Title %>.</h2>
    <h3 class="login-sec">IDS Soft</h3>
    <address>
        Trendz Utility,Plot No.25,<br />
        Survey No.37- 41, 3rd Floor, Gafoor Nagar,<br />
        Vittal Rao Nagar Road, Madhapur,Hyderabad,<br />
        <abbr title="Telephone Number">Tel:</abbr>
        +91 40405 27700,<br />
        <abbr title="Sales Number">Sales:</abbr>
        +91 40405 27733.
    </address>

    <address>
        <strong>Support:</strong>   <a href="info@idssoft.com">info@idssoft.com</a><br />
    </address>
</asp:Content>

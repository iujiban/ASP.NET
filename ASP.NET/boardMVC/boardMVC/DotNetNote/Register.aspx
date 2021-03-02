<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="boardMVC.DotNetNote.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html: charset=utf-8" />
    <title></title>
<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen" />
    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.428571429;
            color: #555;
            vertical-align: middle;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            -webkit-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
        .auto-style2 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.428571429;
            color: #555;
            vertical-align: middle;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            -webkit-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style ="max-width: 400px;">
            <h2 class="text-left">회원 등록</h2>
            <label for ="txtRegisterName">이름: </label>
            <em>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="auto-style2" palceholder="Enter UserName" Width="211px"></asp:TextBox> <br />
            </em>
            <label for ="txtRegisterID">아이디: </label>
            <em>
                <asp:TextBox ID="txtUserID" runat="server" CssClass="auto-style2" placeholder="Enter UserID" Width="211px"></asp:TextBox> <br />
            </em>
            <label for="txtPassword">비번: </label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="auto-style1" placeholder="Enter Password" TextMode="Password" Width ="211px"></asp:TextBox> <br />
            <asp:Button ID="btnEnter" runat="server" Visible="true" OnClick="btnEnter_Click" CssClass="btn btn-primary" Width="236px" Text="회원등록" /> <br />
            <br />
        </div>
    </form>
</body>
</html>

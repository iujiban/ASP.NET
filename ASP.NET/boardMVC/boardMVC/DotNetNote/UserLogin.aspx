<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="boardMVC.DotNetNote.UserLogin" %>

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
    <form id="form1" runat="server" method="post">
        <div style="max-width: 400px;">
            <h2 class="text-left">login</h2>
            <label for="txtUsername">UserID</label>
            <em>
                <asp:TextBox ID="txtUserID" runat="server" CssClass="auto-style2" placeholder="Enter Username" Width="211px"></asp:TextBox>
            </em>
            <br />
            <label for="txtPassword">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="auto-style1" placeholder="Enter Password" Width="212px"></asp:TextBox>
            <br />

            <asp:Button ID="btnLogin" runat="server" Visible="true" OnClick="btnLogin_Click" Class="btn btn-primary" Width="116px" Text="로그인"></asp:Button>
            <asp:Button ID="btnRegister" runat="server" Visible="true" OnClick="btnRegister_Click" CssClass=" btn btn-primary" Width="116px" Text="회원 가입" />
            <br />
            <br />
        </div>

        <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
            <strong>Error!</strong>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="boardMVC.DotNetNote.Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>회원 관리 - 로그인 확인</title>
    <style type="text/css">
        .auto-style1 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>회원 관리</h1>
            <h2>로그인 확인</h2>

            <asp:Label ID="lblName" runat="server"></asp:Label>님,
            반갑습니다.

            
        </div>
        <p>
            <asp:Button ID="lblButton" runat="server" OnClick="lblButton_Click" Text="게시판" Height="44px" Width="111px" />
            <asp:Button ID="Button1" runat="server" CssClass="auto-style1" Height="42px" OnClick="Button1_Click" Text="로그아웃" Width="110px" />
        </p>
        <p>
            &nbsp;
        </p>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="boardMVC.DotNetNote.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
            position: relative;
            visibility: inherit;
            list-style-type: circle;
            table-layout: fixed;
            left: 0px;
            top: 0px;
            border-style: double;
            border-width: 5px;
            background: #00FFFF no-repeat fixed center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h2>
                Log Out을 하시겠습니까?
            </h2>
        </div>
        <asp:Button ID="YesButton" runat="server" Height="69px" OnClick="YesButton_Click" Text="Yes" Width="91px" CssClass="auto-style1" ForeColor="#CCFF33" />
        <asp:Button ID="NoButton" runat="server" Height="69px"  Text="No" Width="91px" CssClass="newStyle1" ForeColor="#CC3300" OnClick="NoButton_Click" />
    </form>
</body>
</html>

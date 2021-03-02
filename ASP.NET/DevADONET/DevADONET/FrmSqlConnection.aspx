<%@ Page Language="C#" AutoEventWireup="true"
     CodeBehind="FrmSqlConnection.aspx.cs"
     Inherits="DevADONET.FrmSqlConnection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnsqlConnection" runat="server" Text="SQL Server 연결하기"  OnClick="btnsqlConnection_Click"/>
        <hr />
        <asp:Label ID="lblDisplay" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>

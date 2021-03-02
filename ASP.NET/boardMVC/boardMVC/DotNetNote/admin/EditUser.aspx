<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="boardMVC.DotNetNote.admin.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 177px;
            text-align: center;
            margin-top: 0px;
        }

        .auto-style2 {
            width: 19%;
        }

        .auto-style3 {
            width: 85px;
        }

        .auto-style4 {
            width: 85px;
            height: 20px;
        }

        .auto-style5 {
            height: 20px;
        }

        .auto-style6 {
            height: 50px;
            margin-bottom: 26px;
        }

        .auto-style7 {
            width: 128px;
            height: 20px;
            text-align: right;
        }

        .auto-style8 {
            width: 30%;
        }
    </style>
</head>
<body>
    <form id="SearchForm" method="post" runat="server">
        <div class="auto-style6">
            <table align="center" class="auto-style8">
                <tr>
                    <td class="auto-style7">조회할 아이디:</td>
                    <td class="auto-style5">
                        <asp:TextBox ID="UserIdtext" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SearchText" Text="조회" runat="server" OnClick="SearchText_Click" Height="19px" Width="115px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <form id="modifyForm" method="post"  runat="server">
        <div class="auto-style1">
            <table align="center" class="auto-style2">
                <tr>
                    <td class="auto-style4">NumberID:</td>
                    <td class="auto-style5">
                        <%=Session["ModifyKey"] %>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">아이디:</td>
                    <td>
                        <%=Session["ModifyUser"] %>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Password:</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Level:</td>
                    <td>
                        <asp:TextBox ID="txtLevel" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Button ID="ModifyButton" runat="server" Height="37px" Text="수정" Width="128px" OnClick="ModifyButtonEvent" />
        </div>
    </form>

</body>
</html>

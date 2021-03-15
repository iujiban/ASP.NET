<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IExportUpload.aspx.cs" Inherits="UploadDemo.IExportUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Import / Export Database Data to/ from EXCEL file.</h3>
    <div style="height: 780px">

        <table border="1" class="nav-justified" style="height: 158px; width: 58%; background-color: #FFFF99">
            <tr>
                <td class="modal-sm" style="width: 175px; height: 85px; text-align: right;"><strong>Upload Excel Files:</strong></td>
                <td style="height: 85px; width: 380px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Height="40px" Width="346px" />
                </td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 175px; height: 96px; text-align: center;">
                    <asp:Button ID="Button2" runat="server" Height="67px" Style="background-color: #FF5050" Text="Save into Oracle" Visible="False" Width="179px" OnClick="Button2_Click" />
                </td>
                <td style="height: 96px; width: 380px;">
                    <asp:Button ID="Button1" runat="server" Height="43px" Style="background-color: #66FF99" Text="Start Upload" Width="122px" OnClick="Button1_Click" />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <asp:Label ID="Label2" runat="server"></asp:Label>

            <br />
            <%if (Session["userID"].ToString() == "admin")
                { %>
            <a target="_blank" id="idinquiry" href="adminTable.aspx">관리자</a>
            <% } %>
            <!--
            <%if (Session["level"].ToString() == "PREMIUM")
                { %>
            <a target="_blank" id="idinquiry" href="adminTable.aspx">관리자</a>
            <% } %>
            <%if (Session["level"].ToString() == "SMART")
                { %>
            <a target="_blank" id="idinquiry" href="adminTable.aspx">관리자</a>
            <% } %>
            <%if (Session["level"].ToString() == "BASIC")
                { %>
            <a target="_blank" id="idinquiry" href="adminTable.aspx">관리자</a>
            <% } %>
            -->
        </div>

        <br />



        <br />
        <br />
        <br />

    </div>
</asp:Content>

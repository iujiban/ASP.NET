<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFormSingleControl.ascx.cs" Inherits="UploadDemo.SearchFormSingleControl" %>
<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<div style="text-align: left">
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="SearchField" runat="server" CssClass="form-control" Width="80px" Style="display: inline-block;">
                    <asp:ListItem Value="발신자">발신자</asp:ListItem>
                    <asp:ListItem Value="수신자">수신자</asp:ListItem>
                    <asp:ListItem Value="거래처명">거래처명</asp:ListItem>
                </asp:DropDownList>&nbsp;
            </td>
            <td>
                <asp:TextBox ID="SearchQuery" runat="server" Width="200px" CssClass="form-control" Style="display: inline-block;"></asp:TextBox>
                <!-- <asp:RequiredFieldValidator ID="valSearchQuery" runat="server" ControlToValidate="SearchQuery" Display="None" ErrorMessage="검색할 단어를 입력하세요."></asp:RequiredFieldValidator>-->         &nbsp;
            </td>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="검색" OnClick="btnSearch_Click" Theme="MetropolisBlue"></dx:ASPxButton>
            </td>

            <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="false" ShowMessageBox="true" />


        </tr>
    </table>
</div>
<br />

<% if (!string.IsNullOrEmpty(Request.QueryString["SearchField"]) && !String.IsNullOrEmpty(Request.QueryString["SearchQuery"]))
    {
%>
<div style="text-align: left;">
    <a href="ManagePhone.aspx" class="btn btn-success">검색완료</a>
</div>
<% } %>
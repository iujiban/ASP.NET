<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminDelete.aspx.cs" Inherits="boardMVC.DotNetNote.admin.adminDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function ConfirmDelete() {
            var varFlag = false;
            if (window.confirm("현재 글을 삭제하시겠습니까?")) {
                varFlag = true;
            } else {
                varFlag = false;
            } return varFlag;
        }
    </script>

    <h2 style="text-align:center;">게시판</h2>
    <span style="color:#ff0000">글 삭제- 글을 삭제하려면 글 작성 시에 기록하였던 비밀번호가 필요합니다.</span>
    <hr />
    <div style="text-align: center;">
        <asp:Label ID="lblId" runat="server" ForeColor="Red"></asp:Label>
        번 글을 지우겠습니까?
    <br />
        <asp:Button ID="btnDelete" runat="server" Width="100px" CssClass="btn btn-danger" Style="display:inline-block;" 
            Text="지우기" OnClick="btnDelete_Click" />
        <asp:HyperLink ID="lnkCancel" runat="server" CssClass="btn btn-default">취소</asp:HyperLink>
        <br />
        <asp:Label ID ="lblMessage" runat="server" ForeColor="Red" />
        <br />
    </div>
</asp:Content>
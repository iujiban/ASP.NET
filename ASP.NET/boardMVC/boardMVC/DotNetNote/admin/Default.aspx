<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="boardMVC.DotNetNote.admin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <h1>관리자 전용</h1>
    <h2>관리자명: <asp:LoginName ID="LoginName1" runat="server"></asp:LoginName></h2>
</head>
<body>
    <form id="DefaultForm" runat="server">
        <div id="GridView" style="height: 500px; background-color: white; padding: 10px; overflow: auto">
            <%--Form 2개를 webform 하나로 만든 edit --%>
            <h1>회원자 명단들<asp:HyperLink ID="EditLink" runat="server" NavigateUrl="~/DotNetNote/admin/EditUser.aspx">Edit</asp:HyperLink>
            </h1>
            <asp:ScriptManager ID="ScriptManger1" runat="server"></asp:ScriptManager>
            <%--UpdatePanel 컨트롤의 내용을 정의하는 템플릿을 가져오거나 설정합니다. --%>
            <asp:UpdatePanel ID="UpdatePanelService" runat="server" UpdateMode="Conditional">
                <%--UpdatePanel 컨드롤이 랜더링 될 때 컨트롤 내에 표시되는 콘텐츠를 포함합니다. --%>
                <ContentTemplate>
                    <asp:GridView ID="gridService" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                        OnRowEditing="gridService_RowEditing" OnRowUpdating="gridService_RowUpdating" 
                        OnRowCancelingEdit="gridService_RowCancelingEdit" OnRowDeleting="gridService_RowDeleting">
                        <Columns>
                            <%--ID --%>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="User_key">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("user_id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--UserID --%>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="User_ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%#Eval("user_uid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Name --%>
                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("user_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Password --%>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="UserPassword">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("user_Password") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPassword" runat="server" Text='<%#Eval("user_Password") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <%--Level --%>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="UserLevel">
                                <ItemTemplate>
                                    <asp:Label ID="lblLevel" runat="server" Text='<%#Eval("user_Level") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLevel" runat="server" Text='<%#Eval("user_Level") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--Behind Controls 
                            <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CausesValidation="true" Text="Update" CommandName="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" Text="Cancel" CommandName="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                                --%>
                            <%--Delete --%>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
                            <asp:CommandField HeaderText="Edit" ShowEditButton="true" />
                        </Columns>
                    </asp:GridView>
                    <br />
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>

        </div>


    </form>
</body>
</html>

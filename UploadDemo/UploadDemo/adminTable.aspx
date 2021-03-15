<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminTable.aspx.cs" Inherits="UploadDemo.adminTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style>
        .admin {
            text-align: center;
            height: 79px;
        }

        .newStyle1 {
            font-family: HY견고딕;
            text-align: center;
            background-color: #00FFFF;
            background-repeat: no-repeat;
            background-attachment: fixed;
            border-style: double;
            font-size: x-large;
        }

        .newStyle2 {
            text-align: center;
            background-color: #FF0000;
            background-repeat: no-repeat;
            background-attachment: fixed;
            border-style: double;
            font-size: xx-large;
            font-family: HY견고딕;
        }

        .newStyle3 {
            font-family: HY견고딕;
            font-size: xx-large;
            text-indent: inherit;
            text-align: center;
            background-color: #00FF00;
            background-repeat: no-repeat;
            background-attachment: fixed;
            border-style: double;
        }

        .auto-style1 {
        }
        .auto-style2 {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="admin">

            <a>
                <asp:Button ID="PREMIUM" runat="server" Text="PREMIUM" CssClass="newStyle1" Height="66px" OnClick="PREMIUM_Click" /></a>&nbsp;<a><asp:Button ID="SMART" runat="server" Text="SMART" CssClass="newStyle2" Height="66px" OnClick="SMART_Click" /></a>&nbsp;<a><asp:Button ID="BASIC" runat="server" Text="BASIC" CssClass="newStyle3 auto-style1" Height="66px" OnClick="BASIC_Click" /></a>
        </div>
        <br />
        <br />
        <div class="text-center">
            <asp:ScriptManager ID="ScriptManger1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatePanelService" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gridService" runat="server" CellPadding="4" Height="391px" Width="1256px" Visible="False"
                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDeleting="GridView1_RowDeleting" GridLines="None" CssClass="auto-style2" AutoGenerateColumns="False" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="번호" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("번호")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="수신인" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiver" runat="server" Text='<%#Bind("수신인")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="발신인" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCall" runat="server" Text='<%#Bind("발신인")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="전화번호" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNumber" runat="server" Text='<%#Bind("전화번호")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="거래처명" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusinessName" runat="server" Text='<%#Bind("거래처명")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBusinessName" runat="server" Text='<%#Eval("거래처명")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="레벨" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblLevel" runat="server" Text='<%#Bind("ServiceLevel")%>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="수신월일" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiveDate" runat="server" Text='<%#Bind("수신월일")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtReceiveDate" runat="server" Text='<%#Eval("수신월일")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="수신시간" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiveTime" runat="server" Text='<%#Bind("수신시간")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtReceiveTime" runat="server" Text='<%#Eval("수신시간")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="통화종료월일" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Bind("통화종료월일")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%#Eval("통화종료월일")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="통화종료시간" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndTime" runat="server" Text='<%#Bind("통화종료시간")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndTime" runat="server" Text='<%#Eval("통화종료시간")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="근무외시간여부" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblException" runat="server" Text='<%#Bind("근무외시간여부")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtException" runat="server" Text='<%#Eval("근무외시간여부")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="통화시간" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCallTime" runat="server" Text='<%#Bind("통화시간")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
                            <asp:CommandField HeaderText="Edit" ShowEditButton="true" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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

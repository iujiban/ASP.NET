<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IExportUpload.aspx.cs" Inherits="UploadDemo.IExportUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Import / Export Database Data to/ from EXCEL file.</h3>
    <div style="height: 593px">

        <table border="1" class="nav-justified" style="height: 158px; width: 58%; background-color: #FFFF99">
            <tr>
                <td class="modal-sm" style="width: 175px; height: 85px; text-align: right;"><strong>Upload Excel Files:</strong></td>
                <td style="height: 85px; width: 497px;">
                    
                    <asp:FileUpload ID="FileUpload1" runat="server" Height="40px" Width="346px" />
                </td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 175px; height: 96px; text-align: center;">
                    <div align ="center">
                        <asp:Button ID="Button2" runat="server" Height="67px" Style="background-color: #FF5050" Text="Save into Oracle" Visible="False" Width="174px" OnClick="Button2_Click" />
                    </div>
                </td>
                <td style="height: 96px; width: 497px;">
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
        </div>

        <br />
        <br />
        <div>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Height="181px" Width="649px">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="번호" ReadOnly="True" SortExpression="ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="POSTNAME" HeaderText="작성자" SortExpression="POSTNAME" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FILENAME" HeaderText="파일 이름" SortExpression="FILENAME" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FILESIZE" HeaderText="파일 사이즈" SortExpression="FILESIZE" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="POSTLEVEL" HeaderText="레벨" SortExpression="POSTLEVEL" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText ="파일" HeaderStyle-Width ="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#ClassLibrary1.BoardLibrary.FuncFileDownSingle(Convert.ToInt32(Eval("Id")), Eval("FileName").ToString(), Eval("FileSize").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM &quot;FILEUPLOADSAVE&quot;"></asp:SqlDataSource>
        </div>

        <br />
        <br />

    </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="UploadDemo.FileUpload" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="color: chocolate; font-style:normal; font-family:'Times New Roman'">업무용지 파일</h3>
    <div style="background-color: white">
        <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
        <div>
            <table>
                <tr>
                    <td class="modal-sm" style="width: 175px; height: 38px; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="파일 업로드: " Font-Bold="True" Font-Size="Larger" Theme="MetropolisBlue"></dx:ASPxLabel>
                    </td>

                    <td style="height: 38px">
                         <asp:FileUpload ID="FileUpload1" runat="server" Height="40px" Width="346px" CssClass="dxMonthGrid_MetropolisBlue" Font-Bold="False" />
                    </td>
                    <td>
                        &nbsp
                    </td>
                    <td style="text-align: left">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Theme="MetropolisBlue" Text="submit" OnClick="Button2_Click" Height="31px" Font-Size="Large" HorizontalAlign="Center" Width="73px"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <br />
             <asp:Label ID="SucceedMessage" runat="server"></asp:Label> <span aria-hidden="true">|</span>
                <a target="_blank" id="passwordinquiry" href="ManagePhone.aspx">업무폰 파일일지</a>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
            <div>

            </div>
        </div>
    </div>
</asp:Content>

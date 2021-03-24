<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePhone.aspx.cs" Inherits="UploadDemo.ManagePhone" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/SearchFormSingleControl.ascx" TagPrefix="ucl" TagName="SearchFormSingleControl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .dxBase_MetropolisBlue {
            font-family: 'Arial Rounded MT';
            color: chocolate;
        }

        .auto-style1 {
            height: 52px;
        }

        .auto-style2 {
            height: 453px;
        }

        .auto-style4 {
            margin-left: 0px;
        }

        .auto-style5 {
            height: 52px;
            width: 185px;
        }

        .auto-style6 {
            width: 61px;
            height: 52px;
        }

        .auto-style7 {
            width: 72px;
            height: 52px;
        }

        .auto-style9 {
            width: 196px;
            height: 52px;
        }

        .auto-style10 {
            width: 33px;
        }

        .auto-style11 {
            width: 126px;
        }
    </style>
    <!--
    <script type="text/javascript">
        function DateChanged(s, e) {
            var dateValue = s.GetValue();
            getValue.SetText(dateValue);
        }
</script>
        -->
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: white">
            <div class="templateList Text">
                <span class="dxBase_MetropolisBlue ListText" id="ctLabel">업무폰 통화일지</span>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;">
            </div>
            <div class="dx-justification" style="padding-top: 25px;">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="사용자: " Font-Size="Large" Theme="MetropolisBlue"></dx:ASPxLabel>
                        </td>
                        <td class="auto-style5">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="auto-style1">
                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="사용자 조회" Theme="MetropolisBlue" OnClick="ASPxButton2_Click" CssClass="auto-style4"></dx:ASPxButton>
                        </td>
                        <td class="auto-style6">&nbsp
                             &nbsp
                             &nbsp
                        </td>
                        <td class="auto-style10">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="월: " Theme="MetropolisBlue" Font-Size="Large"></dx:ASPxLabel>
                        </td>
                        <td class="auto-style9">

                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"  EditFormat="DateTime" EditFormatString="MM-yy" EnableTheming="True" PickerType="Months" Theme="MetropolisBlue" AutoPostBack="false">
                                <CalendarProperties ShowDayHeaders="False">
                                </CalendarProperties>
                                <ClientSideEvents DateChanged="function(s, e) {cb.PerformCallback(s.GetDate().getTime() / 1000); } " />
                            </dx:ASPxDateEdit>
                            <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="cb" OnCallback="ASPxCallback1_Callback"></dx:ASPxCallback>
                        </td>
                        <td class="auto-style7">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="날짜조회" OnClick="ASPxButton1_Click" Theme="MetropolisBlue"></dx:ASPxButton>
                        </td>
                        <td>&nbsp
                            &nbsp
                            &nbsp
                        </td>
                        <td class="auto-style11">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="문서 다운로드: " Theme="MetropolisBlue" Font-Size="Large"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton3" runat="server" Text="다운로드" Theme="MetropolisBlue" OnClick="ASPxButton3_Click"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
            <div class="SearchList" style="text-align: left">

                <ucl:SearchFormSingleControl runat="server" ID="SearchFormSingleControl"></ucl:SearchFormSingleControl>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
            <div class="auto-style2">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" EnableTheming="True" Theme="MetropolisBlue" Width="1277px" OnRowDeleting="ASPxGridView1_RowDeleting" KeyFieldName="수신인">
                    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" />
                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="TRUE"></FilterControl>
                    </SettingsPopup>
                    <Columns>
                        <dx:GridViewCommandColumn Caption="삭제" VisibleIndex="0" ShowDeleteButton="True">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />

                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="수신인" VisibleIndex="1" Caption="수신인">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="발신인" VisibleIndex="2" Caption="발신인">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="전화번호" VisibleIndex="3" Caption="전화번호">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="거래처명" VisibleIndex="4" Caption="거래처명">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="수신월일" VisibleIndex="5" Caption="수신월일">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="수신시간" VisibleIndex="6" Caption="수신시간">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="통화종료월일" VisibleIndex="7" Caption="통화종료월일">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="통화종료시간" VisibleIndex="8" Caption="통화종료시간">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="근무외시간여부" VisibleIndex="9" Caption="근무외시간여부">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="통화시간" VisibleIndex="10" Caption="통화시간">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="300" />
                    <SettingsPager PageSize="200">
                        <PageSizeItemSettings Visible="false" ShowAllItem="true"></PageSizeItemSettings>
                    </SettingsPager>
                </dx:ASPxGridView>

            </div>

        </div>
    </form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DEMO.Default" %>



<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 57px;
        }

        .templateListText {
            font-family: Arial;
            font-size: medium;
            font-style: normal;
            font-variant: normal;
            text-transform: capitalize;
            color: darkred;
            line-height: normal;
            text-align: left;
        }

        .auto-style2 {
            height: 420px;
        }
    </style>
    <script type="text/javascript">

        var callbackCheck = false;

        function clickEvents() {
            var popupX = (window.screen.width / 2) - (1200 / 2);
            var popupY = (window.screen.height / 2) - (800 / 2);
            var win = window.open('UploadPopup.aspx', 'phone_upload', 'height=400,width=450,Top=' + popupY + ', left=' + popupX + ', status=yes, toolbar=no, menubar=no, location=no, scrollbars=yes, resizable=no, titlebar=no');
            win.onload = function () { win.document.title = "UploadPopUp";  }
           
        }
        
        function CallbackComplete_cb(s, e) {
            if (grid.InCallback()) {
                window.alert('working');
                callbackCheck = true;
            }
            else {
                grid.PerformCallback();
            }
        }
        function EndCallback_gridView(s, e) {
            if (callbackCheck == true) {
                callbackCheck = false;
                window.alert('again');
                grid.PerformCallback();
            }
        }
    </script>
</head>
<body style="height: 850px">
    <form id="aspnetForm" runat="server" method="post">
        <div>
            <div class="templateListText">
                <span class="templateListText" id="ctLabel">업무 폰 통화일지</span>
            </div>
            <div style="text-align: left; vertical-align: middle;">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="MetropolisBlue" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;">
            </div>
            <div class="auto-style1">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="8" ColumnCount="8" Height="54px" EnableTheming="True" Theme="MetropolisBlue" Width="100%">
                    <Items>
                        <dx:LayoutItem Caption="사원명" ColSpan="1" HorizontalAlign="Center" Name="USER_NAME" Width="8%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="Name_Combobox" runat="server" EnableTheming="true" Theme="MetropolisBlue" Height="16px" Width="81px" AllowNull="True" HorizontalAlign="Center">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e) {cb.PerformCallback(s.GetValue())}" />
                                    </dx:ASPxComboBox>
                                    <dx:ASPxCallback ID="userNameCallback" runat="server" ClientInstanceName="cb" OnCallback="userNameCallback_Callback">
                                        <ClientSideEvents CallbackComplete="CallbackComplete_cb" />
                                    </dx:ASPxCallback>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="통화날짜" ColSpan="1" HorizontalAlign="Center" Name="Date_Month" Width="5%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxDateEdit ID="dateEdit" runat="server" EditFormat="DateTime" EditFormatString="yyyy-MM"
                                        EnabledTheming="True" PickerType="Months" Theme="MetropolisBlue" AutoPostBack="false" HorizontalAlign="Center"
                                        ClientInstanceName="dateeditselectmonth" Height="22px" Width="95px">
                                        <CalendarProperties ShowDayHeaders="true"></CalendarProperties>
                                        <ClientSideEvents ValueChanged="function(s,e) {dateCB.PerformCallback(s.GetDate().getTime()/1000);}"
                                            DateChanged="function(s,e) {datedselectReceive.SetDate(s.GetDate()); receiveDateCB.PerformCallback(s.GetDate().getTime()/1000);
                                            datedselectEnd.SetDate(new Date(s.GetDate().getFullYear(), s.GetDate().getMonth()+1, 0)); EndDateCB.PerformCallback(datedselectEnd.GetDate().getTime()/1000);
                                            }" />
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="dateCB" OnCallback="ASPxCallback1_Callback">
                                        <ClientSideEvents CallbackComplete="function(s, e) { grid.PerformCallback();}" />
                                    </dx:ASPxCallback>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ColSpan="1" Caption="기간" Width="7%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" Width="10%">
                                    <dx:ASPxDateEdit ID="receiveDateEdit" runat="server" Height="20px" Width="130px" EditFormat="DateTime" EditFormatString="yyyy-MM-dd"
                                        EnabledTheming="True" PickerType="Days" Theme="MetropolisBlue" AutoPostBack="false" ClientInstanceName="datedselectReceive" NullTextStyle-Wrap="Default" HorizontalAlign="Center">
                                        <CalendarProperties ShowDayHeaders="true" ShowClearButton="true"></CalendarProperties>
                                        <ClientSideEvents ValueChanged="function(s,e) {receiveDateCB.PerformCallback(s.GetDate().getTime()/1000);}" />
                                        <DateRangeSettings MinLength="1" />
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxCallback ID="ReceiveCallback" runat="server" ClientInstanceName="receiveDateCB" OnCallback="ReceiveCallback_Callback">
                                        <ClientSideEvents CallbackComplete="function(s, e) { grid.PerformCallback();}" />
                                    </dx:ASPxCallback>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ColSpan="1" Caption="" Width="1%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" Width="1%">
                                    <dx:ASPxLabel ID="ASPxFormLayout1_E4" runat="server" Text="~" Theme="MetropolisBlue">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ColSpan="1" Caption="" Width="7%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" Width="10%">
                                    <dx:ASPxDateEdit ID="endDateEdit" runat="server" Height="20px" Width="130px"
                                        EditFormat="DateTime" EditFormatString="yyyy-MM-dd" EnabledTheming="True"
                                        PickerType="Days" Theme="MetropolisBlue" AutoPostBack="false" ClientInstanceName="datedselectEnd" CalendarProperties-ShowClearButton="False" HorizontalAlign="Center">
                                        <CalendarProperties ShowDayHeaders="true" ShowClearButton="true"></CalendarProperties>
                                        <ClientSideEvents ValueChanged="function(s,e) {EndDateCB.PerformCallback(s.GetDate().getTime()/1000);}" />
                                        <DateRangeSettings StartDateEditID="receiveDateEdit" MinDayCount="1" />
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxCallback ID="EndCallback" runat="server" ClientInstanceName="EndDateCB" OnCallback="EndCallback_Callback">
                                        <ClientSideEvents CallbackComplete="function(s, e) { grid.PerformCallback();}" />
                                    </dx:ASPxCallback>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem ColSpan="1" Width="34%">
                        </dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem ColSpan="1" Width="10%">
                        </dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="" ColSpan="1" Width="5%" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" Height="27px" Text="업로드" Theme="MetropolisBlue"
                                        Width="73px" AutoPostBack="False" CausesValidation="false" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="clickEvents" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </div>
        </div>
        <div class="auto-style2">
            <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" Width="100%" Theme="MetropolisBlue" EnableTheming="True"
                OnCustomSummaryCalculate="grid_CustomSummaryCalculate" KeyFieldName="PHONE_KEY" ClientInstanceName="grid" ViewStateMode="Enabled"  OnCellEditorInitialize="gridView_CellEditorInitialize" OnRowUpdating="gridView_RowUpdating">
                <SettingsPopup>
                    <FilterControl AutoUpdatePosition="True"></FilterControl>
                </SettingsPopup>
                <SettingsPager Mode="EndlessPaging" PageSize="1000">
                    <PageSizeItemSettings Visible="false" ShowAllItem="TRUE"></PageSizeItemSettings>
                </SettingsPager>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="400" ShowFooter="True" GridLines="None" />
                <SettingsDataSecurity AllowInsert="False" AllowDelete="False" />
                <SettingsSearchPanel ColumnNames="*" Visible="True" />
                <ClientSideEvents EndCallback="EndCallback_gridView" />
                <Columns>
                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" Caption="New" Width="3%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle Font-Bold="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="PHONE_KEY" Visible="False">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="수신인" FieldName="RECIPIENT" VisibleIndex="1" Width="13%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="수신 번호" FieldName="RNUMBER" VisibleIndex="2" Width="6%" >
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="발신인" FieldName="CALLER" VisibleIndex="3" Width="13%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="발신 번호" FieldName="CNUMBER" VisibleIndex="4" Width="6%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="거래처 명" FieldName="BUSINESSNAME" VisibleIndex="5" Width="12%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SERVICELEVEL" VisibleIndex="6" Caption="레벨" Width="5%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="시작 날짜" FieldName="RECEIVEDATE" VisibleIndex="8" AllowTextTruncationInAdaptiveMode="True" ReadOnly="True" UnboundType="DateTime" Width="9%">
                        <PropertiesDateEdit DisplayFormatString="g"></PropertiesDateEdit>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="종료 날짜" FieldName="ENDDATE" VisibleIndex="9" UnboundType="DateTime" Width="9%">
                        <PropertiesDateEdit DisplayFormatString="g"></PropertiesDateEdit>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Caption="외" FieldName="OFFTIME" VisibleIndex="7" Width="2%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="통화시간" FieldName="DURATIONCALL" VisibleIndex="10" Width="5%">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="통화상태" FieldName="CALLSTATUS" VisibleIndex="11" Width="5%"> 
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="비고" FieldName="NOTE" VisibleIndex="12" ShowInCustomizationForm="True" Width="12%">
                        <EditFormSettings Visible="True" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="OFFTIME" SummaryType="Custom" Tag="2" Visible="False" />
                    <dx:ASPxSummaryItem FieldName="DURATIONCALL" ShowInColumn="BUSINESSNAME" SummaryType="Custom" Tag="1" />

                </TotalSummary>
                <Styles>
                    <Header>
                        <Border BorderStyle="None" />
                    </Header>
                    <Cell>
                        <Border BorderStyle="None" />
                        <BorderRight BorderStyle="None" />
                    </Cell>
                    <Footer HorizontalAlign="Center">
                    </Footer>
                    <GroupFooter>
                        <Border BorderStyle="None" />
                    </GroupFooter>
                    <CommandColumnItem>
                        <Border BorderStyle="None" />
                    </CommandColumnItem>
                    <InlineEditCell>
                        <Border BorderStyle="None" />
                    </InlineEditCell>
                    <FilterCell>
                        <Border BorderStyle="None" />
                    </FilterCell>
                </Styles>
                <Border BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </dx:ASPxGridView>
            <dx:ASPxCallback ID="ASPxCallback2" runat="server">
                <ClientSideEvents CallbackComplete="function UpdateGrid() {<%=Page.GetPostBackEventReference(gridview); %> }" />
            </dx:ASPxCallback>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT &quot;PHONE_KEY&quot;, &quot;RECIPIENT&quot;, &quot;RNUMBER&quot;, &quot;CALLER&quot;, &quot;CNUMBER&quot;, &quot;BUSINESSNAME&quot;, &quot;SERVICELEVEL&quot;, &quot;RECEIVEDATE&quot;, &quot;ENDDATE&quot;, &quot;OFFTIME&quot;, &quot;DURATIONCALL&quot;, &quot;CALLSTATUS&quot;, NOTE&quot; FROM &quot;PHONE_DATA&quot;"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE USER_KEY =: USER_KEY AND TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') >= :RECEIVEDATE AND TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') <= :ENDDATE AND TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE2"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="receiveDate" Type="String" />
                    <asp:SessionParameter Name="ENDDATE" SessionField="endDate" Type="String" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="findUser" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE2" SessionField="date" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_ukr" Type="Int32" />
                    <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA  WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM') =:RECEIVEDATE"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="dateEdit" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') >= :RECEIVEDATE AND USER_KEY = :USER_KEY AND TO_CHAR(RECEIVEDATE, 'YYYY-MM')= :RECEIVEDATE2"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="USER_KEY" SessionField="findUser" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="receiveDate" Type="String" />
                    <asp:SessionParameter Name="RECEIVEDATE2" SessionField="date" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="date" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceNull" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT NULL FROM PHONE_DATA WHERE PHONE_KEY = NULL"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sds6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') >= :RECEIVEDATE and TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') <= :RECEIVEDATE2 and TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE3 and USER_KEY = :USER_KEY"
                  UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY" OnUpdated="sds6_Updated1">
                <SelectParameters>
                    <asp:SessionParameter Name="USER_KEY" SessionField="UserKey" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="receiveDate" Type="String" />
                    <asp:SessionParameter Name="RECEIVEDATE2" SessionField="endDate" Type="String" />
                    <asp:SessionParameter Name="RECEIVEDATE3" SessionField="date" Type="String" />
                </SelectParameters>
                 <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                      <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sds7" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM-dd') >= :RECEIVEDATE and TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE2 and USER_KEY = :USER_KEY"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="USER_KEY" SessionField="UserKey" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="receiveDate" Type="String" />
                    <asp:SessionParameter Name="RECEIVEDATE2" SessionField="date" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_ukr" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sds8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE AND USER_KEY = :USER_KEY"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="USER_KEY" SessionField="UserKey" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="dateEdit" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sds9" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE AND USER_KEY = :USER_KEY"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="USER_KEY" SessionField="USER_KEY" Type="Int32" />
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="dateEdit" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                    <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT PHONE_KEY, RECIPIENT, RNUMBER, CALLER, CNUMBER, BUSINESSNAME, SERVICELEVEL, ENDDATE, RECEIVEDATE, DURATIONCALL, OFFTIME, CALLSTATUS, NOTE FROM PHONE_DATA WHERE TO_CHAR(RECEIVEDATE, 'YYYY-MM') = :RECEIVEDATE AND USER_KEY = :USER_KEY"
                UpdateCommand="UPDATE PHONE_DATA SET NOTE = :NOTE WHERE USER_KEY = :USER_KEY and phone_key =: PHONE_KEY">
                <SelectParameters>
                    <asp:SessionParameter Name="RECEIVEDATE" SessionField="date" Type="String" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="USER_KEY" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="PHONE_KEY" SessionField="phone_key" Type="Int32" />
                    <asp:SessionParameter Name="USER_KEY" SessionField="update_uk" Type="Int32" />
                     <asp:SessionParameter Name="NOTE" SessionField="textchanged" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>

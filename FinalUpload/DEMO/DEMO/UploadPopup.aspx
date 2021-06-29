<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPopup.aspx.cs" Inherits="DEMO.UploadPopup" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">
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
    </style>
    <script type="text/javascript">

        function onFileUploadComplete(s, e) {
            if (e.isValid) {
                var time = dateeditselectmonth.GetDate().getMonth() + 1;

                var month = confirm(time + "월의 데이터를 넣으시겠습니까?");
                if (month == true) {
                    cb.PerformCallback(dateeditselectmonth.GetDate().getTime() / 1000);
                    LoadingPanel.Show();
                } else {
                    var cancel = confirm("업데이트가 취소 되었습니다. 창을 닫으시겠습니까?");
                    if (cancel == true) {
                        window.close();
                    } else {

                        document.location.reload(true);
                        document.reload = function () { document.title = "UploadPopUp"; }

                    }

                }

            } else {
                window.alert("Error: 잘못된 형식의 파일 입니다.");
                var select = confirm("파일 형식의 설명서를 보시겠습니까?");
                if (select == true) {
                    var explain = window.open('Explain.aspx', 'Explain_Solution', 'height=400 width=1000, status=yes, toolbar=no, location=no, scrolbars=yes, resizeable=no, titlebar=no');
                    explain.onload = function () { explain.document.title = "Explain"; }
                } else {

                    document.location.reload(true);
                    document.reload = function () { document.title = "UploadPopUp"; }

                }
            }


        }
        function alertFunction(s, e) {
            try {
                if (e.result == "Existsed") {
                    LoadingPanel.Hide();
                    var select = confirm("기존의 데이터를 지우고 새로운 데이터를 입력하려고 합니다. 동의 하시겠습니까?");
                    if (select == true) {
                        LoadingPanel2.Show();
                        noCB.PerformCallback();
                    } else {
                        var cancel = confirm("업데이트가 취소 되었습니다. 창을 닫으시겠습니까?");
                        if (cancel == true) {
                            window.close();
                        } else {
                            document.location.reload(true);
                            document.reload = function () { document.title = "UploadPopUp"; }

                        }
                    }

                } else {
                    LoadingPanel.Hide();
                    if (e.result == "error") {
                        window.alert("Error: 선택된 달의 데이터가 없습니다.");
                        var select = confirm("파일 형식의 설명서를 보시겠습니까?");
                        if (select == true) {
                            var explain = window.open('Explain.aspx', 'Explain_Solution', 'height=400 width=1000, status=yes, toolbar=no, location=no, scrolbars=yes, resizeable=no, titlebar=no');
                            explain.onload = function () { explain.document.title = "Explain"; }
                        } else {
                            window.close();
                        }
                    }
                    else {
                        window.alert("업로드 완료");
                    }

                }

            } catch (e) {
                var explain = window.open('Explain.aspx', 'Explain_Solution', 'height=400 width=1000, status=yes, toolbar=no, location=no, scrolbars=yes, resizeable=no, titlebar=no');
                explain.onload = function () { explain.document.title = "Explain"; }
            }

        }

        function noCBalertFunction(s, e) {
            LoadingPanel2.Hide();
            if (e.result == "error") {
                window.alert("Error: 선택된 달의 데이터가 없습니다.");
                var select = confirm("파일 형식의 설명서를 보시겠습니까?");
                if (select == true) {
                    window.open('Explain.aspx', 'Explain_Solution', 'height=400 width=1000, status=yes, toolbar=no, location=no, scrolbars=yes, resizeable=no, titlebar=no');
                } else {
                    window.close();
                }


            } else {
                window.alert("업로드 완료");
            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="templateListText">
            <span class="templateListText" id="ctLabel">업무 폰 통화 일지 업로드(XML)</span>
        </div>
        <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
        <div class="DateEdit">
            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Height="205px">
                <Items>
                    <dx:LayoutItem ColSpan="1" RowSpan="2" Width="30%" Caption="통화 월">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxDateEdit ID="DateEdit" runat="server" EditFormat="DateTime" EditFormatString="yyyy-MM" EnabledTheming="True" PickerType="Months" Theme="MetropolisBlue" AutoPostBack="true" ClientInstanceName="dateeditselectmonth" Height="20px" Width="81px">
                                    <CalendarProperties ShowDayHeaders="true"></CalendarProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="1" Width="30%" Caption="" RowSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="100%" NullText="Select xml Files"
                                    UploadMode="Advanced" ShowUploadButton="true" ShowProgressPanel="true" Theme="MetropolisBlue" OnFileUploadComplete="uploadControl_FileUploadComplete">
                                    <AdvancedModeSettings EnableMultiSelect="false" EnableFileList="true" EnableDragAndDrop="true"></AdvancedModeSettings>
                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".xml" NotAllowedFileExtensionErrorText="Error: xml 파일이 아닙니다."></ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="function(s,e) {onFileUploadComplete(s,e)}" />
                                </dx:ASPxUploadControl>
                                <dx:ASPxLoadingPanel ID="UploadLoadingPanel1" runat="server" Text="Loading..." Modal="true" ClientInstanceName="LoadingPanel">
                                </dx:ASPxLoadingPanel>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Loading...." Modal="true" ClientInstanceName="LoadingPanel2">
                                </dx:ASPxLoadingPanel>
                                <dx:ASPxCallback runat="server" ID="aspxCallback" ClientInstanceName="cb" OnCallback="aspxCallback_Callback">
                                    <ClientSideEvents CallbackComplete="alertFunction" />
                                </dx:ASPxCallback>
                                <dx:ASPxCallback runat="server" ID="aspxCallbackNo" ClientInstanceName="noCB" OnCallback="aspxCallbackNo_Callback">
                                    <ClientSideEvents CallbackComplete="noCBalertFunction" />
                                </dx:ASPxCallback>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
        </div>
    </form>
</body>
</html>

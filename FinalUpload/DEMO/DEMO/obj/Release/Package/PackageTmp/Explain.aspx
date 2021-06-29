<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Explain.aspx.cs" Inherits="DEMO.Explain" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 224px;
        }

        .templateListText {
            font-family: HY견고딕;
            font-size: medium;
            font-weight: bold;
            font-style: normal;
            font-variant: normal;
            text-transform: capitalize;
            color: dodgerblue;
            line-height: normal;
            text-align: left;
        }

        .auto-style2 {
            width: 1335px;
        }
    </style>
</head>
<body style="height: 420px">
    <form id="form1" runat="server">
        <div class="auto-style1">
            <div class="Explain">
                <span class="templateListText" id="ctLabel">업로드 설명서</span>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
            <div class="dxcpCurrentColor_MetropolisBlue">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Text="Error: 잘못된 형식의 파일 인 경우" Theme="MetropolisBlue" Font-Size="X-Large" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
            <div class="erroExplain">
                <dx:ASPxLabel ID="ASPxLabel" runat="server" Text="
                    xml 파일 형식대로 안 맞추어 이러한 error가 나타난다. 따라서, 밑에 있는 이미지 파일 형식과 맞게 이러한 xml 형식대로
                    dur에서는 통화시간, new에서는 숫자 1: 수신,2: 발신,3: 부재중, name에서는 이름, date와 time도 이러한 형식의 맞추어 
                    값을 적어줘야한다. number에서는 상대방의 전화번호로 지정을 해야 한다. 단 하나라도 dur, new, name, type, date, 
                    time, number의 순서와 형식대로 안 맞추어 진다면 데이터의 값들을 넣지를 못합니다. 
                    따라서, 형식대로 맞추어서 값을 재 입력 혹은 제대로 된 데이터 값들 을 받아주시기 바랍니다."
                    Theme="MetropolisBlue" Font-Size="Medium" Width="100%">
                </dx:ASPxLabel>
            </div>&nbsp
            <div class="auto-style2">
                <dx:ASPxImage ID="xmlError" runat="server" ShowLoadingImage="true" ImageUrl="~/Images/xmlFormat.png" Width="60%"></dx:ASPxImage>
            </div>
        
        <div style="margin: 10px 0px 10px 0px; border-top-style: solid; border-top-width: 1px; border-top-color: #E1E1E1;"></div>
        <div>
            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" Text="Error: xml 파일이 아닙니다." Theme="MetropolisBlue" Font-Size="X-Large" Width="100%">
            </dx:ASPxLabel>
        </div>
        <div class="erroExplain">
            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text=".xml 파일이 아니므로 업로드가 진행을 안합니다. .xml 파일로 바꿔주시기 바랍니다."
                Theme="MetropolisBlue" Font-Size="Medium" Width="100%">
            </dx:ASPxLabel>
        </div>
        <div>
            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" Text="Error: 계속 중복된 파일 에러" Theme="MetropolisBlue" Font-Size="X-Large" Width="100%">
            </dx:ASPxLabel>
        </div>
        <div class="erroExplain">
            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="계속 중복된 파일이 너무 많아 혹은 데이터가 너무 방대해서 에러가 납니다. 업무효율화 팀에 문의 해주시길 바랍니다."
                Theme="MetropolisBlue" Font-Size="Medium" Width="100%">
            </dx:ASPxLabel>
        </div>
        </div>
    </form>
</body>
</html>

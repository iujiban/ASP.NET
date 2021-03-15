<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="UploadDemo.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
        .newStyle1 {
            font-family: HY견고딕;
            font-size: xx-large;
            font-weight: bold;
            font-style: normal;
            font-variant: small-caps;
            text-transform: capitalize;
            color: #0099FF;
            vertical-align: middle;
            text-align: center;
            text-indent: inherit;
            line-height: normal;
            word-spacing: normal;
            background-color: #FFFFFF;
            background-repeat: no-repeat;
            background-attachment: fixed;
            border: thin none #FFFFFF;
        }

        .auto-style2 {
            height: 106px;
            text-align: center;
        }

        .auto-style4 {
            margin-left: 0px;
            width: 167px;
            height: 21px;
        }

        .auto-style5 {
            margin-left: 0px;
            width: 167px;
            height: 21px;
        }

        .auto-style6 {
            width: 177px;
            height: 34px;
            font-weight: bold;
            color: #FFFFFF;
            background-color: #0099CC;
        }

        .auto-style7 {
            height: 23px;
            text-align: center;
        }

        .auto-style8 {
            text-align: center;
        }
        .auto-style9 {
            height: 304px;
        }
    </style>
</head>
<body style="height: 366px">
    <form id="LoginInputType" method="post" runat="server">
        <fieldset class="auto-style9">
                <h1 class="auto-style7"><span class="newStyle1">PhoneUser</span></h1>
            <div class="auto-style2">
                <span>

                    <input name ="userID" id="userID" class="auto-style4" type="text" placeholder="아이디 " />

                    <!--
                     <asp:TextBox ID="txtUser" runat="server" CssClass="auto-style4"></asp:TextBox>-->
                </span>
                <br />

                <span><!--
                     <asp:TextBox ID="txtPassword" runat="server" CssClass="auto-style5" TextMode="Password"></asp:TextBox>-->
                    <input name="userPassword" id="userPassword" class="auto-style5" type="password" placeholder="패스워드" />
                </span>
                <br />
                <strong>
                    <asp:Button ID="LoginButton" Text="로그인" runat="server" CssClass="auto-style6" OnClick="LoginButton_Click" />
                </strong>
            </div>
            <div class="auto-style8">
                <a target="_blank" id="idinquiry" href="FindingUser.aspx">아이디 찾기</a> <span aria-hidden="true">|</span>
                <a target="_blank" id="passwordinquiry" href="FindingPassword.aspx">비밀번호 찾기</a> <span aria-hidden="true">|</span>
                <a target="_blank" id="registerinquiry" href="Register.aspx">회원 가입</a>
            </div>
        </fieldset>
    </form>
</body>
</html>

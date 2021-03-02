<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionDataDisplay.aspx.cs" Inherits="boardMVC.DotNetNote.SessionDataDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
       <form id="form1" 
          runat="server" >
    <div>
       Session Id =  <asp:Label ID="Session_display" runat="server" OnTextChanged="Session_display_TextChanged"></asp:Label> 입니다.
       Session PW =<asp:Label ID="Session_pw" runat="server" onTextChanged="Session_pw_TextChanged"></asp:Label>
    
    </div>
    </form>
</body>
</html>

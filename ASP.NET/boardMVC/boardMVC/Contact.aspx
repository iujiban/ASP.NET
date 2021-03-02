<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="boardMVC.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    Session Id= <asp:Label ID="Session_display" runat="server"></asp:Label> 입니다.
     Session PW =<asp:Label ID="Session_pw" runat="server" onTextChanged="Session_pw_TextChanged"></asp:Label>
</asp:Content>

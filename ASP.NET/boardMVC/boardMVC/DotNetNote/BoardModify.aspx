<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="BoardModify.aspx.cs" 
    ValidateRequest="false"
    Inherits="boardMVC.DotNetNote.BoardModify" 
    EnableSessionState ="True"%>
<%@ Register Src="~/DotNetNote/Controls/BoardEditorFormControl.ascx"
    TagPrefix="ucl" TagName="BoardEditorFormControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ucl:BoardEditorFormControl runat="server" ID="ctlBoardEditorFormControl" />
</asp:Content>

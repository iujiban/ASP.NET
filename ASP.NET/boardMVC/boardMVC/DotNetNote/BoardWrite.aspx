<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="BoardWrite.aspx.cs" ValidateRequest="false"
     Inherits="boardMVC.DotNetNote.BoardWrite" EnableSessionState="True" %>
<%@ Register Src="~/DotNetNote/Controls/BoardEditorFormControl.ascx"
    TagPrefix ="ucl" TagName ="BoardEditorFormControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID ="MainContent" runat ="server">
    <!-- eror rendering -->
    <ucl:BoardEditorFormControl runat="server" id ="ctlBoardEditorFormControl"></ucl:BoardEditorFormControl>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="ChildDocs.aspx.cs" Inherits="Cure.WebAdmin.Admin.ChildDocs" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagName="ResultBox" TagPrefix="uc" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <uc:ResultBox ID="uxResult" runat="server" />
        <div class="panelsdelimiter"></div>

        <div class="content">Документы</div>

        <div class="panelsdelimiter"></div>
        <dx:ASPxFileManager ID="uxFileManager" ClientInstanceName="fmfilemanager" runat="server" Theme="SoftOrange">
            <SettingsEditing AllowDelete="true" AllowDownload="True" AllowRename="true"></SettingsEditing>
            <SettingsToolbar ShowDownloadButton="True" />
            <SettingsUpload UseAdvancedUploadMode="True">
                <AdvancedModeSettings EnableMultiSelect="True" />
            </SettingsUpload>
        </dx:ASPxFileManager>
    </div>
</asp:Content>

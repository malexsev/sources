<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoUploader.ascx.cs" Inherits="Cure.WebAdmin.Controls.PhotoUploader" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxImageGallery" TagPrefix="dx" %>

<asp:HiddenField runat="server" ID="uxItemIdHidden" />
<dx:ASPxUploadControl ID="uxUploadControl" runat="server" UploadMode="Auto" Width="100px" Size="30" ShowProgressPanel="true"
    ShowAddRemoveButtons="true" ShowUploadButton="true" ShowClearFileSelectionButton="true" OnFileUploadComplete="uxUploadControl_FileUploadComplete">
    <ClientSideEvents FileUploadComplete="InitGalery"></ClientSideEvents>
</dx:ASPxUploadControl>

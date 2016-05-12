<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Report.master" CodeBehind="VisitInvitationReport.aspx.cs" Inherits="Cure.WebAdmin.Reports.VisitInvitationReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.1.Web, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <div class="content">
        <dx:ASPxDocumentViewer ID="uxDocumentViewer" runat="server"></dx:ASPxDocumentViewer>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Report.master" CodeBehind="PacientVisitDetailsReport.aspx.cs" Inherits="Cure.WebAdmin.Reports.PacientVisitDetailsReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.1.Web, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <div class="content">
        <dx:ASPxDocumentViewer ID="uxDocumentViewer" runat="server">
            <SettingsReportViewer EnableReportMargins="True" />
            <SettingsSplitter RightPaneVisible="False" SidePaneVisible="False" />
            <SettingsLoadingPanel Text="Обновление&amp;hellip;" />
            <StylesViewer>
                <BookmarkSelectionBorder BorderColor="Gray" BorderStyle="Dashed" BorderWidth="3px"></BookmarkSelectionBorder>
            </StylesViewer>

            <StylesSplitter>
                <Pane>
                    <Paddings Padding="16px"></Paddings>
                </Pane>
            </StylesSplitter>
        </dx:ASPxDocumentViewer>
    </div>
</asp:Content>

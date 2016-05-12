<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="OrderDocs.aspx.cs" Inherits="Cure.WebAdmin.Admin.OrderDocs" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagName="ResultBox" TagPrefix="uc" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <uc:ResultBox ID="uxResult" runat="server" />
        <div class="panelsdelimiter"></div>

        <div class="content">Пациенты</div>

        <dx:ASPxGridView ID="uxVisitGrid" ClientInstanceName="gvVisits" runat="server" AutoGenerateColumns="False" DataSourceID="uxVisitDataSource" KeyFieldName="Id" OnRowInserting="uxVisitGrid_RowInserting" OnRowUpdating="uxVisitGrid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Width="36px" Visible="False">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OrderId" Visible="False" VisibleIndex="16">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Цена" FieldName="Price" VisibleIndex="7" Width="80px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Фонд" FieldName="Fond" VisibleIndex="8" Width="80px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Договор" FieldName="DateDogovor" VisibleIndex="9" Width="60px">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Амб. Номер." FieldName="AmbNumber" VisibleIndex="3" Width="50px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Приглашен" FieldName="IsInvite" VisibleIndex="10" Width="60px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Распечатано" FieldName="IsInvicePrint" VisibleIndex="11" Width="60px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn Caption="Печать" VisibleIndex="12" Width="60px" Visible="False">
                    <DataItemTemplate>
                        <a onclick="javascript:Invite('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">Приглашение</a>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Пациент" FieldName="PacientId" VisibleIndex="4">
                    <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="FullName" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsPager Visible="False">
            </SettingsPager>
            <StylesEditors></StylesEditors>
            <SettingsPopup>
                <EditForm Width="1200px" MinWidth="900px" />
            </SettingsPopup>
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        </dx:ASPxGridView>


        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxPacientDataSource" runat="server" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxVisitDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Visit" DeleteMethod="DeleteVisit" InsertMethod="InsertVisit" SelectMethod="GetOrderVisits" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateVisit">
            <SelectParameters>
                <asp:SessionParameter Name="orderId" SessionField="ExpandOrderId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

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

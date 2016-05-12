<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientSputniks.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.ClientSputniks" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<dx:ASPxGridView ID="uxSputnikGrid" runat="server" ClientInstanceName="gvSputniks" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowInserting="uxSputnikGrid_RowInserting" OnRowUpdating="uxSputnikGrid_RowUpdating">
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0" Width="36px">
        </dx:GridViewCommandColumn>
        <dx:GridViewDataCheckColumn Caption="Главн." FieldName="IsPrimary" VisibleIndex="21" Width="40px">
        </dx:GridViewDataCheckColumn>
        <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEn" Visible="False" VisibleIndex="7">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Фамилия" FieldName="Familiya" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Фамилия En" FieldName="FamiliyaEn" Visible="False" VisibleIndex="6">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Отчество" FieldName="Otchestvo" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="14">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Телефон" FieldName="Contacts" VisibleIndex="15">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Дата рождения" FieldName="BirthDate" VisibleIndex="16" Width="80px">
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Номер паспорта" FieldName="SeriaNumber" Visible="False" VisibleIndex="17">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Пользователь" FieldName="OwnerUser" VisibleIndex="20" Visible="False">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Родство" FieldName="RodstvoId" VisibleIndex="19">
            <PropertiesComboBox DataSourceID="uxRodstvoDataSource" TextField="Name" ValueField="Id">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="OwnerUser" Visible="False" VisibleIndex="31" ReadOnly="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="LastDate" Visible="False" VisibleIndex="32">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="33">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CreateDate" Visible="False" VisibleIndex="34">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="35">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsText EmptyDataRow="Пока ни одного сопровождающего пациента(ов) не добавлено. Введите данные выше, и нажмите кнопку &quot;Добавить сопровождающего&quot;." />
    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
    <SettingsBehavior ConfirmDelete="True" />
    <SettingsPager Visible="False">
    </SettingsPager>
    <SettingsDataSecurity AllowInsert="False" />
</dx:ASPxGridView>

<asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Sputnik" DeleteMethod="DeleteSputnik" SelectMethod="GetSputniks" TypeName="Cure.WebAdmin.Logic.SputnikDataSoruce" UpdateMethod="UpdateSputnik"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

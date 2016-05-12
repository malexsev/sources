<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientCurrOrder.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.ClientCurrOrder" %>
<%@ Import Namespace="Cure.Utils" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<div class="content">Информация по Заявке</div>

<dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Пользователь" FieldName="OwnerUser" VisibleIndex="12" Width="60px">
        </dx:GridViewDataTextColumn>
<%--        <dx:GridViewDataTextColumn Caption="Статус-инфо" FieldName="StatusDecription" VisibleIndex="7">
        </dx:GridViewDataTextColumn>--%>
        <dx:GridViewDataComboBoxColumn Caption="Клиника/Отделение" FieldName="DepartmentId" VisibleIndex="10">
            <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="Name" ValueField="Id">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Статус" FieldName="StatusId" VisibleIndex="4" Width="80px">
            <PropertiesComboBox DataSourceID="uxOrderStatusDataSource" TextField="Name" ValueField="Id">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataDateColumn Caption="Дата начала" FieldName="DateFrom" VisibleIndex="8" Width="60px">
            <PropertiesDateEdit>
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="Дата окончания" FieldName="DateTo" VisibleIndex="9" Width="60px">
            <PropertiesDateEdit>
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Номер рейса" FieldName="TicketInfo" VisibleIndex="14" ToolTip="Прибытие | Убытие">
            <DataItemTemplate>
                <%# SiteUtils.GetReisNumber(Eval("TicketInfo")) %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Время прибытия" FieldName="TicketPribitieTime" VisibleIndex="15" Width="105px">
            <PropertiesDateEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="Время убытия" FieldName="TicketUbitieTime" VisibleIndex="16" Width="105px">
            <PropertiesDateEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
<%--        <dx:GridViewDataTextColumn Caption="Номер" FieldName="Name" VisibleIndex="3" Width="40px">
        </dx:GridViewDataTextColumn>--%>
    </Columns>
    <SettingsPager Visible="False">
    </SettingsPager>
    <SettingsEditing EditFormColumnCount="1">
    </SettingsEditing>
</dx:ASPxGridView>

<div class="content">

    <div class="content"><asp:Label runat="server" ID="uxPacientsLabel" Text="Пациент(ы)"></asp:Label></div>

    <dx:ASPxGridView ID="uxVisitGrid" runat="server" AutoGenerateColumns="False" DataSourceID="uxVisitDataSource" KeyFieldName="Id">
        <Columns>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Имя">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Familiya" Caption="Фамилия" VisibleIndex="3">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Отчество" FieldName="Otchestvo" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Город" FieldName="CityName" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Адрес" FieldName="Address" VisibleIndex="10" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="BirthDate" VisibleIndex="9" Caption="Дата рождения" Width="80px">
                    <PropertiesDateEdit NullDisplayText="Не указано">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="SerialNumber" Visible="False" VisibleIndex="19" Caption="Пасспорт">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Гражданство" FieldName="CountryId" VisibleIndex="5" Width="100px">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Description" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
        </Columns>
        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsPager Visible="False">
        </SettingsPager>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    </dx:ASPxGridView>

    <div class="content"><asp:Label runat="server" ID="uxSputniksLabel" Text="Сопровождающие"></asp:Label></div>

    <dx:ASPxGridView ID="uxSputnikGrid" runat="server" AutoGenerateColumns="False" DataSourceID="uxSputnikDataSource" KeyFieldName="Id">
        <Columns>
            <dx:GridViewDataCheckColumn Caption="Главн." FieldName="IsPrimary" VisibleIndex="21" Width="40px">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEn" VisibleIndex="7">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Фамилия" FieldName="Familiya" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Фамилия En" FieldName="FamiliyaEn" VisibleIndex="6">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Отчество" FieldName="Otchestvo" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="14">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Телефон" FieldName="Contacts" VisibleIndex="15">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Дата рождения" FieldName="BirthDate" VisibleIndex="16">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Номер паспорта" FieldName="SeriaNumber" VisibleIndex="17">
                <EditFormSettings Visible="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Родство" FieldName="RodstvoId" VisibleIndex="19">
                <PropertiesComboBox DataSourceID="uxRodstvoDataSource" TextField="Name" ValueField="Id">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
        </Columns>
        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsPager Visible="False">
        </SettingsPager>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    </dx:ASPxGridView>
</div>

<asp:ObjectDataSource ID="uxVisitDataSource" runat="server" SelectMethod="GetPacientsCurrentOrder" TypeName="Cure.WebAdmin.Logic.PacientDataSoruce"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxOrderStatusDataSource" runat="server" SelectMethod="GetOrderStatus" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>


<asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="GetCurrentOrder" TypeName="Cure.WebAdmin.Logic.PacientDataSoruce"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxSputnikDataSource" runat="server" SelectMethod="GetSputniksCurrentOrder" TypeName="Cure.WebAdmin.Logic.PacientDataSoruce"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxPacientDataSource" runat="server" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="OrderList.aspx.cs" Inherits="Cure.WebAdmin.Admin.OrderList" %>

<%@ Import Namespace="Cure.Utils" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallbackPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register TagPrefix="uc" TagName="PhotoGallery" Src="~/Controls/PhotoGallery.ascx" %>
<%@ Register TagPrefix="uc" TagName="PhotoUploader" Src="~/Controls/PhotoUploader.ascx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function InitGalery(s, e) {
            window.grid.GetRowValues(window.grid.GetFocusedRowIndex(), 'GuidId', OnGetRowValues);
        }

        function OnGetRowValues(values) {
            if (typeof (window.cpGalery) != "undefined") {
                window.cpGalery.PerformCallback(values);
            }
        }

        function RemovePhoto(index) {
            window.cpGalery.PerformCallback(index);
        }

        function Invite(visitId) {
            var cp = window.cpInviteReport;
            cp.PerformCallback(visitId);
        }

        function InviteReportCreated(s, e) {
            var cp = window.pcInformation;
            cp.SetVisible(true);
        }

        function OpenDocs(orderId) {
            var url = '/Admin/OrderDocs.aspx?orderId=' + orderId;
            var win = window.open(url, '_blank');
            win.focus();
        }

        function OpenVipiska(visitId) {
            var url = '/Admin/EditVipiska.aspx?visitId=' + visitId;
            var win = window.open(url, '_blank');
            win.focus();
        }

        function OpenReps(visitId) {
            var url = '/Output/PacientVisitDetailsReport.aspx?visitId=' + visitId;
            var win = window.open(url, '_blank');
            win.focus();
        }

        function OpenInvoice(visitId) {
            var url = '/Output/InvoiceReportParams.aspx?visitId=' + visitId;
            var win = window.open(url, '_blank');
            win.focus();
        }


    </script>
    <div class="content">

        <dx:ASPxCallbackPanel ID="uxInviteCallbackPanel" ClientInstanceName="cpInviteReport" runat="server" Width="200px" OnCallback="uxInviteCallbackPanel_Callback">
            <ClientSideEvents EndCallback="InviteReportCreated"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent ID="PanelContent2" runat="server">
                    <dx:ASPxPopupControl ID="ASPxPopupControl2" runat="server" ClientInstanceName="pcInformation" Top="200" Left="300" Height="140px" Width="250px" AutoUpdatePosition="True" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" HeaderText="Формирование приглашения">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <div class="content">
                                    Приглашение сгенерировано!<br />
                                    файл приглашения сохранён в документах выделенного заказа.
                                </div>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <table cellspacing="4">
            <tr>
                <td style="padding: 5px">Фильтры:</td>
                <td>
                    <dx:ASPxComboBox ID="uxFilter" runat="server" SelectedIndex="0" AutoPostBack="True">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="Все" Value="-1" />
                            <dx:ListEditItem Text="Сейчас на лечении" Value="0" />
                            <dx:ListEditItem Text="Ближайшие 5 дней" Value="5" />
                            <dx:ListEditItem Text="Ближайшие 15 дней" Value="15" />
                            <dx:ListEditItem Text="Ближайшие 30 дней" Value="30" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td style="padding: 5px">Email:</td>
                <td>
                    <dx:ASPxTextBox ID="uxFilterEmailTextBox" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td style="padding: 5px">Фамилия:</td>
                <td>
                    <dx:ASPxTextBox ID="uxFilterFamiliyaTextBox" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td style="padding: 5px">
                    <dx:ASPxButton ID="uxFilterButton" runat="server" Text="Поиск" OnClick="uxFilterButton_Click"></dx:ASPxButton>
                </td>
            </tr>
        </table>
        <div class="panelsdelimiter"></div>
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowUpdating="uxMainGrid_RowUpdating" OnRowInserting="uxMainGrid_RowInserting" OnDetailRowExpandedChanged="uxMainGrid_DetailRowExpandedChanged">
            <ClientSideEvents FocusedRowChanged="InitGalery"></ClientSideEvents>
            <Templates>
                <DetailRow>
                    <div class="content">

                        <div class="content">Пациенты</div>

                        <dx:ASPxGridView ID="uxVisitGrid" ClientInstanceName="gvVisits" runat="server" AutoGenerateColumns="False" DataSourceID="uxVisitDataSource" KeyFieldName="Id" OnRowInserting="uxVisitGrid_RowInserting" OnRowUpdating="uxVisitGrid_RowUpdating">
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="1">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OrderId" Visible="False" VisibleIndex="16">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="13">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="14">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="15">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="CreateDate" Visible="False" VisibleIndex="17">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Пациент" FieldName="PacientId" VisibleIndex="4">
                                    <EditFormSettings Visible="True" />
                                    <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="FullName" ValueField="Id">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesComboBox>
                                    <DataItemTemplate>
                                        <a onclick="javascript:OpenReps('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b"><%# Container.Text %></a>
                                    </DataItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Диагноз" FieldName="TodaysDiagnoz" Visible="False" VisibleIndex="18">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="История болезни и сопутствующие заболевания" FieldName="HystoryA" Visible="False" VisibleIndex="19">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Продолжительность заболевания" FieldName="Hystoryb" Visible="False" VisibleIndex="20">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Доп. информация" FieldName="Razvitie" Visible="False" VisibleIndex="21">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Находитесь ли на диспансерном учете?" FieldName="Dispanser" Visible="False" VisibleIndex="22">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Находитесь ли на нарко-диспансерном учете?" FieldName="DispanserNarko" Visible="False" VisibleIndex="23">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Находитесь ли на кожно-венерологическом диспансерном учете?" FieldName="Dispanser2" Visible="False" VisibleIndex="24">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Имеет ли опасные для здоровья заболевания?" FieldName="DangerousDiseases" Visible="False" VisibleIndex="25">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Сердечно – сосудистые заболевания?" FieldName="Serdce" Visible="False" VisibleIndex="26">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Заболевания дыхательной системы?" FieldName="Dihalka" Visible="False" VisibleIndex="27">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Инфекционные заболевания?" FieldName="Infections" Visible="False" VisibleIndex="28">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Другие опасные для здоровья и окружающих заболевания?" FieldName="OtherDiseases" Visible="False" VisibleIndex="29">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Эпилепсия (Судороги)" FieldName="Epilispiya" Visible="False" VisibleIndex="30">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Тип судорог" FieldName="SudorogiType" Visible="False" VisibleIndex="31">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Количество эпилептических приступов" FieldName="SudorogiCount" Visible="False" VisibleIndex="32">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Противосудорожные препараты" FieldName="SudorogiMedcine" Visible="False" VisibleIndex="33">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Ремиссия" FieldName="Remission" Visible="False" VisibleIndex="34">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Заключение электроэнцефалограммы" FieldName="Encefalogram" Visible="False" VisibleIndex="35">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Цель поездки?" FieldName="MainGoal" Visible="False" VisibleIndex="36">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="физические навыки?" FieldName="Fisical" Visible="False" VisibleIndex="37">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Особенности питания ребенка?" FieldName="Diet" Visible="False" VisibleIndex="38">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Как жует и глотает?" FieldName="Eating" Visible="False" VisibleIndex="39">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Пищеварительные проблемы?" FieldName="EatingProblems" Visible="False" VisibleIndex="40">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Аппетит?" FieldName="Appetit" Visible="False" VisibleIndex="41">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Стул?" FieldName="Stul" Visible="False" VisibleIndex="42">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Аллергия на лекарственные препараты?" FieldName="Alergiya" Visible="False" VisibleIndex="43">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Иммунитет к простудными заболеваниями?" FieldName="Imunitet" Visible="False" VisibleIndex="44">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Как переносит физические нагрузки?" FieldName="Fiznagruzki" Visible="False" VisibleIndex="45">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Состояние и качество сна?" FieldName="Son" Visible="False" VisibleIndex="46">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Увеличиваются приступы при физических нагрузках?" FieldName="ProstupUp" Visible="False" VisibleIndex="47">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Задыхается или закатывается во время болезненных процедур?" FieldName="Zakativaetsa" Visible="False" VisibleIndex="48">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Какое ранее проводилось лечение?" FieldName="KursesRanee" Visible="False" VisibleIndex="49">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Проходили ли когда-либо лечение в Китае?" FieldName="KursesChinaRanee" Visible="False" VisibleIndex="50">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Лечились ли когда-либо нетрадиционными методами?" FieldName="NonTradicial" Visible="False" VisibleIndex="51">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Проводились ли хирургические операции по Вашему заболеванию?" FieldName="Hirurg" Visible="False" VisibleIndex="52">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Получали ли травмы от внешних факторов?" FieldName="Travmi" Visible="False" VisibleIndex="53">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Особые пожелания по проживанию, лечению, реабилитации" FieldName="Requirements" Visible="False" VisibleIndex="54">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Какие необходимы документы от клиники." FieldName="RequiredDocs" Visible="False" VisibleIndex="55">
                                    <PropertiesTextEdit MaxLength="250">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Гражданство" FieldName="PacientId" VisibleIndex="56">
                                    <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="CountryId" ValueField="Id">
                                    </PropertiesComboBox>
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <%# GetSitizenship(Container.Text) %>
                                    </DataItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Город" FieldName="PacientId" VisibleIndex="58">
                                    <EditFormSettings Visible="False" />
                                    <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="CityName" ValueField="Id">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Номер паспорта" FieldName="PacientId" VisibleIndex="59">
                                    <EditFormSettings Visible="False" />
                                    <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="SerialNumber" ValueField="Id">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Приглашение" VisibleIndex="80" Width="80px" ToolTip="Генерация приглашения">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <a onclick="javascript:Invite('<%# Container.KeyValue %>');"  class="hyperlink" style="color: #27408b">
                                            Сгенерировать</a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Выписка" VisibleIndex="81" Width="80px" ToolTip="Редактирование выписки">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <a onclick="javascript:OpenVipiska('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">
                                            Редактировать</a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Счёт" VisibleIndex="81" Width="80px" ToolTip="Счёт на лечение">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <a onclick="javascript:OpenInvoice('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">
                                            Сформировать</a>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <StylesEditors></StylesEditors>
                            <SettingsPopup>
                                <EditForm Width="1200px" MinWidth="900px" />
                            </SettingsPopup>
                        </dx:ASPxGridView>

                        <div class="content">Сопровождающие</div>

                        <dx:ASPxGridView ID="uxSputnikGrid" runat="server" AutoGenerateColumns="False" DataSourceID="uxSputnikDataSource" KeyFieldName="Id" OnRowInserting="uxSputnikGrid_RowInserting" OnRowUpdating="uxSputnikGrid_RowUpdating">
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="1">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OrderId" Visible="False" VisibleIndex="2">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn Caption="Главн." FieldName="IsPrimary" VisibleIndex="21" Width="40px">
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEn" Visible="False" VisibleIndex="6">
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Фамилия" FieldName="Familiya" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Фамилия En" FieldName="FamiliyaEn" Visible="False" VisibleIndex="7">
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
                                <dx:GridViewDataTextColumn Caption="Номер паспорта" FieldName="SeriaNumber" Visible="False" VisibleIndex="17">
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Гражданство" FieldName="CountryId" VisibleIndex="18" Width="100px">
                                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Пользователь" FieldName="OwnerUser" VisibleIndex="20" Visible="False">
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="22">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="23">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="24">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="CreateDate" Visible="False" VisibleIndex="25">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataDateColumn>
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
                        </dx:ASPxGridView>
                    </div>
                </DetailRow>
            </Templates>
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px" ShowClearFilterButton="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Пользователь" FieldName="OwnerUser" VisibleIndex="13" Width="60px">
                    <EditFormSettings ColumnSpan="2" VisibleIndex="14" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="21">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="22">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="23">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="DateSend" VisibleIndex="9" Caption="Дата создания" Width="60px">
                    <PropertiesDateEdit NullDisplayText="&lt;нет данных&gt;">
                    </PropertiesDateEdit>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Статус-инфо" FieldName="StatusDecription" VisibleIndex="20" Visible="false">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Клиника/Отделение" FieldName="DepartmentId" VisibleIndex="11">
                    <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="ShortName" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="4" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Доп. информация" FieldName="Description" Visible="False" VisibleIndex="19">
                    <PropertiesMemoEdit MaxLength="500" Rows="5">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" ColumnSpan="2" VisibleIndex="13" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataComboBoxColumn Caption="Статус" FieldName="StatusId" VisibleIndex="5" Width="80px">
                    <PropertiesComboBox DataSourceID="uxOrderStatusDataSource" TextField="Name" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="3" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Примечание" ToolTip="Видно только администраторам" FieldName="Notes" VisibleIndex="26">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings VisibleIndex="11" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataDateColumn Caption="Дата начала" FieldName="DateFrom" VisibleIndex="8" Width="60px">
                    <PropertiesDateEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <EditFormSettings VisibleIndex="5" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Дата окончания" FieldName="DateTo" VisibleIndex="10" Width="60px">
                    <PropertiesDateEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <EditFormSettings VisibleIndex="6" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Номер рейса" FieldName="TicketInfo" VisibleIndex="14" ToolTip="Прибытие | Убытие">
                    <EditFormSettings VisibleIndex="9" />
                    <DataItemTemplate>
                        <%# SiteUtils.GetReisNumber(Eval("TicketInfo")) %>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Время прибытия" FieldName="TicketPribitieTime" VisibleIndex="16" Width="105px">
                    <PropertiesDateEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
                    </PropertiesDateEdit>
                    <EditFormSettings VisibleIndex="7" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Время убытия" FieldName="TicketUbitieTime" VisibleIndex="17" Width="105px">
                    <PropertiesDateEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
                    </PropertiesDateEdit>
                    <EditFormSettings VisibleIndex="8" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Номер" FieldName="Name" VisibleIndex="3" Width="40px" Visible="false">
                    <EditFormSettings Visible="True" VisibleIndex="1" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="GuidId" FieldName="GuidId" Visible="False" VisibleIndex="24">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Док" VisibleIndex="27" Width="25px">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <a onclick="javascript:OpenDocs('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">
                            <img src="../Content/Images/editors_upload.gif" />
                        </a>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Другие услуги в Пекине" FieldName="ServicePekinOther" Visible="False" VisibleIndex="31">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Другие услуги в Юньчэн" FieldName="ServiceUnchenOther" Visible="False" VisibleIndex="34">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn Caption="Виза Дней" FieldName="VizaDney" VisibleIndex="15" Width="60px" Visible="False">
                    <EditFormSettings Visible="True" VisibleIndex="12" />
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataCheckColumn Caption="Услуги переводчика" FieldName="ServicePekinIsPerevod" Visible="False" VisibleIndex="28">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Забронировать Отель" FieldName="ServicePekinIsHotel" Visible="False" VisibleIndex="30">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Встреча в Юньчэн" FieldName="ServiceUnchenIsVstrecha" Visible="False" VisibleIndex="33">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Туалетная Бумага" FieldName="ServiceRoomIsPaper" Visible="False" VisibleIndex="36">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Стиральный порошок" FieldName="ServiceRoomIsStiral" Visible="False" VisibleIndex="38">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Ополаскиватель для белья" FieldName="ServiceRoomIsOpolask" Visible="False" VisibleIndex="40">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Питьевая вода" FieldName="ServiceRoomIsVoda" Visible="False" VisibleIndex="44">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Жидкое мыло" FieldName="ServiceRoomIsMilo" Visible="False" VisibleIndex="43">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Средство для мытья посуды" FieldName="ServiceRoomIsPosuda" Visible="False" VisibleIndex="45">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataComboBoxColumn Caption="Вариант прибытия" FieldName="TransferInfo" Visible="False" VisibleIndex="46">
                    <PropertiesComboBox DataSourceID="uxTransferInfoDataSource" NullDisplayText="(уточняется)" NullText="(уточняется)" TextField="Name" ValueField="Id" ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" VisibleIndex="10" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataCheckColumn Caption="Согласен" FieldName="IsAgree" VisibleIndex="4" Width="45px">
                    <EditFormSettings VisibleIndex="2" />
                </dx:GridViewDataCheckColumn>
            </Columns>
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsPager AlwaysShowPager="True" NumericButtonCount="20">
            </SettingsPager>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowGroupPanel="True" />
            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Order" DeleteMethod="DeleteOrder" InsertMethod="InsertOrder" SelectMethod="GetOrders" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateOrder" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="uxFilter" DefaultValue="-1" Name="filter" PropertyName="Value" Type="Int32" />
                <asp:ControlParameter ControlID="uxFilterEmailTextBox" DefaultValue="" Name="email" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="uxFilterFamiliyaTextBox" DefaultValue="" Name="familiya" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxVisitDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Visit" DeleteMethod="DeleteVisit" InsertMethod="InsertVisit" SelectMethod="GetOrderVisits" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateVisit">
            <SelectParameters>
                <asp:SessionParameter Name="orderId" SessionField="ExpandOrderId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxPacientDataSource" runat="server" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxSputnikDataSource" runat="server" SelectMethod="GetOrderSputniks" TypeName="Cure.DataAccess.BLL.DataAccessBL" DeleteMethod="DeleteSputnik" DataObjectTypeName="Cure.DataAccess.Sputnik" InsertMethod="InsertSputnik" UpdateMethod="UpdateSputnik">
            <SelectParameters>
                <asp:SessionParameter Name="orderId" SessionField="ExpandOrderId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxOrderStatusDataSource" runat="server" SelectMethod="GetOrderStatus" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxTransferInfoDataSource" runat="server" SelectMethod="GetDepartmentTransferInfos" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

        <dx:ASPxCallbackPanel ID="uxCallbackPanel" ClientInstanceName="cpGalery" runat="server" Width="100%" OnCallback="uxCallbackPanel_OnCallback">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <uc:PhotoGallery runat="server" ID="uxPhotoGallery"></uc:PhotoGallery>
                    <uc:PhotoUploader runat="server" ID="uxPhotoUploader"></uc:PhotoUploader>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>

</asp:Content>

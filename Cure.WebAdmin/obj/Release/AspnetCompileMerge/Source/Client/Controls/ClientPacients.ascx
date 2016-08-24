<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientPacients.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.ClientPacients" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<dx:ASPxGridView ID="uxPacientsGrid" ClientInstanceName="gvPacients" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnRowUpdating="uxPacientsGrid_RowUpdating" OnRowInserting="uxPacientsGrid_RowInserting" DataSourceID="uxMainDataSource" OnDetailRowExpandedChanged="uxPacientsGrid_DetailRowExpandedChanged">
    <Templates>

        <DetailRow>
            <div class="content">

                <div class="content">Медицинские показания, состояние и навыки</div>

                <dx:ASPxGridView ID="uxVisitGrid" ClientInstanceName="gvVisits" runat="server" AutoGenerateColumns="False" DataSourceID="uxVisitDataSource" KeyFieldName="Id" OnRowUpdating="uxVisitGrid_RowUpdating">
                    <Columns>
                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" Width="36px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="6">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="18">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="19">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="20">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="CreateDate" Visible="False" VisibleIndex="22">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataMemoColumn Caption="Выписка" FieldName="Vipiska" VisibleIndex="11" Visible="False">
                            <EditFormSettings Visible="True" />
                            <PropertiesMemoEdit MaxLength="500" Rows="4">
                            </PropertiesMemoEdit>
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataTextColumn Caption="Диагноз" FieldName="TodaysDiagnoz" VisibleIndex="2">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="История болезни и сопутствующие заболевания" FieldName="HystoryA" Visible="False" VisibleIndex="23">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Продолжительность заболевания" FieldName="Hystoryb" Visible="False" VisibleIndex="24">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Доп. информация" FieldName="Razvitie" Visible="False" VisibleIndex="25">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Находитесь ли на диспансерном учете?" FieldName="Dispanser" Visible="False" VisibleIndex="26">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Находитесь ли на нарко-диспансерном учете?" FieldName="DispanserNarko" Visible="False" VisibleIndex="27">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Находитесь ли на кожно-венерологическом диспансерном учете?" FieldName="Dispanser2" Visible="False" VisibleIndex="28">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Имеет ли опасные для здоровья заболевания?" FieldName="DangerousDiseases" Visible="False" VisibleIndex="29">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Сердечно – сосудистые заболевания?" FieldName="Serdce" Visible="False" VisibleIndex="30">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Заболевания дыхательной системы?" FieldName="Dihalka" Visible="False" VisibleIndex="31">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Инфекционные заболевания?" FieldName="Infections" Visible="False" VisibleIndex="32">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Другие опасные для здоровья и окружающих заболевания?" FieldName="OtherDiseases" Visible="False" VisibleIndex="33">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Эпилепсия (Судороги)" FieldName="Epilispiya" Visible="False" VisibleIndex="34">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Тип судорог" FieldName="SudorogiType" Visible="False" VisibleIndex="35">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Количество эпилептических приступов" FieldName="SudorogiCount" Visible="False" VisibleIndex="36">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Противосудорожные препараты" FieldName="SudorogiMedcine" Visible="False" VisibleIndex="37">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ремиссия" FieldName="Remission" Visible="False" VisibleIndex="38">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Заключение электроэнцефалограммы" FieldName="Encefalogram" Visible="False" VisibleIndex="39">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Цель поездки?" FieldName="MainGoal" VisibleIndex="1">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Физические навыки?" FieldName="Fisical" VisibleIndex="3">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Особенности питания ребенка?" FieldName="Diet" Visible="False" VisibleIndex="40">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Как жует и глотает?" FieldName="Eating" Visible="False" VisibleIndex="41">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Пищеварительные проблемы?" FieldName="EatingProblems" Visible="False" VisibleIndex="42">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Аппетит?" FieldName="Appetit" VisibleIndex="4">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Стул?" FieldName="Stul" VisibleIndex="5">
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
                        <dx:GridViewDataTextColumn Caption="Разговаривает?" FieldName="Razgovor" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="56">
                            <PropertiesTextEdit MaxLength="250">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Инструкции выполняет?" FieldName="Instructcii" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="57">
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ConfirmDelete="True" />
                    <SettingsPager Visible="False">
                    </SettingsPager>
                    <SettingsPopup>
                        <EditForm Width="1200px" MinWidth="900px" />
                    </SettingsPopup>
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                </dx:ASPxGridView>
            </div>
        </DetailRow>
    </Templates>
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0" Width="36px" ShowInCustomizationForm="True">
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" Caption="Имя" ShowInCustomizationForm="True">
            <PropertiesTextEdit>
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEng" Visible="False" VisibleIndex="15" ShowInCustomizationForm="True">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Familiya" Caption="Фамилия" VisibleIndex="2" ShowInCustomizationForm="True">
            <PropertiesTextEdit>
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Фамилия En" FieldName="FamiliyaEn" Visible="False" VisibleIndex="14" ShowInCustomizationForm="True">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Отчество" FieldName="Otchestvo" VisibleIndex="4" ShowInCustomizationForm="True">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Город" FieldName="CityName" VisibleIndex="6" ShowInCustomizationForm="True">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="BirthDate" VisibleIndex="9" Caption="Дата рождения" Width="80px" ShowInCustomizationForm="True">
            <PropertiesDateEdit NullDisplayText="Не указано">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
            <EditFormSettings Visible="True" />
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="SerialNumber" Visible="False" VisibleIndex="19" Caption="Пасспорт" ShowInCustomizationForm="True">
            <EditFormSettings Visible="True" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="OwnerUser" Visible="False" VisibleIndex="31" ReadOnly="True" ShowInCustomizationForm="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="5" ShowInCustomizationForm="True">
            <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataMemoColumn Caption="Anamnez" FieldName="Anamnez" Visible="False" VisibleIndex="26" ShowInCustomizationForm="True">
            <PropertiesMemoEdit Rows="10">
            </PropertiesMemoEdit>
            <EditFormSettings Visible="False" />
        </dx:GridViewDataMemoColumn>
        <dx:GridViewDataMemoColumn Caption="Диагноз" FieldName="Diagnoz" Visible="False" VisibleIndex="25" ShowInCustomizationForm="True">
            <PropertiesMemoEdit Rows="5">
            </PropertiesMemoEdit>
            <EditFormSettings Visible="False" />
        </dx:GridViewDataMemoColumn>
        <dx:GridViewDataTextColumn FieldName="LastDate" Visible="False" VisibleIndex="32" ShowInCustomizationForm="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="33" ShowInCustomizationForm="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CreateDate" Visible="False" VisibleIndex="34" ShowInCustomizationForm="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="35" ShowInCustomizationForm="True">
            <EditFormSettings Visible="False" />
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsText EmptyDataRow="Пока ни одного пациента не добавлено. Введите данные выше, и нажмите кнопку &quot;Добавить пациента&quot;." />
    <SettingsDataSecurity AllowInsert="False" />
    <SettingsPager Visible="False">
    </SettingsPager>
    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" ShowDetailButtons="True" />
</dx:ASPxGridView>

<asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Pacient" DeleteMethod="DeletePacient" SelectMethod="GetPacients" TypeName="Cure.WebAdmin.Logic.PacientDataSoruce" UpdateMethod="UpdatePacient"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxSputnikDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Pacient" DeleteMethod="DeletePacient" InsertMethod="InsertPacient" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdatePacient"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

<asp:ObjectDataSource ID="uxVisitDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Visit" DeleteMethod="DeleteVisit" InsertMethod="InsertVisit" SelectMethod="GetVisit" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateVisit" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:SessionParameter Name="visitId" SessionField="ExpandVisitId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

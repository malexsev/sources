<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewOrder.aspx.cs" Inherits="Cure.WebAdmin.Client.NewOrder" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Src="~/Client/Controls/ClientPacients.ascx" TagPrefix="uc" TagName="ClientPacients" %>
<%@ Register Src="~/Client/Controls/ClientSputniks.ascx" TagPrefix="uc" TagName="ClientSputniks" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagPrefix="uc" TagName="ResultBox" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var addIteType;

        function AddPacient(s, e) {
            addIteType = 'addpacient';
            if (window.ASPxClientEdit.ValidateGroup('groupPacients')) {
                window.cbOrder.PerformCallback(addIteType);
            }
        }

        function AddSputnik(s, e) {
            addIteType = 'addsputnik';
            if (window.ASPxClientEdit.ValidateGroup('groupSputniks')) {
                window.cbOrder.PerformCallback(addIteType);
            }
        }

        function FillBasedOnThePrevious(s, e) {
            addIteType = 'fillprevious';
            window.ASPxClientEdit.ClearGroup('groupPacients');
            window.addNewPacientPanel.SetCollapsed(true);
            window.gvPacients.Refresh();
            window.ASPxClientEdit.ClearGroup('groupSputniks');
            window.addNewSputnikPanel.SetCollapsed(true);
            window.gvSputniks.Refresh();
            window.cbOrder.PerformCallback(addIteType);
        }

        function EndCallbackActs(s, e) {
            if (addIteType == 'addpacient') {
                window.ASPxClientEdit.ClearGroup('groupPacients');
                window.addNewPacientPanel.SetCollapsed(true);
                window.gvPacients.Refresh();
            }
            if (addIteType == 'addsputnik') {
                window.ASPxClientEdit.ClearGroup('groupSputniks');
                window.addNewSputnikPanel.SetCollapsed(true);
                window.gvSputniks.Refresh();
            }
        }

        function HideAddPacientPanel(s, e) {
            window.addNewPacientPanel.SetCollapsed(!window.addNewPacientPanel.GetCollapsed());
        }
        function HideAddSputnikPanel(s, e) {
            window.addNewSputnikPanel.SetCollapsed(!window.addNewSputnikPanel.GetCollapsed());
        }

    </script>


    <div class="content">

        <p><strong>При заполнении заявки следует принять во внимание:</strong></p>
        <ul>
            <li><font color="red">если Вы заполняете заявку первый раз</font>, рекомендуем Вам воспользоваться <a href="NewOrderWizard.aspx">"Мастером заполнения новой заявки"</a></li>
            <li>в п. 2.1, 2.2  история болезни и сопутствующие заболевания подробно опишите из анамнеза жизни.</li>
            <li>в п.5, п.6 и п.7 кратко изложить, где, когда проходили лечение, подробнее о результатах лечения.</li>
            <li>дети до 18 лет принимаются на лечение только с сопровождающим
            </li>
            <li>в п.14 п.15  укажите точные даты, в течении которого можете поехать на лечение. Срок прохождения лечения в нашей клинике – от 30 дней.</li>
            <li>если у Вашего ребёнка есть патология сердечно сосудистой и дыхательной систем, частые и тяжело протекающие эпиприступы, заразные инфекционные заболевания, и другие опасные для здоровья и окружающих заболевания - эти пациенты не могут получать лечение в нашей больнице.</li>
            <li>больница обеспечивает конфиденциальность представленной информации.
            </li>
            <li>ссылки на другие Документы (выписки) не допускаются, просим Вас самостоятельно заполнять все данные.</li>
        </ul>

        <dx:ASPxCallbackPanel ID="uxCallbackPanel" ClientInstanceName="cbOrder" runat="server" Width="100%" OnCallback="uxCallbackPanel_Callback">
            <ClientSideEvents EndCallback="EndCallbackActs"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent ID="PanelContent4" runat="server">
                    <div class="panelsdelimiter"></div>
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" ShowHeader="False" runat="server" DefaultButton="uxAddPacientButton" ShowCollapseButton="True" ShowDefaultImages="False" Width="100%" HeaderText="Пациент(ы) на лечение">
                        <PanelCollection>
                            <dx:PanelContent ID="ctPacient" runat="server" ClientIDMode="Static">

                                <p><a href="#" runat="server" id="uxFillFromPreviousLink" onclick="FillBasedOnThePrevious();">Заполнить заявку на основе данных предыдущей заявки</a></p>

                                <dx:ASPxRoundPanel ID="ASPxRoundPanel3" ClientInstanceName="addNewPacientPanel" runat="server" ShowCollapseButton="True" ShowDefaultImages="False" Width="100%" HeaderText="Данные пациента">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" ClientIDMode="Static">
                                            <strong>После заполнения данных нового пациента - обязательно нажмите "Добавить пациента" ниже. Только в этом случае введённый пациент добавится в список пациентов.</strong>
                                            <table>
                                                <tr>
                                                    <td class="item">1.1.1. Фамилия</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Иванов" ID="uxPacientFamiliya" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.1.2. Имя</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Иван" ID="uxPacientName" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.1.3. Отчество</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Иванович" ID="uxPacientOtchestvo" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.2.1. Фамилия на английском языке (с заграничного паспорта)
                                                        <br />
                                                        При отсутствии заграничного паспорта введите фамилию латинскими буквами
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Ivavon" ID="uxPacientFamiliyaEn" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.2.2. Имя на английском языке (с заграничного паспорта) 
                                                        <br />
                                                        При отсутствии заграничного паспорта введите имя латинскими буквами
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Ivan" ID="uxPacientNameEng" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.3. Серия и номер заграничного паспорта
                                                        <br />
                                                        При отсутствии заграничного паспорта введите серию и номер свидетельства о рождении</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="0000-00000" ID="uxPacientSerialNumber" runat="server" Width="100px" MaxLength="250">
                                                            <CaptionSettings></CaptionSettings>
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.4 Дата рождения</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxDateEdit NullText="12.07.1962" ID="uxPacientBirthDate" runat="server" Width="100px">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.5.1 Гражданство</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxComboBox NullText="Россия" ID="uxPacientCountryId" runat="server" DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" Width="300px">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">1.5.2 Область, город постоянного места проживания</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox NullText="Москва" ID="uxPacientCityName" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td class="item">1.5.3 Адрес</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxPacientAddress" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>--%>
                                                <%--                                                <tr>
                                                    <td class="item">1.6 Дополнительная информация о пациенте</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitAdditional" runat="server" Height="50px" Width="700px" MaxLength="2500">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                            <div class="panelsdelimiter"></div>
                                            <p><strong>2. Медицинские показания пациента.</strong></p>
                                            <div class="panelsdelimiter"></div>
                                            <table>
                                                <tr>
                                                    <td class="item">2.1 Диагноз пациента из последних выписок</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitTodaysDiagnoz" runat="server" Width="700px" MaxLength="2500">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.2.a История болезни. Опишите подробно из медицинских документов: беременность, роды, вес, АПГАР, ИВЛ,<br />
                                                        реанимация, обследование, лечение. Когда заметили у ребенка проблемы, сопутствующие заболевания.</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitHystoryAm" runat="server" Width="700px" Rows="10">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.2.b Продолжительность заболевания</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitHystoryB" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.3. Этапы физического развития с момента рождения<br />
                                                        (Когда начал держать голову, сидеть, ползать, стоять, ходить, и т д.?)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitRazvitie" runat="server" Width="700px" Rows="5">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.4.а. Находится ли ребенок на психиатрическом или наркологическом диспансерном учете? С каким диагнозом?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitDispanser" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.4.б. Находится ли ребенок на кожно-венерологическом диспансерном учете? С каким диагнозом?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitDispanserB" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.5. Имеет ли опасные для здоровья заболевания?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitDangerousDiseases" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.5.1. Сердечно – сосудистые заболевания?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitSerdce" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.5.2. Заболевания дыхательной системы?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitDihalka" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.5.3. Инфекционные заболевания?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitInfections" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.5.4. Другие опасные для здоровья и окружающих заболевания?</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitOtherDiseases" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6. Эпилепсия (Судороги) </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitEpilispiya" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6.1. Тип судорог (На фоне чего начинаются судороги, какая продолжительность.<br />
                                                        Опишите эпи-приступы и подробно как они протекают.)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitSudorogiTypem" runat="server" Width="700px">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6.2. Количество эпилептических приступов (За один день или месяц)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitSudorogiCount" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6.3. Противосудорожные препараты (Наименование и дозировка)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitSudorogiMedcine" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6.4. Ремиссия (Когда был последний приступ)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitRemission" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">2.6.5. Заключение электроэнцефалограммы</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxMemo ID="uxVisitEncefalogramm" runat="server" Width="700px">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">3.Цель поездки? (Какие Вы хотите получить реальные результаты от реабилитации)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitMainGoal" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="panelsdelimiter"></div>
                                            <p><strong>4. Состояние пациента и её (его) навыки:</strong></p>
                                            <div class="panelsdelimiter"></div>
                                            <table>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitRazgovor" NullText="Разговаривает? Словарный запас?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitInstructcii" NullText="Инструкции выполняет?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitFisical" NullText="Физические навыки? (Что умеет ребенок делать сам и с помощью)" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitDiet" NullText="Опишите особенности питания ребенка? (Сам ест, перетертая пища или нет)" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitEating" NullText="Как жует и глотает?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitEatingProblems" NullText="Пищеварительные проблемы?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitAppetit" NullText="Аппетит?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitStul" NullText="Стул?" runat="server" Width="700px" MaxLength="50">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitAlergiya" NullText="Аллергия на лекарственные препараты?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitImunitet" NullText="Иммунитет к простудными заболеваниями?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitFiznagruzki" NullText="Как переносит физические нагрузки?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitSon" NullText="Состояние и качество сна?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitProstupUp" NullText="Увеличиваются приступы при физических нагрузках?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitZakativaetsa" NullText="Задыхается или закатывается во время болезненных процедур?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td class="item">5. Какое ранее проводилось лечение по Вашему заболеванию и его результаты?<br />
                                                        (Где, продолжительность, количество пройденных курсов и результаты)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitKursesRanee" NullText="Где, продолжительность, количество пройденных курсов и результаты?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">6. Проходили ли когда-либо лечение в Китае? (Где, продолжительность, количество пройденных курсов и результаты)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitKursesChinaRanee" NullText="Где, продолжительность, количество пройденных курсов и результаты?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">7. Лечились ли когда-либо методами китайской традиционной медицины за пределами Китая?<br />
                                                        (Иглорефлексотерапия, китайский массаж, лечение китайскими лекарственными препаратами.<br />
                                                        Где, продолжительность, количество пройденных курсов и результаты)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitNonTradicial" NullText="Где, продолжительность, количество пройденных курсов и результаты?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">8. Проводились ли хирургические операции по Вашему заболеванию? (Название, дата, результат, рецидив)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitHirurg" NullText="Название, дата, результат, рецидив" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">9. Получали ли травмы от внешних факторов? (ДТП, несчастные случаи)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitTravmi" NullText="Опишите подробно?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">10. Особые пожелания по проживанию, лечению, реабилитации.</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitRequirements" NullText="Опишите подробно?" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">11. Какие необходимы документы от клиники. (Приглашение, cчет на лечение. )</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitRequiredDocs" NullText="Приглашение, Счет на лечение" runat="server" Width="700px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">12 Дополнительная информация о  пациенте (заполняется по желанию родителей\законных представителей).<br />
                                                        Какую еще информацию, Вы можете добавить о пациенте касающуюся предстоящего лечения</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxVisitAdditional" runat="server" Height="50px" Width="700px" MaxLength="2500">
                                                            <ValidationSettings ValidationGroup="groupPacients">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <strong>Проверьте введённые данные пациента, затем нажмите кнопку ниже.</strong>
                                            <table>
                                                <tr>
                                                    <td class="item" colspan="2">
                                                        <dx:ASPxButton ID="uxAddPacientButton" runat="server" Text="Добавить пациента" AutoPostBack="False">
                                                            <ClientSideEvents Click="AddPacient"></ClientSideEvents>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <div class="panelsdelimiter"></div>

                                <uc:ClientPacients runat="server" ID="uxClientPacients" Width="100%" />

                                <%--<p><a href="#" onclick="HideAddPacientPanel();">Доавить нового пациента в завяку:</a></p>--%>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <div class="panelsdelimiter"></div>
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowCollapseButton="True" ShowDefaultImages="False" Width="100%" HeaderText="Детали заезда">
                        <PanelCollection>
                            <dx:PanelContent ID="ctDepartnemt" runat="server" ClientIDMode="Static">
                                <table>
                                    <tr>
                                        <td class="item">13. Выбор реабилитационного отделения.</td>
                                        <td class="item">
                                            <dx:ASPxComboBox ID="uxOrderDepartment" runat="server" DataSourceID="uxDepartmentDataSource" TextField="Name" ValueField="Id" Width="500px">
                                                <ValidationSettings ValidationGroup="groupOrder" Display="Static">
                                                    <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">14. Предполагаемая дата заезда</td>
                                        <td class="item">
                                            <dx:ASPxDateEdit ID="uxOrderDateFrom" runat="server" Height="17px" Width="100px">
                                                <ValidationSettings ValidationGroup="groupOrder" Display="Static">
                                                    <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">15. Предполагаемая дата выезда</td>
                                        <td class="item">
                                            <dx:ASPxDateEdit ID="uxOrderDateTo" runat="server" Width="100px">
                                                <ValidationSettings ValidationGroup="groupOrder" Display="Static">
                                                    <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">16. Дополнительная информация по заявке</td>
                                        <td class="item">
                                            <dx:ASPxMemo ID="uxOrderDescription" runat="server" Height="30px" Width="700px" MaxLength="500">
                                                <ValidationSettings ValidationGroup="groupPacients">
                                                </ValidationSettings>
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                    <div class="panelsdelimiter"></div>
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" DefaultButton="uxAddSputnikButton" ShowCollapseButton="True" ShowDefaultImages="False" Width="100%" HeaderText="Сопровождающие">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server" ClientIDMode="Static">
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel5" ClientInstanceName="addNewSputnikPanel" runat="server" ShowCollapseButton="True" ShowDefaultImages="False" Width="100%" HeaderText="Данные сопровождающих">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent3" runat="server" ClientIDMode="Static">
                                            <strong>После заполнения данных нового сопровождающего - обязательно нажмите "Добавить сопровождающего" ниже. Только в этом случае введённый пациент сопровождающий сохранится в список сопровождающих.</strong>
                                            <table>
                                                <tr>
                                                    <td class="item">17.1.1 Фамилия</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikFamiliya" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.1.2 Имя</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikName" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.1.3 Отчество</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikOtchestvo" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks">
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.2.1 Фамилия на английском языке (с заграничного паспорта)
                                                        <br />
                                                        При отсутствии заграничного паспорта введите фамилию латинскими буквами
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikFamiliyaEn" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.2.2 Имя на английском языке (с заграничного паспорта)
                                                        <br />
                                                        При отсутствии заграничного паспорта введите имя латинскими буквами
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikNameEn" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.3 Серия и номер с заграничного паспорта:
                                                        <br />
                                                        (если загран. паспорт отсутствует, тогда с гражданского паспорта)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikSeriaNumber" NullText="00 2233445566" runat="server" Width="170px" MaxLength="50">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.4 Дата рождения</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxDateEdit ID="uxSputnikBirthDate" NullText="20.02.2000" runat="server" Width="100px">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.5. Родственные отношения к пациенту:</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxComboBox ID="uxSputnikRodstvo" runat="server" DataSourceID="uxRodstvoDataSource" TextField="Name" ValueField="Id" Width="300px" NullText="не выбрано" ValueType="System.Int32">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.6. Ваш e-mail: (укажите e-mail для связи с Вами. Переписка ведется только по e-mail.)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikEmail" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RegularExpression ErrorText="Неверный формат электронного адреса" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.7 Контактный телефон: (укажите основной и дополнительный телефон для связи с Вами)</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxTextBox ID="uxSputnikContact" runat="server" Width="170px" MaxLength="250">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="item">17.8 Гражданство</td>
                                                </tr>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxComboBox NullText="Россия" ID="uxSputnikCountryId" runat="server" DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" Width="300px">
                                                            <ValidationSettings ValidationGroup="groupSputniks" Display="Static">
                                                                <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <strong>Проверьте введённые данные сопровождающего, затем нажмите кнопку ниже.</strong>
                                            <table>
                                                <tr>
                                                    <td class="item">
                                                        <dx:ASPxButton ID="uxAddSputnikButton" runat="server" Text="Добавить сопровождающего" AutoPostBack="False">
                                                            <ClientSideEvents Click="AddSputnik"></ClientSideEvents>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>


                                <div class="panelsdelimiter"></div>

                                <uc:ClientSputniks runat="server" ID="uxClientSputniks" Width="100%" />

                                <%--<p><a href="#" onclick="HideAddSputnikPanel();">Доавить нового сопровождающего в завяку:</a></p>--%>

                                <uc:ResultBox runat="server" ID="uxSendResultBox" />
                                <table>
                                    <tr>
                                        <td class="item">
                                            <dx:ASPxButton ID="uxSave" runat="server" Text="Сохранить заявку" ToolTip="Сохраните изменения Вашей заявки и Вы сможете продолжить заполнять в любое время" OnClick="uxSave_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td class="item">
                                            <dx:ASPxButton ID="uxSend" runat="server" Text="Отправить заявку" ToolTip="После этого заявка будет отправлена в таком виде, как она есть и каждое изменение будет необходимо согласовывать с администратором" OnClick="uxSend_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>

                    <asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
    <div style="height: 50px;"></div>

</asp:Content>

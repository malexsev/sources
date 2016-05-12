<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewOrderWizard.aspx.cs" Inherits="Cure.WebAdmin.Client.NewOrderWizard" %>

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
    <div class="content">
        <a name="top"></a>
        <asp:Wizard ID="uxWizard" runat="server" CellSpacing="5" DisplayCancelButton="False" Width="100%" OnCancelButtonClick="uxWizard_CancelButtonClick" OnFinishButtonClick="uxWizard_FinishButtonClick"
            HeaderStyle-VerticalAlign="top" StepStyle-VerticalAlign="top" OnNextButtonClick="uxWizard_NextButtonClick">
            <StepStyle HorizontalAlign="Left" />
            <SideBarStyle VerticalAlign="Top" />
            <HeaderStyle VerticalAlign="Top"></HeaderStyle>
            <NavigationStyle HorizontalAlign="Left"></NavigationStyle>
            <StartNavigationTemplate>
                <dx:ASPxButton runat="server" ID="uxStartNextButton" Text="Продолжить" CommandName="MoveNext" CausesValidation="True"></dx:ASPxButton>
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <%--<dx:ASPxButton ID="uxStepPreviousButton" runat="server" Text="Шаг назад" CommandName="MovePrevious"></dx:ASPxButton>--%>
                <dx:ASPxButton ID="uxStepNextButton" runat="server" Text="Продолжить" CommandName="MoveNext" CausesValidation="True"></dx:ASPxButton>
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <dx:ASPxButton runat="server" ID="uxFinishButton" Text="Отправить Заявку" CommandName="MoveComplete" CausesValidation="True"></dx:ASPxButton>
            </FinishNavigationTemplate>
            <SideBarTemplate>
                <asp:DataList ID="SideBarList" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" OnClientClick="return false;">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" OnClientClick="return false;">LinkButton</asp:LinkButton>
                    </AlternatingItemTemplate>
                    <SelectedItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" Font-Bold="True" Font-Italic="True"
                            OnClientClick="return false;">LinkButton</asp:LinkButton>
                    </SelectedItemTemplate>
                </asp:DataList>
            </SideBarTemplate>
            <WizardSteps>
                <asp:WizardStep ID="stepGeneral" runat="server" StepType="Start" Title="Выбор клиники и времени лечения.">
                    <p>
                        Для бронирования лечения необходимо выбрать название больницы и планируемые даты лечения.
                        <br />
                        В дальнейшем даты лечения можно откорректировать.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Выбор реабилитационного отделения.</td>
                            <td class="item">
                                <dx:ASPxComboBox ID="uxOrderDepartment" runat="server" DataSourceID="uxDepartmentDataSource" TextField="Name" ValueField="Id" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <%--<tr>--%>
           <%--                 <td>
                                <table style="width: 500px">
                                    <tr>--%>
                                        <td class="item">Дата заезда</td>
                                        <td class="item">
                                            <dx:ASPxDateEdit ID="uxOrderDateFrom" runat="server" Height="17px" Width="100px">
                                                <ValidationSettings Display="Static">
                                                    <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
<%--                                    </tr>
                                </table>
                            </td>
                            <td>--%>
                                <%--<table style="width: 500px">--%>
                                    <tr>
                                        <td class="item">Дата отъезда</td>
                                        <td class="item">
                                            <dx:ASPxDateEdit ID="uxOrderDateTo" runat="server" Width="100px">
                                                <ValidationSettings Display="Static">
                                                    <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                   <%--             </table>
                            </td>
                        </tr>--%>
<%--                        <tr>
                            <td class="item">Дополнительная информация по заявке</td>
                            <td class="item">
                                <dx:ASPxMemo ID="uxOrderDescription" runat="server" Height="30px" Width="500px" MaxLength="500">
                                    <ValidationSettings>
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </td>
                        </tr>--%>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="stepPacientMedinfo" runat="server" StepType="Step" Title="Пациент - медицинсткие данные">

                    <p>
                        Что бы доктора больницы смогли определить может ли пациент проходить реабилитацию и лечение в нашей больнице, просим Вас ответить на важные вопросы.
                        <br />
                        Все поля не являются обязательными для заполнения.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Диагноз пациента из последних выписок</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitTodaysDiagnoz" runat="server" Width="700px" MaxLength="2500">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">История болезни. Опишите подробно из медицинских документов: беременность, роды, вес, АПГАР, ИВЛ,<br />
                                реанимация, обследование, лечение. Когда заметили у ребенка проблемы, сопутствующие заболевания.</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitHystoryAm" runat="server" Width="700px" Rows="10">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Продолжительность заболевания</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitHystoryB" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Этапы физического развития с момента рождения<br />
                                (Когда начал держать голову, сидеть, ползать, стоять, ходить, и т д.?)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitRazvitie" runat="server" Width="700px" Rows="5">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Находится ли ребенок на психиатрическом или наркологическом диспансерном учете? С каким диагнозом?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitDispanser" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Находится ли ребенок на кожно-венерологическом диспансерном учете? С каким диагнозом?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitDispanserB" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Имеет ли опасные для здоровья заболевания?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitDangerousDiseases" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Сердечно – сосудистые заболевания?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitSerdce" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Заболевания дыхательной системы?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitDihalka" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Инфекционные заболевания?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitInfections" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Другие опасные для здоровья и окружающих заболевания?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitOtherDiseases" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">2.6.5. Заключение электроэнцефалограммы</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitEncefalogramm" runat="server" Width="700px">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="stepPacientIsEpilepsy" runat="server" StepType="Step" Title="Пациент - имеются-ли признаки эпилепсии?">

                    <p>
                        <%--В случае отсутствия данных признаков - вся информация на этот счёт будет пропущена.--%>
                    </p>
                    <table>
                        <tr>
                            <td class="item">Имеются-ли признаки эпилепсии, судороги?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxRadioButtonList ID="uxIsEpilepcy" runat="server" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="Не имеются" Value="0" Selected="True"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Имеются" Value="1" Selected="False"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="stepPacientEpilepsy" runat="server" StepType="Step" Title="Пациент - эпилепсия, судороги">

                    <p>
                        Эта информация необходима для корректирования лечения по симптомам эпилепсии.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Эпилепсия (Судороги) </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitEpilispiya" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Тип судорог (На фоне чего начинаются судороги, какая продолжительность.<br />
                                Опишите эпи-приступы и подробно как они протекают.)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitSudorogiTypem" runat="server" Width="700px">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Количество эпилептических приступов (За один день или месяц)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitSudorogiCount" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Противосудорожные препараты (Наименование и дозировка)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitSudorogiMedcine" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Ремиссия (Когда был последний приступ)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitRemission" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="stepPacientNaviki" runat="server" StepType="Step" Title="Пациент - состояние и навыки">

                    <p>
                        Данные вопросы помогут докторам точнее сформировать мнение о состоянии пациента. 
                        <br />
                        Все поля являются не обязательными для заполнения.
                    </p>
                    <table>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitRazgovor" NullText="Разговаривает? Словарный запас?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitInstructcii" NullText="Инструкции выполняет?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitFisical" NullText="Физические навыки? (Что умеет ребенок делать сам и с помощью)" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitDiet" NullText="Опишите особенности питания ребенка? (Сам ест, перетертая пища или нет)" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitEating" NullText="Как жует и глотает?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitEatingProblems" NullText="Пищеварительные проблемы?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitAppetit" NullText="Аппетит?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitStul" NullText="Стул?" runat="server" Width="700px" MaxLength="50">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitAlergiya" NullText="Аллергия на лекарственные препараты?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitImunitet" NullText="Иммунитет к простудными заболеваниями?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitFiznagruzki" NullText="Как переносит физические нагрузки?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitSon" NullText="Состояние и качество сна?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitProstupUp" NullText="Увеличиваются приступы при физических нагрузках?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitZakativaetsa" NullText="Задыхается или закатывается во время болезненных процедур?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="stepPacientAdditional" runat="server" StepType="Step" Title="Пациент - краткая информация по истории лечения">

                    <p>
                        Данная информация позволит докторам учесть пройдённое ранее лечение.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Какое ранее проводилось лечение по Вашему заболеванию и его результаты (где)?<br />
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitKursesRanee" NullText="Где?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Проходили ли когда-либо лечение в Китае (где)?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitKursesChinaRanee" NullText="Где?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Лечились ли когда-либо методами китайской традиционной медицины за пределами Китая?<br />
                                (Иглорефлексотерапия, китайский массаж, лечение китайскими лекарственными препаратами.<br />
                                Где, результаты)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitNonTradicial" NullText="Где, результаты?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Проводились ли хирургические операции по Вашему заболеванию? (Название, дата, результат, рецидив)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitHirurg" NullText="Название, дата, результат, рецидив" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Получали ли травмы от внешних факторов? (ДТП, несчастные случаи)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitTravmi" NullText="Опишите подробно?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <%--<asp:WizardStep ID="stepPacientRequirements" runat="server" StepType="Step" Title="Пациент - пожелания по лечению">

                    <p>
                        Более подробная информация позволит подготовиться докторам к лечению пациента для повышения Вашей оценки лечения.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Особые пожелания по проживанию, лечению, реабилитации.</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitRequirements" NullText="Опишите подробно?" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Какие необходимы документы от клиники. (Приглашение, cчет на лечение. )</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitRequiredDocs" NullText="Приглашение, Счет на лечение" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Дополнительная информация о  пациенте (заполняется по желанию родителей\законных представителей).<br />
                                Какую еще информацию, Вы можете добавить о пациенте касающуюся предстоящего лечения</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxMemo ID="uxVisitAdditional" Rows="3" runat="server" Height="50px" Width="700px" MaxLength="2500">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Цель поездки? (Какие Вы хотите получить реальные результаты от реабилитации)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxVisitMainGoal" runat="server" Width="700px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:WizardStep>--%>
                <asp:WizardStep ID="stepPacientInfo" runat="server" StepType="Step" Title="Пациент - паспортные данные">

                    <p>
                        Просим Вас заполнить паспортные данные (данные свидетельства о рождении) пациента для подготовки Вам:
                        <br />
                        Приглашение, Счет на лечение, Договор оказания медицинских услуг, Акт выполненных работ, различного вида справок.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Фамилия</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Иванов" ID="uxPacientFamiliya" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Имя</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Иван" ID="uxPacientName" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Отчество</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Иванович" ID="uxPacientOtchestvo" runat="server" Width="170px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Фамилия на английском языке (с заграничного паспорта)<br />
                                При отсутствии заграничного паспорта введите фамилию латинскими буквами
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Ivavon" ID="uxPacientFamiliyaEn" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Имя на английском языке (с заграничного паспорта)
                                <br />
                                При отсутствии заграничного паспорта введите имя латинскими буквами
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Ivan" ID="uxPacientNameEng" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Серия и номер заграничного паспорта
                                                        <br />
                                При отсутствии заграничного паспорта введите серию и номер свидетельства о рождении</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="0000-00000" ID="uxPacientSerialNumber" runat="server" Width="100px" MaxLength="250">
                                    <CaptionSettings></CaptionSettings>
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Дата рождения</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxComboBox NullText="25" ID="uxPacientBirthDateDay" runat="server" Width="40px">
                                                <ValidationSettings Display="Static">
                                                    <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                </ValidationSettings>
                                                <Items>
                                                    <dx:ListEditItem Text="1" Value="1" Selected="true" />
                                                    <dx:ListEditItem Text="2" Value="2" />
                                                    <dx:ListEditItem Text="3" Value="3" />
                                                    <dx:ListEditItem Text="4" Value="4" />
                                                    <dx:ListEditItem Text="5" Value="5" />
                                                    <dx:ListEditItem Text="6" Value="6" />
                                                    <dx:ListEditItem Text="7" Value="7" />
                                                    <dx:ListEditItem Text="8" Value="8" />
                                                    <dx:ListEditItem Text="9" Value="9" />
                                                    <dx:ListEditItem Text="10" Value="10" />
                                                    <dx:ListEditItem Text="11" Value="11" />
                                                    <dx:ListEditItem Text="12" Value="12" />
                                                    <dx:ListEditItem Text="13" Value="13" />
                                                    <dx:ListEditItem Text="14" Value="14" />
                                                    <dx:ListEditItem Text="15" Value="15" />
                                                    <dx:ListEditItem Text="16" Value="16" />
                                                    <dx:ListEditItem Text="17" Value="17" />
                                                    <dx:ListEditItem Text="18" Value="18" />
                                                    <dx:ListEditItem Text="19" Value="19" />
                                                    <dx:ListEditItem Text="20" Value="20" />
                                                    <dx:ListEditItem Text="21" Value="21" />
                                                    <dx:ListEditItem Text="22" Value="22" />
                                                    <dx:ListEditItem Text="23" Value="23" />
                                                    <dx:ListEditItem Text="24" Value="24" />
                                                    <dx:ListEditItem Text="25" Value="25" />
                                                    <dx:ListEditItem Text="26" Value="26" />
                                                    <dx:ListEditItem Text="27" Value="27" />
                                                    <dx:ListEditItem Text="28" Value="28" />
                                                    <dx:ListEditItem Text="29" Value="29" />
                                                    <dx:ListEditItem Text="30" Value="30" />
                                                    <dx:ListEditItem Text="31" Value="31" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox NullText="07" ID="uxPacientBirthDateMonth" runat="server" Width="40px">
                                                <ValidationSettings Display="Static">
                                                    <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                </ValidationSettings>
                                                <Items>
                                                    <dx:ListEditItem Text="1" Value="1" Selected="true" />
                                                    <dx:ListEditItem Text="2" Value="2" />
                                                    <dx:ListEditItem Text="3" Value="3" />
                                                    <dx:ListEditItem Text="4" Value="4" />
                                                    <dx:ListEditItem Text="5" Value="5" />
                                                    <dx:ListEditItem Text="6" Value="6" />
                                                    <dx:ListEditItem Text="7" Value="7" />
                                                    <dx:ListEditItem Text="8" Value="8" />
                                                    <dx:ListEditItem Text="9" Value="9" />
                                                    <dx:ListEditItem Text="10" Value="10" />
                                                    <dx:ListEditItem Text="11" Value="11" />
                                                    <dx:ListEditItem Text="12" Value="12" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox NullText="1962" ID="uxPacientBirthDateYear" runat="server" Width="60px">
                                                <ValidationSettings Display="Static">
                                                    <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                                </ValidationSettings>
                                                <Items>
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>

                                <%--<dx:ASPxDateEdit NullText="12.07.1962" ID="uxPacientBirthDate" runat="server" Width="100px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Гражданство</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxComboBox NullText="Россия" ID="uxPacientCountryId" runat="server" DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" Width="300px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Область, город постоянного места проживания</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox NullText="Москва" ID="uxPacientCityName" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Хотите добавить в заявку на лечение ещё одного пациента?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxRadioButtonList ID="uxIsAddNewPacient" runat="server" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="Нет, на лечение не планируется больше пациентов" Value="0" Selected="True"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Да, хочу добавить ещё одного пациента" Value="1" Selected="False"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <%--                <asp:WizardStep ID="stepPacientFinish" runat="server" StepType="Step" Title="Пациент - завершение">

                    <p>Вы заполнили всю необходимую информацию по данному пациенту.</p>

                    <table>
                        <tr>
                            <td class="item">Хотите добавить в заявку на лечение ещё одного пациента?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxRadioButtonList ID="uxIsAddNewPacient" runat="server" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="Нет, на лечение не планируется больше пациентов" Value="0" Selected="True"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Да, хочу добавить ещё одного пациента" Value="1" Selected="False"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>--%>
                <asp:WizardStep ID="stepSputnikInfo" runat="server" StepType="Step" Title="Сопровождающие - паспортные данные">
                    <script type="text/javascript">
                        document.location.href = "#top";
                    </script>
                    <p>
                        Эти данные необходимы для подготовки документов для сопровождающих лечение.
                    </p>
                    <table>
                        <tr>
                            <td class="item">Фамилия</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikFamiliya" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Имя</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikName" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Отчество</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikOtchestvo" runat="server" Width="170px" MaxLength="250">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Фамилия на английском языке (с заграничного паспорта)
                                                        <br />
                                При отсутствии заграничного паспорта введите фамилию латинскими буквами
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikFamiliyaEn" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Имя на английском языке (с заграничного паспорта)
                                                        <br />
                                При отсутствии заграничного паспорта введите имя латинскими буквами
                            </td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikNameEn" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Серия и номер с заграничного паспорта:
                                                        <br />
                                (если загран. паспорт отсутствует, тогда с гражданского паспорта)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikSeriaNumber" NullText="00 2233445566" runat="server" Width="170px" MaxLength="50">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Дата рождения</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxDateEdit ID="uxSputnikBirthDate" NullText="20.02.2000" runat="server" Width="100px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField ErrorText="Обязательное поле" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Родственные отношения к пациенту:</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxComboBox ID="uxSputnikRodstvo" runat="server" DataSourceID="uxRodstvoDataSource" TextField="Name" ValueField="Id" Width="300px" NullText="не выбрано" ValueType="System.Int32">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Адрес электронной почты: (укажите e-mail для связи с сопровождающим.)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikEmail" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RegularExpression ErrorText="Неверный формат электронного адреса" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Контактный телефон: (укажите основной и дополнительный телефон для связи с Вами)</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxTextBox ID="uxSputnikContact" runat="server" Width="170px" MaxLength="250">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Гражданство</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxComboBox NullText="Россия" ID="uxSputnikCountryId" runat="server" DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" Width="300px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="item">Хотите добавить в завяку на лечение ещё одного сопровождающего?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxRadioButtonList ID="uxIsAddNewSputnik" runat="server" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="Нет, сопровождающих больше не будет" Value="0" Selected="True"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Да, хочу добавить ещё одного сопровождающего" Value="1" Selected="False"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>
                <%--                <asp:WizardStep ID="stepSputnikFinish" runat="server" StepType="Step" Title="Сопровождающие - завершение">

                    <p>Вы заполнили всю необходимую информацию по данному сопровождающему.</p>

                    <table>
                        <tr>
                            <td class="item">Хотите добавить в завяку на лечение ещё одного сопровождающего?</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <dx:ASPxRadioButtonList ID="uxIsAddNewSputnik" runat="server" Width="500px">
                                    <ValidationSettings Display="Static">
                                        <RequiredField IsRequired="True" ErrorText="Обязательное поле" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="Нет, сопровождающих больше не будет" Value="0" Selected="True"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Да, хочу добавить ещё одного сопровождающего" Value="1" Selected="False"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>--%>
                <asp:WizardStep ID="stepFinish" runat="server" StepType="Finish" Title="Отправка заявки">

                    <p>
                        Вы успешно заполнили Заявку на лечение!
                        <br />
                        <br />
                        Ваша Заявка будет рассмотрена в течении 2-х рабочих дней.
                        <br />
                        Если Вы считаете, что у Вас есть дополнения по Заявке просим Вас написать об этом на электронную почту <a href="mailto:zqcpchina@gmail.com">zqcpchina@gmail.com</a>. Администратор внесет необходимые изменения
                        <br />
                        Для отправки Заявки на рассмотрение - нажмите Отправить.

                    </p>

                </asp:WizardStep>

            </WizardSteps>
        </asp:Wizard>

        <asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
    </div>

</asp:Content>

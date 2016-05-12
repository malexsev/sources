<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step4Steady.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.Step4Steady" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="~/Client/Controls/ClientCurrOrder.ascx" TagPrefix="uc" TagName="ClientCurrOrder" %>
<%@ Register Src="~/Client/Controls/StepAditionalInfo.ascx" TagPrefix="uc" TagName="StepAditionalInfo" %>

<p>
    Ваша Заявка ОДОБРЕНА.
</p>

<uc:ClientCurrOrder runat="server" ID="uxClientCurrOrder" Width="100%" />

<br />
<p>
    Порядок Ваших дальнейших действий:
</p>
<ul>
    <li>1.	Преобритение билетов до <%=clientContainer.CurrentOrder.Department.Address %>
        <br />помощь в выборе билетов пройдите по <a href="http://www.dcp-china.ru/informatciya/kak-dobratsya-do-goroda-yunchen">ссылке на сайт</a>
        <br />
        <br />
        После покупки билетов внесите данные с билетов:
        <br>
        Прибытие (обязательно):
        <table>
            <tr>
                <td class="item">Рейс №</td>
                <td>
                    <dx:ASPxTextBox ID="uxPribitieInfo" runat="server" Width="170px">
                        <ValidationSettings>
                            <RequiredField ErrorText="Обязательно" IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="item">Дата, время</td>
                <td>
                    <dx:ASPxDateEdit ID="uxPribitieTime" runat="server" EditFormat="DateTime">
                        <ValidationSettings>
                            <RequiredField ErrorText="Обязательно" IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
            </tr>
        </table>
        <br>
        Убытие (необязательно, можно указать позже):
        <table>
            <tr>
                <td class="item">Рейс №</td>
                <td>
                    <dx:ASPxTextBox ID="uxUbytieInfo" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="item">Дата, время</td>
                <td>
                    <dx:ASPxDateEdit ID="uxUbytieTime" runat="server" EditFormat="DateTime"></dx:ASPxDateEdit>
                </td>
            </tr>
        </table>
        <br>
    </li>
    <li>2.	Оформление Визы - <a href="http://www.dcp-china.ru/informatciya/oformlenie-vizy">подробнее</a>
        <br />
        На какое количество дней Вам выдана виза (введите число дней, необязательно):
        <div class="panelsdelimiter"></div>
        <dx:ASPxSpinEdit ID="uxVisaDney" runat="server" Number="0" MinValue="0" MaxValue="5000" NullText="Виза не получена" Width="60px">
        </dx:ASPxSpinEdit>
        <br>
    </li>
    <li>3.	Настоящее время убытия (возможно изменить в будущем)
        <br />
        <dx:ASPxDateEdit ID="uxDateTo" runat="server" EditFormat="Date" Width="90px"></dx:ASPxDateEdit>
    </li>
</ul>
<div class="panelsdelimiter"></div>
<dx:ASPxButton ID="uxSave" runat="server" Text="Сохранить данные и перейти на следующий этап" OnClick="uxSave_Click"></dx:ASPxButton>

<div class="panelsdelimiter"></div>
<uc:StepAditionalInfo runat="server" ID="uxStepAditionalInfo" Width="100%" />

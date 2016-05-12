<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step5Go.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.Step5Go" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Src="~/Client/Controls/ClientCurrOrder.ascx" TagPrefix="uc" TagName="ClientCurrOrder" %>
<%@ Register Src="~/Client/Controls/StepAditionalInfo.ascx" TagPrefix="uc" TagName="StepAditionalInfo" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagPrefix="uc" TagName="ResultBox" %>

<p>
    Ваша Заявка ОДОБРЕНА, ожидание Вашего прибытия.
</p>

<uc:ClientCurrOrder runat="server" ID="uxClientCurrOrder" Width="100%" VisibleOfVisitGrid="false" VisibleOfSputnikGrid="false" />
<br />
<dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="200px" HeaderText="Вы имеете возможность уточнить время убытия">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxDateEdit ID="uxDateTo" runat="server" EditFormat="Date" Width="90px"></dx:ASPxDateEdit>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
<p>
    Для комфортного путешествия и встречи выбирайте услуги:
</p>

<p>Какая помощь Вам нужна в Пекине:</p>
<div class="panelsdelimiter"></div>
<dx:ASPxCheckBoxList ID="uxServicesPekin" runat="server">
    <Items>
        <dx:ListEditItem Text="Услуги переводчика" Value="Услуги переводчика" />
        <dx:ListEditItem Text="Забронировать Отель " Value="Забронировать Отель" />
    </Items>
</dx:ASPxCheckBoxList>
<div class="panelsdelimiter"></div>
Другие услуги:
<div class="panelsdelimiter"></div>
<dx:ASPxMemo ID="uxServicesPekinOther" runat="server" Height="50px" Width="500px"></dx:ASPxMemo>
<div class="panelsdelimiter"></div>

<p>Какая нужна помощь при встрече в городе Юньчэн:</p>
<dx:ASPxCheckBoxList ID="uxServicesUnchenVstrecha" runat="server">
    <Items>
        <dx:ListEditItem Text="Встреча" Value="Встреча" />
    </Items>
</dx:ASPxCheckBoxList>
<div class="panelsdelimiter"></div>
Другие услуги:
<div class="panelsdelimiter"></div>
<dx:ASPxMemo ID="uxServicesUnchenOther" runat="server" Height="50px" Width="500px"></dx:ASPxMemo>
<div class="panelsdelimiter"></div>

<p>Услуги в месте проживания. Опишите что Вам необходимо в комнате по приезду:</p>
<dx:ASPxCheckBoxList ID="uxServicesUnchenRoom" runat="server">
    <Items>
        <dx:ListEditItem Text="Туалетная Бумага" Value="Туалетная Бумага" />
        <dx:ListEditItem Text="Стиральный порошок" Value="Стиральный порошок" />
        <dx:ListEditItem Text="Ополаскиватель для белья" Value="Ополаскиватель для белья" />
        <dx:ListEditItem Text="Жидкое мыло" Value="Жидкое мыло" />
        <dx:ListEditItem Text="Питьевая вода" Value="Питьевая вода" />
        <dx:ListEditItem Text="Средство для мытья посуды" Value="Средство для мытья посуды" />
    </Items>
</dx:ASPxCheckBoxList>
<br />
<div class="panelsdelimiter"></div>
<table>
    <tr>
        <td class="item">
            <dx:ASPxButton ID="uxSave" runat="server" Text="Сохранить данные" OnClick="uxSave_Click"></dx:ASPxButton>
        </td>
        <td class="item">
            <uc:ResultBox runat="server" ID="uxResultBox" />
        </td>
    </tr>
</table>


<div class="panelsdelimiter"></div>
<uc:StepAditionalInfo runat="server" ID="uxStepAditionalInfo" Width="100%" />

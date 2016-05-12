<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step6Process.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.Step6Process" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Src="~/Client/Controls/ClientCurrOrder.ascx" TagPrefix="uc" TagName="ClientCurrOrder" %>
<%@ Register Src="~/Client/Controls/StepAditionalInfo.ascx" TagPrefix="uc" TagName="StepAditionalInfo" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagPrefix="uc" TagName="ResultBox" %>

<p>
    Производится Заявка:
</p>

<uc:ClientCurrOrder runat="server" ID="uxClientCurrOrder" Width="100%" />
<br />
<dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="200px" HeaderText="Вы имеете возможность уточнить время убытия">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxDateEdit ID="uxDateTo" runat="server" EditFormat="Date" Width="90px"></dx:ASPxDateEdit>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
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

<br />
<div class="panelsdelimiter"></div>
<uc:StepAditionalInfo runat="server" ID="uxStepAditionalInfo" Width="100%" />

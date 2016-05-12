<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step7Finish.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.Step7Finish" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="~/Client/Controls/ClientCurrOrder.ascx" TagPrefix="uc" TagName="ClientCurrOrder" %>
<%@ Register Src="~/Client/Controls/StepAditionalInfo.ascx" TagPrefix="uc" TagName="StepAditionalInfo" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagPrefix="uc" TagName="ResultBox" %>

<p>
    Статус текущей заявки: ЗАВЕРШЕНА
</p>

<uc:ClientCurrOrder runat="server" ID="uxClientCurrOrder" Width="100%" />

<br />
<p>
    Подать новую заявку можно на освное данных последней заявки, для этого на странице <a href="../Client/NewOrder.aspx">"Новая заявка"</a> выберите - "Заполнить из моей предыдущей заявки", и все данные скопируются.
</p>

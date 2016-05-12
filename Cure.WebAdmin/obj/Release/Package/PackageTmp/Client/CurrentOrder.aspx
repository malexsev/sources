<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="CurrentOrder.aspx.cs" Inherits="Cure.WebAdmin.Client.CurrentOrder" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <div class="panelsdelimiter"></div>
        <dx:ASPxTrackBar ID="uxTrackBar" runat="server" ScalePosition="Both" ShowChangeButtons="False" Width="100%" ReadOnly="true" LargeTickEndValue="2" MaxValue="2" Step="1">
            <Items>
                <dx:TrackBarItem Text="Заявка ещё не отправлялась" Value="0" />
                <dx:TrackBarItem Text="Заявка отправлена" Value="1" />
                <dx:TrackBarItem Text="Заявка принята на рассмотрение" Value="2" />
                <dx:TrackBarItem Text="Заявка одобрена" Value="3" />
                <dx:TrackBarItem Text="Закуплены билеты" Value="4" />
                <%--<dx:TrackBarItem Text="Прибытие - отзыв о прибытии" Value="5" />--%>
                <dx:TrackBarItem Text="Прохождение лечения" Value="6" />
                <%--<dx:TrackBarItem Text="Завершение лечения - отзыв о лечении" Value="7" />--%>
                <dx:TrackBarItem Text="Заявка выполнена" Value="8" />
            </Items>
        </dx:ASPxTrackBar>
        <div class="panelsdelimiter"></div>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowCollapseButton="true" Width="100%" HeaderText="Рабочая информация на данном этапе:">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server" ClientIDMode="Static">
                    <asp:PlaceHolder runat="server" ID="uxStepPlaceHolder"></asp:PlaceHolder>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
</asp:Content>

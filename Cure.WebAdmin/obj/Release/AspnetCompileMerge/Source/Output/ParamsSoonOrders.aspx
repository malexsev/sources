<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="ParamsSoonOrders.aspx.cs" Inherits="Cure.WebAdmin.Reports.ParamsSoonOrders" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        
        <dx:ASPxFormLayout ID="uxFormLayout" runat="server" DataSourceID="uxVisitDataSource">
            <Items>
                <dx:LayoutItem Caption="Количество дней" Name="uxSoonDays">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
        <asp:ObjectDataSource ID="uxVisitDataSource" runat="server"></asp:ObjectDataSource>
        <dx:ASPxButton ID="uxSubmit" runat="server" Text="Сгенерировать">
        </dx:ASPxButton>
    </div>

</asp:Content>

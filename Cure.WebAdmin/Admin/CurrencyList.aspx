<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="CurrencyList.aspx.cs" Inherits="Cure.WebAdmin.Admin.CurrencyList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Name">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" Width="60px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Название" Width="150px">
                    <PropertiesTextEdit MaxLength="50">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Caption="Описание">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Currency" DeleteMethod="DeleteCurrency" InsertMethod="InsertCurrency" SelectMethod="GetCurrencies" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateCurrency">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
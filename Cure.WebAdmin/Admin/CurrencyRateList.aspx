<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="CurrencyRateList.aspx.cs" Inherits="Cure.WebAdmin.Admin.CurrencyRateList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="uc" TagName="ResultBox" Src="~/Controls/ResultBox.ascx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <table>
            <tr>
                <td>
                    <div class="contentblock">
                        <dx:ASPxButton ID="uxGetRates" runat="server" Text="Получить котировки ЦБ РФ" OnClick="uxGetRates_Click"></dx:ASPxButton>
                    </div>
                </td>
                <td>
                    <uc:ResultBox runat="server" ID="uxResultBox" />
                </td>
            </tr>
        </table>

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" Width="60px" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataDateColumn Caption="Дата" FieldName="Date" VisibleIndex="2" Width="80px">
                    <PropertiesDateEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataSpinEditColumn Caption="Курс" FieldName="Rate" VisibleIndex="7">
                    <PropertiesSpinEdit DisplayFormatString="g">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataComboBoxColumn Caption="Из валюты" FieldName="CurrencyFrom" VisibleIndex="4" Width="60px">
                    <PropertiesComboBox DataSourceID="uxCurrencyDataSource" TextField="Name" ValueField="Name">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="В валюту" FieldName="CurrencyTo" VisibleIndex="6" Width="60px">
                    <PropertiesComboBox DataSourceID="uxCurrencyDataSource" TextField="Name" ValueField="Name">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsEditing EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.CurrencyRate" DeleteMethod="DeleteCurrencyRate" InsertMethod="InsertCurrencyRate" SelectMethod="GetCurrencyRates" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateCurrencyRate"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCurrencyDataSource" runat="server" SelectMethod="GetCurrencies" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

    </div>

</asp:Content>

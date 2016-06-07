<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="RefBankList.aspx.cs" Inherits="Cure.WebAdmin.Admin.RefBankList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Caption="Описание">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Inn" VisibleIndex="4" Caption="ИНН">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Kpp" VisibleIndex="5" Caption="КПП">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Oktmo" VisibleIndex="6" Caption="ОКТМО">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ОКВЕД" FieldName="Okved" VisibleIndex="7">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="БИК" FieldName="Bik" VisibleIndex="8">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Корр. Счёт" FieldName="KorrAccount" VisibleIndex="9">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Наименование" FieldName="Name" VisibleIndex="1" Width="150px">
                    <PropertiesTextEdit MaxLength="50">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="3" Width="150px">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.RefBank" DeleteMethod="DeleteRefBank" InsertMethod="InsertRefBank" SelectMethod="GetRefBanks" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateRefBank" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
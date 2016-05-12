<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="RefCountryList.aspx.cs" Inherits="Cure.WebAdmin.Admin.RefCountryList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Имя" Width="250px">
                    <PropertiesTextEdit MaxLength="50">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NameCh" VisibleIndex="3" Width="250px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NameEn" VisibleIndex="4" Width="250px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Национальность Ch (Отчёты)" FieldName="NacionalnostChLabel" Visible="False" VisibleIndex="6">
                    <PropertiesTextEdit MaxLength="150">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Посольство Ch (отчёты)" FieldName="PosolstvoChLabel" Visible="False" VisibleIndex="7">
                    <PropertiesTextEdit MaxLength="150">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Посольство (отчёты)" FieldName="PosolstvoRusLabel" Visible="False" VisibleIndex="8">
                    <PropertiesTextEdit MaxLength="150">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.RefCountry" DeleteMethod="DeleteRefCountry" InsertMethod="InsertRefCountry" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateRefCountry">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
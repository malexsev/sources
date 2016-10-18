<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewsletterList.aspx.cs" Inherits="Cure.WebAdmin.Admin.NewsletterList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnHtmlRowPrepared="uxMainGrid_HtmlRowPrepared">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Email" Caption="Электронная почта" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Дата добавления" FieldName="EntryDate" VisibleIndex="3">
                    <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy hh:mm:ss" EditFormat="DateTime" EditFormatString="dd-MM-yyyy hh:mm:ss">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="EntryType" VisibleIndex="4" Caption="Тип добавления">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Settings" VisibleIndex="5" Caption="Настройки">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Успешно" FieldName="SuccessCount" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ошибок" FieldName="ErrorsCount" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Newsletter" DeleteMethod="DeleteNewsletter" InsertMethod="InsertNewsletter" SelectMethod="GetNewsletters" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateNewsletter"></asp:ObjectDataSource>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="SmsLogList.aspx.cs" Inherits="Cure.WebAdmin.Admin.SmsLogList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnHtmlRowPrepared="uxMainGrid_HtmlRowPrepared">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="False">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="PhoneNumber" Width="150px" Caption="Телефонный номер" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Время" FieldName="Date" VisibleIndex="2" Width="150px">
                    <PropertiesDateEdit DisplayFormatString="" EditFormat="DateTime">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Reason" VisibleIndex="4" Caption="Цель" Width="150px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Text" VisibleIndex="5" Caption="Сообщение">
                        <DataItemTemplate>
                            <%# Eval("Text").ToString() %>
                        </DataItemTemplate>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.SmsLog" DeleteMethod="DeleteSmsLog" InsertMethod="InsertSmsLog" SelectMethod="GetSmsLogs" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateSmsLog"></asp:ObjectDataSource>

</asp:Content>

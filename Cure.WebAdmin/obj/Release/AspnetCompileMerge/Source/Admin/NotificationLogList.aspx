<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NotificationLogList.aspx.cs" Inherits="Cure.WebAdmin.Admin.NotificationLogList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowUpdating="uxMainGrid_RowUpdating" OnRowInserting="uxMainGrid_RowInserting">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Type" VisibleIndex="2" Caption="Тип" Width="60px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Result" VisibleIndex="3" Caption="Результат">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Наименование" FieldName="Name" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Описание" FieldName="Description" VisibleIndex="5">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="ExecutionDate" VisibleIndex="6" Caption="Время" Width="120px">
                    <PropertiesDateEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="Custom">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Details" VisibleIndex="7" Caption="Детали">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Получатель" FieldName="ClientName" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Контакт" FieldName="Contacts" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.NotificationLog" DeleteMethod="DeleteNotificationLog" InsertMethod="InsertNotificationLog" SelectMethod="GetNotificationLogs" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateNotificationLog">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
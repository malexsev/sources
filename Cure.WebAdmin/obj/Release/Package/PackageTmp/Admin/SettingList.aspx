<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="SettingList.aspx.cs" Inherits="Cure.WebAdmin.Admin.SettingList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowUpdating="uxMainGrid_RowUpdating" OnRowInserting="uxMainGrid_RowInserting">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Code" VisibleIndex="1" Caption="Код" ToolTip="100px">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Наименование">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Описание" FieldName="Description" VisibleIndex="3">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Тип" FieldName="Type" VisibleIndex="4">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Чек" FieldName="ValueBool" VisibleIndex="5" Width="30px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataMemoColumn Caption="Значение" FieldName="Value" MinWidth="600" Visible="False" VisibleIndex="6">
                    <PropertiesMemoEdit Rows="20">
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" Visible="True" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Setting" DeleteMethod="DeleteSetting" InsertMethod="InsertSetting" SelectMethod="GetSettings" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateSetting">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
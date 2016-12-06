<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="RefMacsList.aspx.cs" Inherits="Cure.WebAdmin.Admin.RefMacsList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataMemoColumn FieldName="Description" Caption="Описание" VisibleIndex="4">
                    <PropertiesMemoEdit MaxLength="250" Rows="4">
                    </PropertiesMemoEdit>
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn Caption="Уровень #" FieldName="Name" VisibleIndex="3">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn FieldName="DescriptionEn" VisibleIndex="6">
                    <PropertiesMemoEdit MaxLength="250" Rows="4">
                    </PropertiesMemoEdit>
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn FieldName="DescriptionCh" VisibleIndex="7">
                    <PropertiesMemoEdit MaxLength="250" Rows="4">
                    </PropertiesMemoEdit>
                </dx:GridViewDataMemoColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.RefMacsLevel" DeleteMethod="DeleteRefMacsLevel" InsertMethod="InsertRefMacsLevel" SelectMethod="GetRefMacsLevels" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateRefMacsLevel">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
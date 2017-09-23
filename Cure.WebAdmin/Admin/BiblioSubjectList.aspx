<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="BiblioSubjectList.aspx.cs" Inherits="Cure.WebAdmin.Admin.BiblioSubjectList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Width="90%" Caption="Наименование">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Sort" VisibleIndex="3" Caption="Сортировка">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.BiblioSubject" DeleteMethod="DeleteBiblioSubject" InsertMethod="InsertBiblioSubject" SelectMethod="GetBiblioSubjects" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateBiblioSubject" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
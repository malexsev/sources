<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="MensionList.aspx.cs" Inherits="Cure.WebAdmin.Admin.MensionList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowInserting="uxMainGrid_RowInserting" OnRowUpdating="uxMainGrid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowDeleteButton="True" ShowNewButtonInHeader="True" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="OwnerUser" VisibleIndex="4" Caption="Пользователь" Width="120px">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Дата создания" FieldName="CreatedDate" VisibleIndex="5" Width="70px">
                    <PropertiesDateEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CopySubject" VisibleIndex="8" Caption="Копия темы" Visible="False">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя пользователя" FieldName="CopyUserName" VisibleIndex="9" Width="100px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Размещение пользователя" FieldName="CopyUserLocation" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SortOrder" VisibleIndex="11" Caption="Рейтинг" ToolTip="Значение от -100 до 100 определяющее положительность отзыва. Влияет на сортировку." Width="60px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Активный" FieldName="IsActive" VisibleIndex="1" Width="50px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataComboBoxColumn Caption="Тема" FieldName="DepartmentId" VisibleIndex="3">
                    <PropertiesComboBox DataSourceID="uxDepartmentDataSource" NullText="Работа сервиса, организация лечения" TextField="Name" ValueField="Id" ValueType="System.Int32" NullDisplayText="Работа сервиса, организация лечения">
                    </PropertiesComboBox>
                    <Settings AllowHeaderFilter="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Содержание" FieldName="Text" Visible="False" VisibleIndex="7">
                    <PropertiesMemoEdit Rows="20">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" ColumnSpan="2" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Mension" DeleteMethod="DeleteMension" InsertMethod="InsertMension" SelectMethod="GetMensions" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateMension" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepSubject" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
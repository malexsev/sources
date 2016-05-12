<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="DepartmentList.aspx.cs" Inherits="Cure.WebAdmin.Admin.DepartmentList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowUpdating="uxMainGrid_RowUpdating" OnRowInserting="uxMainGrid_RowInserting">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" Caption="Название" Width="20%">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя Ch" FieldName="NameChina" Visible="False" VisibleIndex="4">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEnglish" Visible="False" VisibleIndex="5">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Branch" VisibleIndex="6" Caption="Отделение" Width="20%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Вышестоящее заведение" FieldName="ParentId" VisibleIndex="7">
                    <PropertiesComboBox DataSourceID="uxMainDataSource" NullDisplayText="Верхний уровень" TextField="Name" ValueField="Id">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="8">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Адрес" FieldName="Address" Visible="False" VisibleIndex="9">
                    <PropertiesMemoEdit Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Адрес Ch" FieldName="AddressChina" Visible="False" VisibleIndex="12">
                    <PropertiesMemoEdit Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Адрес En" FieldName="AddressEnglish" Visible="False" VisibleIndex="13">
                    <PropertiesMemoEdit Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn Caption="Директор" FieldName="BossName" VisibleIndex="14" Width="120px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn Caption="Реквизиты" FieldName="Requisits" Visible="False" VisibleIndex="15">
                    <PropertiesMemoEdit Rows="7">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn Caption="Телефон" FieldName="Contacts" VisibleIndex="16" Width="200px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Файл печати" FieldName="PechatFileName" Visible="False" VisibleIndex="17">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Файл подписи" FieldName="PodpisFileName" Visible="False" VisibleIndex="18">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastUser" Visible="False" VisibleIndex="19">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="20">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CreateUser" Visible="False" VisibleIndex="21">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="CreateDate" Visible="False" VisibleIndex="22">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataMemoColumn Caption="Доп.текст Ch  (отчёты)" FieldName="AdditionalCh" Visible="False" VisibleIndex="24">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Доп.текст (отчёты)" FieldName="AdditionalRu" Visible="False" VisibleIndex="26">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Описание Ch (отчёты)" FieldName="DescriptionCh" Visible="False" VisibleIndex="28">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Описание (отчёты)" FieldName="DescriptionRu" Visible="False" VisibleIndex="30">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Приглашение Ch (отчёты)" FieldName="PriglashenieCh" Visible="False" VisibleIndex="32">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Приглашение (отчёты)" FieldName="PriglashenieRu" Visible="False" VisibleIndex="33">
                    <PropertiesMemoEdit MaxLength="500" Rows="2">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn Caption="Сокращёное имя" FieldName="ShortName" VisibleIndex="2" Width="60px">
                    <PropertiesTextEdit MaxLength="10">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Department" DeleteMethod="DeleteDepartment" InsertMethod="InsertDepartment" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateDepartment">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
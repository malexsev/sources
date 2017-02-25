<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="TransferInfoList.aspx.cs" Inherits="Cure.WebAdmin.Admin.TransferInfoList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowDeleteButton="True" ShowNewButtonInHeader="True" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="4" Caption="Наименование" Width="150px">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="5" Caption="Описание">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Клиника" FieldName="DepartmentId" VisibleIndex="3" Width="150px">
                    <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="ShortName" ValueField="Id" ValueType="System.Int32">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Текст оповещения" FieldName="Text" VisibleIndex="6">
                    <PropertiesMemoEdit Rows="20">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.DepartmentTransferInfo" DeleteMethod="DeleteDepartmentTransferInfo" InsertMethod="InsertDepartmentTransferInfo" SelectMethod="GetDepartmentTransferInfos" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateDepartmentTransferInfo" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
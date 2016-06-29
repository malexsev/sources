<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="StopVisitList.aspx.cs" Inherits="Cure.WebAdmin.Admin.StopVisitList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="7" Caption="Описание">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Тип блокировки" FieldName="StopTypeId" VisibleIndex="8" Width="150px">
                    <PropertiesComboBox DataSourceID="uxStopVisitTypeDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataDateColumn Caption="Дата начала" FieldName="DateFrom" VisibleIndex="4" Width="80px">
                    <PropertiesDateEdit DisplayFormatString="">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Дата окончания" FieldName="DateTo" VisibleIndex="6" Width="80px">
                    <PropertiesDateEdit DisplayFormatString="">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn Caption="Отделение" FieldName="DepartmentId" VisibleIndex="3">
                    <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="ShortName" ValueField="Id" ValueType="System.Int32">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.StopVisit" DeleteMethod="DeleteStopVisit" InsertMethod="InsertStopVisit" SelectMethod="GetStopVisits" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateStopVisit" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxStopVisitTypeDataSource" runat="server" SelectMethod="GetRefStopVisitTypes" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
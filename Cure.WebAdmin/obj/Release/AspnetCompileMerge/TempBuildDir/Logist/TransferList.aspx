<%@ Page AutoEventWireup="true" CodeBehind="TransferList.aspx.cs" Inherits="Cure.WebAdmin.Logist.TransferList" Language="C#" MasterPageFile="~/Main.master" %>
<%@ Import Namespace="Cure.Utils" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <div class="content">
            Список клиентов, прибывающих в ближайшее время. 
        </div>

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="Имя/名" FieldName="NameEng" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FamiliyaEn" VisibleIndex="5" Caption="Фамилия/姓">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CityName" Visible="False" VisibleIndex="8" Caption="Город">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TicketInfo" VisibleIndex="13" Caption="Номер рейса/地点" ToolTip="Прибытие | Убытие">
                    <DataItemTemplate>
                        <%# SiteUtils.GetReisNumber(Eval("TicketInfo")) %>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTimeEditColumn Caption="Прибытие по билету/时间" FieldName="TicketPribitieTime" VisibleIndex="1" Width="105px">
                    <PropertiesTimeEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="Custom">
                    </PropertiesTimeEdit>
                </dx:GridViewDataTimeEditColumn>
                <dx:GridViewDataComboBoxColumn Caption="Клиника" FieldName="DepartmentId" VisibleIndex="3" Visible="False">
                    <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="ShortName" ValueField="Id">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="7" Visible="False">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
        </dx:ASPxGridView>
        <br />
        <br>
        1. Если номер рейса 0000 - это означает, что до Пекина есть билеты, а до Юньченга билеты отсутствуют
        如果是0000：意思是已经到达北京，没有到运城的票<br />
2. Информация о встрече пациентов может меняться постоянно, каждый день утром просматривайте информацию о встрече пациентов. 
我每天早上看一遍<br />

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="ViewSoonTransferOrders" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="username" SessionField="CurrentUserName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

    </div>

</asp:Content>

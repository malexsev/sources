<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoonVisitsList.ascx.cs" Inherits="Cure.WebAdmin.Admin.Controls.SoonVisitsList" %>
<%@ Import Namespace="Cure.Utils" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<div class="content">
    Список клиентов, прибывающих в ближайшее время. 
</div>

<dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Имя" FieldName="NameEng" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="FamiliyaEn" VisibleIndex="6" Caption="Фамилия">
        <DataItemTemplate>
            <a onclick="javascript:OpenReps('<%# Eval("VisitId")%>');" class="hyperlink" style="color: #27408b"><%# Container.Text %></a>
        </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="OtchestvoEn" Visible="False" VisibleIndex="13">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CityName" VisibleIndex="11" Caption="Город">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="DateFrom" VisibleIndex="14" Visible="False">
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn FieldName="DateTo" VisibleIndex="15" Visible="False">
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="TicketInfo" VisibleIndex="3" Caption="Номер рейса" ToolTip="Прибытие | Убытие">
            <DataItemTemplate>
                <%# SiteUtils.GetReisNumber(Eval("TicketInfo")) %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="21" Caption="Примечания">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="12">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTimeEditColumn Caption="Прибытие по билету" FieldName="TicketPribitieTime" VisibleIndex="1" Width="105px">
            <PropertiesTimeEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
            </PropertiesTimeEdit>
        </dx:GridViewDataTimeEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="Клиника" FieldName="DepartmentId" VisibleIndex="4">
            <PropertiesComboBox DataSourceID="uxDepartmentDataSource" TextField="ShortName" ValueField="Id">
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="10">
            <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn Caption="Дата рождения" FieldName="BirthDate" VisibleIndex="9" Width="60px">
            <DataItemTemplate>
                <%# SiteUtils.ParseDate(Eval("BirthDate"), DateTime.Today, "ru-RU").ToString("dd-MM-yyyy") %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
</dx:ASPxGridView>

<asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="ViewSoonOrders" TypeName="Cure.DataAccess.BLL.DataAccessBL">
    <SelectParameters>
        <asp:Parameter DefaultValue="90" Name="filter" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

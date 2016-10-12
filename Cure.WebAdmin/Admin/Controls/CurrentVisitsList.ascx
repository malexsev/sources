<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrentVisitsList.ascx.cs" Inherits="Cure.WebAdmin.Admin.Controls.CurrentVisitsList" %>
<%@ Import Namespace="Cure.Utils" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>



<div class="content">
    Сейчас на лечении в клинике (выберите клинику):
    <br/>
    <dx:ASPxComboBox ID="uxDepartment" runat="server" DataSourceID="uxDepartmentDataSource" TextField="Name" ValueField="Id" Width="600px" AutoPostBack="True" OnSelectedIndexChanged="uxDepartment_SelectedIndexChanged" ValueType="System.Int32">
    </dx:ASPxComboBox>
</div>

<dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Имя" FieldName="NameEng" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="FamiliyaEn" VisibleIndex="2" Caption="Фамилия">
            <DataItemTemplate>
                <a onclick="javascript:OpenReps('<%# Eval("VisitId")%>');" class="hyperlink" style="color: #27408b"><%# Container.Text %></a>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="7" Caption="Номер комнаты" ToolTip="Номер комнаты">
            <DataItemTemplate>
                -<%--<%# SiteUtils.GetReisNumber(Eval("TicketInfo")) %>--%>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="8" Caption="Примечания">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="6" Caption="Email">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTimeEditColumn Caption="Прибытие по билету" FieldName="TicketPribitieTime" VisibleIndex="1" Width="105px">
            <PropertiesTimeEdit DisplayFormatString="{0:dd-MM-yyyy H:mm}" EditFormat="DateTime">
            </PropertiesTimeEdit>
        </dx:GridViewDataTimeEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="5">
            <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Дата рождения" FieldName="PacientId" VisibleIndex="4">
            <PropertiesComboBox DataSourceID="uxPacientDataSource" TextField="BirthDate" ValueField="Id" ValueType="System.Int32">
            </PropertiesComboBox>
            <DataItemTemplate>
                <%# SiteUtils.ParseDate(Container.Text, DateTime.Today, "ru-RU").ToString("dd-MM-yyyy") %>
            </DataItemTemplate>
        </dx:GridViewDataComboBoxColumn>
    </Columns>
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
</dx:ASPxGridView>

<asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="ViewCurrentVisits" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter ControlID="uxDepartment" DefaultValue="" Name="departmentId" PropertyName="Value" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxDepartmentDataSource" runat="server" SelectMethod="GetDepartments" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="uxPacientDataSource" runat="server" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

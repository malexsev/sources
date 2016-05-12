<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="PacientList.aspx.cs" Inherits="Cure.WebAdmin.Admin.PacientList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <table>
            <tr>
                <td>Фильтр по email:</td>
                <td>
                    <dx:ASPxTextBox ID="uxFilterEmailTextBox" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxButton ID="uxFilterButton" runat="server" Text="Поиск" OnClick="uxFilterButton_Click"></dx:ASPxButton>
                </td>
            </tr>
        </table>

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="Id" OnRowUpdating="uxMainGrid_RowUpdating" OnRowInserting="uxMainGrid_RowInserting" DataSourceID="uxMainDataSource">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" Caption="Имя">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя En" FieldName="NameEng" Visible="False" VisibleIndex="14">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Familiya" Caption="Фамилия" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Фамилия En" FieldName="FamiliyaEn" Visible="False" VisibleIndex="16">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Отчество" FieldName="Otchestvo" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Город" FieldName="CityName" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Адрес" FieldName="Address" VisibleIndex="10" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="BirthDate" VisibleIndex="9" Caption="Дата рождения" Width="80px">
                    <PropertiesDateEdit NullDisplayText="Не указано">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="SerialNumber" Visible="False" VisibleIndex="19" Caption="Пасспорт">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OwnerUser" Visible="False" VisibleIndex="30" ReadOnly="True">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastUser" ReadOnly="True" Visible="False" VisibleIndex="31">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="LastDate" Visible="False" VisibleIndex="32" ReadOnly="True">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CreateUser" ReadOnly="True" Visible="False" VisibleIndex="33">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="CreateDate" Visible="False" VisibleIndex="34" ReadOnly="True">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="5">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Anamnez" FieldName="Anamnez" Visible="False" VisibleIndex="26">
                    <PropertiesMemoEdit Rows="10">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataMemoColumn Caption="Диагноз" FieldName="Diagnoz" Visible="False" VisibleIndex="25">
                    <PropertiesMemoEdit Rows="5">
                    </PropertiesMemoEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <%--<SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />--%>
<%--            <Templates>
                <DetailRow>
                    Информация по заездам (не готово)
                </DetailRow>
            </Templates>--%>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Pacient" DeleteMethod="DeletePacient" InsertMethod="InsertPacient" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdatePacient"><SelectParameters>
                <asp:ControlParameter ControlID="uxFilterEmailTextBox" DefaultValue="" Name="email" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxSputnikDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Pacient" DeleteMethod="DeletePacient" InsertMethod="InsertPacient" SelectMethod="GetPacients" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdatePacient"></asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL"></asp:ObjectDataSource>

    </div>

</asp:Content>

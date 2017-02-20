<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewsList.aspx.cs" Inherits="Cure.WebAdmin.Admin.NewsList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function OpenDocs(newsid) {
            var url = '/Admin/NewsLetter.aspx?newspageid=' + newsid;
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnHtmlRowPrepared="uxMainGrid_HtmlRowPrepared" ValidateRequestMode="Disabled" OnRowInserting="uxMainGrid_RowInserting" OnRowUpdating="uxMainGrid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataCheckColumn Caption="Активна" FieldName="IsActive" VisibleIndex="1" Width="50px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Alias" VisibleIndex="3" Caption="Псевдоним" ToolTip="Только латинские символы и без пробелов - разрешены знак тире и нижнего подчёркивания!">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Name" Caption="Название" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="GuidId" VisibleIndex="4" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="5" Caption="Тема">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя автора" FieldName="CreatorName" VisibleIndex="6" Visible="False">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Дата" FieldName="Date" VisibleIndex="9" Width="100px">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Настройка" FieldName="Settings" Visible="False" VisibleIndex="12">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Создано" FieldName="CreateDate" ReadOnly="True" Visible="False" VisibleIndex="13">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Редактировано" FieldName="EditDate" ReadOnly="True" Visible="False" VisibleIndex="14">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Пользователь" FieldName="LastUser" ReadOnly="True" Visible="False" VisibleIndex="15">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn Caption="Разметка" FieldName="Text" Visible="False" VisibleIndex="8">
                    <PropertiesMemoEdit Rows="30">
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataSpinEditColumn Caption="Сортировка" FieldName="Sort" VisibleIndex="11" Width="60px">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataTextColumn Caption="Расс." VisibleIndex="26" Width="25px">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <a onclick="javascript:OpenDocs('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">
                            <img src="../Content/Images/editors_mail2.gif" />
                        </a>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.NewsPage" DeleteMethod="DeleteNewsPage" InsertMethod="InsertNewsPage" SelectMethod="GetNewsPages" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateNewsPage" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

</asp:Content>

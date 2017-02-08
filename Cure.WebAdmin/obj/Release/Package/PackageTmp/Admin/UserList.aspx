<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="UserList.aspx.cs" Inherits="Cure.WebAdmin.Admin.UserList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
<%--        <table>
            <tr>
                <td>Фильтр по email:</td>
                <td>
                    <dx:ASPxTextBox ID="uxFilterEmailTextBox" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxButton ID="uxFilterButton" runat="server" Text="Поиск" OnClick="uxFilterButton_Click"></dx:ASPxButton>
                </td>
            </tr>
        </table>--%>

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="UserName" DataSourceID="uxMainDataSource" OnRowDeleting="uxMainGrid_RowDeleting">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" ShowDeleteButton="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="1" Caption="Логин">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="LastActivityDate" VisibleIndex="2" Caption="Последняя активность">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="3">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Активен" FieldName="IsApproved" VisibleIndex="4">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Блокирован" FieldName="IsLockedOut" VisibleIndex="5">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataDateColumn FieldName="CreateDate" VisibleIndex="6" Caption="Создан">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Входил" FieldName="LastLoginDate" VisibleIndex="7">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Менял пароль" FieldName="LastPasswordChangedDate" VisibleIndex="8">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="LastLockoutDate" VisibleIndex="9" Caption="Блокирован">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Коментарий" FieldName="Comment" VisibleIndex="11">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <%--<SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />--%>
<%--            <Templates>
                <DetailRow>
                    Информация по заездам (не готово)
                </DetailRow>
            </Templates>--%>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
            <SettingsDataSecurity AllowInsert="False" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.ViewUserMembership" SelectMethod="ViewUserMembership" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateUserMembership" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>

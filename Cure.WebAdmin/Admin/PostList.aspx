<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="PostList.aspx.cs" Inherits="Cure.WebAdmin.Admin.PostList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnRowInserting="uxMainGrid_RowInserting" OnRowUpdating="uxMainGrid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowDeleteButton="True" ShowNewButtonInHeader="True" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="ParentPostId" Caption="Ответ на пост" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="GuidId" VisibleIndex="3" Caption="Уникальный номер">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OwnerUser" VisibleIndex="4" Caption="Пользователь">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ответ пользователю" FieldName="AnserToUser" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя полльзователя" FieldName="CopyOwnerName" VisibleIndex="6" Visible="False">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя ответ пользователя" FieldName="CopyOwnerLocation" Visible="False" VisibleIndex="7">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="8" Caption="Тема" Visible="False">
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Создан" FieldName="CreateDate" VisibleIndex="11">
                    <PropertiesDateEdit EditFormat="DateTime" EditFormatString="0:dd-MM-yyyy H:mm">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn Caption="Редактирован" FieldName="LastEdit" VisibleIndex="12">
                    <PropertiesDateEdit EditFormat="DateTime" EditFormatString="0:dd-MM-yyyy H:mm">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataMemoColumn Caption="Текст" FieldName="Text" VisibleIndex="10" Visible="False">
                    <PropertiesMemoEdit Rows="10">
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" Visible="True" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Post" DeleteMethod="DeletePost" InsertMethod="InsertPost" SelectMethod="GetPosts" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdatePost" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

    </div>

</asp:Content>

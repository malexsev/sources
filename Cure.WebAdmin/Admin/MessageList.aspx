<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="MessageList.aspx.cs" Inherits="Cure.WebAdmin.Admin.MessageList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowDeleteButton="True" ShowNewButtonInHeader="True" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="FromUserName" VisibleIndex="2" Caption="Отправитель">
                    <PropertiesTextEdit MaxLength="50">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FromDisplay" VisibleIndex="3" Caption="Имя отправителя">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Получатель" FieldName="ToUserName" VisibleIndex="4">
                    <PropertiesTextEdit MaxLength="50">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя получателя" FieldName="ToDisplay" VisibleIndex="5">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="8" Caption="Тема">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTimeEditColumn Caption="Время отправки" FieldName="SendTime" VisibleIndex="7" Width="150px">
                    <PropertiesTimeEdit DisplayFormatString="dd-MM-yyyy hh:mm:ss" EditFormat="DateTime">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTimeEdit>
                </dx:GridViewDataTimeEditColumn>
                <dx:GridViewDataMemoColumn Caption="Сообщение" FieldName="Text" Visible="False" VisibleIndex="9">
                    <PropertiesMemoEdit Rows="10">
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" Visible="True" />
                </dx:GridViewDataMemoColumn>
            </Columns>
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Message" DeleteMethod="DeleteMessage" InsertMethod="InsertMessage" SelectMethod="GetMessages" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateMessage" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

    </div>

</asp:Content>
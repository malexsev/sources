<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewsLetter.aspx.cs" Inherits="Cure.WebAdmin.Admin.NewsLetter" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallback" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var cntSuccess = 0;
        var cntError = 0;
        var sentList = [];

        function SendEmails() {
            $("#result").html("");
            $("#errors").html("");
            cntSuccess = cntError = 0;
            window.grid.GetSelectedFieldValues('Email', OnGetRowValues);
        }

        function OnGetRowValues(arr) {
            arr.forEach(function (email, i) {
                if (jQuery.inArray(email, sentList) == -1) {
                    window.cbSender.PerformCallback(email);
                    sentList.push(email);
                }
            });
        }

        function OnSendComplete(s, e) {
            var result = $("#result");
            var errors = $("#errors");
            if (e.result == "OK") {
                cntSuccess += 1;
            } else if (e.result == "NONE") {
                return;
            }
            else {
                cntError += 1;
                errors.append(e.result);
            }
            result.html("Отправлено, успешно: <b>" + cntSuccess + "</b>; ошибок: <b>" + cntError + "</b>");
        };
    </script>


    <dx:ASPxFilterControl ID="uxFilter" runat="server" Width="350px" ClientInstanceName="filter" ViewMode="VisualAndText">
        <Columns>
            <dx:FilterControlColumn ColumnType="String" DisplayName="Электронная почта" PropertyName="Email">
            </dx:FilterControlColumn>
            <dx:FilterControlColumn ColumnType="DateTime" DisplayName="Дата" PropertyName="Date">
            </dx:FilterControlColumn>
            <dx:FilterControlColumn ColumnType="String" DisplayName="Страна" PropertyName="Country">
            </dx:FilterControlColumn>
            <dx:FilterControlColumn ColumnType="String" DisplayName="Источник" PropertyName="Source">
            </dx:FilterControlColumn>
        </Columns>
        <ClientSideEvents Applied="function(s, e) { grid.ApplyFilter(e.filterExpression);}" />
    </dx:ASPxFilterControl>
    <dx:ASPxButton runat="server" ID="uxApplyButton" Text="Применить фильтр" AutoPostBack="false" UseSubmitBehavior="false" Width="80px" Style="margin: 12px 1em auto auto;">
        <ClientSideEvents Click="function() { filter.Apply(); }" />
    </dx:ASPxButton>

    <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnHtmlRowPrepared="uxMainGrid_HtmlRowPrepared">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="20px" Visible="True" SelectAllCheckboxMode="Page" ShowSelectCheckbox="True">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Email" Caption="Электронная почта" VisibleIndex="2">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Дата" FieldName="Date" VisibleIndex="3" ToolTip="Для профилей НД - это день рождения, для сопровождающих - день рождения, для подписчиков - день подписки, для пользователей - день регистрации" Width="130px">
                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy hh:mm:ss" EditFormat="DateTime" EditFormatString="dd-MM-yyyy hh:mm:ss">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Source" VisibleIndex="5" Caption="Тип добавления" ToolTip="Источник откуда взят данный контакт">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="4" Caption="Имя" ToolTip="Для профиля НД - это контактное лицо, для сопровождающих - имя, для пользователей - username, для подписчиков - пустая строка">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    </dx:ASPxGridView>
    <br />

    <asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="ViewSubscribers" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>


    <dx:ASPxCallback ID="uxCallback" ClientInstanceName="cbSender" runat="server" OnCallback="uxCallback_OnCallback">
        <ClientSideEvents CallbackComplete="OnSendComplete"></ClientSideEvents>
    </dx:ASPxCallback>
    <div id="result"></div>
    
    <dx:ASPxButton runat="server" ID="uxSendButton" Text="Отправить письма" AutoPostBack="false" UseSubmitBehavior="false" Width="80px" Style="margin: 12px 1em auto auto;">
        <ClientSideEvents Click="function() { SendEmails(); }" />
    </dx:ASPxButton>
    <div id="errors"></div>

</asp:Content>

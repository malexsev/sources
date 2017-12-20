<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewsLetter.aspx.cs" Inherits="Cure.WebAdmin.Admin.NewsLetter" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallback" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var result = $('#result');
        var cntSuccess = 0;
        var cntError = 0;
        var sentList = [];

        function SendEmails() {
            uxResultLabel.SetValue("");
            uxLogLabel.SetValue("");
            cntSuccess = cntError = 0;
            window.grid.GetSelectedFieldValues('Email', OnGetRowValues);
        }

        var DoNext = function () {
            if (!sentList.length) {
                uxResultLabel.SetValue("Отправлено, успешно: <b>" + cntSuccess + "</b>; ошибок: <b>" + cntError + "</b>");
                return;
            }

            var email = sentList[0];
            uxLogLabel.SetValue(uxLogLabel.GetValue() == null ? "" : uxLogLabel.GetValue() + email + " - ");
            sentList.splice($.inArray(email, sentList), 1);
            window.cbSender.PerformCallback(email);
            var result = uxResultLabel.GetMainElement();
            result.scrollIntoView();
            result.parentNode.scrollIntoView();
        };

        function OnGetRowValues(arr) {
            arr.forEach(function (email, i) {
                if (jQuery.inArray(email, sentList) == -1) {
                    sentList.push(email);
                }
            });
            DoNext();
        }

        function OnSendComplete(s, e) {
            if (e.result == "OK") {
                cntSuccess += 1;
                uxLogLabel.SetValue(uxLogLabel.GetValue() == null ? "" : uxLogLabel.GetValue() + "успешно<br />");
            } else if (e.result == "NONE") {
                return;
            }
            else {
                cntError += 1;
                uxLogLabel.SetValue(uxLogLabel.GetValue() == null ? "" : uxLogLabel.GetValue() + "ошибка: " + e.result + "<br />");
            }
            DoNext();
        };


        function OnSelectAllRowsLinkClick() {
            window.grid.SelectRows();
        }
        function OnUnselectAllRowsLinkClick() {
            window.grid.UnselectRows();
        }
        function OnGridViewInit() {
            UpdateTitlePanel();
        }
        function OnGridViewSelectionChanged() {
            UpdateTitlePanel();
        }
        function OnGridViewEndCallback() {
            UpdateTitlePanel();
        }
        function UpdateTitlePanel() {
            var selectedFilteredRowCount = GetSelectedFilteredRowCount();
            if (window.selectAllMode.GetValue() != "AllPages") {
                window.lnkSelectAllRows.SetVisible(window.grid.cpVisibleRowCount > selectedFilteredRowCount);
                window.lnkClearSelection.SetVisible(window.grid.GetSelectedRowCount() > 0);
            }

            var text = "Всего выбрано: <b>" + window.grid.GetSelectedRowCount() + "</b>. ";
            var hiddenSelectedRowCount = window.grid.GetSelectedRowCount() - GetSelectedFilteredRowCount();
            if (hiddenSelectedRowCount > 0)
                text += "Выбранные строки скрыты фильтром: <b>" + hiddenSelectedRowCount + "</b>.";
            text += "<br />";
            info.SetText(text);
        }
        function GetSelectedFilteredRowCount() {
            return window.grid.cpFilteredRowCountWithoutPage + window.grid.GetSelectedKeysOnPage().length;
        }
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
    <br />
    <br />
    <dx:ASPxComboBox ID="selectAllMode" ClientInstanceName="selectAllMode" Caption="Тип Выбрать всё:" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="SelectAllMode_SelectedIndexChanged">
        <RootStyle CssClass="OptionsBottomMargin"></RootStyle>
    </dx:ASPxComboBox>
    <span>Page - на странице, AllPages - на всех страницах</span>
    <br />


    <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id"
        OnCustomJSProperties="GridView_CustomJSProperties" EnableRowsCache="false">
        <ClientSideEvents Init="OnGridViewInit" SelectionChanged="OnGridViewSelectionChanged" EndCallback="OnGridViewEndCallback" />
        <Styles>
            <TitlePanel HorizontalAlign="Left">
            </TitlePanel>
        </Styles>
        <Templates>
            <TitlePanel>
                <dx:ASPxLabel ID="lblInfo" ClientInstanceName="info" runat="server" />
                <dx:ASPxHyperLink ID="lnkSelectAllRows" ClientInstanceName="lnkSelectAllRows" OnLoad="lnkSelectAllRows_Load"
                    Text="Выбрать всех" runat="server" Cursor="pointer" ClientSideEvents-Click="OnSelectAllRowsLinkClick" />
                &nbsp;
                <dx:ASPxHyperLink ID="lnkClearSelection" ClientInstanceName="lnkClearSelection" OnLoad="lnkClearSelection_Load"
                    Text="Очистить выбор" runat="server" Cursor="pointer" ClientVisible="false" ClientSideEvents-Click="OnUnselectAllRowsLinkClick" />
            </TitlePanel>
        </Templates>
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
        <Settings ShowTitlePanel="true" ShowFilterBar="Auto" ShowFilterRow="True" ShowGroupPanel="True" />
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
    </dx:ASPxGridView>
    <br />

    <asp:ObjectDataSource ID="uxMainDataSource" runat="server" SelectMethod="ViewSubscribers" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>


    <dx:ASPxCallback ID="uxCallback" ClientInstanceName="cbSender" runat="server" OnCallback="uxCallback_OnCallback">
        <ClientSideEvents CallbackComplete="OnSendComplete"></ClientSideEvents>
    </dx:ASPxCallback>
    
    <dx:ASPxLabel ID="uxLogLabel" ClientInstanceName="uxLogLabel" runat="server" Text=""></dx:ASPxLabel>
    <dx:ASPxLabel ID="uxResultLabel" ClientInstanceName="uxResultLabel" runat="server" Text=""></dx:ASPxLabel>
    <br />
    <dx:ASPxButton runat="server" ID="uxSendButton" Text="Отправить письма" AutoPostBack="false" UseSubmitBehavior="false" Width="80px" Style="margin: 12px 1em auto auto;">
        <ClientSideEvents Click="function() { SendEmails(); }" />
    </dx:ASPxButton>

</asp:Content>

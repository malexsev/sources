<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="NewsList.aspx.cs" Inherits="Cure.WebAdmin.Admin.NewsList" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

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
<%--            <dx:GridViewDataMemoColumn Caption="Разметка" FieldName="Text" Visible="False" VisibleIndex="8">
                <PropertiesMemoEdit Rows="30">
                </PropertiesMemoEdit>
                <EditFormSettings ColumnSpan="2" Visible="True" />
            </dx:GridViewDataMemoColumn>--%>
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
        <Templates>
            <EditForm>
                <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server"></dx:ASPxGridViewTemplateReplacement>
                <div style="margin: 10px 10px;">
                    <dx:ASPxHtmlEditor Width="800px" ID="uxEditor" Html=<%#Bind("Text")%> runat="server" ShowToolbar1="false" ShowToolbar2="true" ShowTableToolbar="false">
                        <SettingsImageUpload UploadImageFolder="~/Content/custom/">
                        </SettingsImageUpload>
                        <ImagesFileManager ImageFolder="~/Content/custom/">
                        </ImagesFileManager>
                        <PartsRoundPanel ShowDefaultImages="False"></PartsRoundPanel>

                        <SettingsImageSelector Enabled="false">
                            <ToolbarSettings />
                            <UploadSettings Enabled="false" />
                        </SettingsImageSelector>

                        <Toolbars>
                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                <Items>
                                    <dx:ToolbarCutButton>
                                    </dx:ToolbarCutButton>
                                    <dx:ToolbarCopyButton>
                                    </dx:ToolbarCopyButton>
                                    <dx:ToolbarPasteButton>
                                    </dx:ToolbarPasteButton>
                                    <dx:ToolbarPasteFromWordButton>
                                    </dx:ToolbarPasteFromWordButton>
                                    <dx:ToolbarUndoButton BeginGroup="True">
                                    </dx:ToolbarUndoButton>
                                    <dx:ToolbarRedoButton>
                                    </dx:ToolbarRedoButton>
                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                    </dx:ToolbarRemoveFormatButton>
                                    <dx:ToolbarBoldButton BeginGroup="True">
                                    </dx:ToolbarBoldButton>
                                    <dx:ToolbarItalicButton>
                                    </dx:ToolbarItalicButton>
                                    <dx:ToolbarUnderlineButton>
                                    </dx:ToolbarUnderlineButton>
                                    <dx:ToolbarStrikethroughButton>
                                    </dx:ToolbarStrikethroughButton>
                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                    </dx:ToolbarSuperscriptButton>
                                    <dx:ToolbarSubscriptButton>
                                    </dx:ToolbarSubscriptButton>
                                    <dx:ToolbarFontNameEdit>
                                        <Items>
                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                        </Items>
                                    </dx:ToolbarFontNameEdit>
                                    <dx:ToolbarFontSizeEdit>
                                        <Items>
                                            <dx:ToolbarListEditItem Text="1 (10pt)" Value="1" />
                                            <dx:ToolbarListEditItem Text="2 (12pt)" Value="2" />
                                            <dx:ToolbarListEditItem Text="3 (14pt)" Value="3" />
                                            <dx:ToolbarListEditItem Text="4 (18pt)" Value="4" />
                                        </Items>
                                    </dx:ToolbarFontSizeEdit>
                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                    </dx:ToolbarFullscreenButton>
                                </Items>
                            </dx:HtmlEditorToolbar>
                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                <Items>
                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                        <Items>
                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                        </Items>
                                    </dx:ToolbarParagraphFormattingEdit>
                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                    </dx:ToolbarJustifyLeftButton>
                                    <dx:ToolbarJustifyCenterButton>
                                    </dx:ToolbarJustifyCenterButton>
                                    <dx:ToolbarJustifyRightButton>
                                    </dx:ToolbarJustifyRightButton>
                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                    </dx:ToolbarInsertLinkDialogButton>
                                    <dx:ToolbarUnlinkButton>
                                    </dx:ToolbarUnlinkButton>
                                    <dx:ToolbarIndentButton BeginGroup="True">
                                    </dx:ToolbarIndentButton>
                                    <dx:ToolbarOutdentButton>
                                    </dx:ToolbarOutdentButton>
                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                        <Items>
                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                            </dx:ToolbarInsertTableDialogButton>
                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                            </dx:ToolbarTablePropertiesDialogButton>
                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                            </dx:ToolbarInsertTableRowAboveButton>
                                            <dx:ToolbarInsertTableRowBelowButton>
                                            </dx:ToolbarInsertTableRowBelowButton>
                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                            <dx:ToolbarInsertTableColumnToRightButton>
                                            </dx:ToolbarInsertTableColumnToRightButton>
                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                            <dx:ToolbarMergeTableCellRightButton>
                                            </dx:ToolbarMergeTableCellRightButton>
                                            <dx:ToolbarMergeTableCellDownButton>
                                            </dx:ToolbarMergeTableCellDownButton>
                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                            </dx:ToolbarDeleteTableButton>
                                            <dx:ToolbarDeleteTableRowButton>
                                            </dx:ToolbarDeleteTableRowButton>
                                            <dx:ToolbarDeleteTableColumnButton>
                                            </dx:ToolbarDeleteTableColumnButton>
                                        </Items>
                                    </dx:ToolbarTableOperationsDropDownButton>

                                    <dx:ToolbarInsertImageDialogButton BeginGroup="True">
                                    </dx:ToolbarInsertImageDialogButton>
                                </Items>
                            </dx:HtmlEditorToolbar>
                            <dx:HtmlEditorToolbar Name="TableToolbar">
                                <Items>
                                    <dx:ToolbarInsertTableDialogButton BeginGroup="True" Text="">
                                    </dx:ToolbarInsertTableDialogButton>
                                    <dx:ToolbarTablePropertiesDialogButton BeginGroup="True" Text="">
                                    </dx:ToolbarTablePropertiesDialogButton>
                                    <dx:ToolbarTableRowPropertiesDialogButton Text="">
                                    </dx:ToolbarTableRowPropertiesDialogButton>
                                    <dx:ToolbarTableColumnPropertiesDialogButton Text="">
                                    </dx:ToolbarTableColumnPropertiesDialogButton>
                                    <dx:ToolbarTableCellPropertiesDialogButton Text="">
                                    </dx:ToolbarTableCellPropertiesDialogButton>
                                    <dx:ToolbarInsertTableRowAboveButton BeginGroup="True" Text="">
                                    </dx:ToolbarInsertTableRowAboveButton>
                                    <dx:ToolbarInsertTableRowBelowButton Text="">
                                    </dx:ToolbarInsertTableRowBelowButton>
                                    <dx:ToolbarInsertTableColumnToLeftButton Text="">
                                    </dx:ToolbarInsertTableColumnToLeftButton>
                                    <dx:ToolbarInsertTableColumnToRightButton Text="">
                                    </dx:ToolbarInsertTableColumnToRightButton>
                                    <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True" Text="">
                                    </dx:ToolbarSplitTableCellHorizontallyButton>
                                    <dx:ToolbarSplitTableCellVerticallyButton Text="">
                                    </dx:ToolbarSplitTableCellVerticallyButton>
                                    <dx:ToolbarMergeTableCellRightButton Text="">
                                    </dx:ToolbarMergeTableCellRightButton>
                                    <dx:ToolbarMergeTableCellDownButton Text="">
                                    </dx:ToolbarMergeTableCellDownButton>
                                    <dx:ToolbarDeleteTableButton BeginGroup="True" Text="">
                                    </dx:ToolbarDeleteTableButton>
                                    <dx:ToolbarDeleteTableRowButton Text="">
                                    </dx:ToolbarDeleteTableRowButton>
                                    <dx:ToolbarDeleteTableColumnButton Text="">
                                    </dx:ToolbarDeleteTableColumnButton>
                                </Items>
                            </dx:HtmlEditorToolbar>
                        </Toolbars>
                    </dx:ASPxHtmlEditor>
                </div>
                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.NewsPage" DeleteMethod="DeleteNewsPage" InsertMethod="InsertNewsPage" SelectMethod="GetNewsPages" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateNewsPage" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

</asp:Content>

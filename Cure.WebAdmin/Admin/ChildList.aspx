<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="ChildList.aspx.cs" Inherits="Cure.WebAdmin.Admin.ChildList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register TagPrefix="uc" TagName="PhotoGallery" Src="~/Controls/PhotoGallery.ascx" %>
<%@ Register TagPrefix="uc" TagName="PhotoUploader" Src="~/Controls/PhotoUploader.ascx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallbackPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function InitGalery(s, e) {
            window.grid.GetRowValues(window.grid.GetFocusedRowIndex(), 'GuidId', OnGetRowValues);
        }

        function OnGetRowValues(values) {
            if (typeof (window.cpGalery) != "undefined") {
                window.cpGalery.PerformCallback(values);
            }
        }

        function RemovePhoto(index) {
            window.cpGalery.PerformCallback(index);
        }

        function OpenDocs(childId) {
            var url = '/Admin/ChildDocs.aspx?childId=' + childId;
            var win = window.open(url, '_blank');
            win.focus();
        }


    </script>

    <div class="content">
        
        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id">
            <ClientSideEvents FocusedRowChanged="InitGalery"></ClientSideEvents>
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="36px">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="OwnerUser" VisibleIndex="8" Caption="Пользователь" ToolTip="Системный пользователь - держатель профиля">
                    <propertiestextedit maxlength="50"></propertiestextedit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Активен" FieldName="IsActive" VisibleIndex="1" Width="40px">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Имя">
                    <PropertiesTextEdit MaxLength="50">
                        <validationsettings>
                        <requiredfield isrequired="True">
                        </requiredfield>
                        </validationsettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Дата рождения" FieldName="Birthday" VisibleIndex="3">
                    <propertiesdateedit><validationsettings><requiredfield isrequired="True"></requiredfield></validationsettings></propertiesdateedit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Region" VisibleIndex="7" Caption="Регион">
                    <propertiestextedit maxlength="150"></propertiestextedit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Diagnoz" VisibleIndex="9" Caption="Диагноз" Visible="False">
                    <propertiestextedit maxlength="250"></propertiestextedit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Имя контакта" FieldName="ContactName" VisibleIndex="10" Visible="False">
                    <propertiestextedit maxlength="50"></propertiestextedit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Email контакта" FieldName="ContactEmail" VisibleIndex="13" Visible="False">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Телефон контакта" FieldName="ContactPhone" VisibleIndex="14" Visible="False">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Одноклассники" FieldName="SocialOk" VisibleIndex="15" Visible="False">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Вконтакте" FieldName="SocialVk" Visible="False" VisibleIndex="17">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Мой мир" FieldName="SocialMm" Visible="False" VisibleIndex="18">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Фейсбук" FieldName="SocialFb" Visible="False" VisibleIndex="19">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Вебмани" FieldName="FinWebmoney" Visible="False" VisibleIndex="20">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ЯндексДеньги" FieldName="FinYandexMoney" Visible="False" VisibleIndex="24">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Киви" FieldName="FinKiwi" Visible="False" VisibleIndex="25">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Номер телефона" FieldName="FinPhoneNumber" Visible="False" VisibleIndex="34">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>
                <%--<dx:GridViewDataTextColumn Caption="Номер карточки банка" FieldName="FinCardNumber" Visible="False" VisibleIndex="44">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <editformsettings visible="True" />
                </dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataComboBoxColumn Caption="Страна" FieldName="CountryId" VisibleIndex="6">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                        <validationsettings>
                        <requiredfield isrequired="True">
                        </requiredfield>
                        </validationsettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Родство контакта" FieldName="ContactRodstvoId" Visible="False" VisibleIndex="12">
                    <propertiescombobox datasourceid="uxRodstvoDataSource" textfield="Name" valuefield="Id" valuetype="System.Int32"><validationsettings><requiredfield isrequired="True"></requiredfield></validationsettings></propertiescombobox>
                    <editformsettings visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Оператор связи" FieldName="FinOperatorId" Visible="False" VisibleIndex="27">
                    <PropertiesComboBox DataSourceID="uxOperatorDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Страна банка" FieldName="FinCountryId" Visible="False" VisibleIndex="39">
                    <PropertiesComboBox DataSourceID="uxCountryDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <%--<dx:GridViewDataComboBoxColumn Caption="Банк" FieldName="FinBankId" Visible="False" VisibleIndex="41">
                    <PropertiesComboBox DataSourceID="uxBankDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>--%>
                <dx:GridViewDataTextColumn Caption="GuidId" FieldName="GuidId" Visible="False" VisibleIndex="44">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Группа диагноза" FieldName="DiagnozId" VisibleIndex="5">
                    <PropertiesComboBox DataSourceID="uxDiagnozDataSource" TextField="Name" ValueField="Id" ValueType="System.Int32">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Вебмани2" FieldName="FinWebmoney2" Visible="False" VisibleIndex="21">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Вебмани3" FieldName="FinWebmoney3" Visible="False" VisibleIndex="22">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ютуб" FieldName="SocialYoutube" Visible="False" VisibleIndex="16">
                    <PropertiesTextEdit MaxLength="250">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <%--<dx:GridViewDataTextColumn Caption="Имя на карточке" FieldName="FinCardName" Visible="False" VisibleIndex="45">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataTextColumn Caption="Док" VisibleIndex="66" Width="25px">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <a onclick="javascript:OpenDocs('<%# Container.KeyValue %>');" class="hyperlink" style="color: #27408b">
                            <img src="../Content/Images/editors_upload.gif" />
                        </a>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Вебмани4" FieldName="FinWebmoney4" Visible="False" VisibleIndex="23">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Номер телефона2" FieldName="FinPhoneNumber2" Visible="False" VisibleIndex="36">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Номер телефона3" FieldName="FinPhoneNumber3" Visible="False" VisibleIndex="37">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Номер телефона4" FieldName="FinPhoneNumber4" Visible="False" VisibleIndex="38">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Оператор связи2" FieldName="FinOperator2Id" Visible="False" VisibleIndex="29">
                    <PropertiesComboBox DataSourceID="uxOperatorDataSource" TextField="Name" ValueField="Id">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Оператор связи3" FieldName="FinOperator3Id" Visible="False" VisibleIndex="31">
                    <PropertiesComboBox DataSourceID="uxOperatorDataSource" TextField="Name" ValueField="Id">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Оператор связи4" FieldName="FinOperator4Id" Visible="False" VisibleIndex="33">
                    <PropertiesComboBox DataSourceID="uxOperatorDataSource" TextField="Name" ValueField="Id">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn Caption="Банковские реквизиты" FieldName="FinBankOther" Visible="False" VisibleIndex="43">
                    <PropertiesMemoEdit Columns="2" MaxLength="500" Rows="7">
                    </PropertiesMemoEdit>
                    <EditFormSettings ColumnSpan="2" Visible="True" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Child" DeleteMethod="DeleteChild" InsertMethod="InsertChild" SelectMethod="GetChilds" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateChild" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxCountryDataSource" runat="server" SelectMethod="GetRefCountries" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxBankDataSource" runat="server" SelectMethod="GetRefBanks" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxDiagnozDataSource" runat="server" SelectMethod="GetRefDiagnozs" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxOperatorDataSource" runat="server" SelectMethod="GetRefOperators" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="uxRodstvoDataSource" runat="server" SelectMethod="GetRefRodstvo" TypeName="Cure.DataAccess.BLL.DataAccessBL" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>

        <dx:ASPxCallbackPanel ID="uxCallbackPanel" ClientInstanceName="cpGalery" runat="server" Width="100%" OnCallback="uxCallbackPanel_OnCallback">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <uc:PhotoGallery runat="server" ID="uxPhotoGallery"></uc:PhotoGallery>
                    <uc:PhotoUploader runat="server" ID="uxPhotoUploader"></uc:PhotoUploader>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

    </div>

</asp:Content>
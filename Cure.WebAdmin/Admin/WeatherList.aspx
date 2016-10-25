<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="WeatherList.aspx.cs" Inherits="Cure.WebAdmin.Admin.WeatherList" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

        <dx:ASPxGridView ID="uxMainGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="uxMainDataSource" KeyFieldName="Id" OnHtmlRowPrepared="uxMainGrid_HtmlRowPrepared">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="False">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="8" Caption="Обстановка">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Details" VisibleIndex="9" Caption="Описание">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTimeEditColumn Caption="Время обновления" FieldName="GetDate" VisibleIndex="3">
                    <PropertiesTimeEdit DisplayFormatString="dd-MM-yyyy hh:mm:ss" EditFormat="DateTime">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTimeEdit>
                </dx:GridViewDataTimeEditColumn>
                <dx:GridViewDataSpinEditColumn Caption="ID Города" FieldName="CityId" VisibleIndex="5">
                    <PropertiesSpinEdit DisplayFormatString="g">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn Caption="Температура" FieldName="Temp" VisibleIndex="7">
                    <PropertiesSpinEdit DisplayFormatString="g">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
            </Columns>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
        </dx:ASPxGridView>

        <asp:ObjectDataSource ID="uxMainDataSource" runat="server" DataObjectTypeName="Cure.DataAccess.Weather" DeleteMethod="DeleteWeather" InsertMethod="InsertWeather" SelectMethod="GetWeathers" TypeName="Cure.DataAccess.BLL.DataAccessBL" UpdateMethod="UpdateWeather"></asp:ObjectDataSource>

</asp:Content>

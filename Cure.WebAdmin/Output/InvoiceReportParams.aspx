<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="InvoiceReportParams.aspx.cs" Inherits="Cure.WebAdmin.Admin.InvoiceReportParams" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagName="ResultBox" TagPrefix="uc" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <uc:ResultBox ID="uxResult" runat="server" />
        <div class="panelsdelimiter"></div>

        <asp:Panel ID="FormPanel" runat="server">
            <table>
                <tr>
                    <th>Наименование</th>
                    <th>Сумма (юаней)</th>
                </tr>
                <tr>
                    <td>Проживание</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine1" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Анализы и исследования</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine2" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Общий массаж</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine3" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Лечебная физкультура</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine4" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Физиопроцедуры</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine5" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Занятия по мелкой моторике</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine6" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Иглотерапия</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine7" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Имплантация калогеновых нитей</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine8" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Капельницы</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine9" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Лекарственные препараты</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine10" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Мед. услуга и расходные материалы</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine11" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
                <tr>
                    <td>Услуги переводчика</td>
                    <td>
                        <dx:ASPxSpinEdit ID="uxLine12" runat="server" Number="0" NumberType="Integer"
                            Increment="1" HorizontalAlign="Right" MinValue="0">
                            <Paddings PaddingRight="5px" />
                            <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
                        </dx:ASPxSpinEdit></td>
                </tr>
            </table>
            <div class="panelsdelimiter"></div>
            <div class="panelsdelimiter"></div>

            <asp:Button ID="SaveButon" runat="server" Text="Сформировать счёт на лечение" OnClick="SaveButon_Click" />
            <div class="panelsdelimiter"></div>

            <br />
            <uc:ResultBox ID="uxResultSave" runat="server" Visible="False" />

        </asp:Panel>
    </div>
</asp:Content>

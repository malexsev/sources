<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="EditVipiska.aspx.cs" Inherits="Cure.WebAdmin.Admin.EditVipiska" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="~/Controls/ResultBox.ascx" TagName="ResultBox" TagPrefix="uc" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <uc:ResultBox ID="uxResult" runat="server" />
        <div class="panelsdelimiter"></div>

        <asp:Panel ID="FormPanel" runat="server">
            
            <div>
                GMFCS:
            </div>
            <dx:ASPxSpinEdit ID="uxGmfcsLevel" runat="server" Number="1" NumberType="Integer"
                Increment="1" HorizontalAlign="Right" MinValue="1" MaxValue="5">
                <Paddings PaddingRight="5px" />
                <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
            </dx:ASPxSpinEdit>
            <div class="panelsdelimiter"></div>
            <div>
                MACS:
            </div>
            <dx:ASPxSpinEdit ID="uxMacsLevel" runat="server" Number="1" NumberType="Integer"
                Increment="1" HorizontalAlign="Right" MinValue="1" MaxValue="5">
                <Paddings PaddingRight="5px" />
                <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
            </dx:ASPxSpinEdit>
            <div class="panelsdelimiter"></div>
            <div>
                CFCS:
            </div>
            <dx:ASPxSpinEdit ID="uxCfcsLevel" runat="server" Number="1" NumberType="Integer"
                Increment="1" HorizontalAlign="Right" MinValue="1" MaxValue="5">
                <Paddings PaddingRight="5px" />
                <SpinButtons Position="Left" ShowLargeIncrementButtons="True" />
            </dx:ASPxSpinEdit>
            <div class="panelsdelimiter"></div>
            <div>
                Результаты лечения:
            </div>
            <div class="panelsdelimiter"></div>
            <asp:TextBox ID="ResultText" Width="100%" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
            <div class="panelsdelimiter"></div>

            <asp:Button ID="SaveButon" runat="server" Text="Сохранить изменения" OnClick="SaveButon_Click" />

            <br />
            <uc:ResultBox ID="uxResultSave" runat="server" Visible="False" />

        </asp:Panel>
    </div>
</asp:Content>

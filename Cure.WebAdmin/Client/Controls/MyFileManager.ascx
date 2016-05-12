<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyFileManager.ascx.cs" Inherits="Cure.WebAdmin.Client.Controls.MyFileManager" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallbackPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxFileManager" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<script type="text/javascript">
    function OpenDocs() {
        var cp = window.cpCheckFolder;
        var pc = window.pcFileManager;
        pc.SetVisible(true);
        cp.PerformCallback();
    }
</script>
<p>
    <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/editors_upload.gif">
        <ClientSideEvents Click="OpenDocs"></ClientSideEvents>
    </dx:ASPxImage>
    <a onclick="javascript:OpenDocs();" class="hyperlink" style="color: #27408b">Файловый обменник Заказа:</a>
</p>
<dx:ASPxCallbackPanel ID="uxCheckFolderCallbackPanel" ClientInstanceName="cpCheckFolder" runat="server" Width="200px" OnCallback="uxCheckFolderCallbackPanel_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="pcFileManager" Height="500px" Width="800px" AutoUpdatePosition="True" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" HeaderText="Документы по заезду">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="content">
                            <dx:ASPxFileManager ID="ASPxFileManager1" ClientInstanceName="fmfilemanager" runat="server" OnFileDownloading="ASPxFileManager1_FileDownloading">
                                <SettingsEditing AllowDelete="true" AllowDownload="True" AllowRename="true"></SettingsEditing>
                                <Settings RootFolder="~\Documents\" />
                            </dx:ASPxFileManager>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoGallery.ascx.cs" Inherits="Cure.WebAdmin.Controls.PhotoGalery" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxImageGallery" TagPrefix="dx" %>
    
<asp:HiddenField runat="server" ID="uxItemIdHidden" />
<p>Для удаления фотогрфии наведите на неё и выбирите "удалить фотографию". Для загрузки новых фотографий нажмите Обзор и выберите файлы, используйте расширения фото-файлов.</p>
<dx:ASPxImageGallery ClientInstanceName="gallery" ID="uxImageGallery" runat="server" Layout="Flow" Width="100%">
    <ItemTextTemplate>
        <div>
            <div class="item_text">
                <a onclick="javascript:RemovePhoto('<%# Container.ItemIndex %>');" >Удалить фотографию</a>
            </div>
        </div>
    </ItemTextTemplate>
    <SettingsFlowLayout ItemsPerPage="30" />
    <PagerSettings Visible="False">
    </PagerSettings>
</dx:ASPxImageGallery>

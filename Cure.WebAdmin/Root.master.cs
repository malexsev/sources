using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses;

namespace Cure.WebAdmin
{
    public partial class RootMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxSplitter1.GetPaneByName("Header").Size = ASPxWebControl.GlobalTheme == "Moderno" ? 95 : 83;
            ASPxSplitter1.GetPaneByName("Header").MinSize = ASPxWebControl.GlobalTheme == "Moderno" ? 95 : 83;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager clientScript = Page.ClientScript;
            Type t = this.GetType();
            if (!clientScript.IsClientScriptIncludeRegistered(t, "jquery"))
                clientScript.RegisterClientScriptInclude(t, "jquery", ResolveClientUrl("~/Scripts/jquery-2.1.3.min.js"));

            uxASPxSiteMapDataSource.SiteMapFileName = HttpContext.Current.User.IsInRole("Администратор")
                ? @"~/Content/admin.sitemap"
                : (HttpContext.Current.User.IsInRole("Трансфер-менеджер") ? @"~/Content/transfer.sitemap" : @"~/Content/client.sitemap");

            TitleLink.InnerText = HttpContext.Current.User.IsInRole("Администратор")
                ? "Панель администрирования"
                : "Личный кабинет";
        }

        protected void HeadLoginStatus_OnLoggedOut(object sender, EventArgs e)
        {
            //SiteUtils.CloseLog();
            Session.Abandon();
        }
    }
}
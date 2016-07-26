using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using System.IO;
    using DevExpress.Web.ASPxClasses;
    using DevExpress.Web.ASPxFileManager;
    using Logic;
    using Utils;

    public partial class MyFileManager : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["RootFolder"] != null)
            {
                ASPxFileManager1.Settings.RootFolder = Session["RootFolder"].ToString();
            }
        }

        protected void uxCheckFolderCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            var path = Path.Combine(@"~\Documents\", clientContainer.CurrentOrder.GuidId + "\\");
            FileUtils.CreateFolderIfNotExists(new HttpServerUtilityWrapper(this.Server), path);
            ASPxFileManager1.Settings.RootFolder = path;
            Session["RootFolder"] = path;
        }

        private ClientContainer clientContainer
        {
            get
            {
                if (HttpContext.Current.Session["ClientContainer"] == null)
                {
                    HttpContext.Current.Session["ClientContainer"] = new ClientContainer(Utils.SiteUtils.GetCurrentUserName());
                }
                return (ClientContainer)HttpContext.Current.Session["ClientContainer"];
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session["ClientContainer"] = value;
                }
            }
        }

        protected void ASPxFileManager1_FileDownloading(object source, FileManagerFileDownloadingEventArgs e)
        {

        }
    }
}
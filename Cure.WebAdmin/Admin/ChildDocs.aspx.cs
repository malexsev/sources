namespace Cure.WebAdmin.Admin
{
    using System;
    using System.IO;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.Web.Data;
    using Utils;

    public partial class ChildDocs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int childId;
                if (int.TryParse(Request.QueryString["childId"], out childId))
                {
                    var dataAccess = new DataAccessBL();
                    ViewChild child = dataAccess.ViewChild(childId);
                    var folderPath = Path.Combine(@"~\Documents\", child.GuidId + "\\");
                    FileUtils.CreateFolderIfNotExists(this, folderPath);
                    uxFileManager.Settings.RootFolder = folderPath;
                    uxFileManager.Visible = true;
                    uxResult.TextGreen =
                        String.Format(
                            "{0} {1} лет, ведёт {2}, тел. {3}, email: {4}",
                            child.Name,
                            DateTime.Today.Year - child.Birthday.Year,
                            child.ContactName,
                            child.ContactPhone,
                            child.ContactEmail
                            );
                    

                }
                else
                {
                    uxFileManager.Visible = false;
                    uxResult.TextRed = "Страница не найдена.";
                }
            }
        }
    }
}
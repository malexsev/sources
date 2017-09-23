namespace Cure.WebAdmin.Admin
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using DevExpress.Web.ASPxGridView;
    using DevExpress.Web.ASPxTabControl;
    using DevExpress.Web.ASPxUploadControl;
    using Utils;
    using Page = System.Web.UI.Page;

    public partial class BiblioList : Page
    {
        protected void uxMainGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["GuidId"] = Guid.NewGuid();
            e.NewValues["Alias"] = e.NewValues["Alias"].ToString().ToLower();
            e.NewValues["IsActive"] = e.NewValues["IsActive"] != null && (bool)e.NewValues["IsActive"];
            e.NewValues["CreateDate"] = e.NewValues["EditDate"] = DateTime.Now;
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["Sort"] = e.NewValues["Sort"] ?? 0;

            var grid = sender as ASPxGridView;
            SaveFileBytesToRow(grid, e.NewValues);
        }

        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["EditDate"] = DateTime.Now;
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["Sort"] = e.NewValues["Sort"] ?? 0;
            
            var grid = sender as ASPxGridView;
            SaveFileBytesToRow(grid, e.NewValues);
        }

        protected bool SaveFileBytesToRow(ASPxGridView grid, OrderedDictionary newValues)
        {
            bool ret = true;
            if (Session["file"] != null)
            {
                var file = Session["file"] as UploadedFile;
                if (file != null)
                {
                    var fileName = file.FileName;
                    const string directory = "~/Content/custom/";
                    var virtualPath = String.Format("{0}{1}", directory, fileName);
                    var fullPath = Server.MapPath(virtualPath);
                    var directoryInfo = new DirectoryInfo(Server.MapPath(directory));
                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }
                    file.SaveAs(fullPath);
                    newValues["Settings"] = System.Web.VirtualPathUtility.ToAbsolute(virtualPath);
                }
                Session.Remove("file");
            }
            else
                ret = false;
            return ret;
        }

        protected void HtmlEditorInit(object sender, EventArgs e)
        {

        }

        protected void ASPxUploadControl1_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var upload = sender as ASPxUploadControl;
            if (upload != null && upload.UploadedFiles[0].IsValid)
            {
                Session["file"] = upload.UploadedFiles[0];
            }
            else
            {
                Session["file"] = null;
            }
        }
    }
}
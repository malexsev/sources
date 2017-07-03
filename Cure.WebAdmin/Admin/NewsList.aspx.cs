namespace Cure.WebAdmin.Admin
{
    using System;
    using Utils;
    using Page = System.Web.UI.Page;

    public partial class NewsList : Page
    {
        protected void uxMainGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            //var editor = uxMainGrid.FindEditFormTemplateControl("uxEditor") as ASPxHtmlEditor;
            //if (editor != null && e.GetValue("Text") != null)
            //{
            //    editor.Html = e.GetValue("Text").ToString();
            //}
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["GuidId"] = Guid.NewGuid();
            e.NewValues["Alias"] = e.NewValues["Alias"].ToString().ToLower();
            e.NewValues["IsActive"] = e.NewValues["IsActive"] != null && (bool)e.NewValues["IsActive"];
            e.NewValues["CreateDate"] = e.NewValues["EditDate"] = DateTime.Now;
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["Sort"] = e.NewValues["Sort"] ?? 0;
        }

        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["EditDate"] = DateTime.Now;
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["Sort"] = e.NewValues["Sort"] ?? 0;
        }

        protected void HtmlEditorInit(object sender, EventArgs e)
        {

        }
    }
}
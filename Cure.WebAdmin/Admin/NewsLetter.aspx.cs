namespace Cure.WebAdmin.Admin
{
    using System;
    using System.Threading;
    using DevExpress.Web;
    using System.Web;
    using DataAccess.BLL;
    using DevExpress.Web.ASPxCallback;
    using DevExpress.Web.ASPxClasses;
    using DevExpress.Web.ASPxEditors;
    using DevExpress.Web.ASPxGridView;
    using Microsoft.VisualBasic;
    using Notification;
    using Page = System.Web.UI.Page;

    public partial class NewsLetter : Page{
    GridViewCommandColumn ComandColumn { get { return (GridViewCommandColumn)uxMainGrid.Columns[0]; } }
        
    protected void Page_Load(object sender, EventArgs e) {
        if(!IsPostBack) {
            selectAllMode.DataSource = Enum.GetValues(typeof(GridViewSelectAllCheckBoxMode));
            selectAllMode.DataBind();
            selectAllMode.SelectedIndex = 1;
        }
    }
    protected void GridView_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) {
        e.Properties["cpVisibleRowCount"] = uxMainGrid.VisibleRowCount;
        e.Properties["cpFilteredRowCountWithoutPage"] = GetFilteredRowCountWithoutPage();
    }
    protected void SelectAllMode_SelectedIndexChanged(object sender, EventArgs e) {
        uxMainGrid.Selection.UnselectAll();
        ComandColumn.SelectAllCheckboxMode = (GridViewSelectAllCheckBoxMode)Enum.Parse(typeof(GridViewSelectAllCheckBoxMode), selectAllMode.Text);
    }
    protected void lnkSelectAllRows_Load(object sender, EventArgs e) {
        ((ASPxHyperLink)sender).Visible = ComandColumn.SelectAllCheckboxMode != GridViewSelectAllCheckBoxMode.AllPages;
    }
    protected void lnkClearSelection_Load(object sender, EventArgs e) {
        ((ASPxHyperLink)sender).Visible = ComandColumn.SelectAllCheckboxMode != GridViewSelectAllCheckBoxMode.AllPages;
    }

    protected int GetFilteredRowCountWithoutPage() {
        int selectedRowsOnPage = 0;
        foreach (var key in uxMainGrid.GetCurrentPageRowValues("Id"))
        {
            if (uxMainGrid.Selection.IsRowSelectedByKey(key))
                selectedRowsOnPage++;
        }
        return uxMainGrid.Selection.FilteredCount - selectedRowsOnPage;
    }

        protected void uxCallback_OnCallback(object s, CallbackEventArgs e)
        {
            int newsId;
            var dal = new DataAccessBL();
            if (!string.IsNullOrEmpty(e.Parameter) && int.TryParse(Request.QueryString["newspageid"], out newsId))
            {
                var email = e.Parameter;
                try
                {
                    var newspage = dal.GetNewsPage(newsId);
                    var notify = new NewsNotification(email, newspage.Subject, newspage.Text,
                        new HttpServerUtilityWrapper(Server));
                    var res = notify.Send();
                    var subscriber = dal.GetNewsletter(email);
                    if (subscriber != null)
                    {
                        if (res)
                        {
                            subscriber.SuccessCount = (subscriber.SuccessCount ?? 0) + 1;
                        }
                        else
                        {
                            subscriber.ErrorsCount = (subscriber.ErrorsCount ?? 0) + 1;
                        }
                        dal.UpdateNewsletter(subscriber);
                    }

                    e.Result = res ? "OK" : this.GetErrorMessage(email);
                }
                catch
                {
                    e.Result = this.GetErrorMessage(email);
                }
            }
            else
            {
                e.Result = "NONE";
            }
        }

        private string GetErrorMessage(string email)
        {
            return string.Format("<font color='red'>{0} - ошибка отправки</font><br>", email);
        }
    }
}
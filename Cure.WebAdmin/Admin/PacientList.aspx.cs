using System;
using System.Web.UI;
using Cure.DataAccess.BLL;
using Cure.Utils;

namespace Cure.WebAdmin.Admin
{

    public partial class PacientList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uxFilterButton_Click(object sender, EventArgs e)
        {
            uxMainGrid.DataBind();
        }

        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["LastDate"] = DateTime.Now;
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["CreateUser"] = e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["CreateDate"] = e.NewValues["LastDate"] = DateTime.Now;
        }
    }
}
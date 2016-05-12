using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin
{
    using DataAccess.BLL;
    using Utils;

    public partial class NotificationLogList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var bll = new DataAccessBL();

            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["LastDate"] = DateTime.Now;
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var bll = new DataAccessBL();

            e.NewValues["CreateUser"] = e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["CreateDate"] = e.NewValues["LastDate"] = DateTime.Now;
        }
    }
}
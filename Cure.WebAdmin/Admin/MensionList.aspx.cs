using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin
{
    using DataAccess.BLL;
    using Notification;
    using Utils;

    public partial class MensionList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if ((int)e.NewValues["DepartmentId"] == -1)
            {
                e.NewValues["DepartmentId"] = null;
            }

            if (((bool)e.OldValues["IsActive"] != (bool)e.NewValues["IsActive"]) && (bool)e.NewValues["IsActive"])
            {
                var notify = new MensionApprovedEmailToUserNotification((int)e.Keys[0], new HttpServerUtilityWrapper(Server));
                notify.Send();
            }
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if ((int)e.NewValues["DepartmentId"] == -1)
            {
                e.NewValues["DepartmentId"] = null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using Logic;
    using Utils;

    public partial class ClientSputniks : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RefreshGrid()
        {
            uxSputnikGrid.DataBind();
        }


        protected void uxSputnikGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["OwnerUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["LastDate"] = DateTime.Now;
        }

        protected void uxSputnikGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["OwnerUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["CreateUser"] = e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["CreateDate"] = e.NewValues["LastDate"] = DateTime.Now;
        }
    }
}
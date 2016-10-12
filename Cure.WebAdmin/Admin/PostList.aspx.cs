using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin
{
    using DataAccess.BLL;
    using DevExpress.Web.Data;
    using Utils;

    public partial class PostList : Page
    {

        protected void uxMainGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (string.IsNullOrEmpty((string)e.NewValues["ParentPostId"]))
            {
                e.NewValues["ParentPostId"] = null;
            }
        }

        protected void uxMainGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (string.IsNullOrEmpty((string)e.NewValues["ParentPostId"]))
            {
                e.NewValues["ParentPostId"] = null;
            }
        }
    }
}
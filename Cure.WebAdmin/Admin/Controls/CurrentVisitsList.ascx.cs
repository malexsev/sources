using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin.Controls
{
    public partial class CurrentVisitsList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            uxMainGrid.DataBind();
        }
    }
}
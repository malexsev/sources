using System;
using System.Web.UI;

namespace Cure.WebAdmin.Logist
{
    using Utils;

    public partial class TransferList : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["CurrentUserName"] = SiteUtils.GetCurrentUserName();
                uxMainGrid.DataBind();
            }
        }
        
    }
}
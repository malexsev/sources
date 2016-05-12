using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin {
    public partial class _Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.IsAuthenticated)
            {
                if (!HttpContext.Current.User.IsInRole("Администратор") &
                    HttpContext.Current.User.IsInRole("Трансфер-менеджер"))
                {
                    Response.Redirect("Client/MyOrders.aspx");
                }
            }
            else
            {
                Response.Redirect("Account/Login.aspx");
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin {
    public partial class RegisterSuccess : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                {
                    Response.Redirect("Login.aspx?justregistered=true");
                }
            }
        }
    }
}
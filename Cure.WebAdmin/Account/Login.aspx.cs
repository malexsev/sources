using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Cure.WebAdmin
{
    using Utils;

    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uxRegisterCompleted.Visible = false;
            if (!IsPostBack)
            {
                if ((Request.QueryString["justregistered"] ?? string.Empty) == "true")
                {
                    uxRegisterCompleted.Visible = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(tbUserName.Text, tbPassword.Text))
            {
                var access = new Cure.DataAccess.BLL.DataAccessBL();
                FormsAuthentication.SetAuthCookie(tbUserName.Text, false);
                Response.Redirect(SiteUtils.IsAdmin(tbUserName.Text) 
                    ? @"~/Admin/SoonList.aspx" 
                    : (SiteUtils.IsTrans(tbUserName.Text) 
                        ? @"~/Logist/TransferList.aspx"
                        : access.GetMyOrders(tbUserName.Text).Any() ? @"~/Client/CurrentOrder.aspx" : @"~/Client/NewOrderWizard.aspx"));
            } else
            {
                tbUserName.ErrorText = "Неверный логин или пароль.";
                tbUserName.IsValid = false;
            }
        }
    }
}
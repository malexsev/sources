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

    public partial class Recovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
        {
            var label =
                (Label)PasswordRecovery1.SuccessTemplateContainer.FindControl("EmailLabel");
            label.Text = e.Message.To[0].Address;
        }
    }
}
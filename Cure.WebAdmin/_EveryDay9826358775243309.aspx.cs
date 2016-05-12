namespace Cure.WebAdmin
{
    using System;
    using DataAccess.BLL;
    using Notification;

    public partial class _EveryDay9826358775243309 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dataAccess = new DataAccessBL();
                dataAccess.SwitchOrderStatusTask();
                var notify = new Before24HoursNotification();
                notify.Send();
                var notifyEmail = new Before24HoursEmailNotification(this);
                notifyEmail.Send();
                var notifyUsers = new BeforeArriveToUserEmailNotification();
                notifyUsers.Send();
            }
        }
    }
}
namespace Cure.WebAdmin
{
    using System;
    using System.Web;
    using DataAccess.BLL;
    using Notification;
    using Utils;

    public partial class _EveryHour2543341928834234 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var notify = new Before2HoursNotification();
                notify.Send();
                var notifyEmail = new Before2HoursEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notifyEmail.Send();
                var dataAccess = new DataAccessBL();
                dataAccess.ClearOnlineUsers();
            }
        }
    }
}
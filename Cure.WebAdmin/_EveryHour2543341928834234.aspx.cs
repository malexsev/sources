namespace Cure.WebAdmin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using Notification;
    using Utils;

    public partial class _EveryHour2543341928834234 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dal = new DataAccessBL();
                var notificationsList = new List<INotification>()
                {
                    new Before2HoursNotification(),
                    new Before2HoursEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new FilesChagedToUserEmailNotification(new HttpServerUtilityWrapper(this.Server))
                };

                notificationsList.ForEach(o =>
                {
                    try
                    {
                        o.Send();
                    }
                    catch (Exception ex)
                    {
                        var notification = new NotificationLog()
                        {
                            ClientName = o.ToString(),
                            Description = ex.Message,
                            Contacts = string.Empty,
                            Details = ex.InnerException == null ? "" : ex.InnerException.Message,
                            ExecutionDate = DateTime.Now,
                            Name = "Ошибка оповещения",
                            Result = string.Empty,
                            Type = "Ошибка",
                            Text = o.GetHashCode().ToString(CultureInfo.InvariantCulture)
                        };

                        dal.InsertNotificationLog(notification);
                    }
                });

                dal.ClearOnlineUsers();
            }
        }
    }
}
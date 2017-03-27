namespace Cure.Notification
{
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public abstract class  BaseNotification : INotification
    {
        private const string subscribedTemplate = @"Вы подписаны на получение наших новостей и акций. Вы всегда можете отписаться от получения уведолений перейдя по <a href=""http://dcp-china.ru/home/subscription?unsubscribe={0}"">ссылке</a>";
        private const string unSubscribedTemplate = @"Вы не подписаны на получение наших новостей и акций. Вы всегда можете подписаться на получения уведолений перейдя по <a href=""http://dcp-china.ru/home/subscription?subscribe={0}"">ссылке</a>";
        
        protected HttpServerUtilityBase server { get; set; }

        public abstract bool Send();

        protected BaseNotification(HttpServerUtilityBase server)
        {
            this.server = server;
        }

        public static void SaveLog(NotificationLog notification)
        {
            var dal = new DataAccessBL();
            dal.InsertNotificationLog(notification);
        }

        public bool SendEmail(string toEmails, string copyEmails, string subject, string body, string reason,
            string attachmentPath = "", string attachmentName = "")
        {
            var dal = new DataAccessBL();
            string subscribeText = null;

            var subcription = dal.GetNewsletter(toEmails);
            subscribeText = string.Format(subcription != null ? subscribedTemplate : unSubscribedTemplate, toEmails);

            var bodyWrapped = string.Format(GetTemplate(), body, subscribeText);
            return EmailUtils.SendEmail(toEmails, copyEmails, subject, bodyWrapped, reason, attachmentPath, attachmentName);
        }

        private string GetTemplate()
        {
            if (this.server == null)
                return "{0}";

            var path = this.server.MapPath(@"~\Content\EmailBodyTemplate.html");
            return System.IO.File.ReadAllText(path);
        }
    }


}

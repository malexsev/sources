namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class SubscribedToUserEmailNotification : BaseNotification
    {
        private string email;
        private string subject = "Подписка на новости и акции.";
        private string body = @"Вы успешно подписаны на рассылку новостей и акций нашего на нашем сайте.<br />";

        public SubscribedToUserEmailNotification(string email, HttpServerUtilityBase server)
            : base(server)
        {
            this.email = email;
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(email.ToLower(), string.Empty, this.subject, this.body, "Пользователю одобрено");
            this.Log(result ? "Доставлено" : "Ошибка доставки", email.ToLower());

            return result;
        }

        private void Log(string result, string recipient)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение о подписке",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Опопвещение о подписке",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

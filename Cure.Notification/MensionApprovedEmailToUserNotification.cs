namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class MensionApprovedEmailToUserNotification : INotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private Mension mension;
        private const string subjectTemplate = "Ваш отзыв на сайте dcp-china.ru";
        private const string bodyTemplate = "Здравствуйте.<br />"
            + @"Вы оставляли отзыв на тему {0} на сайте dcp-china.ru<br />"
            + @"Ваш отзыв прошёл проверку модератора и доступен на <a href='http://dcp-china.ru/Mension'>странице отзывов.</a><br /><br />"
            + @"C уважением, Администрация больницы<br />"
            + @"Тех. поддержка: zqcpchina@gmail.com"; //0 - тема отзыва

        public MensionApprovedEmailToUserNotification(int mensionId)
        {
            var dal = new DataAccessBL();
            this.mension = dal.GetMension(mensionId);
            this.user = dal.GetUserMembership(mension.OwnerUser);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate, mension.CopySubject);
        }

        public bool Send()
        {
            bool result = false;

            result = EmailUtils.SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю Одобрен отзыв");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail);

            return result;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя о модерации его отзыва",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю Одобрен отзыв",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

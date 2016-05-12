namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class OrderSentToUserEmailNotification : INotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private const string subjectTemplate = "Ваша новая заявка на лечение отправлена на рассмотрение";
        private const string bodyTemplate = @"Ваша заполненная Заявка на лечение получена Администрацией больницы.<br/>"
            + @"Ожидайте рассмотрение в течении 3-ех рабочих дней.<br/>"
            + @"Вы можете просмотреть свою Заявку (пдф формат) в Личном кабинете пройдя по <a href='http://www.lk.dcp-china.ru'>ссылке</a><br/><br/>"
            + @"Если Вы считаете, что данное сообщение отправлено Вам ошибочно, просто проигнорируйте его.<br/><br/>"
            + @"Это автоматическое письмо, отвечать на которое не нужно!<br/><br/>"
            + @"Спасибо, что Вы с нами!<br/>"
            + @"C уважением, Администрация больницы<br/>"
            + @"Тех. поддержка: zqcpchina@gmail.com<br/>";

        public OrderSentToUserEmailNotification(string username)
        {
            var dal = new DataAccessBL();
            this.user = dal.GetUserMembership(username);
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public bool Send()
        {
            bool result = false;

            result = EmailUtils.SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю о заявке");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail);

            return result;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об отправке заявки на рассмотрение",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю о заявке",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

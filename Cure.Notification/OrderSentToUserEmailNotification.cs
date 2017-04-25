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

    public class OrderSentToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private const string subjectTemplate = "Ваша новая заявка на лечение отправлена на рассмотрение";

        private const string bodyTemplate = @"Ваша заполненная Заявка на лечение получена Администрацией больницы.<br/>"
                                            + @"Ожидайте рассмотрение в течении 3-ех рабочих дней.<br/>"
                                            + @"Вы можете просмотреть свою Заявку (pdf формат) в Личном кабинете (вкладка Мои файлы), пройдя по <a href='http://www.dcp-china.ru'>ссылке</a><br/><br/>"
                                            + @"Это автоматическое письмо, отвечать на которое не нужно!";

        public OrderSentToUserEmailNotification(string username, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.user = dal.GetUserMembership(username);
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю о заявке");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail, this.body);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об отправке заявки на рассмотрение",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю о заявке",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

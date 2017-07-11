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

    public class ApproveToUserEmailNotification : BaseNotification
    {
        private readonly string subject;
        private readonly string body;
        private string email;
        private const string subjectTemplate = "Завершение решистрации";
        private const string bodyTemplate = "<b>Подтверждение email пользователя.</b><br />"
            + @"Для завершения регистрации пройдите по ссылке <a href='http://dcp-china.ru/Login/Approve?token={0}&email={1}'>ссылке</a><br /><br />"
            //+ @"Для завершения регистрации пройдите по ссылке <a href='http://dcp-china.ru/Login/Approve?token={0}&email={1}'>ссылке</a><br /><br />"
            + @"Это автоматическое письмо, отвечать на которое не нужно!"; //0 - token, 1 - email

        public ApproveToUserEmailNotification(string username, string token, string email, HttpServerUtilityBase server)
            : base(server)
        {
            this.email = email;
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate, token, email);
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.email, string.Empty, this.subject, this.body, "Завершение решистрации");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.email, this.body);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Подтверждение email",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Подтверждение регистрации",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

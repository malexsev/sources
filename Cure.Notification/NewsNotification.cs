namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class NewsNotification : BaseNotification
    {
        private string email { get; set; }
        private string subject { get; set; }
        private string text { get; set; }


        public NewsNotification(string email, string subject, string text, HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                this.email = email;
                this.subject = subject;
                this.text = text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            bool result = SendEmail(this.email, string.Empty, this.subject, this.text, "Рассылка");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.email, this.subject);

            return result;
        }

        private void Log(string result, string recipient, string reason)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Подписчик",
                Description = "Рассылка",
                Contacts = recipient,
                Details = reason,
                ExecutionDate = DateTime.Now,
                Name = "EMail рассылка",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

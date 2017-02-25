namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class FeedbackNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private const string subjectTemplate = "Обратная связь, {0}"; //0 - имя 
        private const string bodyTemplate = "Обратная связь от: {0}.<br/>"
            + "Электронная почта: {1}<br/>"
            + "Телефон: {2},<br/>"
            + "Текст сообщения:<br/>"
            + @"{3}"; //0 - имя, 1 - эл. почта, 2 - телефон, 3 -текст
        private string name { get; set; }
        private string email { get; set; }
        private string phone { get; set; }
        private string text { get; set; }


        public FeedbackNotification(string name, string email, string phone, string text, HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();
                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.name = name;
                this.email = email;
                this.phone = phone;
                this.text = text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            bool result = false;

            string subject = string.Format(subjectTemplate, this.name);
            string body = string.Format(bodyTemplate, this.name
                , this.email
                , this.phone
                , this.text);

            result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, subject, body, "Обратная связь");
            this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, subject, body);

            return result;
        }

        private void Log(string result, string recipient, string subject, string body)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Поступила обратная связь",
                Contacts = recipient,
                Details = subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail обратная связь",
                Result = result,
                Type = "EMail",
                Text = body
            };

            SaveLog(notify);
        }
    }
}

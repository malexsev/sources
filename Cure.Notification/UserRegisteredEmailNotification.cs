namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class UserRegisteredEmailNotification : INotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private string subject;
        private string body;
        private const string subjectTemplate = "ЛК. Новый пользователь: {0}, {1}"; //0 - Логин, 1 - Электронная почта
        private const string bodyTemplate = "Зарегистрирован новый пользователь в Личном кабинете {0}, {1}"; //0 - Логин, 1 - Электронная почта


        public UserRegisteredEmailNotification(string username)
        {
            var dal = new DataAccessBL();
            var user = dal.GetUserMembership(username);
            this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
            this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsRegistrationEmail");
            this.subject = string.Format(subjectTemplate, user.UserName, user.LoweredEmail);
            this.body = string.Format(bodyTemplate, user.UserName, user.LoweredEmail);
        }

        public bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                bool result = false;

                result = EmailUtils.SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Новый пользователь");
                this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value);

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о регистрации нового пользователя",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Регистрация",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

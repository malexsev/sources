namespace Cure.Notification
{
    using System;
    using System.Drawing.Imaging;
    using System.Web;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.Security;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting;
    using Reports;
    using Utils;
    using Page = System.Web.UI.Page;

    public class UserBlockedEmailNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private string subject;
        private string body;
        private const string subjectTemplate = "Пользователь блокирован";
        private const string bodyTemplate = "Блокирован:<br />"
            + "Логин пользователя: {0}<br />"
            + "Время блокировки: {1}<br />"
            + "Электронный адрес: {2}<br /><br />"
            + "<a href='http://lk.dcp-china.ru/Admin/MensionList.aspx'>Страница управления пользователями</a>"; //0 - Логин пользователя, 1 - Время блокировки, 2 - Электронный адрес


        public UserBlockedEmailNotification(MembershipUser membershipUser, HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.subject = subjectTemplate;
                this.body = string.Format(bodyTemplate,
                    membershipUser.UserName,
                    membershipUser.LastLockoutDate.ToString("dd-MM-yyyy H:mm"),
                    membershipUser.Email.ToLower());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Пользователь блокирован");
            this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, this.subject);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Пользователь блокирован",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователь блокирован",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

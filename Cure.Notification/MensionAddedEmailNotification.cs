namespace Cure.Notification
{
    using System;
    using System.Drawing.Imaging;
    using System.Web;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting;
    using Reports;
    using Utils;
    using Page = System.Web.UI.Page;

    public class MensionAddedEmailNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private string subject;
        private string body;
        private const string subjectTemplate = "Поступил отзыв на модерацию"; 
        private const string bodyTemplate = "Поступил отзыв на модерацию<br />"
            + "Имя пользователя с профиля: {0}<br />"
            + "Имя ребёнка: {1}<br />"
            + "Логин пользователя: {2}<br />"
            + "Электронный адрес: {3}<br />"
            + "Текст отзыва:<br /><br />{4}<br /><br />"
            + "<a href='http://lk.dcp-china.ru/Admin/MensionList.aspx'>Страница модерации отзывов</a>"; //0 - Имя пользователя с профиля, 1 - Имя ребёнка, 2 - Логин пользователя, 3 - Электронный адрес, 4 - Текст отзыва


        public MensionAddedEmailNotification(Mension mension, ViewChild view, HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsNewMensionEmail");
                this.subject = subjectTemplate;
                this.body = string.Format(bodyTemplate, 
                    view.ContactName ?? "<не указано>",
                    view.Name ?? "<не указано>", 
                    view.OwnerUser,
                    view.ContactEmail,
                    mension.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                bool result = false;

                result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Новый отзыв");
                this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, this.subject);

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о поступлении нового отзыва",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Новый отзыв",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

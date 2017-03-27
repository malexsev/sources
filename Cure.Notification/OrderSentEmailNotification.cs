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

    public class OrderSentEmailNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private string subject;
        private string body;
        private string attachmentPath;
        private string attachmentName;
        private const string attachmentTemplate = "Заявка {0}, {1}, {2}.pdf"; //0 - аббр. больницы,  1 - ФИО,  2 - «страна»
        private const string subjectTemplate = "Заявка в {0}, {1}, {2}, {3}"; //0 - «короткое имя клиники», 1 - ФИО, 2 - «страна», 3 - «город»
        private const string bodyTemplate = "От пользователя {0} поступила Заявка на лечение.<br />"
            + "Заявка в {5},<br />"
            + "{1},<br />"
            + "{2},<br />"
            + "{3}<br />"
            + "{4}"; //0 - Пользователь, 1 - «ФИО» - «дата рождения», 2 - «страна», 3 - «город», 4 - «планируемая дата приезда и уезда», 5 - «короткое имя клинки»


        public OrderSentEmailNotification(int visitId, HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();
                var visit = dal.GetVisit(visitId);
                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsNewOrderEmail");
                this.subject = string.Format(subjectTemplate, visit.Order.Department.ShortName, visit.Pacient.FullName, visit.Pacient.RefCountry.Name, visit.Pacient.CityName);
                this.body = string.Format(bodyTemplate, visit.Order.OwnerUser, visit.Pacient.FullName, visit.Pacient.RefCountry.Name, visit.Pacient.CityName,
                    string.Format("c {0} по {1}", visit.Order.DateFrom.Year < 1990
                        ? "-"
                        : visit.Order.DateFrom.ToString("dd-MM-yyyy")
                    , visit.Order.DateTo.Year < 1990
                        ? "-"
                        : visit.Order.DateTo.ToString("dd-MM-yyyy"))
                    , visit.Order.Department.ShortName);
                this.attachmentName = string.Format(attachmentTemplate, visit.Order.Department.ShortName, visit.Pacient.FullName, visit.Pacient.RefCountry.Name);

                this.attachmentPath = SiteUtils.GenerateVisitDetailsPdf(visit, this.attachmentName, server, false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                bool result = false;

                result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Новая заявка", this.attachmentPath, this.attachmentName);
                this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, this.body);

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о поступлении новой заявки",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Новая заявка",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

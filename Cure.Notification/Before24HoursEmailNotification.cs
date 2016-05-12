namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
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

    public class Before24HoursEmailNotification : INotification
    {
        private IEnumerable<Visit> visits;
        private Page page;
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private const string attachmentTemplate = "Заявка, {0}, {1}, {2}.pdf"; //0 - ФИО, 1 - «страна», 2 - «город»
        private const string subjectTemplate = "Встречать в {0}, {1}, {2} {3}, {4}, {5}"; //0 - «короткое имя клиники», 1 - дата, время, 2 - рейс, 3 - ФИО, дата рождения, 4 - страна, 5 - город
        private const string bodyTemplate = "Встречать в {0},<br/>"
            + "{1}, {2}»<br/>"
            + "{3},<br/>"
            + "{4},<br/>"
            + @"{5}<br/>{6},<br/><br/><a href='http://lk.dcp-china.ru/{7}'>Ссылка на заявку (пдф формат)</a>"; //0 - короткое имя клиники, 1 - дата, время, 2 - рейс, 3 -ФИО дата рождения, 4 - страна, 5 - город, 6 - примечение, 7 - ссылка на пдф


        public Before24HoursEmailNotification(Page page)
        {
            try
            {
                var timeFrom = DateTime.Now.AddDays(2);
                var timeTo = DateTime.Now.AddDays(3);
                var dal = new DataAccessBL();
                this.page = page;
                this.visits = dal.GetVisitsForTimespan(timeFrom, timeTo);
                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsBefore24HoursArrivalEmail");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Send()
        {
            if (this.settingIsNotify.ValueBool != null && this.settingIsNotify.ValueBool == true && this.visits.Any())
            {
                bool result = false;

                foreach (var visit in visits)
                {
                    string attachmentName = string.Format(attachmentTemplate, visit.Pacient.FullName, visit.Pacient.RefCountry.Name, visit.Pacient.CityName);
                    string attachmentPath = SiteUtils.GenerateVisitDetailsPdf(visit, attachmentName, page);
                    string subject = string.Format(subjectTemplate, visit.Order.Department.ShortName, visit.Order.TicketPribitieTime == null ? "-" : DateTime.Parse(visit.Order.TicketPribitieTime.ToString()).ToString("dd-MM-yyyy H:mm"), visit.Order.TicketInfo
                        , visit.Pacient.FullName
                        , visit.Pacient.RefCountry.Name
                        , visit.Pacient.CityName);
                    string body = string.Format(bodyTemplate, visit.Order.Department.ShortName
                        , visit.Order.TicketPribitieTime == null ? "-" : DateTime.Parse(visit.Order.TicketPribitieTime.ToString()).ToString("dd-MM-yyyy H:mm")
                        , visit.Order.TicketInfo
                        , visit.Pacient.FullName
                        , visit.Pacient.RefCountry.Name
                        , visit.Pacient.CityName
                        , visit.Order.Notes
                        , attachmentPath.Substring(attachmentPath.IndexOf("Documents", StringComparison.Ordinal)).Replace(@"\", @"/"));

                    result = EmailUtils.SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, subject, body, "Прибытие за сутки", attachmentPath, attachmentName);
                    this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, subject);
                }


                return result;
            }

            return false;
        }

        private void Log(string result, string recipient, string subject)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о прибытии за сутки",
                Contacts = recipient,
                Details = subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Прибытие за сутки",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

namespace Cure.Notification
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.Web;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using DataAccess;
    using DataAccess.BLL;
    using DataAccess.Enums;
    using DevExpress.XtraPrinting;
    using Reports;
    using Utils;
    using Page = System.Web.UI.Page;

    public class OrdersUnprocessedEmailNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private IEnumerable<Order> orders;
        private const string attachmentTemplate = "Нерассмотренная Заявка {0}, {1}, {2}.pdf"; //0 - аббр. больницы,  1 - ФИО,  2 - «страна»
        private const string subjectTemplate = "Нерассмотренная Заявка в {0}, {1}, {2}, {3}"; //0 - «короткое имя клиники», 1 - ФИО, 2 - «страна», 3 - «город»
        private const string bodyTemplate = "Нерассмотренная Заявка от пользователя {0}<br />"
            + "Дата отправки: {1},<br />"
            + "Период: {2},<br />"
            + "Клиника: {3},<br />"
            + "ФИО ребёнка, дата рождения: {4},<br />"
            + "Страна: {5},<br />"
            + "Город: {6},<br />"
            + "Электронная почта: {7}<br /><br />"
            + "<a href='http://lk.dcp-china.ru/Admin/OrderList.aspx'>ссылка на перечень заявок</a>"; //0 - Пользователь, 1 - Дата отправки, 2 - Период, 3 -Клиника, 4 - «ФИО», «дата рождения», 5 - Страна, 6 - Город, 7 - Электронная почта

        public OrdersUnprocessedEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();
                this.orders = dal.GetUnprocessedOrders();

                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsUprocessedOrderEmail");
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
                var dal = new DataAccessBL();

                foreach (var order in this.orders)
                {
                    bool result = false;
                    var subject = string.Format(subjectTemplate, order.Department.ShortName, order.Visits.First().Pacient.FullName, order.Visits.First().Pacient.RefCountry.Name, order.Visits.First().Pacient.CityName);
                    var body = string.Format(bodyTemplate, order.OwnerUser,
                        order.LastDate.HasValue ? order.LastDate.Value.ToString("dd-MM-yyyy") : "-",
                        string.Format("c {0} по {1}", order.DateFrom.Year < 1990
                            ? "-"
                            : order.DateFrom.ToString("dd-MM-yyyy")
                        , order.DateTo.Year < 1990
                            ? "-"
                            : order.DateTo.ToString("dd-MM-yyyy")),
                        order.Department.ShortName,
                        order.Visits.First().Pacient.FullName,
                        order.Visits.First().Pacient.RefCountry.Name,
                        order.Visits.First().Pacient.CityName,
                        dal.GetUserMembership(order.OwnerUser).LoweredEmail);
                    var attachmentName = string.Format(attachmentTemplate, order.Department.ShortName, order.Visits.First().Pacient.FullName, order.Visits.First().Pacient.RefCountry.Name);
                    var attachmentPath = SiteUtils.GenerateVisitDetailsPdf(order.Visits.First(), attachmentName, base.server);

                    result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, subject, body, "Нерассмотренная заявка", attachmentPath, attachmentName);
                    this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, subject);
                }
                
                return true;
            }

            return false;
        }

        private void Log(string result, string recipient, string details)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о нерассмотренной заявке",
                Contacts = recipient,
                Details = details,
                ExecutionDate = DateTime.Now,
                Name = "EMail Нерассмотренная заявка",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

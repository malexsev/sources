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

    public class TiketsBoughtEmailNotification : BaseNotification
    {
        private Setting settingAdminsEmails;
        private Setting settingAdminsEmailCopy;
        private Setting settingIsNotify;
        private string subject;
        private string body;
        private const string subjectTemplate = "Встречать в {0}, {1}, {2} {3}, {4}, {5}"; //0 - «короткое имя клиники», 1 - дата, время, 2 - рейс, 3 - ФИО, дата рождения, 4 - страна, 5 - город
        private const string bodyTemplate = "Пользователь {6} ввел информацию о билетах<br/>"
            + "Встречать в {0},<br/>"
            + "{1}, {2}»<br/>"
            + "{3},<br/>"
            + "{4},<br/>"
            + @"{5}<br/><br/><a href='http://lk.dcp-china.ru'>Оперативная информация</a>"; //0 - короткое имя клиники, 1 - дата, время, 2 - рейс, 3 -ФИО дата рождения, 4 - страна, 5 - город, 6 - пользователь


        public TiketsBoughtEmailNotification(int orderId, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            var order = dal.GetOrder(orderId);
            this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
            this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsTiketsBoughtEmail");
            this.subject = string.Format(subjectTemplate, order.Department.ShortName, order.TicketPribitieTime == null ? "-" : DateTime.Parse(order.TicketPribitieTime.ToString()).ToString("dd-MM-yyyy H:mm"), order.TicketInfo
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.FullName : "-"
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.RefCountry.Name : "-"
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.CityName : "-");
            this.body = string.Format(bodyTemplate, order.Department.ShortName
                , order.TicketPribitieTime == null ? "-" : DateTime.Parse(order.TicketPribitieTime.ToString()).ToString("dd-MM-yyyy H:mm")
                , order.TicketInfo
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.FullName : "-"
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.RefCountry.Name : "-"
                , order.Visits.Any() ? order.Visits.FirstOrDefault().Pacient.CityName : "-"
                , order.OwnerUser);
        }

        public override bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                bool result = false;

                result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Покупка билетов");
                this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value);

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о покупке билетов",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Покупка билетов",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class Before45DaysToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private List<Order> orders;
        private const string subjectTemplate = "До планируемого лечения осталось 45 дней";
        private const string bodyTemplate =
            @"Вы записаны на лечение с {0} в настоящее время от Вас не поступало данных о билетах.<br />" +
            @"Пожалуйста сообщите о Ваших планах. Если билеты уже приобретены, отправите нам точное время прибытия в Юньчэн.<br /> " +
            @"Как обстоят дела с оформлением визы?<br />" +
            @"Какая нужна помощь от нас?"; // 0 - DateFrom

        public Before45DaysToUserEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.orders = new List<Order>();
            var searchDate = DateTime.Today.AddDays(45);
            this.orders = dal.GetOrders().Where(x => x.DateFrom == searchDate && x.StatusId == 6).ToList();
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public override bool Send()
        {
            bool result = false;
            var dal = new DataAccessBL();

            foreach (var order in this.orders)
            {
                var user = dal.GetUserMembership(order.OwnerUser);
                var text = string.Format(this.body, order.DateFrom.ToString("dd-MM-yyyy"));
                result = SendEmail(user.LoweredEmail, string.Empty, this.subject, text, "Опопвещение за 45 дней до заезда");
                this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail, text);
            }


            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение за 45 дней до заезда",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Опопвещение за 45 дней до заезда",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

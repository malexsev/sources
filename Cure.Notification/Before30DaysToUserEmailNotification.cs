namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class Before30DaysToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private List<Order> orders;
        private const string subjectTemplate = "До планируемого лечения осталось 30 дней.";
        private const string bodyTemplate =
            @"От Вас не поступало информации. Вы записаны на лечение с {0}.<br />" +
            @"Вынуждены Вам отказать в лечении и удалить Ваше бронирование, что бы дать возможность другим поехать на лечение."; // 0 - DateFrom

        public Before30DaysToUserEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.orders = new List<Order>();
            var searchDate = DateTime.Today.AddDays(30);
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
                result = SendEmail(user.LoweredEmail, string.Empty, this.subject, string.Format(this.body, order.DateFrom.ToString("dd-MM-yyyy")), "Опопвещение за 30 дней до заезда");
                this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail);
            }


            return result;
        }

        private void Log(string result, string recipient)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение за 30 дней до заезда",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Опопвещение за 30 дней до заезда",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

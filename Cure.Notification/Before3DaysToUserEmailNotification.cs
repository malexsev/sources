namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class Before3DaysToUserEmailNotification : BaseNotification
    {
        private readonly string subject;
        private readonly string body;
        private readonly List<Order> orders;

        public Before3DaysToUserEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.orders = new List<Order>();
            var searchDate = DateTime.Today.AddDays(3);
            this.orders = dal.GetOrders().Where(x => x.DateFrom == searchDate && x.StatusId == 7).ToList();
            this.subject = dal.GetSettingByCode("EmailTemplateBefore3DaysSubject").Value;
            this.body = dal.GetSettingByCode("EmailTemplateBefore3DaysBody").Value;
        }

        public override bool Send()
        {
            bool result = false;
            var dal = new DataAccessBL();

            foreach (var order in this.orders)
            {
                var user = dal.GetUserMembership(order.OwnerUser);
                result = SendEmail(user.LoweredEmail, string.Empty, this.subject, this.body, "Опопвещение за 3 днея до заезда");
                this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail, this.body);
            }


            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение за 3 дня до заезда",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Опопвещение за 3 дня до заезда",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

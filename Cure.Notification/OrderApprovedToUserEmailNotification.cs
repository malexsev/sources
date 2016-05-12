namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class OrderApprovedToUserEmailNotification : INotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private Order order;
        private const string subjectTemplate = "Ваша заявка на лечение Одобрена";
        private const string bodyTemplate = @"Здравствуйте.<br /><br />"
            + @"Ваш ребенок подходит для лечения в реабилитационном отделении по лечению ДЦП в {2}.<br />"
            + @"Записали Вас в график с {0} по {1}<br />"
            + @"Ожидайте подготовку документов в течении нескольких дней.<br /><br />"
            + @"C уважением, Администрация больницы<br />"
            + @"Тех. поддержка: zqcpchina@gmail.com"; //0 - «дата приезда», 1 - «дата отъезда», 2 - адрес клиники

        public OrderApprovedToUserEmailNotification(int orderId)
        {
            var dal = new DataAccessBL();
            this.order = dal.GetOrder(orderId);
            this.user = dal.GetUserMembership(order.OwnerUser);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate
                    ,this.order.DateFrom.Year < 1990
                        ? "(уточняется)"
                        : this.order.DateFrom.ToString("dd-MM-yyyy")
                    ,this.order.DateTo.Year < 1990
                        ? "(уточняется)"
                        : this.order.DateTo.ToString("dd-MM-yyyy")
                    ,this.order.Department.Address);
        }

        public bool Send()
        {
            bool result = false;

            result = EmailUtils.SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю одобрено");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail);

            return result;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об одобрении заявки",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю одобрено",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

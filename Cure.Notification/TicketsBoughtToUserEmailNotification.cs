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

    public class TicketsBoughtToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private Order order;
        private const string subjectTemplate = "Ваш статус в личном кабинете изменен на «Куплены билеты»";
        private const string bodyTemplate = @"Здравствуйте.<br />"
            + @"Вы заполнили данные о билетах в своем личном кабинете<br />"
            + @"Встречаем Вас {0} в {1}<br /><br />"
            + @"C уважением, Администрация больницы<br />"
            + @"Тех. поддержка: zqcpchina@gmail.com"; //0 - «дата,время», 1 - адрес клиники

        public TicketsBoughtToUserEmailNotification(int orderId, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.order = dal.GetOrder(orderId);
            this.user = dal.GetUserMembership(order.OwnerUser);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate
                    , this.order.TicketPribitieTime == null ? "-" : DateTime.Parse(this.order.TicketPribitieTime.ToString()).ToString("dd-MM-yyyy H:mm")
                    , this.order.Department.Address);
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю Куплены билеты");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail, this.body);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об изменении статуса на Куплены билеты",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю Куплены билеты",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

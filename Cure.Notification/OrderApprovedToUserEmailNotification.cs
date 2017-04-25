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

    public class OrderApprovedToUserEmailNotification : BaseNotification
    {
        private Setting settingIsNotify;
        private string subject;
        private string body;
        private DateTime dateFrom;
        private DateTime dateTo;
        private ViewUserMembership user;
        private Order order;
        private const string subjectTemplate = "Ваша заявка на лечение Одобрена";
        private const string bodyTemplate = @"Ваш ребенок подходит для лечения в реабилитационном отделении по лечению ДЦП в {2}.<br />"
            + @"Записали Вас в график с {0} по {1}<br /><br />"
            + @"Вам необходимо <b>прочитать и подтверить</b> согласие с <a href='http://dcp-china.ru/home/rules?temp={3}'>Правилами клиники</a>, после прочтения поставьте отметку и нажмите Далее.<br /><br />"
            + @"Ожидайте подготовку документов в течении нескольких дней.<br /><br />"; //0 - «дата приезда», 1 - «дата отъезда», 2 - имя клиники, 3 - guid заявки

        public OrderApprovedToUserEmailNotification(int orderId, DateTime dateFrom, DateTime dateTo, HttpServerUtilityBase server)
            : base(server)
        {
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            var dal = new DataAccessBL();
            this.order = dal.GetOrder(orderId);
            this.user = dal.GetUserMembership(order.OwnerUser);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate
                    , this.dateFrom.Year < 1990
                        ? "(уточняется)"
                        : this.dateFrom.ToString("dd-MM-yyyy")
                    , this.dateTo.Year < 1990
                        ? "(уточняется)"
                        : this.dateTo.ToString("dd-MM-yyyy")
                    ,this.order.Department.Name,
                    this.order.GuidId);
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyUserApproveOrder");
        }

        public override bool Send()
        {
            bool result = false;

            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю одобрено");
                this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail, this.body);
            }

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об одобрении заявки",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю одобрено",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

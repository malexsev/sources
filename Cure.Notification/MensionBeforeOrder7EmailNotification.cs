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
    using DevExpress.XtraPrinting;
    using Reports;
    using Utils;
    using Page = System.Web.UI.Page;

    public class MensionBeforeOrder7EmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private IEnumerable<Order> orders;

        public MensionBeforeOrder7EmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.subject = dal.GetSettingByCode("EmailTemplateMensionRequiredBefore7DaysSubject").Value;
                this.body = dal.GetSettingByCode("EmailTemplateMensionRequiredBefore7DaysBody").Value;

                DateTime day = DateTime.Today.AddDays(7);
                this.orders = dal.GetOrders().Where(x => x.StatusId == 7 && x.DateFrom.Date == day).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            bool result = false;
            var dal = new DataAccessBL();
            foreach (var order in this.orders)
            {
                var email = dal.GetUserMembership(order.OwnerUser).LoweredEmail;
                result = SendEmail(email, string.Empty, this.subject, this.body, "Требуется отзыв за 7 дней");
                this.Log(result ? "Доставлено" : "Ошибка доставки", email, this.body);
            }

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Оповещение требуется отзыв за 7 дней до прибытия",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Требуется отзыв за 7 дней до прибытия",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

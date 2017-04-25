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

    public class MensionBeforeFinish5EmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private IEnumerable<Order> orders;

        public MensionBeforeFinish5EmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.subject = dal.GetSettingByCode("EmailTemplateMensionRequiredBeforeFinish5Subject").Value;
                this.body = dal.GetSettingByCode("EmailTemplateMensionRequiredBeforeFinish5Body").Value;

                DateTime day = DateTime.Today.AddDays(5);
                this.orders = dal.GetOrders().Where(x => x.StatusId == 7 && x.DateTo.Date == day).ToList();
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
                result = SendEmail(email, string.Empty, this.subject, this.body, "Требуется отзыв через 5 дней");
                this.Log(result ? "Доставлено" : "Ошибка доставки", email, this.body);
            }

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Оповещение требуется отзыв через 5 дней после прибытия",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Требуется отзыв через 5 дней после прибытия",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

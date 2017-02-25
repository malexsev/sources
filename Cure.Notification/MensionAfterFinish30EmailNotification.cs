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

    public class MensionAfterFinish30EmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private IEnumerable<Order> orders;

        public MensionAfterFinish30EmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.subject = dal.GetSettingByCode("EmailTemplateMensionRequiredAfterFinish30Subject").Value;
                this.body = dal.GetSettingByCode("EmailTemplateMensionRequiredAfterFinish30Body").Value;

                DateTime day = DateTime.Today.AddDays(-30);
                var mensionUsers = dal.GetMensions().Where(x => x.CreatedDate >= day).Select(x => x.OwnerUser);
                this.orders = dal.GetOrders().Where(x => x.StatusId == 8 && x.DateTo.Date == day && !mensionUsers.Contains(x.OwnerUser)).ToList();
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
                result = SendEmail(email, string.Empty, this.subject, this.body, "Требуется отзыв через 30 дней");
                this.Log(result ? "Доставлено" : "Ошибка доставки", email);
            }

            return result;
        }

        private void Log(string result, string recipient)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Оповещение требуется отзыв через 30 дней после лечения",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Требуется отзыв через 30 дней после лечения",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}

namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class OrderNotCompletedToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private List<ViewUserMembership> users;
        private const string subjectTemplate = "Ваша Заявка на лечение не заполнена";
        private const string bodyTemplate = @"Вы начали заполнять Заявку на сайте dcp-china.ru, но не внесли все данные.<br/>"
            + @"Мы можем Вам помочь.<br/>"
            + @"Вам нужна помощь в заполнении?<br/>";

        public OrderNotCompletedToUserEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.users = new List<ViewUserMembership>();
            dal.GetPendingDrafts().Select(x => x.OwnerUser).ForEach(x => this.users.Add(dal.GetUserMembership(x)));
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public override bool Send()
        {
            bool result = false;

            foreach (var user in this.users)
            {
                result = SendEmail(user.LoweredEmail, string.Empty, this.subject, this.body, "Незаполенная заявка");
                this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail, this.body);
            }


            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя о незаполенной заявке",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю о незаполенной заявке",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

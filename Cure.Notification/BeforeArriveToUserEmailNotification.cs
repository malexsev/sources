namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class BeforeArriveToUserEmailNotification : BaseNotification
    {
        private IEnumerable<Visit> visits;
        private string subject;
        private string body;
        private const string subjectTemplate = "Через 5 дней Вы прибудете на лечение в город Юньчэн Китай";
        private const string bodyTemplate = @"Здравствуйте.<br /><br />"
            + @"Просим Вас ознакомится с порядком действий при прибытии в клинику.<br />"
            + @"Пройдите по <a href='http://dcp-china.ru'>ссылке</a><br /><br />"
            + @"C уважением, Администрация больницы<br />"
            + @"Тех. поддержка: zqcpchina@gmail.com";

        public BeforeArriveToUserEmailNotification(): base(null)
        {
            var timeFrom = DateTime.Now.AddDays(6);
            var timeTo = DateTime.Now.AddDays(7);
            var dal = new DataAccessBL();
            this.visits = dal.GetVisitsForTimespan(timeFrom, timeTo);
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public override bool Send()
        {
            bool result = false;
            var dal = new DataAccessBL();

            foreach (var visit in this.visits)
            {
                var userEmail = dal.GetUserMembership(visit.Order.OwnerUser).LoweredEmail;
                result = SendEmail(userEmail, string.Empty, this.subject, this.body, "Пользователю за 5 дней");
                this.Log(result ? "Доставлено" : "Ошибка доставки", userEmail, this.body);
            }
            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя об прибытии за 5 дней",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю за 5 дней",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

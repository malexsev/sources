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

    public class PacientBirthdayEmailNotification : BaseNotification
    {
        private IEnumerable<Pacient> pacients;
        private IEnumerable<Child> childs;
        private string subject;
        private string body;

        public PacientBirthdayEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();

            this.subject = dal.GetSettingByCode("EmailTemplateBirthdaySubject").Value;
            this.body = dal.GetSettingByCode("EmailTemplateBirthdayBody").Value;

            this.pacients = dal.GetPacients().Where(x => x.Visits.Any()
                && x.BirthDate.HasValue
                && x.BirthDate.Value.Month == DateTime.Today.Month
                && x.BirthDate.Value.Day == DateTime.Today.Day);
            this.childs = dal.GetChilds().Where(x => x.Birthday.Month == DateTime.Today.Month
                && x.Birthday.Day == DateTime.Today.Day);
        }

        public override bool Send()
        {
            bool result = false;
            var dal = new DataAccessBL();

            foreach (var pacient in this.pacients)
            {
                var userEmail = dal.GetUserMembership(pacient.Visits.First().Order.OwnerUser).LoweredEmail;
                var text = this.body.Replace("{0}", pacient.Name);
                result = SendEmail(userEmail, string.Empty, this.subject, text, "Поздравление пациента");
                this.Log(result ? "Доставлено" : "Ошибка доставки", userEmail, "EMail Поздравление пациента", text);
            }
            foreach (var child in this.childs)
            {
                var userEmail = dal.GetUserMembership(child.OwnerUser).LoweredEmail;
                var text = this.body.Replace("{0}", child.Name);
                result = SendEmail(userEmail, string.Empty, this.subject, text, "Поздравление НД");
                this.Log(result ? "Доставлено" : "Ошибка доставки", userEmail, "EMail Поздравление НД", text);
            }
            return result;
        }

        private void Log(string result, string recipient, string name, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Поздравление с днём рождения",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = name,
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

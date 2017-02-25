namespace Cure.Notification
{
    using System;
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

    public class SomeBirthdaysEmailNotification : BaseNotification
    {
        private readonly Setting settingAdminsEmails;
        private readonly Setting settingAdminsEmailCopy;
        private readonly Setting settingIsNotify;
        private readonly string subject;
        private readonly string body;
        private const string subjectTemplate = "День рождения лечащихся пациентов";
        private const string bodyTemplate = "Среди клиентов, находящихся на лечении завтра празднуют дни рождения:<br /><br />" +
            @"<table border='1'><tr><th>Клиника</th><th>Роль</th><th>Дата рождения</th><th>Имя</th><th>Исполняется (лет)</th></tr>{0}</table>";
        private const string lineTemplate = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>"; 


        public SomeBirthdaysEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            try
            {
                var dal = new DataAccessBL();

                this.settingAdminsEmails = dal.GetSettingByCode("AdminsEmails");
                this.settingAdminsEmailCopy = dal.GetSettingByCode("AdminsEmailCopy");
                this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsBirthdaysEmail");
                this.subject = subjectTemplate;
                DateTime birthday = DateTime.Today.AddDays(1);

                var pacients =
                    dal.GetOrders()
                        .Where(x => x.StatusId == 8 && x.Visits.Any(v => v.Pacient.BirthDate.HasValue && v.Pacient.BirthDate.Value.Month == birthday.Month && v.Pacient.BirthDate.Value.Day == birthday.Day))
                        .Select(x => x.Visits.Where(v => v.Pacient.BirthDate.HasValue && v.Pacient.BirthDate.Value.Month == birthday.Month && v.Pacient.BirthDate.Value.Day == birthday.Day).FirstOrDefault().Pacient);
                var sputniks =
                    dal.GetOrders()
                        .Where(x => x.StatusId == 8 && x.Sputniks.Any(s => s.BirthDate.HasValue && s.BirthDate.Value.Month == birthday.Month && s.BirthDate.Value.Day == birthday.Day))
                        .Select(x => x.Sputniks.FirstOrDefault(s => s.BirthDate.HasValue && s.BirthDate.Value.Month == birthday.Month && s.BirthDate.Value.Day == birthday.Day));

                string pacientsLines = "";
                foreach (var pacient in pacients)
                {
                    pacientsLines += string.Format(lineTemplate, pacient.Visits.OrderByDescending(x => x.CreateDate).ToList()[0].Order.Department.ShortName,
                        "Пациент",
                        pacient.BirthDate.Value.ToString("dd-MM-yyyy"),
                        pacient.FullName,
                        SiteUtils.GetAge(pacient.BirthDate.Value));
                }
                string sputniksLines = "";
                foreach (var sputnik in sputniks)
                {
                    sputniksLines += string.Format(lineTemplate, sputnik.Order.Department.ShortName,
                        "Сопровождающий",
                        sputnik.BirthDate.Value.ToString("dd-MM-yyyy"),
                        sputnik.Name + " " + sputnik.Familiya,
                        SiteUtils.GetAge(sputnik.BirthDate.Value));
                }
                if (string.IsNullOrEmpty(pacientsLines) && string.IsNullOrEmpty(sputniksLines))
                {
                    settingIsNotify.ValueBool = false;
                }
                this.body = string.Format(bodyTemplate, pacientsLines + sputniksLines);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                bool result = false;

                result = SendEmail(this.settingAdminsEmails.Value, this.settingAdminsEmailCopy.Value, this.subject, this.body, "Дни рождения у лечащихся");
                this.Log(result ? "Доставлено" : "Ошибка доставки", settingAdminsEmails.Value, this.body);

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение дне рождении лечащихся",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Дни рождения лечащихся",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

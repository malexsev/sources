namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class FilesChagedToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private IEnumerable<UploadLog> logs;
        private const string subjectTemplate = "Новые файлы в Личном кабинете.";
        private const string bodyTemplate =
            @"В Вашем личном кабинете произошли изменения в файлах.<br />" +
            @"Бфли загружены следующие файлы:<br /><br />" +
            @"{0}<br /><br /> " +
            @"Зайдите на сайт <a href='http://dcp-china.ru'>http://dcp-china.ru</a>, проверьте страницу Мои файлы."; // 0 - Список файлов

        public FilesChagedToUserEmailNotification(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.logs = dal.GetUnReportedLogs();
            this.subject = subjectTemplate;
            this.body = bodyTemplate;
        }

        public override bool Send()
        {
            bool result = false;

            if (this.logs.Any())
            {
                var dal = new DataAccessBL();
                foreach (var username in this.logs.GroupBy(x => x.Username))
                {
                    IGrouping<string, UploadLog> group = username;
                    string fileList = logs.Where(x => x.Username == @group.Key).Aggregate("", (current, log) => current + (log.FileName + "<br />"));

                    //var user 

                    //SendEmail(username.LoweredEmail, string.Empty, this.subject, text, "Опопвещение за 60 дней до заезда");
                    //this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail, text);

                }

                result = true;
            }


            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение за 60 дней до заезда",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Опопвещение за 60 дней до заезда",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

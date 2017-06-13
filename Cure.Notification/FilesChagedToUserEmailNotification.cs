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
            @"Документы<br /><br />" +
            @"{0}<br /><br /> " +
            @"Подготовлены и отправлены в Личный кабинет в раздел Мои файлы.<br /><br />" +
            @"пройдите по ссылке <a href='http://dcp-china.ru/cabinet/files'>http://dcp-china.ru/cabinet/files</a><br />" +
            "<img src='http://lk.dcp-china.ru/content/images/files.png'><br />" +
            @"Пожалуйста, проверьте правильность всех паспортных данных, период лечения, оформление.<br /><br />" + 
            @"В случае обнаружения ошибки сразу сообщите нам ответным письмом.<br /> "; // 0 - Список файлов

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

                    var user = dal.GetUserMembership(username.Key);
                    if (user != null)
                    {
                        result = SendEmail(user.LoweredEmail, string.Empty, this.subject,
                            string.Format(this.body, fileList), "Новые файлы в Личном кабинете");
                        this.Log(result ? "Доставлено" : "Ошибка доставки", user.LoweredEmail, fileList);
                    }
                    else
                    {
                        this.Log("Ошибка доставки", username.Key, fileList);
                    }
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
                Description = "Файлы в Личном кабинете",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Файлы в Личном кабинете",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

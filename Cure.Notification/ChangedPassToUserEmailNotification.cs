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

    public class ChangedPassToUserEmailNotification : BaseNotification
    {
        private ViewUserMembership user;
        private string subject;
        private string body;
        private const string subjectTemplate = "Изменение пароля на сайте www.dcp-china.ru"; //0 - Логин, 1 - Электронная почта
        private const string bodyTemplate = "Пароль изменён.<br />"
            + @"Вы изменили пароль в «Личном кабинете пациента» на сайте www.dcp-china.ru<br /><br />"
            + @"<b>Ваши новые учетные данные:</b><br />"
            + @"Логин: <b>{0}</b><br />"
            + @"Новый пароль: <b>{1}</b><br /><br />"
            + @"Вы уже можете заполнить Заявку на лечение.<br />"
            + @"Вам доступны следующие услуги:"
            + @"<ul>"
            + @"<li>Заполнение Заявки на лечение</li>"
            + @"<li>Заполнение и редактирование профиля «Мой ребенок»</li>"
            + @"<li>Редактирование Заявки на лечение на этапе заполнения</li>"
            + @"<li>Просмотр Заявки на лечение в любой момент времени</li>"
            + @"<li>Заполнение информации с билетов</li>"
            + @"<li>Редактирование даты уезда с лечения</li>"
            + @"<li>Справочная информация по подготовке к лечению</li>"
            + @"<li>Написать сообщение Администрации больницы</li>"
            + @"<li>Написать сообщение другим зарегистрированным пользователям</li>"
            + @"</ul>"
            + @"Если Вы не меняли пароль, вероятно Ваш пароль стал известен третьим лицам, Вы можете изменить пароль на новый. Держите пароль в тайне.<br /><br />"
            + @"Это автоматическое письмо, отвечать на которое не нужно!<br /><br />"
            + @"Спасибо, что Вы с нами!<br />"
            + @"C уважением, Администрация больницы <br />"
            + @"Тех. поддержка: zqcpchina@gmail.com"; //0 - Логин, 1 - Пароль


        public ChangedPassToUserEmailNotification(string username, string password, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.user = dal.GetUserMembership(username);
            this.subject = string.Format(subjectTemplate);
            this.body = string.Format(bodyTemplate, user.UserName, password);
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Смена пароля");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail, this.body);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя о смене его пароля",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Смена пароля",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

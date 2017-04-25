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

    public class RegistrationToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private const string subjectTemplate = "Вы успешно зарегистрировались в «Личном кабинете пациента» на сайте www.dcp-china.ru";
        private const string bodyTemplate = "Поздравляем!<br />"
            + @"Вы успешно зарегистрировались в «Личном кабинете пациента» на сайте www.dcp-china.ru<br /><br />"
            + @"Ваша учетная запись создана.<br />"
            + @"Для того что бы войти в Личный кабинет пациента перейдите по <a href='http://www.lk.dcp-china.ru'>ссылке</a><br />"
            + @"Если ссылка не открывается, скопируйте ее и вставьте в адресную строку браузера.<br /><br />"
            + @"<b>Ваши учетные данные:</b><br />"
            + @"Логин: <b>{0}</b><br />"
            + @"Пароль: <b>{1}</b><br /><br />"
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
            + @"</ul><br /><br />"
            + @"Это автоматическое письмо, отвечать на которое не нужно!"; //0 - Логин, 1 - Пароль

        public RegistrationToUserEmailNotification(string username, string password, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.user = dal.GetUserMembership(username);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate, this.user.UserName, password);
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю о регистрации");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail, this.body);

            return result;
        }

        private void Log(string result, string recipient, string text)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя о его регистрации",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Регистрация пользователю",
                Result = result,
                Type = "EMail",
                Text = text
            };

            SaveLog(notify);
        }
    }
}

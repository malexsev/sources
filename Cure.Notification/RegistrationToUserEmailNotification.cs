namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class RegistrationToUserEmailNotification : INotification
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
            + @"Логин: <b>{0}</b><br /><br />"
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
            + @"Если Вы считаете, что данное сообщение отправлено Вам ошибочно, просто проигнорируйте его.<br /><br />"
            + @"Это автоматическое письмо, отвечать на которое не нужно!<br /><br />"
            + @"Спасибо, что Вы с нами!<br />"
            + @"C уважением, Администрация больницы <br />"
            + @"Тех. поддержка: zqcpchina@gmail.com"; //0 - Логин

        public RegistrationToUserEmailNotification(string username)
        {
            var dal = new DataAccessBL();
            this.user = dal.GetUserMembership(username);
            this.subject = subjectTemplate;
            this.body = string.Format(bodyTemplate, this.user.UserName);
        }

        public bool Send()
        {
            bool result = false;

            result = EmailUtils.SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Пользователю о регистрации");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail);

            return result;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Опопвещение пользователя о его регистрации",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Регистрация пользователю",
                Result = result,
                Type = "EMail"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

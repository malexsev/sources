namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class Before2HoursNotification : INotification
    {
        private const string NoPacients = "(без пациента)";
        private IEnumerable<Visit> visits;
        private Setting settingAdmins;
        private Setting settingIsNotify;

        public Before2HoursNotification()
        {
            var timeFrom = DateTime.Now.AddHours(6);
            var timeTo = DateTime.Now.AddHours(7);
            var dal = new DataAccessBL();
            this.visits = dal.GetVisitsForTimespan(timeFrom, timeTo);
            this.settingAdmins = dal.GetSettingByCode("AdminsPhones");
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsBefore2HoursArrival");
        }

        public bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                foreach (var visit in this.visits)
                {
                    string pacientFullName = (visit.Pacient.FullName ?? NoPacients);
                    string text = string.Format("Прибытие за 2 часа: {0}", pacientFullName);
                    string[] recipients = settingAdmins.Value.Split(Convert.ToChar(","));
                    bool result = false;

                    foreach (var recipient in recipients)
                    {
                        result = SmsUtils.SendSms(recipient, text, "Прибытие за 2 часа");
                        this.Log(result ? "Доставлено" : "Ошибка доставки", pacientFullName);
                    }

                    return result;
                }
            }

            return false;
        }

        private void Log(string result, string pacientFullName)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о прибытии за 2-3 часа",
                Contacts = settingAdmins.Value,
                Details = string.Format("Пациент: {0}", pacientFullName),
                ExecutionDate = DateTime.Now,
                Name = "SMS прибытие за 2 часа",
                Result = result,
                Type = "SMS"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

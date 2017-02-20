namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class Before24HoursNotification : BaseNotification
    {
        private const string NoPacients = "(без пациента)";
        private IEnumerable<Visit> visits;
        private Setting settingAdmins;
        private Setting settingIsNotify;

        public Before24HoursNotification()
            : base(null)
        {
            var timeFrom = DateTime.Now.AddDays(2);
            var timeTo = DateTime.Now.AddDays(3);
            var dal = new DataAccessBL();
            this.visits = dal.GetVisitsForTimespan(timeFrom, timeTo);
            this.settingAdmins = dal.GetSettingByCode("AdminsPhones");
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsBefore24HoursArrival");
        }

        public override bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                foreach (var visit in this.visits)
                {
                    string pacientFullName = (visit.Pacient.FullName ?? NoPacients);
                    string text = string.Format("Прибытие за сутки: {0}", pacientFullName);
                    string[] recipients = settingAdmins.Value.Split(Convert.ToChar(","));
                    bool result = false;

                    foreach (var recipient in recipients)
                    {
                        result = SmsUtils.SendSms(recipient, text, "Прибытие за сутки");
                        this.Log(result ? "Доставлено" : "Ошибка доставки", pacientFullName);
                    }

                    return result;
                }
            }

            return false;
        }

        private void Log(string result, string pacientFullName)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о прибытии за сутки",
                Contacts = settingAdmins.Value,
                Details = string.Format("Пациент: {0}", pacientFullName),
                ExecutionDate = DateTime.Now,
                Name = "SMS Прибытие за сутки",
                Result = result,
                Type = "SMS"
            };

            SaveLog(notify);
        }
    }
}

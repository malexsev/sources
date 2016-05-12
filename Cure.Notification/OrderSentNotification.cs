namespace Cure.Notification
{
    using System;
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class OrderSentNotification : INotification
    {
        private const string NoPacients = "(без пациента)";
        private Order order;
        private Setting settingAdmins;
        private Setting settingIsNotify;
        private string pacientFullName;

        public OrderSentNotification(int orderId)
        {
            var dal = new DataAccessBL();
            this.order = dal.GetOrder(orderId);
            this.settingAdmins = dal.GetSettingByCode("AdminsPhones");
            this.settingIsNotify = dal.GetSettingByCode("IsNotifyAdminsNewOrder");
            this.pacientFullName = order.Visits.Any() ? (order.Visits.FirstOrDefault().Pacient.FullName != null ? order.Visits.FirstOrDefault().Pacient.FullName : NoPacients) : NoPacients;
        }

        public bool Send()
        {
            if (settingIsNotify.ValueBool != null && settingIsNotify.ValueBool == true)
            {
                string text = string.Format("Новая заявка: {0}", this.pacientFullName);
                string[] recipients = settingAdmins.Value.Split(Convert.ToChar(","));
                bool result = false;

                foreach (var recipient in recipients)
                {
                    result = SmsUtils.SendSms(recipient, text, "Новая заявка");
                    this.Log(result ? "Доставлено" : "Ошибка доставки", recipient);
                }

                return result;
            }

            return false;
        }

        private void Log(string result, string recipient)
        {
            var dal = new DataAccessBL();

            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Опопвещение о поступлении заявки на подпись",
                Contacts = recipient,
                Details = string.Format("Пациент: {0}", this.pacientFullName),
                ExecutionDate = DateTime.Now,
                Name = "SMS Новая заявка",
                Result = result,
                Type = "SMS"
            };

            dal.InsertNotificationLog(notify);
        }
    }
}

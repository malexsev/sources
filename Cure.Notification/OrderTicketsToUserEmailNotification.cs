﻿namespace Cure.Notification
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class OrderTicketsToUserEmailNotification : BaseNotification
    {
        private string subject;
        private string body;
        private ViewUserMembership user;
        private Order order;
        private const string subjectTemplate = "Информация о встрече в городе Юньчен";

        public OrderTicketsToUserEmailNotification(int orderId, int transferInfoId, HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.order = dal.GetOrder(orderId);
            this.user = dal.GetUserMembership(order.OwnerUser);
            this.subject = subjectTemplate;
            var info = dal.GetDepartmentTransferInfo(transferInfoId);
            this.body = info.Text;
        }

        public override bool Send()
        {
            bool result = false;

            result = SendEmail(this.user.LoweredEmail, string.Empty, this.subject, this.body, "Информация о встрече");
            this.Log(result ? "Доставлено" : "Ошибка доставки", this.user.LoweredEmail);

            return result;
        }

        private void Log(string result, string recipient)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Пользователь",
                Description = "Информирование пользователя о встрече",
                Contacts = recipient,
                Details = this.subject,
                ExecutionDate = DateTime.Now,
                Name = "EMail Пользователю о встрече",
                Result = result,
                Type = "EMail"
            };

            SaveLog(notify);
        }
    }
}
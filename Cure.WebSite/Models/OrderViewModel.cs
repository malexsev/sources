using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using DataAccess;

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Style { get; set; }

        public OrderViewModel(Order order)
        {
            this.OrderId = order.Id;
            this.GuidId = order.GuidId;
            this.Date = order.CreateDate ?? DateTime.Now;
            this.Status = GetStatusName(order.StatusId);
            this.DepartmentName = order.Department != null ? order.Department.Name : "";
            this.DateFrom = order.DateFrom;
            this.DateTo = order.DateTo;
            this.Style = string.Empty;
        }

        protected string GetStatusName(int statusId)
        {
            switch (statusId)
            {
                case 2:
                    {
                        return "Отправлена на рассмотрение";
                    }
                case 3:
                    {
                        return "На рассмотрении";
                    }
                case 4:
                    {
                        return "Отказ заявителя";
                    }
                case 5:
                    {
                        return "Отказано клиникой";
                    }
                case 6:
                    {
                        return "Заявка ободрена";
                    }
                case 7:
                    {
                        return "Куплены билеты";
                    }
                case 8:
                    {
                        return "Выполняется";
                    }
                default:
                    {
                        return "Завершён";
                    }
            }
        }
    }
}
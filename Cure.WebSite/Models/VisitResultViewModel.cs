using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using DataAccess;

    public class VisitResultViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int DaysTotal { get; set; }
        public string DepartmentName { get; set; }
        public string Results { get; set; }

        public VisitResultViewModel(Vipiska vipiska)
        {
            this.DateFrom = vipiska.Visit.Order.DateFrom;
            this.DateTo = vipiska.Visit.Order.DateTo;
            TimeSpan days = this.DateTo - this.DateFrom;
            this.DaysTotal = days.Days;
            this.DepartmentName = vipiska.Visit.Order.Department.Name;
            this.Results = vipiska.Result;
        }
    }
}
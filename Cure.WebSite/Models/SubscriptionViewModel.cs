using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    public class SubscriptionViewModel
    {
        public string Email { get; set; }
        public bool isSubscribe { get; set; }
        public bool isSuccess { get; set; }
    }
}
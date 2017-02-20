using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using Notification;

    public class FeedbackController : Controller
    {
        [HttpPost]
        public JsonResult AddFeedback(string name, string mail, string phone, string text)
        {
            var notifyEmail = new FeedbackNotification(name, mail, phone, text, Server);
            if (notifyEmail.Send())
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
    }
}
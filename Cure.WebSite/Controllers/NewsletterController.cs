using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess;
    using DataAccess.BLL;
    using Notification;

    public class NewsletterController : Controller
    {
        [HttpPost]
        public JsonResult AddNew(string email)
        {
            try
            {
                email = email.Trim().ToLower();
                var dal = new DataAccessBL();
                var exists = dal.GetNewsletter(email);
                if (exists == null)
                {
                    var entry = new Newsletter()
                        {
                            Email = email,
                            EntryDate = DateTime.Now,
                            EntryType = "Футер сайта",
                            ErrorsCount = 0,
                            SuccessCount = 0,
                            Settings = string.Empty
                        };

                    dal.InsertNewsletter(entry);
                    var notify = new SubscribedToUserEmailNotification(email, Server);
                    notify.Send();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }

                return Json("-1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
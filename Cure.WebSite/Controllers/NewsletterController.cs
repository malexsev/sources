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
                var dal = new DataAccessBL();
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

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
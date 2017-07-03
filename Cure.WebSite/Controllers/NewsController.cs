using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess.BLL;
    using Utils;

    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();
            var result = dal.GetAllActive();
            ViewBag.NewsCount = result.Count();
            return View(result);
        }

        [HttpPost]
        public PartialViewResult More(string skiprecords)
        {
            var dal = new DataAccessBL();
            int skipRecords = SiteUtils.ParseInt(skiprecords, 0);

            var result = dal.MoreNews(skipRecords).ToList();
            ViewBag.NewsCount = dal.GetAllActive().Count();

            return PartialView("_News", result);
        }
    }
}
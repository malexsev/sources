using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess.BLL;
    using Models;
    using Utils;

    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();
            var all = dal.GetAllActive().ToList();

            var result = all.Take(12).Select(x => new NewsPageModel(x, Request));
            ViewBag.NewsCount = all.Count();
            return View(result);
        }

        public ActionResult Details(string alias)
        {
            var dal = new DataAccessBL();
            var info = dal.GetNewsPage(alias);
            return View(new NewsPageModel(info, Request));
        }

        [HttpPost]
        public PartialViewResult More(string skiprecords)
        {
            var dal = new DataAccessBL();
            var skip = SiteUtils.ParseInt(skiprecords, 0);

            var all = dal.GetAllActive().ToList();
            var result = all.Skip(skip).Take(4).Select(x => new NewsPageModel(x, Request));
            ViewBag.NewsCount = all.Count();

            return PartialView("_News", result);
        }
    }
}
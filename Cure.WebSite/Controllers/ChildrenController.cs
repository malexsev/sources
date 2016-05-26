using System;
using System.Collections.Generic;
using Cure.DataAccess.BLL;
using Cure.WebSite.Models;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System.Linq;

    public class ChildrenController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();
            
            var result = dal.ViewChilds().Select(x => new ChildVisual(x)).ToList();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Regions = dal.GetRegions();
            ViewBag.Diagnozes = dal.GetExistingDiagnozs();

            return View(result);
        }
    }
}
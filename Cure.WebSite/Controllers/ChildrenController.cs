using System;
using System.Collections.Generic;
using Cure.DataAccess.BLL;
using Cure.WebSite.Models;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System.Linq;
    using System.Net;

    public class ChildrenController : Controller
    {
        [HttpPost]
        public ActionResult Index(string filtercountry, string filterregion, string filterage, string filterdiagnoze)
        {
            var dal = new DataAccessBL();

            int countryId = int.Parse(filtercountry);
            int ageOption = int.Parse(filterage);
            int diagnozeId = int.Parse(filterdiagnoze);
            int pageRecords = 0;//int.Parse(filterrecords);

            var result = dal.FilterChilds(countryId, filterregion, ageOption, diagnozeId, pageRecords).Select(x => new ChildVisual(x)).ToList();
            ViewBag.Countries = dal.GetRefCountries();

            ViewBag.Regions = countryId == 0 ? dal.GetRegions() : dal.GetRegions(countryId);
            ViewBag.Diagnozes = dal.GetExistingDiagnozs();

            return View(result);
        }

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
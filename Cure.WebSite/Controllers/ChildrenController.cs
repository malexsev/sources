﻿using Cure.DataAccess.BLL;
using Cure.WebSite.Models;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System.Linq;

    public class ChildrenController : Controller
    {
        public ActionResult Detail(string id)
        {
            var dal = new DataAccessBL();
            int childId = int.Parse(id);

            return View(new ChildVisualDetailed(dal.ViewChild(childId), dal.GetChildHideFiles(childId), dal.GetChildAvaFile(childId)));
        }

        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            var result = dal.FilterChilds(0, "0", 0, 0, 0).Select(x => new ChildVisual(x, dal.GetChildAvaFile(x.Id))).ToList();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Regions = dal.GetRegions();
            ViewBag.Diagnozes = dal.GetExistingDiagnozs();
            ViewBag.TotalCount = dal.CountChilds(0, "0", 0, 0);

            return View(result);
        }

        [HttpPost]
        public ActionResult Index(string filtercountry, string filterregion, string filterage, string filterdiagnoze, string skiprecords)
        {
            var dal = new DataAccessBL();

            int countryId = int.Parse(filtercountry);
            int ageOption = int.Parse(filterage);
            int diagnozeId = int.Parse(filterdiagnoze);
            int skipRecords = int.Parse(skiprecords);

            var result = dal.FilterChilds(countryId, filterregion, ageOption, diagnozeId, skipRecords).Select(x => new ChildVisual(x, dal.GetChildAvaFile(x.Id))).ToList();
            ViewBag.Countries = dal.GetRefCountries();

            ViewBag.Regions = countryId == 0 ? dal.GetRegions() : dal.GetRegions(countryId);
            ViewBag.Diagnozes = dal.GetExistingDiagnozs();
            ViewBag.TotalCount = dal.CountChilds(countryId, filterregion, ageOption, diagnozeId);

            return View(result);
        }


        [HttpPost]
        public PartialViewResult More(string filtercountry, string filterregion, string filterage, string filterdiagnoze, string skiprecords)
        {
            var dal = new DataAccessBL();

            int countryId = int.Parse(filtercountry);
            int ageOption = int.Parse(filterage);
            int diagnozeId = int.Parse(filterdiagnoze);
            int skipRecords = int.Parse(skiprecords);

            var result = dal.FilterChilds(countryId, filterregion, ageOption, diagnozeId, skipRecords).Select(x => new ChildVisual(x, dal.GetChildAvaFile(x.Id))).ToList();

            return PartialView("_Children", result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System.Data.Objects;
    using DataAccess.BLL;
    using Models;
    using Utils;

    public class BiblioController : Controller
    {
        public ActionResult Index(string filterSubjectId, string filterBiblioId, string filterSearch)
        {
            var dal = new DataAccessBL();
            var all = dal.GetAllBiblioPageActive().ToList();
            var filtered = all.Where(x => (string.IsNullOrEmpty(filterSubjectId) || filterSubjectId == "0" || x.SubjectID == int.Parse(filterSubjectId))
                && (string.IsNullOrEmpty(filterBiblioId) || filterBiblioId == "0" || x.Id == int.Parse(filterBiblioId))
                && (string.IsNullOrEmpty(filterSearch) || x.Name.ToLower().Contains(filterSearch.ToLower()) || x.Subtitle.ToLower().Contains(filterSearch.ToLower()) || x.Title.ToLower().Contains(filterSearch.ToLower()) || x.Text.ToLower().Contains(filterSearch.ToLower()))).ToList();

            var result = filtered.Take(40).Select(x => new BiblioPageModel(x, Request));
            ViewBag.Biblios = all;
            ViewBag.BiblioSubjects = dal.GetBiblioSubjects();
            ViewBag.TotalCount = all.Count();
            return View(result);
        }

        public ActionResult Details(string alias)
        {
            var dal = new DataAccessBL();
            var info = dal.GetBiblioPage(alias);

            ViewBag.SimilarBiblios = dal.GetAllBiblioPageActive().Where(x => x.SubjectID == info.SubjectID && x.Id != info.Id).Take(3).ToList().Select(x => new BiblioPageModel(x, Request));

            return View(new BiblioPageModel(info, Request));
        }

        [HttpPost]
        public PartialViewResult More(string filterSubjectId, string filterBiblioId, string filterSearch, string skiprecords)
        {
            var dal = new DataAccessBL();
            var skip = SiteUtils.ParseInt(skiprecords, 0);

            var all = dal.GetAllBiblioPageActive().ToList();
            var filtered = all.Where(x => (string.IsNullOrEmpty(filterSubjectId) || filterSubjectId == "0" || x.SubjectID == int.Parse(filterSubjectId))
                && (string.IsNullOrEmpty(filterBiblioId) || filterBiblioId == "0" || x.Id == int.Parse(filterBiblioId))
                && (string.IsNullOrEmpty(filterSearch) || x.Name.Contains(filterSearch) || x.Subtitle.Contains(filterSearch) || x.Title.Contains(filterSearch) || x.Text.Contains(filterSearch))).ToList();

            var result = filtered.Skip(skip).Take(4).Select(x => new BiblioPageModel(x, Request));
            ViewBag.TotalCount = all.Count();

            return PartialView("_Biblio", 0);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess;
    using DataAccess.BLL;
    using Utils;

    public class MensionController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            ViewBag.DepSubjects = dal.GetDepSubject();
            ViewBag.MensionsCount = dal.CountMensions(0);
            ViewBag.ViewMensions = dal.ViewMensions(0, 0);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string mensionfilter)
        {
            var dal = new DataAccessBL();
            int filterId = SiteUtils.ParseInt(mensionfilter, 0);

            ViewBag.DepSubjects = dal.GetDepSubject();
            ViewBag.MensionsCount = dal.CountMensions(filterId);
            ViewBag.ViewMensions = dal.ViewMensions(filterId, 0);

            return View();
        }

        [HttpPost]
        public JsonResult AddNew(int subject, string text)
        {
            var dal = new DataAccessBL();
            var view = dal.ViewChild(User.Identity.Name);
            var subjects = dal.GetDepSubject();
            if (view != null)
            {
                var subjText = "Работа сервиса, организация лечения";
                int? depId = null;
                var subj = subjects.FirstOrDefault(x => x.Id == subject);
                if (subj != null && subj.Id != -1)
                {
                    subjText = subj.Name;
                    depId = subj.Id;
                }
                var mension = new Mension()
                {
                    CopySubject = subjText,
                    CopyUserLocation = string.Format("{0}, {1}", view.CountryName, view.Region),
                    CopyUserName = view.ContactName,
                    CreatedDate = DateTime.Now,
                    DepartmentId = depId,
                    IsActive = false,
                    OwnerUser = User.Identity.Name,
                    Text = text,
                    SortOrder = -1
                };

                dal.InsertMension(mension);

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult More(string filter, string skiprecords)
        {
            var dal = new DataAccessBL();
            int filterId = SiteUtils.ParseInt(filter, 0);
            int skipRecords = SiteUtils.ParseInt(skiprecords, 0);

            ViewBag.MensionsCount = dal.CountMensions(filterId);
            ViewBag.ViewMensions = dal.ViewMensions(filterId, skipRecords).ToList();

            return PartialView("_Mensions");
        }
    }
}
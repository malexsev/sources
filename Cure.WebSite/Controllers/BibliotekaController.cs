using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    public class BibliotekaController : Controller
    {
        // GET: China
        public ActionResult Procedures()
        {
            return View();
        }

        public ActionResult Tests()
        {
            return View();
        }

        public ActionResult Metod()
        {
            return View();
        }
    }
}
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
        public ActionResult Lekarstvassoboy()
        {
            return View();
        }

        public ActionResult Lekarstvakitay()
        {
            return View();
        }

        public ActionResult Postsnews()
        {
            return View();
        }
    }
}
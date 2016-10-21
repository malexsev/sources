using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System;
    using System.Linq;
    using DataAccess.BLL;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            ViewBag.ChildrenHome = dal.FilterChilds(0, "0", 0, 0, 0, 8);
            ViewBag.MensionsHome = dal.GetTopMensions();
            ViewBag.Departments = dal.GetDepartments();
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Price()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            return View();
        }

        public ActionResult Procedure()
        {
            return View();
        }

        public ActionResult Medicine()
        {
            return View();
        }

        private decimal GetRate(string currency)
        {
            var dal = new DataAccessBL();
            var rate = dal.GetCurrencyRates().FirstOrDefault(x => x.CurrencyFrom == currency);
            if (rate != null)
            {
                return Math.Round(rate.Rate, 2);
            }
            else
            {
                return 0;
            }
        }
    }
}
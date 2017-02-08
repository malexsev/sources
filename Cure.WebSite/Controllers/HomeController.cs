using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;

    public class HomeController : Controller
    {
        public ActionResult SimpleStyles()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        [OutputCache (Duration = 60)]
        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            ViewBag.ChildrenHome = dal.FilterChilds(0, "0", 0, 0, 0, 8);
            ViewBag.MensionsHome = dal.GetTopMensions();
            ViewBag.Departments = dal.GetActiveDepartments();
            var weathers = new List<Weather> { dal.GetWeatherByCity(33991), dal.GetWeatherByCity(36870), dal.GetWeatherByCity(50207) };
            ViewBag.Weathers = weathers;
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Partnership()
        {
            return View();
        }

        public ActionResult Careers()
        {
            return View();
        }

        public ActionResult Pravo()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Price()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
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
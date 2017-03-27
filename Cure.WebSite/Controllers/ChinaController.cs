using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess.BLL;

    public class ChinaController : Controller
    {
        public ActionResult Price()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Visa()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Dobratsa()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Priglashenie()
        {
            return View();
        }

        public ActionResult Support()
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
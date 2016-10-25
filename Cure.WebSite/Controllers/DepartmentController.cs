using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess.BLL;

    public class DepartmentController : Controller
    {
        public ActionResult Sevastopol()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
            return View();
        }
        public ActionResult Yancheng1()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
            return View();
        }
        public ActionResult Yancheng2()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
            return View();
        }
        public ActionResult Almaata()
        {
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            ViewBag.CurrencyRateUSD = GetRate("USD");
            ViewBag.CurrencyRateKZT = GetRate("KZT");
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
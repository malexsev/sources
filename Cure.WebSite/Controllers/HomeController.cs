using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;
    using Models;
    using Notification;
    using Utils;

    public class HomeController : Controller
    {
        public ActionResult Subscription(string subscribe, string unsubscribe)
        {
            var model = new SubscriptionViewModel();
            if (!string.IsNullOrEmpty(subscribe) && string.IsNullOrEmpty(unsubscribe))
            {
                var dal = new DataAccessBL();
                model.Email = subscribe;
                model.isSubscribe = true;
                model.isSuccess = dal.Subscribe(subscribe);

                var notify = new SubscribedToUserEmailNotification(model.Email, Server);
                notify.Send();
            }
            else if (!string.IsNullOrEmpty(unsubscribe) && string.IsNullOrEmpty(subscribe))
            {
                var dal = new DataAccessBL();
                model.Email = unsubscribe;
                model.isSubscribe = false;
                model.isSuccess = dal.UnSubscribe(unsubscribe);
            }
            else
            {
                model.isSuccess = false;
            }

            return View(model);
        }

        public ActionResult SimpleStyles()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            ViewBag.ChildrenHome = dal.FilterChilds(0, "0", 0, 0, 0, 8);
            ViewBag.MensionsHome = dal.GetTopMensions();
            ViewBag.Departments = dal.GetActiveDepartments();
            
            ViewBag.Weathers = WeatherUtils.GetWeathers();
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

        public ActionResult Procedure()
        {
            return View();
        }

        public ActionResult Medicine()
        {
            return View();
        }
    }
}
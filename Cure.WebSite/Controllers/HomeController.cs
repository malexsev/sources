using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
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

        public ActionResult Rules(string temp)
        {
            Order order = TryParseOrder(temp);
            var sputnikNameTemplate = "(Ваше ФИО)";
            if (order != null && order.Sputniks.Any())
            {
                TempData.Add("UserGuid", order.GuidId.ToString());
                var sputnik = order.Sputniks.FirstOrDefault(x => x.IsPrimary) ?? order.Sputniks.First();
                sputnikNameTemplate = string.Format("{0} {1}", sputnik.Familiya, sputnik.Name);
            }

            ViewBag.SputnikName = sputnikNameTemplate;
            return View();
        }

        [HttpPost]
        public ActionResult RulesAgree(string agreeRadioButton)
        {
            if (agreeRadioButton == "True" && !string.IsNullOrEmpty(TempData["UserGuid"].ToString()))
            {
                Order order = TryParseOrder(TempData["UserGuid"].ToString());
                if (order != null && order.Sputniks.Any())
                {
                    var sputnik = order.Sputniks.FirstOrDefault(x => x.IsPrimary) ?? order.Sputniks.First();
                    //Генерация файла правил и отправка его в папку пользоватлебля.
                    var client = new WebClient();
                    client.OpenRead(string.Format("{0}?orderid={1}&sputnik={2}", ConfigurationManager.AppSettings["AgreeRulesUrl"], order.Id, string.Format("{0} {1}", sputnik.Familiya, sputnik.Name)));
                }
            }
            return View();
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

        private static Order TryParseOrder(string guid)
        {
            Guid orderGuid = Guid.Empty;
            if (Guid.TryParse(guid, out orderGuid))
            {
                var dal = new DataAccessBL();
                return dal.GetOrders().FirstOrDefault(o => o.GuidId == orderGuid);
            }
            return null;
        }
    }
}
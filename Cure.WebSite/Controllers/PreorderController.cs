using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using System;
    using DataAccess.BLL;
    using Utils;

    public class PreorderController : Controller
    {
        private const string CalendarCulture = "ru-RU";

        public ActionResult Index()
        {
            var dal = new DataAccessBL();
            ViewBag.Departments = dal.GetDepartments();
            ViewBag.PlacesAvailable = true;
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string department, string datefrom, string dateto, string pacients, string sputniks)
        {
            int departmentId = SiteUtils.ParseInt(department, 0);
            if (departmentId != 0)
            {
                Session["OrderDepartmentId"] = departmentId;
            }

            DateTime dateFrom = SiteUtils.ParseDate(datefrom, DateTime.Today, CalendarCulture);
            if (dateFrom >= DateTime.Today.AddDays(7))
            {
                Session["OrderDateFrom"] = dateFrom;
            }

            DateTime dateTo = SiteUtils.ParseDate(dateto, DateTime.Today, CalendarCulture);
            if (dateFrom < dateTo)
            {
                Session["OrderDateTo"] = dateTo;
            }

            int visitsCount = SiteUtils.ParseInt(pacients, 0);
            if (visitsCount > 0 && visitsCount <= 2)
            {
                Session["OrderPacientCount"] = visitsCount;
            }

            int sputniksCount = SiteUtils.ParseInt(sputniks, 0);
            if (sputniksCount > 0 || sputniksCount <= 4)
            {
                Session["OrderSputnikCount"] = sputniksCount;
            }

            var dal = new DataAccessBL();
            ViewBag.Departments = dal.GetDepartments();
            ViewBag.PlacesAvailable = !((departmentId != 0 && dateFrom != DateTime.Today && dateTo != DateTime.Today) && dal.CheckStopVisit(departmentId, dateFrom, dateTo));
            
            return View();
        }
    }
}
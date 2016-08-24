using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    using DataAccess.BLL;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dal = new DataAccessBL();

            ViewBag.Departments = dal.GetDepartments();
            return View();
        }
    }
}
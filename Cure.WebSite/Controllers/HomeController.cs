using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
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
    }
}
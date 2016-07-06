using Cure.DataAccess.BLL;
using Cure.WebSite.Models;
using System.Web.Mvc;

namespace Cure.WebSite.Controllers
{
    public class CabinetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyPage()
        {
            var dal = new DataAccessBL();
            var child = dal.ViewChild(User.Identity.Name);

            return View(new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id)));
        }
    }
}
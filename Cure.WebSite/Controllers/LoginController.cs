using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cure.WebSite.Controllers
{
    using System.Security.Cryptography;
    using DataAccess.BLL;
    using Notification;
    using Utils;

    public class LoginController : Controller
    {
        private const int MaxCountRenewPassword = 3;

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult ChangePass(string currentpass, string regpass, string passtwice)
        {
            string loginname = SiteUtils.GetCurrentUserName();
            if (!string.IsNullOrEmpty(loginname))
            {
                var user = Membership.GetUser(loginname);
                if (user != null && user.ChangePassword(currentpass, regpass))
                {
                    var notificationToUser = new ChangedPassToUserEmailNotification(loginname, regpass);
                    notificationToUser.Send();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(string loginname, string loginpass)
        {
            bool isValid = IsValid(loginname, loginpass);
            if (isValid)
            {
                Session.Abandon();
                ActivateUser(loginname);
                FormsAuthentication.SetAuthCookie(loginname, false);
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Recovery(string remindinp)
        {
            var userName = Membership.GetUserNameByEmail(remindinp);
            var user = Membership.GetUser(userName ?? remindinp);

            bool res = user != null;
            if (res)
            {
                var notification = new RecoveryToUserEmailNotification(user.UserName, user.ResetPassword());
                notification.Send();
            }
            return Json(res ? "1" : "0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Register(string regname, string regmail, string regpass, string passtwice)
        {
            MembershipUser usrInfo = Membership.GetUser(regname);
            if (usrInfo != null)
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
            var username = Membership.GetUserNameByEmail(regmail);
            if (username != null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

            MembershipUser user = Membership.CreateUser(regname, regpass, regmail);
            var notification = new UserRegisteredEmailNotification(regname);
            notification.Send();
            var notificationToUser = new RegistrationToUserEmailNotification(regname, regpass);
            notificationToUser.Send();
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        private bool IsValid(string login, string password)
        {
            var dal = new DataAccessBL();
            var user = dal.GetUserMembership(login);
            Session["UserpicUrl"] = null;
            if (user != null)
            {
                return Membership.ValidateUser(login, password);
            }
            return false;
        }

        private void ActivateUser(string login)
        {
            var user = Membership.GetUser(login);
            if (user != null && !user.IsApproved)
            {
                user.IsApproved = true;
                Membership.UpdateUser(user);
            }
        }
    }
}
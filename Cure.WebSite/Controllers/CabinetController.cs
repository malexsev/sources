using Cure.DataAccess.BLL;
using Cure.WebSite.Models;
using System.Web.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Drawing;

namespace Cure.WebSite.Controllers
{
    using System.Configuration;
    using DataAccess;
    using Utils;

    public class CabinetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Messages()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult MyPage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();
            var child = dal.ViewChild(User.Identity.Name);
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.Operators = dal.GetRefOperators(child.CountryId);
            ViewBag.CountryBanks = dal.GetRefBanks(child.FinCountryId ?? child.CountryId);

            ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
            return View(ViewBag.Profile);
        }

        [HttpPost]
        public PartialViewResult GaleryAction(string pictodelete, string pictohideuphide)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();
                var child = dal.ViewChild(User.Identity.Name);

                if (!string.IsNullOrEmpty(pictodelete))
                {
                    var visual = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
                    visual.DeletePhoto(pictodelete);
                    dal.DeleteChildHideFile(child.Id, pictodelete);
                }
                else if (!string.IsNullOrEmpty(pictohideuphide))
                {
                    if (dal.CheckChildHideFile(child.Id, pictohideuphide))
                    {
                        dal.DeleteChildHideFile(
                            dal.GetChildHideFiles(child.Id).FirstOrDefault(x => x.FileName == pictohideuphide));
                    }
                    else
                    {
                        dal.InsertChildHideFile(new ChildHideFile()
                        {
                            FileName = pictohideuphide,
                            ChildId = child.Id,
                            HideDate = DateTime.Now
                        });
                    }
                }

                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
            }
            return PartialView("_CabinetGaleryTales", ViewBag.Profile);
        }

        [HttpPost]
        public PartialViewResult DocumentsAction(string doctodelete, string doctohideuphide)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();
                var child = dal.ViewChild(User.Identity.Name);

                if (!string.IsNullOrEmpty(doctodelete))
                {
                    var visual = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
                    visual.DeleteDoc(doctodelete);
                    dal.DeleteChildHideFile(child.Id, doctodelete);
                }
                else if (!string.IsNullOrEmpty(doctohideuphide))
                {
                    if (dal.CheckChildHideFile(child.Id, doctohideuphide))
                    {
                        dal.DeleteChildHideFile(
                            dal.GetChildHideFiles(child.Id).FirstOrDefault(x => x.FileName == doctohideuphide));
                    }
                    else
                    {
                        dal.InsertChildHideFile(new ChildHideFile()
                        {
                            FileName = doctohideuphide,
                            ChildId = child.Id,
                            HideDate = DateTime.Now
                        });
                    }
                }

                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
            }
            return PartialView("_CabinetDocumentsTales", ViewBag.Profile);
        }

        [HttpPost]
        public PartialViewResult UploadPhoto()
        {
            if (User.Identity.IsAuthenticated)
            {
                UploadFile(false);

                var dal = new DataAccessBL();
                var child = dal.ViewChild(User.Identity.Name);
                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
                return PartialView("_CabinetGaleryTales", ViewBag.Profile);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_CabinetGaleryTales");
            }
        }

        [HttpPost]
        public PartialViewResult UploadDoc()
        {
            if (User.Identity.IsAuthenticated)
            {
                UploadDocFile();

                var dal = new DataAccessBL();
                var child = dal.ViewChild(User.Identity.Name);
                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id));
                return PartialView("_CabinetDocumentsTales", ViewBag.Profile);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_CabinetDocumentsTales");
            }
        }

        [HttpPost]
        public JsonResult SaveTab1(string childname, string birthday, string country, string region, string diagnoz)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    int countryId = int.Parse(country);
                    DateTime birthDate = DateTime.ParseExact(birthday, "dd.MM.yyyy", new CultureInfo("ru-RU"));

                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    child.Name = childname;
                    child.Birthday = birthDate;
                    child.CountryId = countryId;
                    child.Region = region;
                    child.Diagnoz = diagnoz;

                    dal.UpdateChild(child);
                    this.UpdateIsActive(ref child, ref dal);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadAva()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    UploadFile();
                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Upload failed");
                }

                return Json("File uploaded successfully");
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public JsonResult SaveTab2(string contactname, string rodstvo, string email, string telephone)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    int rodstvoId = int.Parse(rodstvo);

                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    child.ContactName = contactname;
                    child.ContactRodstvoId = rodstvoId;
                    child.ContactEmail = email;
                    child.ContactPhone = telephone;

                    dal.UpdateChild(child);
                    this.UpdateIsActive(ref child, ref dal);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public JsonResult SaveTab3(string socialok, string socialvk, string socialmm, string socialfb, string socialyoutube)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    child.SocialOk = socialok;
                    child.SocialVk = socialvk;
                    child.SocialMm = socialmm;
                    child.SocialFb = socialfb;
                    child.SocialYoutube = socialyoutube;

                    dal.UpdateChild(child);
                    this.UpdateIsActive(ref child, ref dal);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public JsonResult SaveTab4(string webmoney
            , string webmoney2
            , string webmoney3
            , string yandexmoney
            , string kiwi
            , string finoperator
            , string fintelephone
            , string fincountry
            , string bank
            , string bankother
            , string cardnumber
            , string cardname)
        {
            if (!User.Identity.IsAuthenticated)
            {
                try
                {
                    int operatorId;
                    int.TryParse(finoperator, out operatorId);

                    int countryId;
                    int.TryParse(fincountry, out countryId);

                    int bankId;
                    int.TryParse(bank, out bankId);

                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    child.FinWebmoney = webmoney;
                    child.FinWebmoney2 = webmoney2;
                    child.FinWebmoney3 = webmoney3;
                    child.FinYandexMoney = yandexmoney;
                    child.FinKiwi = kiwi;
                    if (operatorId == 0)
                    {
                        child.FinOperatorId = null;
                    }
                    else
                    {
                        child.FinOperatorId = operatorId;
                    }
                    child.FinPhoneNumber = fintelephone;
                    if (countryId == 0)
                    {
                        child.FinCountryId = null;
                    }
                    else
                    {
                        child.FinCountryId = countryId;
                    }
                    if (bankId == 0)
                    {
                        child.FinBankId = null;
                    }
                    else
                    {
                        child.FinBankId = bankId;
                    }
                    child.FinBankOther = bankother;
                    child.FinCardNumber = cardnumber;
                    child.FinCardName = cardname;

                    dal.UpdateChild(child);
                    this.UpdateIsActive(ref child, ref dal);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public JsonResult RefreshBanks(string fincountry)
        {
            int fincountryId = int.Parse(fincountry);

            var dal = new DataAccessBL();
            var banks = dal.GetRefBanks(fincountryId);

            return Json(banks.Select(x => new { Id = x.Id, Name = x.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Balance()
        {
            return View();
        }

        private void UpdateIsActive(ref Child child, ref DataAccessBL dal)
        {
            var view = dal.ViewChild(child.Id);
            var visual = new ChildVisual(view, dal.GetChildAvaFile(child.Id));
            if (visual.IsActive != visual.CheckForActive())
            {
                child.IsActive = !child.IsActive;
            }
            dal.UpdateChild(child);
        }

        private void UploadFile(bool isAva = true)
        {
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);

                    const string OriginalDirectory = @"{0}{1}\";
                    const string ThumbDirectory = @"{0}{1}\Thumb\";
                    const string BigDirectory = @"{0}{1}\Big\";

                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file); // + ".jpg";

                    string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
                    string folder = string.Format(OriginalDirectory, photoLocation, view.GuidId);
                    string folderThumb = string.Format(ThumbDirectory, photoLocation, view.GuidId);
                    string folderBig = string.Format(BigDirectory, photoLocation, view.GuidId);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    if (!Directory.Exists(folderThumb))
                    {
                        Directory.CreateDirectory(folderThumb);
                    }
                    if (!Directory.Exists(folderBig))
                    {
                        Directory.CreateDirectory(folderBig);
                    }

                    var path = Path.Combine(folder, fileName);
                    string fileThumbName = Path.Combine(folderThumb, fileName);
                    string fileBigName = Path.Combine(folderBig, fileName);
                    using (Image original = Image.FromStream(stream))
                    using (Image thumb = PhotoUtils.Inscribe(original, 313, 313))
                    using (Image big = PhotoUtils.Inscribe(original, 919, 538))
                    {
                        PhotoUtils.SaveToJpeg(original, path);
                        PhotoUtils.SaveToJpeg(thumb, fileThumbName);
                        PhotoUtils.SaveToJpeg(big, fileBigName);
                    }

                    if (isAva)
                    {
                        var ava = dal.GetChildAvaFile(view.Id);
                        if (ava != null && ava.Id != 0)
                        {
                            dal.DeleteChildAvaFile(ava);
                        }
                        ava = new ChildAvaFile
                        {
                            ChangeDate = DateTime.Today,
                            ChildId = view.Id,
                            FileName = fileName
                        };
                        dal.InsertChildAvaFile(ava);
                    }

                    var child = dal.GetChild(view.Id);
                    this.UpdateIsActive(ref child, ref dal);
                }
            }
        }

        private void UploadDocFile()
        {
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);

                    const string OriginalDirectory = @"{0}{1}\";

                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file); // + ".jpg";

                    string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
                    string docsLocation = photoLocation.Replace("Upload", "Documents");
                    string folder = string.Format(OriginalDirectory, docsLocation, view.GuidId);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var path = Path.Combine(folder, fileName);
                    using (Image original = Image.FromStream(stream))
                    {
                        PhotoUtils.SaveToJpeg(original, path);
                    }
                }
            }
        }
    }
}
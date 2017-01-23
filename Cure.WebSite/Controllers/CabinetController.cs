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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using Notification;
    using Utils;

    public class CabinetController : Controller
    {
        private const string CalendarCulture = "ru-RU";

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();
            var child = dal.ViewChild(User.Identity.Name);
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.Operators = dal.GetRefOperators();
            ViewBag.CountryBanks = dal.GetRefBanks(child.FinCountryId ?? child.CountryId);
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            var weathers = new List<Weather> { dal.GetWeatherByCity(33991), dal.GetWeatherByCity(36870), dal.GetWeatherByCity(50207) };
            ViewBag.Weathers = weathers;       

            var vipiskas = dal.GetMyVipiskas(User.Identity.Name).ToList();
            ViewBag.Vipiskas = vipiskas.Select(x => new VisitResultViewModel(x));
            ViewBag.TestLevel1Val = "-";
            ViewBag.TestLevel2Val = "-";
            ViewBag.TestLevel3Val = "-";
            ViewBag.TestLevel1Desc = "";
            ViewBag.TestLevel2Desc = "";
            ViewBag.TestLevel3Desc = "";

            if (vipiskas.Any())
            {
                var vipiska = vipiskas[0];
                if (vipiska.RefGmfcsLevel != null)
                {
                    ViewBag.TestLevel1Val = vipiska.RefGmfcsLevel.Name;
                    ViewBag.TestLevel1Desc = vipiska.RefGmfcsLevel.Description;
                }
                if (vipiska.RefMacsLevel != null)
                {
                    ViewBag.TestLevel2Val = vipiska.RefMacsLevel.Name;
                    ViewBag.TestLevel2Desc = vipiska.RefMacsLevel.Description;
                }
                if (vipiska.RefCfcsLevel != null)
                {
                    ViewBag.TestLevel3Val = vipiska.RefCfcsLevel.Name;
                    ViewBag.TestLevel3Desc = vipiska.RefCfcsLevel.Description;
                }
            }

            ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id), dal.GetMyPosts(child.Id));
            return View(ViewBag.Profile);
        }

        // Сообщения
        public ActionResult Messages()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetUnreadCount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

            string username = User.Identity.Name;
            var dal = new DataAccessBL();
            dal.BringOnlineUser(username);
            int count = dal.GetUnreadCount(username);
            return Json(count.ToString(CultureInfo.InvariantCulture), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetContacts(string contact, string filter)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();
                if (contact == "undefined")
                {
                    contact = string.Empty;
                }

                var contactViews = dal.GetContacts(User.Identity.Name, SiteUtils.ParseGuid(contact), filter)
                    .Select(x => new MessagesUserModel(x, SiteUtils.ParseBool(x.IsOnline, false), 
                        SiteUtils.ParseBool(x.IsAdmin, false), 
                        x.LastMessageText, 
                        SiteUtils.ParseDate(x.LastMessageDate, DateTime.Now, "ru-RU")));

                return PartialView("_MessagesUsers", contactViews);
            }

            return PartialView("_MessagesUsers");
        }

        [HttpPost]
        public PartialViewResult GetMessages(string contact)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();

                var messages = dal.GetMyMessages(User.Identity.Name, SiteUtils.ParseGuid(contact));

                return PartialView("_Messages", messages);
            }

            return PartialView("_Messages");
        }

        [HttpPost]
        public void SendMessage(string text, string contact)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();

                var member = dal.GetUserMembership(SiteUtils.ParseGuid(contact));
                var contactView = dal.ViewChild(member.UserName);
                var myView = dal.ViewChild(User.Identity.Name);
                if (contactView != null && myView != null)
                {
                    var msg = new Message()
                    {
                        FromDisplay = myView.ContactName,
                        FromUserName = User.Identity.Name,
                        SendTime = DateTime.Now,
                        Subject = "Чат",
                        Text = text,
                        ToDisplay = contactView.ContactName,
                        ToUserName = member.UserName,
                        Unread = true
                    };
                    dal.InsertMessage(msg);
                }
            }
        }

        [HttpPost]
        public JsonResult RemoveMessages(string contact)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();
                dal.RemoveMessages(User.Identity.Name, SiteUtils.ParseGuid(contact));
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }


        // Остальное
        [HttpPost]
        public JsonResult ChangeEmail(string email)
        {
            var dal = new DataAccessBL();
            var view = dal.ViewChild(User.Identity.Name);
            if (view != null)
            {
                var child = dal.GetChild(view.Id);
                child.ContactEmail = email;
                dal.UpdateChild(child);
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangePhone(string phone)
        {
            var dal = new DataAccessBL();
            var view = dal.ViewChild(User.Identity.Name);
            if (view != null)
            {
                var child = dal.GetChild(view.Id);
                child.ContactPhone = phone;
                dal.UpdateChild(child);
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Orders()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();

            ViewBag.Departments = dal.GetActiveDepartments();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.DocFiles = GetDocumentFiles();
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            var weathers = new List<Weather> { dal.GetWeatherByCity(33991), dal.GetWeatherByCity(36870), dal.GetWeatherByCity(50207) };
            ViewBag.Weathers = weathers;

            return View(clientContainer);
        }

        public PartialViewResult OrderStep2Partial()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();

            ViewBag.Departments = dal.GetActiveDepartments();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.DocFiles = GetDocumentFiles();

            return PartialView("_OrderWizardStep2", new MembersViewModel(clientContainer));
        }

        public PartialViewResult OrderStep3Partial()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();

            ViewBag.Departments = dal.GetActiveDepartments();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.DocFiles = GetDocumentFiles();

            return PartialView("_OrderWizardStep3", clientContainer.NewOrder.Visits.Select(x => new VisitInfoViewModel(x)).ToList());
        }

        public PartialViewResult OrderStep4Partial()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();

            ViewBag.Departments = dal.GetActiveDepartments();
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.DocFiles = GetDocumentFiles();

            return PartialView("_OrderWizardStep4", clientContainer.NewOrder.Visits.Select(x => new VisitDetailViewModel(x)).ToList());
        }

        [HttpPost]
        public JsonResult SaveStep1(string department, string datefrom, string dateto, string countpacients, string countsputniks)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    int departmentId = SiteUtils.ParseInt(department, 0);
                    if (departmentId == 0)
                    {
                        return Json("Выберите клинику для лечения", JsonRequestBehavior.AllowGet);
                    }

                    DateTime dateFrom = SiteUtils.ParseDate(datefrom, DateTime.Today, CalendarCulture);
                    if (dateFrom < DateTime.Today.AddDays(7))
                    {
                        return Json("Дата заезда может быть не ранее чем через неделю.", JsonRequestBehavior.AllowGet);
                    }

                    DateTime dateTo = SiteUtils.ParseDate(dateto, DateTime.Today, CalendarCulture);
                    if (dateFrom >= dateTo)
                    {
                        return Json("Дата отъезда введена неверно.", JsonRequestBehavior.AllowGet);
                    }

                    int visitsCount = SiteUtils.ParseInt(countpacients, 0);
                    if (visitsCount <= 0 || visitsCount > 2)
                    {
                        return Json("Проверьте количество пациентов, в одной заявке допускается не более двух человек.", JsonRequestBehavior.AllowGet);
                    }

                    int sputniksCount = SiteUtils.ParseInt(countsputniks, 0);
                    if (sputniksCount <= 0 || sputniksCount > 4)
                    {
                        return Json("Проверьте количество сопровождающих, допускается не более четырёх человек.", JsonRequestBehavior.AllowGet);
                    }

                    this.clientContainer.NewOrder.DepartmentId = departmentId;
                    this.clientContainer.NewOrder.DateFrom = dateFrom;
                    this.clientContainer.NewOrder.DateTo = dateTo;
                    this.clientContainer.NewOrder.Name = "2";
                    this.clientContainer.ActualizeVisitsCount(visitsCount);
                    this.clientContainer.ActualizeSputniksCount(sputniksCount);
                    this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    this.clientContainer.NewOrder.LastDate = DateTime.Now;
                    this.clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult PendStep1(string department, string datefrom, string dateto, string countpacients, string countsputniks)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    int departmentId = SiteUtils.ParseInt(department, 0);
                    if (departmentId != 0)
                    {
                        this.clientContainer.NewOrder.DepartmentId = departmentId;
                    }
                    else
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }

                    DateTime dateFrom = SiteUtils.ParseDate(datefrom, DateTime.Today, CalendarCulture);
                    if (dateFrom >= DateTime.Today.AddDays(7))
                    {
                        this.clientContainer.NewOrder.DateFrom = dateFrom;
                    }

                    DateTime dateTo = SiteUtils.ParseDate(dateto, DateTime.Today, CalendarCulture);
                    if (dateFrom < dateTo)
                    {
                        this.clientContainer.NewOrder.DateTo = dateTo;
                    }

                    int visitsCount = SiteUtils.ParseInt(countpacients, 0);
                    if (visitsCount > 0 || visitsCount <= 2)
                    {
                        this.clientContainer.ActualizeVisitsCount(visitsCount);
                    }

                    int sputniksCount = SiteUtils.ParseInt(countsputniks, 0);
                    if (sputniksCount > 0 && sputniksCount <= 4)
                    {
                        this.clientContainer.ActualizeSputniksCount(sputniksCount);
                    }

                    this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    this.clientContainer.NewOrder.LastDate = DateTime.Now;
                    this.clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveStep2(MembersViewModel membersViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                        {
                            var visit = clientContainer.NewOrder.Visits.ToList()[i];
                            var visitVm = membersViewModel.PacientArray[i];
                            visit.Pacient.BirthDate = SiteUtils.ParseDate(visitVm.BirthDate, DateTime.Today, CalendarCulture);
                            visit.Pacient.CountryId = visitVm.CountryId;
                            visit.Pacient.Familiya = visitVm.Familiya;
                            visit.Pacient.FamiliyaEn = visitVm.FamiliyaEn;
                            visit.Pacient.NameEng = visitVm.NameEn;
                            visit.Pacient.Name = visitVm.Name;
                            visit.Pacient.Otchestvo = visitVm.Otchestvo;
                            visit.Pacient.SerialNumber = visitVm.SerialNumber;
                            visit.Pacient.CityName = visitVm.CityName;
                        }
                        for (int i = 0; i < clientContainer.NewOrder.Sputniks.Count; i++)
                        {
                            var sputnik = clientContainer.NewOrder.Sputniks.ToList()[i];
                            var sputnikVm = membersViewModel.SputnikArray[i];
                            sputnik.BirthDate = SiteUtils.ParseDate(sputnikVm.BirthDate, DateTime.Today, CalendarCulture);
                            sputnik.Email = sputnikVm.Email;
                            sputnik.Contacts = sputnikVm.Contacts;
                            sputnik.CountryId = sputnikVm.CountryId;
                            sputnik.RodstvoId = sputnikVm.RodstvoId;
                            sputnik.Familiya = sputnikVm.Familiya;
                            sputnik.FamiliyaEn = sputnikVm.FamiliyaEn;
                            sputnik.NameEn = sputnikVm.NameEn;
                            sputnik.Name = sputnikVm.Name;
                            sputnik.Otchestvo = sputnikVm.Otchestvo;
                            sputnik.SeriaNumber = sputnikVm.SerialNumber;
                        }

                        this.clientContainer.NewOrder.Name = "3";
                        this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                        this.clientContainer.NewOrder.LastDate = DateTime.Now;
                        this.clientContainer.Save();

                        return Json("1", JsonRequestBehavior.AllowGet);
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PendStep2(MembersViewModel membersViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                    {
                        var visit = clientContainer.NewOrder.Visits.ToList()[i];
                        var visitVm = membersViewModel.PacientArray[i];
                        if (ModelState.IsValidField(String.Format("PacientArray[{0}].BirthDate", i)))
                        {
                            visit.Pacient.BirthDate = SiteUtils.ParseDate(visitVm.BirthDate, DateTime.Today, CalendarCulture);
                        }
                        if (visitVm.CountryId != 0)
                        {
                            visit.Pacient.CountryId = visitVm.CountryId;
                        }
                        if (ModelState.IsValidField(String.Format("PacientArray[{0}].Familiya", i)))
                        {
                            visit.Pacient.Familiya = visitVm.Familiya;
                        }
                        visit.Pacient.FamiliyaEn = visitVm.FamiliyaEn;
                        visit.Pacient.NameEng = visitVm.NameEn;
                        if (ModelState.IsValidField(String.Format("PacientArray[{0}].Name", i)))
                        {
                            visit.Pacient.Name = visitVm.Name;
                        }
                        visit.Pacient.Otchestvo = visitVm.Otchestvo;
                        visit.Pacient.SerialNumber = visitVm.SerialNumber;
                        visit.Pacient.CityName = visitVm.CityName;
                    }
                    for (int i = 0; i < clientContainer.NewOrder.Sputniks.Count; i++)
                    {
                        var sputnik = clientContainer.NewOrder.Sputniks.ToList()[i];
                        var sputnikVm = membersViewModel.SputnikArray[i];
                        if (ModelState.IsValidField(String.Format("SputnikArray[{0}].BirthDate", i)))
                        {
                            sputnik.BirthDate = SiteUtils.ParseDate(sputnikVm.BirthDate, DateTime.Today, CalendarCulture);
                        }
                        if (ModelState.IsValidField(String.Format("SputnikArray[{0}].Email", i)))
                        {
                            sputnik.Email = sputnikVm.Email;
                        }
                        sputnik.Contacts = sputnikVm.Contacts;
                        if (sputnikVm.CountryId != 0)
                        {
                            sputnik.CountryId = sputnikVm.CountryId;
                        }
                        if (sputnikVm.RodstvoId != 0)
                        {
                            sputnik.RodstvoId = sputnikVm.RodstvoId;
                        }
                        if (ModelState.IsValidField(String.Format("SputnikArray[{0}].Familiya", i)))
                        {
                            sputnik.Familiya = sputnikVm.Familiya;
                        }
                        sputnik.FamiliyaEn = sputnikVm.FamiliyaEn;
                        sputnik.NameEn = sputnikVm.NameEn;
                        if (ModelState.IsValidField(String.Format("SputnikArray[{0}].Name", i)))
                        {
                            sputnik.Name = sputnikVm.Name;
                        }
                        sputnik.Otchestvo = sputnikVm.Otchestvo;
                        sputnik.SeriaNumber = sputnikVm.SerialNumber;
                    }

                    this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    this.clientContainer.NewOrder.LastDate = DateTime.Now;
                    this.clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveStep3(IEnumerable<VisitInfoViewModel> infoModels)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                        {
                            var visit = clientContainer.NewOrder.Visits.ToList()[i];
                            var visitVm = infoModels.ToList()[i];
                            visit.TodaysDiagnoz = visitVm.TodaysDiagnoz;
                            visit.HystoryA = visitVm.HystoryA;
                            visit.Hystoryb = visitVm.HystoryB;
                            visit.Razvitie = visitVm.Razvitie;
                            visit.Dispanser = visitVm.Dispanser;
                            visit.DispanserNarko = (string.IsNullOrEmpty(visitVm.IsDispanserNarko) || visitVm.IsDispanserNarko == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.DispanserNarko) ? visitVm.IsDispanserNarko : visitVm.DispanserNarko);
                            visit.Dispanser2 = visitVm.Dispanser2;
                            visit.DangerousDiseases = visitVm.DangerousDiseases;
                            visit.Serdce = visitVm.Serdce;
                            visit.Dihalka = visitVm.Dihalka;
                            visit.Infections = visitVm.Infections;
                            visit.OtherDiseases = visitVm.OtherDiseases;
                            visit.Epilispiya = visitVm.Epilispiya;
                            visit.SudorogiType = visitVm.SudorogiType;
                            visit.SudorogiCount = visitVm.SudorogiCount;
                            visit.SudorogiMedcine = visitVm.SudorogiMedcine;
                            visit.Remission = visitVm.Remission;
                            visit.Encefalogram = visitVm.Encefalogram;
                            visit.KursesRanee = (string.IsNullOrEmpty(visitVm.IsKursesRanee) || visitVm.IsKursesRanee == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.KursesRanee) ? visitVm.IsKursesRanee : visitVm.KursesRanee);
                            visit.KursesChinaRanee = (string.IsNullOrEmpty(visitVm.IsKursesChinaRanee) || visitVm.IsKursesChinaRanee == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.KursesChinaRanee) ? visitVm.IsKursesChinaRanee : visitVm.KursesChinaRanee);
                            visit.NonTradicial = (string.IsNullOrEmpty(visitVm.IsNonTradicial) || visitVm.IsNonTradicial == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.NonTradicial) ? visitVm.IsNonTradicial : visitVm.NonTradicial);
                            visit.Hirurg = visitVm.Hirurg;
                            visit.Travmi = visitVm.Travmi;
                        }

                        this.clientContainer.NewOrder.Name = "4";
                        this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                        this.clientContainer.NewOrder.LastDate = DateTime.Now;
                        this.clientContainer.Save();

                        return Json("1", JsonRequestBehavior.AllowGet);
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PendStep3(IEnumerable<VisitInfoViewModel> infoModels)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                    {
                        var visit = clientContainer.NewOrder.Visits.ToList()[i];
                        var visitVm = infoModels.ToList()[i];
                        visit.TodaysDiagnoz = visitVm.TodaysDiagnoz;
                        visit.HystoryA = visitVm.HystoryA;
                        visit.Hystoryb = visitVm.HystoryB;
                        visit.Razvitie = visitVm.Razvitie;
                        visit.Dispanser = visitVm.Dispanser;
                        visit.DispanserNarko = (string.IsNullOrEmpty(visitVm.IsDispanserNarko) || visitVm.IsDispanserNarko == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.DispanserNarko) ? visitVm.IsDispanserNarko : visitVm.DispanserNarko);
                        visit.Dispanser2 = visitVm.Dispanser2;
                        visit.DangerousDiseases = visitVm.DangerousDiseases;
                        visit.Serdce = visitVm.Serdce;
                        visit.Dihalka = visitVm.Dihalka;
                        visit.Infections = visitVm.Infections;
                        visit.OtherDiseases = visitVm.OtherDiseases;
                        visit.Epilispiya = visitVm.Epilispiya;
                        visit.SudorogiType = visitVm.SudorogiType;
                        visit.SudorogiCount = visitVm.SudorogiCount;
                        visit.SudorogiMedcine = visitVm.SudorogiMedcine;
                        visit.Remission = visitVm.Remission;
                        visit.Encefalogram = visitVm.Encefalogram;
                        visit.KursesRanee = (string.IsNullOrEmpty(visitVm.IsKursesRanee) || visitVm.IsKursesRanee == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.KursesRanee) ? visitVm.IsKursesRanee : visitVm.KursesRanee);
                        visit.KursesChinaRanee = (string.IsNullOrEmpty(visitVm.IsKursesChinaRanee) || visitVm.IsKursesChinaRanee == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.KursesChinaRanee) ? visitVm.IsKursesChinaRanee : visitVm.KursesChinaRanee);
                        visit.NonTradicial = (string.IsNullOrEmpty(visitVm.IsNonTradicial) || visitVm.IsNonTradicial == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.NonTradicial) ? visitVm.IsNonTradicial : visitVm.NonTradicial);
                        visit.Hirurg = visitVm.Hirurg;
                        visit.Travmi = visitVm.Travmi;
                    }

                    this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    this.clientContainer.NewOrder.LastDate = DateTime.Now;
                    this.clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveStep4(IEnumerable<VisitDetailViewModel> detailModels)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                        {
                            var visit = clientContainer.NewOrder.Visits.ToList()[i];
                            var visitVm = detailModels.ToList()[i];
                            visit.Razgovor = visitVm.Razgovor;
                            visit.Instructcii = visitVm.Instructcii;
                            visit.Fisical = visitVm.Fisical;
                            visit.Diet = visitVm.Diet;
                            visit.Eating = visitVm.Eating;
                            visit.Appetit = visitVm.Appetit;
                            visit.Stul = visitVm.Stul;
                            visit.Alergiya = (string.IsNullOrEmpty(visitVm.IsAlergiya) || visitVm.IsAlergiya == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.Alergiya) ? visitVm.IsAlergiya : visitVm.Alergiya);
                            visit.Imunitet = visitVm.Imunitet;
                            visit.Fiznagruzki = visitVm.Fiznagruzki;
                            visit.Son = visitVm.Son;
                            visit.ProstupUp = visitVm.ProstupUp;
                            visit.Zakativaetsa = visitVm.Zakativaetsa;
                        }

                        clientContainer.NewOrder.Name = "5";
                        this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                        this.clientContainer.NewOrder.LastDate = DateTime.Now;
                        this.clientContainer.Save();

                        return Json("1", JsonRequestBehavior.AllowGet);
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PendStep4(IEnumerable<VisitDetailViewModel> detailModels)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    for (int i = 0; i < clientContainer.NewOrder.Visits.Count; i++)
                    {
                        var visit = clientContainer.NewOrder.Visits.ToList()[i];
                        var visitVm = detailModels.ToList()[i];
                        visit.Razgovor = visitVm.Razgovor;
                        visit.Instructcii = visitVm.Instructcii;
                        visit.Fisical = visitVm.Fisical;
                        visit.Diet = visitVm.Diet;
                        visit.Eating = visitVm.Eating;
                        visit.Appetit = visitVm.Appetit;
                        visit.Stul = visitVm.Stul;
                        visit.Alergiya = (string.IsNullOrEmpty(visitVm.IsAlergiya) || visitVm.IsAlergiya == "Нет") ? "Нет" : (string.IsNullOrEmpty(visitVm.Alergiya) ? visitVm.IsAlergiya : visitVm.Alergiya);
                        visit.Imunitet = visitVm.Imunitet;
                        visit.Fiznagruzki = visitVm.Fiznagruzki;
                        visit.Son = visitVm.Son;
                        visit.ProstupUp = visitVm.ProstupUp;
                        visit.Zakativaetsa = visitVm.Zakativaetsa;
                    }

                    this.clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    this.clientContainer.NewOrder.LastDate = DateTime.Now;
                    this.clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveStep5()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    clientContainer.NewOrder.StatusId = 2;
                    clientContainer.NewOrder.Name = String.Empty;
                    clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    clientContainer.NewOrder.LastDate = DateTime.Now;
                    clientContainer.Save();

                    var notify = new OrderSentNotification(clientContainer.NewOrder.Id);
                    notify.Send();
                    foreach (var visit in clientContainer.NewOrder.Visits)
                    {
                        var notifyEmail = new OrderSentEmailNotification(visit.Id, Server);
                        notifyEmail.Send();
                        var notifyEmailToUser = new OrderSentToUserEmailNotification(SiteUtils.GetCurrentUserName());
                        notifyEmailToUser.Send();
                    }
                    Session.Abandon();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PendStep5()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
                    clientContainer.NewOrder.LastDate = DateTime.Now;
                    clientContainer.Save();

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MyPage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var dal = new DataAccessBL();
            var child = dal.ViewChild(User.Identity.Name);
            ViewBag.Countries = dal.GetRefCountries();
            ViewBag.Rodstvos = dal.GetRefRodstvo();
            ViewBag.Operators = dal.GetRefOperators();
            ViewBag.CountryBanks = dal.GetRefBanks(child.FinCountryId ?? child.CountryId);
            ViewBag.CurrencyRateCNY = GetRate("CNY");
            var weathers = new List<Weather> { dal.GetWeatherByCity(33991), dal.GetWeatherByCity(36870), dal.GetWeatherByCity(50207) };
            ViewBag.Weathers = weathers;

            var vipiskas = dal.GetMyVipiskas(User.Identity.Name).ToList();
            ViewBag.Vipiskas = vipiskas.Select(x => new VisitResultViewModel(x));
            ViewBag.TestLevel1Val = "-";
            ViewBag.TestLevel2Val = "-";
            ViewBag.TestLevel3Val = "-";
            ViewBag.TestLevel1Desc = "";
            ViewBag.TestLevel2Desc = "";
            ViewBag.TestLevel3Desc = "";

            if (vipiskas.Any())
            {
                var vipiska = vipiskas[0];
                if (vipiska.RefGmfcsLevel != null)
                {
                    ViewBag.TestLevel1Val = vipiska.RefGmfcsLevel.Name;
                    ViewBag.TestLevel1Desc = vipiska.RefGmfcsLevel.Description;
                }
                if (vipiska.RefMacsLevel != null)
                {
                    ViewBag.TestLevel2Val = vipiska.RefMacsLevel.Name;
                    ViewBag.TestLevel2Desc = vipiska.RefMacsLevel.Description;
                }
                if (vipiska.RefCfcsLevel != null)
                {
                    ViewBag.TestLevel3Val = vipiska.RefCfcsLevel.Name;
                    ViewBag.TestLevel3Desc = vipiska.RefCfcsLevel.Description;
                }
            }

            ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id), dal.GetMyPosts(child.Id));
            return View(ViewBag.Profile);
        }

        [HttpPost]
        public PartialViewResult GaleryAction(string pictodelete, string pictohideuphide, string pictotop)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dal = new DataAccessBL();
                var view = dal.ViewChild(User.Identity.Name);

                if (!string.IsNullOrEmpty(pictodelete))
                {
                    var visual = new ChildVisualDetailed(view, dal.GetChildHideFiles(view.Id), dal.GetChildAvaFile(view.Id));
                    visual.DeletePhoto(pictodelete);
                    dal.DeleteChildHideFile(view.Id, pictodelete);
                }
                else if (!string.IsNullOrEmpty(pictohideuphide))
                {
                    if (dal.CheckChildHideFile(view.Id, pictohideuphide))
                    {
                        dal.DeleteChildHideFile(
                            dal.GetChildHideFiles(view.Id).FirstOrDefault(x => x.FileName == pictohideuphide));
                    }
                    else
                    {
                        dal.InsertChildHideFile(new ChildHideFile()
                        {
                            FileName = pictohideuphide,
                            ChildId = view.Id,
                            HideDate = DateTime.Now
                        });
                    }
                }
                else if (!string.IsNullOrEmpty(pictotop))
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
                        FileName = pictotop
                    };
                    dal.InsertChildAvaFile(ava);
                }

                ViewBag.Profile = new ChildVisualDetailed(view, dal.GetChildHideFiles(view.Id), dal.GetChildAvaFile(view.Id), dal.GetMyPosts(view.Id));
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

                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id), dal.GetMyPosts(child.Id));
            }
            return PartialView("_CabinetDocumentsTales", ViewBag.Profile);
        }

        [HttpPost]
        public PartialViewResult DeleteOrderFile(string filename)
        {
            if (User.Identity.IsAuthenticated)
            {
                this.RemoveOrderFile(filename);

                List<string> files = GetDocumentFiles().ToList();
                return PartialView("_OrderWizardStep3Files", files);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new PartialViewResult();
            }
        }

        [HttpPost]
        public PartialViewResult UploadOrderFile()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<string> files;
                UploadWizardFile(out files);

                return PartialView("_OrderWizardStep3Files", files);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new PartialViewResult();
            }
        }

        [HttpPost]
        public PartialViewResult UploadPhoto()
        {
            if (User.Identity.IsAuthenticated)
            {
                UploadFile(false);

                var dal = new DataAccessBL();
                var child = dal.ViewChild(User.Identity.Name);
                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id), dal.GetMyPosts(child.Id));
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
                ViewBag.Profile = new ChildVisualDetailed(child, dal.GetChildHideFiles(child.Id), dal.GetChildAvaFile(child.Id), dal.GetMyPosts(child.Id));
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
                string newUrl;
                try
                {
                    newUrl = UploadFile().Replace(@"/", @"\");
                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Upload failed");
                }

                return Json(newUrl);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Пользователь не определён");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadUserpic()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    UploadUserpicFile();
                    Session["UserpicUrl"] = null;
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
        public async Task<JsonResult> RemoveUserpic()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    RemoveUserpicFile();
                    Session["UserpicUrl"] = null;
                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Removing failed");
                }

                return Json("File removed successfully");
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

                    if (child.ContactName != contactname)
                    {
                        Session["UserContactName"] = contactname;
                        child.ContactName = contactname;
                    }
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
        public JsonResult SaveTab3(SocialLinksModel socialLinks)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    child.SocialOk = socialLinks.SocialOk;
                    child.SocialVk = socialLinks.SocialVk;
                    child.SocialMm = socialLinks.SocialMm;
                    child.SocialFb = socialLinks.SocialFb;
                    child.SocialYoutube = socialLinks.SocialYoutube;

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
            , string webmoney4
            , string yandexmoney
            , string kiwi
            , string finoperator
            , string finoperator2
            , string finoperator3
            , string finoperator4
            , string fintelephone
            , string fintelephone2
            , string fintelephone3
            , string fintelephone4
            , string fincountry
            , string bank
            , string bankother
            , string cardnumber
            , string cardname)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    int operatorId;
                    int.TryParse(finoperator, out operatorId);
                    int operator2Id;
                    int.TryParse(finoperator2, out operator2Id);
                    int operator3Id;
                    int.TryParse(finoperator3, out operator3Id);
                    int operator4Id;
                    int.TryParse(finoperator4, out operator4Id);

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
                    child.FinWebmoney4 = webmoney4;
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
                    if (operator2Id == 0)
                    {
                        child.FinOperator2Id = null;
                    }
                    else
                    {
                        child.FinOperator2Id = operator2Id;
                    }
                    if (operator3Id == 0)
                    {
                        child.FinOperator3Id = null;
                    }
                    else
                    {
                        child.FinOperator3Id = operator3Id;
                    }
                    if (operator4Id == 0)
                    {
                        child.FinOperator4Id = null;
                    }
                    else
                    {
                        child.FinOperator4Id = operator4Id;
                    }
                    child.FinPhoneNumber = fintelephone;
                    child.FinPhoneNumber2 = fintelephone2;
                    child.FinPhoneNumber3 = fintelephone3;
                    child.FinPhoneNumber4 = fintelephone4;
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

        private void RemoveOrderFile(string fileName)
        {
            string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], clientContainer.NewOrder.GuidId.ToString());
            string docsLocation = photoLocation.Replace("Upload", "Documents");
            if (Directory.Exists(docsLocation))
            {
                FileUtils.DeleteFile(new DirectoryInfo(docsLocation), fileName);
            }
        }

        /// <summary>
        /// Загружает новую фотографию в галерею и делает её главной. Возвращает новый url картинки.
        /// </summary>
        /// <param name="isAva"></param>
        /// <returns></returns>
        private string UploadFile(bool isAva = true)
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

                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file); // + ".jpg";

                    string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
                    string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], view.GuidId.ToString());
                    string folder = string.Format(OriginalDirectory, photoLocation, view.GuidId);
                    string folderThumb = string.Format(ThumbDirectory, photoLocation, view.GuidId);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    if (!Directory.Exists(folderThumb))
                    {
                        Directory.CreateDirectory(folderThumb);
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        var path = Path.Combine(folder, fileName);
                        string fileThumbName = Path.Combine(folderThumb, fileName);
                        using (Image original = Image.FromStream(stream))
                        using (Image thumb = PhotoUtils.Inscribe(original, 313, 313))
                        using (Image big = PhotoUtils.resizeImage(original, (int)(((double)original.Height) / (1.0 * (double)original.Width / 919)), 919, true, true))
                        {
                            //PhotoUtils.SaveToJpeg(original, path);
                            PhotoUtils.SaveToJpeg(thumb, fileThumbName);
                            PhotoUtils.SaveToJpeg(big, path);
                        }
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
                    if (isAva)
                    {
                        return Path.Combine(Path.Combine(photoUrl, "Thumb"), fileName);
                    }
                }
            }
            return string.Empty;
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

        private void UploadUserpicFile()
        {
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var dal = new DataAccessBL();
                    var view = dal.ViewChild(User.Identity.Name);
                    var child = dal.GetChild(view.Id);

                    const string OriginalDirectory = @"{0}{1}\";

                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file); // + ".jpg";

                    child.OwnerUserPic = fileName;
                    dal.UpdateChild(child);

                    string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
                    string docsLocation = photoLocation.Replace("Upload", "Userpics");
                    string folder = string.Format(OriginalDirectory, docsLocation, view.GuidId);

                    if (Directory.Exists(folder))
                    {
                        FileUtils.DeleteFolder(folder);
                    }
                    Directory.CreateDirectory(folder);

                    var path = Path.Combine(folder, fileName);

                    using (Image original = PhotoUtils.Inscribe(Image.FromStream(stream), 48, 48))
                    {
                        PhotoUtils.SaveToJpeg(original, path);
                    }
                }
            }
        }

        private void RemoveUserpicFile()
        {
            var dal = new DataAccessBL();
            var view = dal.ViewChild(User.Identity.Name);
            var child = dal.GetChild(view.Id);
            child.OwnerUserPic = string.Empty;
            dal.UpdateChild(child);

            const string OriginalDirectory = @"{0}{1}\";

            string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
            string docsLocation = photoLocation.Replace("Upload", "Userpics");
            string folder = string.Format(OriginalDirectory, docsLocation, view.GuidId);

            if (Directory.Exists(folder))
            {
                FileUtils.DeleteFolder(folder);
            }
        }

        private void UploadWizardFile(out List<string> files)
        {
            files = new List<string>();
            foreach (string requestFile in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[requestFile];
                if (file != null && file.ContentLength > 0)
                {
                    const string OriginalDirectory = @"{0}{1}\";
                    var fileName = Path.GetFileName(requestFile);


                    string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
                    string docsLocation = photoLocation.Replace("Upload", "Documents");
                    string folder = string.Format(OriginalDirectory, docsLocation, clientContainer.NewOrder.GuidId);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    if (fileName != null)
                    {
                        var path = Path.Combine(folder, fileName);
                        file.SaveAs(path);
                    }
                    files.AddRange(Directory.GetFiles(folder).ToList().Select(Path.GetFileName));
                }
            }
        }

        private IEnumerable<string> GetDocumentFiles()
        {
            const string OriginalDirectory = @"{0}{1}\";
            string photoLocation = ConfigurationManager.AppSettings["PhotoLocation"];
            string docsLocation = photoLocation.Replace("Upload", "Documents");
            string folder = string.Format(OriginalDirectory, docsLocation, clientContainer.NewOrder.GuidId);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return Directory.GetFiles(folder).ToList().Select(Path.GetFileName);

        }

        private ClientContainer clientContainer
        {
            get
            {
                if (Session["ClientContainer"] == null)
                {
                    Session["ClientContainer"] = new ClientContainer(Utils.SiteUtils.GetCurrentUserName());
                }
                return (ClientContainer)Session["ClientContainer"];
            }
            set
            {
                Session["ClientContainer"] = value;
            }
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
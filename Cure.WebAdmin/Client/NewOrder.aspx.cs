using System;
using System.Web;
using Cure.Utils;

namespace Cure.WebAdmin.Client
{
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.Web.ASPxEditors;
    using Logic;
    using Notification;

    public partial class NewOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dal = new DataAccessBL();
                uxFillFromPreviousLink.Visible = dal.GetMyOrders(SiteUtils.GetCurrentUserName()).Any();
                FormBind(ClientContainer);
            }
        }

        private void FormBind(ClientContainer container)
        {
            uxOrderDateFrom.Date = container.NewOrder.DateFrom;
            uxOrderDateTo.Date = container.NewOrder.DateTo;
            uxOrderDescription.Text = container.NewOrder.Description;
            uxOrderDepartment.DataBind();
            foreach (ListEditItem item in uxOrderDepartment.Items)
            {
                item.Selected = Convert.ToInt32(item.Value) == container.NewOrder.DepartmentId;
            }
        }

        protected void uxCallbackPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter == "addpacient")
            {
                AddPacient();
                ClientContainer.Save();
            } else if (e.Parameter == "addsputnik")
            {
                AddSputnik();
                ClientContainer.Save();
            } else if (e.Parameter == "fillprevious")
            {
                ClientContainer.FillFromPrevious();
            }
        }

        protected void AddPacient()
        {
            var pacient = new Pacient()
            {
                //Address = uxPacientAddress.Text,
                SerialNumber = uxPacientSerialNumber.Text,
                BirthDate = uxPacientBirthDate.Date,
                CityName = uxPacientCityName.Text,
                CountryId = Convert.ToInt32(uxPacientCountryId.SelectedItem.Value),
                CreateDate = DateTime.Today,
                CreateUser = SiteUtils.GetCurrentUserName(),
                //Diagnoz = uxPacientDiagnoz.Text,
                Familiya = uxPacientFamiliya.Text,
                LastDate = DateTime.Today,
                LastUser = SiteUtils.GetCurrentUserName(),
                FamiliyaEn = uxPacientFamiliyaEn.Text,
                Name = uxPacientName.Text,
                NameEng = uxPacientNameEng.Text,
                Otchestvo = uxPacientOtchestvo.Text,
                //OtchestvoEn = uxPacientOtchestvoEn.Text,
                OwnerUser = SiteUtils.GetCurrentUserName()
            };
            var vist = new Visit()
            {
                AmbNumber = String.Empty,
                CreateDate = DateTime.Today,
                CreateUser = SiteUtils.GetCurrentUserName(),
                DateDogovor = DateTime.Today,
                Additional = uxVisitAdditional.Text,
                TodaysDiagnoz = uxVisitTodaysDiagnoz.Text,
                HystoryA = uxVisitHystoryAm.Text,
                Hystoryb = uxVisitHystoryB.Text,
                Razvitie = uxVisitRazvitie.Text,
                Dispanser = uxVisitDispanser.Text,
                Dispanser2 = uxVisitDispanserB.Text,
                DangerousDiseases = uxVisitDangerousDiseases.Text,
                Serdce = uxVisitSerdce.Text,
                Dihalka = uxVisitDihalka.Text,
                Infections = uxVisitInfections.Text,
                OtherDiseases = uxVisitOtherDiseases.Text,
                Epilispiya = uxVisitEpilispiya.Text,
                SudorogiType = uxVisitSudorogiTypem.Text,
                SudorogiCount = uxVisitSudorogiCount.Text,
                SudorogiMedcine = uxVisitSudorogiMedcine.Text,
                Remission = uxVisitRemission.Text,
                Encefalogram = uxVisitEncefalogramm.Text,
                MainGoal = uxVisitMainGoal.Text,
                Razgovor = uxVisitRazgovor.Text,
                Instructcii = uxVisitInstructcii.Text,
                Fisical = uxVisitFisical.Text,
                Diet = uxVisitDiet.Text,
                Stul = uxVisitStul.Text,
                Eating = uxVisitEating.Text,
                EatingProblems = uxVisitEatingProblems.Text,
                Appetit = uxVisitAppetit.Text,
                Alergiya = uxVisitAlergiya.Text,
                Imunitet = uxVisitImunitet.Text,
                Fiznagruzki = uxVisitFiznagruzki.Text,
                Son = uxVisitSon.Text,
                ProstupUp = uxVisitProstupUp.Text,
                Zakativaetsa = uxVisitZakativaetsa.Text,
                KursesRanee = uxVisitKursesRanee.Text,
                KursesChinaRanee = uxVisitKursesChinaRanee.Text,
                NonTradicial = uxVisitNonTradicial.Text,
                Hirurg = uxVisitHirurg.Text,
                Travmi = uxVisitTravmi.Text,
                Requirements = uxVisitRequirements.Text,
                RequiredDocs = uxVisitRequiredDocs.Text,

                IsInvicePrint = false,
                IsInvite = false,
                LastDate = DateTime.Today,
                LastUser = SiteUtils.GetCurrentUserName(),
                Pacient = pacient,
                Postuplenie = String.Empty,
                Price = null,
                Recomend = String.Empty,
                Resultat = String.Empty,
                Vipiska = String.Empty
            };
            ClientContainer.NewOrder.Visits.Add(vist);
        }

        protected void AddSputnik()
        {
            var sputnik = new Sputnik()
            {
                OrderId = ClientContainer.NewOrder.Id,
                IsPrimary = ClientContainer.NewOrder.Sputniks.Count == 0,
                Name = uxSputnikName.Text,
                NameEn = uxSputnikNameEn.Text,
                Familiya = uxSputnikFamiliya.Text,
                FamiliyaEn = uxSputnikFamiliyaEn.Text,
                Otchestvo = uxSputnikOtchestvo.Text,
                Email = uxSputnikEmail.Text,
                Contacts = uxSputnikContact.Text,
                BirthDate = uxSputnikBirthDate.Date,
                SeriaNumber = uxSputnikSeriaNumber.Text,
                RodstvoId = (int)uxSputnikRodstvo.SelectedItem.Value,
                CountryId = Convert.ToInt32(uxSputnikCountryId.SelectedItem.Value),
                OwnerUser = SiteUtils.GetCurrentUserName(),
                LastUser = SiteUtils.GetCurrentUserName(),
                LastDate = DateTime.Today,
                CreateUser = SiteUtils.GetCurrentUserName(),
                CreateDate = DateTime.Today,
            };
            ClientContainer.NewOrder.Sputniks.Add(sputnik);
        }

        private void Save(bool isSend = false)
        {
            var isValid = true;
            if (isSend && !ClientContainer.NewOrder.Visits.Any())
            {
                uxSendResultBox.TextRed = "Заявка должна содержать как минимум одного пациента, добавьте, пожалуйста, в заявку пациента.<br>";
                isValid = false;
            }
            if (isSend && !ClientContainer.NewOrder.Sputniks.Any())
            {
                uxSendResultBox.TextRed = "Заявка должна содержать как минимум одного сопровождающего, добавьте, пожалуйста, в заявку сопровождающего.<br>";
                isValid = false;
            }

            if (!isValid && isSend)
            {
                return;
            }

            ClientContainer.NewOrder.DateFrom = uxOrderDateFrom.Date;
            ClientContainer.NewOrder.DateTo = uxOrderDateTo.Date;
            ClientContainer.NewOrder.Description = uxOrderDescription.Text;
            ClientContainer.NewOrder.DepartmentId = Convert.ToInt32(uxOrderDepartment.SelectedItem.Value);
            ClientContainer.NewOrder.StatusId = isSend ? 2 : 1;
            ClientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
            ClientContainer.NewOrder.LastDate = DateTime.Now;
            ClientContainer.Save();
            if (isSend)
            {
                var notify = new OrderSentNotification(ClientContainer.NewOrder.Id);
                notify.Send();
                foreach (var visit in ClientContainer.NewOrder.Visits)
                {
                    var notifyEmail = new OrderSentEmailNotification(visit.Id, new HttpServerUtilityWrapper(this.Server));
                    notifyEmail.Send();
                    var notifyEmailToUser = new OrderSentToUserEmailNotification(SiteUtils.GetCurrentUserName());
                    notifyEmailToUser.Send();
                }
                Session.Abandon();
                Response.Redirect(ResolveUrl("~/Client/MyOrders.aspx"));
            }
        }

        protected void uxSave_Click(object sender, EventArgs e)
        {
            uxSendResultBox.TextRed = string.Empty;
            Save();
        }

        protected void uxSend_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private ClientContainer ClientContainer
        {
            get
            {
                if (HttpContext.Current.Session["ClientContainer"] == null)
                {
                    HttpContext.Current.Session["ClientContainer"] = new ClientContainer(Utils.SiteUtils.GetCurrentUserName());
                }
                return (ClientContainer)HttpContext.Current.Session["ClientContainer"];
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session["ClientContainer"] = value;
                }
            }
        }
    }
}
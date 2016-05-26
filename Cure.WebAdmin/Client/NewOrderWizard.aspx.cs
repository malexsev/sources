using System;
using System.Web;
using Cure.Utils;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client
{
    using System.Globalization;
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.Web.ASPxEditors;
    using Logic;
    using Notification;

    public partial class NewOrderWizard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = DateTime.Today.Year; i > DateTime.Today.AddYears(-45).Year; i--)
                {
                    uxPacientBirthDateYear.Items.Add(new ListEditItem()
                    {
                        Text = i.ToString(CultureInfo.InvariantCulture),
                        Value = i,
                        Selected = i == DateTime.Today.Year
                    });
                }
            }
        }

        protected void uxWizard_CancelButtonClick(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Client/MyOrders.aspx"));
        }

        protected void uxWizard_FinishButtonClick(object sender, EventArgs e)
        {

            clientContainer.NewOrder.DateFrom = uxOrderDateFrom.Date;
            clientContainer.NewOrder.DateTo = uxOrderDateTo.Date;
            //clientContainer.NewOrder.Description = uxOrderDescription.Text;
            clientContainer.NewOrder.DepartmentId = Convert.ToInt32(uxOrderDepartment.SelectedItem.Value);
            clientContainer.NewOrder.StatusId = 2;
            clientContainer.NewOrder.LastUser = SiteUtils.GetCurrentUserName();
            clientContainer.NewOrder.LastDate = DateTime.Now;
            clientContainer.Save();

            var notify = new OrderSentNotification(clientContainer.NewOrder.Id);
            notify.Send();
            foreach (var visit in clientContainer.NewOrder.Visits)
            {
                var notifyEmail = new OrderSentEmailNotification(visit.Id, this);
                notifyEmail.Send();
                var notifyEmailToUser = new OrderSentToUserEmailNotification(SiteUtils.GetCurrentUserName());
                notifyEmailToUser.Send();
            }
            Session.Abandon();
            Response.Redirect(ResolveUrl("~/Client/CurrentOrder.aspx"));
        }

        protected void uxWizard_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (!Page.IsValid)
            {
                e.Cancel = true;
                return;
            }

            var step = uxWizard.WizardSteps[e.CurrentStepIndex];
            switch (step.ID)
            {
                case "stepGeneral":
                    {
                        for (var i = clientContainer.NewOrder.Visits.Count - 1; i >= 0; i--)
                        {
                            var visit = clientContainer.NewOrder.Visits.FirstOrDefault();
                            if (visit != null)
                            {
                                for (int s = visit.Order.Sputniks.Count - 1; s >= 0; s--)
                                {
                                    clientContainer.RemoveSputnik(visit.Order.Sputniks.ToList()[s].Id);
                                }

                                clientContainer.RemovePacient(visit.Id);
                            }
                        }

                        clientContainer.NewOrder.Visits.Clear();

                        clientContainer.NewOrder.DateFrom = uxOrderDateFrom.Date;
                        clientContainer.NewOrder.DateTo = uxOrderDateTo.Date;
                        //clientContainer.NewOrder.Description = uxOrderDescription.Text;
                        clientContainer.NewOrder.DepartmentId = Convert.ToInt32(uxOrderDepartment.SelectedItem.Value);

                        clientContainer.Save();
                        break;
                    }
                case "stepPacientMedinfo":
                    {
                        var pacient = new Pacient()
                        {
                            Name = "-",
                            CountryId = 7,
                            CreateDate = DateTime.Now,
                            CreateUser = SiteUtils.GetCurrentUserName(),
                            LastDate = DateTime.Now,
                            LastUser = SiteUtils.GetCurrentUserName(),
                            OwnerUser = SiteUtils.GetCurrentUserName()
                        };
                        var vist = new Visit()
                        {
                            IsInvicePrint = false,
                            IsInvite = false,
                            LastDate = DateTime.Today,
                            LastUser = SiteUtils.GetCurrentUserName(),
                            Pacient = pacient,
                            Postuplenie = String.Empty,
                            Price = null,
                            Recomend = String.Empty,
                            Resultat = String.Empty,
                            Vipiska = String.Empty,
                            CreateDate = DateTime.Now,

                            AmbNumber = String.Empty,
                            CreateUser = SiteUtils.GetCurrentUserName(),
                            DateDogovor = DateTime.Today,
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
                            Encefalogram = uxVisitEncefalogramm.Text
                        };

                        clientContainer.NewOrder.Visits.Add(vist);
                        clientContainer.Save();
                        break;
                    }
                case "stepPacientIsEpilepsy":
                    {
                        uxWizard.MoveTo(uxIsEpilepcy.SelectedItem.Value.ToString() == "1"
                            ? stepPacientEpilepsy
                            : stepPacientNaviki);

                        break;
                    }
                case "stepPacientEpilepsy":
                    {
                        var visit = clientContainer.NewOrder.Visits.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                        if (visit != null)
                        {
                            visit.Epilispiya = uxVisitEpilispiya.Text;
                            visit.SudorogiType = uxVisitSudorogiTypem.Text;
                            visit.SudorogiCount = uxVisitSudorogiCount.Text;
                            visit.SudorogiMedcine = uxVisitSudorogiMedcine.Text;
                            visit.Remission = uxVisitRemission.Text;
                        }

                        clientContainer.Save();
                        break;
                    }
                case "stepPacientNaviki":
                    {
                        var visit = clientContainer.NewOrder.Visits.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                        if (visit != null)
                        {
                            visit.Razgovor = uxVisitRazgovor.Text;
                            visit.Instructcii = uxVisitInstructcii.Text;
                            visit.Fisical = uxVisitFisical.Text;
                            visit.Diet = uxVisitDiet.Text;
                            visit.Stul = uxVisitStul.Text;
                            visit.Eating = uxVisitEating.Text;
                            visit.EatingProblems = uxVisitEatingProblems.Text;
                            visit.Appetit = uxVisitAppetit.Text;
                            visit.Alergiya = uxVisitAlergiya.Text;
                            visit.Imunitet = uxVisitImunitet.Text;
                            visit.Fiznagruzki = uxVisitFiznagruzki.Text;
                            visit.Son = uxVisitSon.Text;
                            visit.ProstupUp = uxVisitProstupUp.Text;
                            visit.Zakativaetsa = uxVisitZakativaetsa.Text;
                        }

                        clientContainer.Save();
                        break;
                    }
                case "stepPacientAdditional":
                    {
                        var visit = clientContainer.NewOrder.Visits.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                        if (visit != null)
                        {
                            visit.KursesRanee = uxVisitKursesRanee.Text;
                            visit.KursesChinaRanee = uxVisitKursesChinaRanee.Text;
                            visit.NonTradicial = uxVisitNonTradicial.Text;
                            visit.Hirurg = uxVisitHirurg.Text;
                            visit.Travmi = uxVisitTravmi.Text;
                        }

                        clientContainer.Save();
                        break;
                    }
                //case "stepPacientRequirements":
                //    {
                //        var visit = clientContainer.NewOrder.Visits.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                //        if (visit != null)
                //        {
                //            visit.KursesRanee = uxVisitKursesRanee.Text;
                //            visit.KursesChinaRanee = uxVisitKursesChinaRanee.Text;
                //            visit.NonTradicial = uxVisitNonTradicial.Text;
                //            visit.Hirurg = uxVisitHirurg.Text;
                //            visit.Travmi = uxVisitTravmi.Text;
                //            visit.Additional = uxVisitAdditional.Text;
                //        }

                //        clientContainer.Save();
                //        break;
                //    }
                case "stepPacientInfo":
                    {
                        var visit = clientContainer.NewOrder.Visits.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                        if (visit != null)
                        {
                            if (visit.Pacient != null)
                            {
                                visit.Pacient.SerialNumber = uxPacientSerialNumber.Text;
                                //visit.Pacient.BirthDate = uxPacientBirthDate.Date;
                                var birthday = new DateTime(int.Parse(uxPacientBirthDateYear.SelectedItem.Value.ToString()), int.Parse(uxPacientBirthDateMonth.SelectedItem.Value.ToString()), int.Parse(uxPacientBirthDateDay.SelectedItem.Value.ToString()));
                                visit.Pacient.BirthDate = birthday;

                                visit.Pacient.CityName = uxPacientCityName.Text;
                                visit.Pacient.CountryId = Convert.ToInt32(uxPacientCountryId.SelectedItem.Value);
                                visit.Pacient.CreateDate = DateTime.Now;
                                visit.Pacient.CreateUser = SiteUtils.GetCurrentUserName();
                                visit.Pacient.Familiya = uxPacientFamiliya.Text;
                                visit.Pacient.LastDate = DateTime.Now;
                                visit.Pacient.LastUser = SiteUtils.GetCurrentUserName();
                                visit.Pacient.FamiliyaEn = uxPacientFamiliyaEn.Text;
                                visit.Pacient.Name = uxPacientName.Text;
                                visit.Pacient.NameEng = uxPacientNameEng.Text;
                                visit.Pacient.Otchestvo = uxPacientOtchestvo.Text;
                            }
                        }

                        clientContainer.Save();

                        if (uxIsAddNewPacient.SelectedItem.Value.ToString() == "1")
                        {
                            ClearPacient();
                            uxWizard.MoveTo(stepPacientMedinfo);
                        }
                        else
                        {
                            uxWizard.MoveTo(stepSputnikInfo);
                        };

                        break;
                    }
                //case "stepPacientFinish":
                //    {
                //        if (uxIsAddNewPacient.SelectedItem.Value.ToString() == "1")
                //        {
                //            ClearPacient();
                //            uxWizard.MoveTo(stepPacientMedinfo);
                //        }
                //        else
                //        {
                //            uxWizard.MoveTo(stepSputnikInfo);
                //        };

                //        break;
                //    }
                case "stepSputnikInfo":
                    {
                        var sputnik = new Sputnik()
                        {
                            OrderId = clientContainer.NewOrder.Id,
                            IsPrimary = clientContainer.NewOrder.Sputniks.Count == 0,
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
                        clientContainer.NewOrder.Sputniks.Add(sputnik);

                        clientContainer.Save();

                        if (uxIsAddNewSputnik.SelectedItem.Value.ToString() == "1")
                        {
                            ClearSputnik();
                            e.Cancel = true;
                        }
                        else
                        {
                            uxWizard.MoveTo(stepFinish);
                        }

                        break;
                    }
                //case "stepSputnikFinish":
                //    {
                //        if (uxIsAddNewSputnik.SelectedItem.Value.ToString() == "1")
                //        {
                //            ClearSputnik();
                //            uxWizard.MoveTo(stepSputnikInfo);
                //        }
                //        else
                //        {
                //            uxWizard.MoveTo(stepFinish);
                //        }

                //        break;
                //    }
            }
        }

        private void ClearPacient()
        {
            uxPacientSerialNumber.Text = string.Empty;
            //uxPacientBirthDate.Date = new DateTime();
            uxPacientBirthDateDay.SelectedIndex = uxPacientBirthDateMonth.SelectedIndex = uxPacientBirthDateYear.SelectedIndex = 0;

            uxPacientCityName.Text = string.Empty;
            uxPacientFamiliya.Text = string.Empty;
            uxPacientFamiliyaEn.Text = string.Empty;
            uxPacientName.Text = string.Empty;
            uxPacientNameEng.Text = string.Empty;
        }

        private void ClearSputnik()
        {
            uxSputnikName.Text = string.Empty;
            uxSputnikNameEn.Text = string.Empty;
            uxSputnikFamiliya.Text = string.Empty;
            uxSputnikFamiliyaEn.Text = string.Empty;
            uxSputnikOtchestvo.Text = string.Empty;
            uxSputnikEmail.Text = string.Empty;
            uxSputnikContact.Text = string.Empty;
            uxSputnikBirthDate.Date = new DateTime();
            uxSputnikSeriaNumber.Text = string.Empty;
            uxSputnikRodstvo.DataBind();
            uxIsAddNewSputnik.SelectedIndex = 0;
        }

        private ClientContainer clientContainer
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
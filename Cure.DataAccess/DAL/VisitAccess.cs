using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Visit> GetVisitsForTimespan(DateTime fromTime, DateTime toTime)
        {
            return context.Visits.Where(x => x.Order.TicketPribitieTime != null && x.Order.TicketPribitieTime >= fromTime && x.Order.TicketPribitieTime < toTime).ToList();
        }

        public IEnumerable<Visit> GetOrderVisits(int orderId)
        {
            return context.Visits.Where(x => x.OrderId == orderId).OrderByDescending(o => o.CreateDate).ToList();
        }

        public IEnumerable<Visit> GetVisits()
        {
            return context.Visits.OrderByDescending(o => o.CreateDate).ToList();
        }

        public Visit GetVisit(int id)
        {
            return context.Visits.FirstOrDefault(o => o.Id == id);
        }

        public void InsertVisit(Visit visit)
        {
            try
            {
                context.Visits.AddObject(visit);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVisit(Visit visit)
        {
            try
            {
                context.Visits.Attach(visit);
                context.Visits.DeleteObject(visit);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVisit(Visit visit)
        {
            try
            {
                var origVisit = GetVisit(visit.Id);
                origVisit.AmbNumber = visit.AmbNumber;
                origVisit.DateDogovor = visit.DateDogovor;
                origVisit.Fond = visit.Fond;
                origVisit.LastUser = visit.LastUser;
                origVisit.IsInvicePrint = visit.IsInvicePrint;
                origVisit.IsInvite = visit.IsInvite;
                origVisit.LastDate = visit.LastDate;
                origVisit.LastUser = visit.LastUser;
                origVisit.Order = visit.Order;
                origVisit.OrderId = visit.OrderId;
                origVisit.PacientId = visit.PacientId;
                origVisit.Price = visit.Price;
                origVisit.Vipiska = visit.Vipiska;
                origVisit.Additional = visit.Additional;
                origVisit.TodaysDiagnoz = visit.TodaysDiagnoz;
                origVisit.HystoryA = visit.HystoryA;
                origVisit.Hystoryb = visit.Hystoryb;
                origVisit.Razvitie = visit.Razvitie;
                origVisit.Dispanser = visit.Dispanser;
                origVisit.DispanserNarko = visit.DispanserNarko;
                origVisit.Dispanser2 = visit.Dispanser2;
                origVisit.DangerousDiseases = visit.DangerousDiseases;
                origVisit.Serdce = visit.Serdce;
                origVisit.Dihalka = visit.Dihalka;
                origVisit.Infections = visit.Infections;
                origVisit.OtherDiseases = visit.OtherDiseases;
                origVisit.Epilispiya = visit.Epilispiya;
                origVisit.SudorogiType = visit.SudorogiType;
                origVisit.SudorogiCount = visit.SudorogiCount;
                origVisit.SudorogiMedcine = visit.SudorogiMedcine;
                origVisit.Remission = visit.Remission;
                origVisit.Encefalogram = visit.Encefalogram;
                origVisit.MainGoal = visit.MainGoal;
                origVisit.Razgovor = visit.Razgovor;
                origVisit.Instructcii = visit.Instructcii;
                origVisit.Fisical = visit.Fisical;
                origVisit.Diet = visit.Diet;
                origVisit.Eating = visit.Eating;
                origVisit.EatingProblems = visit.EatingProblems;
                origVisit.Appetit = visit.Appetit;
                origVisit.Stul = visit.Stul;
                origVisit.Alergiya = visit.Alergiya;
                origVisit.Imunitet = visit.Imunitet;
                origVisit.Fiznagruzki = visit.Fiznagruzki;
                origVisit.Son = visit.Son;
                origVisit.ProstupUp = visit.ProstupUp;
                origVisit.Zakativaetsa = visit.Zakativaetsa;
                origVisit.KursesRanee = visit.KursesRanee;
                origVisit.KursesChinaRanee = visit.KursesChinaRanee;
                origVisit.NonTradicial = visit.NonTradicial;
                origVisit.Hirurg = visit.Hirurg;
                origVisit.Travmi = visit.Travmi;
                origVisit.Requirements = visit.Requirements;
                origVisit.RequiredDocs = visit.RequiredDocs;
                origVisit.Postuplenie = visit.Postuplenie;
                origVisit.Resultat = visit.Resultat;
                origVisit.Recomend = visit.Recomend;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

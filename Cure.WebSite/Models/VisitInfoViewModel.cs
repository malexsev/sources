using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Web.WebPages;
    using DataAccess;

    public class VisitInfoViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TodaysDiagnoz { get; set; }
        public string HystoryA { get; set; }
        public string HystoryB { get; set; }
        public string Razvitie { get; set; }
        public string Dispanser { get; set; }
        public string DispanserNarko { get; set; }
        public string IsDispanserNarko { get; set; }
        public string Dispanser2 { get; set; }
        public string DangerousDiseases { get; set; }
        public string Serdce { get; set; }
        public string Dihalka { get; set; }
        public string Infections { get; set; }
        public string OtherDiseases { get; set; }
        public string Epilispiya { get; set; }
        public string SudorogiType { get; set; }
        public string SudorogiCount { get; set; }
        public string SudorogiMedcine { get; set; }
        public string Remission { get; set; }
        public string Encefalogram { get; set; }
        public string KursesRanee { get; set; }
        public string IsKursesRanee { get; set; }
        public string KursesChinaRanee { get; set; }
        public string IsKursesChinaRanee { get; set; }
        public string NonTradicial { get; set; }
        public string IsNonTradicial { get; set; }
        public string Hirurg { get; set; }
        public string Travmi { get; set; }


        public VisitInfoViewModel()
        {
        }

        public VisitInfoViewModel(Visit visit)
        {
            this.Id = visit.Id;
            this.Name = (!string.IsNullOrEmpty(visit.Pacient.NameEng) && !string.IsNullOrEmpty(visit.Pacient.FamiliyaEn))
                ? string.Format(" {0} {1}", visit.Pacient.NameEng, visit.Pacient.FamiliyaEn)
                : string.Empty;
            this.TodaysDiagnoz = visit.TodaysDiagnoz;
            this.HystoryA = visit.HystoryA;
            this.HystoryB = visit.Hystoryb;
            this.Razvitie = visit.Razvitie;
            this.Dispanser = string.IsNullOrEmpty(visit.Dispanser) ? "Нет" : visit.Dispanser;
            this.DispanserNarko = visit.DispanserNarko;
            this.IsDispanserNarko = (string.IsNullOrEmpty(visit.DispanserNarko) || visit.DispanserNarko == "Нет") ? "Нет" : "Да";
            this.Dispanser2 = string.IsNullOrEmpty(visit.Dispanser2) ? "Нет" : visit.Dispanser2;
            this.DangerousDiseases = string.IsNullOrEmpty(visit.DangerousDiseases) ? "Нет" : visit.DangerousDiseases;
            this.Serdce = string.IsNullOrEmpty(visit.Serdce) ? "Нет" : visit.Serdce;
            this.Dihalka = string.IsNullOrEmpty(visit.Dihalka) ? "Нет" : visit.Dihalka;
            this.Infections = string.IsNullOrEmpty(visit.Infections) ? "Нет" : visit.Infections;
            this.OtherDiseases = string.IsNullOrEmpty(visit.OtherDiseases) ? "Нет" : visit.OtherDiseases;
            this.Epilispiya = string.IsNullOrEmpty(visit.Epilispiya) ? "Нет" : visit.Epilispiya;
            this.SudorogiType = visit.SudorogiType;
            this.SudorogiCount = visit.SudorogiCount;
            this.SudorogiMedcine = visit.SudorogiMedcine;
            this.Remission = visit.Remission;
            this.Encefalogram = visit.Encefalogram;
            this.KursesRanee = visit.KursesRanee;
            this.IsKursesRanee = (string.IsNullOrEmpty(visit.KursesRanee) || visit.KursesRanee == "Нет") ? "Нет" : "Да"; 
            this.KursesChinaRanee = visit.KursesChinaRanee;
            this.IsKursesChinaRanee = (string.IsNullOrEmpty(visit.KursesChinaRanee) || visit.KursesChinaRanee == "Нет") ? "Нет" : "Да";
            this.NonTradicial = visit.NonTradicial;
            this.IsNonTradicial = (string.IsNullOrEmpty(visit.NonTradicial) || visit.NonTradicial == "Нет") ? "Нет" : "Да";
            this.Hirurg = string.IsNullOrEmpty(visit.Hirurg) ? "Нет" : visit.Hirurg;
            this.Travmi = string.IsNullOrEmpty(visit.Travmi) ? "Нет" : visit.Travmi;
        }
    }
}
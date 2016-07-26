using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using DataAccess;

    public class VisitInfoViewModel
    {
        [Key]
        public int Id { get; set; }
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
            this.TodaysDiagnoz = visit.TodaysDiagnoz;
            this.HystoryA = visit.HystoryA;
            this.HystoryB = visit.Hystoryb;
            this.Razvitie = visit.Razvitie;
            this.Dispanser = visit.Dispanser;
            this.DispanserNarko = visit.DispanserNarko;
            this.IsDispanserNarko = (string.IsNullOrEmpty(visit.DispanserNarko) || visit.DispanserNarko == "Нет") ? "Нет" : "Да";
            this.Dispanser2 = visit.Dispanser2;
            this.DangerousDiseases = visit.DangerousDiseases;
            this.Serdce = visit.Serdce;
            this.Dihalka = visit.Dihalka;
            this.Infections = visit.Infections;
            this.OtherDiseases = visit.OtherDiseases;
            this.Epilispiya = visit.Epilispiya;
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
            this.Hirurg = visit.Hirurg;
            this.Travmi = visit.Travmi;
        }
    }
}
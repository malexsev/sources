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

    public class VisitDetailViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Razgovor { get; set; }
        public string Instructcii { get; set; }
        public string Fisical { get; set; }
        public string Diet { get; set; }
        public string Eating { get; set; }
        public string Appetit { get; set; }
        public string Stul { get; set; }
        public string Alergiya { get; set; }
        public string IsAlergiya { get; set; }
        public string Imunitet { get; set; }
        public string Fiznagruzki { get; set; }
        public string Son { get; set; }
        public string ProstupUp { get; set; }
        public string Zakativaetsa { get; set; }


        public VisitDetailViewModel()
        {
        }

        public VisitDetailViewModel(Visit visit)
        {
            this.Id = visit.Id;
            this.Name = (!string.IsNullOrEmpty(visit.Pacient.NameEng) && !string.IsNullOrEmpty(visit.Pacient.FamiliyaEn))
                ? string.Format(" {0} {1}", visit.Pacient.NameEng, visit.Pacient.FamiliyaEn)
                : string.Empty;
            this.Razgovor = visit.Razgovor;
            this.Instructcii = visit.Instructcii;
            this.Fisical = visit.Fisical;
            this.Diet = visit.Diet;
            this.Eating = visit.Eating;
            this.Appetit = visit.Appetit;
            this.Stul = visit.Stul;
            this.Alergiya = visit.Alergiya;
            this.IsAlergiya = (string.IsNullOrEmpty(visit.Alergiya) || visit.Alergiya == "Нет") ? "Нет" : "Да";
            this.Imunitet = visit.Imunitet;
            this.Fiznagruzki = visit.Fiznagruzki;
            this.Son = visit.Son;
            this.ProstupUp = string.IsNullOrEmpty(visit.ProstupUp) ? "Нет" : visit.ProstupUp;
            this.Zakativaetsa = string.IsNullOrEmpty(visit.Zakativaetsa) ? "Нет" : visit.Zakativaetsa;
        }
    }
}
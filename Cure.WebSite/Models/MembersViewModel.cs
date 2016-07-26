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

    public class MembersViewModel
    {
        public PacientViewModel[] PacientArray { get; set; }
        public SputnikViewModel[] SputnikArray { get; set; }

        public MembersViewModel()
        {
        }

        public MembersViewModel(ClientContainer container)
        {
            this.PacientArray = container.NewOrder.Visits.Select(x => new PacientViewModel(x.Pacient)).ToArray();
            this.SputnikArray = container.NewOrder.Sputniks.Select(x => new SputnikViewModel(x)).ToArray();
        }
    }

    public class PacientViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Фамилия пациента")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        public string Familiya { get; set; }
        [Display(Name = "Имя пациента")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public string FamiliyaEn { get; set; }
        public string SerialNumber { get; set; }
        public string BirthDate { get; set; }
        [HiddenInput]
        [Display(Name = "Гражданство пациента")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        [RegularExpression(@"[1-9][0-9]*")]
        public int CountryId { get; set; }
        public string CityName { get; set; }

        public PacientViewModel()
        {
        }

        public PacientViewModel(Pacient pacient)
        {
            this.Id = pacient.Id;
            this.BirthDate = ((DateTime)(pacient.BirthDate ?? DateTime.Today)).ToString("dd.MM.yyyy");
            this.CityName = pacient.CityName;
            this.CountryId = pacient.CountryId;
            this.Familiya = pacient.Familiya;
            this.FamiliyaEn = pacient.FamiliyaEn;
            this.Name = pacient.Name;
            this.Otchestvo = pacient.Otchestvo;
            this.SerialNumber = pacient.SerialNumber;
        }
    }

    public class SputnikViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Фамилия сопровождающего")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        public string Familiya { get; set; }
        [Display(Name = "Имя сопровождающего")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public string FamiliyaEn { get; set; }
        public string SerialNumber { get; set; }
        [HiddenInput]
        [Display(Name = "Гражданство сопровождающего")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        [RegularExpression(@"[1-9][0-9]*")]
        public int? CountryId { get; set; }
        [HiddenInput]
        [Display(Name = "Родство сопровождающего")]
        [Required(ErrorMessage = "{0} - обязательное поле")]
        [RegularExpression(@"[1-9][0-9]*")]
        public int RodstvoId { get; set; }
        [Display(Name = "Email сопровождающего")]
        [EmailAddress(ErrorMessage = "{0} - неверный формат адреса")]
        public string Email { get; set; }
        public string Contacts { get; set; }
        public string BirthDate { get; set; }

        public SputnikViewModel()
        {
        }

        public SputnikViewModel(Sputnik sputnik)
        {
            this.Id = sputnik.Id;
            this.BirthDate = ((DateTime)(sputnik.BirthDate ?? DateTime.Today)).ToString("dd.MM.yyyy");
            this.Email = sputnik.Email;
            this.Contacts = sputnik.Contacts;
            this.CountryId = sputnik.CountryId;
            this.RodstvoId = sputnik.RodstvoId;
            this.Familiya = sputnik.Familiya;
            this.FamiliyaEn = sputnik.FamiliyaEn;
            this.Name = sputnik.Name;
            this.Otchestvo = sputnik.Otchestvo;
            this.SerialNumber = sputnik.SeriaNumber;
        }
    }
}
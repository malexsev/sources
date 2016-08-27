using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SocialLinksModel
    {
        [Display(Name = "Одноклассники")]
        [Url(ErrorMessage = "Введите корректную ссылку")]
        public string SocialOk { get; set; }
        [Display(Name = "ВКонтакте")]
        [Url(ErrorMessage = "Введите корректную ссылку")]
        public string SocialVk { get; set; }
        [Display(Name = "Мой Мир")]
        [Url(ErrorMessage = "Введите корректную ссылку")]
        public string SocialMm { get; set; }
        [Display(Name = "Фейсбук")]
        [Url(ErrorMessage = "Введите корректную ссылку")]
        public string SocialFb { get; set; }
        [Display(Name = "Инстаграм")]
        [Url(ErrorMessage = "Введите корректную ссылку")]
        public string SocialYoutube { get; set; }

        public SocialLinksModel()
        {
        }

        public SocialLinksModel(ChildVisualDetailed container)
        {
            this.SocialOk = container.SocialOk;
            this.SocialVk = container.SocialVk;
            this.SocialMm = container.SocialMm;
            this.SocialFb = container.SocialFb;
            this.SocialYoutube = container.SocialYoutube;
        }
    }
}
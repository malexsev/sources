using System.IO;

namespace Cure.WebSite.Models
{
    using Utils;

    public class PhotoItem
    {
        public string UrlOriginal { get; set; }
        public string UrlMin { get; set; }

        public PhotoItem(int id)
        {
            string uri = string.Format("/Content/img/userpics/no_photo_{0}.jpg",
                    SiteUtils.GetRandom(id, 3));
            UrlOriginal = UrlMin = uri;
        }

        public PhotoItem(string photoUrl, string fileName)
        {
            UrlOriginal = Path.Combine(photoUrl, fileName);
            UrlMin = Path.Combine(photoUrl + "/Thumb/", fileName);
        }
    }
}
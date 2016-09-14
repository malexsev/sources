using System.IO;

namespace Cure.WebSite.Models
{
    public class DocItem
    {
        public string UrlOriginal { get; set; }

        public DocItem()
        {
            UrlOriginal = "/content/img/userpics/no_photo_min.jpg";
        }

        public DocItem(string photoUrl, string fileName)
        {
            UrlOriginal = Path.Combine(photoUrl, fileName);
        }
    }
}
using System.IO;

namespace Cure.WebSite.Models
{
    public class DocItem
    {
        public string UrlOriginal { get; set; }

        public DocItem()
        {
            UrlOriginal = "/Content/images/no_photo_big.jpg";
        }

        public DocItem(string photoUrl, string fileName)
        {
            UrlOriginal = Path.Combine(photoUrl, fileName);
        }
    }
}
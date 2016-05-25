using System.IO;

namespace Cure.WebSite.Models
{
    public class PhotoItem
    {
        public string UrlOriginal { get; set; }
        public string UrlMin { get; set; }
        public string UrlBig { get; set; }

        public PhotoItem()
        {
            UrlOriginal = "/Content/images/no_photo_big.jpg";
            UrlMin = "/Content/images/no_photo_min.jpg";
            UrlBig = "/Content/images/no_photo_big.jpg";
        }

        public PhotoItem(string photoUrl, string fileName)
        {
            UrlOriginal = Path.Combine(photoUrl, fileName);
            UrlMin = Path.Combine(photoUrl + "/Thumb/", fileName);
            UrlBig = Path.Combine(photoUrl + "/Big/", fileName);
        }
    }
}
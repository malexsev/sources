namespace Cure.Utils
{
    using System;
    using System.IO;
    using System.Web.UI;

    public class ReportUtils
    {
        private const string HelperUri = "~/Content/ReportingHelpers";
        private const string HelperExtension = "png";
        private const string UriFormat = "{0}/{1}-{2}.{3}";

        public static string GetRandomFile(string prefix, Page page)
        {
            string filePath = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                string relationPath = string.Format(UriFormat, HelperUri, prefix, i, HelperExtension);
                if (File.Exists(page.MapPath(relationPath)))
                {
                    filePath = page.MapPath(relationPath);
                    break;
                }
            }

            return filePath;
        }

        public static int GetRandomCorrection(int valatil)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int correction = rnd.Next(-valatil / 2, valatil / 2);
            return correction;
        }
    }
}

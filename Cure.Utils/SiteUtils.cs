namespace Cure.Utils
{
    using System;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Web.Security;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DevExpress.XtraPrinting;
    using Reports;
    using Page = System.Web.UI.Page;

    public class SiteUtils
    {
        public static string GenerateVisitDetailsPdf(Visit visit, string attachmentName, Page page)
        {
            var report = new PacientVisitDetails(visit.Id, page);
            var folderPath = Path.Combine(@"~\Documents\", visit.Order.GuidId + "\\");
            var fileName = String.Format("{0}", attachmentName);
            var pdfFullPath = page.MapPath(Path.Combine(folderPath, fileName));

            FileUtils.CreateFolderIfNotExists(page, folderPath);

            if (File.Exists(pdfFullPath))
            {
                File.Delete(pdfFullPath);
            }

            var options = new ImageExportOptions { Resolution = 180, Format = ImageFormat.Png };
            using (var fs = new FileStream(pdfFullPath, FileMode.Append))
            {
                report.ExportToPdf(fs);
            }
            report.Dispose();

            return pdfFullPath;
        }

        public static string GetCurrentUserName()
        {
            return new Regex(@"(.|\n)+\\").Replace(HttpContext.Current.User.Identity.Name, "");
        }

        public static bool IsAdmin()
        {
            return Roles.IsUserInRole(GetCurrentUserName(), "Администратор");
        }

        public static bool IsAdmin(string username)
        {
            return Roles.IsUserInRole(username, "Администратор");
        }

        public static bool IsTrans(string username)
        {
            return Roles.IsUserInRole(username, "Трансфер-менеджер");
        }

        public static string GetReisNumber(object value)
        {
            string result = value == null ? string.Empty : value.ToString().Trim();
            if (!string.IsNullOrEmpty(result.ToString(CultureInfo.InvariantCulture)))
            {
                string[] arr = result.Trim().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.GetUpperBound(0) == 0)
                {
                    return result.Replace("|", " ");
                }
                else
                {
                    return result;
                }
            }
            return result;
        }

        //public static string GetReisNumberLogist(object value)
        //{
        //    string result = value == null ? string.Empty : value.ToString().Trim();
        //    if (result == "0000")
        //    {
        //        return "До Пекина есть билеты, до Юньченга отсутствуют / 意思是已经到达北京，没有到运城的票";
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(result.ToString(CultureInfo.InvariantCulture)))
        //        {
        //            string[] arr = result.Trim().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        //            if (arr.GetUpperBound(0) == 0)
        //            {
        //                return result.Replace("|", " ");
        //            }
        //            else
        //            {
        //                return result;
        //            }
        //        }
        //    }
        //    return result;
        //}
    }
}

namespace Cure.Utils
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Security;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting;
    using Reports;
    using Page = System.Web.UI.Page;

    public class SiteUtils
    {
        /// <summary>
        /// Возвращает возраст по дате роджения.
        /// </summary>
        public static int GetAge(DateTime birthday)
        {
            if (birthday.Year > 1900 && birthday.Year < 2100)
            {
                if (birthday > DateTime.Today)
                {
                    return -1;
                }
                return new DateTime(((DateTime.Today - birthday)).Ticks).Year - 1;
            }
            return 0;
        }

        /// <summary>
        /// Обновляет информацию в отзывах и постах, при изменении данных профиля (имя, страна, регион)
        /// </summary>
        /// <param name="child"></param>
        /// <param name="dal"></param>
        public static void UpdatePostsInfo(ref Child child, ref DataAccessBL dal)
        {
            var location = ConcatLocation(child.RefCountry == null ? "" : child.RefCountry.Name, child.Region);
            var name = child.ContactName;

            var mensions = dal.GetMensionsByUser(child.OwnerUser).ToList();
            var posts = dal.GetPostsByOwner(child.OwnerUser).ToList();
            var internalDal = dal;

            mensions.ForEach(x =>
            {
                x.CopyUserLocation = location;
                x.CopyUserName = name;
                internalDal.UpdateMension(x);
            });

            posts.ForEach(x =>
            {
                x.CopyOwnerLocation = location;
                x.CopyOwnerName = name;
                internalDal.UpdatePost(x);
            });
        }

        /// <summary>
        /// По указанной ширине обрезает текст и ставит три точки в конце. Если текст меньше, выводит в оригинале.
        /// </summary>
        public static string CutText(string text, int length)
        {
            if (text.Length < length)
            {
                return text;
            }
            return string.Format("{0}...", text.Substring(0, length));
        }

        public static string ConcatLocation(object country, object location)
        {
            if (string.IsNullOrEmpty(location.ToString()))
            {
                return country.ToString();
            }
            return string.Format("{0}, {1}", country, location);
        }

        public static string ReplaceSmiles(string text)
        {
            return text.Replace(":)", "<img src='/Content/img/smiles/smile_01.png' alt='__'>")
                .Replace(":D", "<img src='/Content/img/smiles/smile_02.png' alt='__'>")
                .Replace(";)", "<img src='/Content/img/smiles/smile_03.png' alt='__'>")
                .Replace(":angel:", "<img src='/Content/img/smiles/smile_04.png' alt='__'>")
                .Replace(":*", "<img src='/Content/img/smiles/smile_05.png' alt='__'>")
                .Replace(":(", "<img src='/Content/img/smiles/smile_06.png' alt='__'>")
                .Replace(":tears:", "<img src='/Content/img/smiles/smile_07.png' alt='__'>")
                .Replace(":devil:", "<img src='/Content/img/smiles/smile_08.png' alt='__'>")
                .Replace(":hate:", "<img src='/Content/img/smiles/smile_09.png' alt='__'>")
                .Replace(":like:", "<img src='/Content/img/smiles/smile_10.png' alt='__'>")
                .Replace(":dislike:", "<img src='/Content/img/smiles/smile_11.png' alt='__'>")
                .Replace(":love:", "<img src='/Content/img/smiles/smile_12.png' alt='__'>");

        }

        public static string GetUserUserpic(string userpicUrl,
            Guid? guid,
            string filename)
        {
            if (guid != null && !string.IsNullOrEmpty(filename))
            {
                return Path.Combine(userpicUrl, filename);
            }

            return "/content/img/userpics/no_photo_min.jpg";
        }

        public static string GetMyUserpic(HttpServerUtilityBase server,
            HttpSessionStateBase session,
            string username,
            string photoLocation,
            string photoUrl)
        {
            if (session["UserpicUrl"] == null && !string.IsNullOrEmpty(username))
            {
                var dal = new DataAccessBL();
                var view = dal.ViewChild(username);
                if (view != null)
                {
                    session["UserContactName"] = string.IsNullOrEmpty(view.ContactName) ? username : view.ContactName;
                    if (!string.IsNullOrEmpty(view.OwnerUserPic))
                    {
                        photoUrl = Path.Combine(photoUrl, view.GuidId.ToString());
                        string userpicUrl = photoUrl.Replace("Upload", "Userpics");
                        var result = Path.Combine(userpicUrl, view.OwnerUserPic);
                        session["UserpicUrl"] = result;
                        return result;
                    }
                }
            }
            var res = session["UserpicUrl"] ?? "/content/img/userpics/no_photo_min.jpg";
            return res.ToString();
        }

        /// <summary>
        /// Возвращает число от 1 до upto, в зависимости от указанного ID
        /// </summary>
        /// <returns></returns>
        public static string GetRandom(int id, int upto)
        {
            string thelast = id.ToString().Substring(id.ToString().Length - 1, 1);
            if (thelast == "1" || thelast == "2" || thelast == "3")
            {
                return "1";
            }
            if (thelast == "4" || thelast == "5" || thelast == "6")
            {
                return "2";
            }
            return "3";
        }

        public static string GenerateVisitDetailsPdf(Visit visit, string attachmentName, HttpServerUtilityBase server, bool isHideFile = true)
        {
            var dal = new DataAccessBL();
            var user = dal.GetUserMembership(visit.Order.OwnerUser);
            var report = new PacientVisitDetails(visit.Id);
            var folderPath = Path.Combine(@"~\Documents\", user.Expr1 + (isHideFile ? "" : @"\UserFiles\"));
            var fileName = String.Format("{0}", attachmentName);
            var pdfFullPath = server.MapPath(Path.Combine(folderPath, fileName));

            FileUtils.CreateFolderIfNotExists(server, folderPath);

            if (File.Exists(pdfFullPath))
            {
                File.Delete(pdfFullPath);
            }

            using (var fs = new FileStream(pdfFullPath, FileMode.Append))
            {
                report.ExportToPdf(fs);
            }
            report.Dispose();

            if (!isHideFile)
            {
                UploadLogging(ref dal, fileName, pdfFullPath, user.Expr1, user.UserName);
            }

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

        public static DateTime ParseDate(object originValue, DateTime defaultValue, string culture)
        {
            if (originValue == null) return defaultValue;
            var provider = new CultureInfo(culture);
            DateTime dateFrom;
            if (!DateTime.TryParse(originValue.ToString(), provider, DateTimeStyles.None, out dateFrom))
            {
                if (!DateTime.TryParseExact(originValue.ToString(), "dd.MM.yyyy", provider, DateTimeStyles.None, out dateFrom))
                {
                    dateFrom = defaultValue;
                }
            }
            return dateFrom;
        }

        public static Guid ParseGuid(object originValue)
        {
            Guid defaultValue = Guid.Empty;
            if (originValue == null) return defaultValue;
            Guid result;
            if (!Guid.TryParse(originValue.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        public static bool ParseBool(object originValue, bool defaultValue)
        {
            if (originValue == null) return defaultValue;
            bool result;
            if (!Boolean.TryParse(originValue.ToString(), out result))
            {
                if (originValue.ToString() == "0")
                {
                    result = false;
                }
                else if (originValue.ToString() == "1")
                {
                    result = true;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        public static int ParseInt(object originValue, int defaultValue)
        {
            if (originValue == null) return defaultValue;
            originValue = originValue.ToString().Replace(".00", string.Empty).Replace(",00", string.Empty);
            int result;
            if (!Int32.TryParse(originValue.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        public static byte ParseByte(object originValue, byte defaultValue)
        {
            if (originValue == null)
                return defaultValue;
            byte result;
            if (!Byte.TryParse(originValue.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        public static decimal ParseDecimal(object originValue, decimal defaultValue)
        {
            if (originValue == null)
                return defaultValue;
            decimal result;
            if (!Decimal.TryParse(originValue.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        private static void UploadLogging(ref DataAccessBL dal, string filename, string fullPath, Guid userGuid, string userName)
        {
            var uploadLog = new UploadLog()
            {
                FileName = filename,
                GuidId = userGuid,
                ServerPath = fullPath,
                UploadDate = DateTime.Now,
                Username = userName,
                IsReported = false
            };
            dal.InsertUploadLog(uploadLog);
        }
    }
}

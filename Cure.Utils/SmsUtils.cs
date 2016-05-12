namespace Cure.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using DataAccess;
    using DataAccess.BLL;
    using DataAccess.Enums;

    public static class SmsUtils
    {

        private static string bytehandId = "23485";
        private static string bytehandKey = "95A672F3713233ED";
        private static string bytehandFrom = "dcp-china";

        public static bool SendSms(string to, string text, string reason)
        {
            to = FormatPhone(to);
            string vals = "http://bytehand.com:3800/send?id=" + bytehandId + "&key=" + bytehandKey + "&to=" +
                                Uri.EscapeUriString(to) + "&from=" + Uri.EscapeUriString(bytehandFrom) +
                                "&text=" + Uri.EscapeUriString(text);
            var request = (HttpWebRequest)
            WebRequest.Create(vals);
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    Log(to, text, reason, "Success");
                    return true;
                }
            } catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    var httpResponse = (HttpWebResponse)response;
                    string errorCode = String.Format("Error code: {0}", httpResponse.StatusCode);
                    if (response != null)
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            Log(to, text, reason, errorCode.Length > 50 ? errorCode.Substring(1, 50) : errorCode, String.Format("Отказ отправки СМС по номеру {0}", to));
                        }
                    return false;
                }
            }
        }

        public static string FormatPhone(string phone)
        {
            return phone
                .Replace("_", String.Empty)
                .Replace("-", String.Empty)
                .Replace(" ", String.Empty)
                .Replace("(", String.Empty)
                .Replace(")", String.Empty);
        }

        private static void Log(string to, string text, string reason, string success, string description = "")
        {
            var bll = new DataAccessBL();
            var smsLog = new SmsLog
            {
                Addition = success,
                Date = DateTime.Now,
                Description = description,
                PhoneNumber = to,
                Reason = reason,
                Text = text
            };
            bll.InsertSmsLog(smsLog);
        }
    }
}

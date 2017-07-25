using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.Security.Policy;
    using System.Text.RegularExpressions;
    using DataAccess;
    using Ninject.Activation;

    public class NewsPageModel : NewsPage
    {
        private HttpRequestBase _request;

        public NewsPageModel() { }

        public NewsPageModel(NewsPage basis, System.Web.HttpRequestBase request)
        {
            _request = request;
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(basis))
            {
                var val = item.GetValue(basis);
                if (val == null)
                {
                    continue;
                }
                if (item.Name == "Text")
                {
                    if (val.ToString().Contains("src=\"/"))
                    {
                        var url = new Uri(ConfigurationManager.AppSettings["PhotoUrl"].Replace("/Upload/", "")).ToString();
                        item.SetValue(this, val.ToString().Replace("src=\"/", string.Format("src=\"{0}/", url)));
                    }
                    else
                    {
                        item.SetValue(this, val);
                    }
                }
                else
                {
                    item.SetValue(this, val);
                }
            }
        }

        public string MainPictureSrc(HttpServerUtilityBase server)
        {
            return GetAbsoluteUri(this.Settings).ToString();
        }

        private Uri GetAbsoluteUri(string redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = "/Content/images/no_photo.jpg";
            }

            var redirectUri = new Uri(redirectUrl, UriKind.RelativeOrAbsolute);

            if (!redirectUri.IsAbsoluteUri && _request != null)
            {
                if (_request.Url != null)
                {
                    redirectUri = new Uri(new Uri(ConfigurationManager.AppSettings["PhotoUrl"].Replace("/Upload/", "")), redirectUri);
                }
            }

            return redirectUri;
        }
    }


}
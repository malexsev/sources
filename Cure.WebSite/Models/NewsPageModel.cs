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

        public NewsPageModel(NewsPage basis, System.Web.HttpRequestBase request )
        {
            _request = request;
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(basis))
            {
                var val = item.GetValue(basis);
                if (val == null)
                {
                    continue;
                }
                item.SetValue(this, val);
            }
        }

        public string MainPictureSrc
        {
            get
            {
                var src = Regex.Match(this.Text, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                var url = GetAbsoluteUri(src);
                return url.ToString();
            }
        }

        private Uri GetAbsoluteUri(string redirectUrl)
        {
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
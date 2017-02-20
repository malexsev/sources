namespace Cure.WebAdmin.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using DataAccess.BLL;
    using DevExpress.Web.ASPxCallback;
    using DevExpress.Web.ASPxClasses;
    using Microsoft.VisualBasic;
    using Notification;
    using Page = System.Web.UI.Page;

    public partial class NewsLetter : Page
    {
        protected void uxMainGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            //e.Row["Text"]
        }

        protected void uxCallback_OnCallback(object s, CallbackEventArgs e)
        {
            int newsId;
            var dal = new DataAccessBL();
            if (!string.IsNullOrEmpty(e.Parameter) && int.TryParse(Request.QueryString["newspageid"], out newsId))
            {
                var email = e.Parameter;
                try
                {
                    var newspage = dal.GetNewsPage(newsId);
                    var notify = new NewsNotification(email, newspage.Subject, newspage.Text,
                        new HttpServerUtilityWrapper(Server));
                    var res = notify.Send();
                    var subscriber = dal.GetNewsletter(email);
                    if (subscriber != null)
                    {
                        if (res)
                        {
                            subscriber.SuccessCount = (subscriber.SuccessCount ?? 0) + 1;
                        }
                        else
                        {
                            subscriber.ErrorsCount = (subscriber.ErrorsCount ?? 0) + 1;
                        }
                        dal.UpdateNewsletter(subscriber);
                    }

                    e.Result = res ? "OK" : this.GetErrorMessage(email);
                }
                catch
                {
                    e.Result = this.GetErrorMessage(email);
                }
            }
            else
            {
                e.Result = "NONE";
            }
        }

        private string GetErrorMessage(string email)
        {
            return string.Format("<font color='red'>{0} - ошибка отправки</font><br>", email);
        }
    }
}
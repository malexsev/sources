using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using Logic;
    using Notification;

    public partial class Step4Steady : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindControls();
            }
        }

        protected void uxSave_Click(object sender, EventArgs e)
        {
            clientContainer.CurrentOrder.TicketInfo = String.Format("{0} | {1}", uxPribitieInfo.Text, uxUbytieInfo.Text);
            clientContainer.CurrentOrder.TicketPribitieTime = uxPribitieTime.Date.Year > 2000 ? (DateTime?)uxPribitieTime.Date : null;
            clientContainer.CurrentOrder.TicketUbitieTime = uxUbytieTime.Date.Year > 2000 ? (DateTime?)uxUbytieTime.Date : null;
            clientContainer.CurrentOrder.VizaDney = Convert.ToInt32(uxVisaDney.Value ?? 0);
            clientContainer.CurrentOrder.DateTo = uxDateTo.Date.Year > 2000 ? uxDateTo.Date : clientContainer.CurrentOrder.DateTo;
            clientContainer.CurrentOrder.StatusId = 7;
            clientContainer.UpdateCurrentOrder();

            var notify = new TiketsBoughtEmailNotification(clientContainer.CurrentOrder.Id);
            notify.Send();
            var notifyToUser = new TicketsBoughtToUserEmailNotification(clientContainer.CurrentOrder.Id);
            notifyToUser.Send();

            Response.Redirect("~/Client/CurrentOrder.aspx");
        }

        private void BindControls()
        {
            if (clientContainer.CurrentOrder != null)
            {
                if (!String.IsNullOrEmpty(clientContainer.CurrentOrder.TicketInfo))
                {
                    if (clientContainer.CurrentOrder.TicketInfo.Contains(" | "))
                    {
                        var stringSeparators = new string[] { " | " };
                        string[] borts = clientContainer.CurrentOrder.TicketInfo.Split(stringSeparators, new StringSplitOptions());
                        if (borts.Length > 0)
                        {
                            uxPribitieInfo.Text = borts[0];
                        }
                        if (borts.Length > 1)
                        {
                            uxUbytieInfo.Text = borts[1];
                        }
                    } else
                    {
                        uxPribitieInfo.Text = clientContainer.CurrentOrder.TicketInfo;
                    }
                }

                if (clientContainer.CurrentOrder.TicketPribitieTime.HasValue)
                {
                    uxPribitieTime.Date = (DateTime)clientContainer.CurrentOrder.TicketPribitieTime;
                }
                if (clientContainer.CurrentOrder.TicketUbitieTime.HasValue)
                {
                    uxUbytieTime.Date = (DateTime)clientContainer.CurrentOrder.TicketUbitieTime;
                }
                if (clientContainer.CurrentOrder.VizaDney.HasValue)
                {
                    uxVisaDney.Value = clientContainer.CurrentOrder.VizaDney ?? null;
                }

                uxDateTo.Date = clientContainer.CurrentOrder.DateTo;
            }
        }

        protected ClientContainer clientContainer
        {
            get
            {
                if (HttpContext.Current.Session["ClientContainer"] == null)
                {
                    HttpContext.Current.Session["ClientContainer"] = new ClientContainer(Utils.SiteUtils.GetCurrentUserName());
                }
                return (ClientContainer)HttpContext.Current.Session["ClientContainer"];
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session["ClientContainer"] = value;
                }
            }
        }
    }
}
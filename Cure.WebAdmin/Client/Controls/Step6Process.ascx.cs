using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using DevExpress.Charts.Model;
    using Logic;

    public partial class Step6Process : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindControls();
            }
        }

        private void BindControls()
        {
            if (clientContainer.CurrentOrder != null)
            {
                uxDateTo.Date = clientContainer.CurrentOrder.DateTo;
            }
        }

        protected void uxSave_Click(object sender, EventArgs e)
        {
            clientContainer.CurrentOrder.DateTo = uxDateTo.Date.Year > 2000 ? uxDateTo.Date : clientContainer.CurrentOrder.DateTo;

            clientContainer.UpdateCurrentOrder();
            uxResultBox.TextGreen = "Данные сохранены.";
            uxClientCurrOrder.RefreshGrid();
        }

        private ClientContainer clientContainer
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client
{
    using DataAccess.BLL;
    using DevExpress.Web.ASPxGridView;
    using Logic;
    using Utils;

    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Session["CurrentUserName"] = SiteUtils.GetCurrentUserName();
        }

        protected void uxMainGrid_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded && e.VisibleIndex >= 0)
            {
                var orderId = (int)uxMainGrid.GetRowValues(e.VisibleIndex, "Id");
                Session["ExpandOrderId"] = orderId;
            }
        }

        public int GetVisitIdByPacientId(object pacientId)
        {
            int pacId;
            if (int.TryParse(pacientId.ToString(), out pacId))
            {
                var dal = new DataAccessBL();
                var visit = dal.GetVisits()
                    .Where(x => x.Order.OwnerUser == SiteUtils.GetCurrentUserName())
                    .OrderByDescending(x => x.CreateDate)
                    .FirstOrDefault(x => x.PacientId == pacId);
                if (visit != null)
                {
                    return visit.Id;
                }
            }
            return 0;
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
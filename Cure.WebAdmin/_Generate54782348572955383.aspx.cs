namespace Cure.WebAdmin
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using DataAccess.BLL;
    using DevExpress.XtraCharts.Native;
    using Notification;
    using Utils;

    public partial class _Generate54782348572955383 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int visitId;
            if (!IsPostBack && int.TryParse(Request.QueryString["visitid"], out visitId))
            {
                var dal = new DataAccessBL();
                var visit = dal.GetVisit(visitId);
                var notifyEmail = new OrderSentEmailNotification(visitId, new HttpServerUtilityWrapper(this.Server));
                notifyEmail.Send();
                var notifyEmailToUser = new OrderSentToUserEmailNotification(visit.Order.OwnerUser, new HttpServerUtilityWrapper(Server));
                notifyEmailToUser.Send();
            }
        }
    }
}
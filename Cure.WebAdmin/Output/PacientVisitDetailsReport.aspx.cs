

namespace Cure.WebAdmin.Reports
{
    using System;
    using Cure.Reports;
    using Cure.DataAccess.BLL;
    using Utils;

    public partial class PacientVisitDetailsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int visitId;
            if (int.TryParse(Request.QueryString["VisitId"], out visitId))
            {
                var dal = new DataAccessBL();
                var visit = dal.GetVisit(visitId);
                if (visit != null)
                {
                    var userName = SiteUtils.GetCurrentUserName();
                    var isAdmin = SiteUtils.IsAdmin(userName);
                    if (userName != visit.Order.OwnerUser && isAdmin == false)
                    {
                        throw new Exception("Запрашиваемые на сервере данные связанные с Вашей учётной записью не найдены. Свяжитесь с адиминистрацией для уточнения деталей, возможно это легко исправить.");
                    } else
                    {
                        var report = new PacientVisitDetails(visitId);
                        uxDocumentViewer.Report = report;
                    }
                } else
                {
                    throw new Exception("Запрашиваемые на сервере данные связанные с Вашей учётной записью не найдены. Свяжитесь с адиминистрацией для уточнения деталей, возможно это легко исправить.");
                }
            }
        }
    }
}
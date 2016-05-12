using System;
using Cure.Reports;

namespace Cure.WebAdmin.Reports
{
    public partial class VisitInvitationReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int visitId;
            if (int.TryParse(Request.QueryString["VisitId"], out visitId))
            {
                var report = new VisitInvitation(visitId, this);
                uxDocumentViewer.Report = report;
            }
        }
    }
}
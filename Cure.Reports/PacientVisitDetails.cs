namespace Cure.Reports
{
    using System;
    using DataAccess;
    using DataAccess.BLL;
    using Datasets;
    using DevExpress.XtraReports.UI;

    public partial class PacientVisitDetails : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        private System.Web.UI.Page page;
        private int visitId;

        public PacientVisitDetails(int visitId, System.Web.UI.Page page)
        {
            InitializeComponent();
            this.page = page;
            this.visitId = visitId;

            var dt = new VisitInvitationDataset.dsVistInvitationDataTable();
            this.dataTableAdapter.Fill(dt, visitId);
            uxSputniksSubreport.ReportSource = new PacientVisitDetailsSputniks(visitId);
        }
    }
}

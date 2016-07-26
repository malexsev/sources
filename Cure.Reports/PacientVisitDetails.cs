namespace Cure.Reports
{
    using System;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using Datasets;
    using DevExpress.XtraReports.UI;

    public partial class PacientVisitDetails : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        private int visitId;

        public PacientVisitDetails(int visitId)
        {
            InitializeComponent();
            this.visitId = visitId;

            var dt = new VisitInvitationDataset.dsVistInvitationDataTable();
            this.dataTableAdapter.Fill(dt, visitId);
            uxSputniksSubreport.ReportSource = new PacientVisitDetailsSputniks(visitId);
        }
    }
}

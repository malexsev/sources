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

        private void xrLabel38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var text = sender;
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var text = sender;
        }
    }
}

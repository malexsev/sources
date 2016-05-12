
namespace Cure.Reports
{
    using System;
    using System.Globalization;
    using Datasets;
    using DevExpress.XtraReports.UI;

    public partial class PacientVisitDetailsSputniks : DevExpress.XtraReports.UI.XtraReport
    {
        public PacientVisitDetailsSputniks(int visitId)
        {
            InitializeComponent();

            var dt = new VisitInvitationSputniksDataset.dsVistInvitationSputniksDataTable();
            this.visitInvitationSputniksTableAdapter.Fill(dt, visitId);
        }
    }
}

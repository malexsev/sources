namespace Cure.Reports
{
    partial class VisitInvitationSputniks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.uxSputnikInfoRu = new DevExpress.XtraReports.UI.XRLabel();
            this.uxSputnikInfoCh = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.visitInvitationDataset1 = new Cure.Reports.Datasets.VisitInvitationDataset();
            this.dataTableAdapter = new Cure.Reports.Datasets.VisitInvitationDatasetTableAdapters.VisitInvitationTableAdapter();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.visitInvitationSputniksDataset1 = new Cure.Reports.Datasets.VisitInvitationSputniksDataset();
            this.visitInvitationSputniksTableAdapter = new Cure.Reports.Datasets.VisitInvitationSputniksDatasetTableAdapters.VisitInvitationSputniksTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.visitInvitationDataset1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitInvitationSputniksDataset1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.uxSputnikInfoRu,
            this.uxSputnikInfoCh});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 70.80002F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // uxSputnikInfoRu
            // 
            this.uxSputnikInfoRu.Dpi = 96F;
            this.uxSputnikInfoRu.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uxSputnikInfoRu.ForeColor = System.Drawing.Color.DimGray;
            this.uxSputnikInfoRu.KeepTogether = true;
            this.uxSputnikInfoRu.LocationFloat = new DevExpress.Utils.PointFloat(20.8F, 29.88002F);
            this.uxSputnikInfoRu.Multiline = true;
            this.uxSputnikInfoRu.Name = "uxSputnikInfoRu";
            this.uxSputnikInfoRu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.uxSputnikInfoRu.SizeF = new System.Drawing.SizeF(649.6F, 40.92F);
            this.uxSputnikInfoRu.StylePriority.UseFont = false;
            this.uxSputnikInfoRu.StylePriority.UseForeColor = false;
            this.uxSputnikInfoRu.StylePriority.UseTextAlignment = false;
            this.uxSputnikInfoRu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.uxSputnikInfoRu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.uxSputnikInfoRu_BeforePrint);
            this.uxSputnikInfoRu.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.uxSputnikInfoRu_PrintOnPage);
            // 
            // uxSputnikInfoCh
            // 
            this.uxSputnikInfoCh.Dpi = 96F;
            this.uxSputnikInfoCh.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uxSputnikInfoCh.ForeColor = System.Drawing.Color.DimGray;
            this.uxSputnikInfoCh.KeepTogether = true;
            this.uxSputnikInfoCh.LocationFloat = new DevExpress.Utils.PointFloat(20.79995F, 1.220703E-05F);
            this.uxSputnikInfoCh.Multiline = true;
            this.uxSputnikInfoCh.Name = "uxSputnikInfoCh";
            this.uxSputnikInfoCh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.uxSputnikInfoCh.SizeF = new System.Drawing.SizeF(649.6F, 29.88001F);
            this.uxSputnikInfoCh.StylePriority.UseFont = false;
            this.uxSputnikInfoCh.StylePriority.UseForeColor = false;
            this.uxSputnikInfoCh.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.uxSputnikInfoCh_BeforePrint);
            this.uxSputnikInfoCh.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.uxSputnikInfoCh_PrintOnPage);
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // visitInvitationDataset1
            // 
            this.visitInvitationDataset1.DataSetName = "VisitInvitationDataset";
            this.visitInvitationDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataTableAdapter
            // 
            this.dataTableAdapter.ClearBeforeFill = true;
            // 
            // formattingRule1
            // 
            // 
            // 
            // 
            this.formattingRule1.Formatting.ForeColor = System.Drawing.Color.DimGray;
            this.formattingRule1.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // visitInvitationSputniksDataset1
            // 
            this.visitInvitationSputniksDataset1.DataSetName = "VisitInvitationSputniksDataset";
            this.visitInvitationSputniksDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // visitInvitationSputniksTableAdapter
            // 
            this.visitInvitationSputniksTableAdapter.ClearBeforeFill = true;
            // 
            // VisitInvitationSputniks
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataAdapter = this.visitInvitationSputniksTableAdapter;
            this.DataMember = "dsVistInvitationSputniks";
            this.DataSource = this.visitInvitationSputniksDataset1;
            this.Dpi = 96F;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(0, 40, 0, 0);
            this.PageHeight = 1123;
            this.PageWidth = 794;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Pixels;
            this.Version = "14.1";
            ((System.ComponentModel.ISupportInitialize)(this.visitInvitationDataset1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitInvitationSputniksDataset1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private Datasets.VisitInvitationDataset visitInvitationDataset1;
        private Datasets.VisitInvitationDatasetTableAdapters.VisitInvitationTableAdapter dataTableAdapter;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRLabel uxSputnikInfoRu;
        private DevExpress.XtraReports.UI.XRLabel uxSputnikInfoCh;
        private Datasets.VisitInvitationSputniksDataset visitInvitationSputniksDataset1;
        private Datasets.VisitInvitationSputniksDatasetTableAdapters.VisitInvitationSputniksTableAdapter visitInvitationSputniksTableAdapter;
    }
}

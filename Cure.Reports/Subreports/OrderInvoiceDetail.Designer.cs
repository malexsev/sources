namespace Cure.Reports
{
    partial class OrderInvoiceDetail
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
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.uxPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.uxDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.uxName = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.visitInvitationDataset1 = new Cure.Reports.Datasets.VisitInvitationDataset();
            this.dataTableAdapter =
                new Cure.Reports.Datasets.VisitInvitationDatasetTableAdapters.VisitInvitationTableAdapter();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.visitInvitationSputniksDataset1 = new Cure.Reports.Datasets.VisitInvitationSputniksDataset();
            this.visitInvitationSputniksTableAdapter =
                new Cure.Reports.Datasets.VisitInvitationSputniksDatasetTableAdapters.
                    VisitInvitationSputniksTableAdapter();
            this.FullName = new DevExpress.XtraReports.UI.CalculatedField();
            this.FullNameEn = new DevExpress.XtraReports.UI.CalculatedField();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize) (this.visitInvitationDataset1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.visitInvitationSputniksDataset1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[]
            {
                this.xrLine2,
                this.xrLabel29,
                this.uxPrice,
                this.uxDescription,
                this.uxName
            });
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 17F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 96F;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(35.79999F, 14F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(722.4F, 3F);
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 96F;
            this.xrLabel29.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.xrLabel29.ForeColor = System.Drawing.Color.Black;
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(709.4F, 1F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(48.79993F, 12.08002F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseForeColor = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "юаней";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // uxPrice
            // 
            this.uxPrice.Dpi = 96F;
            this.uxPrice.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.uxPrice.ForeColor = System.Drawing.Color.Black;
            this.uxPrice.LocationFloat = new DevExpress.Utils.PointFloat(630.9999F, 1F);
            this.uxPrice.Name = "uxPrice";
            this.uxPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.uxPrice.SizeF = new System.Drawing.SizeF(68F, 12.08001F);
            this.uxPrice.StylePriority.UseFont = false;
            this.uxPrice.StylePriority.UseForeColor = false;
            this.uxPrice.StylePriority.UseTextAlignment = false;
            this.uxPrice.Text = "7360";
            this.uxPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.uxPrice.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.uxPrice_BeforePrint);
            // 
            // uxDescription
            // 
            this.uxDescription.Dpi = 96F;
            this.uxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.uxDescription.ForeColor = System.Drawing.Color.Black;
            this.uxDescription.LocationFloat = new DevExpress.Utils.PointFloat(144.6F, 1F);
            this.uxDescription.Name = "uxDescription";
            this.uxDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.uxDescription.SizeF = new System.Drawing.SizeF(486.4F, 12.08001F);
            this.uxDescription.StylePriority.UseFont = false;
            this.uxDescription.StylePriority.UseForeColor = false;
            this.uxDescription.StylePriority.UseTextAlignment = false;
            this.uxDescription.Text = "(80 юаней за сутки)";
            this.uxDescription.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.uxDescription.BeforePrint +=
                new System.Drawing.Printing.PrintEventHandler(this.uxDescription_BeforePrint);
            // 
            // uxName
            // 
            this.uxName.Dpi = 96F;
            this.uxName.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.uxName.ForeColor = System.Drawing.Color.Black;
            this.uxName.LocationFloat = new DevExpress.Utils.PointFloat(35.79999F, 1F);
            this.uxName.Name = "uxName";
            this.uxName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.uxName.SizeF = new System.Drawing.SizeF(108.8F, 12.08001F);
            this.uxName.StylePriority.UseFont = false;
            this.uxName.StylePriority.UseForeColor = false;
            this.uxName.StylePriority.UseTextAlignment = false;
            this.uxName.Text = "Проживание";
            this.uxName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.uxName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.uxName_BeforePrint);
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
            this.visitInvitationSputniksDataset1.SchemaSerializationMode =
                System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // visitInvitationSputniksTableAdapter
            // 
            this.visitInvitationSputniksTableAdapter.ClearBeforeFill = true;
            // 
            // FullName
            // 
            this.FullName.DataMember = "dsVistInvitationSputniks";
            this.FullName.Expression = "Concat([Familiya], \' \', [Name], \' \', IsNull([Otchestvo], \'\'))";
            this.FullName.Name = "FullName";
            // 
            // FullNameEn
            // 
            this.FullNameEn.DataMember = "dsVistInvitationSputniks";
            this.FullNameEn.Expression = "Concat([FamiliyaEn], \' \', [NameEn])";
            this.FullNameEn.Name = "FullNameEn";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 100F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.HeightF = 100F;
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 100F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // OrderInvoiceDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[]
            {
                this.topMarginBand1,
                this.detailBand1,
                this.bottomMarginBand1
            });
            this.Version = "14.1";
            ((System.ComponentModel.ISupportInitialize) (this.visitInvitationDataset1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.visitInvitationSputniksDataset1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private Datasets.VisitInvitationDataset visitInvitationDataset1;
        private Datasets.VisitInvitationDatasetTableAdapters.VisitInvitationTableAdapter dataTableAdapter;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private Datasets.VisitInvitationSputniksDataset visitInvitationSputniksDataset1;

        private Datasets.VisitInvitationSputniksDatasetTableAdapters.VisitInvitationSputniksTableAdapter
            visitInvitationSputniksTableAdapter;

        private DevExpress.XtraReports.UI.CalculatedField FullName;
        private DevExpress.XtraReports.UI.CalculatedField FullNameEn;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel29;
        private DevExpress.XtraReports.UI.XRLabel uxPrice;
        private DevExpress.XtraReports.UI.XRLabel uxDescription;
        private DevExpress.XtraReports.UI.XRLabel uxName;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
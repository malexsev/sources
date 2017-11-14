namespace Cure.Reports
{
    using System;
    using System.Globalization;
    using DevExpress.XtraReports.UI;
    using Models;

    public partial class OrderInvoiceDetail : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly OrderInvoiceRow _row;

        public OrderInvoiceDetail(OrderInvoiceRow row)
        {
            InitializeComponent();
            _row = row;
        }

        private void uxName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = _row.Name;
        }

        private void uxDescription_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = _row.Description;
        }

        private void uxPrice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = Convert.ToInt32(_row.Price).ToString(CultureInfo.InvariantCulture);
        }
    }
}

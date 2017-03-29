using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Cure.Reports
{
    public partial class Rules : DevExpress.XtraReports.UI.XtraReport
    {
        string name { get; set; }

        public Rules(string name)
        {
            this.name = name;
            InitializeComponent();
        }

        private void Rules_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void uxName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.name;
        }

    }
}

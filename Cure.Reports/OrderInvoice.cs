
namespace Cure.Reports
{
    using System;
    using System.Linq;
    using Cure.Utils;
    using DataAccess;
    using DataAccess.BLL;
    using Datasets;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;

    public partial class OrderInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        private Visit visit;
        private int[] prices;

        public OrderInvoice(int visitId, int[] prices)
        {
            var dal = new DataAccessBL();
            this.visit = dal.GetVisit(visitId);
            this.prices = prices;
            if (this.visit != null)
            {
                InitializeComponent();
            }
        }

        private void uxPacientNameHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} {1}", this.visit.Pacient.FamiliyaEn, this.visit.Pacient.NameEng);
        }

        private void uxDocNumber_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.visit.Id.ToString();
        }

        private void uxDocDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void uxPacientNameFull_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} {1} {2} ({3} {4})",
                this.visit.Pacient.Familiya,
                this.visit.Pacient.Name,
                this.visit.Pacient.Otchestvo,
                this.visit.Pacient.FamiliyaEn,
                this.visit.Pacient.NameEng);
        }

        private void uxPacientBirthday_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.visit.Pacient.BirthDate.HasValue ? this.visit.Pacient.BirthDate.Value.ToString("dd-MM-yyyy") : "-";
        }

        private void uxPacientPassport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0}, {1}", this.visit.Pacient.SerialNumber, this.visit.Pacient.RefCountry.NameEn);
        }

        private void uxPacientDiagnoz_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.visit.Pacient.Diagnoz;
        }

        private void uxOrderDays_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var dateSpan = this.visit.Order.DateTo - this.visit.Order.DateFrom;
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} дней с {1} по {2}", dateSpan.Days.ToString(), this.visit.Order.DateFrom.ToString("dd-MM-yyyy"), this.visit.Order.DateTo.ToString("dd-MM-yyyy"));
        }

        private void uxSputnikDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var sputnik = this.visit.Order.Sputniks.FirstOrDefault(x => x.IsPrimary) ?? new Sputnik();
            var label = (XRLabel)sender;
            if (sputnik != null)
            {
                label.Text = string.Format("Сопровождающий - {0}: {1} {2} {3} ({4} {5}), {6} года рождения. Паспортные данные: {7}.",
                    sputnik.RefRodstvo == null ? "" : sputnik.RefRodstvo.Name,
                    sputnik.Familiya,
                    sputnik.Name,
                    sputnik.Otchestvo,
                    sputnik.FamiliyaEn,
                    sputnik.NameEn,
                    sputnik.BirthDate.HasValue ? sputnik.BirthDate.Value.ToString("dd-MM-yyyy") : "-",
                    sputnik.SeriaNumber);
            }

        }

        private void uxLine1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[0].ToString();
        }

        private void uxLine2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[1].ToString();
        }

        private void uxLine3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[2].ToString();
        }

        private void uxLine4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[3].ToString();
        }

        private void uxLine5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[4].ToString();
        }

        private void uxLine6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[5].ToString();
        }

        private void uxLine7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[6].ToString();
        }

        private void uxLine8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[7].ToString();
        }

        private void uxLine9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[8].ToString();
        }

        private void uxLine10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[9].ToString();
        }

        private void uxLine11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[10].ToString();
        }

        private void uxLine12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices[11].ToString();
        }

        private void uxLineTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.prices.Sum(x => x).ToString();
        }

        private void uxTotalTextUan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            var total = this.prices.Sum(x => x);
            label.Text = string.Format("{0} юаней", Utils.RuDateAndMoneyConverter.NumeralsToTxt(total, TextCase.Accusative, false, true)) ;
        }

        private void uxTotalTextUsd_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var dal = new DataAccessBL();
            var label = (XRLabel)sender;
            var usdRate = (double)GetRate("USD");
            var cnyRate = (double)GetRate("CNY");
            var total = (this.prices.Sum(x => x) * cnyRate) / usdRate;
            label.Text = string.Format("{0} долларов США", Math.Round(total, 0).ToString());

        }

        private decimal GetRate(string currency)
        {
            var dal = new DataAccessBL();
            var rate = dal.GetCurrencyRates().FirstOrDefault(x => x.CurrencyFrom == currency);
            if (rate != null)
            {
                return Math.Round(rate.Rate, 2);
            }
            else
            {
                return 0;
            }
        }

    }
}


namespace Cure.Reports
{
    using System;
    using System.Globalization;
    using Datasets;
    using DevExpress.XtraReports.UI;

    public partial class VisitInvitationSputniks : DevExpress.XtraReports.UI.XtraReport
    {
        public VisitInvitationSputniks(int visitId)
        {
            InitializeComponent();

            var dt = new VisitInvitationSputniksDataset.dsVistInvitationSputniksDataTable();
            this.visitInvitationSputniksTableAdapter.Fill(dt, visitId);
        }

        private void uxSputnikInfoCh_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void uxSputnikInfoRu_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void uxSputnikInfoCh_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            DateTime sputnikBirthday;
            if (DateTime.TryParse((GetCurrentColumnValue("BirthDate") ?? string.Empty).ToString(), out sputnikBirthday))
            {
                label.Text = string.Format("{0} {1} {2}, {3} 年 {4} 月 {5}, 日出生，{6}, 护照号, {7}",
                    GetCurrentColumnValue("RodstvoSoprovodChLabel"),
                    GetCurrentColumnValue("FamiliyaEn"),
                    GetCurrentColumnValue("NameEn"),
                    sputnikBirthday.Year,
                    sputnikBirthday.Month,
                    sputnikBirthday.Day,
                    GetCurrentColumnValue("CountryNacionalnostChLabel"),
                    GetCurrentColumnValue("SeriaNumber"));
            } else
            {
                label.Text = "No Sputnik Birthday Data. Не хватает данных о сопровождающем - дата рождения.";
            }
        }

        private void uxSputnikInfoRu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            DateTime sputnikBirthday;
            if (DateTime.TryParse((GetCurrentColumnValue("BirthDate") ?? string.Empty).ToString(), out sputnikBirthday))
            {
                label.Text = string.Format("{0} {1} {2}, дата рождения: {3}, гражданство: {4}, серия и номер заграничного паспорта: {5}",
                    GetCurrentColumnValue("RodstvoSoprovodRuLabel"),
                    GetCurrentColumnValue("FamiliyaEn"),
                    GetCurrentColumnValue("NameEn"),
                    sputnikBirthday.ToString("dd.MM.yyyy"),
                    GetCurrentColumnValue("CountryDescription"),
                    GetCurrentColumnValue("SeriaNumber"));
            } else
            {
                label.Text = "No Sputnik Birthday Data. Не хватает данных о сопровождающем - дата рождения.";
            }
        }
    }
}

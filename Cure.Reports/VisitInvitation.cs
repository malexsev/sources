
namespace Cure.Reports
{
    using System;
    using Cure.Utils;
    using DataAccess;
    using DataAccess.BLL;
    using Datasets;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;

    public partial class VisitInvitation : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        private System.Web.UI.Page page;
        private int visitId;

        public VisitInvitation(int visitId, System.Web.UI.Page page)
        {
            InitializeComponent();
            this.page = page;
            this.visitId = visitId;

            var dt = new VisitInvitationDataset.dsVistInvitationDataTable();
            this.dataTableAdapter.Fill(dt, visitId);
            uxSputniksSubreport.ReportSource = new VisitInvitationSputniks(visitId);
        }

        private void uxPacientInfoCh_PrintOnPage(object sender, DevExpress.XtraReports.UI.PrintOnPageEventArgs e)
        {
            PacientInfoCh(sender);
        }

        private void uxPacientInfoRu_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            PacientInfoRu(sender);
        }

        private void uxCurrentDateChLabel_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} 年 {1} 月 {2} 日",
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.Today.Day);
        }

        private void PacientInfoCh(object sender)
        {
            var label = (XRLabel)sender;
            DateTime pacientBirthday;
            if (DateTime.TryParse((GetCurrentColumnValue("PacientBirthDate") ?? string.Empty).ToString(), out pacientBirthday))
            {
                label.Text = string.Format("    患者英文名:	{0} {1}, {2} 年 {3} 月 {4}, 日出生, {5}, 护照号: {6}, 患有脑性瘫痪.",
                    GetCurrentColumnValue("PacientFamiliyaEn"),
                    GetCurrentColumnValue("PacientNameEng"),
                    pacientBirthday.Year,
                    pacientBirthday.Month,
                    pacientBirthday.Day,
                    GetCurrentColumnValue("CountryNacionalnostChLabel"),
                    GetCurrentColumnValue("PacientSerialNumber"));
            } else
            {
                label.Text = "No Pacient Birthday Data. Не хватает данных о пациенте - дата рождения.";
            }
        }

        private void PacientInfoRu(object sender)
        {
            var label = (XRLabel)sender;
            DateTime pacientBirthday;
            if (DateTime.TryParse((GetCurrentColumnValue("PacientBirthDate") ?? string.Empty).ToString(), out pacientBirthday))
            {
                label.Text = string.Format("На английском фамилия и имя пациента: {0} {1}, дата рождения: {2}, гражданство: {3}, серия и номер заграничного паспорта: {4}. Диагноз: детский церебральный паралич.",
                    GetCurrentColumnValue("PacientFamiliyaEn"),
                    GetCurrentColumnValue("PacientNameEng"),
                    pacientBirthday.ToString("dd.MM.yyyy"),
                    GetCurrentColumnValue("CountryDescription"),
                    GetCurrentColumnValue("PacientSerialNumber"));
            } else
            {
                label.Text = "No Pacient Birthday Data. Не хватает данных о пациенте - дата рождения.";
            }
        }

        private void uxPriglashaemCh_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            var label = (XRLabel)sender;
            DateTime orderDateFrom;
            DateTime orderDateTo;
            if (DateTime.TryParse((GetCurrentColumnValue("DateFrom") ?? string.Empty).ToString(), out orderDateFrom) &&
                DateTime.TryParse((GetCurrentColumnValue("DateTo") ?? string.Empty).ToString(), out orderDateTo))
            {
                label.Text = string.Format("随同患儿于 {0} 年 {1} 月 {2} 日至 {3} 年 {4} 月 {5} {6}",
                    orderDateFrom.Year,
                    orderDateFrom.Month,
                    orderDateFrom.Day,
                    orderDateTo.Year,
                    orderDateTo.Month,
                    orderDateTo.Day,
                    GetCurrentColumnValue("DepartmentPriglashenieCh"));
            } else
            {
                label.Text = "No Order Dates informtaion. Не хватает данных о планируемых датах заезда и выезда.";
            }
        }

        private void uxPriglashaemRu_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            var label = (XRLabel)sender;
            DateTime orderDateFrom;
            DateTime orderDateTo;
            if (DateTime.TryParse((GetCurrentColumnValue("DateFrom") ?? string.Empty).ToString(), out orderDateFrom) &&
                DateTime.TryParse((GetCurrentColumnValue("DateTo") ?? string.Empty).ToString(), out orderDateTo))
            {
                label.Text = string.Format("Вместе с ребенком с {0} по {1} {2}",
                    orderDateFrom.ToString("dd.MM.yyyy"),
                    orderDateTo.ToString("dd.MM.yyyy"),
                    GetCurrentColumnValue("DepartmentPriglashenieRu"));
            } else
            {
                label.Text = "No Order Dates informtaion. Не хватает данных о планируемых датах заезда и выезда.";
            }
        }

        private void uxPicturePost_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            AdjastImagePost((XRPictureBox)sender);
        }

        private void uxPictureSign_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            AdjastImageSign((XRPictureBox)sender);
        }

        private void AdjastImagePost(XRPictureBox picture)
        {
            Visit visit = dataAccess.GetVisit(visitId);
            string prefixPost = visit.Order.Department.PechatFileName;
            string imagePostUrl = ReportUtils.GetRandomFile(prefixPost, page);
            picture.ImageUrl = imagePostUrl;

            picture.Left += ReportUtils.GetRandomCorrection(20);
            picture.Top += ReportUtils.GetRandomCorrection(20);
        }

        private void AdjastImageSign(XRPictureBox picture)
        {
            Visit visit = dataAccess.GetVisit(visitId);
            string prefixSign = visit.Order.Department.PodpisFileName;
            string imageSignUrl = ReportUtils.GetRandomFile(prefixSign, page);
            picture.ImageUrl = imageSignUrl;

            picture.Left += ReportUtils.GetRandomCorrection(30);
            picture.Top += ReportUtils.GetRandomCorrection(30);
        }
    }
}

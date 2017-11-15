namespace Cure.WebAdmin.Admin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Cure.Reports.Models;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.Office.Utils;
    using DevExpress.Web.ASPxEditors;
    using DevExpress.Web.Data;
    using Utils;

    public partial class InvoiceReportParams : Page
    {
        protected int DaysCount = 0;
        protected int ProcCount = 0;
        protected decimal RatePendo = 0;
        protected decimal RateRubl = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                uxResultSave.Visible = false;
                int visitId;
                if (int.TryParse(Request.QueryString["visitId"], out visitId))
                {
                    var dataAccess = new DataAccessBL();
                    Visit visit = dataAccess.GetVisit(visitId);

                    this.DaysCount = (int)(visit.Order.DateTo - visit.Order.DateFrom).TotalDays;
                    var percentDays = (int)(this.DaysCount / 100.0 * 10);
                    this.ProcCount = this.DaysCount - percentDays;
                    this.RatePendo = GetRate("USD");
                    this.RateRubl = GetRate("CNY");

                    uxResult.TextGreen =
                        String.Format(
                            "{6}. Счёт на лечение по заезду №{0} (дата создания {1}), даты лечения с {2} по {3} ({7}), пациент {4} {5}",
                            visit.Order.Id,
                            visit.Order.CreateDate == null
                                ? "(не задано)"
                                : visit.CreateDate == null
                                    ? "[дата создания отсутствует]"
                                    : ((DateTime)visit.CreateDate).ToShortDateString(),
                            visit.Order.DateFrom.ToShortDateString(),
                            visit.Order.DateTo.ToShortDateString(),
                            string.IsNullOrEmpty(visit.Pacient.Name) ? "[имя не указано]" : visit.Pacient.Name,
                            string.IsNullOrEmpty(visit.Pacient.Familiya)
                                ? "[фамилия не указана]"
                                : visit.Pacient.Familiya,
                            visit.Order.Department.ShortName,
                            this.DaysCount
                        );

                    FormPanel.Visible = true;
                }
                else
                {
                    FormPanel.Visible = false;
                    uxResult.TextRed = "Страница не найдена.";
                }
            }
        }

        protected void PreviewButon_Click(object sender, EventArgs e)
        {
            int visitId;
            string pdfFullPath = "";
            string fileName = "";
            if (int.TryParse(Request.QueryString["visitId"], out visitId))
            {
                var dal = new DataAccessBL();
                Visit visit = dal.GetVisit(visitId);
                if (visit == null)
                {
                    FormPanel.Visible = false;
                    uxResult.TextRed = "Странная ошибка.";
                }
                else
                {
                    var user = dal.GetUserMembership(visit.Order.OwnerUser);
                    var report = new Cure.Reports.OrderInvoice(visitId, CreateRows(), SiteUtils.ParseInt(uxBottomUaniTotal.Text, 0));
                    var folderPath = Path.Combine(@"~\Documents\4DACCF0D-A806-45C7-872B-2CE9D95419D2\UserFiles\");
                    fileName = string.Format("Счет на лечение {0} {1} {2} {3} {4}.pdf",
                        visit.Pacient.Familiya,
                        visit.Pacient.Name,
                        visit.Order.Department.ShortName,
                        visit.Order.DateFrom.ToString("dd-MM-yyyy"),
                        visit.Order.DateTo.ToString("dd-MM-yyyy"));
                    pdfFullPath = Server.MapPath(Path.Combine(folderPath, fileName));
                    FileUtils.CreateFolderIfNotExists(new HttpServerUtilityWrapper(Server), folderPath);

                    if (File.Exists(pdfFullPath))
                    {
                        File.Delete(pdfFullPath);
                    }

                    try
                    {
                        using (var fs = new FileStream(pdfFullPath, FileMode.Append))
                        {
                            report.ExportToPdf(fs);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        report.Dispose();
                    }
                }
            }

            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            Response.TransmitFile(pdfFullPath);
            Response.End();
        }

        protected void SaveButon_Click(object sender, EventArgs e)
        {
            int visitId;
            if (int.TryParse(Request.QueryString["visitId"], out visitId))
            {
                var dal = new DataAccessBL();
                Visit visit = dal.GetVisit(visitId);
                if (visit == null)
                {
                    FormPanel.Visible = false;
                    uxResult.TextRed = "Странная ошибка.";
                }
                else
                {
                    var user = dal.GetUserMembership(visit.Order.OwnerUser);
                    var report = new Cure.Reports.OrderInvoice(visitId, CreateRows(), SiteUtils.ParseInt(uxBottomUaniTotal.Text, 0));
                    //int.Parse(uxLine1.Text), int.Parse(uxLine2.Text), int.Parse(uxLine3.Text), int.Parse(uxLine4.Text), int.Parse(uxLine5.Text), int.Parse(uxLine6.Text), int.Parse(uxLine7.Text), int.Parse(uxLine8.Text), int.Parse(uxLine9.Text), int.Parse(uxLine10.Text), int.Parse(uxLine11.Text), int.Parse(uxLine12.Text) });
                    var folderPath = Path.Combine(@"~\Documents\", user.Expr1 + @"\UserFiles\");
                    var fileName = string.Format("Счет на лечение {0} {1} {2} {3} {4}.pdf",
                        visit.Pacient.Familiya,
                        visit.Pacient.Name,
                        visit.Order.Department.ShortName,
                        visit.Order.DateFrom.ToString("dd-MM-yyyy"),
                        visit.Order.DateTo.ToString("dd-MM-yyyy"));
                    var pdfFullPath = Server.MapPath(Path.Combine(folderPath, fileName));
                    FileUtils.CreateFolderIfNotExists(new HttpServerUtilityWrapper(Server), folderPath);

                    if (File.Exists(pdfFullPath))
                    {
                        File.Delete(pdfFullPath);
                    }

                    try
                    {
                        using (var fs = new FileStream(pdfFullPath, FileMode.Append))
                        {
                            report.ExportToPdf(fs);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        report.Dispose();
                    }


                    uxTable.Visible = false;
                    SaveButon.Visible = false;
                    uxResultSave.TextGreen = "Отчёт сгенерирован и сохранён в Мои Документы пользователя.";
                    uxResultSave.Visible = true;
                }
            }
        }

        private List<OrderInvoiceRow> CreateRows()
        {
            var result = new List<OrderInvoiceRow>();
            for (var i = 1; i <= 17; i++)
            {
                var uxLineCheckBox = (ICheckBoxControl)FormPanel.FindControl(string.Format("uxLine{0}CheckBox", i));
                var uxLineName = (ITextControl)FormPanel.FindControl(string.Format("uxLine{0}Name", i));
                var uxLineTotal = (ITextControl)FormPanel.FindControl(string.Format("uxLine{0}Total", i));
                var uxLineAmount = (HiddenField)FormPanel.FindControl(string.Format("uxLine{0}ProcHidden", i));
                var uxLineCostPerOne = (ITextControl)FormPanel.FindControl(string.Format("uxLine{0}Cost", i));
                var uxLineDescription = (ITextControl)FormPanel.FindControl(string.Format("uxLine{0}Description", i));
                decimal total;
                if (uxLineCheckBox.Checked && decimal.TryParse(uxLineTotal.Text, out total) && total > 0)
                {
                    var row = new OrderInvoiceRow(uxLineDescription.Text,
                        uxLineAmount.Value,
                        uxLineCostPerOne.Text, total);
                    result.Add(row);
                }
            }

            return result;
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
namespace Cure.WebAdmin.Admin
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.Web.Data;
    using Utils;

    public partial class InvoiceReportParams : Page
    {
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

                    uxResult.TextGreen =
                        String.Format(
                            "Генерация Счёт на лечение по заезду №{0} от {1}, даты с {2} по {3}, пациент {4} {5}",
                            visit.Order.Id,
                            visit.Order.CreateDate == null
                                ? "(не задано)"
                                : visit.CreateDate == null ? "[дата создания отсутствует]" : ((DateTime)visit.CreateDate).ToShortDateString(),
                            visit.Order.DateFrom.ToShortDateString(),
                            visit.Order.DateTo.ToShortDateString(),
                            string.IsNullOrEmpty(visit.Pacient.Name) ? "[имя не указано]" : visit.Pacient.Name,
                            string.IsNullOrEmpty(visit.Pacient.Familiya) ? "[фамилия не указана]" : visit.Pacient.Familiya
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
                    uxResult.TextRed = "Страная ошибка.";
                }
                else
                {
                    var user = dal.GetUserMembership(visit.Order.OwnerUser);
                    var report = new Cure.Reports.OrderInvoice(visitId, new[] { int.Parse(uxLine1.Text), int.Parse(uxLine2.Text), int.Parse(uxLine3.Text), int.Parse(uxLine4.Text), int.Parse(uxLine5.Text), int.Parse(uxLine6.Text), int.Parse(uxLine7.Text), int.Parse(uxLine8.Text), int.Parse(uxLine9.Text), int.Parse(uxLine10.Text), int.Parse(uxLine11.Text), int.Parse(uxLine12.Text) });
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


                    uxResultSave.TextGreen = "Отчёт сгенерирован и сохранён в Мои Документы пользователя.";
                    uxResultSave.Visible = true;
                }
            }
        }
    }
}
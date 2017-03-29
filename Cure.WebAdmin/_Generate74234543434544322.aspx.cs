namespace Cure.WebAdmin
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web;
    using DataAccess.BLL;
    using DevExpress.XtraCharts.Native;
    using Notification;
    using Utils;

    public partial class _Generate74234543434544322 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int orderId;
            if (!IsPostBack && int.TryParse(Request.QueryString["orderid"], out orderId) && !string.IsNullOrEmpty(Request.QueryString["sputnik"]))
            {
                var sputnik = Request.QueryString["sputnik"];
                var dal = new DataAccessBL();
                var order = dal.GetOrder(orderId);
                var user = dal.GetUserMembership(order.OwnerUser);
                var report = new Cure.Reports.Rules(sputnik);
                var folderPath = Path.Combine(@"~\Documents\", user.Expr1 + @"\UserFiles\");
                var fileName = "Условиях и правила приёма и реабилитации в клинике.pdf";
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



            }
        }
    }
} ;
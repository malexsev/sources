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

    public partial class OrderDocs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int orderId;
                if (int.TryParse(Request.QueryString["orderId"], out orderId))
                {
                    Session["ExpandOrderId"] = orderId;
                    var dataAccess = new DataAccessBL();
                    Order order = dataAccess.GetOrder(orderId);
                    var folderPath = Path.Combine(@"~\Documents\", order.GuidId + "\\");
                    FileUtils.CreateFolderIfNotExists(new HttpServerUtilityWrapper(this.Server), folderPath);
                    uxFileManager.Settings.RootFolder = folderPath;
                    uxFileManager.Visible = true;
                    uxResult.TextGreen =
                        String.Format(
                            "Документы по заезду №{0} от {1}, даты с {2} по {3}",
                            order.Id,
                            order.CreateDate == null
                                ? "(не задано)"
                                : ((DateTime) order.CreateDate).ToShortDateString(),
                            order.DateFrom.ToShortDateString(),
                            order.DateTo.ToShortDateString()
                            );
                    

                }
                else
                {
                    uxFileManager.Visible = false;
                    uxResult.TextRed = "Страница не найдена.";
                }
            }
        }

        protected void uxVisitGrid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["OrderId"] = (int)Session["ExpandOrderId"];
            SetUpdateUserData(ref sender, ref e);
        }

        protected void uxVisitGrid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            e.NewValues["OrderId"] = (int)Session["ExpandOrderId"];
            SetInsertUserData(ref sender, ref e);
        }

        private void SetInsertUserData(ref object sender, ref ASPxDataInsertingEventArgs e)
        {
            e.NewValues["CreateUser"] = e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["CreateDate"] = e.NewValues["LastDate"] = DateTime.Now;
        }

        private void SetUpdateUserData(ref object sender, ref ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["LastUser"] = SiteUtils.GetCurrentUserName();
            e.NewValues["LastDate"] = DateTime.Now;
        }
    }
}
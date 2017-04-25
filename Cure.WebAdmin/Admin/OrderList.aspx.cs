using System;

namespace Cure.WebAdmin.Admin
{
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Cure.Reports;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraRichEdit.API.Native;
    using DevExpress.Web.ASPxClasses;
    using DevExpress.Web.ASPxFileManager;
    using DevExpress.Web.ASPxGridView;
    using DevExpress.Web.Data;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraRichEdit;
    using Notification;
    using Utils;
    using Microsoft.VisualBasic;
    using Page = System.Web.UI.Page;

    public partial class OrderList : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session["RootFolder"] != null)
            //{
            //    ASPxFileManager1.Settings.RootFolder = Session["RootFolder"].ToString();
            //}
        }

        protected void uxFilterButton_Click(object sender, EventArgs e)
        {
            uxMainGrid.DataBind();
        }

        public string GetSitizenship(string countryId)
        {
            var dal = new DataAccessBL();
            int id;
            string citizenship = "-";
            if (int.TryParse(countryId, out id))
            {
                citizenship = dal.GetRefCountry(id).Description;
            }

            return citizenship;
        }

        protected void uxMainGrid_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded && e.VisibleIndex >= 0)
            {
                var orderId = (int)uxMainGrid.GetRowValues(e.VisibleIndex, "Id");
                Session["ExpandOrderId"] = orderId;
            }
        }

        protected void uxMainGrid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["OwnerUser"] == null || String.IsNullOrEmpty(e.NewValues["OwnerUser"].ToString()))
            {
                e.NewValues["OwnerUser"] = SiteUtils.GetCurrentUserName();
            }
            if ((int)e.NewValues["StatusId"] == 6 && (int)e.NewValues["StatusId"] != (int)e.OldValues["StatusId"])
            {
                //Изменение статуса на Одобрено
                try
                {
                    var notify = new OrderApprovedToUserEmailNotification((int)e.Keys[0], (DateTime)e.NewValues["DateFrom"], (DateTime)e.NewValues["DateTo"], new HttpServerUtilityWrapper(Server));
                    notify.Send();
                }
                catch
                {
                }

            }
            if ((int)e.NewValues["StatusId"] == 9 && e.NewValues["StatusId"] != e.OldValues["StatusId"])
            {
                if (!(e.NewValues["TicketUbitieTime"] is DateTime))
                {
                    throw new Exception("Статус \"Завершён\" не может быть применён, не установлено Время Убытия.");
                }
            }
            //Изменение статуса на Куплены билеты
            if ((int)e.NewValues["StatusId"] == 7 && e.NewValues["StatusId"] != e.OldValues["StatusId"])
            {
                if (((int?)e.NewValues["TransferInfo"]).HasValue)
                {
                    if (!string.IsNullOrEmpty((e.NewValues["TicketInfo"] ?? "").ToString()))
                    {
                        var notify = new OrderTicketsToUserEmailNotification((int)e.Keys[0], (int)e.NewValues["TransferInfo"], new HttpServerUtilityWrapper(Server));
                        notify.Send();
                    }
                    else
                    {
                        throw new Exception("Статус \"Куплены Билеты\" не может быть применён, нет информации о билетах.");
                    }
                }
                else
                {
                    throw new Exception("Статус \"Куплены Билеты\" не может быть применён, не установлен Вариант прибытия.");
                }
            }

            SetUpdateUserData(ref sender, ref e);
        }

        protected void uxMainGrid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            if (String.IsNullOrEmpty(e.NewValues["OwnerUser"].ToString()))
            {
                e.NewValues["OwnerUser"] = SiteUtils.GetCurrentUserName();
            }
            SetInsertUserData(ref sender, ref e);
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

        protected void uxSputnikGrid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["OrderId"] = (int)Session["ExpandOrderId"];
            SetUpdateUserData(ref sender, ref e);
        }

        protected void uxSputnikGrid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

        protected void uxCallbackPanel_OnCallback(object s, CallbackEventArgsBase e)
        {
            Guid itemGuid;
            if (Guid.TryParse(e.Parameter, out itemGuid))
            {
                uxPhotoGallery.ItemGuid = itemGuid;
                uxPhotoUploader.ItemGuid = itemGuid;
            }
            else if (Information.IsNumeric(e.Parameter))
            {
                int index = Convert.ToInt32(e.Parameter);
                uxPhotoGallery.RemovePhotoByIndex(index);
            }
        }

        protected void uxInviteCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            int visitId;
            if (int.TryParse(e.Parameter, out visitId))
            {
                var dataAccess = new DataAccessBL();
                Visit visit = dataAccess.GetVisit(visitId);
                if (visit != null)
                {
                    var report = new VisitInvitation(visitId, this);
                    var folderPath = Path.Combine(@"~\Documents\", visit.Order.GuidId + "\\");
                    var fileName = String.Format("Invitation_{0}.pdf", visitId);
                    var fileNamePng = String.Format("Invitation_{0}.png", visitId);
                    var pdfFullPath = this.MapPath(Path.Combine(folderPath, fileName));
                    var pngFullPath = this.MapPath(Path.Combine(folderPath, fileNamePng));

                    FileUtils.CreateFolderIfNotExists(new HttpServerUtilityWrapper(this.Server), folderPath);

                    if (File.Exists(pdfFullPath))
                    {
                        File.Delete(pdfFullPath);
                    }
                    if (File.Exists(pngFullPath))
                    {
                        File.Delete(pngFullPath);
                    }

                    var options = new ImageExportOptions { Resolution = 180, Format = ImageFormat.Png };
                    report.ExportToImage(pngFullPath, options);
                    var doc = new RichEditDocumentServer();
                    var img = doc.Document.InsertImage(doc.Document.CaretPosition,
                        DocumentImageSource.FromFile(pngFullPath));
                    img.Size = img.OriginalSize;
                    using (var fs = new FileStream(pdfFullPath, FileMode.Append))
                    {
                        doc.ExportToPdf(fs);
                    }
                    doc.Document.Unprotect();
                    doc.Dispose();
                    report.Dispose();
                    //if (File.Exists(pngFullPath))
                    //{
                    //    File.Delete(pngFullPath);
                    //}
                }
            }
        }

        protected void ASPxFileManager1_FileDownloading(object source, FileManagerFileDownloadingEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using DevExpress.Web.Data;
    using Logic;
    using Utils;

    public partial class ClientPacients : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RefreshGrid()
        {
            uxPacientsGrid.DataBind();
        }


        protected void uxPacientsGrid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            SetUpdateUserData(ref sender, ref e);
        }

        protected void uxPacientsGrid_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            SetInsertUserData(ref sender, ref e);
        }

        protected void uxVisitGrid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["PacientId"] = Session["ExpandPacientId"];
            e.NewValues["OrderId"] = ClientContainer.NewOrder.Id;
            SetUpdateUserData(ref sender, ref e);
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

        protected void uxPacientsGrid_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded && e.VisibleIndex >= 0)
            {
                var pacientId = (int)uxPacientsGrid.GetRowValues(e.VisibleIndex, "Id");
                Session["ExpandPacientId"] = pacientId;
                var curVisit = ClientContainer.NewOrder.Visits.FirstOrDefault(x => x.PacientId == pacientId);
                if (curVisit != null)
                {
                    Session["ExpandVisitId"] = curVisit.Id;
                }
                else
                {
                    throw new Exception("Произошла ошибка завершения сессии. Сохраняйте резултаты своей работы, до покадания рабочего места.");
                }
            }
        }

        private ClientContainer ClientContainer
        {
            get
            {
                if (HttpContext.Current.Session["ClientContainer"] == null)
                {
                    HttpContext.Current.Session["ClientContainer"] = new ClientContainer(Utils.SiteUtils.GetCurrentUserName());
                }
                return (ClientContainer)HttpContext.Current.Session["ClientContainer"];
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session["ClientContainer"] = value;
                }
            }
        }
    }
}
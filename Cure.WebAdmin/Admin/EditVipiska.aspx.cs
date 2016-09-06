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

    public partial class EditVipiska : Page
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
                    Vipiska vipiska = dataAccess.GetVipiska(visitId);
                    if (vipiska == null)
                    {
                        vipiska = new Vipiska
                        {
                            CreateDate = DateTime.Now,
                            CreateUser = SiteUtils.GetCurrentUserName(),
                            LastDate = DateTime.Now,
                            VisitId = visitId,
                            LastUser = SiteUtils.GetCurrentUserName()
                        };
                        dataAccess.InsertVipiska(vipiska);
                        vipiska = dataAccess.GetVipiska(visitId);
                    }
                    ResultText.Text = vipiska.Result;
                    uxResult.TextGreen =
                        String.Format(
                            "Выписка по заезду №{0} от {1}, даты с {2} по {3}, пациент {4} {5}",
                            vipiska.Visit.Order.Id,
                            vipiska.Visit.Order.CreateDate == null
                                ? "(не задано)"
                                : vipiska.Visit.CreateDate == null ? "[дата создания отсутствует]" : ((DateTime)vipiska.Visit.CreateDate).ToShortDateString(),
                            vipiska.Visit.Order.DateFrom.ToShortDateString(),
                            vipiska.Visit.Order.DateTo.ToShortDateString(),
                            string.IsNullOrEmpty(vipiska.Visit.Pacient.Name) ? "[имя не указано]" : vipiska.Visit.Pacient.Name,
                            string.IsNullOrEmpty(vipiska.Visit.Pacient.Familiya) ? "[фамилия не указана]" : vipiska.Visit.Pacient.Familiya
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

        protected void SaveButon_Click(object sender, EventArgs e)
        {
            int visitId;
            if (int.TryParse(Request.QueryString["visitId"], out visitId))
            {
                var dataAccess = new DataAccessBL();
                Vipiska vipiska = dataAccess.GetVipiska(visitId);
                if (vipiska == null)
                {
                    FormPanel.Visible = false;
                    uxResult.TextRed = "Страная ошибка.";
                }
                else
                {
                    vipiska.Result = ResultText.Text;
                    vipiska.LastDate = DateTime.Now;
                    vipiska.LastUser = SiteUtils.GetCurrentUserName();
                    dataAccess.UpdateVipiska(vipiska);
                    uxResultSave.TextGreen = "Результаты лечения обновлены.";
                    uxResultSave.Visible = true;
                }
            }
        }
    }
}
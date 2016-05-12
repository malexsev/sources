using System;
using System.Web;
using Cure.Utils;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client
{
    using Controls;
    using DataAccess;
    using DevExpress.Web.ASPxEditors;
    using Logic;

    public partial class CurrentOrder : System.Web.UI.Page
    {
        //"Заявка ещё не отправлялась" Value="0" 
        //"Заявка отправлена" Value="1" 
        //"Заявка принята на рассмотрение" Value="2" 
        //"Заявка одобрена" Value="3" 
        //"Закуплены билеты" Value="4" 
        //"Прибытие - отзыв о прибытии" Value="5" 
        //"Прохождение лечения" Value="6" 
        //"Завершение лечения - отзыв о лечении" Value="7" 
        //"Заявка выполнена" Value="8" 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.clientContainer.RefreshData();
            }
            this.SetTrackBar();
        }

        private void SetTrackBar()
        {
            if (this.clientContainer.CurrentOrder == null)
            {
                uxTrackBar.Value = 0;
                this.LoadStep("Step1Start.ascx");
                return;
            }

            switch (this.clientContainer.CurrentOrder.StatusId)
            {
                case 2:
                    uxTrackBar.Value = 1;
                    this.LoadStep("Step2Ready.ascx");
                    break;
                case 3:
                    uxTrackBar.Value = 2;
                    this.LoadStep("Step3Checking.ascx");
                    break;
                case 6:
                    uxTrackBar.Value = 3;
                    this.LoadStep("Step4Steady.ascx");
                    break;
                case 7:
                    uxTrackBar.Value = 4;
                    this.LoadStep("Step5Go.ascx");
                    break;
                case 8:
                    uxTrackBar.Value = 6;
                    this.LoadStep("Step6Process.ascx");
                    break;
                case 9:
                    uxTrackBar.Value = 8;
                    this.LoadStep("Step7Finish.ascx");
                    break;
                default:
                    uxTrackBar.Value = 0;
                    break;
            }
        }

        private void LoadStep(string controlName)
        {
            Control ctrl = Page.LoadControl(String.Format(@"Controls/{0}", controlName));
            uxStepPlaceHolder.Controls.Add(ctrl);
        }

        private ClientContainer clientContainer
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
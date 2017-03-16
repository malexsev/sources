namespace Cure.WebAdmin
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using DataAccess.BLL;
    using Notification;
    using Utils;

    public partial class _EveryDay9826358775243309 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dal = new DataAccessBL();
                dal.MixEntities();
                dal.SwitchOrderStatusTask();
                INotification notify = new Before60DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new Before45DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new Before30DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new Before3DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionBeforeOrder7EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionAfterStart7EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionBeforeFinish5EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionAfterFinish15EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionAfterFinish20EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new MensionAfterFinish30EmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new OrderNotCompletedToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new OrdersUnprocessedEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new Before24HoursNotification();
                notify.Send();
                notify = new Before24HoursEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new BeforeArriveToUserEmailNotification();
                notify.Send();
                notify = new PacientBirthdayEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                notify = new SomeBirthdaysEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notify.Send();
                DataTable data = CurrencyUtils.GetRates(dal.GetCurrencies().Select(o => o.Name).ToList());
                dal.UpdateCurrencyRates(data, SiteUtils.GetCurrentUserName());
                WeatherUtils.ParseWeather();
            }
        }
    }
}
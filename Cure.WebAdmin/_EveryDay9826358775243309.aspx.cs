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
                var pending = new OrderNotCompletedToUserEmailNotification(new HttpServerUtilityWrapper(this.Server));
                pending.Send();
                var unprocess = new OrdersUnprocessedEmailNotification(new HttpServerUtilityWrapper(this.Server));
                unprocess.Send();
                var notify = new Before24HoursNotification();
                notify.Send();
                var notifyEmail = new Before24HoursEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notifyEmail.Send();
                var notifyUsers = new BeforeArriveToUserEmailNotification();
                notifyUsers.Send();
                DataTable data = CurrencyUtils.GetRates(dal.GetCurrencies().Select(o => o.Name).ToList());
                dal.UpdateCurrencyRates(data, SiteUtils.GetCurrentUserName());
                WeatherUtils.ParseWeather();
                var birthday = new PacientBirthdayEmailNotification(new HttpServerUtilityWrapper(this.Server));
                birthday.Send();
            }
        }
    }
}
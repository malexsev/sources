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
                var dataAccess = new DataAccessBL();
                dataAccess.SwitchOrderStatusTask();
                var notify = new Before24HoursNotification();
                notify.Send();
                var notifyEmail = new Before24HoursEmailNotification(new HttpServerUtilityWrapper(this.Server));
                notifyEmail.Send();
                var notifyUsers = new BeforeArriveToUserEmailNotification();
                notifyUsers.Send();
                DataTable data = CurrencyUtils.GetRates(dataAccess.GetCurrencies().Select(o => o.Name).ToList());
                dataAccess.UpdateCurrencyRates(data, SiteUtils.GetCurrentUserName());
                WeatherUtils.ParseWeather();
            }
        }
    }
}
namespace Cure.WebAdmin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraSpreadsheet.Utils;
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

                var notificationsList = new List<INotification>
                {
                    new Before60DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new Before45DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new Before30DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new Before3DaysToUserEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new OrderNotCompletedToUserEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new OrdersUnprocessedEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new Before24HoursNotification(),
                    new Before24HoursEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    //new BeforeArriveToUserEmailNotification(),
                    new PacientBirthdayEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new SomeBirthdaysEmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionBeforeOrder7EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionAfterStart7EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionBeforeFinish5EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionAfterFinish15EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionAfterFinish20EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new MensionAfterFinish30EmailNotification(new HttpServerUtilityWrapper(this.Server)),
                    new RemoveUnapprovedUsers10DaysLeft(new HttpServerUtilityWrapper(this.Server))
                };

                notificationsList.ForEach(o =>
                {
                    try
                    {
                        o.Send();
                    }
                    catch (Exception ex)
                    {
                        var notification = new NotificationLog()
                        {
                            ClientName = o.ToString(),
                            Description = ex.Message,
                            Contacts = string.Empty,
                            Details = ex.InnerException == null ? "" : ex.InnerException.Message,
                            ExecutionDate = DateTime.Now,
                            Name = "Ошибка оповещения",
                            Result = string.Empty,
                            Type = "Ошибка",
                            Text = o.GetHashCode().ToString(CultureInfo.InvariantCulture)
                        };

                        dal.InsertNotificationLog(notification);
                    }
                });

                DataTable data = CurrencyUtils.GetRates(dal.GetCurrencies().Select(o => o.Name).ToList());
                dal.UpdateCurrencyRates(data, SiteUtils.GetCurrentUserName());
                WeatherUtils.ParseWeather();
            }
        }
    }
} ;
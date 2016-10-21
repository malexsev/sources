using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin
{
    using System.Data;
    using System.Net;
    using DataAccess.BLL;
    using DevExpress.Web.Data;
    using Utils;

    public partial class CurrencyRateList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uxGetRates_Click(object sender, EventArgs e)
        {
            DataAccessBL bll = new DataAccessBL();
            try
            {
                DataTable data = CurrencyUtils.GetRates(bll.GetCurrencies().Select(o => o.Name).ToList());
                bll.UpdateCurrencyRates(data, SiteUtils.GetCurrentUserName());
                uxMainGrid.DataBind();
                uxResultBox.TextGreen = "Успешно обновлено";
            }
            catch (Exception ex)
            {
                uxResultBox.TextRed = ex.Message;
            }
        }
    }
}
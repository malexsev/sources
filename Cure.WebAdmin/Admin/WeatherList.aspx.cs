﻿namespace Cure.WebAdmin.Admin
{
    using Page = System.Web.UI.Page;

    public partial class WeatherList : Page
    {

        protected void uxMainGrid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            //e.Row["Text"]
        }
    }
}
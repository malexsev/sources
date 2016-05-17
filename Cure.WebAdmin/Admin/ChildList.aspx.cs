using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Admin
{
    using DevExpress.Web.ASPxClasses;
    using Microsoft.VisualBasic;

    public partial class ChildList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}
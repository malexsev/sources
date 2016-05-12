using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using System.Security.Cryptography.X509Certificates;
    using Logic;
    using Utils;

    public partial class ClientCurrOrder : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RefreshGrid()
        {
            uxMainGrid.DataBind();
        }

        public bool VisibleOfVisitGrid
        {
            get
            {
                return uxVisitGrid.Visible;
            }
            set
            {
                uxVisitGrid.Visible = uxSputniksLabel.Visible = value;
                
            }
        }

        public bool VisibleOfSputnikGrid
        {
            get
            {
                return uxSputnikGrid.Visible;
            }
            set
            {
                uxSputnikGrid.Visible = uxPacientsLabel.Visible = value;
            }
        }

    }
}
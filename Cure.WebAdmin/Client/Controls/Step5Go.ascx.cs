using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Client.Controls
{
    using DevExpress.Charts.Model;
    using Logic;

    public partial class Step5Go : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindControls();
            }
        }

        protected void uxSave_Click(object sender, EventArgs e)
        {
            clientContainer.CurrentOrder.ServicePekinIsPerevod = uxServicesPekin.Items.FindByValue("Услуги переводчика").Selected;
            //ServicePekinIsHotel
            clientContainer.CurrentOrder.ServicePekinIsHotel = uxServicesPekin.Items.FindByValue("Забронировать Отель").Selected;
            //ServicePekinOther
            clientContainer.CurrentOrder.ServicePekinOther = uxServicesPekinOther.Text;
            //ServiceUnchenIsVstrecha
            clientContainer.CurrentOrder.ServiceUnchenIsVstrecha = uxServicesUnchenVstrecha.Items.FindByValue("Встреча").Selected;
            //ServiceUnchenOther
            clientContainer.CurrentOrder.ServiceUnchenOther = uxServicesUnchenOther.Text;
            //ServiceRoomIsPaper
            clientContainer.CurrentOrder.ServiceRoomIsPaper = uxServicesUnchenRoom.Items.FindByValue("Туалетная Бумага").Selected;
            //ServiceRoomIsStiral
            clientContainer.CurrentOrder.ServiceRoomIsStiral = uxServicesUnchenRoom.Items.FindByValue("Стиральный порошок").Selected;
            //ServiceRoomIsOpolask
            clientContainer.CurrentOrder.ServiceRoomIsOpolask = uxServicesUnchenRoom.Items.FindByValue("Ополаскиватель для белья").Selected;
            //ServiceRoomIsMilo
            clientContainer.CurrentOrder.ServiceRoomIsMilo = uxServicesUnchenRoom.Items.FindByValue("Жидкое мыло").Selected;
            //ServiceRoomIsVoda
            clientContainer.CurrentOrder.ServiceRoomIsVoda = uxServicesUnchenRoom.Items.FindByValue("Питьевая вода").Selected;
            //ServiceRoomIsPosuda
            clientContainer.CurrentOrder.ServiceRoomIsPosuda = uxServicesUnchenRoom.Items.FindByValue("Средство для мытья посуды").Selected;
            //uxDateTo
            clientContainer.CurrentOrder.DateTo = uxDateTo.Date.Year > 2000 ? uxDateTo.Date : clientContainer.CurrentOrder.DateTo;
            
            clientContainer.UpdateCurrentOrder();
            uxResultBox.TextGreen = "Данные сохранены.";
            uxClientCurrOrder.RefreshGrid();
        }

        private void BindControls()
        {
            if (clientContainer.CurrentOrder != null)
            {
                //ServicePekinIsPerevod
                uxServicesPekin.Items.FindByValue("Услуги переводчика").Selected = clientContainer.CurrentOrder.ServicePekinIsPerevod ?? false;
                //ServicePekinIsHotel
                uxServicesPekin.Items.FindByValue("Забронировать Отель").Selected = clientContainer.CurrentOrder.ServicePekinIsHotel ?? false;
                //ServicePekinOther
                uxServicesPekinOther.Text = clientContainer.CurrentOrder.ServicePekinOther;
                //ServiceUnchenIsVstrecha
                uxServicesUnchenVstrecha.Items.FindByValue("Встреча").Selected = clientContainer.CurrentOrder.ServiceUnchenIsVstrecha ?? false;
                //ServiceUnchenOther
                uxServicesUnchenOther.Text = clientContainer.CurrentOrder.ServiceUnchenOther;
                //ServiceRoomIsPaper
                uxServicesUnchenRoom.Items.FindByValue("Туалетная Бумага").Selected = clientContainer.CurrentOrder.ServiceRoomIsPaper ?? false;
                //ServiceRoomIsStiral
                uxServicesUnchenRoom.Items.FindByValue("Стиральный порошок").Selected = clientContainer.CurrentOrder.ServiceRoomIsStiral ?? false;
                //ServiceRoomIsOpolask
                uxServicesUnchenRoom.Items.FindByValue("Ополаскиватель для белья").Selected = clientContainer.CurrentOrder.ServiceRoomIsOpolask ?? false;
                //ServiceRoomIsMilo
                uxServicesUnchenRoom.Items.FindByValue("Жидкое мыло").Selected = clientContainer.CurrentOrder.ServiceRoomIsMilo ?? false;
                //ServiceRoomIsVoda
                uxServicesUnchenRoom.Items.FindByValue("Питьевая вода").Selected = clientContainer.CurrentOrder.ServiceRoomIsVoda ?? false;
                //ServiceRoomIsPosuda
                uxServicesUnchenRoom.Items.FindByValue("Средство для мытья посуды").Selected = clientContainer.CurrentOrder.ServiceRoomIsPosuda ?? false;

                uxDateTo.Date = clientContainer.CurrentOrder.DateTo;
            }
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
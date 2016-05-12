using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebAdmin.Logic
{
    using DataAccess;

    public class PacientDataSoruce
    {
        public IEnumerable<Pacient> GetPacients()
        {
            return ClientContainer.NewOrder.Visits.Select(x => x.Pacient);
        }

        public IEnumerable<Pacient> GetPacientsCurrentOrder()
        {
            return ClientContainer.CurrentOrder.Visits.Select(x => x.Pacient);
        }

        public IEnumerable<Sputnik> GetSputniksCurrentOrder()
        {
            return ClientContainer.CurrentOrder.Sputniks;
        }

        public Order GetCurrentOrder()
        {
            return ClientContainer.CurrentOrder;
        }

        public void DeletePacient(Pacient pacient)
        {
            ClientContainer.RemovePacient(ClientContainer.NewOrder.Visits.First(x => x.PacientId == pacient.Id).Id);
        }

        public void UpdatePacient(Pacient pacient)
        {
            ClientContainer.UpdatePacient(pacient);
        }
            

        private ClientContainer ClientContainer
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
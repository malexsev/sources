using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebAdmin.Logic
{
    using DataAccess;

    public class SputnikDataSoruce
    {
        public IEnumerable<Sputnik> GetSputniks()
        {
            return ClientContainer.NewOrder.Sputniks;
        }

        public void DeleteSputnik(Sputnik sputnik)
        {
            ClientContainer.RemoveSputnik(sputnik.Id);
        }

        public void UpdateSputnik(Sputnik sputnik)
        {
            sputnik.OrderId = ClientContainer.NewOrder.Id;
            ClientContainer.UpdateSputnik(sputnik);
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
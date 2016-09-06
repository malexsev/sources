using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Vipiska> GetMyVipiskas(string username)
        {
            return context.Vipiskas.Where(x => x.Visit.Order.OwnerUser == username).OrderByDescending(x => x.Visit.Order.DateTo);
        }

        public Vipiska GetVipiska(int visitId)
        {
            return context.Vipiskas.FirstOrDefault(o => o.VisitId == visitId);
        }

        public void InsertVipiska(Vipiska vipiska)
        {
            try
            {
                context.Vipiskas.AddObject(vipiska);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVipiska(Vipiska vipiska)
        {
            try
            {
                context.Vipiskas.Attach(vipiska);
                context.Vipiskas.DeleteObject(vipiska);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVipiska(Vipiska vipiska)
        {
            try
            {
                var origVipiska = GetVipiska(vipiska.VisitId);
                origVipiska.CreateDate = vipiska.CreateDate;
                origVipiska.CreateUser = vipiska.CreateUser;
                origVipiska.Description = vipiska.Description;
                origVipiska.LastDate = vipiska.LastDate;
                origVipiska.LastUser = vipiska.LastUser;
                origVipiska.Result = vipiska.Result;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

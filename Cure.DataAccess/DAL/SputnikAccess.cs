using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Sputnik> GetOrderSputniks(int orderId)
        {
            return context.Sputniks.Where(x => x.OrderId == orderId).OrderBy(o => o.Name).ToList();
        }

        public IEnumerable<Sputnik> GetSputniks()
        {
            return context.Sputniks.OrderBy(o => o.Name).ToList();
        }

        public Sputnik GetSputnik(int id)
        {
            return context.Sputniks.FirstOrDefault(o => o.Id == id);
        }

        public void InsertSputnik(Sputnik sputnik)
        {
            try
            {
                context.Sputniks.AddObject(sputnik);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSputnik(Sputnik sputnik)
        {
            try
            {
                context.Sputniks.Attach(sputnik);
                context.Sputniks.DeleteObject(sputnik);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSputnik(Sputnik sputnik)
        {
            try
            {
                var origSputnik = GetSputnik(sputnik.Id);
                origSputnik.OrderId = sputnik.OrderId;
                origSputnik.BirthDate = sputnik.BirthDate;
                origSputnik.Contacts = sputnik.Contacts;
                origSputnik.Email = sputnik.Email;
                origSputnik.Familiya = sputnik.Familiya;
                origSputnik.FamiliyaEn = sputnik.FamiliyaEn;
                origSputnik.IsPrimary = sputnik.IsPrimary;
                origSputnik.LastDate = sputnik.LastDate;
                origSputnik.LastUser = sputnik.LastUser;
                origSputnik.Name = sputnik.Name;
                origSputnik.NameEn = sputnik.NameEn;
                origSputnik.Otchestvo = sputnik.Otchestvo;
                origSputnik.OwnerUser = sputnik.OwnerUser;
                origSputnik.RodstvoId = sputnik.RodstvoId;
                origSputnik.CountryId = sputnik.CountryId;
                origSputnik.SeriaNumber = sputnik.SeriaNumber;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

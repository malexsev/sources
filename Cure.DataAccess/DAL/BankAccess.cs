using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefBank> GetRefBanks()
        {
            return context.RefBanks.OrderByDescending(o => o.Name).ToList();
        }

        public IEnumerable<RefBank> GetRefBanks(int countryId)
        {
            return context.RefBanks.Where(o => o.CountryId == countryId).OrderByDescending(o => o.Name).ToList();
        }

        public RefBank GetRefBank(int id)
        {
            return context.RefBanks.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefBank(RefBank refBank)
        {
            try
            {
                context.RefBanks.AddObject(refBank);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefBank(RefBank refBank)
        {
            try
            {
                context.RefBanks.Attach(refBank);
                context.RefBanks.DeleteObject(refBank);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefBank(RefBank refBank)
        {
            try
            {
                var origRefBank = GetRefBank(refBank.Id);
                origRefBank.Bik = refBank.Bik;
                origRefBank.Description = refBank.Description;
                origRefBank.Inn = refBank.Inn;
                origRefBank.KorrAccount = refBank.KorrAccount;
                origRefBank.Kpp = refBank.Kpp;
                origRefBank.Name = refBank.Name;
                origRefBank.Oktmo = refBank.Oktmo;
                origRefBank.Okved = refBank.Okved;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

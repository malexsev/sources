using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefCountry> GetRefCountries()
        {
            return context.RefCountries.OrderBy(o => o.Name).ToList();
        }

        public RefCountry GetRefCountry(int id)
        {
            return context.RefCountries.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefCountry(RefCountry refCountry)
        {
            try
            {
                context.RefCountries.AddObject(refCountry);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefCountry(RefCountry refCountry)
        {
            try
            {
                context.RefCountries.Attach(refCountry);
                context.RefCountries.DeleteObject(refCountry);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefCountry(RefCountry refCountry)
        {
            try
            {
                var origRefCountry = GetRefCountry(refCountry.Id);
                origRefCountry.Name = refCountry.Name;
                origRefCountry.NameCh = refCountry.NameCh;
                origRefCountry.NameEn = refCountry.NameEn;
                origRefCountry.Description = refCountry.Description;
                origRefCountry.NacionalnostChLabel = refCountry.NacionalnostChLabel;
                origRefCountry.PosolstvoChLabel = refCountry.PosolstvoChLabel;
                origRefCountry.PosolstvoRusLabel = refCountry.PosolstvoRusLabel;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

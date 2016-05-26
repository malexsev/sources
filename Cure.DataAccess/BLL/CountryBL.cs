using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public IEnumerable<string> GetRegions()
        {
            return dataRepository.GetRegions();
        }

        public IEnumerable<string> GetRegions(int countryId)
        {
            return dataRepository.GetRegions(countryId);
        }

        public RefCountry GetRefCountry(int countryId)
        {
            return dataRepository.GetRefCountry(countryId);
        }

        public IEnumerable<RefCountry> GetRefCountries()
        {
            return dataRepository.GetRefCountries();
        }

        public void InsertRefCountry(RefCountry refCountry)
        {
            try
            {
                dataRepository.InsertRefCountry(refCountry);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefCountry(RefCountry refCountry)
        {
            try
            {
                dataRepository.DeleteRefCountry(refCountry);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefCountry(RefCountry refCountry)
        {
            try
            {
                dataRepository.UpdateRefCountry(refCountry);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

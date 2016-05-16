namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<RefBank> GetRefBanks()
        {
            return dataRepository.GetRefBanks();
        }

        public IEnumerable<RefBank> GetRefBanks(int countryId)
        {
            return dataRepository.GetRefBanks(countryId);
        }

        public void InsertRefBank(RefBank refBank)
        {
            try
            {
                dataRepository.InsertRefBank(refBank);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefBank(RefBank refBank)
        {
            try
            {
                dataRepository.DeleteRefBank(refBank);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefBank(RefBank refBank)
        {
            try
            {
                dataRepository.UpdateRefBank(refBank);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<RefOperator> GetRefOperators()
        {
            return dataRepository.GetRefOperators();
        }

        public IEnumerable<RefOperator> GetRefOperators(int countryId)
        {
            return dataRepository.GetRefOperators(countryId);
        }

        public void InsertRefOperator(RefOperator refOperator)
        {
            try
            {
                dataRepository.InsertRefOperator(refOperator);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefOperator(RefOperator refOperator)
        {
            try
            {
                dataRepository.DeleteRefOperator(refOperator);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefOperator(RefOperator refOperator)
        {
            try
            {
                dataRepository.UpdateRefOperator(refOperator);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<RefDiagnoz> GetExistingDiagnozs()
        {
            return dataRepository.GetExistingDiagnozs();
        }

        public IEnumerable<RefDiagnoz> GetRefDiagnozs()
        {
            return dataRepository.GetRefDiagnozs();
        }

        public void InsertRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                dataRepository.InsertRefDiagnoz(refDiagnoz);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                dataRepository.DeleteRefDiagnoz(refDiagnoz);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                dataRepository.UpdateRefDiagnoz(refDiagnoz);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

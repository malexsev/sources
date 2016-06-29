namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<RefStopVisitType> GetRefStopVisitTypes()
        {
            return dataRepository.GetRefStopVisitTypes();
        }
        
        public void InsertRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                dataRepository.InsertRefStopVisitType(refStopVisitType);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                dataRepository.DeleteRefStopVisitType(refStopVisitType);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                dataRepository.UpdateRefStopVisitType(refStopVisitType);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

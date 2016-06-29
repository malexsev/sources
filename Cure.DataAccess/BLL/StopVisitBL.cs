namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public bool CheckStopVisit(int departmentId, DateTime dateFrom, DateTime dateTo)
        {
            return dataRepository.CheckStopVisit(departmentId, dateFrom, dateTo);
        }

        public IEnumerable<StopVisit> GetStopVisitsForDepartment(int departmentId)
        {
            return dataRepository.GetStopVisitsForDepartment(departmentId);
        }

        public IEnumerable<StopVisit> GetStopVisits()
        {
            return dataRepository.GetStopVisits();
        }
        
        public void InsertStopVisit(StopVisit StopVisit)
        {
            try
            {
                dataRepository.InsertStopVisit(StopVisit);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteStopVisit(StopVisit StopVisit)
        {
            try
            {
                dataRepository.DeleteStopVisit(StopVisit);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStopVisit(StopVisit StopVisit)
        {
            try
            {
                dataRepository.UpdateStopVisit(StopVisit);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

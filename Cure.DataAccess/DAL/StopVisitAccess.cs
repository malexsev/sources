using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public bool CheckStopVisit(int departmentId, DateTime dateFrom, DateTime dateTo)
        {
            return context.StopVisits.Any(x => x.DepartmentId == departmentId
                && (x.DateFrom <= dateTo && x.DateTo >= dateFrom));
        }

        public IEnumerable<StopVisit> GetStopVisitsForDepartment(int departmentId)
        {
            return context.StopVisits.Where(x => x.DepartmentId == departmentId).OrderByDescending(o => o.DateFrom).ToList();
        }

        public IEnumerable<StopVisit> GetStopVisits()
        {
            return context.StopVisits.OrderByDescending(o => o.DateFrom).ToList();
        }

        public StopVisit GetStopVisit(int id)
        {
            return context.StopVisits.FirstOrDefault(o => o.Id == id);
        }

        public void InsertStopVisit(StopVisit StopVisit)
        {
            try
            {
                context.StopVisits.AddObject(StopVisit);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteStopVisit(StopVisit StopVisit)
        {
            try
            {
                context.StopVisits.Attach(StopVisit);
                context.StopVisits.DeleteObject(StopVisit);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStopVisit(StopVisit StopVisit)
        {
            try
            {
                var origStopVisit = GetStopVisit(StopVisit.Id);
                origStopVisit.Description = StopVisit.Description;
                origStopVisit.DateFrom = StopVisit.DateFrom;
                origStopVisit.DateTo = StopVisit.DateTo;
                origStopVisit.DepartmentId = StopVisit.DepartmentId;
                origStopVisit.StopTypeId = StopVisit.StopTypeId;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

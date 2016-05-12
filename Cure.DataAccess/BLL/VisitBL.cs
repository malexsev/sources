using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public IEnumerable<Visit> GetVisitsForTimespan(DateTime fromTime, DateTime toTime)
        {
            return dataRepository.GetVisitsForTimespan(fromTime, toTime);
        }
        
        public IEnumerable<Sputnik> GetOrderSputniks(int orderId)
        {
            return dataRepository.GetOrderSputniks(orderId);
        }

        public IEnumerable<Visit> GetVisits()
        {
            return dataRepository.GetVisits();
        }

        public Visit GetVisit(int visitId)
        {
            return dataRepository.GetVisit(visitId);
        }

        public IEnumerable<Visit> GetOrderVisits(int orderId)
        {
            return dataRepository.GetOrderVisits(orderId);
        }

        public void InsertVisit(Visit visit)
        {
            try
            {
                dataRepository.InsertVisit(visit);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVisit(Visit visit)
        {
            try
            {
                dataRepository.DeleteVisit(visit);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVisit(Visit visit)
        {
            try
            {
                dataRepository.UpdateVisit(visit);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

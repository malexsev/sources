using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public IEnumerable<Order> GetUnprocessedOrders()
        {
            return dataRepository.GetUnprocessedOrders();
        }

        public IEnumerable<Order> GetPendingDrafts()
        {
            return dataRepository.GetPendingDrafts();
        }

        public IEnumerable<ViewScheduler> GetScheduler()
        {
            return dataRepository.GetScheduler();
        }

        public Order GetOrder(int id)
        {
            return dataRepository.GetOrder(id);
        }

        public void SwitchOrderStatusTask()
        {
            dataRepository.SwitchOrderStatusTask();
        }

        public IEnumerable<ViewSoonVisit> ViewSoonOrders(int filter)
        {
            return dataRepository.ViewSoonOrders(filter);
        }

        public IEnumerable<ViewSoonVisit> ViewOutdatedStatus()
        {
            return dataRepository.ViewOutdatedStatus();
        }

        public IEnumerable<ViewSoonVisit> ViewCurrentVisits(int departmentId)
        {
            return dataRepository.ViewCurrentVisits(departmentId);
        }
        
        public IEnumerable<ViewSoonVisit> ViewSoonTransferOrders(string username)
        {
            return dataRepository.ViewSoonTransferOrders(username);
        }

        public IEnumerable<Order> GetOrders(int filter, string email, string familiya)
        {
            return dataRepository.GetOrders(filter, email, familiya);
        }

        public IEnumerable<Order> GetMyOrders(string username)
        {
            return dataRepository.GetMyOrders(username);
        }

        public Order GetOrderDraft(string username)
        {
            return dataRepository.GetOrderDraft(username);
        }

        public Order GetOrderCurrent(string username)
        {
            return dataRepository.GetOrderCurrent(username);
        }

        public IEnumerable<Order> GetOrders()
        {
            return dataRepository.GetOrders();
        }

        public void InsertOrder(Order order)
        {
            try
            {
                dataRepository.InsertOrder(order);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                dataRepository.DeleteOrder(order);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                dataRepository.UpdateOrder(order);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

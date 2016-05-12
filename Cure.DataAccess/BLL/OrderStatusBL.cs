using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<OrderStatu> GetOrderStatus()
        {
            return dataRepository.GetOrderStatus();
        }

        public void InsertOrderStatu(OrderStatu orderStatu)
        {
            try
            {
                dataRepository.InsertOrderStatu(orderStatu);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderStatu(OrderStatu orderStatu)
        {
            try
            {
                dataRepository.DeleteOrderStatu(orderStatu);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderStatu(OrderStatu orderStatu)
        {
            try
            {
                dataRepository.UpdateOrderStatu(orderStatu);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

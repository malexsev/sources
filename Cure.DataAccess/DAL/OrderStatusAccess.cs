using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<OrderStatu> GetOrderStatus()
        {
            return context.OrderStatus.OrderBy(o => o.SortOrder).ToList();
        }

        public OrderStatu GetOrderStatu(int id)
        {
            return context.OrderStatus.FirstOrDefault(o => o.Id == id);
        }

        public void InsertOrderStatu(OrderStatu order)
        {
            try
            {
                context.OrderStatus.AddObject(order);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderStatu(OrderStatu order)
        {
            try
            {
                context.OrderStatus.Attach(order);
                context.OrderStatus.DeleteObject(order);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderStatu(OrderStatu order)
        {
            try
            {
                var origOrderStatu = GetOrderStatu(order.Id);
                origOrderStatu.Name = order.Name;
                origOrderStatu.Description = order.Description;
                origOrderStatu.SortOrder = order.SortOrder;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

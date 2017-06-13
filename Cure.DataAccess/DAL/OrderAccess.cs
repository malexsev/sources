using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    using System.Data.Objects;

    internal partial class DataRepository
    {
        //Возвращает необработанные заявки, которые на рассмотрении 3 дня.
        public IEnumerable<Order> GetUnprocessedOrders()
        {
            var searchDate = DateTime.Today.AddDays(-3);
            return context.Orders.Where(x => x.DepartmentId.HasValue &&
                x.LastDate.HasValue &&
                x.StatusId == (int)Enums.OrderStatus.Новый &&
                (x.LastDate.Value.Year == searchDate.Year && x.LastDate.Value.Month == searchDate.Month && x.LastDate.Value.Day == searchDate.Day));
        }

        //Возвращает заявки, которые небыли отправлены вчера.
        public IEnumerable<Order> GetPendingDrafts()
        {
            var searchDate = DateTime.Today.AddDays(-1);
            return context.Orders.Where(x => x.DepartmentId.HasValue &&
                x.LastDate.HasValue &&
                x.StatusId == (int)Enums.OrderStatus.Черновик &&
                (x.LastDate.Value.Year == searchDate.Year && x.LastDate.Value.Month == searchDate.Month && x.LastDate.Value.Day == searchDate.Day));
        }

        public IEnumerable<ViewScheduler> GetScheduler()
        {
            return context.ViewSchedulers;
        }

        public Order GetOrderDraft(string username)
        {
            context.Refresh(RefreshMode.StoreWins, context.Orders.Where(o => o.OwnerUser == username));
            return context.Orders.FirstOrDefault(o => o.OwnerUser == username && o.StatusId == (int)Enums.OrderStatus.Черновик);
        }

        public void SwitchOrderStatusTask()
        {
            DateTime checkDate = DateTime.Today.AddDays(1);
            IQueryable<Order> orders = context.Orders.Where(x => x.TicketPribitieTime == checkDate && x.StatusId == (int)Enums.OrderStatus.КупленыБилеты);
            if (orders.Any())
            {
                orders.ToList().ForEach(x => x.StatusId = 8);
            }
            SaveChanges();
        }

        public Order GetOrderCurrent(string username)
        {
            return context.Orders.OrderByDescending(x => x.DateFrom).FirstOrDefault(o => o.OwnerUser == username && o.StatusId != (int)Enums.OrderStatus.Черновик);
        }

        public IEnumerable<ViewSoonVisit> ViewSoonOrders(int filter)
        {
            if (filter < 0)
            {
                return context.ViewSoonVisits.OrderBy(o => o.TicketPribitieTime).ToList();
            }
            else if (filter == 0)
            {
                return context.ViewSoonVisits.Where(x => x.StatusId != (int)Enums.OrderStatus.Черновик
                    && x.StatusId != (int)Enums.OrderStatus.Завершён
                    && x.StatusId != (int)Enums.OrderStatus.Отказался
                    && x.StatusId != (int)Enums.OrderStatus.Отказано
                    && x.DateFrom <= DateTime.Today && x.DateTo >= DateTime.Today)
                    .OrderBy(x => x.DateFrom)
                    .ThenBy(o => o.TicketPribitieTime).ToList();
            }
            DateTime maxDate = DateTime.Today.AddDays(filter);
            return context.ViewSoonVisits.Where(x => x.StatusId == (int)Enums.OrderStatus.КупленыБилеты
                    && x.DateFrom >= DateTime.Today && x.DateFrom <= maxDate).OrderBy(o => o.TicketPribitieTime).ThenBy(x => x.DateFrom).OrderBy(o => o.TicketPribitieTime).ToList();
        }

        public IEnumerable<ViewSoonVisit> ViewOutdatedStatus()
        {
            DateTime today = DateTime.Today;
            return context.ViewSoonVisits.Where(x => x.StatusId != (int)Enums.OrderStatus.Черновик
                && x.StatusId < (int)Enums.OrderStatus.Завершён
                && (x.DateTo < today || (x.TicketUbitieTime != null && x.TicketUbitieTime < today))).OrderBy(o => o.TicketPribitieTime).ThenBy(x => x.DateFrom).ToList();
        }

        public IEnumerable<ViewSoonVisit> ViewCurrentVisits(int departmentId)
        {
            DateTime today = DateTime.Today;
            return context.ViewSoonVisits.Where(x => x.DepartmentId == departmentId
                && x.StatusId != (int)Enums.OrderStatus.Черновик
                && x.StatusId < (int)Enums.OrderStatus.Завершён
                && (x.StatusId == (int)Enums.OrderStatus.Выполняется))
                .OrderByDescending(o => o.TicketPribitieTime).ThenByDescending(x => x.DateFrom).ToList();
            //(x.DateFrom >= today || (x.TicketPribitieTime != null && x.TicketPribitieTime >= today))
            //&& (x.DateTo <= today || (x.TicketUbitieTime != null && x.TicketUbitieTime <= today))
        }

        public IEnumerable<ViewSoonVisit> ViewSoonTransferOrders(string username)
        {
            DateTime maxDate = DateTime.Today; //.AddDays(-1);
            IQueryable<TransferUser> dips = context.TransferUsers.Where(x => x.Username == username);
            return context.ViewSoonVisits.Where(x => x.StatusId != (int)Enums.OrderStatus.Черновик && x.DateFrom >= maxDate && dips.Any(d => d.DepartmentId == x.DepartmentId)).OrderBy(o => o.TicketPribitieTime).ToList();
        }

        public IEnumerable<Order> GetOrders(int filter, string email, string familiya)
        {
            int[] sputnikIDs = { };
            int[] paciaentIDs = { };
            string[] userIDs = { };
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(familiya))
            {
                sputnikIDs = context.Sputniks.Where(x => (!string.IsNullOrEmpty(email) && x.Email.ToLower() == email.ToLower())
                    || (!string.IsNullOrEmpty(familiya) && (x.Familiya.ToLower() == familiya.ToLower() || x.FamiliyaEn.ToLower() == familiya.ToLower())))
                    .Select(x => x.Id).ToArray();
                paciaentIDs = context.Pacients.Where(x => (!string.IsNullOrEmpty(familiya) && (x.Familiya.ToLower() == familiya.ToLower() || x.FamiliyaEn.ToLower() == familiya.ToLower()))).Select(x => x.Id).ToArray();
                userIDs = context.ViewUserMemberships.Where(x => (!string.IsNullOrEmpty(email) && x.Email.ToLower() == email.ToLower())).Select(x => x.UserName).ToArray();
            }
            if (filter < 0)
            {
                return context.Orders.Where(x => (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(familiya))
                    || x.Sputniks.Any(s => sputnikIDs.Contains(s.Id))
                    || userIDs.Contains(x.OwnerUser)
                    || x.Visits.Any(v => paciaentIDs.Contains(v.PacientId))).OrderByDescending(x => x.CreateDate).ToList();
            }
            else if (filter == 0)
            {
                return context.Orders.Where(x => ((string.IsNullOrEmpty(email) && string.IsNullOrEmpty(familiya))
                    || x.Sputniks.Any(s => sputnikIDs.Contains(s.Id))
                    || userIDs.Contains(x.OwnerUser)
                    || x.Visits.Any(v => paciaentIDs.Contains(v.PacientId))) && x.StatusId != (int)Enums.OrderStatus.Черновик && x.DateFrom <= DateTime.Today && x.DateTo >= DateTime.Today).OrderByDescending(x => x.CreateDate).ToList();
            }
            DateTime maxDate = DateTime.Today.AddDays(filter);
            return context.Orders.Where(x => ((string.IsNullOrEmpty(email) && string.IsNullOrEmpty(familiya))
                || x.Sputniks.Any(s => sputnikIDs.Contains(s.Id))
                || userIDs.Contains(x.OwnerUser)
                || x.Visits.Any(v => paciaentIDs.Contains(v.PacientId))) && x.StatusId != (int)Enums.OrderStatus.Черновик && x.DateFrom >= DateTime.Today && x.DateFrom <= maxDate).OrderByDescending(o => o.CreateDate).ToList();
        }

        public IEnumerable<Order> GetMyOrders(string username)
        {
            return context.Orders.Where(x => x.StatusId != (int)Enums.OrderStatus.Черновик && x.OwnerUser == username).OrderByDescending(o => o.DateFrom).ToList();
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.Where(x => x.StatusId != (int)Enums.OrderStatus.Черновик).OrderByDescending(o => o.StatusId).ToList();
        }

        public Order GetOrder(int id)
        {
            return context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void InsertOrder(Order order)
        {
            try
            {
                order.GuidId = Guid.NewGuid();
                context.Orders.AddObject(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                context.Orders.Attach(order);
                context.Orders.DeleteObject(order);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                var origOrder = GetOrder(order.Id);
                origOrder.DepartmentId = order.DepartmentId;
                origOrder.TransferInfo = order.TransferInfo;
                origOrder.OwnerUser = order.OwnerUser;
                origOrder.Notes = order.Notes;
                origOrder.DateFrom = order.DateFrom;
                origOrder.DateTo = order.DateTo;
                origOrder.DateSend = order.DateSend;
                origOrder.IsAgree = order.IsAgree;
                origOrder.Dney = order.Dney;
                origOrder.VizaDney = order.VizaDney;
                origOrder.Description = order.Description;
                origOrder.TicketInfo = order.TicketInfo;
                origOrder.TicketPribitieTime = order.TicketPribitieTime;
                origOrder.TicketUbitieTime = order.TicketUbitieTime;
                origOrder.StatusId = order.StatusId;
                origOrder.LastDate = order.LastDate;
                origOrder.LastUser = order.LastUser;
                //Services
                origOrder.ServicePekinIsPerevod = order.ServicePekinIsPerevod;
                origOrder.ServicePekinIsHotel = order.ServicePekinIsHotel;
                origOrder.ServicePekinOther = order.ServicePekinOther;
                origOrder.ServiceUnchenIsVstrecha = order.ServiceUnchenIsVstrecha;
                origOrder.ServiceUnchenOther = order.ServiceUnchenOther;
                origOrder.ServiceRoomIsPaper = order.ServiceRoomIsPaper;
                origOrder.ServiceRoomIsStiral = order.ServiceRoomIsStiral;
                origOrder.ServiceRoomIsOpolask = order.ServiceRoomIsOpolask;
                origOrder.ServiceRoomIsMilo = order.ServiceRoomIsMilo;
                origOrder.ServiceRoomIsVoda = order.ServiceRoomIsVoda;
                origOrder.ServiceRoomIsPosuda = order.ServiceRoomIsPosuda;

                context.Refresh(RefreshMode.StoreWins, context.ViewSoonVisits);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderShort(Order order)
        {
            try
            {
                var origOrder = GetOrder(order.Id);
                origOrder.DepartmentId = order.DepartmentId;
                origOrder.IsAgree = order.IsAgree;
                origOrder.StatusId = order.StatusId;
                origOrder.DateFrom = order.DateFrom;
                origOrder.DateTo = order.DateTo;
                origOrder.TicketPribitieTime = order.TicketPribitieTime;
                origOrder.TicketUbitieTime = order.TicketUbitieTime;
                origOrder.TransferInfo = order.TransferInfo;
                origOrder.TicketInfo = order.TicketInfo;
                origOrder.Description = order.Description;
                origOrder.OwnerUser = order.OwnerUser;
                origOrder.Notes = order.Notes;
                origOrder.VizaDney = order.VizaDney;
                origOrder.LastDate = order.LastDate;
                origOrder.LastUser = order.LastUser;

                context.Refresh(RefreshMode.StoreWins, context.ViewSoonVisits);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<NotificationLog> GetNotificationLogs()
        {
            return context.NotificationLogs.OrderByDescending(o => o.ExecutionDate).ToList();
        }

        public NotificationLog GetNotificationLog(int id)
        {
            return context.NotificationLogs.FirstOrDefault(o => o.Id == id);
        }

        public void InsertNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                context.NotificationLogs.AddObject(notificationLog);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                context.NotificationLogs.Attach(notificationLog);
                context.NotificationLogs.DeleteObject(notificationLog);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                var origNotificationLog = GetNotificationLog(notificationLog.Id);
                origNotificationLog.ClientName = notificationLog.ClientName;
                origNotificationLog.Contacts = notificationLog.Contacts;
                origNotificationLog.Name = notificationLog.Name;
                origNotificationLog.Description = notificationLog.Description;
                origNotificationLog.Details = notificationLog.Details;
                origNotificationLog.Contacts = notificationLog.Contacts;
                origNotificationLog.ExecutionDate = notificationLog.ExecutionDate;
                origNotificationLog.Result = notificationLog.Result;
                origNotificationLog.Type = notificationLog.Type;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<NotificationLog> GetNotificationLogs()
        {
            return dataRepository.GetNotificationLogs();
        }

        public void InsertNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                dataRepository.InsertNotificationLog(notificationLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                dataRepository.DeleteNotificationLog(notificationLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                dataRepository.UpdateNotificationLog(notificationLog);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

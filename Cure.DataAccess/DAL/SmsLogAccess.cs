using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<SmsLog> GetSmsLogs()
        {
            return context.SmsLogs.OrderByDescending(o => o.Date).ToList();
        }

        public SmsLog GetSmsLog(Guid guid)
        {
            return context.SmsLogs.FirstOrDefault(o => o.Id == guid);
        }

        public void InsertSmsLog(SmsLog smsLog)
        {
            try
            {
                smsLog.Id = Guid.NewGuid();
                context.SmsLogs.AddObject(smsLog);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSmsLog(SmsLog smsLog)
        {
            try
            {
                context.SmsLogs.Attach(smsLog);
                context.SmsLogs.DeleteObject(smsLog);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSmsLog(SmsLog smsLog)
        {
            try
            {
                var origSmsLog = GetSmsLog(smsLog.Id);
                origSmsLog.Addition = smsLog.Addition;
                origSmsLog.Date = smsLog.Date;
                origSmsLog.PhoneNumber = smsLog.PhoneNumber;
                origSmsLog.Reason = smsLog.Reason;
                origSmsLog.Text = smsLog.Text;
                origSmsLog.Description = smsLog.Description;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

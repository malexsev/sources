namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<SmsLog> GetSmsLogs()
        {
            return dataRepository.GetSmsLogs();
        }

        public void InsertSmsLog(SmsLog smsLog)
        {
            try
            {
                dataRepository.InsertSmsLog(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSmsLog(SmsLog smsLog)
        {
            try
            {
                dataRepository.DeleteSmsLog(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSmsLog(SmsLog smsLog)
        {
            try
            {
                dataRepository.UpdateSmsLog(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public IEnumerable<UploadLog> GetUnReportedLogs()
        {
            return dataRepository.GetUnReportedLogs();
        }

        public IEnumerable<UploadLog> GetUploadLogs()
        {
            return dataRepository.GetUploadLogs();
        }

        public void InsertUploadLog(UploadLog uploadLog)
        {
            try
            {
                dataRepository.InsertUploadLog(uploadLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUploadLog(UploadLog uploadLog)
        {
            try
            {
                dataRepository.DeleteUploadLog(uploadLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUploadLog(UploadLog uploadLog)
        {
            try
            {
                dataRepository.UpdateUploadLog(uploadLog);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

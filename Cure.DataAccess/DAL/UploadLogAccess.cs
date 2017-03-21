using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<UploadLog> GetUnReportedLogs()
        {
            var logs = context.UploadLogs.Where(o => o.IsReported == false).ToList();
            logs.ForEach(o =>
            {
                o.IsReported = true;
                UpdateUploadLog(o);
            });

            return logs;
        }

        public IEnumerable<UploadLog> GetUploadLogs()
        {
            return context.UploadLogs.OrderByDescending(o => o.UploadDate).ToList();
        }

        public UploadLog GetUploadLog(int id)
        {
            return context.UploadLogs.FirstOrDefault(o => o.Id == id);
        }

        public void InsertUploadLog(UploadLog uploadLog)
        {
            try
            {
                context.UploadLogs.AddObject(uploadLog);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUploadLog(UploadLog uploadLog)
        {
            try
            {
                context.UploadLogs.Attach(uploadLog);
                context.UploadLogs.DeleteObject(uploadLog);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUploadLog(UploadLog uploadLog)
        {
            try
            {
                var origUploadLog = GetUploadLog(uploadLog.Id);
                origUploadLog.FileName = uploadLog.FileName;
                origUploadLog.GuidId = uploadLog.GuidId;
                origUploadLog.ServerPath = uploadLog.ServerPath;
                origUploadLog.UploadDate = uploadLog.UploadDate;
                origUploadLog.Username = uploadLog.Username;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

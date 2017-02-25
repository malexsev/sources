using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<DepartmentTransferInfo> GetDepartmentTransferInfos()
        {
            return context.DepartmentTransferInfoes.OrderBy(o => o.DepartmentId).ThenBy(x => x.Name).ToList();
        }

        public DepartmentTransferInfo GetDepartmentTransferInfo(int id)
        {
            return context.DepartmentTransferInfoes.FirstOrDefault(o => o.Id == id);
        }

        public void InsertDepartmentTransferInfo(DepartmentTransferInfo info)
        {
            try
            {
                context.DepartmentTransferInfoes.AddObject(info);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при записи в БД", ex); ;
            }
        }

        public void DeleteDepartmentTransferInfo(DepartmentTransferInfo info)
        {
            try
            {
                context.DepartmentTransferInfoes.Attach(info);
                context.DepartmentTransferInfoes.DeleteObject(info);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDepartmentTransferInfo(DepartmentTransferInfo info)
        {
            try
            {
                var origDepartmentTransferInfo = GetDepartmentTransferInfo(info.Id);
                origDepartmentTransferInfo.DepartmentId = info.DepartmentId;
                origDepartmentTransferInfo.Description = info.Description;
                origDepartmentTransferInfo.Name = info.Name;
                origDepartmentTransferInfo.Text = info.Text;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

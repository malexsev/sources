using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<ViewMension> ViewMensions(int filterId, int skipRecords, int takeRecords = 12)
        {
            return context.ViewMensions.Where(o => filterId == o.DepartmentId || filterId == 0 || (filterId == -1 && o.DepartmentId == null))
                .OrderByDescending(x => x.CreatedDate)
                .Skip(skipRecords).Take(takeRecords).ToList();
        }

        public int CountMensions(int filterId)
        {
            return context.ViewMensions.Count(o => filterId == o.DepartmentId || filterId == 0 || (filterId == -1 && o.DepartmentId == null));
        }

        public IEnumerable<Mension> GetTopMensions()
        {
            return context.Mensions.OrderByDescending(o => o.SortOrder).ToList();
        }

        public IEnumerable<Mension> GetMensions()
        {
            return context.Mensions.OrderByDescending(o => o.CreatedDate).ToList();
        }

        public IEnumerable<Mension> GetMensionsByDepartment(int? department)
        {
            return context.Mensions.Where(o => department == null || department <= 0 || o.DepartmentId == department).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public Mension GetMension(int id)
        {
            return context.Mensions.FirstOrDefault(o => o.Id == id);
        }

        public void InsertMension(Mension setting)
        {
            try
            {
                context.Mensions.AddObject(setting);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMension(Mension setting)
        {
            try
            {
                context.Mensions.Attach(setting);
                context.Mensions.DeleteObject(setting);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMension(Mension setting)
        {
            try
            {
                var origMension = GetMension(setting.Id);
                origMension.CopySubject = setting.CopySubject;
                origMension.CopyUserLocation = setting.CopyUserLocation;
                origMension.CopyUserName = setting.CopyUserName;
                origMension.CreatedDate = setting.CreatedDate;
                origMension.DepartmentId = setting.DepartmentId;
                origMension.IsActive = setting.IsActive;
                origMension.OwnerUser = setting.OwnerUser;
                origMension.SortOrder = setting.SortOrder;
                origMension.Text = setting.Text;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Department> GetDepartments()
        {
            return context.Departments.OrderBy(o => o.Name).ToList();
        }

        public Department GetDepartment(int id)
        {
            return context.Departments.FirstOrDefault(o => o.Id == id);
        }

        public void InsertDepartment(Department department)
        {
            try
            {
                context.Departments.AddObject(department);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDepartment(Department department)
        {
            try
            {
                context.Departments.Attach(department);
                context.Departments.DeleteObject(department);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                var origDepartment = GetDepartment(department.Id);
                origDepartment.Address = department.Address;
                origDepartment.AddressChina = department.AddressChina;
                origDepartment.AddressEnglish = department.AddressEnglish;
                origDepartment.BossName = department.BossName;
                origDepartment.Branch = department.Branch;
                origDepartment.Contacts = department.Contacts;
                origDepartment.CountryId = department.CountryId;
                origDepartment.LastDate = department.LastDate;
                origDepartment.LastUser = department.LastUser;
                origDepartment.Name = department.Name;
                origDepartment.ShortName = department.ShortName;
                origDepartment.NameChina = department.NameChina;
                origDepartment.NameEnglish = department.NameEnglish;
                origDepartment.ParentId = department.ParentId;
                origDepartment.PechatFileName = department.PechatFileName;
                origDepartment.PodpisFileName = department.PodpisFileName;
                origDepartment.Requisits = department.Requisits;
                origDepartment.AdditionalCh = department.AdditionalCh;
                origDepartment.AdditionalRu = department.AdditionalRu;
                origDepartment.PriglashenieCh = department.PriglashenieCh;
                origDepartment.PriglashenieRu = department.PriglashenieRu;
                origDepartment.DescriptionCh = department.DescriptionCh;
                origDepartment.DescriptionRu = department.DescriptionRu;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

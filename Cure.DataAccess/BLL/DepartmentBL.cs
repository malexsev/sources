using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<Department> GetDepSubject()
        {
            var deps = dataRepository.GetDepartments().ToList();
            deps.Add(new Department() { Name = "Работа сервиса, организация лечения", Id = -1 });
            return deps;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return dataRepository.GetDepartments();
        }

        public void InsertDepartment(Department department)
        {
            try
            {
                dataRepository.InsertDepartment(department);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDepartment(Department department)
        {
            try
            {
                dataRepository.DeleteDepartment(department);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                dataRepository.UpdateDepartment(department);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

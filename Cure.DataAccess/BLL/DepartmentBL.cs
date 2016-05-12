using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

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

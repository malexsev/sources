using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    using System.Data;

    public partial class DataAccessBL
    {
        public DepartmentTransferInfo GetDepartmentTransferInfo(int id)
        {
            return dataRepository.GetDepartmentTransferInfo(id);
        }

        public IEnumerable<DepartmentTransferInfo> GetDepartmentTransferInfos()
        {
            return dataRepository.GetDepartmentTransferInfos();
        }

        public void InsertDepartmentTransferInfo(DepartmentTransferInfo info)
        {
            try
            {
                dataRepository.InsertDepartmentTransferInfo(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDepartmentTransferInfo(DepartmentTransferInfo info)
        {
            try
            {
                dataRepository.DeleteDepartmentTransferInfo(info);
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
                dataRepository.UpdateDepartmentTransferInfo(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

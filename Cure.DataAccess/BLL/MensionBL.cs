using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public IEnumerable<Mension> GetMensionsByDepartment(int? depId)
        {
            return dataRepository.GetMensionsByDepartment(depId);
        }

        public IEnumerable<Mension> GetTopMensions()
        {
            return dataRepository.GetTopMensions();
        }

        public IEnumerable<Mension> GetMensions()
        {
            return dataRepository.GetMensions();
        }

        public void InsertMension(Mension mension)
        {
            try
            {
                dataRepository.InsertMension(mension);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMension(Mension mension)
        {
            try
            {
                dataRepository.DeleteMension(mension);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMension(Mension mension)
        {
            try
            {
                dataRepository.UpdateMension(mension);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

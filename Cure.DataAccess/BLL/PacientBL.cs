using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<Pacient> GetPacients(int orderId)
        {
            return dataRepository.GetPacients(orderId);
        }

        public IEnumerable<Pacient> GetPacients()
        {
            return dataRepository.GetPacients();
        }

        public IEnumerable<Pacient> GetPacients(string email)
        {
            return dataRepository.GetPacients(email);
        }

        public void InsertPacient(Pacient pacient)
        {
            try
            {
                dataRepository.InsertPacient(pacient);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePacient(int pacientId)
        {
            try
            {
                dataRepository.DeletePacient(pacientId);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePacient(Pacient pacient)
        {
            try
            {
                dataRepository.DeletePacient(pacient);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePacient(Pacient pacient)
        {
            try
            {
                dataRepository.UpdatePacient(pacient);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

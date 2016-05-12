using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<Sputnik> GetSputniks()
        {
            return dataRepository.GetSputniks();
        }

        public Sputnik GetSputnik(int sputnikId)
        {
            return dataRepository.GetSputnik(sputnikId);
        }

        public IEnumerable<Sputnik> GetSputniks(int orderId)
        {
            return dataRepository.GetOrderSputniks(orderId);
        }

        public void InsertSputnik(Sputnik sputnik)
        {
            try
            {
                dataRepository.InsertSputnik(sputnik);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSputnik(Sputnik sputnik)
        {
            try
            {
                dataRepository.DeleteSputnik(sputnik);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSputnik(Sputnik sputnik)
        {
            try
            {
                dataRepository.UpdateSputnik(sputnik);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

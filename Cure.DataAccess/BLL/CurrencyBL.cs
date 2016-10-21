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
        public IEnumerable<Currency> GetCurrencies()
        {
            return dataRepository.GetCurrencies();
        }

        public void InsertCurrency(Currency currency)
        {
            try
            {
                dataRepository.InsertCurrency(currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCurrency(Currency currency)
        {
            try
            {
                dataRepository.DeleteCurrency(currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCurrency(Currency currency)
        {
            try
            {
                dataRepository.UpdateCurrency(currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

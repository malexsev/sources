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

        public string ValidateCurrencyRate(CurrencyRate currencyRate)
        {
            return string.Empty;
        }

        public IEnumerable<CurrencyRate> GetCurrencyRates()
        {
            return dataRepository.GetCurrencyRates();
        }

        public void InsertCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                dataRepository.InsertCurrencyRate(currencyRate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                dataRepository.DeleteCurrencyRate(currencyRate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                dataRepository.UpdateCurrencyRate(currencyRate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateCurrencyRates(DataTable data, string username)
        {
            try
            {
                foreach (DataRow dr in data.Rows)
                {
                    CurrencyRate currentRate = dataRepository.GetCurrencyRate(dr["from"].ToString(), dr["to"].ToString(), DateTime.Today);
                    if (currentRate == null)
                    {
                        currentRate = new CurrencyRate
                                          {
                                              CurrencyFrom = dr["from"].ToString(),
                                              CurrencyTo = dr["to"].ToString(),
                                              Rate = (decimal)dr["rate"],
                                              Date = DateTime.Today,
                                              GetDate = DateTime.Now
                                          };
                        dataRepository.InsertCurrencyRate(currentRate);
                    }
                    else
                    {
                        currentRate.Rate = (decimal)dr["rate"];
                        currentRate.GetDate = DateTime.Now;
                        dataRepository.UpdateCurrencyRate(currentRate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

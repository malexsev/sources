using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public CurrencyRate GetCurrencyRate(string curFrom, string curTo, DateTime date)
        {
            return context.CurrencyRates.FirstOrDefault(o => o.CurrencyFrom == curFrom && o.CurrencyTo == curTo && o.Date == date);
        }

        public IEnumerable<CurrencyRate> GetCurrencyRates()
        {
            return context.CurrencyRates.OrderByDescending(o => o.Date).ThenBy(o => o.CurrencyFrom).ThenBy(o => o.CurrencyTo).ToList();
        }

        public CurrencyRate GetCurrencyRate(int id)
        {
            return context.CurrencyRates.FirstOrDefault(o => o.Id == id);
        }

        public void InsertCurrencyRate(CurrencyRate currencyRate)
        {
            try
            {
                context.CurrencyRates.AddObject(currencyRate);
                context.SaveChanges();
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
                context.CurrencyRates.Attach(currencyRate);
                context.CurrencyRates.DeleteObject(currencyRate);
                SaveChanges();
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
                var origCurrencyRate = GetCurrencyRate(currencyRate.Id);
                origCurrencyRate.CurrencyFrom = currencyRate.CurrencyFrom;
                origCurrencyRate.CurrencyTo = currencyRate.CurrencyTo;
                origCurrencyRate.GetDate = currencyRate.GetDate;
                origCurrencyRate.Date = currencyRate.Date;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

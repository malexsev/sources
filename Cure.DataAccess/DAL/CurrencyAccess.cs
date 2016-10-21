using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Currency> GetCurrencies()
        {
            return context.Currencies.OrderBy(o => o.Name).ToList();
        }

        public Currency GetCurrency(string name)
        {
            return context.Currencies.FirstOrDefault(o => o.Name == name);
        }

        public void InsertCurrency(Currency currency)
        {
            try
            {
                context.Currencies.AddObject(currency);
                context.SaveChanges();
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
                context.Currencies.Attach(currency);
                context.Currencies.DeleteObject(currency);
                SaveChanges();
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
                var origCurrency = GetCurrency(currency.Name);
                origCurrency.Description = currency.Description;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

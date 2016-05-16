using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefOperator> GetRefOperators()
        {
            return context.RefOperators.OrderByDescending(o => o.Name).ToList();
        }

        public IEnumerable<RefOperator> GetRefOperators(int countryId)
        {
            return context.RefOperators.Where(o => o.CountryId == countryId).OrderByDescending(o => o.Name).ToList();
        }

        public RefOperator GetRefOperator(int id)
        {
            return context.RefOperators.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefOperator(RefOperator refOperator)
        {
            try
            {
                context.RefOperators.AddObject(refOperator);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefOperator(RefOperator refOperator)
        {
            try
            {
                context.RefOperators.Attach(refOperator);
                context.RefOperators.DeleteObject(refOperator);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefOperator(RefOperator refOperator)
        {
            try
            {
                var origRefOperator = GetRefOperator(refOperator.Id);
                origRefOperator.Description = refOperator.Description;
                origRefOperator.Name = refOperator.Name;
                origRefOperator.Params = refOperator.Params;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

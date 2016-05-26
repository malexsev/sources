using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {

        public IEnumerable<RefDiagnoz> GetExistingDiagnozs()
        {
            var existsIds = context.Children.Where(x => x.IsActive == true).GroupBy(x => x.DiagnozId).Select(x => x.Key);
            return context.RefDiagnozs.Where(x => existsIds.Contains(x.Id)).OrderBy(o => o.Name).ToList();
        }

        public IEnumerable<RefDiagnoz> GetRefDiagnozs()
        {
            return context.RefDiagnozs.OrderBy(o => o.Name).ToList();
        }

        public RefDiagnoz GetRefDiagnoz(int id)
        {
            return context.RefDiagnozs.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                context.RefDiagnozs.AddObject(refDiagnoz);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                context.RefDiagnozs.Attach(refDiagnoz);
                context.RefDiagnozs.DeleteObject(refDiagnoz);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefDiagnoz(RefDiagnoz refDiagnoz)
        {
            try
            {
                var origRefDiagnoz = GetRefDiagnoz(refDiagnoz.Id);
                origRefDiagnoz.Description = refDiagnoz.Description;
                origRefDiagnoz.Name = refDiagnoz.Name;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

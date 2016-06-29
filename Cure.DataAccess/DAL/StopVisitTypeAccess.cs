using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefStopVisitType> GetRefStopVisitTypes()
        {
            return context.RefStopVisitTypes.OrderByDescending(o => o.Name).ToList();
        }

        public RefStopVisitType GetRefStopVisitType(int id)
        {
            return context.RefStopVisitTypes.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                context.RefStopVisitTypes.AddObject(refStopVisitType);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                context.RefStopVisitTypes.Attach(refStopVisitType);
                context.RefStopVisitTypes.DeleteObject(refStopVisitType);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefStopVisitType(RefStopVisitType refStopVisitType)
        {
            try
            {
                var origRefStopVisitType = GetRefStopVisitType(refStopVisitType.Id);
                origRefStopVisitType.Description = refStopVisitType.Description;
                origRefStopVisitType.Name = refStopVisitType.Name;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

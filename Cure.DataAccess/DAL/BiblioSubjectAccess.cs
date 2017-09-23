using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<BiblioSubject> GetBiblioSubjects()
        {
            return context.BiblioSubjects.OrderBy(o => o.Sort).ToList();
        }

        public BiblioSubject GetBiblioSubject(int id)
        {
            return context.BiblioSubjects.FirstOrDefault(o => o.Id == id);
        }

        public void InsertBiblioSubject(BiblioSubject gmfcsLevel)
        {
            try
            {
                context.BiblioSubjects.AddObject(gmfcsLevel);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBiblioSubject(BiblioSubject gmfcsLevel)
        {
            try
            {
                context.BiblioSubjects.Attach(gmfcsLevel);
                context.BiblioSubjects.DeleteObject(gmfcsLevel);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBiblioSubject(BiblioSubject gmfcsLevel)
        {
            try
            {
                var origBiblioSubject = GetBiblioSubject(gmfcsLevel.Id);
                origBiblioSubject.Name = gmfcsLevel.Name;
                origBiblioSubject.Sort = gmfcsLevel.Sort;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

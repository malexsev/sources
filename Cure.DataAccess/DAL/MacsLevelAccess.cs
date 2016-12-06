using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefMacsLevel> GetRefMacsLevels()
        {
            return context.RefMacsLevels.OrderBy(o => o.Name).ToList();
        }

        public RefMacsLevel GetRefMacsLevel(int id)
        {
            return context.RefMacsLevels.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefMacsLevel(RefMacsLevel gmfcsLevel)
        {
            try
            {
                context.RefMacsLevels.AddObject(gmfcsLevel);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefMacsLevel(RefMacsLevel gmfcsLevel)
        {
            try
            {
                context.RefMacsLevels.Attach(gmfcsLevel);
                context.RefMacsLevels.DeleteObject(gmfcsLevel);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefMacsLevel(RefMacsLevel gmfcsLevel)
        {
            try
            {
                var origRefMacsLevel = GetRefMacsLevel(gmfcsLevel.Id);
                origRefMacsLevel.Description = gmfcsLevel.Description;
                origRefMacsLevel.DescriptionCh = gmfcsLevel.DescriptionCh;
                origRefMacsLevel.DescriptionEn = gmfcsLevel.DescriptionEn;
                origRefMacsLevel.Name = gmfcsLevel.Name;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

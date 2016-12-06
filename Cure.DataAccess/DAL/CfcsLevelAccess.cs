using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefCfcsLevel> GetRefCfcsLevels()
        {
            return context.RefCfcsLevels.OrderBy(o => o.Name).ToList();
        }

        public RefCfcsLevel GetRefCfcsLevel(int id)
        {
            return context.RefCfcsLevels.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefCfcsLevel(RefCfcsLevel gmfcsLevel)
        {
            try
            {
                context.RefCfcsLevels.AddObject(gmfcsLevel);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefCfcsLevel(RefCfcsLevel gmfcsLevel)
        {
            try
            {
                context.RefCfcsLevels.Attach(gmfcsLevel);
                context.RefCfcsLevels.DeleteObject(gmfcsLevel);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefCfcsLevel(RefCfcsLevel gmfcsLevel)
        {
            try
            {
                var origRefCfcsLevel = GetRefCfcsLevel(gmfcsLevel.Id);
                origRefCfcsLevel.Description = gmfcsLevel.Description;
                origRefCfcsLevel.DescriptionCh = gmfcsLevel.DescriptionCh;
                origRefCfcsLevel.DescriptionEn = gmfcsLevel.DescriptionEn;
                origRefCfcsLevel.Name = gmfcsLevel.Name;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

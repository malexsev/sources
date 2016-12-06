using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefGmfcsLevel> GetRefGmfcsLevels()
        {
            return context.RefGmfcsLevels.OrderBy(o => o.Name).ToList();
        }

        public RefGmfcsLevel GetRefGmfcsLevel(int id)
        {
            return context.RefGmfcsLevels.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefGmfcsLevel(RefGmfcsLevel gmfcsLevel)
        {
            try
            {
                context.RefGmfcsLevels.AddObject(gmfcsLevel);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefGmfcsLevel(RefGmfcsLevel gmfcsLevel)
        {
            try
            {
                context.RefGmfcsLevels.Attach(gmfcsLevel);
                context.RefGmfcsLevels.DeleteObject(gmfcsLevel);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefGmfcsLevel(RefGmfcsLevel gmfcsLevel)
        {
            try
            {
                var origRefGmfcsLevel = GetRefGmfcsLevel(gmfcsLevel.Id);
                origRefGmfcsLevel.Description = gmfcsLevel.Description;
                origRefGmfcsLevel.DescriptionCh = gmfcsLevel.DescriptionCh;
                origRefGmfcsLevel.DescriptionEn = gmfcsLevel.DescriptionEn;
                origRefGmfcsLevel.Name = gmfcsLevel.Name;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

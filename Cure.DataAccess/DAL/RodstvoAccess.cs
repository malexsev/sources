using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<RefRodstvo> GetRefRodstvo()
        {
            return context.RefRodstvoes.OrderBy(o => o.Sort).ToList();
        }

        public RefRodstvo GetRefRodstvo(int id)
        {
            return context.RefRodstvoes.FirstOrDefault(o => o.Id == id);
        }

        public void InsertRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                context.RefRodstvoes.AddObject(rodstvo);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                context.RefRodstvoes.Attach(rodstvo);
                context.RefRodstvoes.DeleteObject(rodstvo);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                var origRefRodstvo = GetRefRodstvo(rodstvo.Id);
                origRefRodstvo.Description = rodstvo.Description;
                origRefRodstvo.Name = rodstvo.Name;
                origRefRodstvo.Sort = rodstvo.Sort;
                origRefRodstvo.SoprovodChLabel = rodstvo.SoprovodChLabel;
                origRefRodstvo.SoprovodRuLabel = rodstvo.SoprovodRuLabel;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

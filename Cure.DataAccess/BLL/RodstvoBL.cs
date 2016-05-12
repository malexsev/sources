using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<RefRodstvo> GetRefRodstvo()
        {
            return dataRepository.GetRefRodstvo();
        }

        public void InsertRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                dataRepository.InsertRefRodstvo(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                dataRepository.DeleteRefRodstvo(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefRodstvo(RefRodstvo rodstvo)
        {
            try
            {
                dataRepository.UpdateRefRodstvo(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

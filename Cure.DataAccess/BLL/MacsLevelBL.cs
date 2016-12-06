using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<RefMacsLevel> GetRefMacsLevels()
        {
            return dataRepository.GetRefMacsLevels();
        }

        public void InsertRefMacsLevel(RefMacsLevel rodstvo)
        {
            try
            {
                dataRepository.InsertRefMacsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefMacsLevel(RefMacsLevel rodstvo)
        {
            try
            {
                dataRepository.DeleteRefMacsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefMacsLevel(RefMacsLevel rodstvo)
        {
            try
            {
                dataRepository.UpdateRefMacsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

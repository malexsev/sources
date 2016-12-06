using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<RefGmfcsLevel> GetRefGmfcsLevels()
        {
            return dataRepository.GetRefGmfcsLevels();
        }

        public void InsertRefGmfcsLevel(RefGmfcsLevel rodstvo)
        {
            try
            {
                dataRepository.InsertRefGmfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefGmfcsLevel(RefGmfcsLevel rodstvo)
        {
            try
            {
                dataRepository.DeleteRefGmfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefGmfcsLevel(RefGmfcsLevel rodstvo)
        {
            try
            {
                dataRepository.UpdateRefGmfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

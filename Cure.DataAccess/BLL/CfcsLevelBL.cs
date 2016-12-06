using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<RefCfcsLevel> GetRefCfcsLevels()
        {
            return dataRepository.GetRefCfcsLevels();
        }

        public void InsertRefCfcsLevel(RefCfcsLevel rodstvo)
        {
            try
            {
                dataRepository.InsertRefCfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRefCfcsLevel(RefCfcsLevel rodstvo)
        {
            try
            {
                dataRepository.DeleteRefCfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRefCfcsLevel(RefCfcsLevel rodstvo)
        {
            try
            {
                dataRepository.UpdateRefCfcsLevel(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

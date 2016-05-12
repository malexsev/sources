using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {
        public Setting GetSettingByCode(string code)
        {
            return dataRepository.GetSettingByCode(code);
        }

        public IEnumerable<Setting> GetSettings()
        {
            return dataRepository.GetSettings();
        }

        public void InsertSetting(Setting setting)
        {
            try
            {
                dataRepository.InsertSetting(setting);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSetting(Setting setting)
        {
            try
            {
                dataRepository.DeleteSetting(setting);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSetting(Setting setting)
        {
            try
            {
                dataRepository.UpdateSetting(setting);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public Setting GetSettingByCode(string code)
        {
            return context.Settings.FirstOrDefault(o => o.Code == code);
        }

        public IEnumerable<Setting> GetSettings()
        {
            return context.Settings.OrderBy(o => o.Name).ToList();
        }

        public Setting GetSetting(int id)
        {
            return context.Settings.FirstOrDefault(o => o.Id == id);
        }

        public void InsertSetting(Setting setting)
        {
            try
            {
                context.Settings.AddObject(setting);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSetting(Setting setting)
        {
            try
            {
                context.Settings.Attach(setting);
                context.Settings.DeleteObject(setting);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSetting(Setting setting)
        {
            try
            {
                var origSetting = GetSetting(setting.Id);
                origSetting.Code = setting.Code;
                origSetting.Description = setting.Description;
                origSetting.Name = setting.Name;
                origSetting.Type = setting.Type;
                origSetting.Value = setting.Value;
                origSetting.ValueBool = setting.ValueBool;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

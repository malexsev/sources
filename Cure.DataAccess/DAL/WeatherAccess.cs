using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public Weather GetWeatherByCity(int cityId)
        {
            return context.Weathers.Where(x => x.CityId == cityId).OrderByDescending(o => o.GetDate).FirstOrDefault();
        }

        public IEnumerable<Weather> GetWeathers()
        {
            return context.Weathers.OrderByDescending(o => o.GetDate).ToList();
        }

        public Weather GetWeather(int id)
        {
            return context.Weathers.FirstOrDefault(o => o.Id == id);
        }

        public void InsertWeather(Weather newsletter)
        {
            try
            {
                context.Weathers.AddObject(newsletter);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteWeather(Weather newsletter)
        {
            try
            {
                context.Weathers.Attach(newsletter);
                context.Weathers.DeleteObject(newsletter);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateWeather(Weather newsletter)
        {
            try
            {
                var origWeather = GetWeather(newsletter.Id);
                origWeather.CityId = newsletter.CityId;
                origWeather.Description = newsletter.Description;
                origWeather.Details = newsletter.Details;
                origWeather.Temp = newsletter.Temp;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

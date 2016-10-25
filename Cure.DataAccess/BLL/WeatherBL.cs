namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public Weather GetWeatherByCity(int cityId)
        {
            return dataRepository.GetWeatherByCity(cityId);
        }

        public IEnumerable<Weather> GetWeathers()
        {
            return dataRepository.GetWeathers();
        }

        public void InsertWeather(Weather weather)
        {
            try
            {
                dataRepository.InsertWeather(weather);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteWeather(Weather weather)
        {
            try
            {
                dataRepository.DeleteWeather(weather);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateWeather(Weather weather)
        {
            try
            {
                dataRepository.UpdateWeather(weather);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

using System;

namespace Cure.Utils
{
    using System.Collections.Generic;
    using System.Xml;
    using DataAccess;
    using DataAccess.BLL;

    public static class WeatherUtils
    {
        const int HOUR = 2; // 0 - 3; 1 - 9; 2 - 15; 3 - 21
        private const string URI = "http://informer.gismeteo.ua/xml/{0}_1.xml";

        public static void ParseWeather()
        {
            var dal = new DataAccessBL();
            InsertWeather(CityWeather("Алматы", 36870), ref dal);
            InsertWeather(CityWeather("Янчен", 50207), ref dal);
            InsertWeather(CityWeather("Москва", 27612), ref dal);
            InsertWeather(CityWeather("Ташкент", 38457), ref dal);
            InsertWeather(CityWeather("Киев", 33345), ref dal);
            InsertWeather(CityWeather("Минск", 26850), ref dal);
            InsertWeather(CityWeather("Красноярск", 29574), ref dal);
            InsertWeather(CityWeather("Новосибирск", 29634), ref dal);
            InsertWeather(CityWeather("Владивосток", 31960), ref dal);
        }

        public static IEnumerable<Weather> GetWeathers()
        {
            var dal = new DataAccessBL();
            var weathers = new List<Weather> { dal.GetWeatherByCity(36870), 
                dal.GetWeatherByCity(50207), 
                dal.GetWeatherByCity(27612), 
                dal.GetWeatherByCity(38457), 
                dal.GetWeatherByCity(33345), 
                dal.GetWeatherByCity(26850), 
                dal.GetWeatherByCity(29574), 
                dal.GetWeatherByCity(29634), 
                dal.GetWeatherByCity(31960)  };
            return weathers;
        }

        private static void InsertWeather(WeatherItem item, ref DataAccessBL dal)
        {
            if (item != null)
            {
                var weather = new Weather()
                {
                    CityId = item.CityId,
                    Temp = item.TempMin,
                    Description = item.CloudinessName,
                    Details = item.CityName,
                    GetDate = DateTime.Now
                };
                dal.InsertWeather(weather);
            }
        }

        private static WeatherItem CityWeather(string cityName, int cityId)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(string.Format(URI, cityId));
            var tempNode = xmlDoc.SelectNodes("//FORECAST//HEAT");
            var fenNode = xmlDoc.SelectNodes("//FORECAST//PHENOMENA");
            if (tempNode != null && fenNode != null)
            {
                if (tempNode.Item(2) != null && fenNode.Item(2) != null)
                {
                    return new WeatherItem(cityId,
                        cityName,
                        int.Parse(tempNode.Item(2).Attributes["min"].Value),
                        int.Parse(tempNode.Item(2).Attributes["max"].Value),
                        int.Parse(fenNode.Item(2).Attributes["cloudiness"].Value));
                }
            }
            return null;
        }

        private class WeatherItem
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public int TempMin { get; set; }
            public int TempMax { get; set; }
            public int Cloudiness { get; set; }
            public string CloudinessName
            {
                get
                {
                    switch (this.Cloudiness)
                    {
                        case 1:
                            return "малооблачно";
                            break;
                        case 2:
                            return "облачно";
                            break;
                        case 3:
                            return "пасмурно";
                            break;
                        default:
                            return "ясно";
                            break;
                    }
                }
            }

            public WeatherItem(int cityId,
                string cityName,
                int tempMin,
                int tempMax,
                int cloudiness)
            {
                this.CityId = cityId;
                this.CityName = cityName;
                this.TempMin = tempMin;
                this.TempMax = tempMax;
                this.Cloudiness = cloudiness;
            }
        }
    }
}

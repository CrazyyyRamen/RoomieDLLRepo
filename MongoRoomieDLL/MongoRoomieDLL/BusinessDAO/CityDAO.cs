using MongoDB.Bson;
using MongoDB.Driver;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessDAO
{
    public class CityDAO : DAO<City>
    {
        private string clusterName = "City";

        public CityDAO()
        {
            collection = databaseMongo.GetCollection<City>(clusterName);
        }

        #region insert / update / delete
        public void CreateOneCity(string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            Province _province = new Province { province_code = provinceCode, province_name = provinceName };
            Country _country = new Country { country_code = countryCode, country_name = countryName };

            City city = new City { city_name = cityName, province = _province, country = _country };

            collection.InsertOne(city);
        }

        public long UpdateCityById(string id, string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            var result = collection.UpdateOne(Builders<City>.Filter.Eq("_id", ObjectId.Parse(id)), Builders<City>.Update.Set("city_name", cityName)
                                            .Set("province.province_name", provinceName)
                                            .Set("province.province_code", provinceCode)
                                            .Set("country.country_name", countryName)
                                            .Set("country.country_code", countryCode));

            return result.ModifiedCount;
        }

        public long DeleteCityById(string id)
        {
            var result = collection.DeleteOne(Builders<City>.Filter.Eq("_id", ObjectId.Parse(id)));
            return result.DeletedCount;
        }

        #endregion

        #region select
        public List<City> GetAllCityList()
        {
            var cities = collection.AsQueryable<City>().ToList();

            return cities;
        }

        public List<City> GetCityByKeyword(string keyword)
        {
            var cities = collection.AsQueryable<City>().Where(c => c.city_name.ToLower().Contains(keyword.ToLower())).ToList();

            return cities;
        }
        #endregion
    }
}

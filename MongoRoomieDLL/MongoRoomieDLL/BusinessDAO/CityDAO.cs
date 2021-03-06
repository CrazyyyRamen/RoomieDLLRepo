﻿using MongoDB.Bson;
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
        public async Task CreateOneCity(string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            Province _province = new Province { province_code = provinceCode, province_name = provinceName };
            Country _country = new Country { country_code = countryCode, country_name = countryName };

            City city = new City { city_name = cityName, province = _province, country = _country };

            await collection.InsertOneAsync(city);
        }

        public async Task<long> UpdateCityById(string id, string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            var result = await collection.UpdateOneAsync(Builders<City>.Filter.Eq("_id", ObjectId.Parse(id)), Builders<City>.Update.Set("city_name", cityName)
                                            .Set("province.province_name", provinceName)
                                            .Set("province.province_code", provinceCode)
                                            .Set("country.country_name", countryName)
                                            .Set("country.country_code", countryCode));

            return result.ModifiedCount;
        }

        public async Task<long> DeleteCityById(string id)
        {
            var result = await collection.DeleteOneAsync(Builders<City>.Filter.Eq("_id", ObjectId.Parse(id)));
            return result.DeletedCount;
        }

        #endregion

        #region select
        public async Task<List<City>> GetAllCityList()
        {
            var cities = await collection.AsQueryable<City>().ToListAsync();

            return cities;
        }

        public List<City> GetCityByKeyword(string keyword)
        {
            var cities = collection.AsQueryable<City>().Where(c => c.city_name.ToLower().Contains(keyword.ToLower())).ToList();

            return cities;
        }

        public List<City> GetCityByCityNameAndProvinceAndCountry(string cityName, string province, string country)
        {
            var cities = collection.AsQueryable<City>().Where(c => c.city_name == cityName
                                                              && (c.province.province_code == province || c.province.province_name == province)
                                                              && (c.country.country_code == country || c.country.country_name == country)).ToList();

            return cities;
        }
        #endregion
    }
}

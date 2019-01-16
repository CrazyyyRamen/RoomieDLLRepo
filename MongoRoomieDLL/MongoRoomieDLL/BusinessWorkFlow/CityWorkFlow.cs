using MongoRoomieDLL.BusinessDAO;
using MongoRoomieDLL.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRoomieDLL.BusinessWorkFlow
{
    public static class CityWorkFlow
    {
        #region insert / update / delete
        public static async Task CreateOneCity(string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            CityDAO cityDAO = new CityDAO();
            await cityDAO.CreateOneCity(cityName, provinceName, provinceCode, countryName, countryCode);
        }

        public static async Task<long> UpdateCityById(string id, string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            CityDAO cityDAO = new CityDAO();
            return await cityDAO.UpdateCityById(id, cityName, provinceName, provinceCode, countryName, countryCode);
        }

        public static async Task<long> DeleteCityById(string id)
        {
            CityDAO cityDAO = new CityDAO();
            return await cityDAO.DeleteCityById(id);
        }
        #endregion

        #region select
        public static async Task<List<City>> GetAllCityList()
        {
            CityDAO cityDAO = new CityDAO();
            return await cityDAO.GetAllCityList();
        }

        public static List<City> GetCityByKeyword(string keyword)
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.GetCityByKeyword(keyword);
        }

        public static List<City> GetCityByCityNameAndProvinceAndCountry(string cityName, string province, string country)
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.GetCityByCityNameAndProvinceAndCountry(cityName, province, country);
        }
        #endregion
    }
}

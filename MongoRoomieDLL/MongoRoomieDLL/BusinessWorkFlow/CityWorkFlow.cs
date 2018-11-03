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
        public static void CreateOneCity(string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            CityDAO cityDAO = new CityDAO();
            cityDAO.CreateOneCity(cityName, provinceName, provinceCode, countryName, countryCode);
        }

        public static long UpdateCityById(string id, string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.UpdateCityById(id, cityName, provinceName, provinceCode, countryName, countryCode);
        }

        public static long DeleteCityById(string id)
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.DeleteCityById(id);
        }
        #endregion

        #region select
        public static List<City> GetAllCityList()
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.GetAllCityList();
        }

        public static List<City> GetCityByKeyword(string keyword)
        {
            CityDAO cityDAO = new CityDAO();
            return cityDAO.GetCityByKeyword(keyword);
        }
        #endregion
    }
}

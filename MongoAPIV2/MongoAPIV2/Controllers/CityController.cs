using MongoRoomieDLL.BusinessEntity;
using MongoRoomieDLL.BusinessWorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MongoAPIV2.Controllers
{
    [RoutePrefix("api/city")]
    public class CityController : ApiController
    {
        /// <summary>
        /// Get information of city (include province and country) e.g: Toronto,ON,Canada
        /// </summary>
        /// <returns>a list of cities in json format</returns>
        [Route("getcity")]
        [HttpGet]
        public List<City> GetCity()
        {
            return CityWorkFlow.GetAllCityList();
        }

        /// <summary>
        /// Get cities that names contain keywords e.g: keyword: Hami => Hamilton
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>a list of city with names contains keyword</returns>
        [Route("citybykeyword")]
        [HttpGet]
        public List<City> SearchCityByKeyWord(string keyword)
        {
            return CityWorkFlow.GetCityByKeyword(keyword);
        }

        /// <summary>
        /// Create a new city 
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="provinceName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="countryName"></param>
        /// <param name="countryCode"></param>
        /// <returns>http response message with city name added</returns>
        [Route("addonecity")]
        [HttpPost]
        public HttpResponseMessage AddCity(string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            try 
            {
                CityWorkFlow.CreateOneCity(cityName, provinceName, provinceCode, countryName, countryCode);

                var message = Request.CreateResponse(HttpStatusCode.Created, cityName);
                message.Headers.Location = new Uri(Request.RequestUri + cityName);

                return message;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
        }

        /// <summary>
        /// Update city information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cityName"></param>
        /// <param name="provinceName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="countryName"></param>
        /// <param name="countryCode"></param>
        /// <returns>http response message with number of cities updated and city id</returns>
        [Route("updateonecity")]
        [HttpPut]
        public HttpResponseMessage Put(string id, string cityName, string provinceName, string provinceCode, string countryName, string countryCode)
        {
            try
            {
                long result = CityWorkFlow.UpdateCityById(id, cityName, provinceName, provinceCode, countryName, countryCode);

                var message = Request.CreateResponse(HttpStatusCode.Created, result);
                message.Headers.Location = new Uri(Request.RequestUri + " city id: " + id.ToString());

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Delete a city by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>http response message with city id of which city was deleted</returns>
        [Route("deleteonecity")]
        [HttpDelete]
        public HttpResponseMessage DeleteCityById(string id)
        {
            try
            {
                long result = CityWorkFlow.DeleteCityById(id);

                var message = Request.CreateResponse(HttpStatusCode.Created, result);
                message.Headers.Location = new Uri(Request.RequestUri + " city id: " + id.ToString());

                return message;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}

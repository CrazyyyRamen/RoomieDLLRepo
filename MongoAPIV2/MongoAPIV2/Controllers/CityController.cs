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
        [Route("getcity")]
        [HttpGet]
        public List<City> GetCity()
        {
            return CityWorkFlow.GetAllCityList();
        }

        [Route("citybykeyword")]
        [HttpGet]
        public List<City> SearchCityByKeyWord(string keyword)
        {
            return CityWorkFlow.GetCityByKeyword(keyword);
        }

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

using MongoRoomieDLL.BusinessEntity;
using MongoRoomieDLL.BusinessWorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MongoAPIV2.Controllers
{
    [RoutePrefix("api/member")]
    public class MemberController : ApiController
    {
        /// <summary>
        /// Get member account information by id
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>one member account infomation</returns>
        [Route("getonemember")]
        [HttpGet]
        public List<MemberAccountInfo> SearchMemberById(string itxt)
        {
            return MemberAccountInfoWorkFlow.GetMemberById(itxt);
        }

        /// <summary>
        /// Search all the members who may have or may not have places in a specified location
        /// </summary>
        /// <param name="itxt"></param>
        /// <param name="offerPlace"></param>
        /// <param name="neighbourhood"></param>
        /// <param name="cityName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="countryName"></param>
        /// <returns>a list of members who currently have or don't ahave place in a specified location</returns>
        [Route("memberexceptself")]
        [HttpGet]
        public List<MemberAccountInfo> SearchActiveMembersExcludeId(string itxt, string offerPlace, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            return MemberAccountInfoWorkFlow.GetActiveMemberAccountExcludeId(itxt, offerPlace, neighbourhood, cityName, provinceCode, countryName);
        }

        /// <summary>
        /// Get a specified number of members who are in a specified location
        /// </summary>
        /// <param name="count"></param>
        /// <param name="neighbourhood"></param>
        /// <param name="cityName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="countryName"></param>
        /// <returns>number of members who are in a specified location</returns>
        [Route("memberbylocation")]
        [HttpGet]
        public List<MemberAccountInfo> SearchMemberByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            return MemberAccountInfoWorkFlow.GetActiveMemberAccountByLocation(count, neighbourhood, cityName, provinceCode, countryName);
        }

        /// <summary>
        /// check user login information
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>member information if user name and password match</returns>
        [Route("memberlogin")]
        [HttpPost]
        public List<MemberAccountInfo> SearchMemberByLoginInfo(MemberAccountInfo mai)
        {
            return MemberAccountInfoWorkFlow.GetMemberAccountLoginInfo(mai.user_name, UIHelper.EncryptDataMD5(mai._password));
        }

        /// <summary>
        /// Check if email already exist in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if email exist and vise versa</returns>
        [Route("checkemail")]
        [HttpGet]
        public bool SearchDuplicateEmail(string email)
        {
            return MemberAccountInfoWorkFlow.CheckEmail(email);
        }

        /// <summary>
        /// Check if user name is unique
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>true if user name is unique and vise versa</returns>
        [Route("checkusername")]
        [HttpGet]
        public bool SearchUsedUserName(string userName)
        {
            return MemberAccountInfoWorkFlow.CheckUserName(userName);
        }

        /// <summary>
        /// User signup process
        /// </summary>
        /// <param name="mai"></param>
        /// <returns>responsed message with newly created member's email</returns>
        [Route("addonemember")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddOneMember([FromBody]MemberAccountInfo mai)
        {
            try
            {
                mai._password = UIHelper.EncryptDataMD5(mai._password);
                await MemberAccountInfoWorkFlow.CreateMemberAccount(mai);

                var message = Request.CreateResponse(HttpStatusCode.Created, mai);
                message.Headers.Location = new Uri(Request.RequestUri + " added account: " + mai._email);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        /// <summary>
        /// Update member account information
        /// </summary>
        /// <param name="mai"></param>
        /// <returns>response message of updated member's email</returns>
        [Route("updateonememberaccount")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateMemberAccount([FromBody]MemberAccountInfo mai)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.UpdateMemberAccount(mai);

                var message = Request.CreateResponse(HttpStatusCode.Created, " modified: " + result.ToString());
                message.Headers.Location = new Uri(Request.RequestUri + " account email: " + mai._email);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Save user's login date
        /// </summary>
        /// <param name="itxt"></param>
        /// <param name="loginDay"></param>
        /// <returns>response message with id of whose login day</returns>
        [Route("recordlogin")]
        [HttpPut]
        public async Task<HttpResponseMessage> RecordLoginDay(string itxt, string loginDay)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.RecordLoginDay(itxt, loginDay);

                var message = Request.CreateResponse(HttpStatusCode.Created, " modified: " + result.ToString());
                message.Headers.Location = new Uri(Request.RequestUri + " saved login day for : " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Activate member's account
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>response message with id of whose account is activated</returns>
        [Route("activateaccount")]
        [HttpPut]
        public async Task<HttpResponseMessage> ActivateMember(string itxt)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.ActivateAccount(itxt);

                var message = Request.CreateResponse(HttpStatusCode.Created, " modified: " + result.ToString());
                message.Headers.Location = new Uri(Request.RequestUri + " activate for : " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Deactivate user's account
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>response message with id of whose account is deactivated</returns>
        [Route("deactivateaccount")]
        [HttpPut]
        public async Task<HttpResponseMessage> DectivateMember(string itxt)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.DeactivateAccount(itxt);

                var message = Request.CreateResponse(HttpStatusCode.Created, " modified: " + result.ToString());
                message.Headers.Location = new Uri(Request.RequestUri + " deactivate for : " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// User can pend their account by themselves if they no longer need the service
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>response message with id of whose account was pending</returns>
        [Route("pendingaccount")]
        [HttpPut]
        public async Task<HttpResponseMessage> PendingMember(string itxt)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.PendingAccount(itxt);

                var message = Request.CreateResponse(HttpStatusCode.Created, " modified: " + result.ToString());
                message.Headers.Location = new Uri(Request.RequestUri + " pending for : " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        /// <summary>
        /// Delete member account
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>response message with id of whose account is deleted</returns>
        [Route("deleteonemember")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteMember(string itxt)
        {
            try
            {
                long result = await MemberAccountInfoWorkFlow.DeleteMemberAccountById(itxt);

                var message = Request.CreateResponse(HttpStatusCode.Created, " deleted: " + result);
                message.Headers.Location = new Uri(Request.RequestUri + " deleted for: " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}

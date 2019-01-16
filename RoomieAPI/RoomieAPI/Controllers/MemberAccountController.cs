using MongoRoomieDLL.BusinessEntity;
using MongoRoomieDLL.BusinessWorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace RoomieAPI.Controllers
{
    [RoutePrefix("api/member")]
    public class MemberAccountController : ApiController
    {
        /// <summary>
        /// check user login information and generate token for validation
        /// </summary>
        /// <param name="mai"></param>
        /// <returns>user token</returns>
        [Route("login")]
        [HttpPost]
        public IHttpActionResult MemberAuthenticate([FromBody]AccountInfo mai)
        {
            var loginResponse = new HttpResponseMessage { };
            AccountInfo AccountInfo = new AccountInfo();

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            if (mai != null)
            {
                AccountInfo._email = mai._email;
                AccountInfo._password = mai._password;

                List<AccountInfo> lstAccount = new List<AccountInfo>();
                lstAccount = AccountInfoWorkFlow.GetMemberAccountLoginInfo(AccountInfo._email, UIHelperController.EncryptDataMD5(AccountInfo._password));

                if(lstAccount.Count > 0)
                {
                    isUsernamePasswordValid = true;
                }
            }
                
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = CreateToken(AccountInfo._email);
                //return the token
                return Ok<string>(token);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse);
                return response;
            }
        }

        /// <summary>
        /// Get member account information by id
        /// </summary>
        /// <param name="itxt"></param>
        /// <returns>one member account infomation</returns>
        [Route("getonemember")]
        [HttpGet]
        public IHttpActionResult SearchMemberById(string itxt)
        {
            List<AccountInfo> lstAccount = new List<AccountInfo>();
            try
            {
                lstAccount = AccountInfoWorkFlow.GetMemberById(itxt);
                return Ok(lstAccount);
            }
            catch(Exception e)
            {
                var httpResponse = new HttpResponseMessage { };
                IHttpActionResult response;

                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(httpResponse);
                return response;
            }
            
            
        }

        /// <summary>
        /// Search all the members who may have or may not have places in a specified location
        /// </summary>
        /// <param name="itxt"></param>
        /// <param name="role"></param>
        /// <param name="neighbourhood"></param>
        /// <param name="cityName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="countryName"></param>
        /// <returns>a list of members who currently have or don't ahave place in a specified location</returns>
        [Route("memberexceptself")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult SearchActiveMembersExcludeId(string itxt, string role, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<AccountInfo> lstAccount = new List<AccountInfo>();
            try
            {
                lstAccount = AccountInfoWorkFlow.GetActiveMemberAccountExcludeId(itxt, role, neighbourhood, cityName, provinceCode, countryName);
                return Ok(lstAccount);
            }
            catch (Exception e)
            {
                var httpResponse = new HttpResponseMessage { };
                IHttpActionResult response;

                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(httpResponse);
                return response;
            }
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
        public IHttpActionResult SearchMemberByLocation(int count, string neighbourhood, string cityName, string provinceCode, string countryName)
        {
            List<AccountInfo> lstAccount = new List<AccountInfo>();
            try
            {
                lstAccount = AccountInfoWorkFlow.GetActiveMemberAccountByLocation(count, neighbourhood, cityName, provinceCode, countryName);
                return Ok(lstAccount);
            }
            catch (Exception e)
            {
                var httpResponse = new HttpResponseMessage { };
                IHttpActionResult response;

                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(httpResponse);
                return response;
            }
        }

        /// <summary>
        /// Check if email already exist in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if email exist and vise versa</returns>
        [Route("checkemail")]
        [HttpGet]
        public IHttpActionResult SearchDuplicateEmail(string email)
        {
            bool isExisted = false;

            try
            {
                isExisted = AccountInfoWorkFlow.CheckEmail(email);
                return Ok<bool>(isExisted);
            }
            catch(Exception e)
            {
                var httpResponse = new HttpResponseMessage { };
                IHttpActionResult response;

                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(httpResponse);
                return response;
            }
        }

        /// <summary>
        /// User signup process
        /// </summary>
        /// <param name="mai"></param>
        /// <returns>responsed message with newly created member's email</returns>
        [Route("addonemember")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddOneMember([FromBody]AccountInfo mai)
        {
            try
            {
                mai._password = UIHelperController.EncryptDataMD5(mai._password);
                await AccountInfoWorkFlow.CreateMemberAccount(mai);

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
        public async Task<HttpResponseMessage> UpdateMemberAccount([FromBody]AccountInfo mai)
        {
            try
            {
                long result = await AccountInfoWorkFlow.UpdateMemberAccount(mai);

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
                long result = await AccountInfoWorkFlow.RecordLoginDay(itxt, loginDay);

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
                long result = await AccountInfoWorkFlow.ActivateAccount(itxt);

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
                long result = await AccountInfoWorkFlow.DeactivateAccount(itxt);

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
        [Route("archiveaccount")]
        [HttpPut]
        public async Task<HttpResponseMessage> ArchiveMember(string itxt)
        {
            try
            {
                long result = await AccountInfoWorkFlow.ArchiveAccount(itxt);

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
                long result = await AccountInfoWorkFlow.DeleteMemberAccountById(itxt);

                var message = Request.CreateResponse(HttpStatusCode.Created, " deleted: " + result);
                message.Headers.Location = new Uri(Request.RequestUri + " deleted for: " + itxt);

                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        private string CreateToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(1);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            string sec = ConfigurationManager.AppSettings["loginKey"].ToString();
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            var issuer = ConfigurationManager.AppSettings["keyIssuer"].ToString();

            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: issuer, audience: issuer,
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
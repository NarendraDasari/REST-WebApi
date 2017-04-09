using Api.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Api.Web.Controllers
{
    public class MyKeyController : BaseController
    {

        private readonly IToken tokenService = default(IToken);

        public MyKeyController(IToken serviceInstance)
        {
            this.tokenService = serviceInstance;
        }

        /// <summary>
        /// Gets a token for your API calls , where the caller should send this token for subquent calls with out fail.
        /// </summary>
        /// <returns>Api Key/Token as an embeded cookie to the Response Object</returns>
        [HttpGet]
        [Route("api/public/ApiKey")]
        public HttpResponseMessage ApiKey()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string token = Guid.NewGuid().ToString();
            this.tokenService.SaveToken(token);
            var cookie = new CookieHeaderValue("api.token-id", token);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}

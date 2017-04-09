using Api.Services;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Web.Extensions
{
    public class TokenAuthenicationHandler : DelegatingHandler
    {
         public static string TokenId = "api.tpken-id";

         //public TokenAuthenicationHandler(HttpConfiguration configuration)
         //{
         //    InnerHandler = new HttpControllerDispatcher(configuration);
         //}

         protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
         {
             bool isRequestValid = false;
             var cookies = request.Headers.GetCookies("api.token-id").FirstOrDefault();
             if(cookies != null)
             {
                 string token = cookies["api.token-id"].Value;
                 if (!string.IsNullOrEmpty(token))
                 {
                     IToken tokenService = new TokenValidationService();
                     isRequestValid = tokenService.Validate(token);
                     if (isRequestValid)
                     {
                         Task.Run(() => tokenService.RenewToken(token));
                     }
                 }
             }
             if (!isRequestValid)
             {
                 HttpResponseMessage responseResult = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                 responseResult.Headers.Add("WWW-Authenticate", "Token Required or Invalid");
                 return Task.FromResult(responseResult);
             }
             return   base.SendAsync(request, cancellationToken);
         }
    }
}
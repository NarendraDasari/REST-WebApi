using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Web.Extensions
{
    public class CustomMessageResult : IHttpActionResult
    {
        public string Message { get; set; }
        private HttpStatusCode status = HttpStatusCode.InternalServerError;
        public HttpRequestMessage requestMessage;

        public CustomMessageResult()
        { 

        }
        public CustomMessageResult(string message,HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            this.Message = message;
            status = statusCode;
        }

       
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(status);
            response.Content = new StringContent(Message);
            response.RequestMessage = requestMessage;
            return Task.FromResult(response);
        }
    }
}
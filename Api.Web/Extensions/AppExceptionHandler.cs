using System.Web.Http.ExceptionHandling;

namespace Api.Web.Extensions
{
    public class AppExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
           
            context.Result = new CustomMessageResult("Oops! Sorry! Something went wrong. Please try later !");
            
        }

    }
}
using Api.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace Api.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Services.Replace(typeof(IExceptionHandler), new AppExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new AppExceptionLogger());
            // Web API routes

            // Support for More Media Types
            var bson = new BsonMediaTypeFormatter();
            bson.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.contoso"));
            config.Formatters.Add(bson);


            // DelegatingHandler[] handlers = new DelegatingHandler[]{new TokenAuthenicationHandler()};

            // Create a message handler chain with an end-point.
            // var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);

            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //  name: "DefaultPublicApi",
            //  routeTemplate: "apiV2/public/{id}",
            //  defaults: new { controller = "MyKeyController", Id = RouteParameter.Optional }
            //  );

            config.Routes.MapHttpRoute(
                name: "ApiRoot",
                routeTemplate: "api/public/{method}",
                defaults: new { controller = "MyKeyController", method = "ApiKey" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[]
                        {
                            new TokenAuthenicationHandler()
                        })
                // handler: new TokenAuthenicationHandler(GlobalConfiguration.Configuration)
            );



            // config.MessageHandlers.Add(new TokenAuthenicationHandler());


        }
    }
}

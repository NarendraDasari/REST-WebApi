Author : Narendra Babu Dasari
LinkedIn Profile : https://www.linkedin.com/in/narendra-babu-dasari/


Description:

This is a sample solution to demonstrate WEB API 2 capabilities . 
I used the business concept of managing a simple Customer CONTACTS list and performed CRUD operation on the contact list.
I made maximun efforts to keep this as simple as possible and tried to showcase minimum architectural design aspects without over designing the app with more number of layers. I personally believe more Layers/Tiers to an solution adds cost on Servers Performance.

This solution is made purely on top of the Web Api framework. I have leveraged ASP.NET MVC framework for generating the Web Api documentation.


What you can expect:
1) CRUD Operations to Database - Used LiteDB(MongoDB styled)(http://www.litedb.org/) document database.
2) Security - Custom Database driven Token(ApiKey) for authenticating client apps.
3) Extensibility capability of Web Api.
   a.Custom Exception Handler
   b.Custom Exception Logger
   c.Custom HttpResponseMessage return type to handle custom messages to the client.
   d.Mesasge Handler - used for authenication of the Api Key.
   e.Content Negotiation - Media Formatter --  Added BJSON(http://bjson.org/) support for Response type.
 
 4) Integrating of IoC (DI) pattern into WebApi using - Autofac (https://autofac.org/)
    a. Custom Logger Module registration with Autofac.
 5) Logging - Flat file logger - Used Log4Net(https://logging.apache.org/log4net/) as Logging Module.

 Nuget Packages Used:
 1) Microsoft.AspNet.WebApi.HelpPage - Web Api Documention
 2) LiteDB - Portable Document Database.
 3) Autofac.WebApi2 - IoC conatiner for Web Api.
 4) log4net.AutoFac - Logging Module.


 This application is developed using Visual Studio 2013 Web Express edition.
 This application strongly supports IIS Hosting.

 Developer Guide to API:
 The List of API documentation should be available on  http://<<service endpoint>>/Help  endpoint. Alternatively, you can also use any REST API documentation tools like swagger(http://swagger.io/) to view the API documentation.
 I used POSTMAN(https://www.getpostman.com/) app to test my API.


 NOTE :
 To access any Web Api service method the client app needs an ApiKey. 
 Prior to calling any api,You should call endpoint (http://<<HOSTNAME>>/api/public/apikey). This would issue you an string key/ApiKey as an cookie with name "api.token-id" embedded into the response stream.
 This token should be good for 10 min. If the client doesn't make further requests using this token within 10 min, they might need a fresh api key gain.
 You should pass on the cookie "api.token-id" as part of your Request header in making other calls to the api. The actual Api would throw an 401-UnAuthorized Response along with WWW-Authenticate Header set to the response if the cookie with name "api.token-id" is not found or expires.

 Dependencies:
 LiteDB - Databse will be created at "C:\Api.Database\ folder on your Hard drive when you try accessing the contact list API. This is not made configurable. ( This May be improvemed). You can change the path and Database name when you try running the solution. 
 Log Files - Log4Net Log file will is configured to be placed at "C:\Logs". This setting can be changed in the log4net.config.


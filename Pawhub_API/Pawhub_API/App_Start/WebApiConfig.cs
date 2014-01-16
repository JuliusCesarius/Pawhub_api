using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Pawhub_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9a-f]{24}$" }
            );

            config.Routes.MapHttpRoute(
                name: "PagedGet",
                routeTemplate: "api/{controller}/page/{pageNumber}",
                defaults: new { action = "Get", pageNumber = 0 }
            );

            #region Routing de Reports

            //config.Routes.MapHttpRoute(
            //    name: "ReportsType",
            //    routeTemplate: "lnf/reports/{type}",
            //    defaults: new { controller = "Reports", action = "ReportsType", pageNumber = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "ReportsTypePaged",
            //    routeTemplate: "lnf/reports/{type}/page/{pageNumber}",
            //    defaults: new { controller = "Reports", action = "ReportsType", pageNumber = RouteParameter.Optional }
            //);
            
            #endregion

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            BsonClassMap.RegisterClassMap<Abuse>();
            BsonClassMap.RegisterClassMap<Found>();
            BsonClassMap.RegisterClassMap<Lost>();
            BsonClassMap.RegisterClassMap<Resque>();
        }
    }
}

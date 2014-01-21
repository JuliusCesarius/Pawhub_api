using blastic.pawhub.models.LostAndFound;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace blastic.pawhub.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "lnf/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetPaged",
                routeTemplate: "lnf/{controller}/page/{pageNumber}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { pageNumber = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
            
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;


            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            BsonClassMap.RegisterClassMap<Abuse>();
            BsonClassMap.RegisterClassMap<Found>();
            BsonClassMap.RegisterClassMap<Lost>();
            BsonClassMap.RegisterClassMap<Resque>();
        }
    }
}

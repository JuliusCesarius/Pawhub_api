using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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

            #region Routing de Reports

            config.Routes.MapHttpRoute(
                name: "Reports",
                routeTemplate: "api/{controller}",
                defaults: new { action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "ReportsPaged",
                routeTemplate: "api/{controller}/page/{pageSize}",
                defaults: new { controller = "Reports", action = "Get", pageSize = 0 }
            );

            config.Routes.MapHttpRoute(
                name: "ReportsType",
                routeTemplate: "api/{controller}/{type}",
                defaults: new { controller = "Reports", action = "ReportsType", pageSize = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ReportsTypePaged",
                routeTemplate: "api/{controller}/{type}/page/{pageSize}",
                defaults: new { controller = "Reports", action = "ReportsType", pageSize = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ReportsTypeById",
                routeTemplate: "api/{controller}/{type}/{id}",
                defaults: new { controller = "Reports", action = "ReportsType", id = RouteParameter.Optional }
            );

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
        }
    }
}

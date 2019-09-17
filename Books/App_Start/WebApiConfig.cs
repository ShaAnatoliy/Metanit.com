using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Books.Models;
using Books.Resolver;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Books
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services 
            var container = new UnityContainer();
            container.RegisterType<IBookRepository, BookRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes 
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
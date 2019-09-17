﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Books
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Init()
        {
            HttpContext.Current.Application.Add(Properties.Resources.Db3FilePathName1, String.Empty);
        }

        protected void Application_Start()
        {
            // AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpContext.Current.Application[Properties.Resources.Db3FilePathName1] = SQLiteDB.CreateTables();
        }
    }
}

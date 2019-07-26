using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.Controllers
{
    public class MyController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            var response = requestContext.HttpContext.Response;

            response.Charset = "utf-8";

            response.Write("<h2>Request.Browser: " + requestContext.HttpContext.Request.Browser.Browser + "</h2>");

            response.Write("<h2>Request.ApplicationPath: " + requestContext.HttpContext.Request.ApplicationPath + "</h2>");
            response.Write("<h2>Request.AppRelativeCurrentExecutionFilePath: " + requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath + "</h2>");
            response.Write("<h2>Server.MachineName: " + requestContext.HttpContext.Server.MachineName + "</h2>");
            response.Write("<h2>Request.MachineName: " + requestContext.HttpContext.Request.Path + "</h2>");
        }

    }
}
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI.Infrastructure.Attributes
{
    public class AdminAuthAttribute : AuthorizeAttribute
    {

      
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return UserSession.Name == "admin";
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }
    }
}
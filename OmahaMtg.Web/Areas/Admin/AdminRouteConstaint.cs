using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace OmahaMtg.Web.Areas.Admin
{
    public class AdminRouteConstaint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return httpContext.User.IsInRole("Admin");
            }
    }
}
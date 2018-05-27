using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcAppMain.Filters
{
    public class AdminFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "NotFound",
                    controller = "Error",
                    area = ""
                }));
            }
        }
    }
}
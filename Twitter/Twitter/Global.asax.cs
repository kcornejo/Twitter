using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Twitter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            String url = context.Request.Url.ToString();
            if (context.Session["Usuario"] == null)
            {
                if (!url.Contains("Usuario/Login") && !url.Contains("Usuario/ValidaLogin"))
                {
                    context.Response.Redirect("Usuario/Login");
                }
            }
            else
            {
                if (url.Contains("Usuario/Login"))
                {
                    context.Response.Redirect("default.aspx");
                }
            }
        }
    }
}

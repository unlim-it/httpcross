﻿[assembly: Microsoft.Owin.OwinStartup(typeof(HttpCross.Testing.API.Startup))]
namespace HttpCross.Testing.API
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });


            app.UseWebApi(config);
        }
    }
}
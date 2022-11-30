using GAT_TaskResolutionUtility.DI;
using GAT_TaskResolutionUtility.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GAT_TaskResolutionAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapper.Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            log4net.Config.XmlConfigurator.Configure();

            //IsoDateTimeConverter converter = new IsoDateTimeConverter
            //{
            //    DateTimeStyles = DateTimeStyles.AdjustToUniversal,
            //    DateTimeFormat = "yyyy-MM-dd"
            //};
            //GlobalConfiguration.Configuration.Formatters
            // .JsonFormatter.SerializerSettings.Converters.Add(converter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
            new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                Culture = CultureInfo.GetCultureInfo("en-US")
            };

        }
    }
}

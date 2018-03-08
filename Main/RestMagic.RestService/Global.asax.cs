using System;
using System.Net.Http.Formatting;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;
using RestMagic.Lib.Data;

namespace RestMagic.RestService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configure(WebApiConfig.Register);
             
         
             
        }

        

        private static void ConfigureAppInsights() =>
            TelemetryConfiguration.Active.InstrumentationKey = WebConfigurationManager.AppSettings["AppInsightsInstrumentationKey"];
    }
}

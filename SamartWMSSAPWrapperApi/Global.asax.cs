using SamartWMSSAPWrapperApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SamartWMSSAPWrapperApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Logger = new LoggerConfiguration()
                  .WriteTo.File(@"C:/inetpub/wwwroot/SamartWMSApiWrapper/Logs/log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt")
                  .CreateLogger();


            Log.Logger.Information("SAP Web API Service Logs");

            try
            {
                GlobalVar.Host = ConfigurationManager.AppSettings["serviceurl"].ToString();
                GlobalVar.CompanyDB = ConfigurationManager.AppSettings["database"].ToString();
                GlobalVar.Username = ConfigurationManager.AppSettings["username"].ToString();
                string encrpPwd = ConfigurationManager.AppSettings["password"].ToString();

                Encryption.Encryption encrypt = new Encryption.Encryption();
                GlobalVar.Password = encrypt.Decrypt(GlobalVar.Username, encrpPwd);
            }
            catch (Exception e)
            {
                Serilog.Log.Logger.Error("Error reading config file. " + e.ToString());
            }
        }
    }
}

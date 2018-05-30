using AutoMapper;
using PSIQ.Models;
using PSIQ.WebUI.ViewModels;
using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PSIQ.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TeraViewModel, Usuario>();
                cfg.CreateMap<PaciViewModel, Usuario>();
            });
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializer = new JavaScriptSerializer();
                    var serializeModel = serializer.Deserialize<Usuario>(authTicket.UserData);
                    HttpContext.Current.User = serializeModel;
                }
            }
            catch (CryptographicException)
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}

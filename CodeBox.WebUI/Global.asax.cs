using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete;
using CodeBox.WebUI.Infrastructure;
using Ninject;

namespace CodeBox.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "PublicRouteForSnippet",
                "Public/{snippetId}",
                new {controller = "Home", action = "PublicSnippet"});


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );

            routes.MapRoute(
                "AccountLogin",
                "Account/{action}",
                new {controller = "Account", action = "EditAccountDetails"}
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Register the new controllerfactory as the default one
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            var ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ISnippetRepository>().To<EFSnippetRepository>();
            ninjectKernel.Bind<ILanguageRepository>().To<EFLanguageRepository>();
            ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
            ninjectKernel.Bind<IRoleRepository>().To<EFRoleRepository>();


            ninjectKernel.Inject(Roles.Provider);
            ninjectKernel.Inject(Membership.Provider);
        }

        //protected void Application_AuthenticateRequest()
        //{
        //    var user = HttpContext.Current.User;
        //    if (user != null)
        //    {
        //        if (Membership.GetUser(user.Identity.Name, true) == null)
        //        {
        //            FormsAuthentication.SignOut();
        //            FormsAuthentication.RedirectToLoginPage();
        //        }
        //    }
        //}
    }
}
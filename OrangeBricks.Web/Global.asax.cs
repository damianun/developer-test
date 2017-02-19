using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrangeBricks.Web.Cqrs;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.VMBuilder;
using OrangeBricks.Web.VMBuilder.Interfaces;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace OrangeBricks.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            //Allows "Per Web Request" lifetime scope - available from SimpleInjector.Integration.Web.dll
            var weblifestyle = new WebRequestLifestyle();

            // DB Context
            container.Register<IOrangeBricksContext, ApplicationDbContext>(weblifestyle);
            
            // Auth
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.Register(() => HttpContext.Current.GetOwinContext().Authentication);
            
            // MVC
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            //Commands
            container.Register<ICommandSender, CommandSender>(weblifestyle);
            container.Register<ICommandHandlerResolver, SimpleInjectorCommandHandlerResolver>(weblifestyle);
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<>), weblifestyle, Assembly.GetExecutingAssembly());

            //ViewModelBuilders
            container.Register<IViewModelFactory, ViewModelFactory>(weblifestyle);
            container.Register<IViewModelBuilderResolver, SimpleInjectorViewModelBuilderResolver>(weblifestyle);
            container.RegisterManyForOpenGeneric(typeof(IViewModelBuilder<,>), weblifestyle, Assembly.GetExecutingAssembly());

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }
    }
}

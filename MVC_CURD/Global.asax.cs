using Data;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_CURD
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected void Application_Start()
        {

        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new DataNinjectModule());
            RegisterServices(kernel);
            IocContainer.Instance.Initialize(kernel);
            return kernel;
        }

        private void RegisterServices(StandardKernel kernel)
        {
            kernel.Bind<IUnitofWork>().To<EmployeeContext>().InRequestScope();
            kernel.Bind<IEmployee>().To<EmployeeRepository>().InRequestScope();
            //kernel.Bind<IEmployeeContext>().To<Employee>();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //var context = IocContainer.Instance.Get<IUnitofWork>();
            //var context1 = IocContainer.Instance.Get<IEmployee>();
            //context.CommiteChanges();
        }
    }
}

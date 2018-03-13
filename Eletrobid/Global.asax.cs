using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ClientDependency.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Eletrobid.Models;
using static Eletrobid.Plumbing.WindsonControllerFactory;

namespace Eletrobid
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(LoggedUser), new LoggedUserModelBinder());

            CreateBundles();
            WindsorContainer();

            //RETIRA AS OUTRAS VIEW ENGINE DO SISTEMA - MELHORIA DE PERFORMANCE
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private static void CreateBundles()
        {
            BundleManager.CreateCssBundle("Bootstrap",
                new CssFile("~/Content/bootstrap.css"),
                new CssFile("~/Content/bootstrap-theme.css"),
                new CssFile("~/Content/css/font-awesome.css"),
                new CssFile("~/Content/index-style.css"),
                new CssFile("~/Content/site.css"),
                new CssFile("~/Content/estilo.css"));

            BundleManager.CreateJsBundle("BaseJavascript",
                new JavascriptFile("~/Scripts/jquery.validate.js"),
                new JavascriptFile("~/Scripts/jquery.validate.unobtrusive.js"),
                new JavascriptFile("~/Scripts/bootstrap.js"),
                new JavascriptFile("~/Scripts/jquery.matchHeight.js"),
                new JavascriptFile("~/Scripts/raty/jquery.raty.js"),
                new JavascriptFile("~/Scripts/jquery.inputmask/jquery.inputmask-2.4.30.js"),
                new JavascriptFile("~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions-2.4.30.js"),
                new JavascriptFile("~/Scripts/jquery.blockUI.js"),
                new JavascriptFile("~/scripts/bootbox.js"),
                new JavascriptFile("~/scripts/jquery.google-analytics.js")
                );
        }

        private static IWindsorContainer _container;

        private static void WindsorContainer()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfoTrack.Seo.Web.Interfaces;
using InfoTrack.Seo.Web.Helpers;
using log4net;
using Autofac;
using InfoTrack.Seo.Web.Controllers;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using InfoTrack.Seo.Web.Models;
using InfoTrack.Seo.Web.Clients;

namespace InfoTrack.Seo.Web.Ioc
{
    /// <summary>
    /// Register dependancies
    /// </summary>
    public class AppRegistration
    {
         public IContainer Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(logger => LogManager.GetLogger(MethodBase.GetCurrentMethod().ReflectedType)).As<ILog>();
            builder.RegisterType<GoogleSearchPositionHelper>().As<IGoogleSearchPositionHelper>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleClient>().As<IGoogleClient>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

    }
}
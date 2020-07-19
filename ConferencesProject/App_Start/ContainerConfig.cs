using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ConferencesProject.Data;
using ConferencesProject.Models;

namespace ConferencesProject.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Repository>()
                .As<IRepository>().InstancePerRequest();
            builder.RegisterType<ConferenceContext>()
                .InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>()
                .InstancePerRequest();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}

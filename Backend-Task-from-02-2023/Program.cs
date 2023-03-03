using Autofac;
using Backend.Interfaces;
using Backend.Services;
using Newtonsoft.Json;
using System;
using System.Web;

namespace Backend
{
    public class Program
    {
        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationService>();
            //builder.RegisterType<MovieStarService>().As<IMovieService>();
            //builder.RegisterType<SalarуService>().As<ISalaryService>();

            return builder.Build();
        }

        static void Main()
        {
            var container = ConfigureContainer();

            var application = container.Resolve<ApplicationService>();

            application.Run();
        }
    }
}

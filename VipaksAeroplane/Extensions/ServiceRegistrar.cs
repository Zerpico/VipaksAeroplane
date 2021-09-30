using Autofac;
using VipaksAeroplane.Interfaces;
using VipaksAeroplane.Services;

namespace VipaksAeroplane.Extensions
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<FileRepository>().AsImplementedInterfaces().SingleInstance().AsSelf();
            builder.RegisterType<GeneratorFlight>().As<IGeneratorFlight>();
            builder.RegisterType<DispatcherTime>().As<IDispatcherTime>();

            return builder;
        }
    }
}

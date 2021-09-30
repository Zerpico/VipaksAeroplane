using System.Windows;
using Autofac;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Autofac.SmartNavigation;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using VipaksAeroplane.Extensions;
using Autofac.SmartNavigation.Base;

namespace VipaksAeroplane
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ILifetimeScope Scope { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // создаем Autofac
            var builder = new ContainerBuilder()
                .UseAutofind(typeof(VipaksAeroplane.App).Assembly)   // для использования автоматической регистрации представлений и моделей представлений
                .RegisterServices();        // регистрация сервисов в Autofac

            // создание и конфигурация стандартного контейнера          
            ConfigureServices(builder);

            // формируем скоп Autofac
            Scope = builder.Build().BeginLifetimeScope();

            // получаем сервис навигации из скопа Autofac
            var navigation = Scope.Resolve<INavigationService>();
            navigation.Navigate("ShellWindow");
        }

        private void ConfigureServices(ContainerBuilder builder)
        {
            // для примера сервис навигации регистрируется в стандартном контейнере
            builder.RegisterType<AppNavigationService>().As<INavigationService>();
        }
    }
}

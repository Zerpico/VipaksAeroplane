using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Documents;
using Autofac.SmartNavigation.Base;

namespace Autofac.SmartNavigation.Extensions
{
    internal static partial class AutofacExtensions
    {
        internal static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, IEnumerable<Type> viewModelTypes)
        {
            foreach (var type in viewModelTypes)
            {
                RegisterAsm(type);
            }

            void RegisterAsm(Type type)
            {
                builder.RegisterType(type)
                    .Keyed<INotifyPropertyChanged>(type.Name.ToLower())
                    .Named<INotifyPropertyChanged>(type.Name.ToLower().Replace("viewmodel", ""))
                    .AsSelf();

               /* builder.RegisterAssemblyTypes(assembly)
                    .PublicOnly()
                    .Keyed<INotifyPropertyChanged>(t => t.Name.ToLower())
                    .Named<INotifyPropertyChanged>(t => t.Name.ToLower().Replace("viewmodel", ""))
                    .AsSelf();*/
            }

            return builder;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac.SmartNavigation.Base;

namespace Autofac.SmartNavigation.Extensions
{
    internal static partial class AutofacExtensions
    {
        internal static ContainerBuilder RegisterViews(this ContainerBuilder builder, IEnumerable<Type> viewTypes)
        {
            foreach (var type in viewTypes)
            {
                RegisterAsm(type);
            }

            void RegisterAsm(Type type)
            {
                if (type.IsSubclassOf(typeof(NavigationalWindow)))
                {
                    builder.RegisterType(type)
                        .Keyed<NavigationalWindow>(type.Name.ToLower())
                        .AsImplementedInterfaces()
                        .AsSelf();
                }
                if (type.IsSubclassOf(typeof(Window)))
                {
                    builder.RegisterType(type)
                    .Keyed<Window>(type.Name.ToLower())
                    .AsImplementedInterfaces()
                    .AsSelf();
                }
                if (type.IsSubclassOf(typeof(Page)))
                {
                    builder.RegisterType(type)
                    .Keyed<Page>(type.Name.ToLower())
                    .AsImplementedInterfaces()
                    .AsSelf();
                }               
            }
            
            return builder;
        }
    }
}

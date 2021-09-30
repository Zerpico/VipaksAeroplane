using Autofac.SmartNavigation.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Autofac.SmartNavigation.Extensions
{
    public static class AutofacExtension
    {
        /// <summary>
        /// Использовать автопоиск и регистрацию представлений и моделей представлений
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder UseAutofind(this ContainerBuilder builder, Assembly assembly)
        {
            var vmtypes = assembly.GetTypes().Where(d => d.IsSubclassOf(typeof(BaseVM)));
            var viewtypes = assembly.GetTypes().Where(d => d.IsSubclassOf(typeof(Window)) || d.IsSubclassOf(typeof(System.Windows.Controls.Page)));

            builder.RegisterViewModels(vmtypes).RegisterViews(viewtypes);
            return builder;
        }
    }
}

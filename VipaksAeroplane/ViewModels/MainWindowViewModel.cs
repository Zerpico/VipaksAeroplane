using Autofac.SmartNavigation.Base;
using Autofac.SmartNavigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace VipaksAeroplane.ViewModels
{
    public class MainWindowViewModel : BaseVM
    {
        public string Title { get; } = "Информационное табло аэропорта";

        private INavigationService _navigation;

        public MainWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;
        }
    }
}

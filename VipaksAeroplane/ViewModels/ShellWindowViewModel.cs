using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Autofac.SmartNavigation.Base;
using Autofac.SmartNavigation.Interfaces;
using VipaksAeroplane.Commands;
using VipaksAeroplane.Services;


namespace VipaksAeroplane.ViewModels
{
    public class ShellWindowViewModel : BaseVM
    {
        private readonly INavigationService _navigation;

        public string ViewModelName { get; } = nameof(ShellWindowViewModel);
        public ICommand NavigateCommand { get; private set; }

        public ShellWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            CommandsInitialize();
        }

        private void CommandsInitialize()
        {
            NavigateCommand = new LambdaCommand(() =>
            {
                _navigation.Navigate(nameof(Views.Pages.PlanePage), true, true);
                return;
            });
        }
    }
}

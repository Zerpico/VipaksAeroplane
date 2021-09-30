using Autofac.SmartNavigation.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VipaksAeroplane.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : NavigationalWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public override Frame Frame
        {
            get => ShellWindowFrame;
            set => ShellWindowFrame = value;
        }
    }
}

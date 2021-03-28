using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.ViewModels;
using WPF_App_With_Car.Views;

namespace WPF_App_With_Car
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var currentDomain = AppDomain.CurrentDomain;
            var basePath = currentDomain.BaseDirectory.Substring(0,currentDomain.BaseDirectory.Length-11);
            currentDomain.SetData("DataDirectory", basePath);
            ViewModelService.Register<MainWindowViewModel>();
            ViewService.Register<MainWindow>();
            ViewService.Resolve<MainWindow>(ViewModelService.Resolve<MainWindowViewModel>()).Show();
        }
    }
}

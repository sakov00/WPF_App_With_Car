using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models.Contexts;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class RefreshInfoCommand:ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RefreshInfoCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            using (Cars_DB db = new Cars_DB())
            {
                ViewService.Resolve<MainWindow>().DataGridTask2.ItemsSource = db.BrandCar.ToList();
                ViewService.Resolve<MainWindow>().DataGridTask.ItemsSource = db.Cars.ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.ViewModels;
using WPF_App_With_Car.Views;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class DeleteCarCommand:ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteCarCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public async void Execute(object parameter)
        {
            TextBox text = (TextBox)parameter;
            string value = text.Text;
            await Task.Run(() => DeleteCar(value));
            using (Cars_DB db = new Cars_DB())
                ViewService.Resolve<WorkCarWindow>().DataGridTask.ItemsSource = db.Cars.ToList();
        }
        private static void DeleteCar(string parameter)
        {
            try
            {
                using (Cars_DB db = new Cars_DB())
                {
                    db.Cars.Remove(db.Cars.Find(parameter));
                    db.SaveChanges();
                    MessageBox.Show("Автомобиль успешно удалён");

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}

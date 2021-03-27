using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class DeleteBrandCommand:ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteBrandCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            await Task.Run(() => DeleteBrand(value));
            using (Cars_DB db = new Cars_DB())
                ViewService.Resolve<WorkBrandWindow>().DataGridTask.ItemsSource = db.BrandCar.ToList();
        }
        private static void DeleteBrand(string parameter)
        {
            try
            {
                using (Cars_DB db = new Cars_DB())
                {
                    db.BrandCar.Remove(db.BrandCar.Find(parameter));
                    db.SaveChanges();
                    MessageBox.Show("Брэнд успешно удалён");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.ViewModels;
using WPF_App_With_Car.Views;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class CreateBrandCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CreateBrandCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            await Task.Run(() => CreateBrand(parameter));
            using (Cars_DB db = new Cars_DB())
            ViewService.Resolve<WorkBrandWindow>().DataGridTask.ItemsSource = db.BrandCar.ToList();
        }
        private static void CreateBrand(object parameter)
        {
            try
            {
                using (Cars_DB db = new Cars_DB())
                {
                    BrandCar brand = (BrandCar)parameter;
                    brand.Logotype = ViewModelService.Resolve<WorkBrandWindowViewModel>().Logotype;
                    brand.Type_Image = ViewModelService.Resolve<WorkBrandWindowViewModel>().ImageType;
                    db.BrandCar.Add(brand);
                    db.SaveChanges();
                    MessageBox.Show("Брэнд успешно добавлен");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}

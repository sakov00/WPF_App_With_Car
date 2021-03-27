using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.ViewModels;
using WPF_App_With_Car.Views;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class UpdateBrandCommand:ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public UpdateBrandCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            await Task.Run(() => UpdateBrand(parameter));
            using (Cars_DB db = new Cars_DB())
            ViewService.Resolve<WorkBrandWindow>().DataGridTask.ItemsSource = db.BrandCar.ToList();

        }
        private static void UpdateBrand(object parameter)
        {
            try
            {
                using (Cars_DB db = new Cars_DB())
                {
                    BrandCar brand = (BrandCar)parameter;

                    if (brand.Brand_Name!= ViewModelService.Resolve<WorkBrandWindowViewModel>().Brand_Name_For_Update)
                    {
                        db.BrandCar.Add(brand);
                        foreach (var p in db.Cars.ToList())
                        {
                            if (ViewModelService.Resolve<WorkBrandWindowViewModel>().Brand_Name_For_Update == p.Brand_Name)
                            {
                                p.Brand_Name = ViewModelService.Resolve<WorkBrandWindowViewModel>().Brand_Name;
                                db.Entry(p).State = EntityState.Modified;
                            }
                        }
                        db.BrandCar.Remove(db.BrandCar.Find(ViewModelService.Resolve<WorkBrandWindowViewModel>().Brand_Name_For_Update));
                        db.SaveChanges();
                        return;
                    }
                    brand.Brand_Name = ViewModelService.Resolve<WorkBrandWindowViewModel>().Brand_Name;
                    brand.Logotype = ViewModelService.Resolve<WorkBrandWindowViewModel>().Logotype;
                    brand.Type_Image = ViewModelService.Resolve<WorkBrandWindowViewModel>().ImageType;
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Брэнд успешно изменён");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}

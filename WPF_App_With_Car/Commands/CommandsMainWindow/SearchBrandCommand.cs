using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.ViewModels;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class SearchBrandCommand:ICommand
    {
        Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public SearchBrandCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            List<BrandCar> list= await Task.Run(() => SearchBrand(parameter));
            ViewService.Resolve<MainWindow>().DataGridTask2.ItemsSource = list;
        }
        private static List<BrandCar> SearchBrand(object parameter)
        {
            string[] str = (string[])parameter;
            using (Cars_DB db = new Cars_DB())
                try
                {
                    if (str[1] != null && str[1] != "")
                        switch (str[0])
                        {
                            case "Название":
                                var selectedcars2 = db.BrandCar.ToList();
                                List<BrandCar> BrandsGroup2 = new List<BrandCar>();
                                foreach (var c in selectedcars2)
                                {
                                    if (c.Brand_Name != null && c.Brand_Name.Contains(str[1]))
                                    {
                                        BrandsGroup2.Add(c);
                                    }
                                }
                                return BrandsGroup2;
                            case "Описание":
                                var selectedcars3 = db.BrandCar.ToList();
                                List<BrandCar> BrandsGroup3 = new List<BrandCar>();
                                foreach (var c in selectedcars3)
                                {
                                    if (c.Description != null && c.Description.Contains(str[1]))
                                    {
                                        BrandsGroup3.Add(c);
                                    }
                                }
                                return BrandsGroup3;
                        }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            return null;
        }
    }
}

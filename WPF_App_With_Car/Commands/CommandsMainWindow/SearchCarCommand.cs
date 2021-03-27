using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.ViewModels;

namespace WPF_App_With_Car.Commands.CommandsMainWindow
{
    class SearchCarCommand:ICommand
    {
        Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCarCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            List<Cars> list=await Task.Run(() => SearchCar(parameter));
            ViewService.Resolve<MainWindow>().DataGridTask.ItemsSource = list;
        }
        private static List<Cars> SearchCar(object parameter)
        {
            string[] str = (string[])parameter;
            using (Cars_DB db = new Cars_DB())
                try
                {
                    if (str[1] != null && str[1] != "")
                    switch (str[0])
                    {
                        case "Название":
                            var selectedcars1 = db.Cars.ToList();
                            List<Cars> CarsGroup1 = new List<Cars>();
                            foreach (var c in selectedcars1)
                            {
                                if (c.Car_Name != null && c.Car_Name.Contains(str[1]))
                                {
                                    CarsGroup1.Add(c);
                                }
                            }
                            return CarsGroup1;
                        case "Брэнд":
                            var selectedcars2 = db.Cars.ToList();
                            List<Cars> CarsGroup2 = new List<Cars>();
                            foreach (var c in selectedcars2)
                            {
                                if (c.Brand_Name != null && c.Brand_Name.Contains(str[1]))
                                {
                                    CarsGroup2.Add(c);
                                }
                            }
                            return CarsGroup2;
                        case "Тип топлива":
                            var selectedcars3 = db.Cars.ToList();
                            List<Cars> CarsGroup3 = new List<Cars>();
                            foreach (var c in selectedcars3)
                            {
                                if (c.Type_Fuel != null && c.Type_Fuel.Contains(str[1]))
                                {
                                    CarsGroup3.Add(c);
                                }
                            }
                            return CarsGroup3;
                        case "Цена":
                            var selectedcars4 = db.Cars.ToList();
                            List<Cars> CarsGroup4 = new List<Cars>();
                            foreach (var c in selectedcars4)
                            {
                                if (c.Price != null && c.Price.ToString().Contains(str[1]))
                                {
                                    CarsGroup4.Add(c);
                                }
                            }
                            return CarsGroup4;
                        case "Дата появления":
                            var selectedcars5 = db.Cars.ToList();
                            List<Cars> CarsGroup5 = new List<Cars>();
                                if (str[1].Contains("/"))
                                    str[1] = str[1].Replace("/", ".");
                                if (str[1].Contains("-"))
                                    str[1] = str[1].Replace("-", ".");
                                foreach (var c in selectedcars5)
                            {
                                if (c.Date_Appearance != null && c.Date_Appearance.ToString().Contains(str[1]))
                                {
                                    CarsGroup5.Add(c);
                                }
                            }
                            return CarsGroup5;
                        case "Класс":
                            var selectedcars6 = db.Cars.ToList();
                            List<Cars> CarsGroup6 = new List<Cars>();
                            foreach (var c in selectedcars6)
                            {
                                if (c.Class_Car != null && c.Class_Car.Contains(str[1]))
                                {
                                    CarsGroup6.Add(c);
                                }
                            }
                            return CarsGroup6;

                        case "Вместимость":
                            var selectedcars7 = db.Cars.ToList();
                            List<Cars> CarsGroup7 = new List<Cars>();
                            foreach (var c in selectedcars7)
                            {
                                if (c.Capacity_People != null && c.Capacity_People.ToString().Contains(str[1]))
                                {
                                    CarsGroup7.Add(c);
                                }
                            }
                            return CarsGroup7;
                        case "Доступность":
                            var selectedcars8 = db.Cars.ToList();
                            List<Cars> CarsGroup8 = new List<Cars>();
                            foreach (var c in selectedcars8)
                            {
                                if (c.Availability != null && c.Availability.ToString().Contains(str[1]))
                                {
                                    CarsGroup8.Add(c);
                                }
                            }
                            return CarsGroup8;
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

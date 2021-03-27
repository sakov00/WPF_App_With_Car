using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_App_With_Car.Commands.CommandsMainWindow;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Ioc.Interfaces;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;

namespace WPF_App_With_Car.ViewModels
{
    class WorkCarWindowViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkCarWindowViewModel()
        {
            listcars = GetCars();
        }
        public CreateCarCommand Create_Car_Click
        {
            get
            {
                CommandService.Register<CreateCarCommand>();
                return CommandService.Resolve<CreateCarCommand>();
            }
        }
        public UpdateCarCommand Update_Car_Click
        {
            get
            {
                CommandService.Register<UpdateCarCommand>();
                return CommandService.Resolve<UpdateCarCommand>();
            }
        }
        public DeleteCarCommand Delete_Car_Click
        {
            get
            {
                CommandService.Register<DeleteCarCommand>();
                return CommandService.Resolve<DeleteCarCommand>();
            }
        }

        private Cars selectedcar = new Cars();
        public Cars SelectedCar
        {
            get { return selectedcar; }
            set
            {
                selectedcar = value;
                OnPropertyChanged("SelectedCar");
            }
        }
        private List<Cars> listcars;
        private List<Cars> GetCars()
        {
            using (Cars_DB db = new Cars_DB())
                listcars = db.Cars.ToList();
            return listcars;
        }
        public List<Cars> SelectedListCars
        {
            get
            {
                return listcars;
            }
            set
            {
                OnPropertyChanged("SelectedListCars");
            }
        }
        private string carname;
        public string Car_Name_For_Update
        {
            get { return carname; }
            set
            {
                carname = value;
                OnPropertyChanged("Car_Name_For_Update");
            }
        }
        public string Car_Name
        {
            get { return selectedcar.Car_Name; }
            set
            {
                selectedcar.Car_Name = value;
                OnPropertyChanged("Car_Name");
            }
        }
        public string Type_Fuel
        {
            get { return selectedcar.Type_Fuel; }
            set
            {
                selectedcar.Type_Fuel = value;
                OnPropertyChanged("Type_Fuel");
            }
        }
        public float Price
        {
            get { return selectedcar.Price; }
            set
            {
                selectedcar.Price = value;
                OnPropertyChanged("Price");
            }
        }
        public DateTime Date_Appearance
        {
            get { return selectedcar.Date_Appearance; }
            set
            {
                selectedcar.Date_Appearance = value;
                OnPropertyChanged("Date_Appearance");
            }
        }
        public string Class_Car
        {
            get { return selectedcar.Class_Car; }
            set
            {
                selectedcar.Class_Car = value;
                OnPropertyChanged("Class_Car");
            }
        }
        public int Capacity_People
        {
            get { return selectedcar.Capacity_People; }
            set
            {
                selectedcar.Capacity_People = value;
                OnPropertyChanged("Capacity_People");
            }
        }
        public bool Availability
        {
            get { return selectedcar.Availability; }
            set
            {
                selectedcar.Availability = value;
                OnPropertyChanged("Availability");
            }
        }
        public string ChoiseBrandForCar
        {
            get 
            {
                    return selectedcar.Brand_Name; 
            }
            set
            {
                selectedcar.Brand_Name = value;
                OnPropertyChanged("ChoiseBrandForCar");
            }
        }
        public string[] BrandCar
        {
            get { return GetBrandForComboBox(); }
            set
            {
                OnPropertyChanged("BrandCar");
            }
        }
        private static string[] GetBrandForComboBox()
        {
            using (Cars_DB db = new Cars_DB())
            {
                string[] mas = new string[db.BrandCar.ToList().Count()];
                for (int i = 0; i < db.BrandCar.ToList().Count(); i++)
                {
                    mas[i] = db.BrandCar.ToList().ElementAt(i).Brand_Name.ToString();
                }
                return mas;
            }
        }

    }
}

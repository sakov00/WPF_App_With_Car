using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF_App_With_Car.Commands.CommandsMainWindow;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Ioc.Interfaces;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;
using WPF_App_With_Car.Views;

namespace WPF_App_With_Car.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged,IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MainWindowViewModel()
        {
            listbrands = GetBrands();
            listcars = GetCars();
        }
        /*public MainWindowViewModel()
        {
            RefreshData();
        }
        private async void RefreshData()
        {
            await Task.Run(() => this.Refresh(ref AllViews.mainwindow));
        }
        private void Refresh(ref MainWindow Viewmain)
        {
            while (true)
            {
                Thread.Sleep(2000);
                MessageBox.Show("обнова");
                Viewmain.DataGridTask.ItemsSource = GetBrands();
                Viewmain.DataGridTask2.ItemsSource = GetCars();
            }
        }*/

        public ToWorkCarWindowCommand To_Work_Car_Click
        {
            get
            {
                CommandService.Register<ToWorkCarWindowCommand>();
                return CommandService.Resolve<ToWorkCarWindowCommand>();
            }
        }
        public ToWorkBrandWindowCommand To_Work_Brand_Click
        {
            get
            {
                CommandService.Register<ToWorkBrandWindowCommand>();
                return CommandService.Resolve<ToWorkBrandWindowCommand>();
            }
        }
        public SearchCarCommand Search_Car_Click
        {
            get
            {
                CommandService.Register<SearchCarCommand>();
                return CommandService.Resolve<SearchCarCommand>();
            }
        }
        public SearchBrandCommand Search_Brand_Click
        {
            get
            {
                CommandService.Register<SearchBrandCommand>();
                return CommandService.Resolve<SearchBrandCommand>();
            }
        }
        public RefreshInfoCommand Refresh_Info_Click
        {
            get
            {
                CommandService.Register<RefreshInfoCommand>();
                return CommandService.Resolve<RefreshInfoCommand>();
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
        private List<BrandCar> listbrands;
        private List<BrandCar> GetBrands()
        {
            using (Cars_DB db = new Cars_DB())
                listbrands = db.BrandCar.ToList();
            return listbrands;
        }
        public List<BrandCar> SelectedListBrands
        {
            get
            {
                    return listbrands;
            }
            set
            {
                OnPropertyChanged("SelectedListBrands");
            }
        }
        private string[] strcar=new string[2];
        public string[] SearchCar
        {
            get{return strcar; }
            set
            {
                strcar = value;
                OnPropertyChanged("SearchCar");
            }
        }
        public string SearchCarField
        {
            get { return strcar[0]; }
            set
            {
                strcar[0] = value;
                OnPropertyChanged("SearchCarField");
            }
        }
        public string SearchCarValue
        {
            get { return strcar[1]; }
            set
            {
                strcar[1] = value;
                OnPropertyChanged("SearchCarValue");
            }
        }
        private string[] strbrand = new string[2];
        public string[] SearchBrand
        {
            get { return strbrand; }
            set
            {
                strbrand = value;
                OnPropertyChanged("SearchBrand");
            }
        }
        public string SearchBrandField
        {
            get { return strbrand[0]; }
            set
            {
                strbrand[0] = value;
                OnPropertyChanged("SearchBrandField");
            }
        }
        public string SearchBrandValue
        {
            get { return strbrand[1]; }
            set
            {
                strbrand[1] = value;
                OnPropertyChanged("SearchBrandValue");
            }
        }



    }
}

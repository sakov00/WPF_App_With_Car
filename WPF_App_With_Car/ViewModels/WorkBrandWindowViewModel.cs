using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_App_With_Car.Commands.CommandsMainWindow;
using WPF_App_With_Car.Commands.CommandsWorkBrandWindow;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.Ioc.Interfaces;
using WPF_App_With_Car.Models;
using WPF_App_With_Car.Models.Contexts;

namespace WPF_App_With_Car.ViewModels
{
    class WorkBrandWindowViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkBrandWindowViewModel()
        {
            listbrands = GetBrands();
        }
        public CreateBrandCommand Create_Brand_Click
        {
            get
            {
                CommandService.Register<CreateBrandCommand>();
                return CommandService.Resolve<CreateBrandCommand>();
            }
        }
        public UpdateBrandCommand Update_Brand_Click
        {
            get
            {
                CommandService.Register<UpdateBrandCommand>();
                return CommandService.Resolve<UpdateBrandCommand>();
            }
        }
        public DeleteBrandCommand Delete_Brand_Click
        {
            get
            {
                CommandService.Register<DeleteBrandCommand>();
                return CommandService.Resolve<DeleteBrandCommand>();
            }
        }
        public AddBrandLogotypeCommand Add_Brand_Logotype_Click
        {
            get
            {
                CommandService.Register<AddBrandLogotypeCommand>();
                return CommandService.Resolve<AddBrandLogotypeCommand>();
            }
        }

        private BrandCar selectedbrand = new BrandCar();
        public BrandCar SelectedBrand
        {
            get { return selectedbrand; }
            set
            {
                selectedbrand = value;
                OnPropertyChanged("SelectedBrand");
            }
        }
        private List<BrandCar> listbrands;
        public List<BrandCar> GetBrands()
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
        private string brandname;
        public string Brand_Name_For_Update
        {
            get { return brandname; }
            set
            {
                brandname = value;
                OnPropertyChanged("Brand_Name_For_Update");
            }
        }
        public string Brand_Name
        {
            get { return selectedbrand.Brand_Name; }
            set
            {
                selectedbrand.Brand_Name = value;
                OnPropertyChanged("Brand_Name");
            }
        }
        public string Description
        {
            get { return selectedbrand.Description; }
            set
            {
                selectedbrand.Description = value;
                OnPropertyChanged("Description");
            }
        }
        private byte[] logotype;
        public string ImageType { get; set; }
        public byte[] Logotype
        {
            get { return logotype; }
            set
            {
                logotype = value;
                OnPropertyChanged("Logotype");
            }
        }
    }
}

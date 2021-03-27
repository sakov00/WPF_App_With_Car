using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_App_With_Car.Ioc.Containers;
using WPF_App_With_Car.ViewModels;

namespace WPF_App_With_Car.Commands.CommandsWorkBrandWindow
{
    class AddBrandLogotypeCommand:ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddBrandLogotypeCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            this.execute(parameter);
            try
            {
                Image image = (Image)parameter;
                OpenFileDialog OPF = new OpenFileDialog();
                if (OPF.ShowDialog() == DialogResult.OK)
                {
                    image.Source = BitmapFrame.Create(new Uri(OPF.FileName));
                    byte[] imageData = null;
                    FileInfo fInfo = new FileInfo(OPF.FileName);
                    long numBytes = fInfo.Length;
                    FileStream fStream = new FileStream(OPF.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    imageData = br.ReadBytes((int)numBytes);
                    string iImageExtension = (Path.GetExtension(OPF.FileName)).Replace(".", "").ToLower();
                    ViewModelService.Resolve<WorkBrandWindowViewModel>().Logotype = imageData;
                    ViewModelService.Resolve<WorkBrandWindowViewModel>().ImageType = iImageExtension;
                    parameter = image;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Вы загрузили не изображение.\nДобавьте изображение ещё раз");
            }
        }
    }
}

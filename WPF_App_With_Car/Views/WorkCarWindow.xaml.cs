﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_App_With_Car.Ioc.Interfaces;

namespace WPF_App_With_Car.Views
{
    /// <summary>
    /// Логика взаимодействия для WorkCarWindow.xaml
    /// </summary>
    public partial class WorkCarWindow : Window, IView
    {
        public WorkCarWindow()
        {
            InitializeComponent();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for ViewAddress.xaml
    /// </summary>
    public partial class ViewAddress : Page
    {
        public ViewAddress()
        {
            InitializeComponent();
        }

        public ViewAddress(object data):this()
        {
            this.DataContext = data;
        }
    }
}
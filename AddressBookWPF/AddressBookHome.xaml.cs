using Microsoft.Win32;
using System;
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
    /// Interaction logic for AddressBookHome.xaml
    /// </summary>
    public partial class AddressBookHome : Page
    {
        public AddressBookHome()
        {
            InitializeComponent();
        }

        private void loadAddressBook_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == true)
            {
                
            }
        }
    }
}

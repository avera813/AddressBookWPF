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
    /// Interaction logic for AddressBookContents.xaml
    /// </summary>
    public partial class ViewAddressBook : Page
    {
        public ViewAddressBook()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enables/disables View/Edit address buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox entryList = sender as ListBox;
            if (entryList != null && entryList.SelectedIndex > -1)
            {
                viewAddress.IsEnabled = true;
                editAddress.IsEnabled = true;
            }
            else
            {
                viewAddress.IsEnabled = false;
                editAddress.IsEnabled = false;
            }
        }

        private void editAddress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void viewAddress_Click(object sender, RoutedEventArgs e)
        {
            ViewAddress viewAddressPage = new ViewAddress(this.entryListBox.SelectedItem);
            this.NavigationService.Navigate(viewAddressPage);
        }
    }
}

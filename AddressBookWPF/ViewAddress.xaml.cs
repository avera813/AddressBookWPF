using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

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

        public ViewAddress(object data) : this()
        {
            this.DataContext = data;
            addressInfo.ItemsSource = (data as Person).Addresses;
        }

        private void returnToAddressBook_Click(object sender, RoutedEventArgs e)
        {
            ViewAddressBook viewAddressBookPage = new ViewAddressBook();
            this.NavigationService.Navigate(viewAddressBookPage);
        }
    }
}

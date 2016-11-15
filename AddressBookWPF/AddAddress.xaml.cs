using System;
using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Page
    {
        private AddressBook addressBook;

        public AddAddress()
        {
            InitializeComponent();
            addressBook = new AddressBook();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                addressBook.Add(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                MessageBox.Show("The entry has been added.");
                this.NavigationService.Navigate(new ViewAddressBook());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

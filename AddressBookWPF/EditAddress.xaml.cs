using System;
using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for EditAddress.xaml
    /// </summary>
    public partial class EditAddress : Page
    {
        private AddressBook addressBook;
        private string oldName;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data) : this()
        {
            addressBook = new AddressBook();
            this.DataContext = data;
            //oldName = (data as Address).Name;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                addressBook.Update(oldName, this.DataContext as Address);
                MessageBox.Show("The entry has been updated.");
                this.NavigationService.Navigate(new ViewAddressBook());               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }
    }
}

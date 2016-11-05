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

        private string oldName;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data) : this()
        {
            this.DataContext = data;
            oldName = (data as AddressBookDBDataSet.PersonRow).Name;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressBookData.UpdateEntry(this.DataContext as AddressBookDBDataSet.PersonRow, oldName);
                MessageBox.Show("The entry has been updated.");
                this.NavigationService.Navigate(new ViewAddressBook());               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }
    }
}

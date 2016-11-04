using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for EditAddress.xaml
    /// </summary>
    public partial class EditAddress : Page
    {
        private AddressBookDBDataSet addressBookDbDataSet;
        private AddressBookDBDataSetTableAdapters.PersonTableAdapter addressBookDbDataSetPersonTableAdapter;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data) : this()
        {
            this.DataContext = data;
            addressBookDbDataSet = new AddressBookDBDataSet();
            addressBookDbDataSetPersonTableAdapter = new AddressBookDBDataSetTableAdapters.PersonTableAdapter();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressForm.Validate(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                
                AddressBookDBDataSet.PersonRow personRow = this.DataContext as AddressBookDBDataSet.PersonRow;
                personRow.Name = name.Text;
                personRow.Street = street.Text;
                personRow.City = city.Text;
                personRow.State = state.Text;
                personRow.Zip = zip.Text;
                personRow.Country = country.Text;
                addressBookDbDataSetPersonTableAdapter.Update(personRow);

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

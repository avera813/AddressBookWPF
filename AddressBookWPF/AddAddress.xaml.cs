using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Page
    {
        private AddressBookDBDataSet addressBookDbDataSet;
        private AddressBookDBDataSetTableAdapters.PersonTableAdapter addressBookDbDataSetPersonTableAdapter;

        public AddAddress()
        {
            InitializeComponent();
            addressBookDbDataSet = new AddressBookDBDataSet();
            addressBookDbDataSetPersonTableAdapter = new AddressBookDBDataSetTableAdapters.PersonTableAdapter();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressForm.Validate(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                int nextId = (int)addressBookDbDataSetPersonTableAdapter.GetData().Rows[addressBookDbDataSetPersonTableAdapter.GetData().Count - 1][0] + 1;
                addressBookDbDataSetPersonTableAdapter.Insert(nextId, name.Text, city.Text, country.Text, state.Text, street.Text, zip.Text);

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

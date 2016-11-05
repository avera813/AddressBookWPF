﻿using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for ViewAddressBook.xaml
    /// </summary>
    public partial class ViewAddressBook : Page
    {

        public ViewAddressBook()
        {
            InitializeComponent();
            UpdateEntries();
            addAddress.IsEnabled = true;
        }

        private void UpdateEntries()
        {
            this.DataContext = AddressBookData.GetEntries();
            this.entryListBox.ItemsSource = AddressBookData.GetEntries().OrderBy(item => item.Name);
        }

        /// <summary>
        /// Enables/disables View/Edit address buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox entryList = sender as ListBox;
            viewAddress.IsEnabled = (entryList != null && entryList.SelectedIndex > -1) ? true : false;
            editAddress.IsEnabled = (entryList != null && entryList.SelectedIndex > -1) ? true : false;
            deleteAddress.IsEnabled = (entryList != null && entryList.SelectedIndex > -1) ? true : false;
        }

        private void editAddress_Click(object sender, RoutedEventArgs e)
        {
            EditAddress editAddressPage = new EditAddress(this.entryListBox.SelectedItem);
            this.NavigationService.Navigate(editAddressPage);
        }

        private void viewAddress_Click(object sender, RoutedEventArgs e)
        {
            ViewAddress viewAddressPage = new ViewAddress(this.entryListBox.SelectedItem);
            this.NavigationService.Navigate(viewAddressPage);
        }

        private void addAddress_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddAddress());
        }

        private void deleteAddress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressBookData.DeleteEntry(this.entryListBox.SelectedItem as AddressBookDBDataSet.PersonRow);
                MessageBox.Show("The entry has been removed from the address book.");
                UpdateEntries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

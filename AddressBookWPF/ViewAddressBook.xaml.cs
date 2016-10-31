using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for ViewAddressBook.xaml
    /// </summary>
    public partial class ViewAddressBook : Page
    {
        string fileName;

        public ViewAddressBook()
        {
            InitializeComponent();
        }

        public ViewAddressBook(string fileName) : this()
        {
            try
            {
                this.DataContext = ReaderWriter.GetXmlDocument(fileName);
                this.fileName = fileName;
                List<XmlElement> elems = (this.DataContext as XmlDocument).SelectNodes("Addresses/Person").Cast<XmlElement>().ToList();
                this.entryListBox.ItemsSource = elems.OrderBy(item => item.GetAttribute("Name").ToString());
                addAddress.IsEnabled = true;
                closeAddressBook.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        }

        private void editAddress_Click(object sender, RoutedEventArgs e)
        {
            EditAddress editAddressPage = new EditAddress(this.entryListBox.SelectedItem, fileName);
            this.NavigationService.Navigate(editAddressPage);
        }

        private void viewAddress_Click(object sender, RoutedEventArgs e)
        {
            ViewAddress viewAddressPage = new ViewAddress(this.entryListBox.SelectedItem, fileName);
            this.NavigationService.Navigate(viewAddressPage);
        }

        private void addAddress_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddAddress(fileName));
        }

        private void closeAddressBook_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddressBookHome());
        }
    }
}

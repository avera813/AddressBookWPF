using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for EditAddress.xaml
    /// </summary>
    public partial class EditAddress : Page
    {
        string oldName;
        string fileName;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data, string fileName) : this()
        {
            this.DataContext = data;
            this.fileName = fileName;
            oldName = (data as XmlElement).GetAttribute("Name");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressForm.Validate(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                XmlDocument xmlDoc = ReaderWriter.GetXmlDocument(fileName);

                // Set current node based on initial name value
                XmlElement personElem = AddressForm.CreateNewPerson(xmlDoc, name.Text, oldName);

                // Create new address element and add to currElem
                XmlElement addressElem = AddressForm.CreateAddressElement(xmlDoc, street.Text, city.Text, state.Text, zip.Text, country.Text);
                personElem.PrependChild(addressElem);
                        
                ReaderWriter.WriteXmlToDocument(fileName, xmlDoc);
                MessageBox.Show("The entry has been updated.");
                this.NavigationService.Navigate(new ViewAddressBook(fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook(fileName));
        }
    }
}

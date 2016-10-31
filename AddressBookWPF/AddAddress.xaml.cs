using System;
using System.Collections.Generic;
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
        string fileName;

        public AddAddress()
        {
            InitializeComponent();
        }

        public AddAddress(string fileName) : this()
        {
            this.fileName = fileName;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook(fileName));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressForm.Validate(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                XmlDocument xmlDoc = ReaderWriter.GetXmlDocument(fileName);

                // Set current node based on initial name value
                XmlElement personElem = AddressForm.CreateNewPerson(xmlDoc, name.Text);

                // Create new address element and add attributes
                XmlElement addressElem = AddressForm.CreateAddressElement(xmlDoc, street.Text, city.Text, state.Text, zip.Text, country.Text);

                // Append address to person element
                personElem.PrependChild(addressElem);

                // Append person element to xmlDoc
                xmlDoc.SelectSingleNode("Addresses").AppendChild(personElem);

                ReaderWriter.WriteXmlToDocument(fileName, xmlDoc);
                MessageBox.Show("The entry has been added.");
                this.NavigationService.Navigate(new ViewAddressBook(fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

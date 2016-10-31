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
                List<XmlElement> nodes = xmlDoc.SelectNodes("//Person").Cast<XmlElement>().Where(item => item.GetAttribute("Name").ToLower().Equals(name.Text.ToLower())).ToList();

                // Make sure name does not already exist in the address book
                if (nodes.Count > 0)
                    throw new ArgumentException("An entry with the same name already exists.");

                // Set current node based on initial name value
                XmlElement personElem = xmlDoc.CreateElement("Person");
                personElem.SetAttribute("Name", name.Text);

                // Create new address element and add attributes
                XmlElement addressElem = xmlDoc.CreateElement("Address");
                addressElem.SetAttribute("Street", street.Text);
                addressElem.SetAttribute("City", city.Text);
                addressElem.SetAttribute("State", state.Text);
                addressElem.SetAttribute("Zip", zip.Text);
                addressElem.SetAttribute("Country", country.Text);

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
